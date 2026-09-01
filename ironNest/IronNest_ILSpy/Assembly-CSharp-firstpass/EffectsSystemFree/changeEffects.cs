using System;
using Cpp2ILInjected;
using UnityEngine;

namespace EffectsSystemFree;

public class changeEffects : MonoBehaviour
{
	public GameObject[] effects;

	private GameObject currentObject;

	private int currentObjectID;

	public GameObject guiTextLink;

	private void Start()
	{
		//IL_004e: Expected O, but got I
		//IL_010c: Expected O, but got I
		//IL_0148: Expected O, but got I
		//IL_017f: Expected I, but got O
		UnityEngine.Object obj = (UnityEngine.Object)(object)effects;
		if (effects != null)
		{
			int num = currentObjectID;
			int num2 = currentObjectID;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rcx_v3 (UnityEngine.Object)+18]");
			if ((nint)num2 >= (nint)0)
			{
				goto IL_022e;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rcx_v3 (UnityEngine.Object)+20+v42 @ rax_v11 (System.Int32)*8]");
			GameObject gameObject = UnityEngine.Object.Instantiate((GameObject)0);
			currentObject = gameObject;
			obj = guiTextLink;
			if ((object)guiTextLink != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				obj = (UnityEngine.Object)(object)effects;
				bool flag = effects == null;
				nint num3 = 0;
				if (!flag)
				{
					int num4 = currentObjectID;
					int num5 = currentObjectID;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rcx_v3 (UnityEngine.Object)+18]");
					bool flag2 = (nint)num5 >= (nint)0;
					num3 = 0;
					if (flag2)
					{
						goto IL_022e;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rcx_v3 (UnityEngine.Object)+20+v72 @ rax_v16 (System.Int32)*8]");
					obj = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rcx_v3 (UnityEngine.Object)+20+v72 @ rax_v16 (System.Int32)*8]");
					bool flag3 = (nint)0 == 0;
					num3 = 0;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rcx_v3 (UnityEngine.Object)+20+v72 @ rax_v16 (System.Int32)*8]");
						string text = ((UnityEngine.Object)0).name;
						UnityEngine.Object obj2 = default(UnityEngine.Object);
						bool flag4 = (object)obj2 == null;
						num3 = 0;
						obj = obj2;
						if (!flag4)
						{
							nint num6 = (nint)obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v262 @ r8_v5 (Il2CppClass<UnityEngine.Object>)+5E8] (should have been resolved before IL gen)");
							return;
						}
					}
				}
			}
		}
		throw new NullReferenceException();
		IL_022e:
		throw new IndexOutOfRangeException();
	}

	private void FixedUpdate()
	{
		//IL_003c: Expected O, but got I4
		//IL_0178: Expected O, but got I4
		//IL_00a6: Expected O, but got I4
		object obj4 = default(object);
		if (Input.GetKeyDownInt(KeyCode.RightArrow))
		{
			GameObject[] array = effects;
			object obj = array.Length - 1;
			if (currentObjectID < (nint)obj)
			{
				UnityEngine.Object.Destroy(currentObject);
				GameObject[] array2 = effects;
				int num = currentObjectID + 1;
				currentObjectID = num;
				object obj2 = currentObjectID + 1;
				GameObject gameObject = UnityEngine.Object.Instantiate(array2[obj2]);
				currentObject = gameObject;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				GameObject[] array3 = effects;
				int num2 = currentObjectID;
				string text = array3[num2].name;
				object obj3 = obj4;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v438 @ r8_v9+5E8] (should have been resolved before IL gen)");
			}
		}
		if (Input.GetKeyDownInt(KeyCode.LeftArrow) && currentObjectID > 0)
		{
			UnityEngine.Object.Destroy(currentObject);
			GameObject[] array4 = effects;
			int num3 = currentObjectID - 1;
			currentObjectID = num3;
			object obj5 = currentObjectID - 1;
			GameObject gameObject2 = UnityEngine.Object.Instantiate(array4[obj5]);
			currentObject = gameObject2;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			GameObject[] array5 = effects;
			int num4 = currentObjectID;
			string text2 = array5[num4].name;
			object obj6 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v439 @ r8_v5+5E8] (should have been resolved before IL gen)");
		}
	}

	public void NextEffect()
	{
		//IL_0015: Expected O, but got I4
		//IL_007f: Expected O, but got I4
		GameObject[] array = effects;
		object obj = array.Length - 1;
		if (currentObjectID < (nint)obj)
		{
			UnityEngine.Object.Destroy(currentObject);
			GameObject[] array2 = effects;
			int num = currentObjectID + 1;
			currentObjectID = num;
			object obj2 = currentObjectID + 1;
			GameObject gameObject = UnityEngine.Object.Instantiate(array2[obj2]);
			currentObject = gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			GameObject[] array3 = effects;
			int num2 = currentObjectID;
			string text = array3[num2].name;
			object obj4 = default(object);
			object obj3 = obj4;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v252 @ r8_v5+5E8] (should have been resolved before IL gen)");
		}
	}

	public void PrevEffect()
	{
		//IL_004e: Expected O, but got I4
		if (currentObjectID > 0)
		{
			UnityEngine.Object.Destroy(currentObject);
			GameObject[] array = effects;
			int num = currentObjectID - 1;
			currentObjectID = num;
			object obj = currentObjectID - 1;
			GameObject gameObject = UnityEngine.Object.Instantiate(array[obj]);
			currentObject = gameObject;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			GameObject[] array2 = effects;
			int num2 = currentObjectID;
			string text = array2[num2].name;
			object obj3 = default(object);
			object obj2 = obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v237 @ r8_v5+5E8] (should have been resolved before IL gen)");
		}
	}

	public void RefreshEffect()
	{
		UnityEngine.Object.Destroy(currentObject);
		GameObject[] array = effects;
		int num = currentObjectID;
		GameObject gameObject = UnityEngine.Object.Instantiate(array[num]);
		currentObject = gameObject;
	}
}
