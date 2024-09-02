namespace prologue_mgear
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bt_shift_up = new System.Windows.Forms.Button();
            this.bt_shift_down = new System.Windows.Forms.Button();
            this.tmr_scan = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            // 
            // lb_status
            // 
            this.lb_status.AutoSize = true;
            this.lb_status.Location = new System.Drawing.Point(95, 17);
            this.lb_status.Name = "lb_status";
            this.lb_status.Size = new System.Drawing.Size(72, 24);
            this.lb_status.TabIndex = 1;
            this.lb_status.Text = "Waiting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Shift Up:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Shift Down:";
            // 
            // bt_shift_up
            // 
            this.bt_shift_up.Location = new System.Drawing.Point(197, 67);
            this.bt_shift_up.Name = "bt_shift_up";
            this.bt_shift_up.Size = new System.Drawing.Size(128, 32);
            this.bt_shift_up.TabIndex = 3;
            this.bt_shift_up.Text = "-";
            this.bt_shift_up.UseVisualStyleBackColor = true;
            this.bt_shift_up.Click += new System.EventHandler(this.bt_shift_up_Click);
            // 
            // bt_shift_down
            // 
            this.bt_shift_down.Location = new System.Drawing.Point(197, 118);
            this.bt_shift_down.Name = "bt_shift_down";
            this.bt_shift_down.Size = new System.Drawing.Size(128, 32);
            this.bt_shift_down.TabIndex = 3;
            this.bt_shift_down.Text = "-";
            this.bt_shift_down.UseVisualStyleBackColor = true;
            this.bt_shift_down.Click += new System.EventHandler(this.bt_shift_up_Click);
            // 
            // tmr_scan
            // 
            this.tmr_scan.Enabled = true;
            this.tmr_scan.Interval = 5000;
            this.tmr_scan.Tick += new System.EventHandler(this.tmr_scan_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 181);
            this.Controls.Add(this.bt_shift_down);
            this.Controls.Add(this.bt_shift_up);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_status);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Velocity Prologue Manual Gear";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bt_shift_up;
        private System.Windows.Forms.Button bt_shift_down;
        private System.Windows.Forms.Timer tmr_scan;
    }
}

