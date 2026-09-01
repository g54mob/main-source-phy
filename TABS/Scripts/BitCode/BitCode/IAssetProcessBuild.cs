using UnityEngine;

namespace BitCode
{
	public interface IAssetProcessBuild
	{
		bool RestoreState { get; }

		bool Preprocess(RuntimePlatform platform);

		bool Postprocess(RuntimePlatform platform);
	}
}
