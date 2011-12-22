using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generico.Modelo
{
   public  class Pessoa_Juridica
    {
       public int id_pessoa_juridica { set; get; }
       public string razao_social { set; get; }
       public string nome_fantasia { set; get; }
       public string cnpj { set; get; }
       public string inscricao_estadual { set; get; }
       public string telefone { set; get; }
       public string celular { set; get; }
       public string email { set; get; }
       public Endereco endereco { set; get; }
    }
}
