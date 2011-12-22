using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Generico.Modelo;
using Generico.Visao.Construcao;
using System.Windows.Forms;
namespace Generico.util
{
   public class Verificacoes
    {
       public static bool verifica_estoque_minimo(FrmPrincipal principal)
       {
           string query = "select * from produto where quantidade_minima >=estoque and status=1";
           MySqlConnection con = new MySqlConnection(Config.Conexao);
           MySqlCommand cmd = new MySqlCommand(query,con);
           con.Open();
           MySqlDataReader reader = cmd.ExecuteReader();
           if (!reader.HasRows)
           {
               con.Close();
               return false;
           }
           else
           {
               List<Produto> lista = new List<Produto>();
               while (reader.Read())
               {
                   Produto p = new Produto();
                   p.codigo = reader["codigo"].ToString();
                   p.descricao = reader["descricao"].ToString();
                   p.estoque = Convert.ToInt64(reader["estoque"].ToString());
                   lista.Add(p);
               }
               con.Close();
               FrmEstoqueMinimo frm = new FrmEstoqueMinimo(lista);
               frm.MdiParent = principal;
               frm.Show();
               return true;
           }
           
       }

       public static bool verifica_Contas_pagar_atrazadas(FrmPrincipal principal)
       {
           string data_atual = GeraDataMysql.Gerar(DateTime.Now);
           string query = "select * from parcela_pagar as p inner join conta_pagar as c on p.id_conta_pagar = c.id_conta_pagar inner join fornecedor as f on f.id_fornecedor = c.id_fornecedor ";
           query += "inner join pessoa_juridica as j on f.id_pessoa_juridica = j.id_pessoa_juridica ";
           query += "where p.data_vencimento <='"+data_atual+"' and c.status=1 and p.situacao='ABERTA'";
           MySqlConnection con = new MySqlConnection(Config.Conexao);
           MySqlCommand cmd = new MySqlCommand(query, con);
           con.Open();
           MySqlDataReader reader = cmd.ExecuteReader();
           if (!reader.HasRows)
           {
               con.Close();
               return false;
           }
           else
           {
               List<ParcelaPagar> lista = new List<ParcelaPagar>();

               while (reader.Read())
               {
                  ContaPagar c = new ContaPagar();
                  ParcelaPagar p = new ParcelaPagar();
                  Fornecedor f = new Fornecedor();
                  f.razao_social = reader["razao_social"].ToString();
                  c.credor = reader["credor"].ToString();
                  c.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                  c.valor_total = Convert.ToDecimal(reader["valor_total"].ToString());
                  c.valor_pago = Convert.ToDecimal(reader["valor_pago"].ToString());
                  c.situacao="ABERTA";
                   c.id_conta_pagar = Convert.ToInt32(reader["id_conta_pagar"].ToString());
                  c.data_emissao= DateTime.Parse(reader["data_emissao"].ToString());
                  c.documento= reader["documento"].ToString();
                  c.entrada= Convert.ToDecimal(reader["entrada"].ToString());
                  p.valor = Convert.ToDecimal(reader["valor"].ToString());
                  p.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                  p.conta_pagar=c;
                  c.fornecedor = f;
                  lista.Add(p);
               }
               con.Close();
               FrmContasPagarAtrazadas frm = new FrmContasPagarAtrazadas(lista);
               frm.MdiParent= principal;
               frm.Activate();
               frm.Show();
               return true;
           }

       }

       public static bool verifica_Contas_receber_atrazadas(FrmPrincipal principal)
       {
           string data_atual = GeraDataMysql.Gerar(DateTime.Now);
           string query = "select * from parcela_receber as p inner join conta_receber as c on p.id_conta_receber = c.id_conta_receber inner join cliente as f on f.id_cliente = c.id_cliente ";
           query += "inner join pessoa_fisica as j on f.id_pessoa_fisica = j.id_pessoa_fisica ";
           query += "where p.data_vencimento <='" + data_atual + "' and c.status=1 and p.situacao='ABERTA'";
           MySqlConnection con = new MySqlConnection(Config.Conexao);
           MySqlCommand cmd = new MySqlCommand(query, con);
           con.Open();
           MySqlDataReader reader = cmd.ExecuteReader();
           if (!reader.HasRows)
           {
               con.Close();
               return false;
           }
           else
           {
               List<ParcelaReceber> lista = new List<ParcelaReceber>();

               while (reader.Read())
               {
                   ContaReceber c = new ContaReceber();
                   ParcelaReceber p = new ParcelaReceber();
                   Cliente cli = new Cliente();
                   cli.nome = reader["nome"].ToString();
                   c.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                   c.valor_total = Convert.ToDecimal(reader["valor_total"].ToString());
                   c.valor_pago = Convert.ToDecimal(reader["valor_pago"].ToString());
                   c.situacao = "ABERTA";
                   c.id_conta_receber = Convert.ToInt32(reader["id_conta_receber"].ToString());
                   c.data_emissao = DateTime.Parse(reader["data_emissao"].ToString());
                   c.documento = reader["documento"].ToString();
                   c.entrada = Convert.ToDecimal(reader["entrada"].ToString());
                   p.valor = Convert.ToDecimal(reader["valor"].ToString());
                   p.data_vencimento = DateTime.Parse(reader["data_vencimento"].ToString());
                   p.conta_receber = c;
                   c.cliente = cli;
                   lista.Add(p);
               }
               con.Close();
               FrmContasReceberAtrazadas frm = new FrmContasReceberAtrazadas(lista);
               frm.MdiParent = principal;
               frm.Activate();
               frm.Show();
               return true;
           }

       }

    }
}
