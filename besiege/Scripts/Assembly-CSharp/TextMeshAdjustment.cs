using UnityEngine;

public class TextMeshAdjustment : MonoBehaviour
{
	private float lastPixelHeight = -1f;

	private TextMesh textMesh;

	private Camera hudCamera;

	private void Start()
	{
		textMesh = GetComponent<TextMesh>();
		Resize();
	}

	private void Update()
	{
		if ((float)Camera.main.pixelHeight != lastPixelHeight || (Application.isEditor && !Application.isPlaying))
		{
			Resize();
		}
	}

	private void Resize()
	{
		float num = Camera.main.pixelHeight;
		float orthographicSize = Camera.main.orthographicSize;
		float num2 = orthographicSize * 2f / num;
		float num3 = 128f;
		textMesh.characterSize = num2 * Camera.main.orthographicSize / Mathf.Max(base.transform.localScale.x, base.transform.localScale.y);
		textMesh.fontSize = Mathf.RoundToInt(num3 / textMesh.characterSize);
		lastPixelHeight = num;
	}
}
