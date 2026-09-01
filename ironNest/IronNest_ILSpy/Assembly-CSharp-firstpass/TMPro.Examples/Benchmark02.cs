using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro.Examples;

public class Benchmark02 : MonoBehaviour
{
	public int SpawnType;

	public int NumberOfNPC = 12;

	public bool IsTextObjectScaleStatic;

	private TextMeshProFloatingText floatingText_Script;

	private unsafe void Start()
	{
		//IL_0008: Expected O, but got Ref
		//IL_04e1: Expected O, but got Ref
		//IL_050d: Expected I, but got O
		//IL_02ef: Expected O, but got Ref
		//IL_0571: Expected O, but got I
		//IL_0579: Expected I, but got O
		//IL_0599: Expected O, but got I
		//IL_05af: Expected O, but got I
		//IL_05c5: Expected O, but got I
		//IL_0607: Expected I, but got O
		//IL_00b2: Expected O, but got Ref
		//IL_0102: Expected O, but got Ref
		//IL_03b4: Expected O, but got I
		//IL_03ca: Expected O, but got I
		//IL_03e0: Expected O, but got I
		//IL_041d: Expected O, but got Ref
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Expected O, but got Unknown
		//IL_0173: Expected I, but got O
		//IL_0193: Expected O, but got I
		//IL_01a9: Expected O, but got I
		//IL_01bf: Expected O, but got I
		//IL_0222: Expected I, but got O
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (NumberOfNPC <= 0)
		{
			return;
		}
		int num = 0;
		Vector2 vector = default(Vector2);
		Vector2 vector2 = default(Vector2);
		Vector2 vector3 = default(Vector2);
		Renderer renderer = default(Renderer);
		do
		{
			if (SpawnType != 0)
			{
				if (SpawnType != 1)
				{
					if (SpawnType == 2)
					{
						GameObject gameObject = new GameObject();
						Canvas canvas = gameObject.AddComponent<Canvas>();
						Camera main = Camera.main;
						canvas.worldCamera = main;
						Transform transform = gameObject.transform;
						transform.localScale = (Vector3)(&vector);
						Transform transform2 = gameObject.transform;
						float num2 = Random.Range(-95f, 95f);
						float num3 = Random.Range(-95f, 95f);
						Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
						transform2.position = position;
						GameObject gameObject2 = new GameObject();
						TextMeshProUGUI textMeshProUGUI = gameObject2.AddComponent<TextMeshProUGUI>();
						RectTransform rectTransform = textMeshProUGUI.rectTransform;
						Transform parent = gameObject.transform;
						rectTransform.SetParent(parent, worldPositionStays: false);
						nint num4 = (nint)textMeshProUGUI;
						_ = 4278255615L;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+90]");
						object obj3 = (nint)0 >> 8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+90]");
						object obj4 = (nint)0 >> 16;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+90]");
						object obj5 = (nint)0 >> 24;
						float num5 = (float)obj3 / 255f;
						float num6 = (float)obj4 / 255f;
						float num7 = (float)obj5 / 255f;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v969 @ r8_v34 (Il2CppClass<UnityEngine.GameObject>)+2A8] (should have been resolved before IL gen)");
						textMeshProUGUI.alignment = TextAlignmentOptions.Bottom;
						textMeshProUGUI.fontSize = 96f;
						nint num8 = (nint)textMeshProUGUI;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1015 @ r8_v38 (Il2CppClass<UnityEngine.GameObject>)+558] (should have been resolved before IL gen)");
						TextMeshProFloatingText textMeshProFloatingText = gameObject.AddComponent<TextMeshProFloatingText>();
						Benchmark02 benchmark = (Benchmark02)(this + 48);
						floatingText_Script = textMeshProFloatingText;
						TextMeshProFloatingText textMeshProFloatingText2 = floatingText_Script;
						textMeshProFloatingText2.SpawnType = 0;
						vector2 = vector3;
						float num9 = 96f;
						vector = vector3;
					}
				}
				else
				{
					GameObject gameObject3 = new GameObject();
					Transform transform3 = gameObject3.transform;
					float num10 = Random.Range(-95f, 95f);
					float num11 = Random.Range(-95f, 95f);
					Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
					transform3.position = position2;
					TextMesh textMesh = gameObject3.AddComponent<TextMesh>();
					Font font = Resources.Load<Font>("Fonts/ARIAL");
					textMesh.font = font;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					Font font2 = textMesh.font;
					Material material = font2.material;
					renderer.SetMaterial(material);
					textMesh.anchor = TextAnchor.LowerCenter;
					textMesh.fontSize = 96;
					_ = 4278255615L;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+A0]");
					object obj6 = (nint)0 >> 8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+A0]");
					object obj7 = (nint)0 >> 16;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+A0]");
					object obj8 = (nint)0 >> 24;
					float num5 = (float)obj6 / 255f;
					float num9 = (float)obj7 / 255f;
					float num7 = (float)obj8 / 255f;
					textMesh.color = (Color)(&vector2);
					textMesh.text = "!";
					TextMeshProFloatingText textMeshProFloatingText3 = gameObject3.AddComponent<TextMeshProFloatingText>();
					Benchmark02 benchmark = (Benchmark02)(this + 48);
					floatingText_Script = textMeshProFloatingText3;
					TextMeshProFloatingText textMeshProFloatingText4 = floatingText_Script;
					textMeshProFloatingText4.SpawnType = 1;
					vector2 = vector3;
				}
			}
			else
			{
				GameObject gameObject4 = new GameObject();
				Transform transform4 = gameObject4.transform;
				float num12 = Random.Range(-95f, 95f);
				float num13 = Random.Range(-95f, 95f);
				Vector3 position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 96));
				transform4.position = position3;
				TextMeshPro textMeshPro = gameObject4.AddComponent<TextMeshPro>();
				nint num14 = (nint)textMeshPro;
				textMeshPro.autoSizeTextContainer = true;
				RectTransform rectTransform2 = textMeshPro.rectTransform;
				rectTransform2.pivot = vector3;
				textMeshPro.alignment = TextAlignmentOptions.Bottom;
				textMeshPro.fontSize = 96f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v603 @ rax_v11 (TMPro.TextMeshPro)+340]");
				((List<OTL_FeatureTag>)0).Clear();
				nint num15 = (nint)textMeshPro;
				_ = 4278255615L;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+A8]");
				object obj9 = (nint)0 >> 8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+A8]");
				object obj10 = (nint)0 >> 16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+A8]");
				object obj11 = (nint)0 >> 24;
				float num5 = (float)obj9 / 255f;
				float num9 = (float)obj10 / 255f;
				float num7 = (float)obj11 / 255f;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v858 @ r8_v13 (Il2CppClass<UnityEngine.GameObject>)+2A8] (should have been resolved before IL gen)");
				nint num16 = (nint)textMeshPro;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v902 @ r8_v15 (Il2CppClass<UnityEngine.GameObject>)+558] (should have been resolved before IL gen)");
				textMeshPro.isTextObjectScaleStatic = IsTextObjectScaleStatic;
				TextMeshProFloatingText textMeshProFloatingText5 = gameObject4.AddComponent<TextMeshProFloatingText>();
				floatingText_Script = textMeshProFloatingText5;
				TextMeshProFloatingText textMeshProFloatingText6 = floatingText_Script;
				textMeshProFloatingText6.SpawnType = 0;
				Benchmark02 benchmark = (Benchmark02)(object)floatingText_Script;
				_ = IsTextObjectScaleStatic;
				vector2 = vector3;
			}
			num++;
		}
		while (num < NumberOfNPC);
	}
}
