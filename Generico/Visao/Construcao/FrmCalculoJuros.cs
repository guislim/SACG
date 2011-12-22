using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Generico.Visao.Construcao
{
    public partial class FrmCalculoJuros : Form
    {
        private FrmCadProduto produto;
        private decimal valor_principal;
        public FrmCalculoJuros(decimal valor,FrmCadProduto produto)
        {
            InitializeComponent();

            //Inicializa as variaveis globais com o valor a ser calculado e a tela de cadastro de produto
            this.valor_principal = valor;
            this.produto = produto;
        }

        public FrmCalculoJuros(decimal valor, FrmCadProduto produto,decimal valor_juros)
        {
            InitializeComponent();

            //Inicializa as variaveis globais com o valor a ser calculado e a tela de cadastro de produto
            this.valor_principal = valor;
            this.produto = produto;
            numericUpDown1.Value = valor_juros;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            calcularJuros();
        }

        public void calcularJuros()
        {
            decimal valor = numericUpDown1.Value;
            decimal novo_valor = valor_principal + (valor_principal * valor) / 100;
            string texto = String.Format("{0:F2}", novo_valor);
            novo_valor = Convert.ToDecimal(texto);
            produto.determinaValorVenda(novo_valor);
            this.Close();
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                calcularJuros();
            }
        }
    }
}
