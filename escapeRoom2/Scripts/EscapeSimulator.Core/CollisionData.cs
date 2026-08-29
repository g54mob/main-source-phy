using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionData : MonoBehaviour
{
	[NonSerialized]
	private readonly List<Game.CollisionEventData> onEnterCollisions = new List<Game.CollisionEventData>();

	[NonSerialized]
	private readonly List<Game.CollisionEventData> onExitCollisions = new List<Game.CollisionEventData>();

	private readonly List<Collider> allTouching = new List<Collider>();

	public List<Collider> allTouchingNonAllocUnsafe => allTouching;

	public bool isTouching(Collider collider)
	{
		return allTouching.Contains(collider);
	}

	public Collider[] allTouchingSnapshot()
	{
		return allTouching.ToArray();
	}

	private void OnTriggerEnter(Collider col)
	{
		addCollider(col);
	}

	private void OnTriggerExit(Collider col)
	{
		removeCollider(col);
	}

	private void OnCollisionEnter(Collision collision)
	{
		addCollider(collision.collider);
	}

	private void OnCollisionExit(Collision collision)
	{
		removeCollider(collision.collider);
	}

	private void Update()
	{
		foreach (Game.CollisionEventData onEnterCollision in onEnterCollisions)
		{
			if (allTouching.Contains(onEnterCollision.collider))
			{
				Game.onCollisionsEnter.Add(onEnterCollision);
			}
		}
		foreach (Game.CollisionEventData onExitCollision in onExitCollisions)
		{
			if (!allTouching.Contains(onExitCollision.collider))
			{
				Game.onCollisionsExit.Add(onExitCollision);
			}
		}
		onEnterCollisions.Clear();
		onExitCollisions.Clear();
	}

	private void addCollider(Collider collider)
	{
		if (!allTouching.Contains(collider))
		{
			allTouching.Add(collider);
			onEnterCollisions.Add(new Game.CollisionEventData
			{
				source = this,
				collider = collider,
				frame = Time.frameCount
			});
			CollisionDataCollider collisionDataCollider = collider.GetComponent<CollisionDataCollider>();
			if (collisionDataCollider == null)
			{
				collisionDataCollider = collider.gameObject.AddComponent<CollisionDataCollider>();
				collisionDataCollider.thisCollider = collider;
			}
			collisionDataCollider.allTouchingDatas.Add(this);
		}
	}

	public void removeCollider(Collider collider)
	{
		if (allTouching.Contains(collider))
		{
			allTouching.Remove(collider);
			onExitCollisions.Add(new Game.CollisionEventData
			{
				source = this,
				collider = collider,
				frame = Time.frameCount
			});
		}
	}
}
