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
    public partial class FrmCadParcelaReceber : Form
    {
        private FrmContasReceber form;
        private ContaReceber conta;
        public FrmCadParcelaReceber(ContaReceber conta,FrmContasReceber form)
        {
            InitializeComponent();
            this.conta = conta;
            txtdata.MinDate = conta.data_emissao;
            txtdata.Value = conta.data_emissao;
            txtcodigo.Text = conta.id_conta_receber + "";
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
                string sql = "insert into parcela_receber(id_conta_receber,valor,data_vencimento,desconto,acrescimo,situacao) values(@a,@b,@c,@d,@e,@g)";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@a",conta.id_conta_receber);
                cmd.Parameters.AddWithValue("@b", Convert.ToDecimal(txtvalor.Text));
                cmd.Parameters.AddWithValue("@c",txtdata.Value);
                cmd.Parameters.AddWithValue("@d",0);
                cmd.Parameters.AddWithValue("@e", 0);
                cmd.Parameters.AddWithValue("@g","ABERTA");
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                string sit = "";
                if (!conta.situacao.Equals("CANCELADA"))
                    sit = ", situacao='ABERTA'";
                sql = "update conta_receber set valor_total=valor_total+" + Convert.ToDecimal(txtvalor.Text) + sit + " where id_conta_receber =" + conta.id_conta_receber;
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
