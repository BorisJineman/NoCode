using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace NoCode.FBDCore
{
	public class EditNetworkLabelForm : Form
	{
		private string _value;
		private IContainer components;
		private Button button1;
		private Button button2;
		private Label label1;
		private TextBox textBox1;
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				if (this._value != null)
				{
					this.textBox1.Text = this._value;
				}
			}
		}
		public EditNetworkLabelForm()
		{
			this.InitializeComponent();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			this._value = this.textBox1.Text;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}
		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}
		private void EditNetworkLabelForm_Load(object sender, EventArgs e)
		{
			if (this._value != null)
			{
				this.textBox1.Text = this._value;
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.button1 = new Button();
			this.button2 = new Button();
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			base.SuspendLayout();
			this.button1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.button1.Location = new Point(384, 67);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "确定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.button2.DialogResult = DialogResult.Cancel;
			this.button2.Location = new Point(465, 67);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(59, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "注释内容:";
			this.textBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.textBox1.Location = new Point(12, 35);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(528, 21);
			this.textBox1.TabIndex = 0;
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.button2;
			base.ClientSize = new Size(552, 102);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			this.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "EditNetworkLabelForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "网络注释";
			base.Load += new EventHandler(this.EditNetworkLabelForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
