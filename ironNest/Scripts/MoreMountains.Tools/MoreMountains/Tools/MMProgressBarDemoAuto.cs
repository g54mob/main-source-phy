using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMProgressBarDemoAuto : MonoBehaviour
	{
		public enum TestModes
		{
			Permanent = 0,
			OneTime = 1
		}

		public TestModes TestMode;

		[MMEnumCondition("TestMode", new int[] { 0 })]
		public float CurrentValue;

		[MMEnumCondition("TestMode", new int[] { 0 })]
		public float MinValue;

		[MMEnumCondition("TestMode", new int[] { 0 })]
		public float MaxValue;

		[MMEnumCondition("TestMode", new int[] { 0 })]
		public float Speed;

		[MMEnumCondition("TestMode", new int[] { 1 })]
		public float OneTimeNewValue;

		[MMEnumCondition("TestMode", new int[] { 1 })]
		public float OneTimeMinValue;

		[MMEnumCondition("TestMode", new int[] { 1 })]
		public float OneTimeMaxValue;

		[MMEnumCondition("TestMode", new int[] { 1 })]
		[MMInspectorButton("OneTime")]
		public bool OneTimeButton;

		protected float _direction;

		protected MMProgressBar _progressBar;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void OneTime()
		{
		}
	}
}
