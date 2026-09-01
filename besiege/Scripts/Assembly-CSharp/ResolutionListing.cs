using UnityEngine;

public class ResolutionListing : MonoBehaviour
{
	private void Start()
	{
		Resolution[] resolutions = Screen.resolutions;
		Resolution[] array = resolutions;
		for (int i = 0; i < array.Length; i++)
		{
			Resolution resolution = array[i];
			MonoBehaviour.print(resolution.width + "x" + resolution.height);
		}
	}

	private void Update()
	{
	}
}
