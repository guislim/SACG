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
    public partial class FrmCadClienteConstrucao : Form
    {
        private Cliente cliente = null;
        private FrmConsultaClienteConstrucao consulta;
        public FrmCadClienteConstrucao(FrmConsultaClienteConstrucao consulta)
        {
            InitializeComponent();
            init(consulta);

        }
        public FrmCadClienteConstrucao()
        {
            InitializeComponent();
            cbgenero.SelectedIndex = 0;
            cbuf.SelectedIndex = 0;
        }
        public FrmCadClienteConstrucao(FrmConsultaClienteConstrucao consulta, Cliente cliente)
        {
            InitializeComponent();
            init(consulta);
            this.cliente = cliente;
            txtcpf.Enabled = false;
            txtnome.Enabled = false;
            PreencherCampos();
        }

        public void init(FrmConsultaClienteConstrucao consulta)
        {
            cbgenero.SelectedIndex = 0;
            cbuf.SelectedIndex = 0;
            this.consulta = consulta;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void PreencherCampos()
        {
            txtnome.Text = cliente.nome;
            txtcpf.Text = cliente.cpf;
            txtemail.Text = cliente.email;
            txttelefone.Text = cliente.telefone;
            txtrua.Text = cliente.endereco.rua;
            txtbairro.Text = cliente.endereco.bairro;
            txtcomplemento.Text = cliente.endereco.complemento;
            txtcidade.Text = cliente.endereco.cidade;
            txtcep.Text = cliente.endereco.cep;
            txtnumero.Text = cliente.endereco.numero+"";
            cbuf.Text = cliente.endereco.uf;
            txtrg.Text = cliente.rg;
            txtdata.Value = cliente.data;
            txtcelular.Text = cliente.celular;
            cbgenero.Text = cliente.genero;
        }

        public bool validacao()
        {
            bool cond = true;
            string erro = "";
            if (txtnome.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o nome do cliente\n";
            }
            if (txtcep.Text.Length != 9)
            {
                cond = false;
                erro += "Preencha corretamente o cep no endereço do cliente\n";
            }

            if (!Validador.ValidaCPF(txtcpf.Text))
            {
                cond = false;
                erro += "Insira um cpf válido\n";
            }

            if (txtrua.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha a rua no endereço do cliente\n";
            }

            if (txtbairro.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o bairro no endereço do cliente\n";
            }
            if (txtcidade.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha a cidade no endereço do cliente\n";
            }

            if (txtnumero.Text.Length == 0)
            {
                cond = false;
                erro += "Preencha o número residencial no endereço do cliente\n";
            }
            else
            {
                try
                {
                    Convert.ToInt32(txtnumero.Text);
                }
                catch(Exception e){
                    cond = false;
                    erro += "Insira um número residencial válido\n";
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
                cmd.CommandText = "insert into pessoa_fisica(nome,email,cpf,telefone,id_endereco,rg,data_nascimento,genero,celular) values(@a,@b,@c,@d,@e,@f,@g,@h,@i)";
                cmd.Parameters.AddWithValue("@a", txtnome.Text);
                cmd.Parameters.AddWithValue("@b", txtemail.Text);
                cmd.Parameters.AddWithValue("@c", txtcpf.Text);
                string telefone = "",celular="";
                if (txttelefone.Text.Length == 14)
                    telefone = txttelefone.Text;
                if (txtcelular.Text.Length == 14)
                    celular = txtcelular.Text;
                cmd.Parameters.AddWithValue("@d", telefone);
                cmd.Parameters.AddWithValue("@e", id);
                cmd.Parameters.AddWithValue("@f", txtrg.Text);
                cmd.Parameters.AddWithValue("@g", txtdata.Value);
                cmd.Parameters.AddWithValue("@h", cbgenero.Text);
                cmd.Parameters.AddWithValue("@i", celular);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText="select max(id_pessoa_fisica) from pessoa_fisica";
                id= Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "insert into cliente(id_pessoa_fisica) values(" + id + ")";
                cmd.ExecuteNonQuery();
                con.Close();
                if (consulta != null)
                consulta.PreencheGridGeral();
                MessageBox.Show("Cliente Cadastrado com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                cmd.Parameters.AddWithValue("@h", cliente.endereco.id_endereco);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = "update pessoa_fisica set nome=@a,email=@b,cpf=@c,telefone=@d,rg=@f,data_nascimento=@g,genero=@h,celular=@i where id_pessoa_fisica =@e";
                cmd.Parameters.AddWithValue("@a", txtnome.Text);
                cmd.Parameters.AddWithValue("@b", txtemail.Text);
                cmd.Parameters.AddWithValue("@c", txtcpf.Text);
                string telefone = "",celular="";
                if (txttelefone.Text.Length == 14)
                    telefone = txttelefone.Text;
                if (txtcelular.Text.Length == 14)
                    celular = txtcelular.Text;
                cmd.Parameters.AddWithValue("@d", telefone);
                cmd.Parameters.AddWithValue("@e", cliente.id_cliente);
                cmd.Parameters.AddWithValue("@f", txtrg.Text);
                cmd.Parameters.AddWithValue("@g", txtdata.Value);
                cmd.Parameters.AddWithValue("@h", cbgenero.Text);
                cmd.Parameters.AddWithValue("@i", celular);
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
            if (cliente == null)
                gravar();
            else
                editar();
        }

        private void FrmCadCliente_Load(object sender, EventArgs e)
        {

        }

        private void FrmCadClienteConstrucao_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(FrmPrincipal.lista_telas.Contains(5))
                FrmPrincipal.lista_telas.Remove(5);
        }
    }
}
