using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

[AddComponentMenu("Mesh/Dynamic Text")]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class DynamicTextClean : MonoBehaviour
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
	protected string text = string.Empty;

	[HideInInspector]
	[SerializeField]
	public string initialText = "Text";

	[SerializeField]
	public string assignedText = "Text";

	[HideInInspector]
	protected readonly int assetVersion = 1101;

	[HideInInspector]
	[SerializeField]
	protected int version = 1023;

	protected static string _info_copyrightSrcLicense;

	[HideInInspector]
	private Mesh mesh;

	[HideInInspector]
	private Transform b;

	[HideInInspector]
	private float c;

	[HideInInspector]
	private float d;

	[HideInInspector]
	private float e;

	[HideInInspector]
	private int f;

	[HideInInspector]
	private int g;

	[HideInInspector]
	private float h;

	[HideInInspector]
	private Vector3 i;

	[HideInInspector]
	private int _width;

	[HideInInspector]
	private int _height;

	[HideInInspector]
	private float _offsetZ;

	[HideInInspector]
	private float _size;

	[HideInInspector]
	private float _lineSpacing;

	[HideInInspector]
	private float _letterSpacing;

	[HideInInspector]
	private DynamicTextAnchor _anchor;

	[HideInInspector]
	private TextAlignment _alignment;

	[HideInInspector]
	private float _tabSize;

	[HideInInspector]
	private FontStyle _fontStyle;

	[HideInInspector]
	private Font _font;

	[HideInInspector]
	private bool _autoSetFontMaterial;

	[HideInInspector]
	private Color _color;

	[HideInInspector]
	private char _baseLineRefChar;

	[HideInInspector]
	private string x = string.Empty;

	[HideInInspector]
	private bool _pixelsSnapTransformPos;

	[HideInInspector]
	private Vector3 z;

	[HideInInspector]
	private Camera _cam;

	[HideInInspector]
	private Transform ab;

	[HideInInspector]
	private Vector3 ac;

	[HideInInspector]
	private Quaternion ad;

	[HideInInspector]
	private int ae = -1;

	[HideInInspector]
	private bool af;

	[HideInInspector]
	private bool ag;

	[HideInInspector]
	private bool finishedText;

	[HideInInspector]
	private List<char> ai = new List<char>();

	[HideInInspector]
	private StringBuilder aj = new StringBuilder();

	[HideInInspector]
	private string ak = string.Empty;

	[HideInInspector]
	private bool al;

	[HideInInspector]
	private bool editorPlaying;

	[HideInInspector]
	private Vector3[] vertices;

	[HideInInspector]
	private Vector2[] uv;

	[HideInInspector]
	private Color32[] colors32;

	[HideInInspector]
	private int[] triangles;

	[HideInInspector]
	private int @as;

	[HideInInspector]
	private float at = 1f;

	[HideInInspector]
	private bool au;

	public bool suppressDebugLogs;

	private static bool av;

	private static bool aw;

	public float ascent
	{
		get
		{
			if (h == 0f)
			{
				return 0f;
			}
			return d / h;
		}
	}

	public float baseline
	{
		get
		{
			if (h == 0f)
			{
				return 0f;
			}
			return c / h;
		}
	}

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

	public float descent
	{
		get
		{
			if (h == 0f)
			{
				return 0f;
			}
			return e / h;
		}
	}

	static DynamicTextClean()
	{
		_info_copyrightSrcLicense = "Copyright © 2013-2015 Strobotnik Ltd. Dynamic Text component for Unity. If you want access to source code, contact us for a separate license: www.strobotnik.com";
		av = false;
		aw = false;
	}

	private void FuncA(ref Vector3[] A_0, float A_1, int A_2, int A_3)
	{
		float num = 0f;
		if (alignment != TextAlignment.Right)
		{
			if (alignment != TextAlignment.Center)
			{
				return;
			}
			num = Mathf.Floor((0f - A_1) / 2f) / h;
		}
		else
		{
			num = Mathf.Floor(0f - A_1) / h;
		}
		Vector3 vector = new Vector3(num, 0f, 0f);
		for (int i = A_2; i < A_3; i++)
		{
			A_0[i] += vector;
		}
	}

	private int FuncA(ref float A_0)
	{
		if (size < 0.001f)
		{
			size = 0.001f;
		}
		int num = (int)(size * A_0);
		int num2 = minFontPxSize;
		int num3 = maxFontPxSize;
		if (at < 1f)
		{
			if (num2 > 6)
			{
				num2 = 6;
			}
			num3 = (int)((float)maxFontPxSize * at);
			if (num3 < num2)
			{
				num3 = num2;
			}
		}
		if (num < num2 || num > num3)
		{
			num = Mathf.Clamp(num, num2, num3);
			A_0 = (float)num / size;
		}
		return num;
	}

	private void FuncA(Font A_0)
	{
		if (!(A_0 != font))
		{
			@as++;
			if (@as >= 3)
			{
				at *= 0.75f;
			}
			GenerateMesh();
		}
	}

	[ContextMenu("Regenerate Mesh")]
	private void FuncA()
	{
		ag = true;
		GenerateMesh();
	}

	public void Awake()
	{
		FuncG();
	}

	private void FuncB()
	{
		@as++;
		if (@as >= 3)
		{
			at *= 0.75f;
		}
		GenerateMesh();
	}

	private float FuncC()
	{
		if (!cam.orthographic)
		{
			float num = (float)Math.PI / 180f * cam.fieldOfView / 2f;
			Vector3 forward = ab.forward;
			float distanceToPoint = new Plane(forward, ab.position).GetDistanceToPoint(i + new Vector3(0f, 0f, offsetZ));
			float num2 = distanceToPoint * Mathf.Tan(num);
			if (num2 < 1E-06f)
			{
				num2 = -1f;
			}
			return (float)(cam.pixelHeight / 2) / num2;
		}
		return (float)cam.pixelHeight / (2f * cam.orthographicSize);
	}

	private void FuncD()
	{
		font.RequestCharactersInTexture(metricsRefChars, f, fontStyle);
		font.RequestCharactersInTexture(baselineRefChar, f, fontStyle);
		c = size / 2f;
		CharacterInfo info;
		if (font.GetCharacterInfo(baselineRefChar[0], out info, f, fontStyle))
		{
			c = info.minY;
		}
		else if (!suppressDebugLogs)
		{
			string[] array = new string[5] { "Can't get baseline ref character info (baselineRefChar:'", baselineRefChar, "', Font:", font.name, ")" };
			Debug.LogWarning(string.Concat(array), this);
		}
		bool flag = true;
		d = float.NegativeInfinity;
		e = float.PositiveInfinity;
		string text = string.Empty;
		int length = metricsRefChars.Length;
		for (int i = 0; i < length; i++)
		{
			if (font.GetCharacterInfo(metricsRefChars[i], out info, f, fontStyle))
			{
				d = Mathf.Max(d, (float)info.maxY - c);
				e = Mathf.Min(e, (float)info.minY - c);
				flag = false;
			}
			else
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
			d = size / 2f;
			e = 0f - d;
			if (Application.isEditor && !suppressDebugLogs)
			{
				Debug.LogWarning(((length != 0) ? string.Empty : "No metrics ref chars - ") + "Using half of given size as fallback ascent & descent", this);
			}
		}
	}

	private void FuncE()
	{
		if (b == null || cam == null)
		{
			return;
		}
		Vector3 lossyScale = b.lossyScale;
		if (Mathf.Approximately(lossyScale.x, 0f) || Mathf.Approximately(lossyScale.y, 0f))
		{
			if (!aw && Application.isEditor)
			{
				if (!suppressDebugLogs)
				{
					Debug.LogWarning("Object has zero scale - Not snapping (this warning is logged only once even if problem persists)", this);
				}
				aw = true;
			}
			return;
		}
		if (_pixelsSnapTransformPos != pixelSnapTransformPos)
		{
			i = b.position;
		}
		_pixelsSnapTransformPos = pixelSnapTransformPos;
		if (!pixelSnapTransformPos)
		{
			i = b.position;
			z = i;
			return;
		}
		Vector3 position = i + new Vector3(0f, 0f, offsetZ);
		Vector3 position2 = cam.WorldToScreenPoint(position);
		position2.x = Mathf.Round(position2.x);
		position2.y = Mathf.Round(position2.y);
		position = cam.ScreenToWorldPoint(position2);
		Vector3 position3 = new Vector3(position.x, position.y, position.z - offsetZ);
		b.position = position3;
		z = position3;
	}

	private void FuncF()
	{
		if (!(b == null) && z != b.position)
		{
			i += b.position - z;
		}
	}

	public void FinishedTextSB()
	{
		finishedText = true;
		if (Application.isEditor)
		{
			if (Application.isPlaying)
			{
				editorPlaying = true;
			}
			else
			{
				initialText = textSB.ToString();
			}
		}
		ai.Clear();
		for (int i = 0; i < metricsRefChars.Length; i++)
		{
			char item = metricsRefChars[i];
			if (!ai.Contains(item))
			{
				ai.Add(item);
			}
		}
		int length = textSB.Length;
		for (int j = 0; j < length; j++)
		{
			char item2 = textSB[j];
			if (!ai.Contains(item2))
			{
				ai.Add(item2);
			}
		}
		aj.EnsureCapacity(ai.Count);
		int count = ai.Count;
		aj.Length = count;
		bool flag = count != ak.Length;
		for (int k = 0; k < count; k++)
		{
			char c = ai[k];
			aj[k] = c;
			if (!flag && ak[k] != c)
			{
				flag = true;
			}
		}
		if (flag)
		{
			ak = aj.ToString();
		}
		GenerateMesh();
	}

	private bool FuncG()
	{
		if (au)
		{
			return true;
		}
		if (Screen.width == 0 || Screen.height == 0)
		{
			return false;
		}
		if (base.transform == null && !suppressDebugLogs)
		{
			Debug.LogWarning("transform null", this);
		}
		b = base.transform;
		ai.Clear();
		if (ai.Capacity < metricsRefChars.Length)
		{
			ai.Capacity = metricsRefChars.Length;
		}
		FuncI();
		if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 9"))
		{
			al = true;
		}
		if (font == null)
		{
			TextMesh component = GetComponent<TextMesh>();
			if ((bool)component)
			{
				cam = Camera.main;
				initialText = component.text;
				textSB.Length = 0;
				textSB.Append(component.text);
				offsetZ = component.offsetZ;
				size = component.characterSize * (float)((component.fontSize != 0) ? component.fontSize : 16) / 10f;
				lineSpacing = 1.25f * component.lineSpacing;
				anchor = (DynamicTextAnchor)component.anchor;
				alignment = component.alignment;
				tabSize = component.tabSize / 10f;
				fontStyle = component.fontStyle;
				font = component.font;
				color = component.color;
				UnityEngine.Object.DestroyImmediate(component);
			}
			else
			{
				if (GetComponent<MeshFilter>() != null)
				{
					UnityEngine.Object.DestroyImmediate(GetComponent<MeshFilter>());
					mesh.Clear();
					mesh = null;
				}
				component = base.gameObject.AddComponent<TextMesh>();
				font = component.font;
				UnityEngine.Object.DestroyImmediate(component);
			}
		}
		b = base.transform;
		z = (i = b.position);
		if (pixelSnapTransformPos)
		{
			FuncF();
			FuncE();
		}
		if (!finishedText)
		{
			SetText(initialText);
		}
		af = false;
		au = true;
		return true;
	}

	public void GenerateMesh()
	{
		bool flag = ag;
		ag = false;
		if (!font)
		{
			if (!suppressDebugLogs)
			{
				Debug.LogWarning("No font specified - not generating mesh!", this);
			}
			return;
		}
		if (baselineRefChar.Length != 1)
		{
			if (baselineRefChar.Length != 0)
			{
				baselineRefChar = baselineRefChar.Substring(0, 1);
			}
			else
			{
				if (!suppressDebugLogs)
				{
					Debug.LogWarning("No Baseline Reference Char, using 'x'!", this);
				}
				baselineRefChar = "x";
			}
		}
		_width = Screen.width;
		_height = Screen.height;
		if (cam == null)
		{
			cam = Camera.main;
			if (cam == null)
			{
				if (!av)
				{
					if (!suppressDebugLogs)
					{
						Debug.LogError("No camera - can't generate mesh (this error is logged only once even if problem persists)", this);
					}
					av = true;
				}
				return;
			}
		}
		if (ab == null || _cam != cam)
		{
			ab = cam.transform;
		}
		_cam = cam;
		ac = ab.position;
		ad = ab.rotation;
		FuncF();
		h = FuncC();
		_offsetZ = offsetZ;
		if (size == 0f)
		{
			if (!cam.orthographic)
			{
				size = 1f;
			}
			else
			{
				size = cam.orthographicSize / 5f;
			}
		}
		_size = size;
		_lineSpacing = lineSpacing;
		_letterSpacing = letterSpacing;
		_anchor = anchor;
		_alignment = alignment;
		_tabSize = tabSize;
		_fontStyle = fontStyle;
		bool flag2 = false;
		bool flag3 = false;
		if (_font != font)
		{
			if (_font != null)
			{
				Font.textureRebuilt -= FuncA;
			}
			if ((bool)GetComponent<MeshRenderer>())
			{
				flag3 = true;
			}
			else if (!suppressDebugLogs)
			{
				Debug.LogWarning("No Mesh Renderer component", this);
			}
			_font = font;
			if (font != null)
			{
				flag2 = true;
				Font.textureRebuilt -= FuncA;
				Font.textureRebuilt += FuncA;
			}
		}
		if (flag3 || _autoSetFontMaterial != autoSetFontMaterial)
		{
			if (autoSetFontMaterial)
			{
				GetComponent<Renderer>().sharedMaterial = font.material;
			}
			_autoSetFontMaterial = autoSetFontMaterial;
		}
		_color = this.color;
		if (baselineRefChar == null || baselineRefChar.Length != 1 || _baseLineRefChar != baselineRefChar[0] || x == null || !x.Equals(metricsRefChars))
		{
			flag2 = true;
		}
		_baseLineRefChar = baselineRefChar[0];
		x = metricsRefChars;
		g = f;
		f = FuncA(ref h);
		FuncE();
		if (g != f)
		{
			flag2 = true;
		}
		if (flag2)
		{
			FuncD();
		}
		font.RequestCharactersInTexture(ak, f, fontStyle);
		MeshFilter meshFilter = base.gameObject.GetComponent<MeshFilter>();
		if (!meshFilter)
		{
			meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		if (mesh == null)
		{
			mesh = new Mesh
			{
				name = "Dynamic Text Mesh",
				hideFlags = HideFlags.HideAndDontSave
			};
			meshFilter.mesh = mesh;
		}
		Color32 color = this.color;
		int length = textSB.Length;
		int num = length * 4;
		bool flag4 = false;
		if (ae != length || vertices == null || vertices.Length < num || uv == null || uv.Length < num || colors32 == null || colors32.Length < num || triangles == null || triangles.Length < textSB.Length * 6)
		{
			mesh.Clear();
			vertices = new Vector3[num];
			uv = new Vector2[num];
			colors32 = new Color32[num];
			triangles = new int[textSB.Length * 6];
			flag = true;
		}
		else
		{
			flag4 = true;
		}
		ae = length;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = offsetZ;
		if (al)
		{
			float num5 = 0.5f;
			num3 = num5;
			num2 = num5;
		}
		float num6 = num2;
		float num7 = num3;
		float num8 = num4;
		int num9 = 0;
		int num10 = 0;
		float num11 = 0f;
		float num12 = 0f;
		int num13 = 0;
		float num14 = h * size * letterSpacing;
		if (pixelSnapTransformPos)
		{
			num14 = Mathf.Floor(num14);
		}
		for (int i = 0; i < textSB.Length; i++)
		{
			char c = textSB[i];
			switch (c)
			{
			case '\n':
			{
				float num16 = num6 - num2 - num11;
				num12 = Mathf.Max(num16, num12);
				if (alignment != TextAlignment.Left)
				{
					int a_ = num10 << 2;
					int a_2 = num9 << 2;
					FuncA(ref vertices, num16, a_, a_2);
				}
				num6 = num2;
				num7 -= Mathf.Round((float)f * lineSpacing);
				num10 = num9;
				num11 = 0f;
				num13 = 0;
				continue;
			}
			case '\t':
			{
				float num15 = Mathf.Round(tabSize * (float)f);
				if (num15 != 0f)
				{
					num6 = Mathf.Floor((num6 - num2) / num15 + 1f) * num15 + num2;
				}
				continue;
			}
			}
			CharacterInfo info;
			if (font.GetCharacterInfo(c, out info, f, fontStyle))
			{
				if (num13 == 0)
				{
					num6 -= (float)info.minX;
				}
				float num17 = info.minX;
				float num18 = info.maxX;
				float num19 = (float)info.minY - this.c;
				float num20 = (float)info.maxY - this.c;
				int num21 = num9 << 2;
				int num22 = num21;
				int num23 = num21;
				vertices[num22] = new Vector3((num6 + num17) / h, (num7 + num19) / h, num8);
				int num24 = num22 + 1;
				num22 = num24;
				vertices[num24] = new Vector3((num6 + num17) / h, (num7 + num20) / h, num8);
				int num25 = num22 + 1;
				num22 = num25;
				vertices[num25] = new Vector3((num6 + num18) / h, (num7 + num20) / h, num8);
				int num26 = num22 + 1;
				num22 = num26;
				vertices[num26] = new Vector3((num6 + num18) / h, (num7 + num19) / h, num8);
				colors32[num23] = color;
				int num27 = num23 + 1;
				num23 = num27;
				colors32[num27] = color;
				int num28 = num23 + 1;
				num23 = num28;
				colors32[num28] = color;
				int num29 = num23 + 1;
				num23 = num29;
				colors32[num29] = color;
				int num30 = num21 + (num9 << 1);
				triangles[num30] = num21;
				int num31 = num30 + 1;
				num30 = num31;
				triangles[num31] = num21 + 1;
				int num32 = num30 + 1;
				num30 = num32;
				triangles[num32] = num21 + 2;
				int num33 = num30 + 1;
				num30 = num33;
				triangles[num33] = num21;
				int num34 = num30 + 1;
				num30 = num34;
				triangles[num34] = num21 + 2;
				int num35 = num30 + 1;
				num30 = num35;
				triangles[num35] = num21 + 3;
				int num36 = num21;
				uv[num36] = info.uvBottomLeft;
				int num37 = num36 + 1;
				num36 = num37;
				uv[num37] = info.uvTopLeft;
				int num38 = num36 + 1;
				num36 = num38;
				uv[num38] = info.uvTopRight;
				int num39 = num36 + 1;
				num36 = num39;
				uv[num39] = info.uvBottomRight;
				num9++;
				num6 += (float)info.advance + num14;
				num11 = info.advance - info.glyphWidth;
				num13++;
			}
		}
		if (alignment != TextAlignment.Left)
		{
			float a_3 = num6 - num2 - num11;
			int a_4 = num10 << 2;
			int a_5 = num9 << 2;
			FuncA(ref vertices, a_3, a_4, a_5);
		}
		int num40 = vertices.Length;
		int num41 = num9 << 2;
		if (num41 == 0 && num40 > 0)
		{
			int num42 = num41++;
			vertices[num42] = Vector3.zero;
		}
		if (num41 < num40)
		{
			Vector3 vector = vertices[num41 - 1];
			Color32 color2 = colors32[num41 - 1];
			Vector2 vector2 = uv[num41 - 1];
			for (int j = num41; j < num40; j++)
			{
				vertices[j] = vector;
				colors32[j] = color2;
				uv[j] = vector2;
			}
			int num43 = triangles.Length;
			for (int k = num9 * 6; k < num43; k++)
			{
				triangles[k] = 0;
			}
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.colors32 = colors32;
		if (!flag4 || flag)
		{
			mesh.triangles = triangles;
		}
		mesh.RecalculateBounds();
		float num44 = 0f;
		if (alignment == TextAlignment.Center)
		{
			num44 = 0.5f;
		}
		else if (alignment == TextAlignment.Right)
		{
			num44 = 1f;
		}
		if (anchor != DynamicTextAnchor.BaselineLeft || alignment != TextAlignment.Left)
		{
			Bounds bounds = mesh.bounds;
			float num45 = bounds.extents.x;
			float num46 = Mathf.Floor(num45 * 2f * num44 * h);
			float num47 = 0f;
			switch (anchor)
			{
			case DynamicTextAnchor.UpperLeft:
				num47 += (0f - bounds.max.y) * h;
				break;
			case DynamicTextAnchor.UpperCenter:
				num46 += (0f - bounds.extents.x) * h;
				num47 += (0f - bounds.max.y) * h;
				break;
			case DynamicTextAnchor.UpperRight:
				num46 += (0f - bounds.extents.x) * 2f * h;
				num47 += (0f - bounds.max.y) * h;
				break;
			case DynamicTextAnchor.MiddleLeft:
				num47 += (0f - bounds.max.y + bounds.extents.y) * h;
				break;
			case DynamicTextAnchor.MiddleCenter:
				num46 += (0f - bounds.extents.x) * h;
				num47 += (0f - bounds.max.y + bounds.extents.y) * h;
				break;
			case DynamicTextAnchor.MiddleRight:
				num46 += (0f - bounds.extents.x) * 2f * h;
				num47 += (0f - bounds.max.y + bounds.extents.y) * h;
				break;
			case DynamicTextAnchor.LowerLeft:
				num47 += (0f - bounds.min.y) * h;
				break;
			case DynamicTextAnchor.LowerCenter:
				num46 += (0f - bounds.extents.x) * h;
				num47 += (0f - bounds.min.y) * h;
				break;
			case DynamicTextAnchor.LowerRight:
				num46 += (0f - bounds.extents.x) * 2f * h;
				num47 += (0f - bounds.min.y) * h;
				break;
			case DynamicTextAnchor.BaselineCenter:
				num46 += (0f - bounds.extents.x) * h;
				break;
			case DynamicTextAnchor.BaselineRight:
				num46 += (0f - bounds.extents.x) * 2f * h;
				break;
			}
			num46 = Mathf.Floor(num46) / h;
			num47 = Mathf.Floor(num47) / h;
			Vector3 vector3 = new Vector3(num46, num47, 0f);
			for (int l = 0; l < vertices.Length; l++)
			{
				vertices[l] += vector3;
			}
			mesh.vertices = vertices;
			mesh.RecalculateBounds();
		}
		assignedText = textSB.ToString();
	}

	public string GetText()
	{
		if (!finishedText)
		{
			return initialText;
		}
		string text = textSB.ToString();
		if (initialText == null || !initialText.Equals(text))
		{
			initialText = text;
		}
		return text;
	}

	private void FuncH()
	{
		if ((bool)font)
		{
			Font.textureRebuilt -= FuncA;
		}
		if ((bool)_font)
		{
			Font.textureRebuilt -= FuncA;
		}
		_font = null;
		if ((bool)mesh)
		{
			mesh.Clear();
			ag = true;
		}
	}

	private void FuncI()
	{
		if (version < assetVersion || (text != null && text.Length > 0))
		{
			if (text != null && text.Length > 0)
			{
				initialText = text;
			}
			if (initialText == null)
			{
				initialText = string.Empty;
			}
			text = string.Empty;
			version = assetVersion;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public string internal_GetDeprecatedText()
	{
		return text;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public int internal_GetVersion()
	{
		return version;
	}

	public void OnDestroy()
	{
		if ((bool)font)
		{
			Font.textureRebuilt -= FuncA;
		}
		GetComponent<MeshFilter>().mesh = null;
		UnityEngine.Object.DestroyImmediate(mesh);
		mesh = null;
	}

	public void OnDisable()
	{
	}

	public void OnEnable()
	{
		GetComponent<MeshFilter>().mesh = mesh;
		FuncI();
		SetText((assignedText == null) ? initialText : assignedText);
		FuncG();
		FuncH();
	}

	public void Reset()
	{
		FuncH();
		af = true;
	}

	public void SetText(string newText)
	{
		if (newText == null)
		{
			newText = string.Empty;
		}
		textSB.EnsureCapacity(newText.Length);
		textSB.Length = 0;
		textSB.Append(newText);
		FinishedTextSB();
	}

	public void Start()
	{
		FuncG();
	}

	public void Update()
	{
		if (!au)
		{
			FuncG();
			if (!au)
			{
				return;
			}
		}
		if (at < 1f && !suppressDebugLogs)
		{
			Debug.LogWarning("Font px size reduced to " + at + "x due to rebuild callback loop (likely low on memory or using too large text).", this);
		}
		at = 1f;
		@as = 0;
		if (af)
		{
			FuncG();
		}
		if (editorPlaying && Time.renderedFrameCount % 50 == 0)
		{
			initialText = textSB.ToString();
			editorPlaying = false;
		}
		bool flag = false;
		if (ag || _color != color || _size != size || _lineSpacing != lineSpacing || _letterSpacing != letterSpacing || (_pixelsSnapTransformPos != pixelSnapTransformPos && letterSpacing != 0f) || _fontStyle != fontStyle || Screen.width != _width || Screen.height != _height || _offsetZ != offsetZ || _anchor != anchor || _alignment != alignment || _tabSize != tabSize || _font != font || _autoSetFontMaterial != autoSetFontMaterial || baselineRefChar.Length != 1 || _baseLineRefChar != baselineRefChar[0] || x == null || !x.Equals(metricsRefChars) || _cam != cam)
		{
			GenerateMesh();
			flag = true;
		}
		else if (ab != null && (ac != ab.position || ad != ab.rotation))
		{
			float A_ = FuncC();
			if ((float)FuncA(ref A_) == (float)f)
			{
				FuncF();
				FuncE();
				flag = true;
			}
			else
			{
				GenerateMesh();
				flag = true;
			}
		}
		else if ((b != null && z != b.position) || _pixelsSnapTransformPos != pixelSnapTransformPos)
		{
			if (z.z == b.position.z)
			{
				FuncF();
				FuncE();
				flag = true;
			}
			else
			{
				GenerateMesh();
				flag = true;
			}
		}
		if (flag && autoFaceCam && cam != null && b != null)
		{
			base.transform.LookAt(b.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
		}
	}
}
