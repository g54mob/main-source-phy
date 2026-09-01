using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

namespace Landfall.TABC
{
	public class SmartCamera : MonoBehaviour
	{
		public float distanceMultiplier = 1f;

		public float baseDistance = 1f;

		public float velocityMultiplier = 1f;

		public float velocityLerpSpeed = 1f;

		private Vector3 velocity;

		private Vector3 startPos;

		private void Start()
		{
			startPos = base.transform.position;
		}

		private void Update()
		{
			Vector3 vector;
			if (RoundHandler.instance.roundState == RoundHandler.RoundState.Battle)
			{
				float num = float.MaxValue;
				float num2 = float.MinValue;
				float num3 = float.MaxValue;
				float num4 = float.MinValue;
				List<Unit> battlingUnits = BattleManager.instance.battlingUnits;
				for (int i = 0; i < battlingUnits.Count; i++)
				{
					if (!battlingUnits[i].data.Dead)
					{
						float x = battlingUnits[i].data.mainRig.position.x;
						float z = battlingUnits[i].data.mainRig.position.z;
						if (x > num4)
						{
							num4 = x;
						}
						if (z > num2)
						{
							num2 = z;
						}
						if (x < num3)
						{
							num3 = x;
						}
						if (z < num)
						{
							num = z;
						}
					}
				}
				vector = new Vector3(num3 + num4, 0f, num + num2) * 0.5f;
				float num5 = Mathf.Abs(num3 - num4);
				float num6 = Mathf.Abs(num - num2);
				float num7 = num5;
				if (num6 > num7)
				{
					num7 = num6;
				}
				num7 += baseDistance;
				num7 *= distanceMultiplier;
				vector += -base.transform.forward * num7;
			}
			else
			{
				vector = startPos;
			}
			velocity = Vector3.Lerp(velocity, vector - base.transform.position, Time.deltaTime * velocityLerpSpeed);
			base.transform.position += velocity * Time.deltaTime;
		}
	}
}
