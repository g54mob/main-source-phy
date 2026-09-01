using System.Collections.Generic;

namespace CloudinaryDotNet.Actions
{
	public class SpriteParams : MultiAssetsParams
	{
		public SpriteParams(string tag)
			: base(tag)
		{
		}

		public SpriteParams(List<string> urls)
			: base(urls)
		{
		}
	}
}
