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
    public partial class FrmContasPagar : Form
    {
        private Fornecedor fornecedor;
        public FrmContasPagar()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {

            txtdataInicial.Value = DateTime.Now;
            txtdatafinal.Value = DateTime.Now.AddMonths(1);
            verificar_todos();
            filtragem();
           
        }
        public void pesquisar(string query)
        {
            tabela.Rows.Clear();
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            string razao="",nome="";
            while (reader.Read())
            {
                if(reader["razao"]!=null)
                    razao=reader["razao"].ToString();
                string codigo = reader["CODIGO"].ToString();
                string total = String.Format("{0:C}",Convert.ToDecimal(reader["total"].ToString()));
                string valor = String.Format("{0:C}", Convert.ToDecimal(reader["valor"].ToString()));
                tabela.Rows.Add(new String[] { codigo, reader["credor"].ToString(), valor, razao, reader["data"].ToString().Replace("00:00:00", ""), total, reader["situacao"].ToString() });
            }
            reader.Close();
            con.Close();
        }

        


        private void button2_Click(object sender, EventArgs e)
        {
            fornecedor = null;
            txtFornecedor.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmCadContaPagar cad = new FrmCadContaPagar(this);
            cad.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                ContaPagar c = GerarConta();
                FrmCadParcela cad = new FrmCadParcela(c, this);
                cad.Show();
            }
        }

        public ContaPagar GerarConta()
        {
            ContaPagar c = new ContaPagar();
            int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            string query = "select * from conta_pagar where id_conta_pagar ="+id;
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            c.id_conta_pagar = id;
            c.numDias = Convert.ToInt32(reader["num_dias"].ToString());
            c.valor_total = Convert.ToDecimal(reader["valor_total"].ToString());
            c.numParcelas = c.numDias = Convert.ToInt32(reader["num_parcelas"].ToString());
            c.situacao = reader["situacao"].ToString();
            c.credor = reader["credor"].ToString();
            c.valor_pago = Convert.ToDecimal(reader["valor_pago"].ToString());
            c.data_emissao = DateTime.Parse(reader["data_emissao"].ToString());
            c.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
            c.entrada = Convert.ToDecimal(reader["entrada"].ToString());
            string id_forn=reader["id_fornecedor"].ToString();
            reader.Close();
            if(id_forn.Length!=0){
            query = "select * from fornecedor as f inner join pessoa_juridica as p on f.id_pessoa_juridica=p.id_pessoa_juridica where f.id_fornecedor=" + id_forn;
            
            cmd.CommandText = query;
            reader = cmd.ExecuteReader();
            reader.Read();
           while (reader.Read())
            {
                Fornecedor f = new Fornecedor();
                f.celular = reader["celular"].ToString();
                f.cnpj = reader["p.cnpj"].ToString();
                f.email = reader["p.email"].ToString();
                f.razao_social = reader["p.razao_social"].ToString();
                f.telefone = f.celular = reader["p.telefone"].ToString();
                f.inscricao_estadual = f.celular = reader["p.inscricao_estadual"].ToString();
                f.id_pessoa_juridica = Convert.ToInt32(reader["p.id_pessoa_juridica"]);
                c.fornecedor = f;
            }
            }
            else
            c.fornecedor=null;
            con.Close();
            return c;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                FrmGerenciarContaPagar g = new FrmGerenciarContaPagar(GerarConta(), this);
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
            if (situacao.Equals("QUITADA"))
            {
                MessageBox.Show("Esta conta ja esta quitada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja quitar essa conta?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sql = "update conta_pagar set situacao='QUITADA',valor_pago=" + valor;
                    sql += " where id_conta_pagar=" + id;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    List<int> lista_id = new List<int>();
                    cmd.CommandText = "select id_parcela_pagar from parcela_pagar where id_conta_pagar= "+id;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        lista_id.Add(reader.GetInt32(0));
                    reader.Close();
                    for (int i = 0; i < lista_id.Count; i++)
                    {
                        id = lista_id[i];
                        sql = "update parcela_pagar set situacao='QUITADA' where id_parcela_pagar=" + id;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    filtragem();
                    MessageBox.Show("Conta quitada com sucesso", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        public void modificacao_situacao(string sit,string mensagem)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                string situacao = tabela.CurrentRow.Cells[tabela.ColumnCount - 1].Value.ToString();
                if (situacao.Equals("QUITADA"))
                {
                    MessageBox.Show("Esta conta ja esta quitada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                    string sql = "update conta_pagar set situacao='"+sit+"' where id_conta_pagar=" + id;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    filtragem();
                    MessageBox.Show(mensagem, "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            modificacao_situacao("CANCELADA", "Conta cancelada com sucesso");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            modificacao_situacao("ABERTA", "Conta ativada com sucesso");
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
                if (situacao.Equals("QUITADA"))
                {
                    MessageBox.Show("Esta conta ja esta quitada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Se deletar essa conta perderá todos os dados sobre ela\nDeseja realmente deletar esta conta?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                        string sql = "update conta_pagar set status= 0 where id_conta_pagar=" + id;
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

        public void filtragem()
        {
          string order = " order by ";
          string baseQuery = "select c.id_conta_pagar as CODIGO,c.credor as CREDOR,c.valor_total as total,pag.valor as valor,pes.razao_social as razao,pag.data_vencimento as data,c.situacao";
          baseQuery += " from conta_pagar as c left join fornecedor f on c.id_fornecedor=f.id_fornecedor left join funcionario as fun on fun.id_funcionario=c.id_funcionario ";
          baseQuery += " left join pessoa_juridica as pes on pes.id_pessoa_juridica = f.id_pessoa_juridica left join pessoa_fisica as fis on fis.id_pessoa_fisica = fun.id_pessoa_fisica ";
          baseQuery += "left join parcela_pagar as pag on pag.id_conta_pagar = c.id_conta_pagar ";
          string  where = "where c.status=1 ";
          string data_inicial = GeraDataMysql.Gerar(txtdataInicial.Value);
          string data_final = GeraDataMysql.Gerar(txtdatafinal.Value);
          where += "and pag.data_vencimento between '" + data_inicial + "' and '" + data_final + "' ";
          if (!cktodos.Checked)
          {
              if (fornecedor != null)
              {
                  where += "and f.id_fornecedor=" + fornecedor.id_fornecedor;
              }
              else
              {
                  where += "and f.id_fornecedor is null";
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
                  check += "  c.situacao='QUITADA' ";
              }

          }
          else
          {
              check = " c.situacao <>'QUITADA' and c.situacao <> 'ABERTA'";
          }
          where += check + ") ";
            if (rdcodigo.Checked)
            {
                order += " c.id_conta_pagar";
            }
                else
                    if (rddatavencimento.Checked)
                    {
                        order += " pag.data_vencimento desc";
                    }
                    else if (rdfornecedor.Checked)
                    {
                        order += " f.id_fornecedor ";
                    }
                    else order = "";
            pesquisar(baseQuery + where + order);
        }

        private void ckabertas_CheckedChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void ckquitadas_CheckStateChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void rddatavencimento_CheckedChanged(object sender, EventArgs e)
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

        private void txtdataInicial_ValueChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void txtdatafinal_ValueChanged(object sender, EventArgs e)
        {
            filtragem();
        }

        private void txtFornecedor_TextChanged(object sender, EventArgs e)
        {
            filtragem();
        }
        public void seleciona_Fornecedor(Fornecedor f)
        {
            fornecedor = f;

            txtFornecedor.Text = f.razao_social;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSelecionaFornecedor sel = new FrmSelecionaFornecedor(this);
            sel.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            verificar_todos();
        }

        public void verificar_todos()
        {
            bt1.Enabled = !cktodos.Checked;
            bt2.Enabled = !cktodos.Checked;
            filtragem();
        }

        private void FrmContasPagar_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipal.lista_telas.Remove(1);
        }

    }
}
