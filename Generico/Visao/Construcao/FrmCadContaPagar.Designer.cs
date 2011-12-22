namespace Generico.Visao.Construcao
{
    partial class FrmCadContaPagar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCadContaPagar));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtcredor = new System.Windows.Forms.TextBox();
            this.lbsg = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtdocumento = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtcarencia = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtentrada = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtnumparcelas = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.txtvalortotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnumdias = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdataemissao = new System.Windows.Forms.DateTimePicker();
            this.tabela = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnumparcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabela)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtcredor);
            this.groupBox1.Controls.Add(this.lbsg);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.txtdocumento);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtcarencia);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtentrada);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtnumparcelas);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtvalortotal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtnumdias);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtFornecedor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtdataemissao);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(672, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados da Nova Conta";
            // 
            // txtcredor
            // 
            this.txtcredor.Location = new System.Drawing.Point(130, 118);
            this.txtcredor.Name = "txtcredor";
            this.txtcredor.Size = new System.Drawing.Size(266, 25);
            this.txtcredor.TabIndex = 10;
            // 
            // lbsg
            // 
            this.lbsg.AutoSize = true;
            this.lbsg.Location = new System.Drawing.Point(15, 121);
            this.lbsg.Name = "lbsg";
            this.lbsg.Size = new System.Drawing.Size(49, 17);
            this.lbsg.TabIndex = 29;
            this.lbsg.Text = "Credor";
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(513, 121);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 32);
            this.button3.TabIndex = 11;
            this.button3.Text = "Gerar Parcelas";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtdocumento
            // 
            this.txtdocumento.Location = new System.Drawing.Point(523, 90);
            this.txtdocumento.Name = "txtdocumento";
            this.txtdocumento.Size = new System.Drawing.Size(107, 25);
            this.txtdocumento.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(443, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "documento";
            // 
            // txtcarencia
            // 
            this.txtcarencia.Location = new System.Drawing.Point(402, 90);
            this.txtcarencia.Name = "txtcarencia";
            this.txtcarencia.Size = new System.Drawing.Size(35, 25);
            this.txtcarencia.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Carência dias 1º parcela";
            // 
            // txtentrada
            // 
            this.txtentrada.Location = new System.Drawing.Point(130, 90);
            this.txtentrada.Name = "txtentrada";
            this.txtentrada.Size = new System.Drawing.Size(107, 25);
            this.txtentrada.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Entrada R$";
            // 
            // txtnumparcelas
            // 
            this.txtnumparcelas.Location = new System.Drawing.Point(588, 59);
            this.txtnumparcelas.Name = "txtnumparcelas";
            this.txtnumparcelas.Size = new System.Drawing.Size(42, 25);
            this.txtnumparcelas.TabIndex = 6;
            this.txtnumparcelas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(454, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Número de parcelas";
            // 
            // txtvalortotal
            // 
            this.txtvalortotal.Location = new System.Drawing.Point(293, 59);
            this.txtvalortotal.Name = "txtvalortotal";
            this.txtvalortotal.Size = new System.Drawing.Size(155, 25);
            this.txtvalortotal.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Valor Total R$:";
            // 
            // txtnumdias
            // 
            this.txtnumdias.Location = new System.Drawing.Point(130, 59);
            this.txtnumdias.Name = "txtnumdias";
            this.txtnumdias.Size = new System.Drawing.Size(57, 25);
            this.txtnumdias.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Número de dias:";
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(573, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 31);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(534, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 31);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.Enabled = false;
            this.txtFornecedor.Location = new System.Drawing.Point(324, 31);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(204, 25);
            this.txtFornecedor.TabIndex = 13;
            this.txtFornecedor.Text = "Nenhum";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Fornecedor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data de Emissao:";
            // 
            // txtdataemissao
            // 
            this.txtdataemissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtdataemissao.Location = new System.Drawing.Point(130, 28);
            this.txtdataemissao.Name = "txtdataemissao";
            this.txtdataemissao.Size = new System.Drawing.Size(107, 25);
            this.txtdataemissao.TabIndex = 1;
            // 
            // tabela
            // 
            this.tabela.AllowUserToAddRows = false;
            this.tabela.AllowUserToDeleteRows = false;
            this.tabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabela.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Valor,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.tabela.Location = new System.Drawing.Point(9, 190);
            this.tabela.Name = "tabela";
            this.tabela.ReadOnly = true;
            this.tabela.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabela.Size = new System.Drawing.Size(686, 160);
            this.tabela.TabIndex = 1;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nº";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Valor
            // 
            this.Valor.HeaderText = "Valor R$";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Data Vencimento";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 130;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Acrescimo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Desconto";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Situação";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(9, 368);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 32);
            this.button4.TabIndex = 12;
            this.button4.Text = "Gravar";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(100, 368);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 32);
            this.button5.TabIndex = 13;
            this.button5.Text = "Cancelar";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FrmCadContaPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 425);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tabela);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmCadContaPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Contas a Pagar";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnumparcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabela)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtdataemissao;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtdocumento;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtcarencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtentrada;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtnumparcelas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtvalortotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtnumdias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView tabela;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtcredor;
        private System.Windows.Forms.Label lbsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}