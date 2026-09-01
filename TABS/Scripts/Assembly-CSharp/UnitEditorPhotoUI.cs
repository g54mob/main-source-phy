using Landfall.TABS.UnitEditor;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitEditorPhotoUI : UIComponentMainMenu
{
	public CodeAnimation buttonAnimation;

	public RectTransform frameRect;

	public Image[] ImagesToFade;

	private float[] imagesTargetAlpha;

	private bool isVisable;

	private CanvasScaler canvasScaler;

	private UnitEditorManager unitEditorManager;

	protected override void Awake()
	{
		base.Awake();
		canvasScaler = GetComponentInParent<CanvasScaler>();
	}

	public void Setup(UnitEditorManager manager)
	{
		unitEditorManager = manager;
		base.gameObject.SetActive(value: true);
		imagesTargetAlpha = new float[ImagesToFade.Length];
		for (int i = 0; i < imagesTargetAlpha.Length; i++)
		{
			imagesTargetAlpha[i] = ImagesToFade[i].color.a;
			Color color = ImagesToFade[i].color;
			color.a = 0f;
			ImagesToFade[i].color = color;
		}
		buttonAnimation.PlayOut();
	}

	public Texture2D TakePhoto()
	{
		UnitEditorRenderer unitEditorRenderer = Object.FindObjectOfType<UnitEditorRenderer>();
		RenderTexture renderTexture = unitEditorRenderer.GetRenderTexture();
		Camera renderCamera = unitEditorRenderer.GetRenderCamera();
		int num = (int)((float)renderTexture.height * 0.48f);
		Vector3 vector = renderCamera.ViewportToScreenPoint(Vector3.one * 0.5f);
		Vector3 vector2 = vector + new Vector3(num, num, 0f) * 0.5f;
		Vector3 vector3 = vector - new Vector3(num, num, 0f) * 0.5f;
		int num2 = (int)(vector2.y - vector3.y);
		int num3 = (int)(vector2.x - vector3.x);
		Debug.Log("height: " + num2);
		Debug.Log("width: " + num3);
		Texture2D texture2D = new Texture2D(num3, num2);
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(new Vector2(vector3.x, vector3.y), new Vector2(num3, num2)), 0, 0);
		texture2D.Apply();
		RenderTexture.active = active;
		return Resize(texture2D, 512, 512);
	}

	private Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
	{
		RenderTexture renderTexture = (RenderTexture.active = new RenderTexture(targetX, targetY, 24));
		Graphics.Blit(texture2D, renderTexture);
		Texture2D texture2D2 = new Texture2D(targetX, targetY);
		texture2D2.ReadPixels(new Rect(0f, 0f, targetX, targetY), 0, 0);
		texture2D2.Apply();
		renderTexture.Release();
		return texture2D2;
	}

	public void AnimateIn()
	{
		buttonAnimation.PlayIn();
		isVisable = true;
	}

	public void AnimateOut()
	{
		buttonAnimation.PlayOut();
		isVisable = false;
	}

	protected override void Update()
	{
		base.Update();
		if (!base.IsActive)
		{
			return;
		}
		if (PlayerActions.Instance.m_accept.WasPressed)
		{
			EventSystem current = EventSystem.current;
			if (current != null && current.currentSelectedGameObject == null)
			{
				if (!SelectDefault())
				{
					unitEditorManager.TakePhoto();
				}
				else
				{
					ExecuteEvents.Execute(current.currentSelectedGameObject, new BaseEventData(current), ExecuteEvents.submitHandler);
				}
			}
		}
		for (int i = 0; i < imagesTargetAlpha.Length; i++)
		{
			Color color = ImagesToFade[i].color;
			float b = 0f;
			if (isVisable)
			{
				b = imagesTargetAlpha[i];
			}
			color.a = Mathf.Lerp(color.a, b, Time.deltaTime * 20f);
			ImagesToFade[i].color = color;
		}
	}
}
