using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class Outline : MonoBehaviour
{
	public enum Mode
	{
		OutlineAll,
		OutlineVisible,
		OutlineHidden,
		OutlineAndSilhouette,
		SilhouetteOnly
	}

	[Serializable]
	private class ListVector3
	{
		public List<Vector3> data;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Vector3, int, KeyValuePair<Vector3, int>> _003C_003E9__34_0;

		public static Func<KeyValuePair<Vector3, int>, Vector3> _003C_003E9__34_1;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe KeyValuePair<Vector3, int> _003CSmoothNormals_003Eb__34_0(Vector3 vertex, int index)
		{
			//IL_000e: Expected O, but got I4
			//IL_001c: Expected O, but got Ref
			//IL_0017: Expected native int or pointer, but got O
			_003C_003Ec obj = (_003C_003Ec)0;
			object obj2 = default(object);
			object obj3 = default(object);
			*(KeyValuePair<Vector3, int>*)(nint)this = new KeyValuePair<Vector3, int>((Vector3)(&obj2), (int)(&obj3));
			return (KeyValuePair<Vector3, int>)this;
		}

		internal unsafe Vector3 _003CSmoothNormals_003Eb__34_1(KeyValuePair<Vector3, int> pair)
		{
			//IL_0008: Expected O, but got Ref
			//IL_0038: Expected O, but got I
			//IL_008d: Expected F4, but got O
			//IL_0088: Expected native int or pointer, but got O
			//IL_00a2: Expected F4, but got I
			//IL_009d: Expected native int or pointer, but got O
			object obj2 = default(object);
			object obj = (object)(&obj2);
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v3 (Il2CppClass<UnityEngine.Vector3>)+FC]");
			object obj3 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v3 (Il2CppClass<UnityEngine.Vector3>)+FC]");
			if ((nint)obj3 > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			}
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+8]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
	}

	private static HashSet<Mesh> registeredMeshes;

	private Mode outlineMode;

	private Color outlineColor;

	private float outlineWidth;

	private bool precomputeOutline;

	private bool useDistanceCheck;

	public float distance;

	private List<Mesh> bakeKeys;

	private List<ListVector3> bakeValues;

	private MeshRenderer[] renderers;

	private Material outlineMaskMaterial;

	private Material outlineFillMaterial;

	private Transform player;

	private bool isEnabled;

	private bool needsUpdate;

	public Mode OutlineMode
	{
		get
		{
			return outlineMode;
		}
		set
		{
			outlineMode = value;
			needsUpdate = true;
		}
	}

	public unsafe Color OutlineColor
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Color color = default(Color);
			((Color*)(nint)color)->r = (float)outlineColor;
			return color;
		}
		set
		{
			//IL_001a: Expected O, but got F4
			needsUpdate = true;
			outlineColor = (Color)value.r;
		}
	}

	public float OutlineWidth
	{
		get
		{
			return outlineWidth;
		}
		set
		{
			outlineWidth = value;
			needsUpdate = true;
		}
	}

	private unsafe void Awake()
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_00f2: Expected O, but got I4
		//IL_00fb: Expected O, but got I4
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0155: Expected O, but got Ref
		MeshRenderer[] componentsInChildren = GetComponentsInChildren<MeshRenderer>();
		renderers = componentsInChildren;
		Material original = Resources.Load<Material>("Materials/OutlineMask");
		Material material = UnityEngine.Object.Instantiate(original);
		outlineMaskMaterial = material;
		Material original2 = Resources.Load<Material>("Materials/OutlineFill");
		Material material2 = UnityEngine.Object.Instantiate(original2);
		outlineFillMaterial = material2;
		outlineMaskMaterial.name = "OutlineMask (Instance)";
		outlineFillMaterial.name = "OutlineFill (Instance)";
		LoadSmoothNormals();
		needsUpdate = true;
		GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
		Transform transform = gameObject.transform;
		player = transform;
		MeshRenderer[] array = renderers;
		isEnabled = true;
		object obj = renderers + 32;
		object obj2 = 0;
		object obj3 = 0;
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		Vector3 vector = default(Vector3);
		while ((nint)obj2 < array.Length)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			if (obj4 != null)
			{
				Mesh sharedMesh = ((MeshFilter)obj4).sharedMesh;
				sharedMesh.bounds = (Bounds)(&vector);
			}
			obj3++;
			obj += 8;
			obj2 = obj3;
		}
	}

	private void OnEnable()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001e: Expected O, but got I4
		//IL_0027: Expected O, but got I4
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		MeshRenderer[] array = renderers;
		object obj = renderers + 32;
		object obj2 = 0;
		object obj3 = 0;
		IEnumerable<Material> source = default(IEnumerable<Material>);
		while ((nint)obj3 < array.Length)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
			List<Material> list = Enumerable.ToList(source);
			list.Add(outlineMaskMaterial);
			list.Add(outlineFillMaterial);
			Material[] array2 = list.ToArray();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		isEnabled = true;
	}

	private void OnValidate()
	{
		needsUpdate = true;
		if (!precomputeOutline)
		{
			List<Mesh> list = bakeKeys;
			if (list._size != 0)
			{
				goto IL_007d;
			}
		}
		List<Mesh> list2 = bakeKeys;
		List<ListVector3> list3 = bakeValues;
		if (list2._size != list3._size)
		{
			goto IL_007d;
		}
		goto IL_01ea;
		IL_01ea:
		if (precomputeOutline)
		{
			List<Mesh> list4 = bakeKeys;
			if (list4._size == 0)
			{
				Bake();
			}
		}
		return;
		IL_007d:
		List<Mesh> list5 = bakeKeys;
		int version = list5._version + 1;
		list5._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			list5._size = 0;
		}
		else
		{
			list5._size = 0;
			if (list5._size > 0)
			{
				Array.Clear(list5._items, 0, list5._size);
			}
		}
		List<ListVector3> list6 = bakeValues;
		int version2 = list6._version + 1;
		list6._version = version2;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<ListVector3>())
		{
			list6._size = 0;
		}
		else
		{
			list6._size = 0;
			if (list6._size > 0)
			{
				Array.Clear(list6._items, 0, list6._size);
			}
		}
		goto IL_01ea;
	}

	private void Update()
	{
		//IL_04b4: Expected I, but got O
		//IL_0432: Expected I, but got O
		//IL_032d: Invalid comparison between F4 and I4
		//IL_0123: Expected F8, but got I4
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Expected O, but got Unknown
		//IL_0362: Expected O, but got I4
		//IL_036b: Expected O, but got I4
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_0183: Expected O, but got I4
		//IL_018c: Expected O, but got I4
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		if (needsUpdate)
		{
			needsUpdate = false;
			UpdateMaterialProperties();
		}
		if (!(player != null))
		{
			return;
		}
		object obj2 = default(object);
		object obj3 = default(object);
		if (isEnabled)
		{
			Vector3 position = player.position;
			Transform transform = base.transform;
			Vector3 position2 = transform.position;
			nint num = (nint)typeof(Math);
			float num2 = position.x - position2.x;
			object obj = obj2 - obj3;
			float num3 = position.z - position2.z;
			object obj4 = obj * obj;
			float num4 = num2 * num2;
			float num5 = num3 * num3;
			float num6 = (float)obj4 + num4;
			float num7 = num6 + num5;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rcx_v31 (Il2CppClass<System.Math>)+E4]");
			double num8;
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
				num8 = 0.0;
			}
			else
			{
				num8 = Math.Sqrt(num7);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
			if (num8 > (double)distance)
			{
				MeshRenderer[] array = renderers;
				object obj5 = renderers + 32;
				object obj6 = 0;
				object obj7 = 0;
				IEnumerable<Material> source = default(IEnumerable<Material>);
				while ((nint)obj7 < array.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
					List<Material> list = Enumerable.ToList(source);
					bool flag = list.Remove(outlineMaskMaterial);
					bool flag2 = list.Remove(outlineFillMaterial);
					Material[] array2 = list.ToArray();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
					obj6++;
					obj5 += 8;
					obj7 = obj6;
				}
				isEnabled = false;
				return;
			}
			if (isEnabled)
			{
				return;
			}
		}
		Vector3 position3 = player.position;
		Transform transform2 = base.transform;
		Vector3 position4 = transform2.position;
		nint num9 = (nint)typeof(Math);
		float num10 = position3.x - position4.x;
		object obj8 = obj3 - obj2;
		float num11 = position3.z - position4.z;
		object obj9 = obj8 * obj8;
		float num12 = num10 * num10;
		float num13 = num11 * num11;
		float num14 = (float)obj9 + num12;
		float num15 = num14 + num13;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v551 @ rcx_v14 (Il2CppClass<System.Math>)+E4]");
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
		}
		else
		{
			double num16 = Math.Sqrt(num15);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm0\"");
		if (distance > 0f)
		{
			MeshRenderer[] array3 = renderers;
			object obj10 = renderers + 32;
			object obj11 = 0;
			object obj12 = 0;
			IEnumerable<Material> source2 = default(IEnumerable<Material>);
			while ((nint)obj12 < array3.Length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
				List<Material> list2 = Enumerable.ToList(source2);
				list2.Add(outlineMaskMaterial);
				list2.Add(outlineFillMaterial);
				Material[] array4 = list2.ToArray();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
				obj11++;
				obj10 += 8;
				obj12 = obj11;
			}
			isEnabled = true;
		}
	}

	private void OnDisable()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_001e: Expected O, but got I4
		//IL_0027: Expected O, but got I4
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		MeshRenderer[] array = renderers;
		object obj = renderers + 32;
		object obj2 = 0;
		object obj3 = 0;
		IEnumerable<Material> source = default(IEnumerable<Material>);
		while ((nint)obj3 < array.Length)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
			List<Material> list = Enumerable.ToList(source);
			bool flag = list.Remove(outlineMaskMaterial);
			bool flag2 = list.Remove(outlineFillMaterial);
			Material[] array2 = list.ToArray();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9C660");
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		isEnabled = false;
	}

	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(outlineMaskMaterial);
		UnityEngine.Object.Destroy(outlineFillMaterial);
	}

	private void Bake()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0025: Expected O, but got I4
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		HashSet<Mesh> hashSet = new HashSet<Mesh>();
		MeshFilter[] componentsInChildren = GetComponentsInChildren<MeshFilter>();
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		object obj3 = 0;
		object obj4 = default(object);
		while ((nint)obj3 < componentsInChildren.Length)
		{
			Mesh sharedMesh = ((MeshFilter)obj).sharedMesh;
			hashSet.Add(sharedMesh);
			if (obj4 != null)
			{
				Mesh sharedMesh2 = ((MeshFilter)obj).sharedMesh;
				List<Vector3> data = SmoothNormals(sharedMesh2);
				Mesh sharedMesh3 = ((MeshFilter)obj).sharedMesh;
				bakeKeys.Add(sharedMesh3);
				ListVector3 listVector = new ListVector3();
				listVector.data = data;
				bakeValues.Add(listVector);
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
	}

	private void LoadSmoothNormals()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0025: Expected O, but got I4
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0291: Expected O, but got I4
		//IL_029a: Expected O, but got I4
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Expected O, but got Unknown
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Expected O, but got Unknown
		//IL_00ce: Expected O, but got I
		MeshFilter[] componentsInChildren = GetComponentsInChildren<MeshFilter>();
		object obj = componentsInChildren + 32;
		object obj2 = 0;
		object obj3 = 0;
		object obj4 = default(object);
		UnityEngine.Object obj5 = default(UnityEngine.Object);
		while ((nint)obj3 < componentsInChildren.Length)
		{
			Mesh sharedMesh = ((MeshFilter)obj).sharedMesh;
			registeredMeshes.Add(sharedMesh);
			if (obj4 != null)
			{
				Mesh sharedMesh2 = ((MeshFilter)obj).sharedMesh;
				int num = bakeKeys.IndexOf(sharedMesh2);
				List<Vector3> uvs;
				if (num >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ stack_18+10]");
					uvs = (List<Vector3>)0;
				}
				else
				{
					Mesh sharedMesh3 = ((MeshFilter)obj).sharedMesh;
					List<Vector3> list = SmoothNormals(sharedMesh3);
					uvs = list;
				}
				Mesh sharedMesh4 = ((MeshFilter)obj).sharedMesh;
				sharedMesh4.SetUVs(3, uvs);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				if (obj5 != null)
				{
					Mesh sharedMesh5 = ((MeshFilter)obj).sharedMesh;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
					int subMeshCount = sharedMesh5.subMeshCount;
					if (subMeshCount != 1)
					{
						int subMeshCount2 = sharedMesh5.subMeshCount;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v47+18]");
						if ((nint)subMeshCount2 <= (nint)0)
						{
							int subMeshCount3 = sharedMesh5.subMeshCount;
							int subMeshCount4 = subMeshCount3 + 1;
							sharedMesh5.subMeshCount = subMeshCount4;
							int[] triangles = sharedMesh5.triangles;
							int subMeshCount5 = sharedMesh5.subMeshCount;
							int submesh = subMeshCount5 - 1;
							sharedMesh5.SetTriangles(triangles, submesh);
						}
					}
				}
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		SkinnedMeshRenderer[] componentsInChildren2 = GetComponentsInChildren<SkinnedMeshRenderer>();
		object obj6 = componentsInChildren2 + 32;
		object obj7 = 0;
		object obj8 = 0;
		object obj9 = default(object);
		while ((nint)obj8 < componentsInChildren2.Length)
		{
			Mesh sharedMesh6 = ((SkinnedMeshRenderer)obj6).sharedMesh;
			registeredMeshes.Add(sharedMesh6);
			if (obj9 != null)
			{
				Mesh sharedMesh7 = ((SkinnedMeshRenderer)obj6).sharedMesh;
				Mesh sharedMesh8 = ((SkinnedMeshRenderer)obj6).sharedMesh;
				int vertexCount = sharedMesh8.vertexCount;
				Vector2[] uv = new Vector2[vertexCount];
				sharedMesh7.uv4 = uv;
				Mesh sharedMesh9 = ((SkinnedMeshRenderer)obj6).sharedMesh;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D9AD10");
				int subMeshCount6 = sharedMesh9.subMeshCount;
				if (subMeshCount6 != 1)
				{
					int subMeshCount7 = sharedMesh9.subMeshCount;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v155 @ rax_v22+18]");
					if ((nint)subMeshCount7 <= (nint)0)
					{
						int subMeshCount8 = sharedMesh9.subMeshCount;
						int subMeshCount9 = subMeshCount8 + 1;
						sharedMesh9.subMeshCount = subMeshCount9;
						int[] triangles2 = sharedMesh9.triangles;
						int subMeshCount10 = sharedMesh9.subMeshCount;
						int submesh2 = subMeshCount10 - 1;
						sharedMesh9.SetTriangles(triangles2, submesh2);
					}
				}
			}
			obj7++;
			obj6 += 8;
			obj8 = obj7;
		}
	}

	private unsafe List<Vector3> SmoothNormals(Mesh mesh)
	{
		//IL_00bb: Expected I4, but got O
		//IL_061e: Expected O, but got I4
		//IL_0627: Expected O, but got I4
		//IL_0630: Expected O, but got I4
		//IL_03bb: Expected O, but got Ref
		//IL_0110: Expected O, but got I4
		//IL_03eb: Expected I4, but got O
		//IL_017c: Expected I4, but got O
		//IL_0188: Expected O, but got Ref
		//IL_0192: Expected O, but got I4
		//IL_01e3: Expected O, but got I4
		//IL_05ac: Invalid comparison between O and F4
		//IL_0282: Expected I4, but got O
		//IL_0295: Expected I4, but got O
		//IL_05d3: Expected I4, but got O
		//IL_05df: Expected O, but got Ref
		//IL_05e8: Expected O, but got I4
		//IL_022c: Expected O, but got Ref
		//IL_02fd: Expected O, but got I4
		//IL_03a4: Expected I4, but got O
		//IL_033e: Expected O, but got Ref
		//IL_035d: Expected O, but got Ref
		if ((object)mesh != null)
		{
			Vector3[] vertices = mesh.vertices;
			Func<Vector3, int, KeyValuePair<Vector3, int>> selector = _003C_003Ec._003C_003E9__34_0;
			if (_003C_003Ec._003C_003E9__34_0 == null)
			{
				selector = (_003C_003Ec._003C_003E9__34_0 = delegate
				{
					//IL_000e: Expected O, but got I4
					//IL_001c: Expected O, but got Ref
					//IL_0017: Expected native int or pointer, but got O
					_003C_003Ec obj7 = (_003C_003Ec)0;
					object obj8 = default(object);
					object obj9 = default(object);
					*(KeyValuePair<Vector3, int>*)(nint)_003C_003Ec._003C_003E9 = new KeyValuePair<Vector3, int>((Vector3)(&obj8), (int)(&obj9));
					return (KeyValuePair<Vector3, int>)_003C_003Ec._003C_003E9;
				});
			}
			IEnumerable<KeyValuePair<Vector3, int>> source = Enumerable.Select(vertices, selector);
			Func<KeyValuePair<Vector3, int>, Vector3> keySelector = _003C_003Ec._003C_003E9__34_1;
			if (_003C_003Ec._003C_003E9__34_1 == null)
			{
				keySelector = (_003C_003Ec._003C_003E9__34_1 = delegate
				{
					//IL_0008: Expected O, but got Ref
					//IL_0038: Expected O, but got I
					//IL_008d: Expected F4, but got O
					//IL_0088: Expected native int or pointer, but got O
					//IL_00a2: Expected F4, but got I
					//IL_009d: Expected native int or pointer, but got O
					object obj8 = default(object);
					object obj7 = (object)(&obj8);
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v3 (Il2CppClass<UnityEngine.Vector3>)+FC]");
					object obj9 = (nint)0 + (nint)15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v3 (Il2CppClass<UnityEngine.Vector3>)+FC]");
					if ((nint)obj9 > 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
					}
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					Vector3 vector11 = default(Vector3);
					((Vector3*)(nint)vector11)->x = (float)obj7;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+8]");
					((Vector3*)(nint)vector11)->z = 0f;
					return vector11;
				});
			}
			IEnumerable<IGrouping<Vector3, KeyValuePair<Vector3, int>>> enumerable = Enumerable.GroupBy(source, keySelector);
			Vector3[] normals = mesh.normals;
			List<Vector3> list = new List<Vector3>(normals);
			if (enumerable != null)
			{
				Vector3 vector = ((List<Vector3>)(object)typeof(IEnumerable<IGrouping<Vector3, KeyValuePair<Vector3, int>>>)).get_Item((int)enumerable);
				KeyValuePair<Vector3, int> keyValuePair = (KeyValuePair<Vector3, int>)0;
				KeyValuePair<Vector3, int> keyValuePair2 = (KeyValuePair<Vector3, int>)0;
				KeyValuePair<Vector3, int> keyValuePair3 = (KeyValuePair<Vector3, int>)0;
				int num = default(int);
				int num3 = default(int);
				object obj2 = default(object);
				object obj4 = default(object);
				object obj5 = default(object);
				while (true)
				{
					if (num != 0)
					{
						Vector3 vector2 = ((List<Vector3>)(object)typeof(IEnumerator)).get_Item(num);
						if ((object)vector2 != null)
						{
							bool flag = num == 0;
							KeyValuePair<Vector3, int> keyValuePair4 = (KeyValuePair<Vector3, int>)0;
							if (!flag)
							{
								IEnumerable<KeyValuePair<Vector3, int>> enumerable2 = (IEnumerable<KeyValuePair<Vector3, int>>)((List<Vector3>)(object)typeof(IEnumerator<IGrouping<Vector3, KeyValuePair<Vector3, int>>>)).get_Item(num);
								int num2 = Enumerable.Count(enumerable2);
								if (num2 == 1)
								{
									continue;
								}
								if (enumerable2 != null)
								{
									Vector3 vector3 = ((List<Vector3>)(object)typeof(IEnumerable<KeyValuePair<Vector3, int>>)).get_Item((int)enumerable2);
									object obj = (object)(&num3);
									KeyValuePair<Vector3, int> keyValuePair5 = (KeyValuePair<Vector3, int>)0;
									while (true)
									{
										if (num3 != 0)
										{
											Vector3 vector4 = ((List<Vector3>)(object)typeof(IEnumerator)).get_Item(num3);
											if ((object)vector4 == null)
											{
												break;
											}
											bool flag2 = num3 == 0;
											keyValuePair5 = (KeyValuePair<Vector3, int>)0;
											if (!flag2)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800021B0");
												keyValuePair3 = (KeyValuePair<Vector3, int>)obj2;
												int value = keyValuePair2.Value;
												bool flag3 = list == null;
												keyValuePair5 = (KeyValuePair<Vector3, int>)(&keyValuePair2);
												if (!flag3)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
													keyValuePair2 = (KeyValuePair<Vector3, int>)obj2;
													continue;
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									bool flag4 = obj == null;
									int index = num3;
									if (!flag4)
									{
										index = (int)obj;
										Vector3 vector5 = ((List<Vector3>)(object)typeof(IDisposable)).get_Item((int)obj);
									}
									Vector3 vector6 = ((List<Vector3>)null).get_Item(index);
									if (System.Runtime.CompilerServices.Unsafe.As<KeyValuePair<Vector3, int>, UIntPtr>(ref keyValuePair3) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
									{
									}
									Vector3 vector7 = ((List<Vector3>)(object)typeof(IEnumerable<KeyValuePair<Vector3, int>>)).get_Item((int)enumerable2);
									object obj3 = (object)(&num3);
									keyValuePair4 = (KeyValuePair<Vector3, int>)0;
									while (true)
									{
										if (num3 != 0)
										{
											Vector3 vector8 = ((List<Vector3>)(object)typeof(IEnumerator)).get_Item(num3);
											if ((object)vector8 == null)
											{
												break;
											}
											bool flag5 = num3 == 0;
											keyValuePair4 = (KeyValuePair<Vector3, int>)0;
											if (!flag5)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800021B0");
												int value2 = keyValuePair.Value;
												bool flag6 = list == null;
												keyValuePair4 = (KeyValuePair<Vector3, int>)(&keyValuePair);
												if (!flag6)
												{
													list.set_Item(value2, (Vector3)(&obj4));
													keyValuePair = (KeyValuePair<Vector3, int>)obj5;
													keyValuePair3 = (KeyValuePair<Vector3, int>)obj5;
													continue;
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									if (obj3 != null)
									{
										Vector3 vector9 = ((List<Vector3>)(object)typeof(IDisposable)).get_Item((int)obj3);
									}
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						object obj6 = (object)(&num);
						if (obj6 != null)
						{
							Vector3 vector10 = ((List<Vector3>)(object)typeof(IDisposable)).get_Item((int)obj6);
						}
						break;
					}
					throw new NullReferenceException();
				}
				return list;
			}
		}
		throw new NullReferenceException();
	}

	private void CombineSubmeshes(Mesh mesh, Material[] materials)
	{
		int subMeshCount = mesh.subMeshCount;
		if (subMeshCount != 1)
		{
			int subMeshCount2 = mesh.subMeshCount;
			if (subMeshCount2 <= materials.Length)
			{
				int subMeshCount3 = mesh.subMeshCount;
				int subMeshCount4 = subMeshCount3 + 1;
				mesh.subMeshCount = subMeshCount4;
				int[] triangles = mesh.triangles;
				int subMeshCount5 = mesh.subMeshCount;
				int submesh = subMeshCount5 - 1;
				mesh.SetTriangles(triangles, submesh);
			}
		}
	}

	private unsafe void UpdateMaterialProperties()
	{
		//IL_0047: Expected O, but got Ref
		//IL_0076: Expected O, but got I4
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_0123: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39C38]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		object obj = default(object);
		outlineFillMaterial.SetColor("_OutlineColor", (Color)(&obj));
		bool flag = outlineMode == Mode.OutlineAll;
		Material material;
		float value;
		Material material2;
		float value2;
		Material material3;
		float value3;
		if (!flag)
		{
			object obj2 = outlineMode - 1;
			if (!flag)
			{
				object obj3 = obj2 - 1;
				if (!flag)
				{
					object obj4 = obj3 - 1;
					if (!flag)
					{
						if ((nint)obj4 == 1)
						{
							outlineMaskMaterial.SetFloat("_ZTest", 4f);
							outlineFillMaterial.SetFloat("_ZTest", 5f);
							material = outlineFillMaterial;
							value = 0f;
							goto IL_01e4;
						}
						return;
					}
					material2 = outlineMaskMaterial;
					value2 = 4f;
					goto IL_01f7;
				}
				outlineMaskMaterial.SetFloat("_ZTest", 8f);
				material3 = outlineFillMaterial;
				value3 = 5f;
			}
			else
			{
				outlineMaskMaterial.SetFloat("_ZTest", 8f);
				material3 = outlineFillMaterial;
				value3 = 4f;
			}
			goto IL_0218;
		}
		material2 = outlineMaskMaterial;
		value2 = 8f;
		goto IL_01f7;
		IL_01e4:
		material.SetFloat("_OutlineWidth", value);
		return;
		IL_01f7:
		material2.SetFloat("_ZTest", value2);
		material3 = outlineFillMaterial;
		value3 = 8f;
		goto IL_0218;
		IL_0218:
		material3.SetFloat("_ZTest", value3);
		material = outlineFillMaterial;
		value = outlineWidth;
		goto IL_01e4;
	}

	public Outline()
	{
		//IL_003c: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		outlineColor = (Color)0;
		outlineMode = Mode.OutlineVisible;
		outlineWidth = 2f;
		useDistanceCheck = true;
		distance = 4f;
		List<Mesh> list = new List<Mesh>();
		bakeKeys = list;
		bakeValues = new List<ListVector3>();
		base._002Ector();
	}

	static Outline()
	{
		HashSet<Mesh> hashSet = new HashSet<Mesh>();
		registeredMeshes = hashSet;
	}
}
