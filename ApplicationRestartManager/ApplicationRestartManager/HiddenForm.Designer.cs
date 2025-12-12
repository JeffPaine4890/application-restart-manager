namespace ApplicationRestartManager
{
    partial class HiddenForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            MainLoopTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // MainLoopTimer
            // 
            MainLoopTimer.Enabled = true;
            MainLoopTimer.Interval = 1;
            MainLoopTimer.Tick += MainLoopTimer_Tick;
            // 
            // HiddenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(0, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "HiddenForm";
            Opacity = 0D;
            ShowInTaskbar = false;
            Text = "HiddenForm";
            WindowState = FormWindowState.Minimized;
            FormClosing += HiddenForm_FormClosing;
            Load += HiddenForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer MainLoopTimer;
    }
}
