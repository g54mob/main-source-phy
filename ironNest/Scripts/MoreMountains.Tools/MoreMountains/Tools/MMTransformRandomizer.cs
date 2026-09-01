using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMTransformRandomizer : MonoBehaviour
	{
		public enum AutoExecutionModes
		{
			Never = 0,
			OnAwake = 1,
			OnStart = 2,
			OnEnable = 3
		}

		[Header("Position")]
		public bool RandomizePosition;

		[MMCondition("RandomizePosition", true)]
		public Vector3 MinRandomPosition;

		[MMCondition("RandomizePosition", true)]
		public Vector3 MaxRandomPosition;

		[Header("Rotation")]
		public bool RandomizeRotation;

		[MMCondition("RandomizeRotation", true)]
		public Vector3 MinRandomRotation;

		[MMCondition("RandomizeRotation", true)]
		public Vector3 MaxRandomRotation;

		[Header("Scale")]
		public bool RandomizeScale;

		[MMCondition("RandomizeScale", true)]
		public Vector3 MinRandomScale;

		[MMCondition("RandomizeScale", true)]
		public Vector3 MaxRandomScale;

		[Header("Settings")]
		public bool AutoRemoveAfterRandomize;

		public bool RemoveAllColliders;

		public AutoExecutionModes AutoExecutionMode;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void Randomize()
		{
		}

		protected virtual void ProcessRandomizePosition()
		{
		}

		protected virtual void ProcessRandomizeRotation()
		{
		}

		protected virtual void ProcessRandomizeScale()
		{
		}

		protected virtual void RemoveColliders()
		{
		}

		protected virtual void Cleanup()
		{
		}
	}
}
