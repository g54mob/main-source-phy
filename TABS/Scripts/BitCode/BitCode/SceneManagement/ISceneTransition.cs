using System;

namespace BitCode.SceneManagement
{
	public interface ISceneTransition
	{
		void StartTransition(Action sceneSwitch, bool willEnterLoadingScene);

		void EnteredLoadingScene(Action queueSceneLoad);

		void EnteredFinalScene(Action transitionComplete);
	}
}
