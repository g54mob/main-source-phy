using UnityEngine;

namespace FluffyUnderware.DevTools
{
	public static class DTTime
	{
		private static float _EditorDeltaTime;

		private static float _EditorLastTime;

		public static double TimeSinceStartup => Time.timeSinceLevelLoad;

		public static float deltaTime
		{
			get
			{
				if (!Application.isPlaying)
				{
					return _EditorDeltaTime;
				}
				return Time.deltaTime;
			}
		}

		public static void InitializeEditorTime()
		{
			_EditorLastTime = Time.realtimeSinceStartup;
			_EditorDeltaTime = 0f;
		}

		public static void UpdateEditorTime()
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			_EditorDeltaTime = realtimeSinceStartup - _EditorLastTime;
			_EditorLastTime = realtimeSinceStartup;
		}
	}
}
