using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour
{
	public class EndCondition
	{
		public static readonly EndCondition EMPTY = new EndCondition("", "", showSpinner: true, 1f);

		public bool done;

		public string hint;

		public string title;

		public bool showSpinner;

		public float fadeSpeedModifier;

		public EndCondition(string hint, string title, bool showSpinner, float fadeSpeedModifier)
		{
			this.hint = hint;
			this.title = title;
			this.showSpinner = showSpinner;
			this.fadeSpeedModifier = fadeSpeedModifier;
		}
	}

	private static LoadingCanvas instance;

	public Material outlineCombineMaterial;

	public StudioEventEmitter soundEmitter;

	public Text levelName;

	public Text loadingHint;

	public RawImage levelImage;

	public Image darkerOverlay;

	public LoadingSpinner spinner;

	private bool useDissolveEffect;

	private Queue<EndCondition> endConditions = new Queue<EndCondition>();

	private CanvasGroup canvasGroup;

	private float alphaCurrent;

	private const int initialAlphaSpeed = 2;

	private int alphaSpeed = 2;

	[NonSerialized]
	public bool vrForceShowCanvas;

	private void init()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		alphaCurrent = canvasGroup.alpha;
		levelImage.material = new Material(levelImage.material);
	}

	private void Update()
	{
		bool flag = VR.instance != null && VR.instance.isActive();
		bool flag2 = endConditions.Count > 0 || (flag && !isTransparent());
		canvasGroup.blocksRaycasts = flag2;
		canvasGroup.interactable = flag2;
		if (endConditions.Count > 0)
		{
			EndCondition endCondition = endConditions.Peek();
			alphaCurrent = Mathf.MoveTowards(alphaCurrent, 1f, Time.deltaTime * (float)alphaSpeed * endCondition.fadeSpeedModifier);
			loadingHint.text = endCondition.hint;
			levelName.text = endCondition.title;
			spinner.gameObject.SetActive(endCondition.showSpinner);
			if (endCondition.done)
			{
				Debug.Log("Condition \"" + endCondition.hint + "\" is done, removing it from the queue.");
				endConditions.Dequeue();
			}
			if (isOpaque() && !soundEmitter.EventReference.IsNull && !soundEmitter.IsPlaying())
			{
				soundEmitter.Play();
			}
			levelImage.material.SetFloat("_FadeAmount", -0.1f);
			float value = getAnimationTime(0.2f);
			Image image = darkerOverlay;
			Color color = darkerOverlay.color;
			float? a = value;
			image.color = color.With(null, null, null, a);
		}
		else
		{
			bool flag3 = true;
			if (useDissolveEffect)
			{
				float num = levelImage.material.GetFloat("_FadeAmount");
				num += Time.deltaTime * 0.5f;
				flag3 = num >= 0.3f;
				if (num >= 0.99f)
				{
					Debug.Log("Ending dissolve effect");
					useDissolveEffect = false;
					num = 1f;
					alphaSpeed = 10;
				}
				levelImage.material.SetFloat("_FadeAmount", num);
			}
			if (flag3)
			{
				alphaCurrent = Mathf.MoveTowards(alphaCurrent, 0f, Time.deltaTime * (float)alphaSpeed);
				if (alphaCurrent <= 0.01f)
				{
					if (levelImage.texture != null)
					{
						UnityEngine.Object.Destroy(levelImage.texture);
						levelImage.texture = null;
					}
					alphaSpeed = 2;
					spinner.reset();
					soundEmitter.Stop();
				}
			}
		}
		canvasGroup.alpha = UnityUtils.smootherStep(0f, 1f, alphaCurrent);
		if (flag && !vrForceShowCanvas && SceneManager.GetActiveScene().name != "Empty")
		{
			canvasGroup.alpha = 0f;
		}
		static float getAnimationTime(float maxValue = 1f)
		{
			float time = Time.time;
			return Mathf.Clamp01(((1f + Mathf.Sin(time * 3f)) * 0.5f + (1f + Mathf.Cos(time * 3.66f)) * 0.25f) * 0.5f / 0.75f) * maxValue;
		}
	}

	private void createBluredImage(Texture2D image, out Texture2D bluredTexture, out Rect uvRect)
	{
		RenderTexture renderTexture = new RenderTexture(image.width / 2, image.height / 2, 0, RenderTextureFormat.Default);
		RenderTexture renderTexture2 = new RenderTexture(image.width / 2, image.height / 2, 0, RenderTextureFormat.Default);
		renderTexture.wrapMode = TextureWrapMode.Clamp;
		renderTexture2.wrapMode = TextureWrapMode.Clamp;
		Shader.SetGlobalVector("_BlurDirection", new Vector4(1f, 0f));
		Graphics.Blit(image, renderTexture, outlineCombineMaterial, 0);
		Shader.SetGlobalVector("_BlurDirection", new Vector4(0f, 1f));
		Graphics.Blit(renderTexture, renderTexture2, outlineCombineMaterial, 0);
		renderTexture.Release();
		RenderTexture renderTexture3 = new RenderTexture(image.width / 4, image.height / 4, 0, RenderTextureFormat.Default);
		RenderTexture renderTexture4 = new RenderTexture(image.width / 4, image.height / 4, 0, RenderTextureFormat.Default);
		renderTexture3.wrapMode = TextureWrapMode.Clamp;
		renderTexture4.wrapMode = TextureWrapMode.Clamp;
		Shader.SetGlobalVector("_BlurDirection", new Vector4(1f, 0f));
		Graphics.Blit(renderTexture2, renderTexture3, outlineCombineMaterial, 0);
		Shader.SetGlobalVector("_BlurDirection", new Vector4(0f, 1f));
		Graphics.Blit(renderTexture3, renderTexture4, outlineCombineMaterial, 0);
		renderTexture2.Release();
		renderTexture3.Release();
		Texture2D texture2D = new Texture2D(image.width / 4, image.height / 4, TextureFormat.RGB24, mipChain: false);
		RenderTexture.active = renderTexture4;
		texture2D.ReadPixels(new Rect(0f, 0f, image.width / 4, image.height / 4), 0, 0);
		texture2D.Apply();
		RenderTexture.active = null;
		renderTexture4.Release();
		float num = (float)texture2D.width / (float)Screen.width;
		float num2 = (float)texture2D.height / (float)Screen.height;
		float num3 = Mathf.Min(num, num2);
		float num4 = num3 / num;
		float num5 = num3 / num2;
		float x = (1f - num4) * 0.5f;
		float y = (1f - num5) * 0.5f;
		UnityEngine.Object.Destroy(renderTexture);
		UnityEngine.Object.Destroy(renderTexture2);
		UnityEngine.Object.Destroy(renderTexture3);
		UnityEngine.Object.Destroy(renderTexture4);
		bluredTexture = texture2D;
		uvRect = new Rect(x, y, num4, num5);
	}

	public void clearAllConditions()
	{
		foreach (EndCondition endCondition in endConditions)
		{
			endCondition.done = true;
		}
	}

	public EndCondition enqueueEndCondition(string hint, string title, bool showSpinner = true, float fadeSpeedModifier = 1f, string metaAssetName = null)
	{
		Debug.Log("Adding new condition for loading canvas (hint: \"" + hint + "\", title: \"" + title + "\").");
		EndCondition endCondition = new EndCondition(hint, title, showSpinner, fadeSpeedModifier);
		if (metaAssetName != null)
		{
			RoomMetaAssets asset = AssetBundleLoader.getAsset<RoomMetaAssets>(AssetBundleType.RoomMetaAssets, "Assets/_RoomMetaAssets/" + metaAssetName + ".asset");
			if (asset != null)
			{
				soundEmitter.EventReference = asset.loadingAudioPath;
			}
			else
			{
				soundEmitter.EventReference = default(EventReference);
			}
		}
		endConditions.Enqueue(endCondition);
		return endCondition;
	}

	public EndCondition getTopEndCondition()
	{
		if (endConditions.TryPeek(out var result))
		{
			return result;
		}
		return EndCondition.EMPTY;
	}

	public void useDissolveEffectNextTime()
	{
		useDissolveEffect = true;
	}

	public void stopDissolveEffectNextTime()
	{
		useDissolveEffect = false;
	}

	public void setOpaque()
	{
		alphaCurrent = 1f;
		canvasGroup.alpha = alphaCurrent;
	}

	public void changeImage(Texture2D newTexture)
	{
		if (levelImage.texture != null)
		{
			UnityEngine.Object.Destroy(levelImage.texture);
			levelImage.texture = null;
			levelImage.color = Color.white;
		}
		if (newTexture == null)
		{
			Color fade = Menu.getTheme().otherColors.fade;
			fade.a = 1f;
			levelImage.color = fade;
		}
		else
		{
			createBluredImage(newTexture, out var bluredTexture, out var uvRect);
			levelImage.texture = bluredTexture;
			levelImage.uvRect = uvRect;
			levelImage.color = Color.white;
		}
	}

	public bool isActive()
	{
		return canvasGroup.interactable;
	}

	public bool isOpaque()
	{
		return alphaCurrent >= 0.99f;
	}

	public bool isTransparent()
	{
		return alphaCurrent <= 0.01f;
	}

	public void changeHint(EndCondition condition, string newHint)
	{
		condition.hint = newHint;
	}

	public string getCurrentHint()
	{
		return endConditions.Peek().hint;
	}

	public bool hasEndConditions()
	{
		return endConditions.Count > 0;
	}

	public static LoadingCanvas get()
	{
		if (instance == null)
		{
			instance = UnityEngine.Object.Instantiate(AssetBundleLoader.getAsset<GameObject>(AssetBundleType.LoadingCanvas, "Assets/_UI/LoadingCanvas.prefab").GetComponent<LoadingCanvas>());
			instance.init();
			UnityEngine.Object.DontDestroyOnLoad(instance);
		}
		return instance;
	}
}
