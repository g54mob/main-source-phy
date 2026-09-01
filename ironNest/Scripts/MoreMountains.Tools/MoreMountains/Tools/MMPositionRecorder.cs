using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPositionRecorder : MonoBehaviour
	{
		public enum Modes
		{
			Framecount = 0,
			Time = 1
		}

		[Header("Recording Settings")]
		public int NumberOfPositionsToRecord;

		public Modes Mode;

		[MMEnumCondition("Mode", new int[] { 0 })]
		public int FrameInterval;

		[MMEnumCondition("Mode", new int[] { 1 })]
		public float TimeInterval;

		public bool RecordOnTimescaleZero;

		[Header("Debug")]
		public Vector3[] Positions;

		[MMReadOnly]
		public int FrameCounter;

		protected int _frameCountLastRecord;

		protected float _timeLastRecord;

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void StorePositions()
		{
		}
	}
}
