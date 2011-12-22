﻿using System;
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
    public partial class FrmGerenciarContaPagar : Form
    {
        private ContaPagar conta;
        private FrmContasPagar form;
        private DataTable table;
        public FrmGerenciarContaPagar(ContaPagar conta,FrmContasPagar form)
        {
            InitializeComponent();
            init(conta, form);
        }

        public void init(ContaPagar conta, FrmContasPagar form)
        {
            this.form = form;
            this.conta = conta;
            table = new DataTable();
            pesquisa();
            tabela.Columns[0].Visible = false;
            tabela.Columns[1].HeaderText = "Data de Vencimento";
            tabela.Columns[1].Width = 150;
            tabela.Columns[2].HeaderText = "Valor Total R$";
            tabela.Columns[2].Width = 120;
            tabela.Columns[3].HeaderText = "Situação";
        }

        public void pesquisa()
        {
            table.Clear();
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            string query = "select id_parcela_pagar,p.data_vencimento,p.valor,p.situacao from parcela_pagar as p inner join conta_pagar as c on p.id_conta_pagar = c.id_conta_pagar";
            query += " where p.id_conta_pagar="+conta.id_conta_pagar;
            MySqlCommand cmd = new MySqlCommand(query,con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(table);
            tabela.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                if (checa_quitacao())
                {
                    FrmOperacaoParcela f = new FrmOperacaoParcela(this, 3, conta, GerarParcela());
                    f.Show();
                }
            }

        }



        public void verificar_pagamento()
        {
            bool cond = true;

            for (int i = 0; i < tabela.RowCount; i++)
            {
                string val = tabela.Rows[i].Cells[3].Value.ToString();
                if (!val.Equals("QUITADA"))
                    cond = false;
            }
            if (cond)
            {
                string sql = "update conta_pagar set situacao='QUITADA' where id_conta_pagar="+conta.id_conta_pagar;
                MySqlConnection con = new MySqlConnection(Config.Conexao);
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                form.filtragem();
            }
        }

        public void pagamento()
        {
            string situacao = tabela.CurrentRow.Cells[3].Value.ToString();
            if (situacao.Equals("QUITADA"))
            {
                MessageBox.Show("Esta parcela já está quitada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja pagar essa conta?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
                    string valor = tabela.CurrentRow.Cells[2].Value.ToString().Replace(',','.');
                    string sql = "update parcela_pagar set situacao='QUITADA' where id_parcela_pagar=" + id;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    sql = "update conta_pagar set valor_pago=valor_pago+" + valor + " where id_conta_pagar=" + conta.id_conta_pagar;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    pesquisa();
                    if(form!=null)
                    form.filtragem();
                    verificar_pagamento();
                }
            }
        }

        public bool checa_quitacao()
        {
            string situacao = tabela.CurrentRow.Cells[3].Value.ToString();
            if (situacao.Equals("ABERTA"))
            {
                return true;
            }
            else
            {
                if(situacao.Equals("QUITADA"))
                    MessageBox.Show("Esta parcela já esta quitada ","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Esta parcela foi cancelada ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                return false;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {

            if (tabela.SelectedRows.Count == 1)
            {
                    quitar();
            }
        }
        public ParcelaPagar GerarParcela()
        {
            ParcelaPagar c = new ParcelaPagar();
            int id = Convert.ToInt32(tabela.CurrentRow.Cells[0].Value.ToString());
            MySqlConnection con = new MySqlConnection(Config.Conexao);
            string query = "select * from parcela_pagar where id_parcela_pagar =" + id;
            MySqlCommand cmd = new MySqlCommand(query, con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            c.id_parcela_pagar = id;
            c.valor = Convert.ToDecimal(reader["valor"].ToString());
            c.situacao = reader["situacao"].ToString();
            c.acrescimo = Convert.ToDecimal(reader["acrescimo"].ToString());
            c.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
            c.desconto = Convert.ToDecimal(reader["desconto"].ToString());
            con.Close();
            return c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {

                if (checa_quitacao())
                {
                    FrmOperacaoParcela f = new FrmOperacaoParcela(this, 2, conta, GerarParcela());
                    f.Show();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tabela.SelectedRows.Count == 1)
            {
                pagamento();
            }
        }

        public void quitar()
        {
            if (conta.situacao.Equals("QUITADA"))
            {
                MessageBox.Show("Esta conta ja esta quitada","Atenção",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Deseja quitar essa conta?", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sql = "update conta_pagar set situacao='QUITADA',valor_pago=" + conta.valor_total.ToString().Replace(',','.');
                    sql += " where id_conta_pagar=" + conta.id_conta_pagar;
                    MySqlConnection con = new MySqlConnection(Config.Conexao);
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = sql;
                    for (int i = 0; i < tabela.RowCount;i++)
                    {
                        int id = Convert.ToInt32(tabela.Rows[i].Cells[0].Value.ToString());
                        sql = "update parcela_pagar set situacao='QUITADA' where id_parcela_pagar=" +id;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    if(form!=null)
                    form.filtragem();
                    MessageBox.Show("Conta quitada com sucesso", "Atencao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        public void lista_tudo()
        {
            form.filtragem();
        }

    }
}
