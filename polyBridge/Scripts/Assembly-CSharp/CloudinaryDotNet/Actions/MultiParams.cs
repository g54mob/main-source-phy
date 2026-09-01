using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class MultiParams : MultiAssetsParams
	{
		public MultiParams(string tag)
			: base(tag)
		{
		}

		public MultiParams(List<string> urls)
			: base(urls)
		{
		}
	}
}
