using UnityEngine;

public class MoveGUIElementsWithKeys : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKey("up"))
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime * 4f, base.transform.position.z);
		}
		if (Input.GetKey("down"))
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - Time.deltaTime * 4f, base.transform.position.z);
		}
		if (Input.GetKey("left"))
		{
			base.transform.position = new Vector3(base.transform.position.x - Time.deltaTime * 4f, base.transform.position.y, base.transform.position.z);
		}
		if (Input.GetKey("right"))
		{
			base.transform.position = new Vector3(base.transform.position.x + Time.deltaTime * 4f, base.transform.position.y, base.transform.position.z);
		}
	}
}
