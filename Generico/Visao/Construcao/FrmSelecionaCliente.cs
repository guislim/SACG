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
namespace Generico
{
    public partial class FrmSelecionaCliente : Form
    {
        private BindingSource binding;
        private DataTable table;
        private FrmContasReceber frame;
        private FrmCadContaReceber cad;
        public FrmSelecionaCliente(FrmContasReceber frame)
        {
            InitializeComponent();
            init();
            this.frame = frame;
        }

        public FrmSelecionaCliente(FrmCadContaReceber cad)
        {
            InitializeComponent();
            init();
            this.cad=cad;
        }

        public void init()
        {
            cbpesquisa.SelectedIndex = 0;
            table = new DataTable();
            binding = new BindingSource();
            PreencheGridGeral();
            tabela.Columns[1].Visible = false;
            tabela.Columns[2].Width = 180;
            tabela.Columns[7].Width = 130;
            tabela.Columns[10].Width = 150;
        }

        private void FrmConsultaCliente_Load(object sender, EventArgs e)
        {

        }

        public void PreencherGrid(string sql)
        {
            table.Clear();
            MySqlConnection con = new MySqlConnection(Config.Conexao);
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

        
        public void PreencheGridGeral()
        {
            string sql = "select c.id_pessoa_fisica as CODIGO ,e.id_endereco, c.nome as NOME,c.cpf as CPF,c.rg as RG,c.data_nascimento,c.genero as GENERO,c.email as EMAIL,c.telefone as TELEFONE,c.celular as CELULAR,e.rua as RUA,e.bairro as BAIRRO,e.numero as NUMERO,e.cidade as CIDADE,e.complemento as COMPLEMENTO,e.cep as CEP,e.uf as UF";
            sql += " from pessoa_fisica as c inner join endereco as e on c.id_endereco = e.id_endereco where c.status=1 order by c.id_pessoa_fisica ";
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
            string sql = "select c.id_pessoa_fisica as CODIGO ,e.id_endereco, c.nome as NOME,c.cpf as CPF,c.rg as RG,c.data_nascimento,c.genero as GENERO,c.email as EMAIL,c.telefone as TELEFONE,c.celular as CELULAR,e.rua as RUA,e.bairro as BAIRRO,e.numero as NUMERO,e.cidade as CIDADE,e.complemento as COMPLEMENTO,e.cep as CEP,e.uf as UF";
            sql += " from pessoa_fisica as c inner join endereco as e on c.id_endereco = e.id_endereco where c.status=1 and  "+key+" like '%"+txtpesquisa.Text+"%' order by c.id_pessoa_fisica";
            PreencherGrid(sql);
        }

        private void txtpesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            pesquisa();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                DialogResult dr = MessageBox.Show("Se vc excluir este cliente perderá todos os dados referentes\n a ele como relatórios e dados pessoais\nDeseja continuar", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                    string sql = "update pessoa_fisica set status =0 where id_pessoa_fisica=" + id;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    PreencheGridGeral();
                    con.Close();
                    MessageBox.Show("O cliente foi removido com sucesso","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }
        public Cliente retorna_cliente()
        {
          
                Cliente cli = new Cliente();
                Endereco end = new Endereco();
                cli.id_cliente = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                end.id_endereco = Convert.ToInt32(tabela.CurrentRow.Cells[1].Value.ToString());
                cli.nome = tabela.CurrentRow.Cells[2].Value.ToString();
                cli.cpf = tabela.CurrentRow.Cells[3].Value.ToString();
                cli.rg = tabela.CurrentRow.Cells[4].Value.ToString();
                string data = tabela.CurrentRow.Cells[5].Value.ToString();
                cli.data = DateTime.Parse(data);
                cli.genero = tabela.CurrentRow.Cells[6].Value.ToString();
                cli.email = tabela.CurrentRow.Cells[7].Value.ToString();
                cli.telefone = tabela.CurrentRow.Cells[8].Value.ToString();
                cli.celular = tabela.CurrentRow.Cells[9].Value.ToString();
                end.rua = tabela.CurrentRow.Cells[10].Value.ToString();
                end.bairro = tabela.CurrentRow.Cells[11].Value.ToString();
                end.numero = Convert.ToInt32(tabela.CurrentRow.Cells[12].Value.ToString());
                end.cidade = tabela.CurrentRow.Cells[13].Value.ToString();
                end.complemento = tabela.CurrentRow.Cells[14].Value.ToString();
                end.cep = tabela.CurrentRow.Cells[15].Value.ToString();
                end.uf = tabela.CurrentRow.Cells[16].Value.ToString();
                cli.endereco = end;
                return cli;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                if (cad == null)
                {
                    frame.seleciona_cliente(retorna_cliente());
                    this.Close();
                }
                else
                {
                    cad.seleciona_cliente(retorna_cliente());
                    this.Close();
                }
            }
        }

    }
}
