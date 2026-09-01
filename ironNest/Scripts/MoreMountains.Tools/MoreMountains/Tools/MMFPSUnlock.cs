using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Performance/MMFPSUnlock")]
	public class MMFPSUnlock : MonoBehaviour
	{
		[Tooltip("the target FPS you want the game to run at, that's up to how many times Update will run every second")]
		public int TargetFPS;

		[Tooltip("the number of frames to wait before rendering the next one. 0 will render every frame, 1 will render every 2 frames, 5 will render every 5 frames, etc")]
		public int RenderFrameInterval;

		[Range(0f, 2f)]
		[Tooltip("whether vsync should be enabled or not (on a 60Hz screen, 1 : 60fps, 2 : 30fps, 0 : don't wait for vsync)")]
		public int VSyncCount;

		[Tooltip("if this is true, the user can press a number key to change the target FPS (1 : 10fps, 2 : 20fps, etc)")]
		public bool EnableNumberShortcuts;

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void OnValidate()
		{
		}

		protected virtual void UpdateSettings()
		{
		}

		protected virtual void HandleInput()
		{
		}
	}
}
