namespace Minecraft_Loup_Garou_Assistant
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonInstallationPC = new System.Windows.Forms.Button();
            this.buttonInstallationServeur = new System.Windows.Forms.Button();
            this.buttonAPropos = new System.Windows.Forms.Button();
            this.buttonMajPC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(458, 182);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonInstallationPC
            // 
            this.buttonInstallationPC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonInstallationPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInstallationPC.Location = new System.Drawing.Point(13, 212);
            this.buttonInstallationPC.Name = "buttonInstallationPC";
            this.buttonInstallationPC.Size = new System.Drawing.Size(457, 40);
            this.buttonInstallationPC.TabIndex = 1;
            this.buttonInstallationPC.Text = "INSTALLATION SUR VOTRE PC";
            this.buttonInstallationPC.UseVisualStyleBackColor = true;
            this.buttonInstallationPC.Click += new System.EventHandler(this.buttonInstallationPC_Click);
            // 
            // buttonInstallationServeur
            // 
            this.buttonInstallationServeur.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonInstallationServeur.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInstallationServeur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonInstallationServeur.Location = new System.Drawing.Point(13, 325);
            this.buttonInstallationServeur.Name = "buttonInstallationServeur";
            this.buttonInstallationServeur.Size = new System.Drawing.Size(457, 40);
            this.buttonInstallationServeur.TabIndex = 2;
            this.buttonInstallationServeur.Text = "INSTALLATION SUR VOTRE HEBERGEUR";
            this.buttonInstallationServeur.UseVisualStyleBackColor = true;
            this.buttonInstallationServeur.Click += new System.EventHandler(this.buttonInstallationServeur_Click);
            // 
            // buttonAPropos
            // 
            this.buttonAPropos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAPropos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAPropos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAPropos.Location = new System.Drawing.Point(12, 384);
            this.buttonAPropos.Name = "buttonAPropos";
            this.buttonAPropos.Size = new System.Drawing.Size(457, 40);
            this.buttonAPropos.TabIndex = 3;
            this.buttonAPropos.Text = "A PROPOS";
            this.buttonAPropos.UseVisualStyleBackColor = true;
            this.buttonAPropos.Click += new System.EventHandler(this.buttonAPropos_Click);
            // 
            // buttonMajPC
            // 
            this.buttonMajPC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonMajPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMajPC.Location = new System.Drawing.Point(13, 267);
            this.buttonMajPC.Name = "buttonMajPC";
            this.buttonMajPC.Size = new System.Drawing.Size(457, 40);
            this.buttonMajPC.TabIndex = 4;
            this.buttonMajPC.Text = "MISE A JOUR DU SERVEUR SUR VOTRE PC";
            this.buttonMajPC.UseVisualStyleBackColor = true;
            this.buttonMajPC.Click += new System.EventHandler(this.buttonMajPC_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(487, 439);
            this.Controls.Add(this.buttonMajPC);
            this.Controls.Add(this.buttonAPropos);
            this.Controls.Add(this.buttonInstallationServeur);
            this.Controls.Add(this.buttonInstallationPC);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Minecraft Loup Garou Assistant";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonInstallationPC;
        private System.Windows.Forms.Button buttonInstallationServeur;
        private System.Windows.Forms.Button buttonAPropos;
        private System.Windows.Forms.Button buttonMajPC;
    }
}

