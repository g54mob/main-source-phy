using UnityEngine;
using UnityEngine.UI;

public class MapImageLoader : MonoBehaviour
{
	public enum ImageTypes
	{
		Primary = 0,
		Topography = 1
	}

	public ImageTypes ImageType;

	public Image Image_Map;

	public void Start()
	{
	}
}
