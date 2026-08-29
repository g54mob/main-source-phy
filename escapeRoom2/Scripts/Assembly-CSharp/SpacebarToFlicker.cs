using UnityEngine;

public class SpacebarToFlicker : MonoBehaviour
{
	public LightFlickering lightFlickering;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			lightFlickering.Flicker();
		}
	}
}
