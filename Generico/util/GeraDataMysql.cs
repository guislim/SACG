using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico
{
   public class GeraDataMysql
    {
       public static string Gerar(DateTime data)
       {
           int ano = data.Year;
           int mes = data.Month;
           int dia = data.Day;
           return ano + "/" + mes + "/" + dia;
       }
    }
}
