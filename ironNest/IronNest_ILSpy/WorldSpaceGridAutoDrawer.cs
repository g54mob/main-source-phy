using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldSpaceGridAutoDrawer : MonoBehaviour
{
	public RectTransform targetCanvas;

	public Color gridColor;

	public float lineWidth;

	public int gridSpacing;

	public bool generateLines;

	public bool generateLabels;

	public Color labelColor;

	public int labelFontSize;

	public Vector3 labelOffset;

	public Vector3 labelScale;

	public TextMeshProUGUI labelPrefab;

	public unsafe void GenerateGrid()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0076: Expected I, but got O
		//IL_015a: Expected O, but got I4
		//IL_0172: Invalid comparison between F8 and I4
		//IL_0182: Expected O, but got I4
		//IL_0205: Invalid comparison between F8 and I4
		//IL_01c3: Invalid comparison between F8 and I4
		//IL_01b5: Expected O, but got I4
		//IL_022e: Invalid comparison between F8 and I4
		//IL_01f7: Expected O, but got I4
		//IL_02b5: Expected O, but got Ref
		//IL_02da: Expected O, but got Ref
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Expected O, but got Unknown
		//IL_031f: Invalid comparison between O and F8
		//IL_0264: Invalid comparison between I4 and F8
		//IL_0345: Expected O, but got Ref
		//IL_0353: Expected O, but got Ref
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Expected O, but got Unknown
		//IL_0398: Invalid comparison between O and F8
		//IL_03d0: Expected O, but got Ref
		//IL_0409: Invalid comparison between I4 and F8
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Transform transform = base.transform;
		bool flag = (nint)transform < 0;
		int childCount = transform.childCount;
		int num = childCount - 1;
		if (!flag)
		{
			do
			{
				Transform transform2 = base.transform;
				Transform child = transform2.GetChild(num);
				GameObject obj3 = child.gameObject;
				nint num2 = (nint)typeof(UnityEngine.Object);
				UnityEngine.Object.Destroy(obj3);
				num--;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rcx_v40 (Il2CppClass<UnityEngine.Object>)+E4]");
			}
			while ((nint)0 >= (nint)0);
		}
		if (!(targetCanvas != null) || gridSpacing < 1)
		{
			return;
		}
		Rect rect = targetCanvas.rect;
		Rect rect2 = targetCanvas.rect;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		double num3 = Math.Floor(0.0);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm8\"");
		double num4 = Math.Floor(0.0);
		bool flag2 = !generateLines;
		Vector3 vector = (Vector3)0;
		if (!flag2)
		{
			bool flag3 = num3 < 0.0;
			vector = (Vector3)0;
			Vector3 vector2 = (Vector3)targetCanvas;
			if (!flag3)
			{
				_ = 0;
				_ = 0;
				object obj4 = 0;
				do
				{
					vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
					_ = 0;
					double num5 = (double)obj4 * (double)gridSpacing;
					vector2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-11]");
					_ = 0;
					CreateLine(vector2, vector);
					obj4++;
				}
				while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num3));
			}
			if (!(num4 < 0.0))
			{
				_ = 0;
				_ = 0;
				_ = 0;
				object obj5 = 0;
				do
				{
					vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					Vector3 start = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
					_ = 0;
					_ = 0;
					CreateLine(start, vector);
					obj5++;
				}
				while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4));
			}
		}
		if (!generateLabels)
		{
			return;
		}
		bool flag4 = !(num3 > 0.0);
		int num6 = 0;
		if (flag4)
		{
			return;
		}
		do
		{
			bool flag5 = !(num4 > 0.0);
			int num7 = 0;
			if (!flag5)
			{
				do
				{
					string gridLabel = GetGridLabel(num6, num7);
					vector = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (WorldSpaceGridAutoDrawer)+60]");
					_ = 0;
					CreateLabel(gridLabel, vector);
					num7++;
				}
				while ((double)num7 < num4);
			}
			num6++;
		}
		while ((double)num6 < num3);
	}

	public void ClearGrid()
	{
		//IL_005f: Expected I, but got O
		Transform transform = base.transform;
		bool flag = (nint)transform < 0;
		int childCount = transform.childCount;
		int num = childCount - 1;
		if (!flag)
		{
			do
			{
				Transform transform2 = base.transform;
				Transform child = transform2.GetChild(num);
				GameObject obj = child.gameObject;
				nint num2 = (nint)typeof(UnityEngine.Object);
				UnityEngine.Object.Destroy(obj);
				num--;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rcx_v9 (Il2CppClass<UnityEngine.Object>)+E4]");
			}
			while ((nint)0 >= (nint)0);
		}
	}

	private unsafe void CreateLine(Vector3 start, Vector3 end)
	{
		//IL_0066: Expected O, but got Ref
		//IL_007e: Expected O, but got Ref
		//IL_00e2: Expected O, but got Ref
		//IL_00f4: Expected O, but got Ref
		GameObject gameObject = new GameObject("GridLine");
		Transform transform = gameObject.transform;
		Transform parent = base.transform;
		transform.SetParent(parent, worldPositionStays: false);
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.positionCount = 2;
		float num = default(float);
		lineRenderer.SetPosition(0, (Vector3)(&num));
		lineRenderer.SetPosition(1, (Vector3)(&num));
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;
		Shader shader = Shader.Find("Sprites/Default");
		Material material = new Material(shader);
		((Renderer)lineRenderer).SetMaterial(material);
		lineRenderer.startColor = (Color)(&num);
		Color color = default(Color);
		lineRenderer.endColor = (Color)(&color);
		lineRenderer.useWorldSpace = false;
		lineRenderer.shadowCastingMode = ShadowCastingMode.Off;
		lineRenderer.receiveShadows = false;
	}

	private unsafe void CreateLabel(string label, Vector3 position)
	{
		//IL_007d: Expected F4, but got I4
		//IL_0090: Expected O, but got Ref
		//IL_00b0: Expected O, but got Ref
		//IL_00cf: Expected O, but got Ref
		if (labelPrefab != null)
		{
			TextMeshProUGUI textMeshProUGUI = UnityEngine.Object.Instantiate(labelPrefab, targetCanvas);
			string text = "GridLabel_" + label;
			textMeshProUGUI.name = text;
			textMeshProUGUI.text = label;
			textMeshProUGUI.fontSize = labelFontSize;
			Color color = default(Color);
			textMeshProUGUI.color = (Color)(&color);
			RectTransform rectTransform = textMeshProUGUI.rectTransform;
			rectTransform.localPosition = (Vector3)(&color);
			RectTransform rectTransform2 = textMeshProUGUI.rectTransform;
			float num = default(float);
			rectTransform2.localScale = (Vector3)(&num);
			RectTransform rectTransform3 = textMeshProUGUI.rectTransform;
			Vector2 vector = default(Vector2);
			rectTransform3.anchorMin = vector;
			RectTransform rectTransform4 = textMeshProUGUI.rectTransform;
			rectTransform4.anchorMax = vector;
			RectTransform rectTransform5 = textMeshProUGUI.rectTransform;
			rectTransform5.pivot = vector;
			Transform transform = textMeshProUGUI.transform;
			Transform parent = base.transform;
			transform.SetParent(parent, worldPositionStays: true);
		}
		else
		{
			Debug.LogError("labelPrefab is not assigned! Please assign a TextMeshProUGUI prefab.");
		}
	}

	private string GetGridLabel(int x, int y)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
		int num = default(int);
		string text = num.ToString();
		string text2 = default(string);
		return text2 + text;
	}

	public WorldSpaceGridAutoDrawer()
	{
		//IL_002e: Expected O, but got F4
		//IL_004b: Expected O, but got I
		//IL_0079: Expected I, but got O
		Color gray = Color.gray;
		lineWidth = 0.02f;
		gridSpacing = 1;
		gridColor = (Color)gray.r;
		generateLines = true;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		labelColor = (Color)0;
		labelFontSize = 20;
		Vector3 vector = default(Vector3);
		labelOffset = vector;
		_ = 0;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		labelScale = Vector3.oneVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
		_ = 0;
		base._002Ector();
	}
}
