using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico.Modelo
{
  public  class ParcelaPagar
    {
      public int id_parcela_pagar { set; get; }
      public decimal valor{set;get;}
      public string situacao;
      public DateTime data_vencimento { set; get; }
      public decimal acrescimo { set; get; }
      public decimal desconto { set; get; }
      public ContaPagar conta_pagar { set; get; }
    }
}
