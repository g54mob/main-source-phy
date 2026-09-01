using System;
using UnityEngine;

namespace Shapes
{
	public class AmmoBar : MonoBehaviour
	{
		public int totalBullets;

		public int bullets;

		[Header("Style")]
		[Range(0f, 1f)]
		public float bulletThicknessScale;

		[Range(0f, 0.5f)]
		public float bulletEjectScale;

		[Header("Animation")]
		public float bulletDisappearTime;

		[Range(0f, (float)Math.PI * 2f)]
		public float bulletEjectAngSpeed;

		[Range(0f, (float)Math.PI * 2f)]
		public float ejectRotSpeedVariance;

		public AnimationCurve bulletEjectX;

		public AnimationCurve bulletEjectY;

		private float[] bulletFireTimes;

		public bool HasBulletsLeft => false;

		private Vector2 GetBulletEjectPos(Vector2 origin, float t)
		{
			return default(Vector2);
		}

		public void Fire()
		{
		}

		public void Reload()
		{
		}

		private void Awake()
		{
		}

		public void DrawBar(FpsController fpsController, float barRadius)
		{
		}
	}
}
