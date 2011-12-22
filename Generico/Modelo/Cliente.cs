using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico.Modelo
{
  public  class Cliente
    {
      public int id_cliente { set; get; }
      public string nome { set; get; }
      public string email { set; get; }
      public string telefone { set; get; }
      public string cpf { set; get; }
      public DateTime data { set; get; }
      public string genero { set; get; }
      public string rg { set; get; }
      public string celular { set; get; }
      public Endereco endereco { set; get; } 
    }
}
