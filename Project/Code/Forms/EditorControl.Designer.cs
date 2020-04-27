namespace tilecon
{
    partial class EditorControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.btnSetInputTileset = new System.Windows.Forms.Button();
            this.btnClearPreview = new System.Windows.Forms.Button();
            this.btnClearAndSet = new System.Windows.Forms.Button();
            this.cbOutput = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.outputPanel = new System.Windows.Forms.Panel();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Location = new System.Drawing.Point(322, 43);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxPreview.TabIndex = 21;
            this.pictureBoxPreview.TabStop = false;
            // 
            // btnSetInputTileset
            // 
            this.btnSetInputTileset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSetInputTileset.Enabled = false;
            this.btnSetInputTileset.Location = new System.Drawing.Point(3, 8);
            this.btnSetInputTileset.Name = "btnSetInputTileset";
            this.btnSetInputTileset.Size = new System.Drawing.Size(109, 23);
            this.btnSetInputTileset.TabIndex = 25;
            this.btnSetInputTileset.Text = "[set input tileset]";
            this.btnSetInputTileset.UseVisualStyleBackColor = true;
            this.btnSetInputTileset.Click += new System.EventHandler(this.BtnSetInputTileset_Click);
            // 
            // btnClearPreview
            // 
            this.btnClearPreview.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearPreview.Location = new System.Drawing.Point(282, 8);
            this.btnClearPreview.Name = "btnClearPreview";
            this.btnClearPreview.Size = new System.Drawing.Size(129, 23);
            this.btnClearPreview.TabIndex = 26;
            this.btnClearPreview.Text = "[clear preview]";
            this.btnClearPreview.UseVisualStyleBackColor = true;
            this.btnClearPreview.Click += new System.EventHandler(this.BtnClearPreview_Click);
            // 
            // btnClearAndSet
            // 
            this.btnClearAndSet.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClearAndSet.Location = new System.Drawing.Point(133, 5);
            this.btnClearAndSet.Name = "btnClearAndSet";
            this.btnClearAndSet.Size = new System.Drawing.Size(124, 23);
            this.btnClearAndSet.TabIndex = 18;
            this.btnClearAndSet.Text = "Clear and Set Tileset";
            this.btnClearAndSet.UseVisualStyleBackColor = true;
            this.btnClearAndSet.Click += new System.EventHandler(this.BtnClearAndSet_Click);
            // 
            // cbOutput
            // 
            this.cbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOutput.DropDownHeight = 161;
            this.cbOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutput.DropDownWidth = 200;
            this.cbOutput.FormattingEnabled = true;
            this.cbOutput.IntegralHeight = false;
            this.cbOutput.Items.AddRange(new object[] {
            "RPG Maker MV (Tileset A1-2)",
            "RPG Maker MV (Tileset A3)",
            "RPG Maker MV (Tileset A4)",
            "RPG Maker MV (Tileset A5)",
            "RPG Maker MV (Tileset B-C)"});
            this.cbOutput.Location = new System.Drawing.Point(3, 6);
            this.cbOutput.Name = "cbOutput";
            this.cbOutput.Size = new System.Drawing.Size(124, 21);
            this.cbOutput.TabIndex = 17;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.1F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.1F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxPreview, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.outputPanel, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.inputPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSetInputTileset, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClearPreview, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.23529F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(695, 346);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // outputPanel
            // 
            this.outputPanel.AutoScroll = true;
            this.outputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputPanel.Location = new System.Drawing.Point(432, 43);
            this.outputPanel.Name = "outputPanel";
            this.outputPanel.Size = new System.Drawing.Size(260, 300);
            this.outputPanel.TabIndex = 23;
            // 
            // inputPanel
            // 
            this.inputPanel.AutoScroll = true;
            this.inputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPanel.Location = new System.Drawing.Point(3, 43);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(258, 300);
            this.inputPanel.TabIndex = 22;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnClearAndSet, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbOutput, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(432, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(260, 34);
            this.tableLayoutPanel4.TabIndex = 24;
            // 
            // EditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditorControl";
            this.Size = new System.Drawing.Size(695, 346);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Button btnSetInputTileset;
        private System.Windows.Forms.Button btnClearPreview;
        private System.Windows.Forms.Button btnClearAndSet;
        private System.Windows.Forms.ComboBox cbOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel outputPanel;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}
