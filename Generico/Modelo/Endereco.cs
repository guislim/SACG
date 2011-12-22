using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico.Modelo
{
   public class Endereco
    {
       public int id_endereco { set; get; }
       public string rua { set; get; }
       public string bairro { set; get; }
       public string cidade { set; get; }
       public string uf { set; get; }
       public string cep { set; get; }
       public string complemento { set; get; }
       public int numero { set; get; }

    }
}
