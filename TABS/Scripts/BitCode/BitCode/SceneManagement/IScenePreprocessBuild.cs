using UnityEngine;

namespace BitCode.SceneManagement
{
	public interface IScenePreprocessBuild
	{
		void ProcessForBuild(RuntimePlatform platform);
	}
}
