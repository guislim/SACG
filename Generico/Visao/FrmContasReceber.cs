using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Generico.Modelo;
using Generico.Visao.Construcao;
namespace Generico.Visao.Construcao
{
    public partial class FrmContasReceber : Form
    {
        private String baseQuery,order,where;
        private Cliente cliente;
        public FrmContasReceber()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            txtdataInicial.Value = DateTime.Now;
            txtdatafinal.Value = DateTime.Now.AddMonths(1);
            verifica_todos();
            filtragem();
            tabela.Columns[0].Visible = false;
        }

        public void pesquisar(string query)
        {
            tabela.Rows.Clear();
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            string nome="";
            while (reader.Read())
            {
                if(reader["nome"]!=null)
                    nome=reader["nome"].ToString();
                string codigo = reader["CODIGO"].ToString();
                string total = String.Format("{0:C}",Convert.ToDecimal(reader["total"].ToString()));
                string valor = String.Format("{0:C}", Convert.ToDecimal(reader["valor"].ToString()));
                tabela.Rows.Add(new String[] { codigo, nome, valor,reader["data"].ToString().Replace("00:00:00", ""), total, reader["situacao"].ToString() });
            }
            reader.Close();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cliente = null;
            txtCliente.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmCadContaReceber cad = new FrmCadContaReceber(this);
           cad.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                ContaReceber c = GerarConta();
                FrmCadParcelaReceber cad = new FrmCadParcelaReceber(c, this);
                cad.Show();
            }
        }

        public ContaReceber GerarConta()
        {
            ContaReceber c = new ContaReceber();
            int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            string query = "select * from conta_receber where id_conta_receber ="+id;
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            c.id_conta_receber = id;
            c.numDias = Convert.ToInt32(reader["num_dias"].ToString());
            c.valor_total = Convert.ToDecimal(reader["valor_total"].ToString());
            c.numParcelas = c.numDias = Convert.ToInt32(reader["num_parcelas"].ToString());
            c.situacao = reader["situacao"].ToString();
            c.valor_pago = Convert.ToDecimal(reader["valor_pago"].ToString());
            c.data_emissao = DateTime.Parse(reader["data_emissao"].ToString());
            c.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
            c.entrada = Convert.ToDecimal(reader["entrada"].ToString());
            string id_cli=reader["id_cliente"].ToString();
            reader.Close();
            if(id_cli.Length!=0){
            query = "select * from cliente as f inner join pessoa_fisica as p on f.id_pessoa_fisica =p.id_pessoa_fisica where f.id_cliente=" + id_cli;
            
            cmd.CommandText = query;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cliente f = new Cliente();
                f.celular = reader["p.celular"].ToString();
                f.cpf = reader["p.cpf"].ToString();
                f.email = reader["p.email"].ToString();
                f.nome = reader["p.nome"].ToString();
                f.telefone = f.celular = reader["p.telefone"].ToString();
                f.rg = f.celular = reader["p.rg"].ToString();
                c.cliente = f;
            }
            }
            else
            c.cliente=null;
            con.Close();
            return c;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                FrmGerenciarContaReceber g = new FrmGerenciarContaReceber(GerarConta(), this);
                g.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                quitar();
            }
        }

        public void quitar()
        {
            string situacao = tabela.CurrentRow.Cells[tabela.ColumnCount - 1].Value.ToString();
            int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
            string valor = tabela.CurrentRow.Cells[3].Value.ToString().Replace(".", "").Replace("R$ ","").Replace(',','.');
            if (situacao.Equals("RECEBIDA"))
            {
                MessageBox.Show("Esta conta ja esta quitada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja quitar essa conta?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sql = "update conta_receber set situacao='RECEBIDA',valor_pago=" + valor;
                    sql += " where id_conta_receber=" + id;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    List<int> lista_id = new List<int>();
                    cmd.CommandText = "select id_parcela_receber from parcela_receber where id_conta_receber= "+id;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        lista_id.Add(reader.GetInt32(0));
                    reader.Close();
                    for (int i = 0; i < lista_id.Count; i++)
                    {
                        id = lista_id[i];
                        sql = "update parcela_receber set situacao='RECEBIDA' where id_parcela_receber=" + id;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    filtragem();
                    MessageBox.Show("Conta quitada com sucesso", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            exclusao();
        }

        public void exclusao()
        {
            if (tabela.SelectedRows.Count == 1)
            {
                

                string situacao = tabela.CurrentRow.Cells[tabela.ColumnCount - 1].Value.ToString();
                if (situacao.Equals("RECEBIDA"))
                {
                    MessageBox.Show("Esta conta ja esta quitada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Se deletar essa conta perderá todos os dados sobre ela\nDeseja realmente deletar esta conta?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                        string sql = "update conta_receber set status= 0 where id_conta_receber=" + id;
                        MySqlConnection con = new MySqlConnection(Config.Conexao);
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        filtragem();
                        MessageBox.Show("Conta excluida com sucesso", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSelecionaCliente sel = new FrmSelecionaCliente(this);
            sel.Show();
        }

        public void seleciona_cliente(Cliente cli)
        {
            cliente = cli;
            txtCliente.Text = cli.nome;
        }

        public void filtragem()
        {
           string where = "where c.status=1 ";
           string order = " order by ";
           string baseQuery = "select c.id_conta_receber as CODIGO,c.valor_total as total,rec.valor as valor,pes.razao_social as razao,fis.nome as nome,rec.data_vencimento as data,c.situacao";
            baseQuery += " from conta_receber as c left join cliente as f on c.id_cliente=f.id_cliente ";
            baseQuery += " left join pessoa_juridica as pes on pes.id_pessoa_juridica = f.id_pessoa_juridica left join pessoa_fisica as fis on fis.id_pessoa_fisica = f.id_pessoa_fisica ";
            baseQuery += "left join parcela_receber as rec on rec.id_conta_receber = c.id_conta_receber ";
            string data_inicial = GeraDataMysql.Gerar(txtdataInicial.Value);
            string data_final = GeraDataMysql.Gerar(txtdatafinal.Value);
            where += "and rec.data_vencimento between '" + data_inicial + "' and '" + data_final + "' ";
            if (!cktodos.Checked)
            {
                if (cliente != null)
                {
                    where += "and f.id_cliente=" + cliente.id_cliente;
                }
                else
                {
                    where += "and f.id_cliente is null";
                }
            }
                string check = "";
                where += " and(";
                if (ckabertas.Checked || ckquitadas.Checked)
                {


                    if (ckabertas.Checked)
                    {

                        check += " c.situacao='ABERTA' ";
                    }
                    if (ckquitadas.Checked)
                    {
                        if (check.Length > 0)
                            check += " or ";
                        check += "  c.situacao='RECEBIDA' ";
                    }

                }
                else
                {
                    check = " c.situacao <>'RECEBIDA' and c.situacao <> 'ABERTA'";
                }
                where += check + ") ";
            
            if (rdcodigo.Checked)
            {
                order += " c.id_conta_pagar";
            }
                else
                    if (rddatavencimento.Checked)
                    {
                        order += " rec.data_vencimento desc";
                    }
                    else if (rdfornecedor.Checked)
                    {
                        order += " f.id_cliente ";
                    }
                    else order = "";
            pesquisar(baseQuery + where + order);
        }

        private void txtdataInicial_ValueChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void txtdatafinal_ValueChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void rdcodigo_CheckedChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void rdfornecedor_CheckedChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void rddataemissao_CheckedChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void rddatavencimento_CheckedChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void ckquitadas_CheckedChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void ckabertas_CheckedChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            verifica_todos();
        }

        public void verifica_todos()
        {
            bt1.Enabled = !cktodos.Checked;
            bt2.Enabled = !cktodos.Checked;
            filtragem();
        }

        private void FrmContasReceber_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipal.lista_telas.Remove(4);
        }
        //CODIGO NAO APROVEITADO

        /* public void modificacao_situacao(string sit,string mensagem)
     {
         if (tabela.SelectedRows.Count == 1)
         {
             string situacao = tabela.CurrentRow.Cells[tabela.ColumnCount - 1].Value.ToString();
             if (situacao.Equals("RECEBIDA"))
             {
                 MessageBox.Show("Esta conta ja esta quitada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
             }
             else
             {
                 int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                 string sql = "update conta_receber set situacao='"+sit+"' where id_conta_receber=" + id;
                 MySqlConnection con = new MySqlConnection(Config.Conexao);
                 MySqlCommand cmd = new MySqlCommand(sql, con);
                 con.Open();
                 cmd.ExecuteNonQuery();
                 con.Close();
                 pesquisar_todos();
                 MessageBox.Show(mensagem, "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
         }
     }*/

        /*public void pesquisar_todos()
      {
          pesquisar(baseQuery+where);
      }*/
    }
}
