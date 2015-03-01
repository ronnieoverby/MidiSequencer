using System.Text.RegularExpressions;

namespace WinDrums
{
    partial class Form1
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
            this.txtPatterns = new System.Windows.Forms.TextBox();
            this.numFrameLen = new System.Windows.Forms.NumericUpDown();
            this.numChannel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Channel = new System.Windows.Forms.Label();
            this.cboDevices = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numFrameLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChannel)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPatterns
            // 
            this.txtPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPatterns.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatterns.Location = new System.Drawing.Point(12, 51);
            this.txtPatterns.Multiline = true;
            this.txtPatterns.Name = "txtPatterns";
            this.txtPatterns.Size = new System.Drawing.Size(744, 416);
            this.txtPatterns.TabIndex = 0;
            this.txtPatterns.Text = "KICK   36|9999 9     9    9 9        9 9  |\r\nSNARE  38|    9       9       9     " +
    "  9   |\r\nHAT(-) 42|8484848484848484    848484848484|\r\nHAT(*) 46|                " +
    "7 7             |";
            // 
            // numFrameLen
            // 
            this.numFrameLen.Location = new System.Drawing.Point(15, 25);
            this.numFrameLen.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numFrameLen.Name = "numFrameLen";
            this.numFrameLen.Size = new System.Drawing.Size(120, 20);
            this.numFrameLen.TabIndex = 1;
            this.numFrameLen.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numChannel
            // 
            this.numChannel.Location = new System.Drawing.Point(301, 25);
            this.numChannel.Name = "numChannel";
            this.numChannel.Size = new System.Drawing.Size(120, 20);
            this.numChannel.TabIndex = 2;
            this.numChannel.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Frame Length";
            // 
            // Channel
            // 
            this.Channel.AutoSize = true;
            this.Channel.Location = new System.Drawing.Point(298, 8);
            this.Channel.Name = "Channel";
            this.Channel.Size = new System.Drawing.Size(46, 13);
            this.Channel.TabIndex = 3;
            this.Channel.Text = "Channel";
            // 
            // cboDevices
            // 
            this.cboDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevices.FormattingEnabled = true;
            this.cboDevices.Location = new System.Drawing.Point(587, 25);
            this.cboDevices.Name = "cboDevices";
            this.cboDevices.Size = new System.Drawing.Size(168, 21);
            this.cboDevices.TabIndex = 4;
            this.cboDevices.SelectedIndexChanged += new System.EventHandler(this.cboDevices_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(584, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output Devices";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 479);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboDevices);
            this.Controls.Add(this.Channel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numChannel);
            this.Controls.Add(this.numFrameLen);
            this.Controls.Add(this.txtPatterns);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numFrameLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChannel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPatterns;
        private System.Windows.Forms.NumericUpDown numFrameLen;
        private System.Windows.Forms.NumericUpDown numChannel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Channel;
        private System.Windows.Forms.ComboBox cboDevices;
        private System.Windows.Forms.Label label2;
    }
}

