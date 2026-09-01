namespace Landfall.TABS.Workshop
{
	public class CustomContentDataPackage
	{
		public DatabaseID id { get; private set; }

		public string folderPath { get; private set; }

		public ContentTypeFilter contentType { get; private set; }

		public CustomContentDataPackage(DatabaseID id, string folderPath, ContentTypeFilter contentType)
		{
			this.id = id;
			this.folderPath = folderPath;
			this.contentType = contentType;
		}
	}
}
