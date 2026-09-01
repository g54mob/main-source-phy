using UnityEngine;

namespace Utils;

public sealed class ApplicationQuitter : MonoBehaviour
{
	private bool stopPlayModeInEditor = true;

	private int defaultExitCode;

	public void Quit()
	{
		Application.Quit(defaultExitCode);
	}

	public void QuitWithExitCode(int exitCode)
	{
		Application.Quit(exitCode);
	}
}
