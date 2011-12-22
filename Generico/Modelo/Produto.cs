using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico.Modelo
{
   public class Produto
    {
       public int id_produto { set; get; }
       public string codigo { set; get; }
       public string descricao { set; get; }
       public string unidade { set; get; }
       public decimal preco_compra { set; get; }
       public decimal preco_venda { set; get; }
       public long estoque { set; get; }
       public int quantidade_minima { set; get; }
       public int status { set; get; }
       public Fornecedor fornecedor { set; get; }
       public DateTime data_cadastro { set; get; }
    }
}
