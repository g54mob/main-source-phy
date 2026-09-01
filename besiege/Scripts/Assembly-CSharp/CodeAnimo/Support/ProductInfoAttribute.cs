using System;

namespace CodeAnimo.Support
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ProductInfoAttribute : Attribute
	{
		public string productName;

		public string productVersion;

		public string startupInfo;

		public string folderName;

		public ProductInfoAttribute(string productName, string startupInfo)
		{
			this.productName = productName;
			this.startupInfo = startupInfo;
		}
	}
}
