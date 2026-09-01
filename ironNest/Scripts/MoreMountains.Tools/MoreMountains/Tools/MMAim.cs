using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMAim
	{
		public enum AimControls
		{
			Off = 0,
			PrimaryMovement = 1,
			SecondaryMovement = 2,
			Mouse = 3,
			Script = 4
		}

		public enum RotationModes
		{
			Free = 0,
			Strict4Directions = 1,
			Strict8Directions = 2
		}

		[Header("Control Mode")]
		[MMInformation("Pick a control mode : mouse (aims towards the pointer), primary movement (you'll aim towards the current input direction), or secondary movement (aims towards a second input axis, think twin stick shooters), and set minimum and maximum angles.", MMInformationAttribute.InformationType.Info, false)]
		public AimControls AimControl;

		public RotationModes RotationMode;

		[Header("Limits")]
		[Range(-180f, 180f)]
		public float MinimumAngle;

		[Range(-180f, 180f)]
		public float MaximumAngle;

		[MMReadOnly]
		public float CurrentAngle;

		protected float[] _possibleAngleValues;

		protected Vector3 _currentAim;

		protected Vector3 _direction;

		protected Vector3 _mousePosition;

		protected Vector2 _inputSystemMousePosition;

		protected Camera _mainCamera;

		public virtual Vector3 CurrentPosition { get; set; }

		public virtual Vector2 PrimaryMovement { get; set; }

		public virtual Vector2 SecondaryMovement { get; set; }

		public virtual void Initialization()
		{
		}

		public virtual Vector2 GetCurrentAim()
		{
			return default(Vector2);
		}

		public virtual void SetAim(Vector2 newAim)
		{
		}
	}
}
