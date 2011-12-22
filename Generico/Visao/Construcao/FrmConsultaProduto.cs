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
    public partial class FrmConsultaProduto : Form
    {
        private BindingSource binding;
        private DataTable table;
        public FrmConsultaProduto()
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
            tabela.Columns[0].Visible = false;
            tabela.Columns[1].Visible = false;
            tabela.Columns[9].Visible = false;
            tabela.Columns[10].Visible = false;
            tabela.Columns[4].Width = 180;
            tabela.Columns[6].Width = 137;
            tabela.Columns[7].Width = 130;
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
            FrmCadProduto cad = new FrmCadProduto(this);
            cad.Show();
        }
        public void PreencheGridGeral()
        {
            string sql = "select p.id_produto, f.id_fornecedor, p.codigo as CODIGO, p.descricao as 'DESCRICAO', p.estoque as 'QUANTIDADE EM ESTOQUE',p.unidade as 'UNIDADE',p.preco_compra as 'PRECO DE COMPRA',p.preco_venda as 'PRECO DE VENDA', j.razao_social as 'FORNECEDOR',p.data_cadastro,p.quantidade_minima ";
            sql += " from pessoa_juridica as j  inner join fornecedor as f on j.id_pessoa_juridica = f.id_pessoa_juridica ";
            sql+="inner join produto as p on p.id_fornecedor = f.id_fornecedor where p.status=1 order by p.codigo ";
            PreencherGrid(sql);
          }
        public void pesquisa()
        {
            string key = "";
            switch (cbpesquisa.SelectedIndex)
            {
                case 0:
                    key = "p.codigo";
                    break;
                case 1:
                    key = "p.descricao";
                    break;
                case 2:
                    key = "preco_compra";
                    break;
                case 3:
                    key = "preco_venda";
                    break;
                case 4:
                    key = "estoque";
                    break;
                   
            }
            string sql = "";
           
                sql = "select p.id_produto, f.id_fornecedor, p.codigo as CODIGO, p.descricao as 'DESCRICAO', p.estoque as 'QUANTIDADE EM ESTOQUE',p.unidade as 'UNIDADE',p.preco_compra as 'PRECO DE COMPRA',p.preco_venda as 'PRECO DE VENDA', j.razao_social as 'FORNECEDOR',p.data_cadastro,p.quantidade_minima ";
                sql += " from pessoa_juridica as j  inner join fornecedor as f on j.id_pessoa_juridica = f.id_pessoa_juridica ";
                sql += "inner join produto as p on p.id_fornecedor = f.id_fornecedor where p.status=1 and ";
                sql += key + " like '%" + txtpesquisa.Text + "%' order by p.codigo";

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
                DialogResult dr = MessageBox.Show("Se vc excluir este produto perderá todos os dados referentes a ele\n como relatórios e dados em geral\nDeseja continuar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                    string sql = "update produto set status =0 where id_produto=" + id;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    PreencheGridGeral();
                    con.Close();
                    MessageBox.Show("O produto foi removido com sucesso","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }
        public void alterar()
        {
            if (tabela.SelectedRows.Count == 1)
            {
                FrmCadProduto cad = new FrmCadProduto(this, getProduto());
                cad.Show();
            }
        }

        public Produto getProduto()
        {
           Produto p = new Produto();
           Fornecedor forn = new Fornecedor();
           p.id_produto = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
           forn.id_fornecedor = Convert.ToInt32(tabela.CurrentRow.Cells[1].Value.ToString());
           p.codigo = tabela.CurrentRow.Cells[2].Value.ToString();
           p.descricao = tabela.CurrentRow.Cells[3].Value.ToString();
           p.estoque= Convert.ToInt64( tabela.CurrentRow.Cells[4].Value.ToString());
           p.unidade = tabela.CurrentRow.Cells[5].Value.ToString();
           p.preco_compra = Convert.ToDecimal(tabela.CurrentRow.Cells[6].Value.ToString());
           p.preco_venda = Convert.ToDecimal(tabela.CurrentRow.Cells[7].Value.ToString());
           forn.razao_social = tabela.CurrentRow.Cells[8].Value.ToString();
           p.data_cadastro = DateTime.Parse(tabela.CurrentRow.Cells[9].Value.ToString());
           p.quantidade_minima = Convert.ToInt32(tabela.CurrentRow.Cells[10].Value.ToString());
           p.fornecedor = forn;
           return p;
        }

        private void tabela_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            alterar();
        }

        private void FrmConsultaFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipal.lista_telas.Remove(7);
        }

        private void cbpesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpesquisa.Text = "";
            txtpesquisa.Focus();
        }
    }
}
