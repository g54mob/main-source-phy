using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class TextMeshSpawner : MonoBehaviour
{
	public int SpawnType;

	public int NumberOfNPC = 12;

	public Font TheFont;

	private TextMeshProFloatingText floatingText_Script;

	private void Awake()
	{
	}

	private unsafe void Start()
	{
		//IL_01ab: Expected O, but got Ref
		//IL_01e8: Expected O, but got Ref
		//IL_006c: Expected O, but got Ref
		//IL_00f0: Expected O, but got Ref
		if (NumberOfNPC <= 0)
		{
			return;
		}
		int num = 0;
		object obj = default(object);
		Renderer renderer = default(Renderer);
		object obj2 = default(object);
		object obj3 = default(object);
		object obj4 = default(object);
		do
		{
			if (SpawnType != 0)
			{
				GameObject gameObject = new GameObject();
				Transform transform = gameObject.transform;
				float num2 = Random.Range(-95f, 95f);
				float num3 = Random.Range(-95f, 95f);
				transform.position = (Vector3)(&obj);
				TextMesh textMesh = gameObject.AddComponent<TextMesh>();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Material material = TheFont.material;
				renderer.SetMaterial(material);
				textMesh.font = TheFont;
				textMesh.anchor = TextAnchor.LowerCenter;
				textMesh.fontSize = 96;
				textMesh.color = (Color)(&obj2);
				textMesh.text = "!";
				TextMeshProFloatingText textMeshProFloatingText = gameObject.AddComponent<TextMeshProFloatingText>();
				floatingText_Script = textMeshProFloatingText;
				TextMeshProFloatingText textMeshProFloatingText2 = floatingText_Script;
				textMeshProFloatingText2.SpawnType = 1;
				obj2 = obj3;
				obj = obj3;
			}
			else
			{
				GameObject gameObject2 = new GameObject();
				Transform transform2 = gameObject2.transform;
				float num4 = Random.Range(-95f, 95f);
				float num5 = Random.Range(-95f, 95f);
				transform2.position = (Vector3)(&obj4);
				TextMeshPro textMeshPro = gameObject2.AddComponent<TextMeshPro>();
				textMeshPro.fontSize = 96f;
				textMeshPro.text = "!";
				textMeshPro.color = (Color)(&obj2);
				TextMeshProFloatingText textMeshProFloatingText3 = gameObject2.AddComponent<TextMeshProFloatingText>();
				floatingText_Script = textMeshProFloatingText3;
				TextMeshProFloatingText textMeshProFloatingText4 = floatingText_Script;
				textMeshProFloatingText4.SpawnType = 0;
				obj4 = obj3;
				obj2 = obj3;
			}
			num++;
		}
		while (num < NumberOfNPC);
	}
}
