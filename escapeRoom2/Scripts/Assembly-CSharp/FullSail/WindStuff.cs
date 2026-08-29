using System;
using UnityEngine;

namespace FullSail
{
	[ExecuteAlways]
	public class WindStuff : MonoBehaviour
	{
		public float windDir;

		[Range(0f, 10f)]
		public float windStrength;

		public SailGroup group;

		public Transform forwardArrow;

		public Transform windArrow;

		public Transform windScale;

		private void Update()
		{
			if ((bool)windArrow)
			{
				Vector3 eulerAngles = windArrow.eulerAngles;
				eulerAngles.y = windDir + 180f;
				windArrow.eulerAngles = eulerAngles;
			}
			if ((bool)windScale)
			{
				float num = 0.2f + 0.2f * windStrength;
				Vector3 localScale = new Vector3(num, num, num);
				windScale.localScale = localScale;
			}
			if ((bool)group)
			{
				Vector3 val = default(Vector3);
				val.x = Mathf.Sin(windDir * (MathF.PI / 180f));
				val.z = Mathf.Cos(windDir * (MathF.PI / 180f));
				val.y = 0f;
				group.SetValue(SailParamID.WindDir, val);
				group.SetValue(SailParamID.Speed, windStrength);
			}
		}
	}
}
