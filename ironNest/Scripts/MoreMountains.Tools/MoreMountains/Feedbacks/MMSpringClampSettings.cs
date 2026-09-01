using System;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMSpringClampSettings
	{
		[Header("Min")]
		[Tooltip("whether or not to clamp the min value of this spring, preventing it from going below a certain value")]
		public bool ClampMin;

		[Tooltip("the value below which this spring can't go")]
		[MMCondition("ClampMin", true)]
		public float ClampMinValue;

		[Tooltip("if ClampMin is true, whether or not to use the initial value as the min value")]
		[MMCondition("ClampMin", true)]
		public bool ClampMinInitial;

		[Tooltip("whether or not the spring should bounce off the min value or not")]
		[MMCondition("ClampMin", true)]
		public bool ClampMinBounce;

		[Header("Max")]
		[Tooltip("whether or not to clamp the max value of this spring, preventing it from going above a certain value")]
		public bool ClampMax;

		[Tooltip("the value above which this spring can't go")]
		[MMCondition("ClampMax", true)]
		public float ClampMaxValue;

		[Tooltip("if ClampMax is true, whether or not to use the initial value as the max value")]
		[MMCondition("ClampMax", true)]
		public bool ClampMaxInitial;

		[Tooltip("whether or not the spring should bounce off the max value or not")]
		[MMCondition("ClampMax", true)]
		public bool ClampMaxBounce;

		public bool ClampNeeded => false;

		public virtual float GetTargetValue(float value, float initialValue)
		{
			return 0f;
		}
	}
}
