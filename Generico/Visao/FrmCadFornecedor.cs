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
    public partial class FrmCadFornecedor : Form
    {
        private Fornecedor fornecedor;
        private FrmConsultaFornecedor consulta;
        public FrmCadFornecedor(FrmConsultaFornecedor consulta)
        {
            InitializeComponent();
            init(consulta);

        }
        public FrmCadFornecedor()
        {
            InitializeComponent();
            cbuf.SelectedIndex = 0;

        }
        public FrmCadFornecedor(FrmConsultaFornecedor consulta, Fornecedor fornecedor)
        {
            InitializeComponent();
            init(consulta);
            this.fornecedor = fornecedor;
            PreencherCampos();
            txtcnpj.Enabled = false;
            txtrazaosocial.Enabled = false;

        }

        public void init(FrmConsultaFornecedor consulta)
        {
            cbuf.SelectedIndex = 0;
            this.consulta = consulta;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void PreencherCampos()
        {
            txtrazaosocial.Text = fornecedor.razao_social;
            txtcnpj.Text = fornecedor.cnpj;
            txtnomefantasia.Text = fornecedor.nome_fantasia;
            txtie.Text = fornecedor.inscricao_estadual;
            txtemail.Text = fornecedor.email;
            txttelefone.Text = fornecedor.telefone;
            txtrua.Text = fornecedor.pessoa_juridica.endereco.rua;
            txtbairro.Text = fornecedor.pessoa_juridica.endereco.bairro;
            txtcomplemento.Text = fornecedor.pessoa_juridica.endereco.complemento;
            txtcidade.Text = fornecedor.pessoa_juridica.endereco.cidade;
            txtcep.Text = fornecedor.pessoa_juridica.endereco.cep;
            txtnumero.Text = fornecedor.pessoa_juridica.endereco.numero + "";
            cbuf.Text = fornecedor.pessoa_juridica.endereco.uf;
            txtcelular.Text = fornecedor.celular;
        }

        public bool validacao()
        {
            bool cond = true;
            string erro = "";
            if (txtrazaosocial.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o campo razão social \n";
            }
            if (txtcep.Text.Length != 9)
            {
                cond = false;
                erro += "Preencha corretamente o cep\n";
            }

            if (!Validador.ValidaCnpj(txtcnpj.Text))
            {
                cond = false;
                erro += "Insira um cnpj válido\n";
            }

            if (txtrua.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o campo rua \n";
            }

            if (txtbairro.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o  campo bairro\n";
            }
            if (txtcidade.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o campo cidade\n";
            }

            if (txtnumero.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o número do endereço \n";
            }
            else
            {
                try
                {
                    Convert.ToInt32(txtnumero.Text);
                }
                catch(Exception e){
                    cond = false;
                    erro += "Insira um número do endereço válido\n";
                }
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
                MySqlCommand cmd = new MySqlCommand("insert into endereco(rua,bairro,cep,cidade,uf,complemento,numero) values(@a,@b,@c,@d,@e,@f,@g)");
                cmd.Parameters.AddWithValue("@a", txtrua.Text);
                cmd.Parameters.AddWithValue("@b", txtbairro.Text);
                cmd.Parameters.AddWithValue("@c", txtcep.Text);
                cmd.Parameters.AddWithValue("@d", txtcidade.Text);
                cmd.Parameters.AddWithValue("@e", cbuf.Text);
                cmd.Parameters.AddWithValue("@f", txtcomplemento.Text);
                cmd.Parameters.AddWithValue("@g", Convert.ToInt32(txtnumero.Text));
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = "select max(id_endereco) from endereco";
                int id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "insert into pessoa_juridica(razao_social,nome_fantasia,email,cnpj,telefone,id_endereco,inscricao_estadual,celular) values(@raz,@a,@b,@c,@d,@e,@f,@g)";
                cmd.Parameters.AddWithValue("@a", txtnomefantasia.Text);
                cmd.Parameters.AddWithValue("@raz", txtrazaosocial.Text);
                cmd.Parameters.AddWithValue("@b", txtemail.Text);
                cmd.Parameters.AddWithValue("@c", txtcnpj.Text);
                string telefone = "",celular="";
                if (txttelefone.Text.Length == 14)
                    telefone = txttelefone.Text;
                if (txtcelular.Text.Length == 14)
                    celular = txtcelular.Text;
                cmd.Parameters.AddWithValue("@d", telefone);
                cmd.Parameters.AddWithValue("@e", id);
                cmd.Parameters.AddWithValue("@f", txtie.Text);
                cmd.Parameters.AddWithValue("@g", celular);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText="select max(id_pessoa_juridica) from pessoa_juridica";
                id= Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "insert into fornecedor(id_pessoa_juridica) values(" + id + ")";
                cmd.ExecuteNonQuery();
                con.Close();
                if(consulta!=null)
                consulta.PreencheGridGeral();
                MessageBox.Show("fornecedor Cadastrado com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        public void editar()
        {
            if (validacao())
            {
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                MySqlCommand cmd = new MySqlCommand("update endereco set rua=@a,bairro=@b,cep=@c,cidade=@d,uf=@e,complemento=@f,numero=@g where id_endereco =@h ");
                cmd.Parameters.AddWithValue("@a", txtrua.Text);
                cmd.Parameters.AddWithValue("@b", txtbairro.Text);
                cmd.Parameters.AddWithValue("@c", txtcep.Text);
                cmd.Parameters.AddWithValue("@d", txtcidade.Text);
                cmd.Parameters.AddWithValue("@e", cbuf.Text);
                cmd.Parameters.AddWithValue("@f", txtcomplemento.Text);
                cmd.Parameters.AddWithValue("@g", Convert.ToInt32(txtnumero.Text));
                cmd.Parameters.AddWithValue("@h", fornecedor.pessoa_juridica.endereco.id_endereco);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = "update pessoa_juridica set nome_fantasia=@a,email=@b,cnpj=@c,telefone=@d,inscricao_estadual=@e,celular=@f,razao_social=@g where id_pessoa_juridica =@h";
                cmd.Parameters.AddWithValue("@a", txtnomefantasia.Text);
                cmd.Parameters.AddWithValue("@b", txtemail.Text);
                cmd.Parameters.AddWithValue("@c", txtcnpj.Text);
                string telefone = "",celular="";
                if (txttelefone.Text.Length == 14)
                    telefone = txttelefone.Text;
                if (txtcelular.Text.Length == 14)
                    celular = txtcelular.Text;
                cmd.Parameters.AddWithValue("@d", telefone);
                cmd.Parameters.AddWithValue("@h", fornecedor.pessoa_juridica.id_pessoa_juridica);
                cmd.Parameters.AddWithValue("@e", txtie.Text);
                cmd.Parameters.AddWithValue("@f", celular);
                cmd.Parameters.AddWithValue("@g", txtrazaosocial.Text);
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
            if (fornecedor == null)
                gravar();
            else
                editar();
        }

        private void FrmCadFornecedor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(FrmPrincipal.lista_telas.Contains(6))
            FrmPrincipal.lista_telas.Remove(6);
        }

    }
}
