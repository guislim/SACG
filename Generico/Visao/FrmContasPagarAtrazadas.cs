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
    public partial class FrmContasPagarAtrazadas : Form
    {
        private List<ParcelaPagar> lista;
        public FrmContasPagarAtrazadas(List<ParcelaPagar> lista)
        {
            InitializeComponent();
            this.lista = lista;
            inicializa_tabela(lista);
        }

        public FrmContasPagarAtrazadas()
        {
            InitializeComponent();
            List<ParcelaPagar> lista = geraLista();
            inicializa_tabela(lista);
        }

        public List<ParcelaPagar> geraLista()
        {
            string data_atual = GeraDataMysql.Gerar(DateTime.Now);
            string query = "select * from parcela_pagar as p inner join conta_pagar as c on p.id_conta_pagar = c.id_conta_pagar inner join fornecedor as f on f.id_fornecedor = c.id_fornecedor ";
            query += "inner join pessoa_juridica as j on f.id_pessoa_juridica = j.id_pessoa_juridica ";
            query += "where p.data_vencimento <='" + data_atual + "' and c.status=1 and p.situacao='ABERTA'";
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            List<ParcelaPagar> lista = new List<ParcelaPagar>();

            while (reader.Read())
            {
                ContaPagar c = new ContaPagar();
                ParcelaPagar p = new ParcelaPagar();
                Fornecedor f = new Fornecedor();
                f.razao_social = reader["razao_social"].ToString();
                c.credor = reader["credor"].ToString();
                c.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                c.valor_total = Convert.ToDecimal(reader["valor_total"].ToString());
                c.valor_pago = Convert.ToDecimal(reader["valor_pago"].ToString());
                c.situacao = "ABERTA";
                c.id_conta_pagar = Convert.ToInt32(reader["id_conta_pagar"].ToString());
                c.data_emissao = DateTime.Parse(reader["data_emissao"].ToString());
                c.documento = reader["documento"].ToString();
                c.entrada = Convert.ToDecimal(reader["entrada"].ToString());
                p.valor = Convert.ToDecimal(reader["valor"].ToString());
                p.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                p.conta_pagar = c;
                c.fornecedor = f;
                lista.Add(p);
            }
            con.Close();
            return lista;
        }

        public void inicializa_tabela(List<ParcelaPagar> lista)
        {
            foreach(ParcelaPagar p in lista){
                dataGridView1.Rows.Add(new String[]{p.conta_pagar.credor,p.conta_pagar.fornecedor.razao_social,p.data_vencimento.ToShortDateString(),p.valor.ToString()});
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGerenciarContaPagar frm = new FrmGerenciarContaPagar(lista[dataGridView1.CurrentRow.Index].conta_pagar, null);
            frm.Show();
        }

        private void FrmContasPagarAtrazadas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(lista==null)
            FrmPrincipal.lista_telas.Remove(10);
        }
    }
}
