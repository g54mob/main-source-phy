using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Mesh/Dynamic Text")]
[RequireComponent(typeof(MeshRenderer))]
public class DynamicText : MonoBehaviour
{
	public Camera cam;

	public StringBuilder textSB = new StringBuilder();

	public float offsetZ;

	public float size;

	public float lineSpacing = 1.25f;

	public float letterSpacing;

	public DynamicTextAnchor anchor = DynamicTextAnchor.BaselineLeft;

	public TextAlignment alignment;

	public float tabSize = 1f;

	public FontStyle fontStyle;

	public Font font;

	public bool autoSetFontMaterial = true;

	public Color color = Color.white;

	public string baselineRefChar = "x";

	public string metricsRefChars = "xlj";

	public bool pixelSnapTransformPos = true;

	public int minFontPxSize = 6;

	public int maxFontPxSize = 150;

	public bool autoFaceCam;

	[SerializeField]
	[HideInInspector]
	protected string text = "";

	[SerializeField]
	public string serializedText = "Text";

	[HideInInspector]
	protected readonly int assetVersion = 1218;

	[SerializeField]
	[HideInInspector]
	protected int version = 1023;

	protected static string _info_copyrightSrcLicense = "Copyright © 2013-2017 Strobotnik Ltd. Dynamic Text component for Unity. If you want access to source code, contact us for a separate license: www.strobotnik.com";

	[HideInInspector]
	private Mesh mesh;

	[HideInInspector]
	private Transform trn;

	[HideInInspector]
	private float pxBaseline;

	[HideInInspector]
	private float pxAscent;

	[HideInInspector]
	private float pxDescent;

	[HideInInspector]
	private int pxFontSize;

	[HideInInspector]
	private int prevPxFontSize;

	[HideInInspector]
	private float pxUnitScale;

	[HideInInspector]
	private Vector3 unsnappedPos;

	[HideInInspector]
	private int refScreenWidth;

	[HideInInspector]
	private int refScreenHeight;

	[HideInInspector]
	private float prevOffsetZ;

	[HideInInspector]
	private float prevSize;

	[HideInInspector]
	private float prevLineSpacing;

	[HideInInspector]
	private float prevLetterSpacing;

	[HideInInspector]
	private DynamicTextAnchor prevAnchor;

	[HideInInspector]
	private TextAlignment prevAlignment;

	[HideInInspector]
	private float prevTabSize;

	[HideInInspector]
	private FontStyle prevFontStyle;

	[HideInInspector]
	private Font prevFont;

	[HideInInspector]
	private bool prevAutoSetFontMaterial;

	[HideInInspector]
	private Color prevColor;

	[HideInInspector]
	private char prevBaselineRefChar;

	[HideInInspector]
	private string prevMetricsRefChars = "";

	[HideInInspector]
	private bool prevPixelSnapTransformPos;

	[HideInInspector]
	private Vector3 prevSnappedPos;

	[HideInInspector]
	private Camera prevCam;

	[HideInInspector]
	private Transform camTrn;

	[HideInInspector]
	private Vector3 prevCamPos;

	[HideInInspector]
	private Quaternion prevCamRot;

	[HideInInspector]
	private int prevTextSBLength = -1;

	[HideInInspector]
	private bool pendingInitializeFromReset;

	[HideInInspector]
	private bool pendingGenerateMesh;

	[HideInInspector]
	private bool sbIsValid;

	[HideInInspector]
	private List<char> textCharacters = new List<char>();

	[HideInInspector]
	private StringBuilder textCharactersSB = new StringBuilder();

	[HideInInspector]
	private string textCharactersString = "";

	[HideInInspector]
	private bool useD3D9Sampling;

	[HideInInspector]
	private bool editorInspectorTextUpdatePending;

	[HideInInspector]
	private Vector3[] vertices;

	[HideInInspector]
	private Vector2[] uv;

	[HideInInspector]
	private Color32[] colors32;

	[HideInInspector]
	private int[] tris;

	[HideInInspector]
	private bool isDefaultFont;

	[HideInInspector]
	private int rebuildCallbacksBeforeUpdate;

	[HideInInspector]
	private float rebuildCallbackLoopFallback_reducedFontPxSize = 1f;

	[HideInInspector]
	private bool initialized;

	public bool suppressDebugLogs;

	private static bool errorShown_noCamCantGenMesh = false;

	private static bool errorShown_zeroScale = false;

	public Bounds bounds
	{
		get
		{
			if ((bool)mesh)
			{
				return mesh.bounds;
			}
			return default(Bounds);
		}
	}

	public float baseline
	{
		get
		{
			if (pxUnitScale == 0f)
			{
				return 0f;
			}
			return pxBaseline / pxUnitScale;
		}
	}

	public float ascent
	{
		get
		{
			if (pxUnitScale == 0f)
			{
				return 0f;
			}
			return pxAscent / pxUnitScale;
		}
	}

	public float descent
	{
		get
		{
			if (pxUnitScale == 0f)
			{
				return 0f;
			}
			return pxDescent / pxUnitScale;
		}
	}

	public void FinishedTextSB()
	{
		if (!initialized)
		{
			initialize();
		}
		sbIsValid = true;
		if (Application.isEditor)
		{
			if (!Application.isPlaying)
			{
				serializedText = textSB.ToString();
			}
			else if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				serializedText = textSB.ToString();
			}
			else
			{
				editorInspectorTextUpdatePending = true;
			}
		}
		else if (!base.enabled || !base.gameObject.activeInHierarchy)
		{
			serializedText = textSB.ToString();
		}
		textCharacters.Clear();
		for (int i = 0; i < metricsRefChars.Length; i++)
		{
			char item = metricsRefChars[i];
			if (!textCharacters.Contains(item))
			{
				textCharacters.Add(item);
			}
		}
		int length = textSB.Length;
		for (int j = 0; j < length; j++)
		{
			char item2 = textSB[j];
			if (!textCharacters.Contains(item2))
			{
				textCharacters.Add(item2);
			}
		}
		textCharactersSB.EnsureCapacity(textCharacters.Count);
		int count = textCharacters.Count;
		textCharactersSB.Length = count;
		bool flag = count != textCharactersString.Length;
		for (int k = 0; k < count; k++)
		{
			char c = textCharacters[k];
			textCharactersSB[k] = c;
			if (!flag && textCharactersString[k] != c)
			{
				flag = true;
			}
		}
		if (flag)
		{
			textCharactersString = textCharactersSB.ToString();
		}
		GenerateMesh();
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public int internal_GetVersion()
	{
		return version;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public string internal_GetDeprecatedText()
	{
		return text;
	}

	public string GetText()
	{
		if (!initialized)
		{
			initialize();
		}
		if (sbIsValid)
		{
			string text = textSB.ToString();
			if (Application.isEditor && (serializedText == null || !serializedText.Equals(text)))
			{
				serializedText = text;
			}
			return text;
		}
		return serializedText;
	}

	public void SetText(string newText)
	{
		if (!initialized)
		{
			initialize();
		}
		if (newText == null)
		{
			newText = "";
		}
		textSB.EnsureCapacity(newText.Length);
		textSB.Length = 0;
		textSB.Append(newText);
		FinishedTextSB();
	}

	private void doVersionUpgrade()
	{
		if (version < assetVersion || (text != null && text.Length > 0))
		{
			if (text != null && text.Length > 0)
			{
				serializedText = text;
			}
			if (serializedText == null)
			{
				serializedText = "";
			}
			text = "";
			version = assetVersion;
		}
	}

	public void Awake()
	{
		if (!initialized)
		{
			initialize();
		}
	}

	public void OnDestroy()
	{
		Font.textureRebuilt -= textureRebuildCallback_U5;
		MeshFilter component = GetComponent<MeshFilter>();
		if ((bool)component)
		{
			component.mesh = null;
		}
		UnityEngine.Object.DestroyImmediate(mesh);
		mesh = null;
		initialized = false;
	}

	public void Start()
	{
		if (!initialized)
		{
			initialize();
		}
	}

	public void Reset()
	{
		resetMeshAndTextureRebuildCallbacks();
		pendingInitializeFromReset = true;
		initialized = false;
	}

	private void resetMeshAndTextureRebuildCallbacks()
	{
		Font.textureRebuilt -= textureRebuildCallback_U5;
		if ((bool)prevFont)
		{
			Font.textureRebuilt -= textureRebuildCallback_U5;
		}
		prevFont = null;
		if ((bool)mesh)
		{
			mesh.Clear();
			pendingGenerateMesh = true;
		}
	}

	private bool initialize()
	{
		if (initialized)
		{
			return true;
		}
		doVersionUpgrade();
		if (Screen.width == 0 || Screen.height == 0)
		{
			return false;
		}
		initialized = true;
		if (base.transform == null && !suppressDebugLogs)
		{
			Debug.LogWarning("transform null", this);
		}
		trn = base.transform;
		textCharacters.Clear();
		if (textCharacters.Capacity < metricsRefChars.Length)
		{
			textCharacters.Capacity = metricsRefChars.Length;
		}
		if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 9"))
		{
			string[] array = Application.unityVersion.Split('.');
			bool flag = true;
			if (array[0].Equals("4") || (array[0].Equals("5") && array[1][0] < '5'))
			{
				flag = false;
			}
			if (!flag)
			{
				useD3D9Sampling = true;
			}
		}
		if (font == null)
		{
			TextMesh component = GetComponent<TextMesh>();
			if (!component)
			{
				if (!suppressDebugLogs)
				{
					Debug.LogWarning("Font is null, replacing with default", this);
				}
				font = Resources.GetBuiltinResource<Font>("Arial.ttf");
				isDefaultFont = true;
			}
			else
			{
				cam = Camera.main;
				serializedText = component.text;
				textSB.Length = 0;
				textSB.Append(component.text);
				offsetZ = component.offsetZ;
				size = component.characterSize * (float)((component.fontSize == 0) ? 16 : component.fontSize) / 10f;
				lineSpacing = 1.25f * component.lineSpacing;
				anchor = (DynamicTextAnchor)component.anchor;
				alignment = component.alignment;
				tabSize = component.tabSize / 10f;
				fontStyle = component.fontStyle;
				font = component.font;
				color = component.color;
				if (font.name.Equals("Arial"))
				{
					isDefaultFont = true;
				}
				UnityEngine.Object.DestroyImmediate(component);
			}
		}
		trn = base.transform;
		prevSnappedPos = (unsnappedPos = trn.position);
		if (pixelSnapTransformPos)
		{
			updateUnsnappedPosByDelta();
			snapTransformPosition();
		}
		if (!sbIsValid)
		{
			SetText(serializedText);
		}
		pendingInitializeFromReset = false;
		return true;
	}

	private void updateUnsnappedPosByDelta()
	{
		if (!(trn == null) && prevSnappedPos != trn.position)
		{
			unsnappedPos += trn.position - prevSnappedPos;
		}
	}

	private void snapTransformPosition()
	{
		if (trn == null || cam == null)
		{
			return;
		}
		Vector3 lossyScale = trn.lossyScale;
		if (Mathf.Approximately(lossyScale.x, 0f) || Mathf.Approximately(lossyScale.y, 0f))
		{
			if (!errorShown_zeroScale && Application.isEditor)
			{
				if (!suppressDebugLogs)
				{
					Debug.LogWarning("Object has zero scale - Not snapping (this warning is logged only once even if problem persists)", this);
				}
				errorShown_zeroScale = true;
			}
			return;
		}
		if (prevPixelSnapTransformPos != pixelSnapTransformPos)
		{
			unsnappedPos = trn.position;
		}
		prevPixelSnapTransformPos = pixelSnapTransformPos;
		if (!pixelSnapTransformPos)
		{
			unsnappedPos = trn.position;
			prevSnappedPos = unsnappedPos;
			return;
		}
		Vector3 position = unsnappedPos + new Vector3(0f, 0f, offsetZ);
		Vector3 position2 = cam.WorldToScreenPoint(position);
		position2.x = Mathf.Round(position2.x);
		position2.y = Mathf.Round(position2.y);
		position = cam.ScreenToWorldPoint(position2);
		Vector3 position3 = new Vector3(position.x, position.y, position.z - offsetZ);
		trn.position = position3;
		prevSnappedPos = position3;
	}

	private void updateMetrics()
	{
		font.RequestCharactersInTexture(metricsRefChars, pxFontSize, fontStyle);
		font.RequestCharactersInTexture(baselineRefChar, pxFontSize, fontStyle);
		pxBaseline = size / 2f;
		CharacterInfo info;
		if (font.GetCharacterInfo(baselineRefChar[0], out info, pxFontSize, fontStyle))
		{
			pxBaseline = info.minY;
		}
		else if (!suppressDebugLogs)
		{
			Debug.LogWarning("Can't get baseline ref character info (baselineRefChar:'" + baselineRefChar + "', Font:" + font.name + ")", this);
		}
		bool flag = true;
		pxAscent = float.NegativeInfinity;
		pxDescent = float.PositiveInfinity;
		string text = "";
		int length = metricsRefChars.Length;
		for (int i = 0; i < length; i++)
		{
			if (font.GetCharacterInfo(metricsRefChars[i], out info, pxFontSize, fontStyle))
			{
				pxAscent = Mathf.Max(pxAscent, (float)info.maxY - pxBaseline);
				pxDescent = Mathf.Min(pxDescent, (float)info.minY - pxBaseline);
				flag = false;
			}
			else if (Application.isEditor)
			{
				text += metricsRefChars[i];
			}
		}
		if (Application.isEditor && text.Length > 0 && !suppressDebugLogs)
		{
			Debug.LogWarning("Unavailable metrics ref chars: '" + text + "'", this);
		}
		if (flag)
		{
			pxAscent = size / 2f;
			pxDescent = 0f - pxAscent;
			if (Application.isEditor && !suppressDebugLogs)
			{
				Debug.LogWarning(((length == 0) ? "No metrics ref chars - " : "") + "Using half of given size as fallback ascent & descent", this);
			}
		}
	}

	private void alignRow(ref Vector3[] vertices, float pxRowWidth, int startIndex, int endIndex)
	{
		float num = 0f;
		if (alignment == TextAlignment.Right)
		{
			num = Mathf.Floor(0f - pxRowWidth) / pxUnitScale;
		}
		else
		{
			if (alignment != TextAlignment.Center)
			{
				return;
			}
			num = Mathf.Floor((0f - pxRowWidth) / 2f) / pxUnitScale;
		}
		Vector3 vector = new Vector3(num, 0f, 0f);
		for (int i = startIndex; i < endIndex; i++)
		{
			vertices[i] += vector;
		}
	}

	public void OnEnable()
	{
		if (Application.isEditor && (bool)mesh && (bool)GetComponent<MeshFilter>())
		{
			GetComponent<MeshFilter>().mesh = mesh;
		}
		initialize();
		SetText(serializedText);
		initialize();
		resetMeshAndTextureRebuildCallbacks();
	}

	public void OnDisable()
	{
		serializedText = textSB.ToString();
		Font.textureRebuilt -= textureRebuildCallback_U5;
	}

	private float calculatePxUnitScale()
	{
		if (cam.orthographic)
		{
			return (float)cam.pixelHeight / (2f * cam.orthographicSize);
		}
		float f = (float)Math.PI / 180f * cam.fieldOfView / 2f;
		Vector3 forward = camTrn.forward;
		float num = new Plane(forward, camTrn.position).GetDistanceToPoint(unsnappedPos + new Vector3(0f, 0f, offsetZ)) * Mathf.Tan(f);
		if (num < 1E-06f)
		{
			num = -1f;
		}
		return (float)(cam.pixelHeight / 2) / num;
	}

	private int calculatePxFontSize(ref float unitScale)
	{
		if (size < 1E-05f)
		{
			size = 1E-05f;
		}
		int num = (int)(size * unitScale);
		int num2 = minFontPxSize;
		int num3 = maxFontPxSize;
		if (rebuildCallbackLoopFallback_reducedFontPxSize < 1f)
		{
			if (num2 > 6)
			{
				num2 = 6;
			}
			num3 = (int)((float)maxFontPxSize * rebuildCallbackLoopFallback_reducedFontPxSize);
			if (num3 < num2)
			{
				num3 = num2;
			}
		}
		if (num < num2 || num > num3)
		{
			num = Mathf.Clamp(num, num2, num3);
			unitScale = (float)num / size;
		}
		return num;
	}

	private void textureRebuildCallback_U5(Font changedFont)
	{
		if (this == null)
		{
			return;
		}
		if (!this)
		{
			Debug.LogError("DT.textureRebuildCallback_U5 - !this", this);
		}
		else if (!initialized)
		{
			Debug.LogWarning("Texture rebuild callback when DynamicText is not in initialized state", this);
		}
		else if (font == null)
		{
			Font.textureRebuilt -= textureRebuildCallback_U5;
		}
		else if (!(changedFont != font))
		{
			rebuildCallbacksBeforeUpdate++;
			if (rebuildCallbacksBeforeUpdate >= 3)
			{
				rebuildCallbackLoopFallback_reducedFontPxSize *= 0.75f;
			}
			GenerateMesh();
		}
	}

	[ContextMenu("Regenerate Mesh")]
	private void contextMenu_RegenerateMesh()
	{
		pendingGenerateMesh = true;
		GenerateMesh();
	}

	public void GenerateMesh()
	{
		if (!initialized && !initialize())
		{
			return;
		}
		bool flag = pendingGenerateMesh;
		pendingGenerateMesh = false;
		if (font == null)
		{
			if (!suppressDebugLogs)
			{
				Debug.LogWarning("No font specified - not generating mesh!", this);
			}
			return;
		}
		if (baselineRefChar.Length != 1)
		{
			if (baselineRefChar.Length == 0)
			{
				if (!suppressDebugLogs)
				{
					Debug.LogWarning("No Baseline Reference Char, using 'x'!", this);
				}
				baselineRefChar = "x";
			}
			else
			{
				baselineRefChar = baselineRefChar.Substring(0, 1);
			}
		}
		refScreenWidth = Screen.width;
		refScreenHeight = Screen.height;
		if (cam == null)
		{
			cam = Camera.main;
			if (cam == null)
			{
				if (!errorShown_noCamCantGenMesh)
				{
					if (!suppressDebugLogs)
					{
						Debug.LogError("No camera - can't generate mesh (this error is logged only once even if problem persists)", this);
					}
					errorShown_noCamCantGenMesh = true;
				}
				return;
			}
		}
		if (camTrn == null || prevCam != cam)
		{
			camTrn = cam.transform;
		}
		prevCam = cam;
		prevCamPos = camTrn.position;
		prevCamRot = camTrn.rotation;
		if (Application.isEditor && !Application.isPlaying && serializedText != null && serializedText.Length > 0 && textSB.Length == 0)
		{
			textSB.Append(serializedText);
		}
		updateUnsnappedPosByDelta();
		pxUnitScale = calculatePxUnitScale();
		prevOffsetZ = offsetZ;
		if (size == 0f)
		{
			if (cam.orthographic)
			{
				size = cam.orthographicSize / 5f;
			}
			else
			{
				size = 1f;
			}
		}
		prevSize = size;
		prevLineSpacing = lineSpacing;
		prevLetterSpacing = letterSpacing;
		prevAnchor = anchor;
		prevAlignment = alignment;
		prevTabSize = tabSize;
		prevFontStyle = fontStyle;
		bool flag2 = false;
		bool flag3 = false;
		if (prevFont != font)
		{
			if (prevFont != null)
			{
				Font.textureRebuilt -= textureRebuildCallback_U5;
			}
			if (!GetComponent<MeshRenderer>())
			{
				if (!suppressDebugLogs)
				{
					Debug.LogWarning("No Mesh Renderer component", this);
				}
			}
			else
			{
				flag3 = true;
			}
			prevFont = font;
			if (font != null)
			{
				flag2 = true;
				Font.textureRebuilt -= textureRebuildCallback_U5;
				Font.textureRebuilt += textureRebuildCallback_U5;
			}
		}
		if (flag3 || prevAutoSetFontMaterial != autoSetFontMaterial)
		{
			if (autoSetFontMaterial)
			{
				GetComponent<Renderer>().sharedMaterial = font.material;
			}
			prevAutoSetFontMaterial = autoSetFontMaterial;
		}
		prevColor = this.color;
		if (baselineRefChar == null || baselineRefChar.Length != 1 || prevBaselineRefChar != baselineRefChar[0] || prevMetricsRefChars == null || !prevMetricsRefChars.Equals(metricsRefChars))
		{
			flag2 = true;
		}
		prevBaselineRefChar = baselineRefChar[0];
		prevMetricsRefChars = metricsRefChars;
		prevPxFontSize = pxFontSize;
		pxFontSize = calculatePxFontSize(ref pxUnitScale);
		snapTransformPosition();
		if (prevPxFontSize != pxFontSize)
		{
			flag2 = true;
		}
		if (flag2)
		{
			updateMetrics();
		}
		font.RequestCharactersInTexture(textCharactersString, pxFontSize, fontStyle);
		MeshFilter meshFilter = base.gameObject.GetComponent<MeshFilter>();
		if (!meshFilter)
		{
			meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		if (mesh == null)
		{
			mesh = new Mesh();
			mesh.name = "Dynamic Text Mesh";
			mesh.hideFlags = HideFlags.HideAndDontSave;
			meshFilter.mesh = mesh;
		}
		Color32 color = this.color;
		int length = textSB.Length;
		int num = length * 4;
		bool flag4 = false;
		if (prevTextSBLength == length && vertices != null && vertices.Length >= num && uv != null && uv.Length >= num && colors32 != null && colors32.Length >= num && tris != null && tris.Length >= textSB.Length * 6)
		{
			flag4 = true;
		}
		else
		{
			mesh.Clear();
			vertices = new Vector3[num];
			uv = new Vector2[num];
			colors32 = new Color32[num];
			tris = new int[textSB.Length * 6];
			flag = true;
		}
		prevTextSBLength = length;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = offsetZ;
		if (useD3D9Sampling)
		{
			num2 = (num3 = 0.5f);
		}
		float num5 = num2;
		float num6 = num3;
		float z = num4;
		int num7 = 0;
		int num8 = 0;
		float num9 = 0f;
		float b = 0f;
		int num10 = 0;
		float num11 = pxUnitScale * size * letterSpacing;
		if (pixelSnapTransformPos)
		{
			num11 = Mathf.Floor(num11);
		}
		for (int i = 0; i < textSB.Length; i++)
		{
			char c = textSB[i];
			switch (c)
			{
			case '\n':
			{
				float num13 = num5 - num2 - num9;
				b = Mathf.Max(num13, b);
				if (alignment != TextAlignment.Left)
				{
					int startIndex = num8 << 2;
					int endIndex = num7 << 2;
					alignRow(ref vertices, num13, startIndex, endIndex);
				}
				num5 = num2;
				num6 -= Mathf.Round((float)pxFontSize * lineSpacing);
				num8 = num7;
				num9 = 0f;
				num10 = 0;
				continue;
			}
			case '\t':
			{
				float num12 = Mathf.Round(tabSize * (float)pxFontSize);
				if (num12 != 0f)
				{
					num5 = Mathf.Floor((num5 - num2) / num12 + 1f) * num12 + num2;
				}
				continue;
			}
			}
			CharacterInfo info;
			if (font.GetCharacterInfo(c, out info, pxFontSize, fontStyle))
			{
				if (num10 == 0)
				{
					num5 -= (float)info.minX;
				}
				float num14 = info.minX;
				float num15 = info.maxX;
				float num16 = (float)info.minY - pxBaseline;
				float num17 = (float)info.maxY - pxBaseline;
				int num18 = num7 << 2;
				int num19 = num18;
				int num20 = num18;
				vertices[num19] = new Vector3((num5 + num14) / pxUnitScale, (num6 + num16) / pxUnitScale, z);
				vertices[++num19] = new Vector3((num5 + num14) / pxUnitScale, (num6 + num17) / pxUnitScale, z);
				vertices[++num19] = new Vector3((num5 + num15) / pxUnitScale, (num6 + num17) / pxUnitScale, z);
				vertices[++num19] = new Vector3((num5 + num15) / pxUnitScale, (num6 + num16) / pxUnitScale, z);
				colors32[num20] = color;
				colors32[++num20] = color;
				colors32[++num20] = color;
				colors32[++num20] = color;
				int num21 = num18 + (num7 << 1);
				tris[num21] = num18;
				tris[++num21] = num18 + 1;
				tris[++num21] = num18 + 2;
				tris[++num21] = num18;
				tris[++num21] = num18 + 2;
				tris[++num21] = num18 + 3;
				int num22 = num18;
				uv[num22] = info.uvBottomLeft;
				uv[++num22] = info.uvTopLeft;
				uv[++num22] = info.uvTopRight;
				uv[++num22] = info.uvBottomRight;
				num7++;
				num5 += (float)info.advance + num11;
				num9 = info.advance - info.glyphWidth;
				num10++;
			}
		}
		if (alignment != TextAlignment.Left)
		{
			float pxRowWidth = num5 - num2 - num9;
			int startIndex2 = num8 << 2;
			int endIndex2 = num7 << 2;
			alignRow(ref vertices, pxRowWidth, startIndex2, endIndex2);
		}
		int num23 = vertices.Length;
		int num24 = num7 << 2;
		if (num24 == 0 && num23 > 0)
		{
			vertices[num24++] = Vector3.zero;
		}
		if (num24 < num23)
		{
			Vector3 vector = vertices[num24 - 1];
			Color32 color2 = colors32[num24 - 1];
			Vector2 vector2 = uv[num24 - 1];
			for (int j = num24; j < num23; j++)
			{
				vertices[j] = vector;
				colors32[j] = color2;
				uv[j] = vector2;
			}
			int num25 = tris.Length;
			for (int k = num7 * 6; k < num25; k++)
			{
				tris[k] = 0;
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.colors32 = colors32;
		if (flag4 && !flag)
		{
			mesh.RecalculateBounds();
		}
		else
		{
			mesh.triangles = tris;
			mesh.RecalculateBounds();
		}
		float num26 = 0f;
		if (alignment == TextAlignment.Center)
		{
			num26 = 0.5f;
		}
		else if (alignment == TextAlignment.Right)
		{
			num26 = 1f;
		}
		if (anchor != DynamicTextAnchor.BaselineLeft || alignment != TextAlignment.Left)
		{
			Bounds bounds = mesh.bounds;
			float num27 = Mathf.Floor(bounds.extents.x * 2f * num26 * pxUnitScale);
			float num28 = 0f;
			switch (anchor)
			{
			case DynamicTextAnchor.UpperLeft:
				num28 += (0f - bounds.max.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.UpperCenter:
				num27 += (0f - bounds.extents.x) * pxUnitScale;
				num28 += (0f - bounds.max.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.UpperRight:
				num27 += (0f - bounds.extents.x) * 2f * pxUnitScale;
				num28 += (0f - bounds.max.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.MiddleLeft:
				num28 += (0f - bounds.max.y + bounds.extents.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.MiddleCenter:
				num27 += (0f - bounds.extents.x) * pxUnitScale;
				num28 += (0f - bounds.max.y + bounds.extents.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.MiddleRight:
				num27 += (0f - bounds.extents.x) * 2f * pxUnitScale;
				num28 += (0f - bounds.max.y + bounds.extents.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.LowerLeft:
				num28 += (0f - bounds.min.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.LowerCenter:
				num27 += (0f - bounds.extents.x) * pxUnitScale;
				num28 += (0f - bounds.min.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.LowerRight:
				num27 += (0f - bounds.extents.x) * 2f * pxUnitScale;
				num28 += (0f - bounds.min.y) * pxUnitScale;
				break;
			case DynamicTextAnchor.BaselineCenter:
				num27 += (0f - bounds.extents.x) * pxUnitScale;
				break;
			case DynamicTextAnchor.BaselineRight:
				num27 += (0f - bounds.extents.x) * 2f * pxUnitScale;
				break;
			}
			num27 = Mathf.Floor(num27) / pxUnitScale;
			num28 = Mathf.Floor(num28) / pxUnitScale;
			Vector3 vector3 = new Vector3(num27, num28, 0f);
			for (int l = 0; l < vertices.Length; l++)
			{
				vertices[l] += vector3;
			}
			mesh.vertices = vertices;
			mesh.RecalculateBounds();
		}
	}

	public void Update()
	{
		if (!initialized)
		{
			initialize();
			if (!initialized)
			{
				return;
			}
		}
		if (rebuildCallbackLoopFallback_reducedFontPxSize < 1f && !suppressDebugLogs)
		{
			Debug.LogWarning("Font px size reduced to " + rebuildCallbackLoopFallback_reducedFontPxSize + "x due to rebuild callback loop (likely low on memory or using too large text).", this);
		}
		rebuildCallbackLoopFallback_reducedFontPxSize = 1f;
		rebuildCallbacksBeforeUpdate = 0;
		if (pendingInitializeFromReset)
		{
			initialize();
		}
		if (editorInspectorTextUpdatePending && Time.renderedFrameCount % 50 == 0)
		{
			serializedText = textSB.ToString();
			editorInspectorTextUpdatePending = false;
		}
		bool flag = false;
		if (pendingGenerateMesh || prevColor != color || prevSize != size || prevLineSpacing != lineSpacing || prevLetterSpacing != letterSpacing || (prevPixelSnapTransformPos != pixelSnapTransformPos && letterSpacing != 0f) || prevFontStyle != fontStyle || Screen.width != refScreenWidth || Screen.height != refScreenHeight || prevOffsetZ != offsetZ || prevAnchor != anchor || prevAlignment != alignment || prevTabSize != tabSize || prevFont != font || prevAutoSetFontMaterial != autoSetFontMaterial || baselineRefChar.Length != 1 || prevBaselineRefChar != baselineRefChar[0] || prevMetricsRefChars == null || !prevMetricsRefChars.Equals(metricsRefChars) || prevCam != cam)
		{
			GenerateMesh();
			flag = true;
		}
		else if (camTrn != null && (prevCamPos != camTrn.position || prevCamRot != camTrn.rotation))
		{
			float unitScale = calculatePxUnitScale();
			if ((float)calculatePxFontSize(ref unitScale) != (float)pxFontSize)
			{
				GenerateMesh();
				flag = true;
			}
			else
			{
				updateUnsnappedPosByDelta();
				snapTransformPosition();
				flag = true;
			}
		}
		else if ((trn != null && prevSnappedPos != trn.position) || prevPixelSnapTransformPos != pixelSnapTransformPos)
		{
			if (prevSnappedPos.z != trn.position.z)
			{
				GenerateMesh();
				flag = true;
			}
			else
			{
				updateUnsnappedPosByDelta();
				snapTransformPosition();
				flag = true;
			}
		}
		if (flag && autoFaceCam && cam != null && trn != null)
		{
			base.transform.LookAt(trn.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
		}
	}
}
