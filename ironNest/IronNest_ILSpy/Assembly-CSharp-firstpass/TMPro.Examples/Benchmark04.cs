using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class Benchmark04 : MonoBehaviour
{
	public int SpawnType;

	public int MinPointSize = 12;

	public int MaxPointSize = 64;

	public int Steps = 4;

	private Transform m_Transform;

	private unsafe void Start()
	{
		//IL_003d: Expected O, but got I4
		//IL_004b: Expected F4, but got O
		//IL_00b0: Expected O, but got I4
		//IL_0109: Invalid comparison between O and F4
		//IL_0180: Expected O, but got Ref
		//IL_01f2: Expected F4, but got I4
		//IL_021e: Expected I, but got O
		//IL_0230: Expected I, but got O
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		Transform transform = base.transform;
		m_Transform = transform;
		Camera main = Camera.main;
		int height = Screen.height;
		int num = height >> 31;
		object obj = height - num;
		float num2 = (main.orthographicSize = obj >> 1);
		int width = Screen.width;
		int height2 = Screen.height;
		int num4 = MinPointSize;
		if (MinPointSize > MaxPointSize)
		{
			return;
		}
		float num5 = num2;
		object obj2 = 0;
		int num6 = default(int);
		Vector2 vector = default(Vector2);
		Vector2 vector2 = default(Vector2);
		float num15 = default(float);
		do
		{
			if (SpawnType == 0)
			{
				string text = num6.ToString();
				string text2 = "Text - " + text + " Pts";
				GameObject gameObject = new GameObject(text2);
				float num7 = num2 + num2;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num7))
				{
					break;
				}
				Transform transform2 = gameObject.transform;
				Vector3 position = m_Transform.position;
				float num8 = num2 * 0.975f;
				float num9 = num8 - (float)obj2;
				float num10 = num9 + (float)vector;
				transform2.position = (Vector3)(&vector2);
				TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
				RectTransform rectTransform = textMeshPro.rectTransform;
				rectTransform.pivot = vector;
				textMeshPro.textWrappingMode = TextWrappingModes.NoWrap;
				textMeshPro.extraPadding = true;
				textMeshPro.isOrthographic = true;
				textMeshPro.fontSize = num6;
				string text3 = num6.ToString();
				string text4 = text3 + " pts - Lorem ipsum dolor sit...";
				nint num11 = (nint)textMeshPro;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v529 @ r8_v16 (Il2CppClass<UnityEngine.Camera>)+558] (should have been resolved before IL gen)");
				nint num12 = (nint)textMeshPro;
				float num13 = 16777215f / 255f;
				num5 = 65535f / 255f;
				float num14 = 255f / 255f;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v534 @ r8_v18 (Il2CppClass<UnityEngine.Camera>)+2A8] (should have been resolved before IL gen)");
				obj2 += num6;
				vector2 = vector;
				num = (int)(&num15);
				num4 = num6;
			}
			num4 += Steps;
		}
		while (num4 <= MaxPointSize);
	}
}
