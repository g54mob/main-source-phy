using UnityEngine;
using UnityEngine.UI;

public class TestingPaint : MonoBehaviour
{
	public Shader paintShader;

	public Shader blitCopyShader;

	public Shader growPixelsShader;

	public RawImage image;

	public Renderer testRenderer;

	public Texture2D brushTexture;

	public Texture2D paletteTexture;

	public RawImage palette;

	private PineDrawContext drawContext;

	private RenderTexture rt;

	private RenderTexture preview;

	private RenderTexture growPixels;

	private float brushSize = 100f;

	private Color brushColor = Color.white;

	private Vector3 lastMousePosition;

	private Vector3 lastPaintedPosition;

	private void Start()
	{
		drawContext = new PineDrawContext();
		PineDraw.init(drawContext, paintShader, blitCopyShader, growPixelsShader, Camera.main);
		rt = PineDraw.createPaintTextureFromDimensions(drawContext, null);
		preview = PineDraw.createPaintTextureFromDimensions(drawContext, null);
		growPixels = PineDraw.createPaintTextureFromDimensions(drawContext, null);
		testRenderer.material.SetTexture("_AlbedoOverlay", growPixels);
		testRenderer.material.EnableKeyword("PINE_OVERLAY_ALBEDO");
		image.texture = growPixels;
	}

	private void Update()
	{
		brushSize = Mathf.Clamp(brushSize + Input.mouseScrollDelta.y * 10f, 1f, 500f);
		if (Input.GetKey(KeyCode.LeftShift))
		{
			PineDraw.eraseBrush(drawContext, rt, preview, testRenderer, brushTexture, brushColor, Input.mousePosition, brushSize);
		}
		else
		{
			PineDraw.drawBrush(drawContext, rt, preview, testRenderer, brushTexture, brushColor, Input.mousePosition, brushSize);
		}
		PineDraw.growPixels(drawContext, preview, growPixels, testRenderer);
		if (Input.GetMouseButton(1))
		{
			Vector3 vector = Input.mousePosition - lastMousePosition;
			testRenderer.transform.rotation = Quaternion.Euler(0f, (0f - vector.x) * 0.1f, 0f) * testRenderer.transform.rotation;
			testRenderer.transform.rotation = Quaternion.Euler(vector.y * 0.1f, 0f, 0f) * testRenderer.transform.rotation;
		}
		if (Input.GetMouseButton(0) && (lastPaintedPosition != Input.mousePosition || lastPaintedPosition == Vector3.zero))
		{
			lastPaintedPosition = Input.mousePosition;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				PineDraw.eraseBrush(drawContext, rt, rt, testRenderer, brushTexture, brushColor, Input.mousePosition, brushSize);
			}
			else
			{
				PineDraw.drawBrush(drawContext, rt, rt, testRenderer, brushTexture, brushColor, Input.mousePosition, brushSize);
			}
			if (GetPositionOnImage01(palette, Input.mousePosition, out var ptLocationRelativeToImage) && ptLocationRelativeToImage.x >= 0f && ptLocationRelativeToImage.x <= 1f && ptLocationRelativeToImage.y >= 0f && ptLocationRelativeToImage.y <= 1f)
			{
				int x = (int)(ptLocationRelativeToImage.x * (float)paletteTexture.width);
				int y = (int)(ptLocationRelativeToImage.y * (float)paletteTexture.height);
				brushColor = paletteTexture.GetPixel(x, y);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			lastPaintedPosition = Vector3.zero;
		}
		lastMousePosition = Input.mousePosition;
	}

	private static bool GetPositionOnImage01(RawImage uiImageObject, Vector2 screenPosition, out Vector2 ptLocationRelativeToImage01)
	{
		ptLocationRelativeToImage01 = default(Vector2);
		RectTransform component = uiImageObject.GetComponent<RectTransform>();
		Vector2 localPoint = default(Vector2);
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(component, screenPosition, null, out localPoint))
		{
			Vector2 vector = new Vector2(localPoint.x - component.rect.x, localPoint.y - component.rect.y);
			Vector2 vector2 = default(Vector2);
			vector2.Set(vector.x, vector.y);
			ptLocationRelativeToImage01.Set(vector2.x / component.rect.width, vector2.y / component.rect.height);
			return true;
		}
		return false;
	}
}
