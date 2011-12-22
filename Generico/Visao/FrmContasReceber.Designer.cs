namespace Generico.Visao.Construcao
{
    partial class FrmContasReceber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmContasReceber));
            this.ckabertas = new System.Windows.Forms.CheckBox();
            this.ckquitadas = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cktodos = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rddatavencimento = new System.Windows.Forms.RadioButton();
            this.bt2 = new System.Windows.Forms.Button();
            this.rdcodigo = new System.Windows.Forms.RadioButton();
            this.rdfornecedor = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.bt1 = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtdatafinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtdataInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabela = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabela)).BeginInit();
            this.SuspendLayout();
            // 
            // ckabertas
            // 
            this.ckabertas.AutoSize = true;
            this.ckabertas.Checked = true;
            this.ckabertas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckabertas.Location = new System.Drawing.Point(15, 159);
            this.ckabertas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckabertas.Name = "ckabertas";
            this.ckabertas.Size = new System.Drawing.Size(62, 17);
            this.ckabertas.TabIndex = 9;
            this.ckabertas.Text = "Abertas";
            this.ckabertas.UseVisualStyleBackColor = true;
            this.ckabertas.CheckedChanged += new System.EventHandler(this.ckabertas_CheckedChanged);
            // 
            // ckquitadas
            // 
            this.ckquitadas.AutoSize = true;
            this.ckquitadas.Location = new System.Drawing.Point(94, 159);
            this.ckquitadas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckquitadas.Name = "ckquitadas";
            this.ckquitadas.Size = new System.Drawing.Size(77, 17);
            this.ckquitadas.TabIndex = 10;
            this.ckquitadas.Text = "Recebidas";
            this.ckquitadas.UseVisualStyleBackColor = true;
            this.ckquitadas.CheckedChanged += new System.EventHandler(this.ckquitadas_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cktodos);
            this.groupBox2.Controls.Add(this.ckabertas);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ckquitadas);
            this.groupBox2.Controls.Add(this.rddatavencimento);
            this.groupBox2.Controls.Add(this.bt2);
            this.groupBox2.Controls.Add(this.rdcodigo);
            this.groupBox2.Controls.Add(this.rdfornecedor);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.bt1);
            this.groupBox2.Controls.Add(this.txtCliente);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtdatafinal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtdataInicial);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(18, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(801, 184);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtragem";
            // 
            // cktodos
            // 
            this.cktodos.AutoSize = true;
            this.cktodos.Checked = true;
            this.cktodos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cktodos.Location = new System.Drawing.Point(570, 48);
            this.cktodos.Name = "cktodos";
            this.cktodos.Size = new System.Drawing.Size(110, 17);
            this.cktodos.TabIndex = 5;
            this.cktodos.Text = "Todos os Clientes";
            this.cktodos.UseVisualStyleBackColor = true;
            this.cktodos.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Mostrar";
            // 
            // rddatavencimento
            // 
            this.rddatavencimento.AutoSize = true;
            this.rddatavencimento.Checked = true;
            this.rddatavencimento.Location = new System.Drawing.Point(19, 102);
            this.rddatavencimento.Name = "rddatavencimento";
            this.rddatavencimento.Size = new System.Drawing.Size(107, 17);
            this.rddatavencimento.TabIndex = 6;
            this.rddatavencimento.TabStop = true;
            this.rddatavencimento.Text = "Data Vencimento";
            this.rddatavencimento.UseVisualStyleBackColor = true;
            this.rddatavencimento.CheckedChanged += new System.EventHandler(this.rddatavencimento_CheckedChanged);
            // 
            // bt2
            // 
            this.bt2.Image = ((System.Drawing.Image)(resources.GetObject("bt2.Image")));
            this.bt2.Location = new System.Drawing.Point(521, 42);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(33, 31);
            this.bt2.TabIndex = 4;
            this.bt2.UseVisualStyleBackColor = true;
            this.bt2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rdcodigo
            // 
            this.rdcodigo.AutoSize = true;
            this.rdcodigo.Location = new System.Drawing.Point(150, 102);
            this.rdcodigo.Name = "rdcodigo";
            this.rdcodigo.Size = new System.Drawing.Size(58, 17);
            this.rdcodigo.TabIndex = 7;
            this.rdcodigo.Text = "Codigo";
            this.rdcodigo.UseVisualStyleBackColor = true;
            this.rdcodigo.CheckedChanged += new System.EventHandler(this.rdcodigo_CheckedChanged);
            // 
            // rdfornecedor
            // 
            this.rdfornecedor.AutoSize = true;
            this.rdfornecedor.Location = new System.Drawing.Point(225, 102);
            this.rdfornecedor.Name = "rdfornecedor";
            this.rdfornecedor.Size = new System.Drawing.Size(57, 17);
            this.rdfornecedor.TabIndex = 8;
            this.rdfornecedor.Text = "Cliente";
            this.rdfornecedor.UseVisualStyleBackColor = true;
            this.rdfornecedor.CheckedChanged += new System.EventHandler(this.rdfornecedor_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ordenar por";
            // 
            // bt1
            // 
            this.bt1.Image = ((System.Drawing.Image)(resources.GetObject("bt1.Image")));
            this.bt1.Location = new System.Drawing.Point(482, 42);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(33, 31);
            this.bt1.TabIndex = 3;
            this.bt1.UseVisualStyleBackColor = true;
            this.bt1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(272, 46);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(204, 25);
            this.txtCliente.TabIndex = 5;
            this.txtCliente.TextChanged += new System.EventHandler(this.txtCliente_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cliente específico";
            // 
            // txtdatafinal
            // 
            this.txtdatafinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtdatafinal.Location = new System.Drawing.Point(148, 46);
            this.txtdatafinal.Name = "txtdatafinal";
            this.txtdatafinal.Size = new System.Drawing.Size(104, 25);
            this.txtdatafinal.TabIndex = 2;
            this.txtdatafinal.ValueChanged += new System.EventHandler(this.txtdatafinal_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Final";
            // 
            // txtdataInicial
            // 
            this.txtdataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtdataInicial.Location = new System.Drawing.Point(19, 46);
            this.txtdataInicial.Name = "txtdataInicial";
            this.txtdataInicial.Size = new System.Drawing.Size(104, 25);
            this.txtdataInicial.TabIndex = 1;
            this.txtdataInicial.ValueChanged += new System.EventHandler(this.txtdataInicial_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Inicial";
            // 
            // tabela
            // 
            this.tabela.AllowUserToAddRows = false;
            this.tabela.AllowUserToDeleteRows = false;
            this.tabela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabela.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column9,
            this.Column7,
            this.Column4,
            this.Column8});
            this.tabela.Location = new System.Drawing.Point(12, 264);
            this.tabela.Name = "tabela";
            this.tabela.ReadOnly = true;
            this.tabela.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabela.Size = new System.Drawing.Size(835, 259);
            this.tabela.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Codigo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 80;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Cliente";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Valor a Ser Recebido R$";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 175;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Data de Vencimento";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 160;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Valor Total da Conta R$";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 175;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Situação";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(18, 216);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 25);
            this.button4.TabIndex = 11;
            this.button4.Text = "Nova Conta";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button7
            // 
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(536, 538);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(123, 24);
            this.button7.TabIndex = 13;
            this.button7.Text = "Gerenciar conta";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(665, 538);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(70, 24);
            this.button8.TabIndex = 14;
            this.button8.Text = "Quitar";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(127, 216);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(110, 25);
            this.button9.TabIndex = 12;
            this.button9.Text = "Nova Parcela";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button11
            // 
            this.button11.Image = ((System.Drawing.Image)(resources.GetObject("button11.Image")));
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.Location = new System.Drawing.Point(743, 538);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(73, 24);
            this.button11.TabIndex = 15;
            this.button11.Text = "Excluir";
            this.button11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // FrmContasReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 576);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.tabela);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button4);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmContasReceber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de contas a receber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmContasReceber_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabela)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckabertas;
        private System.Windows.Forms.CheckBox ckquitadas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker txtdatafinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtdataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rddatavencimento;
        private System.Windows.Forms.Button bt2;
        private System.Windows.Forms.RadioButton rdcodigo;
        private System.Windows.Forms.RadioButton rdfornecedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView tabela;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.CheckBox cktodos;
    }
}