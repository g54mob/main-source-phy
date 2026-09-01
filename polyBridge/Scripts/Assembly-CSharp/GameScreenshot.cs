using System.IO;
using UnityEngine;

public class GameScreenshot
{
	public static readonly string SCREENSHOTS_DIRECTORY = "Screenshots";

	public static string m_LocalizedRoot;

	private static int m_NextLangIndex = -1;

	private static int m_NextScreenShotCounter;

	public static void UpdateManual()
	{
		if (string.IsNullOrEmpty(m_LocalizedRoot))
		{
			if (GameInput.JustPressed(BindingType.SCREENSHOT))
			{
				CaptureScreenshot($"screen_{Utils.GenerateUniqueId()}.png");
			}
			return;
		}
		if (GameInput.JustPressed(BindingType.SCREENSHOT))
		{
			m_NextLangIndex = 0;
			m_NextScreenShotCounter = 0;
		}
		m_NextScreenShotCounter--;
		if (m_NextLangIndex >= 0 && m_NextScreenShotCounter < 0)
		{
			string text = (string.IsNullOrEmpty(m_LocalizedRoot) ? Utils.GenerateUniqueId() : m_LocalizedRoot);
			CaptureScreenshot("screen_" + text + "_" + Localize.m_BuiltInLaguageCodes[m_NextLangIndex] + ".png");
			m_NextLangIndex++;
			m_NextScreenShotCounter = 20;
			if (m_NextLangIndex >= Localize.m_BuiltInLaguageCodes.Length)
			{
				m_NextLangIndex = -1;
			}
		}
		if (m_NextScreenShotCounter == 10)
		{
			if (m_NextLangIndex < 0)
			{
				Profiles.m_ActiveProfile.m_LanguageCode = Localize.m_BuiltInLaguageCodes[0];
				Localize.SwitchToLanguage(Profiles.m_ActiveProfile.m_LanguageCode);
			}
			else
			{
				Profiles.m_ActiveProfile.m_LanguageCode = Localize.m_BuiltInLaguageCodes[m_NextLangIndex];
				Localize.SwitchToLanguage(Profiles.m_ActiveProfile.m_LanguageCode);
			}
		}
	}

	public static void UpdateManualSingle()
	{
	}

	private static void CaptureScreenshot(string filename)
	{
		string text = Path.Combine(Application.persistentDataPath, SCREENSHOTS_DIRECTORY);
		Utils.CreateDirectory(text);
		if (Directory.Exists(text))
		{
			string text2 = Path.Combine(text, filename);
			ScreenCapture.CaptureScreenshot(text2);
			Debug.LogFormat("Screenshot written to: {0}", text2);
		}
	}
}
