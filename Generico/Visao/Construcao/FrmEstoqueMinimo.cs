using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Generico.Modelo;
namespace Generico.Visao.Construcao
{
    public partial class FrmEstoqueMinimo : Form
    {
        public FrmEstoqueMinimo(List<Produto> produtos)
        {
            InitializeComponent();
            inicializa_tabela(produtos);
        }

        private void FrmEstoqueMinimo_Load(object sender, EventArgs e)
        {

        }

        public void inicializa_tabela(List<Produto> produtos)
        {
            foreach(Produto p in produtos){
                dataGridView1.Rows.Add(new String[]{p.codigo,p.descricao,p.estoque.ToString()});
            }
        }
    }
}
