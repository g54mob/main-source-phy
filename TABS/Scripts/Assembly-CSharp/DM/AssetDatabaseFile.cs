using System;
using System.Collections.Generic;

namespace DM
{
	[Serializable]
	public class AssetDatabaseFile
	{
		[Serializable]
		public struct NonStreamableAsset
		{
			public int modId;

			public int id;

			public int index;
		}

		[Serializable]
		public struct StreamableAsset
		{
			public int modId;

			public int id;

			public string path;
		}

		public string buildDateTime;

		public List<NonStreamableAsset> nonStreamableAssets = new List<NonStreamableAsset>();

		public List<StreamableAsset> streamableAssets = new List<StreamableAsset>();
	}
}
