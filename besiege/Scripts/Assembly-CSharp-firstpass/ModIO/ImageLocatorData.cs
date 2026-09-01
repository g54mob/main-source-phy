using System;

namespace ModIO
{
	[Serializable]
	public struct ImageLocatorData
	{
		public string fileName;

		public string url;

		public static ImageLocatorData CreateFromImageLocator(IImageLocator locator)
		{
			return new ImageLocatorData
			{
				fileName = locator.GetFileName(),
				url = locator.GetURL()
			};
		}
	}
}
