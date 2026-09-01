using System.IO;

namespace Landfall.TABS.Workshop
{
	public class GenericCustomContentWrapper
	{
		public WorkshopContentType ContentType { get; private set; }

		public string ItemName { get; private set; }

		public string DirectoryPath { get; private set; }

		public string FullFilePath { get; private set; }

		public DatabaseID ID { get; private set; }

		public UnitBlueprint BluePrint { get; private set; }

		public GenericCustomContentWrapper(string title, string fullFilePath, DatabaseID id, WorkshopContentType content)
		{
			ItemName = title;
			FullFilePath = fullFilePath;
			DirectoryPath = new FileInfo(fullFilePath).Directory.FullName;
			ContentType = content;
			ID = id;
		}

		public GenericCustomContentWrapper(string title, string path, DatabaseID id, WorkshopContentType content, UnitBlueprint bluePrint)
		{
			ItemName = title;
			DirectoryPath = path;
			ContentType = content;
			ID = id;
			BluePrint = bluePrint;
		}
	}
}
