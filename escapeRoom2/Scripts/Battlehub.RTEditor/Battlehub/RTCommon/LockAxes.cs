using System.Linq;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class LockAxes : MonoBehaviour
	{
		public bool PositionX;

		public bool PositionY;

		public bool PositionZ;

		public bool RotationX;

		public bool RotationY;

		public bool RotationZ;

		public bool RotationFree;

		public bool RotationScreen;

		public bool ScaleX;

		public bool ScaleY;

		public bool ScaleZ;

		public bool RectXY;

		public bool RectYZ;

		public bool RectXZ;

		public bool PivotMode;

		public RuntimePivotMode PivotModeValue;

		public bool PivotRotation;

		public RuntimePivotRotation PivotRotationValue;

		public void Reset()
		{
			PositionX = (PositionY = (PositionZ = false));
			RotationX = (RotationY = (RotationZ = (RotationFree = (RotationScreen = false))));
			ScaleX = (ScaleY = (ScaleZ = false));
			RectXY = (RectXZ = (RectYZ = false));
			PivotMode = false;
			PivotModeValue = RuntimePivotMode.Center;
			PivotRotation = false;
			PivotRotationValue = RuntimePivotRotation.Local;
		}

		public static LockObject Eval(LockAxes[] lockAxes)
		{
			LockObject lockObject = new LockObject();
			if (lockAxes != null)
			{
				lockObject.PositionX = lockAxes.Any((LockAxes la) => la.PositionX);
				lockObject.PositionY = lockAxes.Any((LockAxes la) => la.PositionY);
				lockObject.PositionZ = lockAxes.Any((LockAxes la) => la.PositionZ);
				lockObject.RotationX = lockAxes.Any((LockAxes la) => la.RotationX);
				lockObject.RotationY = lockAxes.Any((LockAxes la) => la.RotationY);
				lockObject.RotationZ = lockAxes.Any((LockAxes la) => la.RotationZ);
				lockObject.RotationFree = lockAxes.Any((LockAxes la) => la.RotationFree);
				lockObject.RotationScreen = lockAxes.Any((LockAxes la) => la.RotationScreen);
				lockObject.ScaleX = lockAxes.Any((LockAxes la) => la.ScaleX);
				lockObject.ScaleY = lockAxes.Any((LockAxes la) => la.ScaleY);
				lockObject.ScaleZ = lockAxes.Any((LockAxes la) => la.ScaleZ);
				lockObject.RectXY = lockAxes.Any((LockAxes la) => la.RectXY);
				lockObject.RectYZ = lockAxes.Any((LockAxes la) => la.RectYZ);
				lockObject.RectXZ = lockAxes.Any((LockAxes la) => la.RectXZ);
				lockObject.PivotMode = null;
				if (lockAxes.Any((LockAxes la) => la.PivotMode))
				{
					if (lockAxes.All((LockAxes la) => la.PivotModeValue == RuntimePivotMode.Center))
					{
						lockObject.PivotMode = RuntimePivotMode.Center;
					}
					else if (lockAxes.All((LockAxes la) => la.PivotModeValue == RuntimePivotMode.Pivot))
					{
						lockObject.PivotMode = RuntimePivotMode.Pivot;
					}
				}
				lockObject.PivotRotation = null;
				if (lockAxes.Any((LockAxes la) => la.PivotRotation))
				{
					if (lockAxes.All((LockAxes la) => la.PivotRotationValue == RuntimePivotRotation.Global))
					{
						lockObject.PivotRotation = RuntimePivotRotation.Global;
					}
					else if (lockAxes.All((LockAxes la) => la.PivotRotationValue == RuntimePivotRotation.Local))
					{
						lockObject.PivotRotation = RuntimePivotRotation.Local;
					}
				}
			}
			return lockObject;
		}
	}
}
