using UnityEngine;

public class ZoomLod : MonoBehaviour
{
	public GameObject[] zoomLODs;

	public GameObject[] sceneLODs;

	private void Start()
	{
		syncLODs(isZoom: false);
	}

	public void syncLODs(bool isZoom)
	{
		GameObject[] array = sceneLODs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(!isZoom);
		}
		array = zoomLODs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(isZoom);
		}
	}
}
