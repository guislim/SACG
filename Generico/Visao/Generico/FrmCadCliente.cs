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
using Generico;
namespace Generico
{
    public partial class FrmCadCliente : Form
    {
        private Cliente cliente = null;
        private FrmConsultaCliente consulta;
        public FrmCadCliente(FrmConsultaCliente consulta)
        {
            InitializeComponent();
            init(consulta);

        }
        public FrmCadCliente(FrmConsultaCliente consulta,Cliente cliente)
        {
            InitializeComponent();
            init(consulta);
            this.cliente = cliente;
            PreencherCampos();
        }

        public void init(FrmConsultaCliente consulta)
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

            if (txtcpf.Text.Length !=14)
            {
                cond = false;
                erro += "Preencha o cpf do cliente corretamente\n";
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
                MySqlConnection con = new MySqlConnection(ConfigGenerico.Conexao);
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
                cmd.CommandText = "insert into cliente(nome,email,cpf,telefone,id_endereco) values(@a,@b,@c,@d,@e)";
                cmd.Parameters.AddWithValue("@a", txtnome.Text);
                cmd.Parameters.AddWithValue("@b", txtemail.Text);
                cmd.Parameters.AddWithValue("@c", txtcpf.Text);
                string telefone = "";
                if (txttelefone.Text.Length == 14)
                    telefone = txttelefone.Text;
                cmd.Parameters.AddWithValue("@d", telefone);
                cmd.Parameters.AddWithValue("@e", id);
                cmd.ExecuteNonQuery();
                con.Close();
                consulta.PreencheGridGeral();
                MessageBox.Show("Cliente Cadastrado com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        public void editar()
        {
            if (validacao())
            {
                MySqlConnection con = new MySqlConnection(ConfigGenerico.Conexao);
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
                cmd.CommandText = "update cliente set nome=@a,email=@b,cpf=@c,telefone=@d where id_cliente =@e";
                cmd.Parameters.AddWithValue("@a", txtnome.Text);
                cmd.Parameters.AddWithValue("@b", txtemail.Text);
                cmd.Parameters.AddWithValue("@c", txtcpf.Text);
                string telefone = "";
                if (txttelefone.Text.Length == 14)
                    telefone = txttelefone.Text;
                cmd.Parameters.AddWithValue("@d", telefone);
                cmd.Parameters.AddWithValue("@e", cliente.id_cliente);
                cmd.ExecuteNonQuery();
                con.Close();
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
    }
}
