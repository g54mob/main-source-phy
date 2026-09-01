using System.Collections.Generic;
using UnityEngine;

public class CollisionStressControl : MonoBehaviour
{
	private void OnCollisionStay(Collision collisionInfo)
	{
		List<Transform> list = new List<Transform>();
		ContactPoint[] contacts = collisionInfo.contacts;
		foreach (ContactPoint contactPoint in contacts)
		{
			Transform transform = contactPoint.thisCollider.transform;
			Material material = transform.GetComponent<Renderer>().material;
			material.color = new Color(0.5f + collisionInfo.relativeVelocity.sqrMagnitude / 2f, material.color.g, material.color.b, material.color.a);
			if (transform.parent != null)
			{
				Material material2 = transform.parent.GetComponent<Renderer>().material;
				material2.color = new Color(0.5f + collisionInfo.relativeVelocity.sqrMagnitude / 5f, material2.color.g, material2.color.b, material2.color.a);
			}
			if (transform.childCount > 0)
			{
				Material material3 = transform.GetChild(0).GetComponent<Renderer>().material;
				material3.color = new Color(0.5f + collisionInfo.relativeVelocity.sqrMagnitude / 5f, material3.color.g, material3.color.b, material3.color.a);
			}
			if (collisionInfo.relativeVelocity.sqrMagnitude > 2f)
			{
				list.Add(transform);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			Transform transform = list[j];
			transform.parent = null;
			if (transform.GetComponent<Rigidbody>() == null)
			{
				transform.gameObject.AddComponent<Rigidbody>();
			}
			transform.GetComponent<Renderer>().material.color = Color.black;
		}
	}
}
