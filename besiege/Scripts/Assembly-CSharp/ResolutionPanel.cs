using UnityEngine;

public class ResolutionPanel : MonoBehaviour
{
	public GameObject[] resolutionText;

	public void Set(Resolution[] resolutions, int panel)
	{
		int num = resolutionText.Length;
		for (int i = 0; i < num; i++)
		{
			if (i + panel * num < resolutions.Length)
			{
				Resolution resolution = resolutions[i + num * panel];
				resolutionText[i].GetComponent<TextMesh>().text = resolution.width + " x " + resolution.height;
				resolutionText[i].GetComponent<ResolutionController>().Set(resolution.width, resolution.height);
			}
			else
			{
				resolutionText[i].SetActive(false);
			}
		}
	}
}
