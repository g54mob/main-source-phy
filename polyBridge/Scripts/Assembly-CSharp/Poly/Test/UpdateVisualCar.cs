using Poly.Extension;
using UnityEngine;

namespace Poly.Test
{
	public class UpdateVisualCar : MonoBehaviour
	{
		public Transform[] sources;

		public Transform[] targets;

		private void LateUpdate()
		{
			for (int i = 0; i < targets.Length; i++)
			{
				targets[i].position = sources[i].position;
				targets[i].rotation = sources[i].rotation;
				if (targets[i].localScale.x * sources[i].localScale.x < 0f)
				{
					targets[i].SetLocalScaleX(0f - targets[i].localScale.x);
				}
			}
		}
	}
}
