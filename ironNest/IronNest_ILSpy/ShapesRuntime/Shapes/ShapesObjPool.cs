using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public abstract class ShapesObjPool<T, P> : MonoBehaviour
{
	private const int ALLOCATION_COUNT_WARNING = 500;

	private const int ALLOCATION_COUNT_CAP = 1000;

	private Stack<T> elementsPassive;

	private Dictionary<int, T> elementsActive;

	private static P instance;

	private int ElementCount
	{
		get
		{
			//IL_0010: Expected O, but got I
			//IL_0092: Expected I4, but got O
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected I4, but got Unknown
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+28]");
				if ((nint)0 != 0)
				{
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311B0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2 @ rax_v1+18]");
					object obj2 = default(object);
					return obj2 + 0;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
	}

	public T ImmediateModeElement
	{
		get
		{
			//IL_0016: Expected O, but got I
			//IL_004b: Expected O, but got I
			//IL_005b: Expected O, but got I
			//IL_0095: Expected O, but got I
			//IL_00a5: Expected O, but got I
			//IL_00b5: Expected O, but got I
			//IL_00c2: Expected O, but got I8
			//IL_0103: Expected O, but got I
			//IL_01a3: Expected O, but got I
			//IL_01b3: Expected O, but got I
			//IL_01da: Expected O, but got I
			//IL_01ea: Expected O, but got I
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v13 @ rdx_v1 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+28]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+28]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rsi_v1+20]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v25 @ rax_v4+C0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082F950");
				object obj4 = default(object);
				if (obj4 != null)
				{
					T result = default(T);
					return result;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rsi_v1+20]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ rax_v7+C0]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rcx_v3+A8]");
				object obj7 = 0;
				UnityEngine.Object obj8 = (UnityEngine.Object)4294967295L;
				UnityEngine.Object obj9 = null;
				UnityEngine.Object obj13 = default(UnityEngine.Object);
				while (true)
				{
					if (obj9 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
						object obj10 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
						if ((nint)0 == 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rax_v21+18]");
						if ((nint)0 > (nint)0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
							obj9 = obj8;
							continue;
						}
					}
					if (obj9 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rsi_v4+20]");
						object obj11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rax_v18+C0]");
						object obj12 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180903800");
						obj9 = obj13;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+28]");
					if ((nint)0 == 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rsi_v4+20]");
					object obj14 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rax_v15+C0]");
					object obj15 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082A970");
					return (T)obj9;
				}
			}
			return (T)new NullReferenceException();
		}
	}

	public static int InstanceElementCount
	{
		get
		{
			//IL_0131: Expected I4, but got O
			//IL_0079: Expected O, but got I
			//IL_00d9: Expected O, but got I
			//IL_00e9: Expected O, but got I
			//IL_00f9: Expected O, but got I
			//IL_0113: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Expected I4, but got Unknown
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18006C970");
			object obj = default(object);
			if (obj != null)
			{
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180904560");
				object obj2 = default(object);
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v7+20]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v7+20]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v7+28]");
						if ((nint)0 != 0)
						{
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v144 @ rax_v12 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+58]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v145 @ rdx_v1+20]");
							object obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rax_v13+C0]");
							object obj6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311B0");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rcx_v9+18]");
							object obj7 = default(object);
							return obj7 + 0;
						}
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			return 0;
		}
	}

	public static int InstanceElementCountActive
	{
		get
		{
			//IL_00b7: Expected I4, but got O
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18006C970");
			object obj = default(object);
			if (obj != null)
			{
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180904560");
				object obj2 = default(object);
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v7+28]");
					if ((nint)0 != 0)
					{
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311B0");
						int result = default(int);
						return result;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			return 0;
		}
	}

	public static bool InstanceExists
	{
		get
		{
			//IL_001b: Expected O, but got I
			//IL_0030: Expected O, but got I
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v4 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+40]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rax_v6+B8]");
			object obj2 = 0;
			return (UnityEngine.Object)obj2 != null;
		}
	}

	public abstract string PoolTypeName { get; }

	public static P Instance
	{
		get
		{
			//IL_001b: Expected O, but got I
			//IL_0030: Expected O, but got I
			//IL_0220: Expected O, but got I
			//IL_0235: Expected O, but got I
			//IL_0091: Expected O, but got I
			//IL_00a6: Expected O, but got I
			//IL_00d3: Expected O, but got I
			//IL_00e8: Expected O, but got I
			//IL_012a: Expected O, but got I
			//IL_0191: Expected O, but got I
			//IL_01a6: Expected O, but got I
			//IL_01de: Expected O, but got I
			//IL_01f3: Expected O, but got I
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v4 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+40]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rax_v6+B8]");
			object obj2 = 0;
			if ((UnityEngine.Object)obj2 == null)
			{
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807334B0");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ rax_v26 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+40]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rax_v28+B8]");
				object obj4 = 0;
				object obj5 = default(object);
				obj4 = obj5;
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v310 @ rax_v38 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+40]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ rax_v40+B8]");
				object obj7 = 0;
				if ((UnityEngine.Object)obj7 == null)
				{
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ rax_v44 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+68]");
					object obj8 = 0;
					GameObject gameObject = new GameObject();
					if (Application.isPlaying)
					{
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
					if ((object)gameObject == null)
					{
						return (P)new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ rsi_v3+20]");
					object obj9 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v430 @ rax_v53+C0]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9030");
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					nint num6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v452 @ rax_v58 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+40]");
					object obj11 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v465 @ rax_v60+B8]");
					object obj12 = 0;
					object obj13 = default(object);
					obj12 = obj13;
				}
			}
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rax_v13 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+40]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rax_v15+B8]");
			return (P)0;
		}
	}

	private static P CreatePool()
	{
		GameObject gameObject = new GameObject();
		if (Application.isPlaying)
		{
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
		if ((object)gameObject != null)
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9030");
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			P result = default(P);
			return result;
		}
		return (P)new NullReferenceException();
	}

	private void ClearData()
	{
		//IL_0042: Expected O, but got I4
		//IL_00c8: Expected O, but got I
		//IL_00e3: Expected O, but got I
		//IL_00f3: Expected O, but got I
		//IL_0103: Expected O, but got I
		//IL_014c: Expected O, but got I
		//IL_00aa: Expected O, but got I4
		Transform transform = base.transform;
		bool flag = (nint)transform < 0;
		int childCount = transform.childCount;
		int num = childCount - 1;
		object obj = 0;
		if (!flag)
		{
			bool flag2;
			do
			{
				Transform transform2 = base.transform;
				Transform child = transform2.GetChild(num);
				GameObject obj2 = child.gameObject;
				ShapesExtensions.DestroyBranched(obj2);
				num--;
				flag2 = (nint)child >= 0;
				obj = 0;
			}
			while (flag2);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
		object obj3 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ rcx_v9 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+78]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rax_v12+20]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v180 @ rcx_v10+C0]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj7 = default(object);
		if (obj7 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v7+10]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v7+18]");
			Array.Clear((Array)num3, 0, 0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rbx_v7+1C]");
		_ = (nint)0 + (nint)1;
		_ = 0;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082AB30");
	}

	private void OnEnable()
	{
		//IL_0042: Expected I, but got O
		//IL_0080: Expected O, but got I4
		//IL_00d7: Expected O, but got I
		//IL_0178: Expected O, but got I4
		//IL_00f2: Expected O, but got I
		//IL_0107: Expected O, but got I
		//IL_0125: Expected O, but got I
		//IL_01b8: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3E789]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if ((object)this != null)
		{
			GameObject gameObject = base.gameObject;
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v80 @ rdx_v7 (Il2CppClass<Shapes.ShapesObjPool`2<T, P>>)+178] (should have been resolved before IL gen)");
			string text2 = default(string);
			string text = "Shapes " + text2 + " Pool";
			bool flag = (object)gameObject == null;
			object obj = 0;
			string text3 = " Pool";
			GameObject gameObject2 = gameObject;
			if (!flag)
			{
				gameObject.name = text;
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18006C860");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v173 @ rcx_v12 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+50]");
				gameObject2 = (GameObject)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj2 = default(object);
				bool flag2 = obj2 == null;
				obj = 0;
				text3 = null;
				if (!flag2)
				{
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rdx_v14 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+40]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rax_v18+B8]");
					object obj4 = 0;
					obj4 = obj2;
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v17 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+50]");
					gameObject2 = (GameObject)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj5 = default(object);
					bool flag3 = obj5 == null;
					obj = 0;
					text3 = null;
					if (!flag3)
					{
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		throw new NullReferenceException();
	}

	private void OnDisable()
	{
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18006C860");
	}

	public T GetElement(int id)
	{
		//IL_006b: Expected O, but got I
		//IL_00ac: Expected O, but got I
		//IL_014c: Expected O, but got I
		//IL_015c: Expected O, but got I
		//IL_0183: Expected O, but got I
		//IL_0193: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+28]");
		if ((nint)0 != 0)
		{
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082F950");
			object obj = default(object);
			if (obj != null)
			{
				T result = default(T);
				return result;
			}
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rcx_v3 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+A8]");
			object obj2 = 0;
			UnityEngine.Object obj3 = null;
			UnityEngine.Object obj5 = default(UnityEngine.Object);
			UnityEngine.Object obj8 = default(UnityEngine.Object);
			while (true)
			{
				if (obj3 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
					if ((nint)0 == 0)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v54 @ rax_v20+18]");
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
						obj3 = obj5;
						continue;
					}
				}
				if (obj3 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rsi_v4+20]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rax_v17+C0]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180903800");
					obj3 = obj8;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+28]");
				if ((nint)0 == 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rsi_v4+20]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ rax_v14+C0]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082A970");
				return (T)obj3;
			}
		}
		return (T)new NullReferenceException();
	}

	public T AllocateElement(int id)
	{
		//IL_003c: Expected O, but got I
		UnityEngine.Object obj = null;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		while (true)
		{
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
				if ((nint)0 == 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v16+18]");
				if ((nint)0 > (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
					obj = obj3;
					continue;
				}
			}
			if (obj == null)
			{
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180903800");
				obj = obj4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+28]");
			if ((nint)0 == 0)
			{
				break;
			}
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082A970");
			return (T)obj;
		}
		return (T)new NullReferenceException();
	}

	public void ReleaseElement(int id)
	{
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082F950");
		object obj = default(object);
		if (obj != null)
		{
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082CF90");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1809176D0");
		}
	}

	private unsafe T CreateElement(int id)
	{
		//IL_0272: Expected O, but got I
		//IL_001b: Expected O, but got I
		//IL_002b: Expected O, but got I
		//IL_003b: Expected O, but got I
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_0203: Expected I, but got O
		//IL_0091: Expected O, but got I
		//IL_00ae: Expected I, but got O
		//IL_0125: Expected I, but got O
		//IL_01bd: Expected O, but got Ref
		//IL_01e5: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.ShapesObjPool`2<T, P>)+20]");
		object obj = 0;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rdx_v4 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+58]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v10+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v138 @ rdx_v5+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808311B0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rax_v2+18]");
		object obj6 = default(object);
		object obj5 = obj6 + 0;
		if ((nint)obj5 <= 1000)
		{
			bool flag = (nint)obj5 <= 500;
			IntPtr intPtr = default(IntPtr);
			object obj7 = (nint)intPtr;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				nint num2 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v293 @ rdx_v31 (Il2CppClass<Shapes.ShapesObjPool`2<T, P>>)+178] (should have been resolved before IL gen)");
				string text2 = default(string);
				string text = text2.ToLower();
				object arg = default(object);
				string message = $"Allocating more than {arg} {text} elements. You are probably leaking and not properly disposing text objects";
				Debug.LogWarning(message);
				obj7 = text;
			}
			string text3;
			if (id == -1)
			{
				nint num3 = (nint)this;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v294 @ rdx_v27 (Il2CppClass<Shapes.ShapesObjPool`2<T, P>>)+178] (should have been resolved before IL gen)");
				string text4 = default(string);
				text3 = "Immediate Mode " + text4;
			}
			else
			{
				int num4 = default(int);
				text3 = num4.ToString();
			}
			GameObject gameObject = new GameObject(text3);
			Transform transform = gameObject.transform;
			Transform parent = base.transform;
			transform.SetParent(parent, worldPositionStays: false);
			Transform transform2 = gameObject.transform;
			object obj8 = default(object);
			transform2.localPosition = (Vector3)(&obj8);
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9030");
			nint num6 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v422 @ r8_v12 (Il2CppClass<Shapes.ShapesObjPool`2<T, P>>)+188] (should have been resolved before IL gen)");
			T result = default(T);
			return result;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		nint num7 = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v257 @ rdx_v9 (Il2CppClass<Shapes.ShapesObjPool`2<T, P>>)+178] (should have been resolved before IL gen)");
		string text5 = default(string);
		if (text5 != null)
		{
			string arg2 = text5.ToLower();
			object arg3 = default(object);
			string message2 = $"Text element allocation cap of {arg3} reached. You are probably leaking and not properly disposing {arg2} elements";
			Debug.LogError(message2);
			return (T)null;
		}
		return (T)new NullReferenceException();
	}

	public abstract void OnCreatedNewComponent(T comp);

	protected ShapesObjPool()
	{
		//IL_0026: Expected O, but got I
		//IL_0045: Expected O, but got I
		//IL_0055: Expected O, but got I
		nint num = 0;
		object obj = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v1 (Il2CppRgctx<Shapes.ShapesObjPool`2>)+E8]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ rbx_v1+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rcx_v5+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038490");
		nint num3 = 0;
		object obj5 = null;
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180831000");
		base._002Ector();
	}
}
