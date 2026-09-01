using UnityEngine;

namespace LevelCreator
{
	public class SetTargetPosition : MonoBehaviour
	{
		public enum TraceMode
		{
			TargetPosition = 0,
			TargetPositionIncludingWater = 1,
			TargetPositionOnVolume = 2,
			TargetPositionOnVolumeIncludingWater = 3
		}

		public TraceMode traceMode;

		private void Update()
		{
			base.transform.position = Vector3.Lerp(base.transform.position, GetPosition(), Time.deltaTime * 35f);
		}

		private void OnEnable()
		{
			base.transform.position = GetPosition();
		}

		private Vector3 GetPosition()
		{
			Transform transform = DMEditor.Instance.playerCamera.transform;
			float rayDistance = DMEditor.Instance.rayDistance;
			switch (traceMode)
			{
			case TraceMode.TargetPosition:
				return Utility.GetTargetPosition(transform.position, transform.forward, rayDistance);
			case TraceMode.TargetPositionIncludingWater:
				return Utility.GetTargetPositionIncludingWater(transform.position, transform.forward, rayDistance);
			case TraceMode.TargetPositionOnVolume:
				return Utility.GetTargetPositionOnVolume(transform.position, transform.forward, rayDistance);
			case TraceMode.TargetPositionOnVolumeIncludingWater:
				return Utility.GetTargetPositionOnVolumeIncludingWater(transform.position, transform.forward, rayDistance);
			default:
				return base.transform.position;
			}
		}
	}
}
