using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	internal class InputDialogForm : ExtendedForm
	{
		private SizeF _textMargin = new SizeF(12f, 9f);

		private string _mainInstruction;

		private string _content;

		private IContainer components = null;

		private Panel _primaryPanel;

		private Panel _secondaryPanel;

		private Button _cancelButton;

		private Button _okButton;

		private TextBox _inputTextBox;

		public string MainInstruction
		{
			get
			{
				return _mainInstruction;
			}
			set
			{
				_mainInstruction = value;
			}
		}

		public string Content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}

		public string Input
		{
			get
			{
				return _inputTextBox.Text;
			}
			set
			{
				_inputTextBox.Text = value;
			}
		}

		public int MaxLength
		{
			get
			{
				return _inputTextBox.MaxLength;
			}
			set
			{
				_inputTextBox.MaxLength = value;
			}
		}

		public bool UsePasswordMasking
		{
			get
			{
				return _inputTextBox.UseSystemPasswordChar;
			}
			set
			{
				_inputTextBox.UseSystemPasswordChar = value;
			}
		}

		public event EventHandler<OkButtonClickedEventArgs> OkButtonClicked;

		public InputDialogForm()
		{
			InitializeComponent();
		}

		protected virtual void OnOkButtonClicked(OkButtonClickedEventArgs e)
		{
			if (this.OkButtonClicked != null)
			{
				this.OkButtonClicked(this, e);
			}
		}

		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			_textMargin = new SizeF(_textMargin.Width * factor.Width, _textMargin.Height * factor.Height);
			base.ScaleControl(factor, specified);
		}

		private void SizeDialog()
		{
			int horizontalSpacing = (int)_textMargin.Width * 2;
			int verticalSpacing = base.ClientSize.Height - _inputTextBox.Top + (int)_textMargin.Height * 3;
			using (Graphics dc = _primaryPanel.CreateGraphics())
			{
				base.ClientSize = DialogHelper.SizeDialog(dc, MainInstruction, Content, Screen.FromControl(this), new Font(Font, FontStyle.Bold), Font, horizontalSpacing, verticalSpacing, base.ClientSize.Width, 0);
			}
		}

		private static void DrawThemeBackground(IDeviceContext dc, VisualStyleElement element, Rectangle bounds, Rectangle clipRectangle)
		{
			if (DialogHelper.IsTaskDialogThemeSupported)
			{
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(element);
				visualStyleRenderer.DrawBackground(dc, bounds, clipRectangle);
			}
		}

		private void DrawText(IDeviceContext dc, ref Point location, bool measureOnly, int width)
		{
			DialogHelper.DrawText(dc, MainInstruction, Content, ref location, new Font(Font, FontStyle.Bold), Font, measureOnly, width);
		}

		private void _primaryPanel_Paint(object sender, PaintEventArgs e)
		{
			DrawThemeBackground(e.Graphics, AdditionalVisualStyleElements.TaskDialog.PrimaryPanel, _primaryPanel.ClientRectangle, e.ClipRectangle);
			Point location = new Point((int)_textMargin.Width, (int)_textMargin.Height);
			DrawText(e.Graphics, ref location, false, base.ClientSize.Width - (int)_textMargin.Width * 2);
		}

		private void _secondaryPanel_Paint(object sender, PaintEventArgs e)
		{
			DrawThemeBackground(e.Graphics, AdditionalVisualStyleElements.TaskDialog.SecondaryPanel, _secondaryPanel.ClientRectangle, e.ClipRectangle);
		}

		private void NewInputBoxForm_Load(object sender, EventArgs e)
		{
			SizeDialog();
			CenterToScreen();
		}

		private void _okButton_Click(object sender, EventArgs e)
		{
			OkButtonClickedEventArgs e2 = new OkButtonClickedEventArgs(_inputTextBox.Text, this);
			OnOkButtonClicked(e2);
			if (!e2.Cancel)
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ookii.Dialogs.InputDialogForm));
			this._primaryPanel = new System.Windows.Forms.Panel();
			this._inputTextBox = new System.Windows.Forms.TextBox();
			this._secondaryPanel = new System.Windows.Forms.Panel();
			this._cancelButton = new System.Windows.Forms.Button();
			this._okButton = new System.Windows.Forms.Button();
			this._primaryPanel.SuspendLayout();
			this._secondaryPanel.SuspendLayout();
			base.SuspendLayout();
			this._primaryPanel.Controls.Add(this._inputTextBox);
			resources.ApplyResources(this._primaryPanel, "_primaryPanel");
			this._primaryPanel.Name = "_primaryPanel";
			this._primaryPanel.Paint += new System.Windows.Forms.PaintEventHandler(_primaryPanel_Paint);
			resources.ApplyResources(this._inputTextBox, "_inputTextBox");
			this._inputTextBox.Name = "_inputTextBox";
			this._secondaryPanel.Controls.Add(this._cancelButton);
			this._secondaryPanel.Controls.Add(this._okButton);
			resources.ApplyResources(this._secondaryPanel, "_secondaryPanel");
			this._secondaryPanel.Name = "_secondaryPanel";
			this._secondaryPanel.Paint += new System.Windows.Forms.PaintEventHandler(_secondaryPanel_Paint);
			resources.ApplyResources(this._cancelButton, "_cancelButton");
			this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.UseVisualStyleBackColor = true;
			resources.ApplyResources(this._okButton, "_okButton");
			this._okButton.Name = "_okButton";
			this._okButton.UseVisualStyleBackColor = true;
			this._okButton.Click += new System.EventHandler(_okButton_Click);
			base.AcceptButton = this._okButton;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this._cancelButton;
			base.Controls.Add(this._primaryPanel);
			base.Controls.Add(this._secondaryPanel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InputDialogForm";
			base.ShowInTaskbar = false;
			base.UseSystemFont = true;
			base.Load += new System.EventHandler(NewInputBoxForm_Load);
			this._primaryPanel.ResumeLayout(false);
			this._primaryPanel.PerformLayout();
			this._secondaryPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
