using UnityEngine;

namespace Common
{
	public class FrameRateSetter : MonoBehaviour
	{
		[Range(10f, 120f)]
		public int targetFrameRate = 60;

		public bool maxFrameRate;

		public bool applyInBuildAlso;

		private void Awake()
		{
			SetFrameRate();
		}

		private void OnValidate()
		{
			SetFrameRate();
		}

		private void SetFrameRate()
		{
			if (Application.isEditor || applyInBuildAlso)
			{
				if (maxFrameRate)
				{
					QualitySettings.vSyncCount = 0;
					Application.targetFrameRate = -1;
				}
				else
				{
					Application.targetFrameRate = targetFrameRate;
				}
			}
		}
	}
}
