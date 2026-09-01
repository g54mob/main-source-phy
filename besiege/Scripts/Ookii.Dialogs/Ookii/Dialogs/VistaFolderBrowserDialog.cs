using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	[DefaultEvent("HelpRequest")]
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("SelectedPath")]
	[Description("Prompts the user to select a folder.")]
	public sealed class VistaFolderBrowserDialog : CommonDialog
	{
		private FolderBrowserDialog _downlevelDialog;

		private string _description;

		private bool _useDescriptionForTitle;

		private string _selectedPath;

		private Environment.SpecialFolder _rootFolder;

		private bool _showNewFolderButton;

		[Browsable(false)]
		public static bool IsVistaFolderDialogSupported
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater;
			}
		}

		[Category("Folder Browsing")]
		[DefaultValue("")]
		[Localizable(true)]
		[Browsable(true)]
		[Description("The descriptive text displayed above the tree view control in the dialog box, or below the list view control in the Vista style dialog.")]
		public string Description
		{
			get
			{
				if (_downlevelDialog != null)
				{
					return _downlevelDialog.Description;
				}
				return _description;
			}
			set
			{
				if (_downlevelDialog != null)
				{
					_downlevelDialog.Description = value;
				}
				else
				{
					_description = value ?? string.Empty;
				}
			}
		}

		[Localizable(false)]
		[Description("The root folder where the browsing starts from. This property has no effect if the Vista style dialog is used.")]
		[Category("Folder Browsing")]
		[Browsable(true)]
		[DefaultValue(typeof(Environment.SpecialFolder), "Desktop")]
		public Environment.SpecialFolder RootFolder
		{
			get
			{
				if (_downlevelDialog != null)
				{
					return _downlevelDialog.RootFolder;
				}
				return _rootFolder;
			}
			set
			{
				if (_downlevelDialog != null)
				{
					_downlevelDialog.RootFolder = value;
				}
				else
				{
					_rootFolder = value;
				}
			}
		}

		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Description("The path selected by the user.")]
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Folder Browsing")]
		public string SelectedPath
		{
			get
			{
				if (_downlevelDialog != null)
				{
					return _downlevelDialog.SelectedPath;
				}
				return _selectedPath;
			}
			set
			{
				if (_downlevelDialog != null)
				{
					_downlevelDialog.SelectedPath = value;
				}
				else
				{
					_selectedPath = value ?? string.Empty;
				}
			}
		}

		[Browsable(true)]
		[Localizable(false)]
		[Description("A value indicating whether the New Folder button appears in the folder browser dialog box. This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.")]
		[DefaultValue(true)]
		[Category("Folder Browsing")]
		public bool ShowNewFolderButton
		{
			get
			{
				if (_downlevelDialog != null)
				{
					return _downlevelDialog.ShowNewFolderButton;
				}
				return _showNewFolderButton;
			}
			set
			{
				if (_downlevelDialog != null)
				{
					_downlevelDialog.ShowNewFolderButton = value;
				}
				else
				{
					_showNewFolderButton = value;
				}
			}
		}

		[Category("Folder Browsing")]
		[DefaultValue(false)]
		[Description("A value that indicates whether to use the value of the Description property as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.")]
		public bool UseDescriptionForTitle
		{
			get
			{
				return _useDescriptionForTitle;
			}
			set
			{
				_useDescriptionForTitle = value;
			}
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler HelpRequest
		{
			add
			{
				base.HelpRequest += value;
			}
			remove
			{
				base.HelpRequest -= value;
			}
		}

		public VistaFolderBrowserDialog()
		{
			if (!IsVistaFolderDialogSupported)
			{
				_downlevelDialog = new FolderBrowserDialog();
			}
			else
			{
				Reset();
			}
		}

		public override void Reset()
		{
			_description = string.Empty;
			_useDescriptionForTitle = false;
			_selectedPath = string.Empty;
			_rootFolder = Environment.SpecialFolder.Desktop;
			_showNewFolderButton = true;
		}

		protected override bool RunDialog(IntPtr hwndOwner)
		{
			if (_downlevelDialog != null)
			{
				return _downlevelDialog.ShowDialog((hwndOwner == IntPtr.Zero) ? null : new WindowHandleWrapper(hwndOwner)) == DialogResult.OK;
			}
			IFileDialog fileDialog = null;
			try
			{
				fileDialog = (NativeFileOpenDialog)new FileOpenDialogRCW();
				SetDialogProperties(fileDialog);
				int num = fileDialog.Show(hwndOwner);
				if (num < 0)
				{
					if (num == -2147023673)
					{
						return false;
					}
					throw Marshal.GetExceptionForHR(num);
				}
				GetResult(fileDialog);
				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				if (fileDialog != null)
				{
					Marshal.FinalReleaseComObject(fileDialog);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && _downlevelDialog != null)
				{
					_downlevelDialog.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void SetDialogProperties(IFileDialog dialog)
		{
			if (!string.IsNullOrEmpty(_description))
			{
				if (_useDescriptionForTitle)
				{
					dialog.SetTitle(_description);
				}
				else
				{
					IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
					fileDialogCustomize.AddText(0, _description);
				}
			}
			dialog.SetOptions(NativeMethods.FOS.FOS_PICKFOLDERS | NativeMethods.FOS.FOS_FORCEFILESYSTEM | NativeMethods.FOS.FOS_FILEMUSTEXIST);
			if (!string.IsNullOrEmpty(_selectedPath))
			{
				string directoryName = Path.GetDirectoryName(_selectedPath);
				if (directoryName == null || !Directory.Exists(directoryName))
				{
					dialog.SetFileName(_selectedPath);
					return;
				}
				string fileName = Path.GetFileName(_selectedPath);
				dialog.SetFolder(NativeMethods.CreateItemFromParsingName(directoryName));
				dialog.SetFileName(fileName);
			}
		}

		private void GetResult(IFileDialog dialog)
		{
			IShellItem ppsi;
			dialog.GetResult(out ppsi);
			ppsi.GetDisplayName(NativeMethods.SIGDN.SIGDN_FILESYSPATH, out _selectedPath);
		}
	}
}
