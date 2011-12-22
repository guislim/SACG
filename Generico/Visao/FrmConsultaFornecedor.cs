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
    public partial class FrmConsultaFornecedor : Form
    {
        private BindingSource binding;
        private DataTable table;
        public FrmConsultaFornecedor()
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
            tabela.Columns[9].Width = 150;
            tabela.Columns[tabela.ColumnCount-1].Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCadFornecedor cad = new FrmCadFornecedor(this);
            cad.Show();
        }
        public void PreencheGridGeral()
        {
            string sql = "select f.id_fornecedor as CODIGO ,e.id_endereco, j.razao_social as 'RAZAO SOCIAL',j.nome_fantasia as 'NOME FANTASIA',j.cnpj as CNPJ,j.inscricao_estadual as IE,j.email as EMAIL,j.telefone as TELEFONE,j.celular as CELULAR,e.rua as RUA,e.bairro as BAIRRO,e.numero as NUMERO,e.cidade as CIDADE,e.complemento as COMPLEMENTO,e.cep as CEP,e.uf as UF,j.id_pessoa_juridica";
            sql += " from pessoa_juridica as j inner join endereco as e on j.id_endereco = e.id_endereco inner join fornecedor as f on j.id_pessoa_juridica = f.id_pessoa_juridica where f.status=1 order by f.id_fornecedor ";
            PreencherGrid(sql);
          }
        public void pesquisa()
        {
            string key = "";
            switch (cbpesquisa.SelectedIndex)
            {
                case 0:
                    key = "j.razao_social";
                    break;
                case 1:
                    key = "j.cnpj";
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
            string sql = "select f.id_fornecedor as CODIGO ,e.id_endereco, j.razao_social as 'RAZAO SOCIAL',j.nome_fantasia as 'NOME FANTASIA',j.cnpj as CNPJ,j.inscricao_estadual as IE,j.email as EMAIL,j.telefone as TELEFONE,j.celular as CELULAR,e.rua as RUA,e.bairro as BAIRRO,e.numero as NUMERO,e.cidade as CIDADE,e.complemento as COMPLEMENTO,e.cep as CEP,e.uf as UF,j.id_pessoa_juridica";
            sql += " from pessoa_juridica as j inner join endereco as e on j.id_endereco = e.id_endereco inner join fornecedor as f on j.id_pessoa_juridica = f.id_pessoa_juridica where f.status=1 and  " + key + " like '%" + txtpesquisa.Text + "%' order by f.id_fornecedor";
            PreencherGrid(sql);
        }

        private void txtpesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            pesquisa();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            alterar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                DialogResult dr = MessageBox.Show("Se vc excluir este fornecedor perderá todos os dados referentes\n a ele como relatórios e dados pessoais\nDeseja continuar", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                    string sql = "update fornecedor set status =0 where id_fornecedor=" + id;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    PreencheGridGeral();
                    con.Close();
                    MessageBox.Show("O fornecedor foi removido com sucesso","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }
        public void alterar()
        {
            if (tabela.SelectedRows.Count == 1)
            {
                FrmCadFornecedor cad = new FrmCadFornecedor(this, getFornecedor());
                cad.Show();
            }
        }

        public Fornecedor getFornecedor()
        {
            Fornecedor forn = new Fornecedor();
            Pessoa_Juridica jur = new Pessoa_Juridica();
            Endereco end = new Endereco();
            forn.id_fornecedor = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
            end.id_endereco = Convert.ToInt32(tabela.CurrentRow.Cells[1].Value.ToString());
            forn.razao_social = tabela.CurrentRow.Cells[2].Value.ToString();

            forn.nome_fantasia = tabela.CurrentRow.Cells[3].Value.ToString();
            forn.cnpj = tabela.CurrentRow.Cells[4].Value.ToString();
            forn.inscricao_estadual = tabela.CurrentRow.Cells[5].Value.ToString();
            forn.email = tabela.CurrentRow.Cells[6].Value.ToString();
            forn.telefone = tabela.CurrentRow.Cells[7].Value.ToString();
            forn.celular = tabela.CurrentRow.Cells[8].Value.ToString();
            end.rua = tabela.CurrentRow.Cells[9].Value.ToString();
            end.bairro = tabela.CurrentRow.Cells[10].Value.ToString();
            end.numero = Convert.ToInt32(tabela.CurrentRow.Cells[11].Value.ToString());
            end.cidade = tabela.CurrentRow.Cells[12].Value.ToString();
            end.complemento = tabela.CurrentRow.Cells[13].Value.ToString();
            end.cep = tabela.CurrentRow.Cells[14].Value.ToString();
            end.uf = tabela.CurrentRow.Cells[15].Value.ToString();
            jur.id_pessoa_juridica = Convert.ToInt32(tabela.CurrentRow.Cells[tabela.ColumnCount-1].Value.ToString());
            jur.endereco = end;
            forn.pessoa_juridica=jur;
            return forn;
        }

        private void tabela_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            alterar();
        }

        private void FrmConsultaFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipal.lista_telas.Remove(3);
        }
    }
}
