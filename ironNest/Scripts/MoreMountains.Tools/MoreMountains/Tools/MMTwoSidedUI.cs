using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	public class MMTwoSidedUI : MonoBehaviour
	{
		public enum Axis
		{
			x = 0,
			y = 1,
			z = 2
		}

		[Header("Bindings")]
		public GameObject Front;

		public GameObject Back;

		[Header("Axis")]
		public Axis FlipAxis;

		public float ScaleThreshold;

		[Header("Events")]
		public UnityEvent OnFlip;

		[Header("Debug")]
		public bool DebugMode;

		[Range(-1f, 1f)]
		public float ScaleValue;

		[MMReadOnly]
		public bool BackVisible;

		protected RectTransform _rectTransform;

		protected bool _initialized;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void IfEditor()
		{
		}

		protected virtual float GetScaleValue()
		{
			return 0f;
		}
	}
}
