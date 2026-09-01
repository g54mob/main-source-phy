using UnityEngine;

namespace TFBGames
{
	public class XboxOneBooter : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("The default TabsBooter. It will be temporarily marked as DontDetroyOnLoad while the title screen is loaded.")]
		protected TABSBooter tabsBooter;

		[SerializeField]
		[Tooltip("Title screen scene to load.")]
		protected string titleScreen = "XboxOneTitleScreen";

		private bool didLoadTitleScreen;
	}
}
