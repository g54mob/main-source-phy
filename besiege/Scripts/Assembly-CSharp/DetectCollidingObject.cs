using UnityEngine;

public class DetectCollidingObject : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(base.gameObject.name + " was hit by " + collision.gameObject.name);
	}
}
