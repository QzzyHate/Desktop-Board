namespace Project_Q4
{
    partial class RegisterForm
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
            errorMessageLabel = new Label();
            enterAccountButton = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(266, 150);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 0;
            label1.Text = "User Name";
            // 
            // nameTextBox
            // 
            nameTextBox.Anchor = AnchorStyles.None;
            nameTextBox.Location = new Point(337, 142);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(100, 23);
            nameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(266, 186);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "Email";
            // 
            // emailTextBox
            // 
            emailTextBox.Anchor = AnchorStyles.None;
            emailTextBox.Location = new Point(337, 178);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(100, 23);
            emailTextBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(266, 224);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 4;
            label3.Text = "Password";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Anchor = AnchorStyles.None;
            passwordTextBox.Location = new Point(337, 216);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(100, 23);
            passwordTextBox.TabIndex = 5;
            // 
            // registerButton
            // 
            registerButton.Anchor = AnchorStyles.None;
            registerButton.Location = new Point(350, 245);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(75, 23);
            registerButton.TabIndex = 6;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // errorMessageLabel
            // 
            errorMessageLabel.Anchor = AnchorStyles.None;
            errorMessageLabel.AutoSize = true;
            errorMessageLabel.Location = new Point(337, 124);
            errorMessageLabel.Name = "errorMessageLabel";
            errorMessageLabel.Size = new Size(0, 15);
            errorMessageLabel.TabIndex = 7;
            // 
            // enterAccountButton
            // 
            enterAccountButton.Anchor = AnchorStyles.None;
            enterAccountButton.Location = new Point(326, 310);
            enterAccountButton.Name = "enterAccountButton";
            enterAccountButton.Size = new Size(123, 23);
            enterAccountButton.TabIndex = 9;
            enterAccountButton.Text = "Enter Account";
            enterAccountButton.UseVisualStyleBackColor = true;
            enterAccountButton.Click += enterAccountButton_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(372, 281);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 8;
            label4.Text = "- or -";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(761, 484);
            Controls.Add(enterAccountButton);
            Controls.Add(label4);
            Controls.Add(errorMessageLabel);
            Controls.Add(registerButton);
            Controls.Add(passwordTextBox);
            Controls.Add(label3);
            Controls.Add(emailTextBox);
            Controls.Add(label2);
            Controls.Add(nameTextBox);
            Controls.Add(label1);
            Name = "RegisterForm";
            Text = "Register";
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
        private Label errorMessageLabel;
        private Button enterAccountButton;
        private Label label4;
    }
}
