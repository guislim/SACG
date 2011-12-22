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
    public partial class FrmOperacaoParcelaReceber : Form
    {
        private FrmGerenciarContaReceber form;
        private int opcao;
        private ContaReceber conta;
        private ParcelaReceber parcela;
        public FrmOperacaoParcelaReceber(FrmGerenciarContaReceber form,int opcao,ContaReceber conta,ParcelaReceber parcela)
        {
            InitializeComponent();
            this.form = form;
            this.opcao = opcao;
            this.conta = conta;
            this.parcela = parcela;
        }

       

        public bool validar()
        {
            try
            {
                decimal valor = Convert.ToDecimal(txtvalor.Text);
               
                 if((opcao == 2) && valor > parcela.valor)
                {
                    MessageBox.Show("Este valor é maior que o valor da parcela!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (valor == 0)
                {
                    MessageBox.Show("Insira um valor maior que zero!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                    return true;
            }
            catch (Exception t)
            {
                MessageBox.Show("Informe um valor válido!","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
        }

        public void desconto()
        {
            if (validar())
            {
                decimal valor = Convert.ToDecimal(txtvalor.Text);
                string sql = "update parcela_receber set desconto = desconto+" + valor;
                sql += ",valor=valor-"+valor;
                sql += " where id_parcela_receber=" + parcela.id_parcela_receber;
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                if (valor == parcela.valor)
                {
                    sql = "update parcela_receber set situacao='QUITADA' where id_parcela_receber=" + parcela.id_parcela_receber;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                sql = "update conta_receber set valor_total=valor_total-"+valor;
                sql += " where id_conta_receber="+conta.id_conta_receber;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                con.Close();
                form.pesquisa();
                form.verificar_pagamento();
                form.lista_tudo();
                MessageBox.Show("Desconto inserido com sucesso", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        public void acrescimo()
        {
            if (validar())
            {
                decimal valor = Convert.ToDecimal(txtvalor.Text);
                string sql = "update parcela_receber set acrescimo = acrescimo+" + valor;
                sql += ",valor=valor+" + valor;
                sql += " where id_parcela_receber=" + parcela.id_parcela_receber;
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                sql = "update conta_receber set valor_total=valor_total+" + valor;
                sql += " where id_conta_receber=" + conta.id_conta_receber;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                con.Close();
                form.pesquisa();
                form.verificar_pagamento();
                form.lista_tudo();
                MessageBox.Show("Acrescimo inserido com sucesso", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (opcao)
            {
                case 2:
                    desconto();
                    break;
                case 3:
                    acrescimo();
                    break;
            }
        }
    }
}
