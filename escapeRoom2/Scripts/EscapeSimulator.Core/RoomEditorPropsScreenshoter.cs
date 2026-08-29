using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomEditorPropsScreenshoter : MonoBehaviour
{
	[Serializable]
	public class AxisButtons
	{
		public Button angle0;

		public Button angle90;

		public Button angle180;

		public Button angle270;
	}

	public const string ICON_EXTENSION = ".png";

	public const int ICON_WIDTH = 512;

	public const int ICON_HEIGHT = 512;

	public EditorAssetsMeta assets;

	public Camera camera;

	public RawImage cameraViewport;

	public RectTransform buttonsScrollView;

	public RectTransform buttonsContainer;

	public GameObject buttonPrefab;

	public InputField filter;

	public InputField propDisplayNameField;

	public Button prev;

	public Button next;

	public RawImage currentIconImage;

	public RawImage previewIconImage;

	public Button updateIconButton;

	public Button resetRotationButton;

	[Header("Rendering")]
	[ReadOnly]
	public RenderTexture cameraBlackRenderTexture;

	[ReadOnly]
	public RenderTexture cameraWhiteRenderTexture;

	public Material combineMaterial;

	public Image blackWhiteBackground;

	private bool refresh;

	private GameObject propGO;

	private Quaternion propDefaultRotation;

	private Button[] buttons;

	private List<Prop> props;

	private int selectedPropIndex = -1;

	private int newSelectedPropIndex = -1;

	private Texture2D previewIconTexture;

	private Vector3 defaultCameraPosition;

	private float cameraChangeTime = -1f;

	public AxisButtons xAxisButtons;

	public AxisButtons yAxisButtons;

	public AxisButtons zAxisButtons;

	private Color defaultAxisButtonColor;

	private Color selectedAxisButtonColor = Color.gray;

	private void Awake()
	{
		initCamera();
		initPropButtons();
		initPropNavigation();
		initIconPreviewSection();
		switchColorBufferFormat(supportsTransparency: true);
	}

	private void initCamera()
	{
		defaultCameraPosition = camera.transform.position;
		blackWhiteBackground.gameObject.SetActive(value: true);
		cameraBlackRenderTexture = new RenderTexture(512, 512, 24)
		{
			name = "Black RT"
		};
		cameraWhiteRenderTexture = new RenderTexture(512, 512, 24)
		{
			name = "White RT"
		};
		combineMaterial = new Material(combineMaterial)
		{
			name = "Black White Material"
		};
		combineMaterial.SetTexture("_MainTex", cameraBlackRenderTexture);
		combineMaterial.SetTexture("_WhiteTex", cameraWhiteRenderTexture);
		cameraViewport.material = combineMaterial;
		cameraViewport.texture = cameraBlackRenderTexture;
		camera.aspect = 1f;
		camera.enabled = false;
	}

	private void initPropButtons()
	{
		buttons = new Button[assets.props.Count];
		props = assets.props;
		for (int i = 0; i < assets.props.Count; i++)
		{
			GameObject obj = UnityEngine.Object.Instantiate(buttonPrefab, buttonPrefab.transform.parent);
			obj.SetActive(value: true);
			Button component = obj.GetComponent<Button>();
			string value = assets.props[i].ID.value;
			component.GetComponentInChildren<Text>().text = value;
			component.name = value.ToLower();
			int index = i;
			component.onClick.AddListener(delegate
			{
				newSelectedPropIndex = index;
			});
			buttons[i] = component;
		}
	}

	private void initPropNavigation()
	{
		prev.onClick.AddListener(selectPrevProp);
		next.onClick.AddListener(selectNextProp);
		filter.onValueChanged.AddListener(delegate(string value)
		{
			string value2 = value.ToLower();
			for (int i = 0; i < buttons.Length; i++)
			{
				Button obj = buttons[i];
				bool flag = obj.name.Contains(value2);
				obj.gameObject.SetActive(flag);
				if (selectedPropIndex == i && !flag)
				{
					newSelectedPropIndex = -1;
				}
			}
			if (newSelectedPropIndex == -1)
			{
				for (int j = 0; j < buttons.Length; j++)
				{
					if (buttons[j].gameObject.activeSelf)
					{
						newSelectedPropIndex = j;
						break;
					}
				}
			}
			refresh = true;
		});
		propDisplayNameField.onSubmit.AddListener(renameProp);
		buttons[0].onClick.Invoke();
	}

	private void initIconPreviewSection()
	{
		updateIconButton.onClick.AddListener(updateIcon);
		resetRotationButton.onClick.AddListener(resetPropRotation);
		registerAxisButtons(xAxisButtons, "x");
		registerAxisButtons(yAxisButtons, "y");
		registerAxisButtons(zAxisButtons, "z");
		defaultAxisButtonColor = xAxisButtons.angle0.GetComponent<Image>().color;
	}

	private void registerAxisButtons(AxisButtons axisButtons, string axis)
	{
		axisButtons.angle0.onClick.AddListener(delegate
		{
			handleAxisButtonPress(0f);
		});
		axisButtons.angle90.onClick.AddListener(delegate
		{
			handleAxisButtonPress(90f);
		});
		axisButtons.angle180.onClick.AddListener(delegate
		{
			handleAxisButtonPress(180f);
		});
		axisButtons.angle270.onClick.AddListener(delegate
		{
			handleAxisButtonPress(270f);
		});
		void handleAxisButtonPress(float degrees)
		{
			if (!(propGO == null))
			{
				Vector3 eulerAngles = propGO.transform.eulerAngles;
				if (axis == "x")
				{
					eulerAngles.x = degrees;
				}
				else if (axis == "y")
				{
					eulerAngles.y = degrees;
				}
				else if (axis == "z")
				{
					eulerAngles.z = degrees;
				}
				changePropRotation(Quaternion.Euler(eulerAngles));
				setAxisButtonColors(axisButtons, degrees);
			}
		}
	}

	private void setAxisButtonColors(AxisButtons axisButtons, float degrees)
	{
		axisButtons.angle0.GetComponent<Image>().color = ((degrees == 0f) ? selectedAxisButtonColor : defaultAxisButtonColor);
		axisButtons.angle90.GetComponent<Image>().color = ((degrees == 90f) ? selectedAxisButtonColor : defaultAxisButtonColor);
		axisButtons.angle180.GetComponent<Image>().color = ((degrees == 180f) ? selectedAxisButtonColor : defaultAxisButtonColor);
		axisButtons.angle270.GetComponent<Image>().color = ((degrees == 270f) ? selectedAxisButtonColor : defaultAxisButtonColor);
	}

	private void selectPrevProp()
	{
		int num = selectedPropIndex;
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			if (buttons[num2].gameObject.activeSelf)
			{
				num = num2;
				break;
			}
		}
		newSelectedPropIndex = num;
	}

	private void selectNextProp()
	{
		int num = selectedPropIndex;
		for (int i = num + 1; i < buttons.Length; i++)
		{
			if (buttons[i].gameObject.activeSelf)
			{
				num = i;
				break;
			}
		}
		newSelectedPropIndex = num;
	}

	private void renameProp(string displayName)
	{
	}

	private void updateIcon()
	{
		if (selectedPropIndex != -1)
		{
			saveIconAsset(previewIconTexture, props[selectedPropIndex].ID.getIconFileName());
			Debug.Log($"Icon for prop <b>{props[selectedPropIndex].ID}</b> successfully updated!");
		}
	}

	private void resetPropRotation()
	{
		changePropRotation(propDefaultRotation);
		resetAxisButtons();
	}

	private void resetAxisButtons()
	{
		setAxisButtonColors(xAxisButtons, -1f);
		setAxisButtonColors(yAxisButtons, -1f);
		setAxisButtonColors(zAxisButtons, -1f);
	}

	private void changePropRotation(Quaternion rotation)
	{
		if (!(propGO == null))
		{
			propGO.transform.rotation = rotation;
			centerPropsBasedOnBounds(propGO);
			renderPreviewIcon();
		}
	}

	private void renderPreviewIcon()
	{
		performCameraRender();
		RenderTexture temporary = RenderTexture.GetTemporary(512, 512, 24);
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = temporary;
		Graphics.Blit(Texture2D.blackTexture, temporary);
		Graphics.Blit(cameraViewport.texture, temporary, cameraViewport.material);
		previewIconTexture = new Texture2D(512, 512);
		previewIconTexture.ReadPixels(new Rect(0f, 0f, 512f, 512f), 0, 0);
		previewIconTexture.Apply();
		previewIconTexture = getTrimmedTexture(previewIconTexture, 512, 512);
		previewIconImage.texture = previewIconTexture;
		RenderTexture.active = active;
		RenderTexture.ReleaseTemporary(temporary);
	}

	private void Update()
	{
		updateUserInput();
		performCameraRender();
	}

	private void performCameraRender()
	{
		camera.targetTexture = cameraBlackRenderTexture;
		blackWhiteBackground.material.color = Color.black;
		camera.Render();
		camera.targetTexture = cameraWhiteRenderTexture;
		blackWhiteBackground.material.color = Color.white;
		camera.Render();
	}

	private void updateUserInput()
	{
		if (filter.isFocused || propDisplayNameField.isFocused)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			selectPrevProp();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			selectNextProp();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			UnityUtils.printUIUnderMouse();
		}
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if (Input.mouseScrollDelta.y < 0f)
			{
				camera.orthographicSize *= 1.05f;
				cameraChangeTime = Time.time;
			}
			else if (Input.mouseScrollDelta.y > 0f)
			{
				camera.orthographicSize /= 1.05f;
				cameraChangeTime = Time.time;
			}
			camera.orthographicSize = Mathf.Max(0.01f, camera.orthographicSize);
		}
		Vector2 zero = Vector2.zero;
		if (Input.GetKey(KeyCode.W))
		{
			zero.y += 1f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			zero.x -= 1f;
		}
		if (Input.GetKey(KeyCode.S))
		{
			zero.y -= 1f;
		}
		if (Input.GetKey(KeyCode.D))
		{
			zero.x += 1f;
		}
		zero *= 0.1f;
		if (Input.GetMouseButton(2))
		{
			float axis = Input.GetAxis("Mouse X");
			float axis2 = Input.GetAxis("Mouse Y");
			zero.x += (0f - axis) * 2f;
			zero.y += (0f - axis2) * 2f;
		}
		if (zero != Vector2.zero)
		{
			camera.transform.position += (camera.transform.right * zero.x + camera.transform.up * zero.y) * Time.deltaTime;
			cameraChangeTime = Time.time;
		}
		if (cameraChangeTime != -1f && Time.time - cameraChangeTime >= 0.1f)
		{
			renderPreviewIcon();
			cameraChangeTime = -1f;
		}
	}

	public static void saveIconAsset(Texture2D iconTexture, string iconName)
	{
	}

	public static Texture2D getTrimmedTexture(Texture2D tex, int w, int h)
	{
		Color[] pixels = tex.GetPixels();
		Vector2 vector = new Vector2(w - 1, h - 1);
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < w; i++)
		{
			for (int j = 0; j < h; j++)
			{
				if (pixels[i * w + j].a > 0f)
				{
					if ((float)j < vector.x)
					{
						vector.x = j;
					}
					if ((float)j > zero.x)
					{
						zero.x = j;
					}
					if ((float)i < vector.y)
					{
						vector.y = i;
					}
					if ((float)i > zero.y)
					{
						zero.y = i;
					}
				}
			}
		}
		int num = (int)(zero.x - vector.x);
		int num2 = (int)(zero.y - vector.y);
		if (num <= 0 && num2 <= 0)
		{
			return tex;
		}
		Color[] pixels2 = tex.GetPixels((int)vector.x, (int)vector.y, num, num2);
		int num3 = Mathf.Max(num, num2);
		Texture2D texture2D = new Texture2D(num3, num3);
		Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
		Color32[] pixels3 = texture2D.GetPixels32();
		for (int k = 0; k < pixels3.Length; k++)
		{
			pixels3[k] = color;
		}
		texture2D.SetPixels32(pixels3);
		int x = (num3 - num) / 2;
		int y = (num3 - num2) / 2;
		texture2D.SetPixels(x, y, num, num2, pixels2, 0);
		texture2D.Apply();
		return texture2D;
	}

	public static void preparePropForScreenshot(GameObject propGO)
	{
		int layer = LayerMask.NameToLayer("EditorPropScreenshot");
		int roomEditorLayer = LayerMask.NameToLayer("RoomEditorSpecialObject");
		applyLayer(propGO.transform);
		propGO.transform.position = Vector3.zero;
		propGO.transform.localScale = Vector3.one;
		if (propGO.TryGetComponent<Item>(out var component))
		{
			propGO.transform.rotation = Quaternion.Euler(component.examineBaseRotation);
		}
		if (propGO.TryGetComponent<Polygon>(out var component2))
		{
			component2.recalculate();
		}
		centerPropsBasedOnBounds(propGO);
		void applyLayer(Transform t)
		{
			if (t.gameObject.layer != roomEditorLayer)
			{
				t.gameObject.layer = layer;
			}
			for (int i = 0; i < t.childCount; i++)
			{
				applyLayer(t.GetChild(i));
			}
		}
	}

	private static void centerPropsBasedOnBounds(GameObject propGO)
	{
		Bounds bounds = Game.computeLocalBounds(propGO, overrideAcceptEditorSpecial: true);
		if (bounds.extents.magnitude > 0f)
		{
			float num = Mathf.Max(Mathf.Max(bounds.size.x, bounds.size.y), bounds.size.z);
			propGO.transform.localScale = Vector3.one * (1f / num);
		}
		Vector3 vector = Vector3.Scale(propGO.transform.localScale, propGO.transform.localRotation * bounds.center);
		propGO.transform.localPosition = -vector;
	}

	private void OnDestroy()
	{
		switchColorBufferFormat(supportsTransparency: false);
		destroyRT(cameraBlackRenderTexture);
		destroyRT(cameraWhiteRenderTexture);
		static void destroyRT(RenderTexture renderTexture)
		{
			if (!(renderTexture == null))
			{
				renderTexture.Release();
				UnityEngine.Object.Destroy(renderTexture);
			}
		}
	}

	private void switchColorBufferFormat(bool supportsTransparency)
	{
	}

	private float calculateOrthographicSizeSoThatPropFitsInCameraView(bool isEffect)
	{
		if (propGO == null)
		{
			return 1f;
		}
		Bounds propBoundsInCameraSpace = getPropBoundsInCameraSpace(isEffect);
		float a = propBoundsInCameraSpace.size.x * 0.5f;
		float b = propBoundsInCameraSpace.size.y * 0.5f;
		return Mathf.Max(a, b) * 1.05f;
	}

	private Bounds getPropBoundsInCameraSpace(bool isEffect)
	{
		Bounds bounds = Game.computeLocalBounds(propGO, overrideAcceptEditorSpecial: true);
		if (isEffect && propGO.TryGetComponentInChildren<BoxCollider>(out var component))
		{
			bounds.center = component.center;
			bounds.size = component.size;
		}
		Vector3 center = bounds.center;
		Vector3 size = bounds.size;
		Vector3[] array = new Vector3[8]
		{
			center + new Vector3(0f - size.x, 0f - size.y, 0f - size.z) * 0.5f,
			center + new Vector3(size.x, 0f - size.y, 0f - size.z) * 0.5f,
			center + new Vector3(0f - size.x, size.y, 0f - size.z) * 0.5f,
			center + new Vector3(size.x, size.y, 0f - size.z) * 0.5f,
			center + new Vector3(0f - size.x, 0f - size.y, size.z) * 0.5f,
			center + new Vector3(size.x, 0f - size.y, size.z) * 0.5f,
			center + new Vector3(0f - size.x, size.y, size.z) * 0.5f,
			center + new Vector3(size.x, size.y, size.z) * 0.5f
		};
		for (int i = 0; i < 8; i++)
		{
			array[i] = propGO.transform.TransformPoint(array[i]);
			array[i] = camera.transform.InverseTransformPoint(array[i]);
		}
		Vector3 vector = array[0];
		Vector3 vector2 = array[0];
		for (int j = 1; j < 8; j++)
		{
			vector = Vector3.Min(vector, array[j]);
			vector2 = Vector3.Max(vector2, array[j]);
		}
		return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
	}
}
