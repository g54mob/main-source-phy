using System;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	internal class VistaFileDialogEvents : IFileDialogEvents, IFileDialogControlEvents
	{
		private const uint S_OK = 0u;

		private const uint S_FALSE = 1u;

		private const uint E_NOTIMPL = 2147500033u;

		private VistaFileDialog _dialog;

		public VistaFileDialogEvents(VistaFileDialog dialog)
		{
			if (dialog == null)
			{
				throw new ArgumentNullException("dialog");
			}
			_dialog = dialog;
		}

		public HRESULT OnFileOk(IFileDialog pfd)
		{
			if (_dialog.DoFileOk(pfd))
			{
				return HRESULT.S_OK;
			}
			return HRESULT.S_FALSE;
		}

		public HRESULT OnFolderChanging(IFileDialog pfd, IShellItem psiFolder)
		{
			GC.SuppressFinalize(psiFolder);
			return HRESULT.S_OK;
		}

		public void OnFolderChange(IFileDialog pfd)
		{
		}

		public void OnSelectionChange(IFileDialog pfd)
		{
		}

		public void OnShareViolation(IFileDialog pfd, IShellItem psi)
		{
		}

		public void OnTypeChange(IFileDialog pfd)
		{
		}

		public void OnOverwrite(IFileDialog pfd, IShellItem psi)
		{
		}

		public void OnItemSelected(IFileDialogCustomize pfdc, int dwIDCtl, int dwIDItem)
		{
		}

		public void OnButtonClicked(IFileDialogCustomize pfdc, int dwIDCtl)
		{
			if (dwIDCtl == 16385)
			{
				_dialog.DoHelpRequest();
			}
		}

		public void OnCheckButtonToggled(IFileDialogCustomize pfdc, int dwIDCtl, bool bChecked)
		{
		}

		public void OnControlActivating(IFileDialogCustomize pfdc, int dwIDCtl)
		{
		}
	}
}
