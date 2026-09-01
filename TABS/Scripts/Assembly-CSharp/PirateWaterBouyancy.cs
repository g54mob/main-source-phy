using UnityEngine;

public class PirateWaterBouyancy : MonoBehaviour
{
	private void Update()
	{
		Vector3 position = base.transform.position;
		position.y = PirateWaterManager.GetYLevel(position);
		base.transform.position = Vector3.Lerp(base.transform.position, position, Time.deltaTime * 2.5f);
	}
}
