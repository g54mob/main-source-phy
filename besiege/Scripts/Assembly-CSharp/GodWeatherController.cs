using UnityEngine;

public class GodWeatherController : MonoBehaviour
{
	public Transform tornado;

	private RaycastHit hit;

	private void Update()
	{
		if (StatMaster.levelSimulating && Input.GetKeyDown("1") && Physics.Raycast(Camera.main.ScreenPointToRay(InputManager.CursorPosition()), out hit, 100f))
		{
			Object.Instantiate(tornado, hit.point, Quaternion.identity, ReferenceMaster.physicsGoalInstance);
		}
	}
}
