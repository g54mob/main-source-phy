using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMRigidbodyCenterOfMass : MonoBehaviour
	{
		public enum AutomaticSetModes
		{
			Awake = 0,
			Start = 1,
			ScriptOnly = 2
		}

		[Header("CenterOfMass")]
		public Vector3 CenterOfMassOffset;

		[Header("Automation")]
		public AutomaticSetModes AutomaticSetMode;

		public bool AutoDestroyComponentAfterSet;

		[Header("Test")]
		public float GizmoPointSize;

		[MMInspectorButton("SetCenterOfMass")]
		public bool SetCenterOfMassButton;

		protected Vector3 _gizmoCenter;

		protected Rigidbody _rigidbody;

		protected Rigidbody2D _rigidbody2D;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void SetCenterOfMass()
		{
		}

		protected virtual void OnDrawGizmosSelected()
		{
		}
	}
}
