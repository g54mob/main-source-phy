using UnityEngine;

namespace Lofelt.NiceVibrations
{
	public class MMFPSUnlock : MonoBehaviour
	{
		public int TargetFPS;

		[Range(0f, 2f)]
		public int VSyncCount;

		protected virtual void Start()
		{
		}

		protected virtual void OnValidate()
		{
		}

		protected virtual void UpdateSettings()
		{
		}
	}
}
