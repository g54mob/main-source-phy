using UnityEngine;
using UnityEngine.UI;

public class RawImageSpriteSheetAnimation : MonoBehaviour
{
	public int frameColumns;

	public int frameRows;

	public int totalFrames;

	public float fps;

	private float counter;

	private RawImage rawImage;

	private void Awake()
	{
		rawImage = GetComponent<RawImage>();
	}

	private void Update()
	{
		counter += Time.deltaTime;
		int num = Mathf.FloorToInt(counter * fps) % totalFrames;
		int num2 = num % frameRows;
		int num3 = num / frameRows;
		rawImage.uvRect = new Rect((float)num3 / (float)frameColumns, 1f - (float)num2 / (float)frameRows - 1f / (float)frameRows, 1f / (float)frameColumns, 1f / (float)frameRows);
	}
}
