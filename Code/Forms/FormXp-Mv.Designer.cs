namespace tilecon
{
    partial class FormXpMv
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

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXpMv));
            this.btnConvert = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCutSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxXP = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutsaveIndividualFramesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centralizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertAndSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.langaugeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugueseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupConversion = new System.Windows.Forms.GroupBox();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.checkIgnore = new System.Windows.Forms.CheckBox();
            this.groupUtilities = new System.Windows.Forms.GroupBox();
            this.btnNextImg = new System.Windows.Forms.Button();
            this.btnPreviusImg = new System.Windows.Forms.Button();
            this.cBMaker = new System.Windows.Forms.ComboBox();
            this.tilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPGMaker95ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simRPGMaker97ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rPGMakerXPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxXP)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupConversion.SuspendLayout();
            this.groupUtilities.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(6, 69);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(141, 23);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Open Tileset";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCutSave
            // 
            this.btnCutSave.Enabled = false;
            this.btnCutSave.Location = new System.Drawing.Point(6, 19);
            this.btnCutSave.Name = "btnCutSave";
            this.btnCutSave.Size = new System.Drawing.Size(141, 23);
            this.btnCutSave.TabIndex = 3;
            this.btnCutSave.Text = "Cut/save individual frames";
            this.btnCutSave.UseVisualStyleBackColor = true;
            this.btnCutSave.Click += new System.EventHandler(this.btnCutSave_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBoxXP);
            this.panel1.Location = new System.Drawing.Point(12, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 227);
            this.panel1.TabIndex = 5;
            // 
            // pictureBoxXP
            // 
            this.pictureBoxXP.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxXP.Name = "pictureBoxXP";
            this.pictureBoxXP.Size = new System.Drawing.Size(256, 2432);
            this.pictureBoxXP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxXP.TabIndex = 1;
            this.pictureBoxXP.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(515, 610);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 227);
            this.panel2.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Location = new System.Drawing.Point(384, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 227);
            this.panel3.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 206);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem,
            this.utilitiesToolStripMenuItem,
            this.convertToolStripMenuItem,
            this.langaugeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(603, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTilesetToolStripMenuItem,
            this.tilesetToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.archiveToolStripMenuItem.Text = "File";
            // 
            // openTilesetToolStripMenuItem
            // 
            this.openTilesetToolStripMenuItem.Name = "openTilesetToolStripMenuItem";
            this.openTilesetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openTilesetToolStripMenuItem.Text = "Open Tileset";
            this.openTilesetToolStripMenuItem.Click += new System.EventHandler(this.openTilesetToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // utilitiesToolStripMenuItem
            // 
            this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutsaveIndividualFramesToolStripMenuItem});
            this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.utilitiesToolStripMenuItem.Text = "Utilities";
            // 
            // cutsaveIndividualFramesToolStripMenuItem
            // 
            this.cutsaveIndividualFramesToolStripMenuItem.Enabled = false;
            this.cutsaveIndividualFramesToolStripMenuItem.Name = "cutsaveIndividualFramesToolStripMenuItem";
            this.cutsaveIndividualFramesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.cutsaveIndividualFramesToolStripMenuItem.Text = "Cut/save individual frames";
            this.cutsaveIndividualFramesToolStripMenuItem.Click += new System.EventHandler(this.cutsaveIndividualFramesToolStripMenuItem_Click_2);
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modeToolStripMenuItem,
            this.ignoreToolStripMenuItem,
            this.convertAndSaveToolStripMenuItem});
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.convertToolStripMenuItem.Text = "Convert";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.centralizeToolStripMenuItem,
            this.resizeToolStripMenuItem});
            this.modeToolStripMenuItem.Enabled = false;
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // centralizeToolStripMenuItem
            // 
            this.centralizeToolStripMenuItem.CheckOnClick = true;
            this.centralizeToolStripMenuItem.Name = "centralizeToolStripMenuItem";
            this.centralizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.centralizeToolStripMenuItem.Text = "Centralize";
            this.centralizeToolStripMenuItem.Click += new System.EventHandler(this.centralizeToolStripMenuItem_Click);
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.CheckOnClick = true;
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resizeToolStripMenuItem.Text = "Resize";
            this.resizeToolStripMenuItem.Click += new System.EventHandler(this.resizeToolStripMenuItem_Click);
            // 
            // ignoreToolStripMenuItem
            // 
            this.ignoreToolStripMenuItem.CheckOnClick = true;
            this.ignoreToolStripMenuItem.Enabled = false;
            this.ignoreToolStripMenuItem.Name = "ignoreToolStripMenuItem";
            this.ignoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ignoreToolStripMenuItem.Text = "Ignore Alpha";
            this.ignoreToolStripMenuItem.Click += new System.EventHandler(this.ignoreAlphaToolStripMenuItem_Click);
            // 
            // convertAndSaveToolStripMenuItem
            // 
            this.convertAndSaveToolStripMenuItem.Enabled = false;
            this.convertAndSaveToolStripMenuItem.Name = "convertAndSaveToolStripMenuItem";
            this.convertAndSaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.convertAndSaveToolStripMenuItem.Text = "Convert";
            this.convertAndSaveToolStripMenuItem.Click += new System.EventHandler(this.convertAndSaveToolStripMenuItem_Click);
            // 
            // langaugeToolStripMenuItem
            // 
            this.langaugeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.portugueseToolStripMenuItem});
            this.langaugeToolStripMenuItem.Name = "langaugeToolStripMenuItem";
            this.langaugeToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.langaugeToolStripMenuItem.Text = "Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // portugueseToolStripMenuItem
            // 
            this.portugueseToolStripMenuItem.Name = "portugueseToolStripMenuItem";
            this.portugueseToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.portugueseToolStripMenuItem.Text = "Portuguese";
            this.portugueseToolStripMenuItem.Click += new System.EventHandler(this.portugueseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupConversion
            // 
            this.groupConversion.Controls.Add(this.cbMode);
            this.groupConversion.Controls.Add(this.btnSave);
            this.groupConversion.Controls.Add(this.btnConvert);
            this.groupConversion.Controls.Add(this.checkIgnore);
            this.groupConversion.Location = new System.Drawing.Point(225, 117);
            this.groupConversion.Name = "groupConversion";
            this.groupConversion.Size = new System.Drawing.Size(153, 137);
            this.groupConversion.TabIndex = 8;
            this.groupConversion.TabStop = false;
            this.groupConversion.Text = "Conversion";
            // 
            // cbMode
            // 
            this.cbMode.DisplayMember = "None";
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.Enabled = false;
            this.cbMode.Items.AddRange(new object[] {
            "None",
            "Centralize",
            "Resize"});
            this.cbMode.Location = new System.Drawing.Point(6, 19);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(141, 21);
            this.cbMode.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(6, 97);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Salve";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkIgnore
            // 
            this.checkIgnore.AutoSize = true;
            this.checkIgnore.Enabled = false;
            this.checkIgnore.Location = new System.Drawing.Point(6, 46);
            this.checkIgnore.Name = "checkIgnore";
            this.checkIgnore.Size = new System.Drawing.Size(86, 17);
            this.checkIgnore.TabIndex = 2;
            this.checkIgnore.Text = "Ignore Alpha";
            this.checkIgnore.UseVisualStyleBackColor = true;
            this.checkIgnore.CheckedChanged += new System.EventHandler(this.checkIgnore_CheckedChanged);
            // 
            // groupUtilities
            // 
            this.groupUtilities.Controls.Add(this.btnCutSave);
            this.groupUtilities.Location = new System.Drawing.Point(225, 54);
            this.groupUtilities.Name = "groupUtilities";
            this.groupUtilities.Size = new System.Drawing.Size(153, 57);
            this.groupUtilities.TabIndex = 9;
            this.groupUtilities.TabStop = false;
            this.groupUtilities.Text = "Utilities";
            // 
            // btnNextImg
            // 
            this.btnNextImg.Enabled = false;
            this.btnNextImg.Location = new System.Drawing.Point(556, 33);
            this.btnNextImg.Name = "btnNextImg";
            this.btnNextImg.Size = new System.Drawing.Size(32, 23);
            this.btnNextImg.TabIndex = 10;
            this.btnNextImg.Text = ">";
            this.btnNextImg.UseVisualStyleBackColor = true;
            this.btnNextImg.Click += new System.EventHandler(this.btnNextImg_Click);
            // 
            // btnPreviusImg
            // 
            this.btnPreviusImg.Enabled = false;
            this.btnPreviusImg.Location = new System.Drawing.Point(518, 33);
            this.btnPreviusImg.Name = "btnPreviusImg";
            this.btnPreviusImg.Size = new System.Drawing.Size(32, 23);
            this.btnPreviusImg.TabIndex = 11;
            this.btnPreviusImg.Text = "<";
            this.btnPreviusImg.UseVisualStyleBackColor = true;
            this.btnPreviusImg.Click += new System.EventHandler(this.btnPreviusImg_Click);
            // 
            // cBMaker
            // 
            this.cBMaker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBMaker.FormattingEnabled = true;
            this.cBMaker.Items.AddRange(new object[] {
            "RPG Maker 95",
            "Sim RPG Maker 97",
            "RPG Maker XP"});
            this.cBMaker.Location = new System.Drawing.Point(114, 33);
            this.cBMaker.Name = "cBMaker";
            this.cBMaker.Size = new System.Drawing.Size(105, 21);
            this.cBMaker.TabIndex = 12;
            // 
            // tilesetToolStripMenuItem
            // 
            this.tilesetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rPGMaker95ToolStripMenuItem,
            this.simRPGMaker97ToolStripMenuItem,
            this.rPGMakerXPToolStripMenuItem});
            this.tilesetToolStripMenuItem.Name = "tilesetToolStripMenuItem";
            this.tilesetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tilesetToolStripMenuItem.Text = "Tileset";
            // 
            // rPGMaker95ToolStripMenuItem
            // 
            this.rPGMaker95ToolStripMenuItem.CheckOnClick = true;
            this.rPGMaker95ToolStripMenuItem.Name = "rPGMaker95ToolStripMenuItem";
            this.rPGMaker95ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.rPGMaker95ToolStripMenuItem.Text = "RPG Maker 95";
            this.rPGMaker95ToolStripMenuItem.Click += new System.EventHandler(this.rPGMaker95ToolStripMenuItem_Click);
            // 
            // simRPGMaker97ToolStripMenuItem
            // 
            this.simRPGMaker97ToolStripMenuItem.CheckOnClick = true;
            this.simRPGMaker97ToolStripMenuItem.Name = "simRPGMaker97ToolStripMenuItem";
            this.simRPGMaker97ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.simRPGMaker97ToolStripMenuItem.Text = "Sim RPG Maker 97";
            this.simRPGMaker97ToolStripMenuItem.Click += new System.EventHandler(this.simRPGMaker97ToolStripMenuItem_Click);
            // 
            // rPGMakerXPToolStripMenuItem
            // 
            this.rPGMakerXPToolStripMenuItem.CheckOnClick = true;
            this.rPGMakerXPToolStripMenuItem.Name = "rPGMakerXPToolStripMenuItem";
            this.rPGMakerXPToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.rPGMakerXPToolStripMenuItem.Text = "RPG Maker XP";
            this.rPGMakerXPToolStripMenuItem.Click += new System.EventHandler(this.rPGMakerXPToolStripMenuItem_Click);
            // 
            // FormXpMv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 292);
            this.Controls.Add(this.cBMaker);
            this.Controls.Add(this.btnPreviusImg);
            this.Controls.Add(this.btnNextImg);
            this.Controls.Add(this.groupUtilities);
            this.Controls.Add(this.groupConversion);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormXpMv";
            this.Text = "Tileset Converter to MV";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxXP)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupConversion.ResumeLayout(false);
            this.groupConversion.PerformLayout();
            this.groupUtilities.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBoxXP;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCutSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertAndSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem langaugeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portugueseToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupConversion;
        private System.Windows.Forms.GroupBox groupUtilities;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStripMenuItem openTilesetToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkIgnore;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutsaveIndividualFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centralizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.Button btnNextImg;
        private System.Windows.Forms.Button btnPreviusImg;
        private System.Windows.Forms.ComboBox cBMaker;
        private System.Windows.Forms.ToolStripMenuItem tilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rPGMaker95ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simRPGMaker97ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rPGMakerXPToolStripMenuItem;
    }
}

