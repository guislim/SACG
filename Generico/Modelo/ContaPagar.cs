using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico.Modelo
{
   public class ContaPagar
    {
       public int id_conta_pagar { set; get; }
       public string situacao { set; get; }
       public decimal valor_total { set; get; }
       public decimal entrada { set; get; }
       public DateTime data_emissao { set; get; }
       public DateTime data_vencimento { set; get; }
       public string documento { set; get; }
       public int numDias { set; get; }
       public int numParcelas { set; get; }
       public string credor { set; get; }
       public Fornecedor fornecedor { set; get; }
       public decimal valor_pago { set; get; }
       public List<ParcelaPagar> parcelas { set; get; }
   }
}
