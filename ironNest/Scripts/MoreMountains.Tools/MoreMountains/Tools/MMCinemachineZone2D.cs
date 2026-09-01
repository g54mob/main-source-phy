using Unity.Cinemachine;
using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Collider2D))]
	public class MMCinemachineZone2D : MMCinemachineZone
	{
		protected Collider2D _collider2D;

		protected Collider2D _confinerCollider2D;

		protected Rigidbody2D _confinerRigidbody2D;

		protected CompositeCollider2D _confinerCompositeCollider2D;

		protected BoxCollider2D _boxCollider2D;

		protected CircleCollider2D _circleCollider2D;

		protected PolygonCollider2D _polygonCollider2D;

		protected CinemachineConfiner2D _cinemachineConfiner;

		protected override void InitializeCollider()
		{
		}

		protected override void SetupConfiner()
		{
		}

		protected virtual void CopyCollider()
		{
		}

		protected virtual void OnTriggerEnter2D(Collider2D collider)
		{
		}

		protected virtual void OnTriggerExit2D(Collider2D collider)
		{
		}
	}
}
