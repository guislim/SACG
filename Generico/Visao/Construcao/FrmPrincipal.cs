using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Generico.util;
namespace Generico.Visao.Construcao
{
    public partial class FrmPrincipal : Form
    {
        public static List<short> lista_telas { get; set; }
        public FrmPrincipal()
        {
            InitializeComponent();
            lista_telas = new List<short>();
           
        }

        public void verificacoes()
        {
            string texto = "";
            if (Verificacoes.verifica_Contas_pagar_atrazadas(this))
            {
                texto += "Existem contas a pagar atrazadas\n";
            }
            if (Verificacoes.verifica_Contas_receber_atrazadas(this))
                texto += "Existem contas a serem recebidas\n";
            if (Verificacoes.verifica_estoque_minimo(this))
                texto += "Alguns produtos estão com o estoque menor do que o recomendável";
            if (texto.Length > 0)
            {
                this.LayoutMdi(MdiLayout.Cascade);
                MessageBox.Show(texto, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void contasAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(1))
            {
                FrmContasPagar form = new FrmContasPagar();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(1);
            }
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(2))
            {
                FrmConsultaClienteConstrucao form = new FrmConsultaClienteConstrucao();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(2);
            }
        }

        private void fornecedoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(3))
            {
                FrmConsultaFornecedor form = new FrmConsultaFornecedor();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(3);
            }
        }

        private void contasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(4))
            {
                FrmContasReceber form = new FrmContasReceber();
                form.MdiParent = this;
                lista_telas.Add(4);
                form.Show();
            }
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(5))
            {
                FrmCadClienteConstrucao form = new FrmCadClienteConstrucao();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(5);
            }
        }

        private void fornecedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(6))
            {
                
                FrmCadFornecedor form = new FrmCadFornecedor();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(6);
            }
        }


        private void produtoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(8))
            {
                FrmCadProduto form = new FrmCadProduto();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(8);
            }
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(7))
            {

                FrmConsultaProduto form = new FrmConsultaProduto();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(7);
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            verificacoes();
        }

        private void entradaESaídaDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(9))
            {

                FrmMovimentacaoProduto form = new FrmMovimentacaoProduto();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(9);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(2))
            {
                FrmConsultaClienteConstrucao form = new FrmConsultaClienteConstrucao();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(2);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(3))
            {
                FrmConsultaFornecedor form = new FrmConsultaFornecedor();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(3);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(7))
            {

                FrmConsultaProduto form = new FrmConsultaProduto();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(7);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(9))
            {

                FrmMovimentacaoProduto form = new FrmMovimentacaoProduto();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(9);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(1))
            {
                FrmContasPagar form = new FrmContasPagar();
                form.MdiParent = this;
                form.Show();
                lista_telas.Add(1);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(4))
            {
                FrmContasReceber form = new FrmContasReceber();
                form.MdiParent = this;
                lista_telas.Add(4);
                form.Show();
            }
        }

        private void contasAPagarAtrazadasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(10))
            {
                FrmContasPagarAtrazadas form = new FrmContasPagarAtrazadas();
                form.MdiParent = this;
                lista_telas.Add(10);
                form.Show();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(10))
            {
                FrmContasPagarAtrazadas form = new FrmContasPagarAtrazadas();
                form.MdiParent = this;
                lista_telas.Add(10);
                form.Show();
            }
        }

        private void contasAReceberAtrasadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(11))
            {
                FrmContasReceberAtrazadas form = new FrmContasReceberAtrazadas();
                form.MdiParent = this;
                lista_telas.Add(11);
                form.Show();
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (!lista_telas.Contains(11))
            {
                FrmContasReceberAtrazadas form = new FrmContasReceberAtrazadas();
                form.MdiParent = this;
                lista_telas.Add(11);
                form.Show();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

    }
}
