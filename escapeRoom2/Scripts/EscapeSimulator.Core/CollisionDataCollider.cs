using System.Collections.Generic;
using UnityEngine;

public class CollisionDataCollider : MonoBehaviour
{
	public Collider thisCollider;

	public HashSet<CollisionData> allTouchingDatas = new HashSet<CollisionData>();

	public void OnDisable()
	{
		foreach (CollisionData allTouchingData in allTouchingDatas)
		{
			allTouchingData.removeCollider(thisCollider);
		}
		allTouchingDatas.Clear();
	}
}
