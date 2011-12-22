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
    public partial class FrmMovimentacaoProduto : Form
    {
        private BindingSource binding;
        private DataTable table;
        public FrmMovimentacaoProduto()
        {
            InitializeComponent();
            init();
            cboperacao.SelectedIndex = 0;
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
            movimentacao();
        }

        public void movimentacao()
        {
            if (cboperacao.SelectedIndex == 0)
                entrada();
            else
                saida();
        }
       
        public void entrada(){
            if (numericUpDown1.Value > 0)
            {
               DialogResult dr= MessageBox.Show("Deseja confirmar esta entrada de produtos?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (dr == DialogResult.Yes)
               {
                   Produto p = getProduto();
                   string sql = "update produto set estoque = estoque+"+numericUpDown1.Value+" where id_produto="+p.id_produto;
                   MySqlConnection con = new MySqlConnection(Config.Conexao);
                   con.Open();
                   MySqlCommand cmd = new MySqlCommand(sql, con);
                   cmd.ExecuteNonQuery();
                   con.Close();
                   pesquisa();
                   MessageBox.Show("Estoque atualizado com sucesso", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
            }
            else
            {
                MessageBox.Show("Insira uma quantidade maior que 0","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        public void saida()
        {
            if (numericUpDown1.Value > 0)
            {
                DialogResult dr = MessageBox.Show("Deseja confirmar esta saída de produtos?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Produto p = getProduto();
                    if (numericUpDown1.Value > p.estoque)
                    {
                        MessageBox.Show("Voce nao tem essa quantidade no estoque", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string sql = "update produto set estoque = estoque-" + numericUpDown1.Value + " where id_produto=" + p.id_produto;
                        MySqlConnection con = new MySqlConnection(Config.Conexao);
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        pesquisa();
                        MessageBox.Show("Estoque atualizado com sucesso", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Insira uma quantidade maior que 0", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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


        private void FrmConsultaFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmPrincipal.lista_telas.Remove(9);
        }

        private void cbpesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpesquisa.Text = "";
            txtpesquisa.Focus();
        }
    }
}
