namespace ClientOne
{
    partial class Form1
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
            MessageTextBox6 = new TextBox();
            ChatTextBox = new TextBox();
            NickName = new TextBox();
            ServerIPtextBox = new TextBox();
            ServerPortTextBox = new TextBox();
            StartButton = new Button();
            SendButton = new Button();
            btnTic1 = new Button();
            btnTic2 = new Button();
            SuspendLayout();
            // 
            // MessageTextBox6
            // 
            MessageTextBox6.Location = new Point(425, 334);
            MessageTextBox6.Multiline = true;
            MessageTextBox6.Name = "MessageTextBox6";
            MessageTextBox6.Size = new Size(298, 79);
            MessageTextBox6.TabIndex = 0;
            // 
            // ChatTextBox
            // 
            ChatTextBox.Location = new Point(425, 174);
            ChatTextBox.Multiline = true;
            ChatTextBox.Name = "ChatTextBox";
            ChatTextBox.Size = new Size(298, 132);
            ChatTextBox.TabIndex = 1;
            // 
            // NickName
            // 
            NickName.Location = new Point(12, 24);
            NickName.Multiline = true;
            NickName.Name = "NickName";
            NickName.Size = new Size(151, 28);
            NickName.TabIndex = 4;
            // 
            // ServerIPtextBox
            // 
            ServerIPtextBox.Location = new Point(425, 39);
            ServerIPtextBox.Multiline = true;
            ServerIPtextBox.Name = "ServerIPtextBox";
            ServerIPtextBox.Size = new Size(150, 28);
            ServerIPtextBox.TabIndex = 5;
            // 
            // ServerPortTextBox
            // 
            ServerPortTextBox.Location = new Point(581, 39);
            ServerPortTextBox.Multiline = true;
            ServerPortTextBox.Name = "ServerPortTextBox";
            ServerPortTextBox.Size = new Size(142, 28);
            ServerPortTextBox.TabIndex = 6;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(430, 86);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(293, 36);
            StartButton.TabIndex = 7;
            StartButton.Text = "START";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // SendButton
            // 
            SendButton.Location = new Point(729, 334);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(59, 79);
            SendButton.TabIndex = 8;
            SendButton.Text = "SEND";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // btnTic1
            // 
            btnTic1.Location = new Point(24, 174);
            btnTic1.Name = "btnTic1";
            btnTic1.Size = new Size(79, 85);
            btnTic1.TabIndex = 9;
            btnTic1.Text = "button3";
            btnTic1.UseVisualStyleBackColor = true;
            btnTic1.Click += btnTic1_Click;
            // 
            // btnTic2
            // 
            btnTic2.Location = new Point(118, 174);
            btnTic2.Name = "btnTic2";
            btnTic2.Size = new Size(79, 85);
            btnTic2.TabIndex = 10;
            btnTic2.Text = "button4";
            btnTic2.UseVisualStyleBackColor = true;
            btnTic2.Click += btnTic2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnTic2);
            Controls.Add(btnTic1);
            Controls.Add(SendButton);
            Controls.Add(StartButton);
            Controls.Add(ServerPortTextBox);
            Controls.Add(ServerIPtextBox);
            Controls.Add(NickName);
            Controls.Add(ChatTextBox);
            Controls.Add(MessageTextBox6);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MessageTextBox6;
        private TextBox ChatTextBox;
        private TextBox NickName;
        private TextBox ServerIPtextBox;
        private TextBox ServerPortTextBox;
        private Button StartButton;
        private Button SendButton;
        private Button btnTic1;
        private Button btnTic2;
    }
}