using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator;

public static class FindComponentUtils
{
	[Serializable]
	private sealed class _003C_003Ec__2<T>
	{
		public static readonly _003C_003Ec__2<T> _003C_003E9;

		public static Func<Scene, bool> _003C_003E9__2_0;

		public static Func<Scene, IEnumerable<GameObject>> _003C_003E9__2_1;

		static _003C_003Ec__2()
		{
			//IL_003f: Expected O, but got I
			//IL_0054: Expected O, but got I
			nint num = 0;
			object obj = null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v11 (Il2CppRgctx<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1>)+10]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rax_v13+B8]");
			object obj3 = 0;
			obj3 = obj;
		}

		internal bool _003CFindComponentsInScenes_003Eb__2_0(Scene s)
		{
			Scene scene = default(Scene);
			return scene.IsValid();
		}

		internal IEnumerable<GameObject> _003CFindComponentsInScenes_003Eb__2_1(Scene s)
		{
			Scene scene = default(Scene);
			return scene.GetRootGameObjects();
		}
	}

	private sealed class _003C_003Ec__DisplayClass2_0<T>
	{
		public bool includeInactive;

		internal bool _003CFindComponentsInScenes_003Eb__2(GameObject g)
		{
			//IL_0062: Expected I4, but got O
			if (includeInactive)
			{
				return true;
			}
			if ((object)g != null)
			{
				return g.activeInHierarchy;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal IEnumerable<T> _003CFindComponentsInScenes_003Eb__3(GameObject g)
		{
			if ((object)g != null)
			{
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9940");
				IEnumerable<T> result = default(IEnumerable<T>);
				return result;
			}
			return (IEnumerable<T>)new NullReferenceException();
		}
	}

	public unsafe static T FindComponentInAllLoadedScenes<T>(bool includeInactive, Predicate<Scene> scenePredicate = null)
	{
		//IL_0008: Expected O, but got Ref
		//IL_004f: Expected O, but got I
		//IL_005f: Expected O, but got I
		//IL_0075: Expected O, but got I
		//IL_02db: Expected O, but got I
		//IL_030c: Expected O, but got Ref
		//IL_0326: Expected O, but got I
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_0112: Expected O, but got I
		//IL_0225: Expected O, but got I
		//IL_0297: Expected O, but got I
		//IL_02b1: Expected O, but got I
		//IL_0282: Expected O, but got I
		//IL_017e: Expected O, but got I
		//IL_018c: Expected O, but got Ref
		//IL_01ba: Expected O, but got I
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r9_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r9_v1+38]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v2+20]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v1+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v1+FC]");
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v1+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v1+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r9_v1+38]");
			object obj7 = 0;
			object obj8 = obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r12_v1+38]");
			if ((nint)0 != 0)
			{
				goto IL_00d0;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r12_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
		}
		goto IL_00d0;
		IL_00d0:
		int sceneCount = SceneManager.sceneCount;
		Scene[] array = new Scene[sceneCount];
		object obj9 = array + 32;
		int num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v1+FC]");
		object obj10 = 0;
		object obj12 = default(object);
		T result = default(T);
		while (true)
		{
			int sceneCount2 = SceneManager.sceneCount;
			object obj13;
			if (num < sceneCount2)
			{
				Scene sceneAt = SceneManager.GetSceneAt(num);
				if (scenePredicate != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [scenePredicate @ rdx (System.Predicate`1<UnityEngine.SceneManagement.Scene>)+28]");
					obj10 = 0;
					object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 104));
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [scenePredicate @ rdx (System.Predicate`1<UnityEngine.SceneManagement.Scene>)+18] (should have been resolved before IL gen)");
					bool flag = obj12 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [scenePredicate @ rdx (System.Predicate`1<UnityEngine.SceneManagement.Scene>)+28]");
					obj13 = 0;
					if (flag)
					{
						goto IL_0356;
					}
				}
				Scene sceneAt2 = SceneManager.GetSceneAt(num);
				if (num >= array.Length)
				{
					break;
				}
				obj9 = sceneAt2;
				obj13 = obj10;
				goto IL_0356;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r12_v1+38]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806B9080");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v239 @ rax_v26+18]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v1+FC]");
				object obj15 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r9_v1+38]");
				object obj16 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ r10_v1+FC]");
				object obj15 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			return result;
			IL_0356:
			num++;
			obj9 += 4;
			obj10 = obj13;
		}
		return (T)new IndexOutOfRangeException();
	}

	public static List<T> FindComponentsInAllLoadedScenes<T>(bool includeInactive, Predicate<Scene> scenePredicate = null)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ r8_v7 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		int sceneCount = SceneManager.sceneCount;
		Scene[] array = new Scene[sceneCount];
		object obj = array + 32;
		int num = 0;
		nint num2 = default(nint);
		object obj2 = default(object);
		while (true)
		{
			int sceneCount2 = SceneManager.sceneCount;
			nint num3;
			if (num < sceneCount2)
			{
				Scene sceneAt = SceneManager.GetSceneAt(num);
				if (scenePredicate != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [scenePredicate @ rdx (System.Predicate`1<UnityEngine.SceneManagement.Scene>)+28]");
					num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [scenePredicate @ rdx (System.Predicate`1<UnityEngine.SceneManagement.Scene>)+18] (should have been resolved before IL gen)");
					bool flag = obj2 == null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [scenePredicate @ rdx (System.Predicate`1<UnityEngine.SceneManagement.Scene>)+28]");
					num3 = 0;
					if (flag)
					{
						goto IL_015a;
					}
				}
				Scene sceneAt2 = SceneManager.GetSceneAt(num);
				if (array == null)
				{
					break;
				}
				obj = sceneAt2;
				num3 = num2;
				goto IL_015a;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 198 Invalid \"Jump target not found in method: 0x1806B9080\"");
			throw new IndexOutOfRangeException();
			IL_015a:
			num++;
			obj += 4;
			num2 = num3;
		}
		return (List<T>)(object)new NullReferenceException();
	}

	public static List<T> FindComponentsInScenes<T>(bool includeInactive, Scene[] scenes)
	{
		//IL_0080: Expected O, but got I
		//IL_0149: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		object CS_0024_003C_003E8__locals1 = null;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rax_v17 (Il2CppClass<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v18 (Il2CppStaticFields<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+8]");
		Func<Scene, bool> predicate = (Func<Scene, bool>)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v18 (Il2CppStaticFields<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+8]");
		if ((nint)0 == 0)
		{
			Func<Scene, bool> func = (Scene s) =>
			{
				Scene scene = default(Scene);
				return scene.IsValid();
			};
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v458 @ rax_v87 (Il2CppClass<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+B8]");
			nint num4 = 0;
			predicate = func;
		}
		IEnumerable<Scene> source = Enumerable.Where(scenes, predicate);
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v27 (Il2CppClass<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rax_v28 (Il2CppStaticFields<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+10]");
		Func<Scene, IEnumerable<GameObject>> selector = (Func<Scene, IEnumerable<GameObject>>)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rax_v28 (Il2CppStaticFields<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+10]");
		if ((nint)0 == 0)
		{
			Func<Scene, IEnumerable<GameObject>> func2 = (Scene s) =>
			{
				Scene scene = default(Scene);
				return scene.GetRootGameObjects();
			};
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v550 @ rax_v61 (Il2CppClass<Kamgam.SettingsGenerator.FindComponentUtils+<>c__2`1<T>>)+B8]");
			nint num8 = 0;
			selector = func2;
		}
		IEnumerable<GameObject> source2 = Enumerable.SelectMany(source, selector);
		Func<GameObject, bool> predicate2 = delegate(GameObject g)
		{
			//IL_0062: Expected I4, but got O
			if (((_003C_003Ec__DisplayClass2_0<T>)CS_0024_003C_003E8__locals1).includeInactive)
			{
				return true;
			}
			if ((object)g == null)
			{
				NullReferenceException ex2 = new NullReferenceException();
				return (byte)(int)ex2 != 0;
			}
			return g.activeInHierarchy;
		};
		IEnumerable<GameObject> source3 = Enumerable.Where(source2, predicate2);
		Func<GameObject, IEnumerable<T>> selector2 = delegate(GameObject g)
		{
			if ((object)g != null)
			{
				nint num9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9940");
				IEnumerable<T> result = default(IEnumerable<T>);
				return result;
			}
			return (IEnumerable<T>)new NullReferenceException();
		};
		IEnumerable<T> enumerable = Enumerable.SelectMany(source3, selector2);
		if (enumerable != null)
		{
			return new List<T>(enumerable);
		}
		Exception ex = System.Linq.Error.ArgumentNull("source");
		throw ex;
	}
}
