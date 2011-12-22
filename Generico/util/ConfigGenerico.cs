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
    public class ConfigGenerico
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
            FileInfo f = new FileInfo(@"C:\Generico\cfg\cfg.txt");
            if (!f.Exists)
            {
                Directory.CreateDirectory(@"C:\Generico\cfg");
                StreamWriter s = new StreamWriter(@"C:\Generico\cfg\cfg.txt");
                s.Write("server=localhost;database=generico;user id=root");
                url_banco = "server=localhost;database=generico;user id=root";
                s.Close();
            }
            else
            {
                StreamReader s = new StreamReader(@"C:\Generico\cfg\cfg.txt");
                string texto = s.ReadLine();
                url_banco = texto;
                s.Close();
            }
        }
    }
}
