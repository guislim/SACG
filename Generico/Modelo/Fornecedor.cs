using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico.Modelo
{
   public  class Fornecedor: Pessoa_Juridica
    {
       public int id_fornecedor { set; get; }
       public Pessoa_Juridica pessoa_juridica { set; get; }
       public int status { set; get; }
    }
}
