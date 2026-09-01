using UnityEngine;

public class StayUpright : MonoBehaviour
{
	private void Update()
	{
		base.transform.eulerAngles = new Vector3(0f, base.transform.eulerAngles.y, 0f);
	}
}
