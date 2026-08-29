using UnityEngine;

namespace CC
{
	public class TransformBone : MonoBehaviour
	{
		public enum mode
		{
			Height = 0,
			FootRotation = 1,
			BallRotation = 2
		}

		public enum axis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		public Vector3 offset = new Vector3(0f, 0f, 0f);

		public axis Axis;

		public mode Mode;

		public void SetOffset(FootOffset footOffset)
		{
			float num = Mode switch
			{
				mode.Height => footOffset.HeightOffset, 
				mode.FootRotation => footOffset.FootRotation, 
				mode.BallRotation => footOffset.BallRotation, 
				_ => 0f, 
			};
			switch (Axis)
			{
			case axis.X:
				offset = new Vector3(num, 0f, 0f);
				break;
			case axis.Y:
				offset = new Vector3(0f, num, 0f);
				break;
			case axis.Z:
				offset = new Vector3(0f, 0f, num);
				break;
			}
		}

		private void LateUpdate()
		{
			switch (Mode)
			{
			case mode.Height:
				base.transform.position = base.transform.position + offset / 100f;
				break;
			case mode.FootRotation:
				base.transform.eulerAngles = base.transform.eulerAngles + offset;
				break;
			case mode.BallRotation:
				base.transform.eulerAngles = base.transform.eulerAngles + offset;
				break;
			}
		}
	}
}
