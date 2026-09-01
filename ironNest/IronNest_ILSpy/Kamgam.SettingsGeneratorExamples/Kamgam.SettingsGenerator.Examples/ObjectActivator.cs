using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class ObjectActivator : MonoBehaviour
{
	public GameObject[] Objects;

	public void Activate(int numOfObjectsToActive)
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_002a: Expected O, but got I4
		//IL_0055: Expected O, but got I
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected I4, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected I4, but got Unknown
		//IL_00ee: Expected O, but got I
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		if (Objects == null)
		{
			return;
		}
		GameObject[] objects = Objects;
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		while ((nint)obj3 < objects.Length)
		{
			GameObject[] objects2 = Objects;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rdi_v3+v167 @ rax_v5 (UnityEngine.GameObject[])]");
			if ((Object)0 != null)
			{
				GameObject[] objects3 = Objects;
				object obj4 = obj2 - numOfObjectsToActive;
				int num = obj2 ^ numOfObjectsToActive;
				object obj5 = obj2 ^ obj4;
				int num2 = num & obj5;
				bool flag = num2 < 0;
				bool flag2 = (nint)obj4 < 0;
				bool active = flag2 != flag;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rdi_v3+v197 @ rax_v14 (UnityEngine.GameObject[])]");
				((GameObject)0).SetActive(active);
			}
			objects = Objects;
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}
}
