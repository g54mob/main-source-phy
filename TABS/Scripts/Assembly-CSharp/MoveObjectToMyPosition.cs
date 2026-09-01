using UnityEngine;

public class MoveObjectToMyPosition : MonoBehaviour
{
	public Transform target;

	public void MoveObj()
	{
		target.position = base.transform.position;
	}
}
