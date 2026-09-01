using System;

namespace ModIO
{
	[Serializable]
	public class EditableImageLocatorField : EditableField<ImageLocatorData>, IImageLocator
	{
		public string GetFileName()
		{
			return value.fileName;
		}

		public string GetURL()
		{
			return value.url;
		}
	}
}
