using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro.Examples;

public class Benchmark03 : MonoBehaviour
{
	public enum BenchmarkType
	{
		TMP_SDF_MOBILE,
		TMP_SDF__MOBILE_SSD,
		TMP_SDF,
		TMP_BITMAP_MOBILE,
		TEXTMESH_BITMAP
	}

	public int NumberOfSamples = 100;

	public BenchmarkType Benchmark;

	public Font SourceFont;

	private void Awake()
	{
	}

	private unsafe void Start()
	{
		//IL_0015: Expected O, but got I4
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Expected O, but got Unknown
		//IL_01be: Expected O, but got Ref
		//IL_0312: Expected O, but got Ref
		//IL_01f8: Expected I, but got O
		//IL_0218: Expected I, but got O
		//IL_03c9: Expected O, but got Ref
		bool flag = Benchmark == BenchmarkType.TMP_SDF_MOBILE;
		TMP_FontAsset font2;
		Font sourceFont;
		int atlasWidth = default(int);
		int atlasHeight = default(int);
		AtlasPopulationMode atlasPopulationMode = default(AtlasPopulationMode);
		bool enableMultiAtlasSupport = default(bool);
		GlyphRenderMode renderMode;
		if (!flag)
		{
			object obj = Benchmark - 1;
			if (!flag)
			{
				Font font = (Font)(obj - 1);
				if (!flag)
				{
					bool flag2 = (nint)font != 1;
					font2 = null;
					if (!flag2)
					{
						sourceFont = SourceFont;
						renderMode = GlyphRenderMode.SMOOTH;
						goto IL_0466;
					}
				}
				else
				{
					TMP_FontAsset tMP_FontAsset = TMP_FontAsset.CreateFontAsset(SourceFont, 90, 9, GlyphRenderMode.SDFAA, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
					Shader shader = Shader.Find("TextMeshPro/Distance Field");
					((TMP_Asset)tMP_FontAsset).m_Material.shader = shader;
					font2 = tMP_FontAsset;
					renderMode = GlyphRenderMode.SDFAA;
				}
			}
			else
			{
				TMP_FontAsset tMP_FontAsset2 = TMP_FontAsset.CreateFontAsset(SourceFont, 90, 9, GlyphRenderMode.SDFAA, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
				Shader shader2 = Shader.Find("TextMeshPro/Mobile/Distance Field SSD");
				((TMP_Asset)tMP_FontAsset2).m_Material.shader = shader2;
				font2 = tMP_FontAsset2;
				renderMode = GlyphRenderMode.SDFAA;
			}
			goto IL_0444;
		}
		sourceFont = SourceFont;
		renderMode = GlyphRenderMode.SDFAA;
		goto IL_0466;
		IL_0466:
		TMP_FontAsset tMP_FontAsset3 = TMP_FontAsset.CreateFontAsset(sourceFont, 90, 9, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		font2 = tMP_FontAsset3;
		goto IL_0444;
		IL_0444:
		if (NumberOfSamples <= 0)
		{
			return;
		}
		TMP_FontAsset tMP_FontAsset4 = null;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj4 = default(object);
		object obj6 = default(object);
		Renderer renderer = default(Renderer);
		do
		{
			if (Benchmark <= BenchmarkType.TMP_BITMAP_MOBILE)
			{
				GameObject gameObject = new GameObject();
				Transform transform = gameObject.transform;
				transform.position = (Vector3)(&obj2);
				TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
				textMeshPro.font = font2;
				textMeshPro.fontSize = 128f;
				nint num = (nint)textMeshPro;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v757 @ r8_v17 (Il2CppClass<UnityEngine.Font>)+558] (should have been resolved before IL gen)");
				textMeshPro.alignment = TextAlignmentOptions.Center;
				nint num2 = (nint)textMeshPro;
				float num3 = 16711935f / 255f;
				float num4 = 65280f / 255f;
				float num5 = 255f / 255f;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v766 @ r8_v20 (Il2CppClass<UnityEngine.Font>)+2A8] (should have been resolved before IL gen)");
				bool flag3 = Benchmark != BenchmarkType.TMP_BITMAP_MOBILE;
				obj3 = obj4;
				obj2 = obj4;
				if (!flag3)
				{
					textMeshPro.fontSize = 132f;
					obj3 = obj4;
					object obj5 = obj4;
					num4 = 132f;
					obj2 = obj4;
				}
			}
			else if (Benchmark == BenchmarkType.TEXTMESH_BITMAP)
			{
				GameObject gameObject2 = new GameObject();
				Transform transform2 = gameObject2.transform;
				transform2.position = (Vector3)(&obj6);
				TextMesh textMesh = gameObject2.AddComponent<TextMesh>();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Material material = SourceFont.material;
				renderer.SetMaterial(material);
				textMesh.font = SourceFont;
				textMesh.anchor = TextAnchor.MiddleCenter;
				textMesh.fontSize = 130;
				float num3 = 16711935f / 255f;
				float num4 = 65280f / 255f;
				float num5 = 255f / 255f;
				textMesh.color = (Color)(&obj3);
				textMesh.text = "@";
				obj6 = obj4;
				obj3 = obj4;
				object obj5 = obj4;
			}
			tMP_FontAsset4 = (TMP_FontAsset)(tMP_FontAsset4 + 1);
		}
		while ((nint)tMP_FontAsset4 < NumberOfSamples);
	}
}
