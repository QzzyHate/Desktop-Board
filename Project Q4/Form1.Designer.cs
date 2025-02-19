namespace Project_Q4
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
            label1 = new Label();
            nameTextBox = new TextBox();
            label2 = new Label();
            emailTextBox = new TextBox();
            label3 = new Label();
            passwordTextBox = new TextBox();
            registerButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(266, 150);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 0;
            label1.Text = "User Name";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(337, 142);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(100, 23);
            nameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(266, 186);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "Email";
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(337, 178);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(100, 23);
            emailTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(266, 224);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 4;
            label3.Text = "Password";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(337, 216);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(100, 23);
            passwordTextBox.TabIndex = 5;
            // 
            // registerButton
            // 
            registerButton.Location = new Point(350, 245);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(75, 23);
            registerButton.TabIndex = 6;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(761, 484);
            Controls.Add(registerButton);
            Controls.Add(passwordTextBox);
            Controls.Add(label3);
            Controls.Add(emailTextBox);
            Controls.Add(label2);
            Controls.Add(nameTextBox);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox nameTextBox;
        private Label label2;
        private TextBox emailTextBox;
        private Label label3;
        private TextBox passwordTextBox;
        private Button registerButton;
    }
}
