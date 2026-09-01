using System.Collections;
using UnityEngine;

namespace SRDebugger.Internal
{
	public class BugReportScreenshotUtil
	{
		public static byte[] ScreenshotData;

		public static IEnumerator ScreenshotCaptureCo()
		{
			if (ScreenshotData != null)
			{
				Debug.LogWarning("[SRDebugger] Warning, overriding existing screenshot data.");
			}
			yield return new WaitForEndOfFrame();
			Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
			tex.Apply();
			ScreenshotData = tex.EncodeToPNG();
			Object.Destroy(tex);
		}
	}
}
