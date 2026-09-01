using System.Collections;
using UnityEngine;

namespace Landfall.TABC
{
	public class TimeCounter : MonoBehaviour
	{
		public static TimeCounter instance;

		public float timeLeft;

		public bool isCounting;

		private void Awake()
		{
			instance = this;
		}

		public static IEnumerator Wait(float timeToWait)
		{
			instance.isCounting = true;
			instance.timeLeft = timeToWait;
			while (instance.timeLeft > 0f)
			{
				instance.timeLeft -= Time.deltaTime;
				yield return null;
			}
			instance.isCounting = false;
		}
	}
}
