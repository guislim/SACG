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
    public partial class FrmCadProduto : Form
    {
        private Fornecedor fornecedor;
        private Produto produto;
        private FrmConsultaProduto consulta;
        public FrmCadProduto(FrmConsultaProduto consulta)
        {
            InitializeComponent();
            init(consulta);

        }
        public FrmCadProduto()
        {
            InitializeComponent();
        }
        public FrmCadProduto(FrmConsultaProduto consulta, Produto produto)
        {
            InitializeComponent();
            init(consulta);
            this.produto=produto;
            this.fornecedor = produto.fornecedor;
            PreencherCampos();
            //txtdescricao.Enabled = false;
            txtcodigo.Enabled = false;
            txtquantidade_inicial.Enabled = false;
            txtunidade.Enabled = false;

        }

        public void init(FrmConsultaProduto consulta)
        {
            this.consulta = consulta;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void PreencherCampos()
        {
            txtcodigo.Text = produto.codigo;
            txtdescricao.Text = produto.descricao;
            txtquantidade_inicial.Text = produto.estoque.ToString();
            txtquantidade_minima.Text = produto.quantidade_minima.ToString();
            txtpreco_compra.Text = produto.preco_compra.ToString();
            txtfornecedor.Text = fornecedor.razao_social;
            txtpreco_venda.Text = produto.preco_venda.ToString();
            txtunidade.Text = produto.unidade;
        }

        public bool validacao()
        {
            bool cond = true;
            string erro = "";
            if (txtcodigo.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o campo codigo \n";
            }

            if (txtdescricao.Text.Length==0)
            {
                cond = false;
                erro += "Preencha o campo descricao\n";
            }

            if (fornecedor==null)
            {
                cond = false;
                erro += "Selecione um fornecedor \n";
            }

                try
                {
                  Convert.ToInt64(txtquantidade_inicial.Text);
                }
                catch(Exception e){
                    cond = false;
                    erro += "Insira uma quantidade inicial válida\n";
                }
                try
                {
                    Convert.ToInt32(txtquantidade_minima.Text);
                }
                catch (Exception e)
                {
                    cond = false;
                    erro += "Insira uma quantidade minima válida\n";
                }
                try
                {
                    Convert.ToDecimal(txtpreco_compra.Text);
                }
                catch (Exception e)
                {
                    cond = false;
                    erro += "Digite um preço de compra válido\n";
                }
                if (txtpreco_venda.Text.Length == 0)
                {
                    cond = false;
                    erro += "Insira o preço de venda\n";
                }
            
            if (!cond)
                MessageBox.Show(erro, "Corrija os seguintes erros:", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return cond;
        }

        public void gravar()
        {
            if (validacao())
            {
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                MySqlCommand cmd = new MySqlCommand("insert into produto(codigo,descricao,estoque,quantidade_minima,preco_compra,preco_venda,unidade,data_cadastro,id_fornecedor) values(@a,@b,@c,@d,@e,@f,@g,@h,@i)");
                cmd.Parameters.AddWithValue("@a", txtcodigo.Text);
                cmd.Parameters.AddWithValue("@b", txtdescricao.Text);
                cmd.Parameters.AddWithValue("@c", txtquantidade_inicial.Text);
                cmd.Parameters.AddWithValue("@d", txtquantidade_minima.Text);
                cmd.Parameters.AddWithValue("@e", Convert.ToDecimal(txtpreco_compra.Text));
                cmd.Parameters.AddWithValue("@f", Convert.ToDecimal(txtpreco_venda.Text));
                cmd.Parameters.AddWithValue("@g", txtunidade.Text);
                cmd.Parameters.AddWithValue("@h", DateTime.Now);
                cmd.Parameters.AddWithValue("@i", fornecedor.id_fornecedor);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                if(consulta!=null)
                consulta.PreencheGridGeral();
                MessageBox.Show("Produto Cadastrado com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        public void editar()
        {
            if (validacao())
            {
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                MySqlCommand cmd = new MySqlCommand("update produto set codigo=@a,descricao=@b,quantidade_minima=@d,preco_compra=@e,preco_venda=@f,unidade=@g, id_fornecedor = @i where id_produto =@j ");
                cmd.Parameters.AddWithValue("@a", txtcodigo.Text);
                cmd.Parameters.AddWithValue("@b", txtdescricao.Text);
                cmd.Parameters.AddWithValue("@d", txtquantidade_minima.Text);
                cmd.Parameters.AddWithValue("@e", Convert.ToDecimal(txtpreco_compra.Text));
                cmd.Parameters.AddWithValue("@f", Convert.ToDecimal(txtpreco_venda.Text));
                cmd.Parameters.AddWithValue("@g", txtunidade.Text);
                cmd.Parameters.AddWithValue("@i", fornecedor.id_fornecedor);
                cmd.Parameters.AddWithValue("@j", produto.id_produto);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                if (consulta != null)
                consulta.PreencheGridGeral();
                MessageBox.Show("Os dados foram editados com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (produto == null)
                gravar();
            else
                editar();
        }

        private void FrmCadFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(FrmPrincipal.lista_telas.Contains(8))
            FrmPrincipal.lista_telas.Remove(8);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmSelecionaFornecedor sel = new FrmSelecionaFornecedor(this);
            sel.Show();
        }

        public void seleciona_fornecedor(Fornecedor f)
        {
            txtfornecedor.Text = f.razao_social;
            this.fornecedor = f;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chama_calculo_juros();
        }

        public void chama_calculo_juros()
        {
            if (produto == null)
            {
                //Inicializa a variavel valor para ele ter um escopo valido para o restante do metodo
                decimal valor = 0;
                try
                {
                    //converte o texto do campo preco de compra para o valor decimal
                    valor = Convert.ToDecimal(txtpreco_compra.Text);
                }
                catch (Exception t)
                {
                    //Erro é exibido se conversao falhar
                    MessageBox.Show("insira um valor válido no preço de compra", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FrmCalculoJuros j = new FrmCalculoJuros(valor, this);
                j.Show();
            }
            else
            {
                decimal val = produto.preco_venda - produto.preco_compra;
                decimal juro_calculado = (val * 100) / produto.preco_compra;
                string texto = String.Format("{0:F2}",juro_calculado);
                juro_calculado = Convert.ToDecimal(texto);
                //Inicializa a variavel valor para ele ter um escopo valido para o restante do metodo
                decimal valor = 0;
                
                try
                {
                    //converte o texto do campo preco de compra para o valor decimal
                    valor = Convert.ToDecimal(txtpreco_compra.Text);
                }
                catch (Exception t)
                {
                    //Erro é exibido se conversao falhar
                    MessageBox.Show("insira um valor válido no preço de compra", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FrmCalculoJuros j = new FrmCalculoJuros(valor, this,juro_calculado);
                j.Show();
            }
        }

        public void determinaValorVenda(decimal valor)
        {
            txtpreco_venda.Text = valor.ToString();
        }

    }
}
