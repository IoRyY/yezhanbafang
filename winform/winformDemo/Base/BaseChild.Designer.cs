namespace yezhanbafang.fw.winform.Demo
{
    partial class BaseChild
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
            this.BasetableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BasetableLayoutPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // BasetableLayoutPanel
            // 
            this.BasetableLayoutPanel.ColumnCount = 7;
            this.BasetableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.BasetableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.BasetableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.BasetableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BasetableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BasetableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BasetableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.BasetableLayoutPanel.Controls.Add(this.panel5, 1, 0);
            this.BasetableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BasetableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.BasetableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.BasetableLayoutPanel.Name = "BasetableLayoutPanel";
            this.BasetableLayoutPanel.RowCount = 1;
            this.BasetableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.BasetableLayoutPanel.Size = new System.Drawing.Size(1061, 702);
            this.BasetableLayoutPanel.TabIndex = 12;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1055, 696);
            this.panel5.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1055, 696);
            this.panel2.TabIndex = 9;
            // 
            // BaseChild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1061, 702);
            this.ControlBox = false;
            this.Controls.Add(this.BasetableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaseChild";
            this.Text = "BaseChild";
            this.SizeChanged += new System.EventHandler(this.BaseChild_SizeChanged);
            this.BasetableLayoutPanel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel BasetableLayoutPanel;
        private System.Windows.Forms.Panel panel5;
        protected System.Windows.Forms.Panel panel2;

    }
}