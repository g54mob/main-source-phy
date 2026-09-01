using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class CatCustomization : MonoBehaviour
{
	private SkinnedMeshRenderer eyesMesh;

	private SkinnedMeshRenderer bodyMesh;

	private SkinnedMeshRenderer furMesh;

	private SkinnedMeshRenderer whiskersMesh;

	private List<GameObject> hatObjects;

	public void SetBlendShapeValue(BlendShapeKey key)
	{
		//IL_003f: Expected F4, but got I4
		//IL_0083: Expected F4, but got I4
		//IL_00c7: Expected F4, but got I4
		//IL_010b: Expected F4, but got I4
		if (key.eyes)
		{
			eyesMesh.SetBlendShapeWeight(key.blendShapeIndex, key.value);
		}
		if (key.body)
		{
			bodyMesh.SetBlendShapeWeight(key.blendShapeIndex, key.value);
		}
		if (key.fur)
		{
			furMesh.SetBlendShapeWeight(key.blendShapeIndex, key.value);
		}
		if (key.whiskers)
		{
			whiskersMesh.SetBlendShapeWeight(key.blendShapeIndex, key.value);
		}
	}

	public void ChangeBodyColor(Material bodyMat, Material furMat)
	{
		((Renderer)bodyMesh).SetMaterial(bodyMat);
		((Renderer)furMesh).SetMaterial(furMat);
	}

	public void ChangeEyesColor(Material eyesMat)
	{
		//IL_0092: Expected O, but got I
		SkinnedMeshRenderer skinnedMeshRenderer = eyesMesh;
		bool flag = (object)eyesMesh == null;
		Material material = eyesMat;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9A9C0");
			object obj = default(object);
			bool flag2 = obj == null;
			material = null;
			if (!flag2)
			{
				if ((object)eyesMat != null)
				{
					object obj2 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rdx_v9+40]");
					material = (Material)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj3 = default(object);
					bool flag3 = obj3 == null;
					skinnedMeshRenderer = (SkinnedMeshRenderer)(object)eyesMat;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj4 = default(object);
						throw obj4;
					}
				}
				skinnedMeshRenderer = eyesMesh;
				bool flag4 = (object)eyesMesh == null;
				material = eyesMat;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe void SetHatType(int type)
	{
		//IL_006c: Expected O, but got I4
		//IL_009d: Expected O, but got Ref
		//IL_0115: Expected O, but got I4
		List<GameObject>.Enumerator enumerator = (List<GameObject>.Enumerator)hatObjects;
		if (hatObjects != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<GameObject>.Enumerator enumerator2 = default(List<GameObject>.Enumerator);
			GameObject gameObject = default(GameObject);
			while (enumerator2.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)gameObject != null)
				{
					gameObject.SetActive(value: false);
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator2.Dispose();
			bool flag = type < 0;
			if (type == 0)
			{
				return;
			}
			object obj = type - 1;
			if (!flag)
			{
				List<GameObject> list = hatObjects;
				bool flag2 = hatObjects == null;
				enumerator = (List<GameObject>.Enumerator)(&enumerator2);
				if (flag2)
				{
					goto IL_012e;
				}
				if ((nint)obj <= list._size)
				{
					object obj2 = obj;
					enumerator = (List<GameObject>.Enumerator)hatObjects;
					goto IL_01c5;
				}
			}
			enumerator = (List<GameObject>.Enumerator)hatObjects;
			if (hatObjects != null)
			{
				object obj2 = 0;
				goto IL_01c5;
			}
		}
		goto IL_012e;
		IL_012e:
		throw new NullReferenceException();
		IL_01c5:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		GameObject gameObject2 = default(GameObject);
		if ((object)gameObject2 != null)
		{
			gameObject2.SetActive(value: true);
			return;
		}
		goto IL_012e;
	}
}
