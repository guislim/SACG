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
namespace Generico
{
    public partial class FrmConsultaCliente : Form
    {
        private BindingSource binding;
        private DataTable table;
        public FrmConsultaCliente()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            cbpesquisa.SelectedIndex = 0;
            table = new DataTable();
            binding = new BindingSource();
            PreencheGridGeral();
            tabela.Columns[1].Visible = false;
            tabela.Columns[2].Width = 180;
            tabela.Columns[4].Width = 130;
            tabela.Columns[6].Width = 150;
        }

        private void FrmConsultaCliente_Load(object sender, EventArgs e)
        {

        }

        public void PreencherGrid(string sql)
        {
            table.Clear();
            MySqlConnection con = new MySqlConnection(ConfigGenerico.Conexao);
            MySqlCommand cmd = new MySqlCommand(sql,con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            binding.DataSource = table;
            tabela.DataSource = binding;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            binding.MoveFirst();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            binding.MovePrevious();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            binding.MoveNext();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            binding.MoveLast();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCadCliente cad = new FrmCadCliente(this);
            cad.Show();
        }
        public void PreencheGridGeral()
        {
            string sql = "select c.id_cliente as CODIGO ,e.id_endereco, c.nome as NOME,c.cpf as CPF,c.email as EMAIL,c.telefone as TELEFONE,e.rua as RUA,e.bairro as BAIRRO,e.numero as NUMERO,e.cidade as CIDADE,e.complemento as COMPLEMENTO,e.cep as CEP,e.uf as UF";
            sql += " from cliente as c inner join endereco as e on c.id_endereco = e.id_endereco where c.status=1 order by c.id_cliente ";
            PreencherGrid(sql);
          }
        public void pesquisa()
        {
            string key = "";
            switch (cbpesquisa.SelectedIndex)
            {
                case 0:
                    key = "c.nome";
                    break;
                case 1:
                    key = "c.cpf";
                    break;
                case 2:
                    key = "e.rua";
                    break;
                case 3:
                    key = "e.bairro";
                    break;
                case 4:
                    key = "e.cidade";
                    break;
            }
            string sql = "select c.id_cliente as CODIGO ,e.id_endereco, c.nome as NOME,c.cpf as CPF,c.email as EMAIL,c.telefone as TELEFONE,e.rua as RUA,e.bairro as BAIRRO,e.numero as NUMERO,e.cidade as CIDADE,e.complemento as COMPLEMENTO,e.cep as CEP,e.uf as UF";
            sql += " from cliente as c inner join endereco as e on c.id_endereco = e.id_endereco where c.status=1 and  "+key+" like '%"+txtpesquisa.Text+"%' order by c.id_cliente";
            PreencherGrid(sql);
        }

        private void txtpesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            pesquisa();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                Cliente cli = new Cliente();
                Endereco end = new Endereco();
                cli.id_cliente = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                end.id_endereco = Convert.ToInt32(tabela.CurrentRow.Cells[1].Value.ToString());
                cli.nome = tabela.CurrentRow.Cells[2].Value.ToString();
                cli.cpf = tabela.CurrentRow.Cells[3].Value.ToString();
                cli.email = tabela.CurrentRow.Cells[4].Value.ToString();
                cli.telefone = tabela.CurrentRow.Cells[5].Value.ToString();
                end.rua = tabela.CurrentRow.Cells[6].Value.ToString();
                end.bairro = tabela.CurrentRow.Cells[7].Value.ToString();
                end.numero = Convert.ToInt32(tabela.CurrentRow.Cells[8].Value.ToString());
                end.cidade = tabela.CurrentRow.Cells[9].Value.ToString();
                end.complemento = tabela.CurrentRow.Cells[10].Value.ToString();
                end.cep = tabela.CurrentRow.Cells[11].Value.ToString();
                end.uf = tabela.CurrentRow.Cells[12].Value.ToString();
                cli.endereco = end;
                FrmCadCliente cad = new FrmCadCliente(this, cli);
                cad.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                DialogResult dr = MessageBox.Show("Se vc excluir este cliente perderá todos os dados referentes\n a ele como relatórios e dados pessoais\nDeseja continuar", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                    string sql = "update cliente set status =0 where id_cliente=" + id;
                    MySqlConnection con = new MySqlConnection(ConfigGenerico.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    PreencheGridGeral();
                    con.Close();
                    MessageBox.Show("O cliente foi removido com sucesso","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }
    }
}
