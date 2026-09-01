using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ShellLoadoutApplier : MonoBehaviour
{
	[Serializable]
	public class TargetLoadout
	{
		public CylinderShellSelector selector;

		public GameObject[] shellPrefabs;

		public bool setAsDesignTimeTemplate;

		public TargetLoadout()
		{
			GameObject[] array = new GameObject[6];
			shellPrefabs = array;
			setAsDesignTimeTemplate = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	[Serializable]
	public class TagLoadout
	{
		public string tag = "Untagged";

		public GameObject[] shellPrefabs;

		public bool setAsDesignTimeTemplate;

		public TagLoadout()
		{
			GameObject[] array = new GameObject[6];
			shellPrefabs = array;
			setAsDesignTimeTemplate = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public List<TargetLoadout> directTargets;

	public List<TagLoadout> tagTargets;

	public bool includeInactiveInTagSearch;

	public bool applyOnSceneLoaded;

	public bool reapplyOnEnable;

	public bool skipSelectorsAlreadyApplied;

	public bool verbose;

	private bool _appliedOnce;

	private HashSet<int> _appliedSelectorIds;

	private static readonly List<CylinderShellSelector> _scratch;

	private void OnEnable()
	{
		if (applyOnSceneLoaded)
		{
			UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
			SceneManager.sceneLoaded += value;
		}
		if (!_appliedOnce || reapplyOnEnable)
		{
			ApplyNow();
			_appliedOnce = true;
		}
	}

	private void OnDisable()
	{
		if (applyOnSceneLoaded)
		{
			UnityAction<Scene, LoadSceneMode> value = OnSceneLoaded;
			SceneManager.sceneLoaded -= value;
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		ApplyNow();
	}

	public unsafe void ApplyNow()
	{
		//IL_0076: Expected O, but got I4
		//IL_00b4: Expected O, but got I4
		//IL_0a75: Expected I4, but got O
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Expected O, but got Unknown
		//IL_012d: Expected O, but got I
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Expected O, but got Unknown
		//IL_043a: Expected O, but got I
		//IL_047a: Expected O, but got I
		//IL_07f0: Expected O, but got I
		//IL_07f0: Expected O, but got Ref
		//IL_0807: Expected O, but got I
		//IL_0807: Expected O, but got Ref
		//IL_0499: Expected O, but got I
		//IL_0178: Expected O, but got I
		//IL_01a3: Expected O, but got I
		//IL_0833: Expected O, but got Ref
		//IL_04c4: Expected O, but got Ref
		//IL_01d0: Expected O, but got I
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Expected O, but got Unknown
		//IL_08b0: Expected I, but got O
		//IL_078d: Expected O, but got I
		//IL_078d: Expected O, but got Ref
		//IL_04e3: Expected O, but got Ref
		//IL_04ed: Expected O, but got I4
		//IL_0202: Expected O, but got I
		//IL_07dc: Expected I, but got O
		//IL_029a: Expected O, but got I4
		//IL_022c: Expected I, but got O
		//IL_091e: Expected I, but got O
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Expected O, but got Unknown
		//IL_02d0: Expected O, but got I4
		//IL_0723: Expected I, but got O
		//IL_0728: Expected I, but got O
		//IL_02e7: Expected O, but got I4
		//IL_0312: Expected O, but got Ref
		//IL_0331: Expected O, but got Ref
		//IL_033b: Expected O, but got I4
		//IL_0360: Expected O, but got I4
		if (skipSelectorsAlreadyApplied && _appliedSelectorIds == null)
		{
			HashSet<int> hashSet = (_appliedSelectorIds = new HashSet<int>());
		}
		Dictionary<int, (CylinderShellSelector, GameObject[], bool)> dictionary = new Dictionary<int, (CylinderShellSelector, GameObject[], bool)>();
		bool flag = tagTargets == null;
		object obj = 0;
		nint num2 = default(nint);
		nint num = num2;
		Dictionary<int, (CylinderShellSelector, GameObject[], bool)> dictionary2 = null;
		Dictionary<int, (CylinderShellSelector, GameObject[], bool)> dictionary3 = dictionary;
		int num5 = default(int);
		object obj3 = default(object);
		int num6 = default(int);
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		if (!flag)
		{
			List<TagLoadout> list = tagTargets;
			nint num4 = default(nint);
			nint num3 = num4;
			obj = 0;
			Dictionary<int, (CylinderShellSelector, GameObject[], bool)> dictionary4 = null;
			dictionary2 = null;
			Dictionary<int, (CylinderShellSelector, GameObject[], bool)> dictionary5 = null;
			dictionary3 = null;
			object obj2 = default(object);
			object arg = default(object);
			bool flag8 = default(bool);
			while (true)
			{
				bool flag2 = (nint)dictionary3 >= list._size;
				num4 = num3;
				num = num2;
				num5 = (int)dictionary4;
				if (flag2)
				{
					break;
				}
				string text = (string)(object)tagTargets;
				if (tagTargets != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ stack_-E0_v14+10]");
						text = (string)0;
					}
					else
					{
						text = (string)(object)dictionary2;
					}
					bool flag3 = string.IsNullOrEmpty(text);
					num2 = 0;
					if (flag3)
					{
						goto IL_09f0;
					}
					if (obj2 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ stack_-E0_v14+10]");
						List<CylinderShellSelector> list2 = FindSelectorsByTag((string)0, includeInactiveInTagSearch);
						bool flag4 = !verbose;
						num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ stack_-E0_v14+10]");
						text = (string)0;
						if (!flag4)
						{
							bool flag5 = list2 == null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ stack_-E0_v14+10]");
							text = (string)0;
							if (flag5)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ stack_-E0_v14+10]");
							string text2 = $"[ShellLoadoutApplier] Tag '{0}' matched {arg} selector(s).";
							Debug.Log(text2, this);
							int size = list2._size;
							num2 = unchecked((nint)null);
							text = text2;
						}
						bool flag6 = list2 == null;
						HashSet<int> hashSet = (HashSet<int>)(object)dictionary2;
						if (!flag6)
						{
							for (; (nint)hashSet < list2._size; hashSet = (HashSet<int>)(hashSet + 1))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								bool flag7 = (UnityEngine.Object)flag8;
								bool flag9 = !flag7;
								num2 = 0;
								if (flag9)
								{
									continue;
								}
								bool flag10 = !flag8;
								text = (string)flag8;
								if (!flag10)
								{
									int instanceID = ((UnityEngine.Object)flag8).GetInstanceID();
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
									bool flag11 = dictionary == null;
									text = (string)(&obj3);
									if (!flag11)
									{
										dictionary.set_Item((int)(&num6), ((CylinderShellSelector, GameObject[], bool))(&obj4));
										object obj5 = 0;
										num3 = 0;
										num6 = instanceID;
										obj = obj3;
										num2 = 0;
										dictionary2 = (Dictionary<int, (CylinderShellSelector, GameObject[], bool)>)instanceID;
										continue;
									}
								}
								throw new NullReferenceException();
							}
							dictionary2 = null;
							goto IL_09f0;
						}
					}
				}
				goto IL_097d;
				IL_09f0:
				dictionary5 = (Dictionary<int, (CylinderShellSelector, GameObject[], bool)>)(dictionary4 + 1);
				list = tagTargets;
				bool flag12 = tagTargets == null;
				text = (string)(object)dictionary5;
				if (!flag12)
				{
					dictionary4 = dictionary5;
					dictionary3 = dictionary5;
					continue;
				}
				goto IL_097d;
			}
		}
		if (directTargets != null)
		{
			List<TargetLoadout> list3 = directTargets;
			Dictionary<int, (CylinderShellSelector, GameObject[], bool)> dictionary6 = dictionary2;
			dictionary3 = dictionary2;
			while ((nint)dictionary3 < list3._size)
			{
				if (directTargets != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag13 = num5 == 0;
					num = 0;
					if (!flag13)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ stack_20 (System.Int32)+10]");
						bool flag14 = (UnityEngine.Object)0 != null;
						num = 0;
						if (flag14)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ stack_20 (System.Int32)+10]");
							bool flag15 = (nint)0 == 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ stack_20 (System.Int32)+10]");
							string text = (string)0;
							if (!flag15)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ stack_20 (System.Int32)+10]");
								int instanceID2 = ((UnityEngine.Object)0).GetInstanceID();
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
								bool flag16 = dictionary == null;
								text = (string)(&obj3);
								if (!flag16)
								{
									dictionary.set_Item((int)(&num6), ((CylinderShellSelector, GameObject[], bool))(&obj4));
									object obj5 = 0;
									nint num4 = 0;
									num6 = instanceID2;
									num = 0;
									goto IL_0508;
								}
							}
							throw new NullReferenceException();
						}
					}
					goto IL_0508;
				}
				goto IL_097d;
				IL_0508:
				dictionary6 = (Dictionary<int, (CylinderShellSelector, GameObject[], bool)>)(dictionary6 + 1);
				list3 = directTargets;
				bool flag17 = directTargets != null;
				dictionary3 = dictionary6;
				if (flag17)
				{
					continue;
				}
				goto IL_097d;
			}
		}
		if (dictionary != null)
		{
			int count = dictionary.Count;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
			bool flag19 = default(bool);
			bool flag18 = flag19;
			GameObject[] array2 = default(GameObject[]);
			GameObject[] array = array2;
			nint num7 = 0;
			Dictionary<int, (CylinderShellSelector, GameObject[], bool)>.Enumerator enumerator = default(Dictionary<int, (CylinderShellSelector, GameObject[], bool)>.Enumerator);
			bool flag22 = default(bool);
			GameObject[] array3 = default(GameObject[]);
			GameObject[] array4 = default(GameObject[]);
			object arg3 = default(object);
			object obj6 = default(object);
			Dictionary<int, (CylinderShellSelector, GameObject[], bool)>.Enumerator enumerator2 = default(Dictionary<int, (CylinderShellSelector, GameObject[], bool)>.Enumerator);
			object obj9 = default(object);
			object arg4 = default(object);
			object arg5 = default(object);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803711A0");
					bool flag20 = obj4;
					bool flag21 = !flag20;
					flag18 = flag22;
					array = array3;
					num7 = 0;
					if (flag21)
					{
						continue;
					}
					if (skipSelectorsAlreadyApplied)
					{
						if ((object)obj4 == null)
						{
							throw new NullReferenceException();
						}
						int instanceID3 = obj4.GetInstanceID();
						if (_appliedSelectorIds == null)
						{
							break;
						}
						bool flag23 = _appliedSelectorIds.Contains((int)(&num5));
						bool flag24 = !flag23;
						num5 = instanceID3;
						if (!flag24)
						{
							bool flag25 = !verbose;
							flag18 = flag22;
							array = array3;
							num7 = 0;
							num5 = instanceID3;
							if (!flag25)
							{
								string text3 = obj4.name;
								string message = "[ShellLoadoutApplier] Skipping already-applied selector '" + text3 + "'.";
								Debug.Log(message, this);
								flag18 = flag22;
								array = array3;
								num7 = unchecked((nint)null);
								num = unchecked((nint)null);
								num5 = instanceID3;
							}
							continue;
						}
					}
					if (verbose)
					{
						if ((object)obj4 == null)
						{
							throw new NullReferenceException();
						}
						string arg2 = obj4.name;
						((Dictionary<int, (CylinderShellSelector, GameObject[], bool)>)(&obj3)).set_Item((int)(&array4), ((CylinderShellSelector, GameObject[], bool))0);
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string message2 = $"[ShellLoadoutApplier] Applying loadout to '{arg2}' (setAsDesignTimeTemplate={arg3}).";
						Debug.Log(message2, this);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1615 @ rax_v51+10]");
						num6 = 0;
						num = unchecked((nint)null);
					}
					((Dictionary<int, (CylinderShellSelector, GameObject[], bool)>)(&obj3)).set_Item((int)(&array4), ((CylinderShellSelector, GameObject[], bool))0);
					array = (GameObject[])obj6;
					((Dictionary<int, (CylinderShellSelector, GameObject[], bool)>)(&enumerator2)).set_Item((int)(&array4), ((CylinderShellSelector, GameObject[], bool))0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1518 @ rax_v46+10]");
					flag18 = false;
					bool flag26 = (object)obj4 == null;
					UnityEngine.Object obj7 = (UnityEngine.Object)(&enumerator2);
					if (!flag26)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
						UnityEngine.Object obj8 = obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1518 @ rax_v46+10]");
						((CylinderShellSelector)obj8).ReplaceAllShells((GameObject[])obj6, false);
						dictionary2 = (Dictionary<int, (CylinderShellSelector, GameObject[], bool)>)(dictionary2 + 1);
						bool flag27 = !skipSelectorsAlreadyApplied;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1583 @ rax_v45+10]");
						flag22 = false;
						obj4 = (UnityEngine.Object)obj9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1518 @ rax_v46+10]");
						num7 = 0;
						num = unchecked((nint)null);
						if (!flag27)
						{
							int instanceID4 = ((UnityEngine.Object)obj9).GetInstanceID();
							if (_appliedSelectorIds == null)
							{
								throw new NullReferenceException();
							}
							_appliedSelectorIds.Add((int)(&num5));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1583 @ rax_v45+10]");
							flag22 = false;
							obj4 = (UnityEngine.Object)obj9;
							num7 = 0;
							num = unchecked((nint)null);
							num5 = instanceID4;
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				if (verbose)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string message3 = $"[ShellLoadoutApplier] Planned {arg4}, applied {arg5} selector loadout(s).";
					Debug.Log(message3, this);
				}
				return;
			}
			throw new NullReferenceException();
		}
		goto IL_097d;
		IL_097d:
		throw new NullReferenceException();
	}

	private static List<CylinderShellSelector> FindSelectorsByTag(string tag, bool includeInactive)
	{
		//IL_00ba: Expected O, but got I4
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_038a: Expected O, but got I4
		List<CylinderShellSelector> scratch = _scratch;
		int version = scratch._version + 1;
		scratch._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			scratch._size = 0;
		}
		else
		{
			scratch._size = 0;
			if (scratch._size > 0)
			{
				Array.Clear(scratch._items, 0, scratch._size);
				object obj2 = 0;
			}
		}
		if (includeInactive)
		{
			int sceneCount = SceneManager.sceneCount;
			bool flag = sceneCount <= 0;
			int num = 0;
			if (!flag)
			{
				Scene scene = default(Scene);
				while (true)
				{
					Scene sceneAt = SceneManager.GetSceneAt(num);
					if (scene.IsValid() && scene.isLoaded)
					{
						GameObject[] rootGameObjects = scene.GetRootGameObjects();
						object obj3 = rootGameObjects + 32;
						scratch = null;
						while ((nint)scratch < rootGameObjects.Length)
						{
							if ((nint)scratch >= rootGameObjects.Length)
							{
								goto end_IL_010f;
							}
							if ((bool)(UnityEngine.Object)obj3)
							{
								Transform t = ((GameObject)obj3).transform;
								TraverseAndCollectByTag(t, tag, _scratch);
								object obj2 = 0;
							}
							scratch = (List<CylinderShellSelector>)(scratch + 1);
							obj3 += 8;
						}
					}
					num++;
					if (num >= sceneCount)
					{
						goto IL_03c8;
					}
					continue;
					end_IL_010f:
					break;
				}
				goto IL_0361;
			}
		}
		else
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
			object obj4 = array + 32;
			int num2 = 0;
			int num3 = 0;
			UnityEngine.Object obj5 = default(UnityEngine.Object);
			while (num3 < array.Length)
			{
				if (num2 < array.Length)
				{
					if ((bool)(UnityEngine.Object)obj4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
						if ((bool)obj5)
						{
							_scratch.Add((CylinderShellSelector)obj5);
						}
					}
					num2++;
					obj4 += 8;
					num3 = num2;
					continue;
				}
				goto IL_0361;
			}
		}
		goto IL_03c8;
		IL_03c8:
		return new List<CylinderShellSelector>(_scratch);
		IL_0361:
		return (List<CylinderShellSelector>)(object)new IndexOutOfRangeException();
	}

	private static void TraverseAndCollectByTag(Transform t, string tag, List<CylinderShellSelector> results)
	{
		if (!t)
		{
			return;
		}
		GameObject gameObject = t.gameObject;
		if (gameObject.CompareTag(tag))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if ((bool)obj)
			{
				results.Add((CylinderShellSelector)obj);
			}
		}
		int childCount = t.childCount;
		if (childCount > 0)
		{
			int num = 0;
			do
			{
				Transform child = t.GetChild(num);
				TraverseAndCollectByTag(child, tag, results);
				num++;
			}
			while (num < childCount);
		}
	}

	public ShellLoadoutApplier()
	{
		List<TargetLoadout> list = new List<TargetLoadout>();
		directTargets = list;
		tagTargets = new List<TagLoadout>();
		reapplyOnEnable = true;
		base._002Ector();
	}

	static ShellLoadoutApplier()
	{
		List<CylinderShellSelector> scratch = new List<CylinderShellSelector>(64);
		_scratch = scratch;
	}
}
