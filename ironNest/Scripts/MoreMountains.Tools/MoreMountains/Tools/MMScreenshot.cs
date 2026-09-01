using UnityEngine;
using UnityEngine.InputSystem;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Utilities/MMScreenshot")]
	public class MMScreenshot : MonoBehaviour
	{
		public enum Methods
		{
			ScreenCapture = 0,
			RenderTexture = 1
		}

		public string FolderName;

		[Header("Screenshot")]
		public Methods Method;

		public Key ScreenshotKey;

		[MMEnumCondition("Method", new int[] { 0 })]
		public int GameViewSizeMultiplier;

		[MMEnumCondition("Method", new int[] { 1 })]
		public Camera TargetCamera;

		[MMEnumCondition("Method", new int[] { 1 })]
		public int ResolutionWidth;

		[MMEnumCondition("Method", new int[] { 1 })]
		public int ResolutionHeight;

		[Header("Controls")]
		[MMInspectorButton("TakeScreenshot")]
		public bool TakeScreenshotButton;

		protected virtual void LateUpdate()
		{
		}

		protected virtual void DetectInput()
		{
		}

		protected virtual void TakeScreenshot()
		{
		}

		protected virtual string TakeScreenCaptureScreenshot()
		{
			return null;
		}

		protected virtual string TakeRenderTextureScreenshot()
		{
			return null;
		}
	}
}
