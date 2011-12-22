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
using Generico.Visao.Construcao;
namespace Generico.Visao.Construcao
{
    public partial class FrmCadContaReceber : Form
    {
        private Cliente cliente;
        private bool gerar = false;
        private decimal entrada;
        private FrmContasReceber tela;
        public FrmCadContaReceber(FrmContasReceber tela)
        {
            InitializeComponent();
            this.tela = tela;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "Nenhum";
            cliente = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gerar)
            {
                DateTime venc = DateTime.Parse(tabela.Rows[tabela.RowCount - 1].Cells[2].Value.ToString());
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                String query = "insert into conta_receber(data_emissao,num_dias,valor_total,num_parcelas,entrada,data_vencimento,situacao,documento,id_cliente,valor_pago) values(@a,@c,@d,@e,@f,@g,@h,@i,@j,@k)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                decimal total= Convert.ToDecimal( txtvalortotal.Text);
                con.Open();
                cmd.Parameters.AddWithValue("@a", txtdataemissao.Value);
                cmd.Parameters.AddWithValue("@c", Convert.ToInt32(txtnumdias.Text));
                cmd.Parameters.AddWithValue("@d",total);
                cmd.Parameters.AddWithValue("@e", Convert.ToInt32(txtnumparcelas.Value));
                cmd.Parameters.AddWithValue("@f", entrada);
                cmd.Parameters.AddWithValue("@g", venc);
                if(entrada!=total)
                cmd.Parameters.AddWithValue("@h", "ABERTA");
                else
                cmd.Parameters.AddWithValue("@h", "RECEBIDA");
                cmd.Parameters.AddWithValue("@i", txtdocumento.Text);
                int? cli = null;
                if (cliente != null)
                    cli = cliente.id_cliente;
                cmd.Parameters.AddWithValue("@j",cli);
                cmd.Parameters.AddWithValue("@k", entrada);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = "select max(id_conta_receber) from conta_receber";
                int id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "insert into parcela_receber(id_conta_receber,valor,data_vencimento,desconto,acrescimo,situacao) values(@a,@b,@c,@d,@e,@g)";
                for (int i = 0; i < tabela.RowCount; i++)
                {
                    cmd.Parameters.AddWithValue("@a", id);
                    cmd.Parameters.AddWithValue("@b",Convert.ToDecimal(tabela.Rows[i].Cells[1].Value.ToString()));
                    cmd.Parameters.AddWithValue("@c",DateTime.Parse(tabela.Rows[i].Cells[2].Value.ToString()));
                    cmd.Parameters.AddWithValue("@d", 0);
                    cmd.Parameters.AddWithValue("@e", 0);
                    string situacao = tabela.Rows[i].Cells[tabela.ColumnCount - 1].Value.ToString();
                    cmd.Parameters.AddWithValue("@g", situacao);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                gerar = false;
                tela.filtragem();
                MessageBox.Show("Conta cadastrada com sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                this.Close();
            }
        }

        public void gerarParcelas()
        {
            if (validacao())
            {
                tabela.Rows.Clear();
                decimal total = 0,valParcela=0;
                entrada = 0;
                int numParcelas = 1,carencia=0;
                if (txtentrada.Text.Length != 0)
                    entrada = Convert.ToDecimal(txtentrada.Text);
                if (txtcarencia.Text.Length != 0)
                    carencia = Convert.ToInt32(txtcarencia.Text);
                numParcelas = Convert.ToInt32(txtnumparcelas.Value);
                total = Convert.ToDecimal(txtvalortotal.Text);
                int numDias = Convert.ToInt32(txtnumdias.Text);
                int j = 0;
                valParcela = Convert.ToDecimal(String.Format("{0:F2}",((total-entrada) / numParcelas)));
                if (entrada > 0)
                {
                    j++;
                    tabela.Rows.Add(new String[] { j.ToString(), entrada.ToString(), txtdataemissao.Value.ToShortDateString(), "0", "0", "RECEBIDA" });
                }
                DateTime data = txtdataemissao.Value;
                for (int i = 0; i < numParcelas;i++ )
                {
                    data = data.AddDays(numDias);
                    if (i == 0 && carencia > 0)
                        data = data.AddDays(carencia);
                    tabela.Rows.Add(new String[] { j.ToString(),valParcela.ToString(), data.ToShortDateString(), "0", "0", "ABERTA" });
                    j++;
                }
                gerar = true;
            }
        }

        public bool validacao()
        {
            bool cond = true;
            string erro = "";

            try
            {
                int num = Convert.ToInt32(txtnumdias.Text);
                if (num < 1)
                {
                    cond = false;
                    erro += "O mínimo de dias a serem contadas de uma parcela a outra é 1\n";
                }
            }
            catch (Exception et)
            {
                cond = false;
                erro += "Insira um valor numérico para o número de dias\n";
            }
            if (txtcarencia.Text.Length != 0)
            {
                try
                {
                    int car = Convert.ToInt32(txtcarencia.Text);
                    if (car < 0)
                    {
                        cond = false;
                        erro += "O mínimo de dias de carência da primera parcela é 0\n";
                    }
                }
                catch (Exception t)
                {
                    cond = false;
                    erro += "Insira um valor númerico para o campo carência de dias da 1º parcela\n";
                }
            }
             try
            {
                decimal total = Convert.ToDecimal(txtvalortotal.Text);
                if (total < 0)
                {
                    cond = false;
                    erro += "O valor da conta deve ser maior que 0\n";
                }
                else
                {

                    if (txtentrada.Text.Length != 0)
                    {
                        try
                        {
                            decimal entr = Convert.ToDecimal(txtentrada.Text);
                            if (entr < 0)
                            {
                                cond = false;
                                erro += "O valor mínimo de entrada é 0\n";
                            }
                            else
                            {
                                if (entr > total)
                                {
                                    cond = false;
                                    erro += "O valor de entrada nao pode ser maior que o total da conta\n";
                                }
                            }
                        }
                        catch (Exception t)
                        {
                            cond = false;
                            erro += "Insira um valor de entrada válido\n";
                        }
                    }
                }
            }
            catch (Exception t)
            {
                cond = false;
                erro += "Insira um valor da conta válido válido\n";
            }
         
            decimal par = txtnumparcelas.Value;

            if (par <= 0)
            {
                cond = false;
                erro += "O número mínimo de parcelas é 1\n";
            }
           
            if (!cond)
            {
                MessageBox.Show(erro, "Corrija os seguintes erros", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gerar = false;
            }
            return cond;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gerarParcelas();
        }

        private void FrmCadContaPagar_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSelecionaCliente sel = new FrmSelecionaCliente(this);
            sel.Show();
        }
        public void seleciona_cliente(Cliente cli)
        {
            cliente = cli;
            txtCliente.Text = cli.nome;
        }

    }
}


