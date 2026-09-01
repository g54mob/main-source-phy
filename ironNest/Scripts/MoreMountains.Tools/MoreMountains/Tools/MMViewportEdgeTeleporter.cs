using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	public class MMViewportEdgeTeleporter : MonoBehaviour
	{
		[Header("Camera")]
		public bool AutoGrabMainCamera;

		public Camera MainCamera;

		[Header("Viewport Bounds")]
		[MMVector(new string[] { "X", "Y" })]
		public Vector2 ViewportOrigin;

		[MMVector(new string[] { "W", "H" })]
		public Vector2 ViewportDimensions;

		[Header("Teleport Bounds")]
		[MMVector(new string[] { "X", "Y" })]
		public Vector2 TeleportOrigin;

		[MMVector(new string[] { "W", "H" })]
		public Vector2 TeleportDimensions;

		[Header("Events")]
		public UnityEvent OnTeleport;

		protected Vector3 _viewportPosition;

		protected Vector3 _newViewportPosition;

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void SetCamera(Camera newCamera)
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void DetectEdges()
		{
		}
	}
}
