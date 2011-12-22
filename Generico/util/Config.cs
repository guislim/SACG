using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;
using MySql;
using System.Data;
namespace Generico
{
    public class Config
    {
        private static string url_banco = null;

        public static string Conexao
        {
            get
            {
                if (url_banco == null)
                    recuperaBanco();
                return url_banco;
            }
        }
        private static void recuperaBanco()
        {
            FileInfo f = new FileInfo(@"C:\Construcao\cfg\cfg.txt");
            if (!f.Exists)
            {
                Directory.CreateDirectory(@"C:\Construcao\cfg");
                StreamWriter s = new StreamWriter(@"C:\Construcao\cfg\cfg.txt");
                s.Write("server=localhost;database=construtora;user id=root");
                url_banco = "server=localhost;database=construtora;user id=root";
                s.Close();
            }
            else
            {
                StreamReader s = new StreamReader(@"C:\Construcao\cfg\cfg.txt");
                string texto = s.ReadLine();
                url_banco = texto;
                s.Close();
            }
        }
    }
}
