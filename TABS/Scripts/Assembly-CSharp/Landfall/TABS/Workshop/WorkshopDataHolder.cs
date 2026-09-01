using Steamworks;

namespace Landfall.TABS.Workshop
{
	public class WorkshopDataHolder
	{
		public class WorkshopData
		{
			public bool IsNew { get; private set; }

			public PublishedFileId_t[] Dependicies { get; private set; }

			public PublishedFileId_t PublishFieldID { get; private set; }

			public string DirectoryPath { get; private set; }

			public string FilePath { get; private set; }

			public string PreviewImagePath { get; private set; }

			public string FileName { get; private set; }

			public string Description { get; private set; }

			public void SetFileName(string file)
			{
				FileName = file;
			}

			public void SetDesciption(string desc)
			{
				Description = desc;
			}

			public void SetDirectoryPath(string dirPath)
			{
				DirectoryPath = dirPath;
			}

			public void SetFilePath(string filePath)
			{
				FilePath = filePath;
			}

			public void SetPublishID(PublishedFileId_t pID)
			{
				PublishFieldID = pID;
			}

			public void SetDependecies(PublishedFileId_t[] dependecies)
			{
				Dependicies = dependecies;
			}
		}

		public WorkshopData workshopData = new WorkshopData();

		private static readonly WorkshopDataHolder _instance = new WorkshopDataHolder();

		public static WorkshopDataHolder Instance => _instance;
	}
}
