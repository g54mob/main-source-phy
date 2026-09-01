using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(SaveFileDialog), "SaveFileDialog.bmp")]
	[Description("Prompts the user to open a file.")]
	public class VistaSaveFileDialog : VistaFileDialog
	{
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("A value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.")]
		public bool CreatePrompt
		{
			get
			{
				if (base.DownlevelDialog != null)
				{
					return ((SaveFileDialog)base.DownlevelDialog).CreatePrompt;
				}
				return GetOption(NativeMethods.FOS.FOS_CREATEPROMPT);
			}
			set
			{
				if (base.DownlevelDialog != null)
				{
					((SaveFileDialog)base.DownlevelDialog).CreatePrompt = value;
				}
				else
				{
					SetOption(NativeMethods.FOS.FOS_CREATEPROMPT, value);
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("A value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.")]
		public bool OverwritePrompt
		{
			get
			{
				if (base.DownlevelDialog != null)
				{
					return ((SaveFileDialog)base.DownlevelDialog).OverwritePrompt;
				}
				return GetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT);
			}
			set
			{
				if (base.DownlevelDialog != null)
				{
					((SaveFileDialog)base.DownlevelDialog).OverwritePrompt = value;
				}
				else
				{
					SetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT, value);
				}
			}
		}

		public VistaSaveFileDialog()
			: this(false)
		{
		}

		public VistaSaveFileDialog(bool forceDownlevel)
		{
			if (forceDownlevel || !VistaFileDialog.IsVistaFileDialogSupported)
			{
				base.DownlevelDialog = new SaveFileDialog();
			}
		}

		public override void Reset()
		{
			base.Reset();
			if (base.DownlevelDialog == null)
			{
				OverwritePrompt = true;
			}
		}

		public Stream OpenFile()
		{
			if (base.DownlevelDialog != null)
			{
				return ((SaveFileDialog)base.DownlevelDialog).OpenFile();
			}
			string fileName = base.FileName;
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("FileName");
			}
			return new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
		}

		protected override void OnFileOk(CancelEventArgs e)
		{
			if (base.DownlevelDialog == null)
			{
				if (CheckFileExists && !File.Exists(base.FileName))
				{
					PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.FileNotFound, Path.GetFileName(base.FileName)), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					e.Cancel = true;
					return;
				}
				if (CreatePrompt && !File.Exists(base.FileName) && !PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.CreatePrompt, Path.GetFileName(base.FileName)), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
				{
					e.Cancel = true;
					return;
				}
			}
			base.OnFileOk(e);
		}

		internal override IFileDialog CreateFileDialog()
		{
			return (NativeFileSaveDialog)new FileSaveDialogRCW();
		}
	}
}
