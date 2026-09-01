using UnityEngine;

public class InterruptCatOnCollision : MonoBehaviour
{
	[SerializeField]
	private CatPickUpHandler _catPickUpHandler;

	[SerializeField]
	private new string tag;

	private void OnTriggerEnter(Collider other)
	{
	}
}
