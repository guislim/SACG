using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Generico.Modelo;
using MySql.Data.MySqlClient;
namespace Generico.Visao.Construcao
{
    public partial class FrmContasReceberAtrazadas : Form
    {
        private List<ParcelaReceber> lista;
        public FrmContasReceberAtrazadas(List<ParcelaReceber> lista)
        {
            InitializeComponent();
            this.lista = lista;
            inicializa_tabela(lista);
        }

        public FrmContasReceberAtrazadas()
        {
            InitializeComponent();
            List<ParcelaReceber>  lista = geraLista();
            inicializa_tabela(lista);
        }


        public void inicializa_tabela(List<ParcelaReceber> lista)
        {
            foreach(ParcelaReceber p in lista){
                dataGridView1.Rows.Add(new String[]{p.conta_receber.cliente.nome,p.data_vencimento.ToShortDateString(),p.valor.ToString()});
            }
        }

        public List<ParcelaReceber> geraLista()
        {
            string data_atual = GeraDataMysql.Gerar(DateTime.Now);
            string query = "select * from parcela_receber as p inner join conta_receber as c on p.id_conta_receber = c.id_conta_receber inner join cliente as f on f.id_cliente = c.id_cliente ";
            query += "inner join pessoa_fisica as j on f.id_pessoa_fisica = j.id_pessoa_fisica ";
            query += "where p.data_vencimento <='" + data_atual + "' and c.status=1 and p.situacao='ABERTA'";
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<ParcelaReceber> lista = new List<ParcelaReceber>();

            while (reader.Read())
            {
                ContaReceber c = new ContaReceber();
                ParcelaReceber p = new ParcelaReceber();
                Cliente cli = new Cliente();
                cli.nome = reader["nome"].ToString();
                c.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                c.valor_total = Convert.ToDecimal(reader["valor_total"].ToString());
                c.valor_pago = Convert.ToDecimal(reader["valor_pago"].ToString());
                c.situacao = "ABERTA";
                c.id_conta_receber = Convert.ToInt32(reader["id_conta_receber"].ToString());
                c.data_emissao = DateTime.Parse(reader["data_emissao"].ToString());
                c.documento = reader["documento"].ToString();
                c.entrada = Convert.ToDecimal(reader["entrada"].ToString());
                p.valor = Convert.ToDecimal(reader["valor"].ToString());
                p.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                p.conta_receber = c;
                c.cliente = cli;
                lista.Add(p);
            }
            con.Close();
            return lista;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGerenciarContaReceber frm = new FrmGerenciarContaReceber(lista[dataGridView1.CurrentRow.Index].conta_receber, null);
            frm.Show();
        }

        private void FrmContasReceberAtrazadas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lista == null)
                FrmPrincipal.lista_telas.Remove(11);
        }
    }
}
