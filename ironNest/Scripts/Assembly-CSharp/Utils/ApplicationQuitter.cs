using UnityEngine;

namespace Utils
{
	public sealed class ApplicationQuitter : MonoBehaviour
	{
		[Header("Quit behavior")]
		[SerializeField]
		[Tooltip("If enabled, quitting while running in the Unity Editor will stop Play Mode instead of doing nothing.\nRecommended: enabled, so your quit button/test input works in-editor too.")]
		private bool stopPlayModeInEditor;

		[SerializeField]
		[Tooltip("Optional exit code to use when quitting (mainly relevant on some platforms/player setups).\nIgnored in the Unity Editor.")]
		private int defaultExitCode;

		public void Quit()
		{
		}

		public void QuitWithExitCode(int exitCode)
		{
		}
	}
}
