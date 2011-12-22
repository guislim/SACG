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
    public partial class FrmCadParcela : Form
    {
        private FrmContasPagar form;
        private ContaPagar conta;
        public FrmCadParcela(ContaPagar conta,FrmContasPagar form)
        {
            InitializeComponent();
            this.conta = conta;
            txtdata.MinDate = conta.data_emissao;
            txtdata.Value = conta.data_emissao;
            txtcodigo.Text = conta.id_conta_pagar + "";
            this.form = form;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gravar();
        }

        private bool validacao()
        {
            bool cond = true;
            string erro = "";
            try
            {
              decimal d=  Convert.ToDecimal(txtvalor.Text);
              if (d <= 0)
              {
                  cond = false;
                  erro += "Insira um valor maior que zero para a parcela";
              }
            }
            catch (Exception er)
            {
                cond = false;
                erro += "Insira um valor válido para a parcela";
            }
            if (!cond)
                MessageBox.Show(erro, "Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            return cond;
        }

        public void gravar()
        {
            if (validacao())
            {
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                string sql = "insert into parcela_pagar(id_conta_pagar,valor,data_vencimento,situacao) values(@a,@b,@c,@d)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@a",conta.id_conta_pagar);
                cmd.Parameters.AddWithValue("@b", Convert.ToDecimal(txtvalor.Text));
                cmd.Parameters.AddWithValue("@c",txtdata.Value);
                cmd.Parameters.AddWithValue("@d","ABERTA");
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                string sit = "";
                if (!conta.situacao.Equals("CANCELADA"))
                    sit = ", situacao='ABERTA'";
                sql = "update conta_pagar set valor_total=valor_total+"+Convert.ToDecimal(txtvalor.Text)+sit+" where id_conta_pagar ="+conta.id_conta_pagar;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                form.filtragem();
                MessageBox.Show("Parcela adicionada com sucesso","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                con.Close();
                this.Close();
            }
        }


    }
}
