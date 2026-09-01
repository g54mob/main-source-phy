using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMPropertyReceiver : MMPropertyPicker
	{
		public bool ShouldModifyValue;

		public bool RelativeValue;

		public bool ModifyX;

		public bool ModifyY;

		public bool ModifyZ;

		public bool ModifyW;

		public float Threshold;

		public bool BoolRemapZero;

		public bool BoolRemapOne;

		public string StringRemapZero;

		public string StringRemapOne;

		public int IntRemapZero;

		public int IntRemapOne;

		public float FloatRemapZero;

		public float FloatRemapOne;

		public Vector2 Vector2RemapZero;

		public Vector2 Vector2RemapOne;

		public Vector3 Vector3RemapZero;

		public Vector3 Vector3RemapOne;

		public Vector4 Vector4RemapZero;

		public Vector4 Vector4RemapOne;

		public Vector3 QuaternionRemapZero;

		public Vector3 QuaternionRemapOne;

		[ColorUsage(true, true)]
		public Color ColorRemapZero;

		[ColorUsage(true, true)]
		public Color ColorRemapOne;

		public float Level;

		public virtual void SetLevel(float newLevel)
		{
		}
	}
}
