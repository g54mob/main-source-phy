using UnityEngine;
using UnityEngine.UI;

public class UnitEditorRenderer : MonoBehaviour
{
	private Camera renderingCamera;

	public RawImage output;

	private void Awake()
	{
		renderingCamera = GetComponent<Camera>();
		renderingCamera.enabled = true;
	}

	private void Start()
	{
		SetSquare();
	}

	public void SetSquare()
	{
		int height = Screen.height;
		renderingCamera.targetTexture = CreateRT(height, height);
		output.texture = renderingCamera.targetTexture;
		RectTransform component = output.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(component.sizeDelta.y, component.sizeDelta.y);
	}

	public void SetFullscreen()
	{
		int height = Screen.height;
		int width = Screen.width;
		renderingCamera.targetTexture = CreateRT(width, height);
		output.texture = renderingCamera.targetTexture;
		RectTransform component = output.GetComponent<RectTransform>();
		float num = (float)Screen.width / (float)Screen.height;
		component.sizeDelta = new Vector2(component.sizeDelta.y * num, component.sizeDelta.y);
	}

	private RenderTexture CreateRT(int width, int height)
	{
		RenderTexture renderTexture = new RenderTexture(width, height, 24);
		renderTexture.Create();
		return renderTexture;
	}

	public RenderTexture GetRenderTexture()
	{
		return renderingCamera.targetTexture;
	}

	public Camera GetRenderCamera()
	{
		return renderingCamera;
	}
}
