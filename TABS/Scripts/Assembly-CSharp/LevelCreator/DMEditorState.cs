using System.Collections.Generic;
using System.IO;

namespace LevelCreator
{
	public class DMEditorState
	{
		public int CurrentHistoryEntry = -1;

		public List<DeltaModel> HistoryDeltaModel = new List<DeltaModel>();

		public int NextHistoryId;

		public int HistoryId;

		public string CurrentFilePath { get; private set; }

		public bool MapIsDirty { get; set; }

		public void SetCurrentFilePath(string filePath)
		{
			if (!string.IsNullOrEmpty(filePath) && Path.HasExtension(filePath))
			{
				filePath = Path.ChangeExtension(filePath, null);
			}
			CurrentFilePath = filePath;
		}
	}
}
