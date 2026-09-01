using UnityEngine;

public class StayUprightAlternate : MonoBehaviour
{
	private void Update()
	{
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, 0f);
	}
}
