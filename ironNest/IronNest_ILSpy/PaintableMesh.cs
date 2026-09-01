using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaintableMesh : MonoBehaviour
{
	public int TextureSize;

	public string MaterialTextureProperty;

	public float InteractionRange;

	public Color BrushColor;

	public float BrushSize;

	public float BrushScaleX;

	public float BrushScaleY;

	public float BrushSoftness;

	public Shader DrawShader;

	public Camera PaintCamera;

	public RenderTexture PaintTexture;

	private MeshRenderer meshRenderer;

	private Material runtimeMaterial;

	private Texture2D cpuBrush;

	private Material drawMaterial;

	private Vector2? lastPaintUv;

	private unsafe void Awake()
	{
		//IL_00f3: Expected O, but got Ref
		//IL_014c: Expected O, but got I4
		//IL_02a6: Expected O, but got Ref
		//IL_02cc: Expected O, but got I4
		//IL_018e: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		MeshRenderer meshRenderer = default(MeshRenderer);
		this.meshRenderer = meshRenderer;
		Material sharedMaterial = ((Renderer)this.meshRenderer).GetSharedMaterial();
		Material material = UnityEngine.Object.Instantiate(sharedMaterial);
		runtimeMaterial = material;
		((Renderer)this.meshRenderer).SetMaterial(runtimeMaterial);
		RenderTextureFormat renderTextureFormat = default(RenderTextureFormat);
		RenderTexture paintTexture = new RenderTexture(TextureSize, TextureSize, 0, renderTextureFormat);
		PaintTexture = paintTexture;
		PaintTexture.enableRandomWrite = true;
		PaintTexture.filterMode = FilterMode.Bilinear;
		PaintTexture.wrapMode = TextureWrapMode.Clamp;
		bool flag = PaintTexture.Create();
		RenderTexture active = RenderTexture.GetActive();
		RenderTexture.SetActive(PaintTexture);
		object obj = default(object);
		GL.Clear(clearDepth: true, clearColor: true, (Color)(&obj));
		RenderTexture.SetActive(active);
		runtimeMaterial.SetTexture(MaterialTextureProperty, PaintTexture);
		Texture2D texture2D = new Texture2D(256, 256, TextureFormat.RGBA32, (byte)renderTextureFormat != 0);
		texture2D.filterMode = FilterMode.Bilinear;
		texture2D.wrapMode = TextureWrapMode.Clamp;
		obj = 0;
		TextureFormat textureFormat = TextureFormat.RGBA32;
		int num = 0;
		bool flag3;
		do
		{
			int num2 = 0;
			bool flag2;
			do
			{
				float num3 = (float)num2 + 0.5f;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
				float num4 = num3 * (1f / 128f);
				if (!(1f < num4))
				{
				}
				texture2D.SetPixel(num2, num, (Color)(&obj));
				num2++;
				flag2 = num2 < 256;
				obj = 1065353216;
				textureFormat = (TextureFormat)(int)(&obj);
			}
			while (flag2);
			num++;
			flag3 = num < 256;
			obj = 1065353216;
			textureFormat = (TextureFormat)(int)(&obj);
		}
		while (flag3);
		texture2D.Apply();
		cpuBrush = texture2D;
		Shader shader2;
		if (DrawShader == null)
		{
			Shader shader = Shader.Find("Sprites/Default");
			shader2 = shader;
		}
		else
		{
			shader2 = DrawShader;
		}
		Material material2 = new Material(shader2);
		drawMaterial = material2;
		if (PaintCamera == null)
		{
			Camera main = Camera.main;
			PaintCamera = main;
		}
	}

	private void Update()
	{
		//IL_0035: Expected O, but got I4
		if (Mouse._003Ccurrent_003Ek__BackingField != null)
		{
			Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
			if (!mouse._003CleftButton_003Ek__BackingField.isPressed)
			{
				lastPaintUv = (Vector2?)(object)0;
				_ = 0;
			}
			else
			{
				Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18088D950");
				Vector2 screenPos = default(Vector2);
				TryPaint(screenPos);
			}
		}
	}

	private unsafe void TryPaint(Vector2 screenPos)
	{
		//IL_0032: Expected O, but got Ref
		//IL_008b: Expected O, but got Ref
		if (!(PaintCamera != null))
		{
			return;
		}
		Vector3 vector = default(Vector3);
		Ray ray = PaintCamera.ScreenPointToRay((Vector3)(&vector));
		int mask = LayerMask.GetMask(new string[1] { "Shell" });
		if (Physics.Raycast((Ray)(&vector), out var hitInfo, InteractionRange, mask))
		{
			Collider collider = hitInfo.collider;
			GameObject gameObject = collider.gameObject;
			GameObject gameObject2 = base.gameObject;
			if (gameObject == gameObject2)
			{
				Vector2 textureCoord = hitInfo.textureCoord;
				PaintStroke(textureCoord);
			}
		}
	}

	private unsafe void PaintStroke(Vector2 uv)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		//IL_00fe: Expected O, but got Ref
		//IL_0109: Expected O, but got I4
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_008b: Invalid comparison between F8 and I4
		//IL_0123: Expected F8, but got I4
		//IL_0179: Invalid comparison between I4 and F8
		//IL_00b3: Expected F8, but got I4
		object obj = this + 144;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj2 = default(object);
		if (obj2 != null)
		{
			object obj3 = this + 144;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CBE10");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			double num2 = Math.Ceiling(0.0);
			bool flag = !(num2 < 1.0);
			double num3 = num2;
			if (!flag)
			{
				num3 = 1.0;
			}
			double num4 = 1.0;
			Vector2 uv2 = default(Vector2);
			do
			{
				double num5 = num4 / num3;
				if (0.0 > num5 || num5 > 1.0)
				{
				}
				PaintAtUV(uv2);
				num4++;
			}
			while (!(num4 > num3));
		}
		else
		{
			PaintAtUV(uv);
		}
		object obj4 = default(object);
		Vector2? vector = (Vector2)(&obj4);
		lastPaintUv = (Vector2?)(object)0;
		_ = 0;
	}

	public unsafe void PaintAtUV(Vector2 uv)
	{
		//IL_002f: Expected F4, but got I4
		//IL_002f: Expected F4, but got I4
		//IL_0045: Expected O, but got Ref
		//IL_0078: Expected O, but got Ref
		//IL_008d: Expected O, but got Ref
		RenderTexture active = RenderTexture.GetActive();
		RenderTexture.SetActive(PaintTexture);
		GL.PushMatrix();
		GL.GLLoadPixelMatrixScript(0f, (float)TextureSize, (float)TextureSize, 0f);
		float num = default(float);
		drawMaterial.color = (Color)(&num);
		drawMaterial.mainTexture = cpuBrush;
		Graphics.DrawTexture((Rect)(&num), cpuBrush, drawMaterial);
		Graphics.DrawTexture((Rect)(&num), cpuBrush, drawMaterial);
		GL.PopMatrix();
		RenderTexture.SetActive(active);
	}

	private unsafe Texture2D CreateCircularBrush(int size)
	{
		//IL_0136: Expected O, but got Ref
		//IL_015b: Expected O, but got I4
		//IL_00b7: Expected O, but got I4
		bool mipChain = default(bool);
		Texture2D texture2D = new Texture2D(size, size, TextureFormat.RGBA32, mipChain);
		if ((object)texture2D != null)
		{
			texture2D.filterMode = FilterMode.Bilinear;
			texture2D.wrapMode = TextureWrapMode.Clamp;
			float num = (float)size * 0.5f;
			float num2 = (float)size * 0.5f;
			float num3 = (float)size * 0.5f;
			if (size > 0)
			{
				int num4 = 0;
				TextureFormat textureFormat = TextureFormat.RGBA32;
				object obj = default(object);
				bool flag2;
				do
				{
					int num5 = 0;
					bool flag;
					do
					{
						float num6 = (float)num5 + 0.5f;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180407E10");
						float num7 = num6 / num3;
						if (!(1f < num7))
						{
						}
						texture2D.SetPixel(num5, num4, (Color)(&obj));
						num5++;
						flag = num5 < size;
						obj = 1065353216;
						textureFormat = (TextureFormat)(int)(&obj);
					}
					while (flag);
					num4++;
					flag2 = num4 < size;
					obj = 1065353216;
					textureFormat = (TextureFormat)(int)(&obj);
				}
				while (flag2);
			}
			texture2D.Apply();
			return texture2D;
		}
		return (Texture2D)(object)new NullReferenceException();
	}

	public unsafe void ClearTexture()
	{
		//IL_002c: Expected O, but got Ref
		RenderTexture active = RenderTexture.GetActive();
		RenderTexture.SetActive(PaintTexture);
		object obj = default(object);
		GL.Clear(clearDepth: true, clearColor: true, (Color)(&obj));
		RenderTexture.SetActive(active);
	}

	public PaintableMesh()
	{
		//IL_0066: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3ABF0]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		TextureSize = 2048;
		MaterialTextureProperty = "_MainTex";
		InteractionRange = 1.8f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C40]");
		BrushColor = (Color)0;
		BrushSize = 0.02f;
		BrushScaleX = 1f;
		BrushScaleY = 1f;
		BrushSoftness = 0.85f;
		base._002Ector();
	}
}
