using UnityEngine;

namespace SimpleFileBrowser
{
	public class FileBrowserQuickLink : FileBrowserItem
	{
		private string m_targetPath;

		public string TargetPath => m_targetPath;

		public void SetQuickLink(Sprite icon, string name, string targetPath)
		{
			SetFile(icon, name, isDirectory: true);
			m_targetPath = targetPath;
		}
	}
}
