using UnityEngine;

public class HideImage : MonoBehaviour
{
	public GameObject controlsImage;

	private void Start()
	{
		controlsImage.SetActive(value: false);
	}
}
