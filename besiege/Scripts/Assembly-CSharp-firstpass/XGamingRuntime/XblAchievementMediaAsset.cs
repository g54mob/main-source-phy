using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblAchievementMediaAsset
	{
		public string Name { get; private set; }

		public XblAchievementMediaAssetType MediaAssetType { get; private set; }

		public string Url { get; private set; }

		internal XblAchievementMediaAsset(XGamingRuntime.Interop.XblAchievementMediaAsset mediaAsset)
		{
			Name = mediaAsset.name.GetString();
			MediaAssetType = mediaAsset.mediaAssetType;
			Url = mediaAsset.url.GetString();
		}
	}
}
