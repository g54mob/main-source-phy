using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Camera/MMBillboard")]
	public class MMBillboard : MonoBehaviour
	{
		[Tooltip("whether or not this object should automatically grab a camera on start")]
		public bool GrabMainCameraOnStart;

		[Tooltip("whether or not to nest this object below a parent container")]
		public bool NestObject;

		[Tooltip("the Vector3 to offset the look at direction by")]
		public Vector3 OffsetDirection;

		[Tooltip("the Vector3 to consider as 'world up'")]
		public Vector3 Up;

		protected GameObject _parentContainer;

		private Transform _transform;

		public virtual Camera MainCamera { get; set; }

		protected virtual void Awake()
		{
		}

		private void Start()
		{
		}

		protected virtual void NestThisObject()
		{
		}

		protected virtual void GrabMainCamera()
		{
		}

		protected virtual void Update()
		{
		}
	}
}
