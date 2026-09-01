using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cpp2ILInjected;
using UnityEngine;

namespace ArticleSystem;

public class ArticlePoolQueueManager : MonoBehaviour
{
	[Serializable]
	private class QueueEntry
	{
		public bool isPool;

		public ArticlePoolDefinition pool;

		public GameObject prefab;

		public int remainingUses = 1;

		public string note;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ArticlePoolDefinition, string> _003C_003E9__12_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal string _003CAwake_003Eb__12_0(ArticlePoolDefinition x)
		{
			if ((object)x != null)
			{
				return x.ID;
			}
			return (string)(object)new NullReferenceException();
		}
	}

	private static ArticlePoolQueueManager s_instance;

	private bool persistAcrossScenes = true;

	private bool logDebug;

	private List<QueueEntry> queue;

	private readonly Dictionary<ArticlePoolDefinition, int> _sequentialNextIndex;

	private readonly Dictionary<ArticlePoolDefinition, List<GameObject>> _passDecks;

	private static Dictionary<string, ArticlePoolDefinition> ArticlePools;

	private System.Random _passRng;

	public static ArticlePoolQueueManager Instance
	{
		get
		{
			//IL_00a7: Expected I, but got O
			if (s_instance == null)
			{
				ArticlePoolQueueManager articlePoolQueueManager = UnityEngine.Object.FindObjectOfType<ArticlePoolQueueManager>();
				if (articlePoolQueueManager == null)
				{
					GameObject gameObject = new GameObject("ArticlePoolQueueManager (Runtime)");
					if ((object)gameObject == null)
					{
						goto IL_00ca;
					}
					ArticlePoolQueueManager articlePoolQueueManager2 = gameObject.AddComponent<ArticlePoolQueueManager>();
					s_instance = articlePoolQueueManager2;
				}
				else
				{
					s_instance = articlePoolQueueManager;
				}
			}
			else
			{
				nint num = (nint)typeof(ArticlePoolQueueManager);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v96 @ rax_v12 (Il2CppClass<ArticleSystem.ArticlePoolQueueManager>)+E4]");
				if ((nint)0 == 0)
				{
					goto IL_00ca;
				}
			}
			return s_instance;
			IL_00ca:
			return (ArticlePoolQueueManager)(object)new NullReferenceException();
		}
	}

	private static void Bootstrap()
	{
		ArticlePoolQueueManager instance = Instance;
	}

	private void Awake()
	{
		if (s_instance != null && s_instance != this)
		{
			if (logDebug)
			{
				Debug.Log("[ArticlePoolQueueManager] Duplicate instance detected. Destroying this one.");
			}
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
			return;
		}
		s_instance = this;
		if (persistAcrossScenes)
		{
			Transform transform = base.transform;
			Transform parent = transform.parent;
			bool flag = parent == null;
			if (!flag)
			{
				if (logDebug != flag)
				{
					Debug.Log("[ArticlePoolQueueManager] 'Persist Across Scenes' is ON but this GameObject is not a scene root — DontDestroyOnLoad skipped. The manager will persist as long as its parent hierarchy is loaded.", this);
				}
			}
			else
			{
				GameObject target = base.gameObject;
				UnityEngine.Object.DontDestroyOnLoad(target);
			}
		}
		ArticlePoolDefinition[] source = Resources.LoadAll<ArticlePoolDefinition>("Articles");
		Func<ArticlePoolDefinition, string> keySelector = _003C_003Ec._003C_003E9__12_0;
		if (_003C_003Ec._003C_003E9__12_0 == null)
		{
			keySelector = (_003C_003Ec._003C_003E9__12_0 = (ArticlePoolDefinition x) => (string)(((object)x != null) ? ((object)x.ID) : ((object)new NullReferenceException())));
		}
		Dictionary<string, ArticlePoolDefinition> articlePools = Enumerable.ToDictionary(source, keySelector);
		ArticlePools = articlePools;
	}

	public void BeginPass(System.Random rng)
	{
		_passRng = rng;
		_passDecks.Clear();
		if (logDebug)
		{
			Debug.Log("[ArticlePoolQueueManager] BeginPass — pass decks cleared.");
		}
	}

	public void EndPass()
	{
		_passDecks.Clear();
		_passRng = null;
		if (logDebug)
		{
			Debug.Log("[ArticlePoolQueueManager] EndPass — pass decks released.");
		}
	}

	public void ClearQueue()
	{
		List<QueueEntry> list = queue;
		int version = list._version + 1;
		list._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			list._size = 0;
		}
		else
		{
			list._size = 0;
			if (list._size > 0)
			{
				Array.Clear(list._items, 0, list._size);
			}
		}
		if (logDebug)
		{
			Debug.Log("[ArticlePoolQueueManager] Queue cleared.");
		}
	}

	public void ResetAllSequentialIndices()
	{
		_sequentialNextIndex.Clear();
		if (logDebug)
		{
			Debug.Log("[ArticlePoolQueueManager] All per-pool sequential indices reset.");
		}
	}

	public unsafe void LogQueueSnapshot()
	{
		//IL_0058: Expected O, but got I4
		//IL_0078: Expected O, but got I4
		//IL_0105: Expected O, but got Ref
		//IL_0161: Expected O, but got I
		//IL_018d: Expected O, but got Ref
		//IL_0195: Expected O, but got Ref
		//IL_060e: Expected I, but got O
		//IL_061e: Expected O, but got I
		//IL_0645: Expected O, but got Ref
		//IL_06a3: Expected O, but got I
		//IL_01ba: Expected O, but got Ref
		//IL_0707: Expected I, but got O
		//IL_0717: Expected O, but got I
		//IL_0746: Expected O, but got Ref
		//IL_0257: Expected O, but got Ref
		//IL_0811: Expected O, but got I
		//IL_01d0: Expected I, but got O
		//IL_01e0: Expected O, but got I
		//IL_020f: Expected O, but got Ref
		//IL_021f: Expected O, but got I
		//IL_08a3: Expected O, but got I
		//IL_07a2: Expected I, but got O
		//IL_07b2: Expected O, but got I
		//IL_07d9: Expected O, but got Ref
		//IL_083e: Expected I, but got O
		//IL_084e: Expected O, but got I
		//IL_0875: Expected O, but got Ref
		//IL_0885: Expected O, but got I
		//IL_0318: Expected O, but got I
		//IL_02b5: Expected O, but got I
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Expected O, but got Unknown
		//IL_092f: Expected O, but got I4
		//IL_032e: Expected I, but got O
		//IL_033e: Expected O, but got I
		//IL_0375: Expected O, but got Ref
		//IL_0385: Expected O, but got I
		//IL_03df: Expected O, but got I
		//IL_0959: Expected O, but got I4
		//IL_0405: Expected O, but got Ref
		//IL_041b: Expected I, but got O
		//IL_042b: Expected O, but got I
		//IL_045a: Expected O, but got Ref
		//IL_046a: Expected O, but got I
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Expected O, but got Unknown
		//IL_04d2: Expected O, but got I
		//IL_0507: Expected I, but got O
		//IL_0517: Expected O, but got I
		//IL_0540: Expected O, but got I
		//IL_054e: Expected O, but got Ref
		//IL_055e: Expected O, but got I
		//IL_056e: Expected O, but got I
		//IL_05ae: Expected O, but got I
		//IL_05c7: Expected I4, but got O
		List<QueueEntry> list = queue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg = default(object);
		string message = $"[ArticlePoolQueueManager] Queue size={arg}";
		Debug.Log(message);
		List<QueueEntry> list2 = queue;
		bool flag = queue == null;
		object obj = 0;
		string text = null;
		string text2 = null;
		nint num2 = default(nint);
		nint num = num2;
		string text3 = null;
		object obj2 = 0;
		int num3 = list._size;
		string text4 = null;
		string text5 = null;
		if (!flag)
		{
			object obj3 = default(object);
			string text8 = default(string);
			object obj4 = default(object);
			string text9 = default(string);
			string text13 = default(string);
			object obj5 = default(object);
			string text14 = default(string);
			string text18 = default(string);
			object obj6 = default(object);
			object obj7 = default(object);
			string text21 = default(string);
			object obj8 = default(object);
			string text25 = default(string);
			object obj9 = default(object);
			string text28 = default(string);
			object obj11 = default(object);
			object obj12 = default(object);
			object obj13 = default(object);
			object obj15 = default(object);
			string text30 = default(string);
			object obj18 = default(object);
			object obj19 = default(object);
			while (true)
			{
				if ((nint)text5 >= list2._size)
				{
					return;
				}
				text2 = (string)(object)queue;
				bool flag2 = queue == null;
				num2 = num;
				obj = obj2;
				text = text4;
				if (flag2)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				bool flag3 = obj3 == null;
				num2 = 0;
				obj = (object)(&obj3);
				text = text3;
				if (flag3)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+10]");
				object[] array;
				string text11;
				string text12;
				if ((nint)0 == 0)
				{
					array = new object[4];
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
					string text6 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					bool flag4 = array == null;
					num2 = 0;
					obj = (object)(&obj3);
					string text7 = (string)(&num3);
					if (!flag4)
					{
						bool flag5 = text8 == null;
						text7 = (string)(&num3);
						if (!flag5)
						{
							nint num4 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v621 @ rdx_v51 (Il2CppClass<System.Object[]>)+40]");
							text7 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag6 = obj4 == null;
							text6 = text8;
							num2 = 0;
							obj = (object)(&obj3);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v621 @ rdx_v51 (Il2CppClass<System.Object[]>)+40]");
							text = (string)0;
							text2 = text8;
							if (flag6)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw text9;
							}
						}
						bool flag7 = array.Length <= 0;
						num2 = 0;
						obj = (object)(&obj3);
						if (!flag7)
						{
							array[0] = text8;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+20]");
							bool flag8 = (nint)0 == 0;
							text = text8;
							if (!flag8)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+20]");
								string text10 = ((UnityEngine.Object)0).name;
								bool flag9 = text10 != null;
								text = null;
								text11 = text10;
								if (flag9)
								{
									goto IL_0326;
								}
							}
							bool flag10 = "<null>" == null;
							text11 = "<null>";
							text12 = "<null>";
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+20]");
							text2 = (string)0;
							if (!flag10)
							{
								goto IL_0326;
							}
							goto IL_039b;
						}
						throw new IndexOutOfRangeException();
					}
					throw new NullReferenceException();
				}
				object[] array2 = new object[4];
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				if (text13 != null)
				{
					nint num5 = (nint)array2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v648 @ rdx_v30 (Il2CppClass<System.Object[]>)+40]");
					string text7 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag11 = obj5 == null;
					num2 = 0;
					obj = (object)(&obj3);
					string text6 = text13;
					if (flag11)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						throw text14;
					}
				}
				array2[0] = text13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+18]");
				string text16;
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+18]");
					string text15 = ((UnityEngine.Object)0).name;
					bool flag12 = text15 != null;
					text16 = text15;
					if (flag12)
					{
						goto IL_06ff;
					}
				}
				bool flag13 = "<null>" == null;
				text16 = "<null>";
				string text17 = "<null>";
				if (!flag13)
				{
					goto IL_06ff;
				}
				goto IL_075c;
				IL_039b:
				string text22;
				string format;
				object[] array3;
				if (array.Length > 1)
				{
					array[1] = text12;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
					text2 = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					bool flag14 = text18 == null;
					text = (string)(&obj6);
					if (!flag14)
					{
						nint num6 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1182 @ rdx_v44 (Il2CppClass<System.Object[]>)+40]");
						text = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						bool flag15 = obj7 == null;
						text2 = text18;
						num2 = 0;
						obj = (object)(&obj3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1182 @ rdx_v44 (Il2CppClass<System.Object[]>)+40]");
						string text19 = (string)0;
						string text20 = text18;
						if (flag15)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							throw text21;
						}
					}
					if (array.Length > 2)
					{
						text2 = (string)(array + 48);
						array[2] = text18;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+30]");
						text22 = (string)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+30]");
						bool flag16 = (nint)0 == 0;
						text = text18;
						if (!flag16)
						{
							nint num7 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1252 @ rdx_v42 (Il2CppClass<System.Object[]>)+40]");
							text = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							bool flag17 = obj8 == null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+30]");
							text2 = (string)0;
							num2 = 0;
							obj = (object)(&obj3);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1252 @ rdx_v42 (Il2CppClass<System.Object[]>)+40]");
							string text23 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+30]");
							string text24 = (string)0;
							if (flag17)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								throw text25;
							}
						}
						if (array.Length > 3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+28]");
							obj6 = 0;
							format = "  [{0}] Prefab={1} uses={2} note={3}";
							array3 = array;
							num3 = (int)text3;
							goto IL_08c1;
						}
					}
				}
				throw new IndexOutOfRangeException();
				IL_0326:
				nint num8 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v910 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
				text = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag18 = obj9 == null;
				text12 = text11;
				text2 = text11;
				num2 = 0;
				obj = (object)(&obj3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v910 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
				string text26 = (string)0;
				string text27 = text11;
				if (!flag18)
				{
					goto IL_039b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				throw text28;
				IL_08c1:
				array3[3] = text22;
				string message2 = string.Format(format, array3);
				Debug.Log(message2);
				list2 = queue;
				text3++;
				bool flag19 = queue == null;
				num2 = 0;
				obj = 0;
				text = null;
				text2 = text3;
				if (flag19)
				{
					break;
				}
				num = 0;
				obj2 = 0;
				text4 = null;
				text5 = text3;
				continue;
				IL_06ff:
				nint num9 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v992 @ rdx_v26 (Il2CppClass<System.Object[]>)+40]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag20 = obj11 == null;
				text17 = text16;
				num2 = 0;
				obj = (object)(&obj3);
				string text29 = text16;
				if (!flag20)
				{
					goto IL_075c;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				throw obj12;
				IL_075c:
				array2[1] = text17;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				if (obj13 != null)
				{
					nint num10 = (nint)array2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1198 @ rdx_v23 (Il2CppClass<System.Object[]>)+40]");
					object obj14 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag21 = obj15 == null;
					num2 = 0;
					obj = (object)(&obj3);
					object obj16 = obj13;
					if (flag21)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						throw text30;
					}
				}
				array2[2] = obj13;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+30]");
				text22 = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+30]");
				if ((nint)0 != 0)
				{
					nint num11 = (nint)array2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1266 @ rdx_v21 (Il2CppClass<System.Object[]>)+40]");
					object obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag22 = obj18 == null;
					num2 = 0;
					obj = (object)(&obj3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+30]");
					string text31 = (string)0;
					if (flag22)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						throw obj19;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v249 @ stack_-40_v3+28]");
				object obj20 = 0;
				string text32 = text3;
				format = "  [{0}] Pool={1} uses={2} note={3}";
				array3 = array2;
				goto IL_08c1;
			}
		}
		throw new NullReferenceException();
	}

	public void EnqueuePool(ArticlePoolDefinition pool, int count = 1, string note = "")
	{
		if (pool != null && count > 0)
		{
			QueueEntry queueEntry = new QueueEntry();
			queueEntry.remainingUses = 1;
			queueEntry.isPool = true;
			queueEntry.pool = pool;
			queueEntry.remainingUses = count;
			queueEntry.note = note;
			queue.Add(queueEntry);
			if (logDebug)
			{
				string arg = pool.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"[ArticlePoolQueueManager] Enqueued Pool='{arg}' x{arg2}. {note}";
				Debug.Log(message);
			}
		}
	}

	public void EnqueueSpecificArticle(GameObject articlePrefab, int count = 1, string note = "")
	{
		if (articlePrefab != null && count > 0)
		{
			QueueEntry queueEntry = new QueueEntry();
			queueEntry.remainingUses = 1;
			queueEntry.isPool = false;
			queueEntry.prefab = articlePrefab;
			queueEntry.remainingUses = count;
			queueEntry.note = note;
			queue.Add(queueEntry);
			if (logDebug)
			{
				string arg = articlePrefab.name;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object arg2 = default(object);
				string message = $"[ArticlePoolQueueManager] Enqueued Prefab='{arg}' x{arg2}. {note}";
				Debug.Log(message);
			}
		}
	}

	public unsafe List<GameObject> RequestSpecialPicks(int desiredCount, System.Random rng, ISet<GameObject> exclude)
	{
		//IL_0058: Expected O, but got I4
		//IL_0061: Expected O, but got I4
		//IL_0436: Expected O, but got I
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Expected O, but got Unknown
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Expected O, but got Unknown
		//IL_0194: Expected O, but got I
		//IL_0110: Expected O, but got I
		//IL_0171: Expected O, but got I
		//IL_0300: Expected O, but got I
		//IL_0233: Expected O, but got I
		//IL_02e2: Expected O, but got I
		//IL_0215: Expected O, but got I
		//IL_026d: Expected O, but got I
		//IL_0370: Expected O, but got I
		List<GameObject> list = new List<GameObject>();
		if (desiredCount > 0)
		{
			List<QueueEntry> list2 = queue;
			if (list2._size != 0)
			{
				List<int> list3 = new List<int>();
				List<QueueEntry> list4 = queue;
				object obj = 0;
				object obj2 = 0;
				int num = desiredCount;
				System.Random rng2 = rng;
				object obj3 = default(object);
				bool advanceSequentialOnSuccess = default(bool);
				object arg2 = default(object);
				object obj5 = default(object);
				object arg5 = default(object);
				for (; (nint)obj2 < list4._size && list._size < num; list4 = queue, obj++, obj2 = obj, num = desiredCount)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+28]");
					string text3;
					GameObject gameObject;
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+10]");
						if ((nint)0 == 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+20]");
							bool flag = (UnityEngine.Object)0 != null;
							bool flag2 = !flag;
							gameObject = null;
							if (!flag2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
								bool flag3 = obj3 != null;
								gameObject = null;
								if (!flag3)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+20]");
									gameObject = (GameObject)0;
								}
							}
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+18]");
							GameObject gameObject2 = TryPickFromPool((ArticlePoolDefinition)0, rng2, exclude, advanceSequentialOnSuccess);
							gameObject = gameObject2;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+28]");
						_ = -1;
						bool flag4 = gameObject != null;
						if (!flag4)
						{
							if (logDebug != flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+10]");
								UnityEngine.Object obj4;
								string text;
								if ((nint)0 != (flag4 ? 1 : 0))
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+18]");
									obj4 = (UnityEngine.Object)0;
									text = "Pool='";
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+20]");
									obj4 = (UnityEngine.Object)0;
									text = "Prefab='";
								}
								string text2 = obj4?.name;
								string arg = text + text2 + "'";
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
								text3 = $"[ArticlePoolQueueManager] Special attempt consumed without placement from {arg}. Remaining uses: {arg2}";
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+28]");
								obj5 = 0;
								goto IL_037b;
							}
						}
						else
						{
							list.Add(gameObject);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
							if (logDebug)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+10]");
								UnityEngine.Object obj6;
								string text4;
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+18]");
									obj6 = (UnityEngine.Object)0;
									text4 = "Pool='";
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+20]");
									obj6 = (UnityEngine.Object)0;
									text4 = "Prefab='";
								}
								string text5 = obj6?.name;
								string arg3 = text4 + text5 + "'";
								if ((object)gameObject != null)
								{
									string arg4 = gameObject.name;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									text3 = $"[ArticlePoolQueueManager] Special pick: {arg4} from {arg3}. Remaining uses: {arg5}";
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+28]");
									object obj7 = 0;
									goto IL_037b;
								}
								return (List<GameObject>)(object)new NullReferenceException();
							}
						}
						goto IL_0391;
					}
					list3.Add((int)(&obj5));
					obj5 = obj;
					continue;
					IL_037b:
					Debug.Log(text3);
					gameObject = (GameObject)(object)text3;
					goto IL_0391;
					IL_0391:
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v201 @ stack_-48_v5+28]");
					if ((nint)0 <= (nint)0)
					{
						list3.Add((int)(&obj5));
						obj5 = obj;
					}
					rng2 = rng;
				}
				bool flag5 = (nint)list3 < 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v422 @ rax_v10 (System.Collections.Generic.List`1<System.Int32>)+18]");
				object obj8 = -1;
				if (!flag5)
				{
					int index = default(int);
					do
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						queue.RemoveAt(index);
						obj8--;
					}
					while ((nint)queue >= 0);
				}
			}
		}
		return list;
	}

	public List<GameObject> PickFromPool(ArticlePoolDefinition pool, int desiredCount, System.Random rng, ISet<GameObject> exclude)
	{
		//IL_004a: Expected O, but got I4
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		List<GameObject> list = new List<GameObject>();
		if (pool != null && desiredCount > 0)
		{
			object obj = 0;
			ISet<GameObject> set = default(ISet<GameObject>);
			bool advanceSequentialOnSuccess = default(bool);
			while (true)
			{
				GameObject gameObject = TryPickFromPool(pool, rng, set, advanceSequentialOnSuccess);
				if (!(gameObject != null))
				{
					break;
				}
				if (list != null)
				{
					list.Add(gameObject);
					if (set != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						obj++;
						if ((nint)obj >= desiredCount)
						{
							break;
						}
						continue;
					}
				}
				return (List<GameObject>)(object)new NullReferenceException();
			}
		}
		return list;
	}

	private unsafe GameObject TryPickFromPool(ArticlePoolDefinition pool, System.Random rng, ISet<GameObject> exclude, bool advanceSequentialOnSuccess)
	{
		//IL_017f: Expected O, but got I4
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected I4, but got Unknown
		GameObject gameObject;
		if (pool != null)
		{
			if ((object)pool == null)
			{
				goto IL_0244;
			}
			IReadOnlyList<GameObject> uniqueArticlePrefabs = pool.UniqueArticlePrefabs;
			if (uniqueArticlePrefabs != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if (obj != null)
				{
					if (pool.selection != ArticlePoolDefinition.SelectionMode.Random)
					{
						if (_sequentialNextIndex == null)
						{
							goto IL_0244;
						}
						bool flag = default(bool);
						int num = default(int);
						int num2 = default(int);
						if (_sequentialNextIndex.TryGetValue(pool, out var _))
						{
							flag = ((Dictionary<ArticlePoolDefinition, int>)null).TryGetValue((ArticlePoolDefinition)(object)typeof(IReadOnlyCollection<GameObject>), out *(int*)uniqueArticlePrefabs);
							bool flag2 = (flag ? 1 : 0) <= (false ? 1 : 0);
							num = 0;
							num2 = 0;
							if (flag2)
							{
								goto IL_023a;
							}
						}
						object obj2 = default(object);
						while (true)
						{
							gameObject = uniqueArticlePrefabs.get_Item(num2);
							if (gameObject != null)
							{
								if (exclude == null)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
								if (obj2 == null)
								{
									break;
								}
							}
							object obj3 = num2 + 1;
							num++;
							int num3 = obj3 % flag;
							if (num < (flag ? 1 : 0))
							{
								num2 = num3;
								continue;
							}
							goto IL_023a;
						}
						object obj4 = default(object);
						if (obj4 != null)
						{
							if (_sequentialNextIndex == null)
							{
								goto IL_0244;
							}
							object obj5 = default(object);
							_sequentialNextIndex.set_Item(pool, (int)(&obj5));
						}
					}
					else
					{
						gameObject = TryPickFromDeck(pool, rng, exclude);
					}
					goto IL_02a6;
				}
			}
		}
		goto IL_023a;
		IL_02a6:
		return gameObject;
		IL_023a:
		gameObject = null;
		goto IL_02a6;
		IL_0244:
		return (GameObject)(object)new NullReferenceException();
	}

	private GameObject TryPickFromDeck(ArticlePoolDefinition pool, System.Random rng, ISet<GameObject> exclude)
	{
		//IL_011e: Expected I, but got O
		bool flag = _passRng == null;
		System.Random rng2 = rng;
		if (!flag)
		{
			rng2 = _passRng;
		}
		List<GameObject> list = default(List<GameObject>);
		if (!_passDecks.TryGetValue(pool, out var _))
		{
			list = BuildShuffledDeck(pool, rng2);
			_passDecks.set_Item(pool, list);
		}
		int num = 0;
		List<GameObject> list2 = list;
		int num2 = 0;
		UnityEngine.Object obj = default(UnityEngine.Object);
		object obj2 = default(object);
		object arg3 = default(object);
		while (true)
		{
			if (num2 < list2._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (!(obj != null))
				{
					goto IL_012c;
				}
				bool flag2 = exclude == null;
				nint num3 = 0;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
					bool flag3 = obj2 == null;
					num3 = (nint)obj;
					if (!flag3)
					{
						goto IL_012c;
					}
				}
				list.RemoveAt(num);
				if (logDebug)
				{
					if ((object)obj == null)
					{
						break;
					}
					string arg = obj.name;
					if ((object)pool == null)
					{
						break;
					}
					string arg2 = pool.name;
					if (list == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string message = $"[ArticlePoolQueueManager] Deck pick '{arg}' from pool '{arg2}'. Remaining in deck: {arg3}";
					Debug.Log(message);
				}
				if (list._size == 0)
				{
					List<GameObject> value2 = BuildShuffledDeck(pool, rng2);
					_passDecks.set_Item(pool, value2);
					if (logDebug)
					{
						string text = pool.name;
						string message2 = "[ArticlePoolQueueManager] Deck for pool '" + text + "' exhausted — reshuffled for next picks.";
						Debug.Log(message2);
					}
				}
				return (GameObject)obj;
			}
			List<GameObject> value3 = BuildShuffledDeck(pool, rng2);
			_passDecks.set_Item(pool, value3);
			if (logDebug)
			{
				string text2 = pool.name;
				string message3 = "[ArticlePoolQueueManager] All deck cards excluded for pool '" + text2 + "' — reshuffled.";
				Debug.Log(message3);
			}
			return null;
			IL_012c:
			num++;
			list2 = list;
			num2 = num;
		}
		return (GameObject)(object)new NullReferenceException();
	}

	private static List<GameObject> BuildShuffledDeck(ArticlePoolDefinition pool, System.Random rng)
	{
		//IL_006b: Expected I, but got O
		//IL_00f6: Expected O, but got I4
		//IL_00a3: Expected O, but got I
		//IL_00ac: Expected O, but got I4
		//IL_0239: Expected O, but got I
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_0128: Expected I, but got O
		//IL_01b3: Expected O, but got I4
		//IL_0160: Expected O, but got I
		//IL_0169: Expected O, but got I4
		//IL_027c: Expected O, but got I
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		ArticlePoolDefinition articlePoolDefinition = default(ArticlePoolDefinition);
		List<GameObject> list;
		if ((object)articlePoolDefinition != null)
		{
			IReadOnlyList<GameObject> uniqueArticlePrefabs = articlePoolDefinition.UniqueArticlePrefabs;
			if (uniqueArticlePrefabs != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				int capacity = default(int);
				list = new List<GameObject>(capacity);
				IEnumerator<GameObject> enumerator = uniqueArticlePrefabs.GetEnumerator();
				UnityEngine.Object obj = default(UnityEngine.Object);
				object obj10 = default(object);
				object obj11 = default(object);
				object obj20 = default(object);
				UnityEngine.Object obj21 = default(UnityEngine.Object);
				while (true)
				{
					object obj2;
					object obj9;
					if ((object)obj != null)
					{
						nint num = (nint)obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r10_v7 (Il2CppClass<UnityEngine.Object>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_00e3;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r10_v7 (Il2CppClass<UnityEngine.Object>)+B0]");
						obj2 = 0;
						object obj3 = 0;
						while (true)
						{
							object obj4 = obj3 + obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v10+v490 @ rax_v48*8]");
							if (0 == (nint)typeof(IEnumerator))
							{
								break;
							}
							obj3++;
							object obj5 = obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ r10_v7 (Il2CppClass<UnityEngine.Object>)+12E]");
							if ((nint)obj5 < 0)
							{
								continue;
							}
							goto IL_00e3;
						}
						object obj6 = obj3 + obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ r8_v10+8+v546 @ rcx_v41*8]");
						object obj7 = (nint)0 << 4;
						object obj8 = obj7 + 312;
						obj9 = obj8 + num;
						goto IL_0469;
					}
					throw new NullReferenceException();
					IL_00e3:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj2 = 0;
					obj9 = obj10;
					goto IL_0469;
					IL_0469:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v551 @ rdx_v12] (should have been resolved before IL gen)");
					if (obj11 == null)
					{
						break;
					}
					object obj12;
					object obj19;
					if ((object)obj != null)
					{
						nint num2 = (nint)obj;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ r10_v8 (Il2CppClass<UnityEngine.Object>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_01a0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ r10_v8 (Il2CppClass<UnityEngine.Object>)+B0]");
						obj12 = 0;
						object obj13 = 0;
						while (true)
						{
							object obj14 = obj13 + obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v667 @ r8_v19+v593 @ rcx_v34*8]");
							if (0 == (nint)typeof(IEnumerator<GameObject>))
							{
								break;
							}
							obj13++;
							object obj15 = obj13;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ r10_v8 (Il2CppClass<UnityEngine.Object>)+12E]");
							if ((nint)obj15 < 0)
							{
								continue;
							}
							goto IL_01a0;
						}
						object obj16 = obj13 + obj13;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v667 @ r8_v19+8+v662 @ rcx_v36*8]");
						object obj17 = (nint)0 << 4;
						object obj18 = obj17 + 312;
						obj19 = obj18 + num2;
						goto IL_0490;
					}
					throw new NullReferenceException();
					IL_01a0:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					obj12 = 0;
					obj19 = obj20;
					goto IL_0490;
					IL_0490:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v668 @ rdx_v22] (should have been resolved before IL gen)");
					if (obj21 != null)
					{
						if (list == null)
						{
							throw new NullReferenceException();
						}
						list.Add((GameObject)obj21);
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1804ADBC0");
				if (list != null)
				{
					int num3 = list._size - 1;
					if (num3 <= 0)
					{
						goto IL_0386;
					}
					GameObject value = default(GameObject);
					GameObject value2 = default(GameObject);
					while (rng != null)
					{
						int maxValue = num3 + 1;
						int index = rng.Next(maxValue);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						list.set_Item(num3, value);
						list.set_Item(index, value2);
						num3--;
						if (num3 > 0)
						{
							continue;
						}
						goto IL_0386;
					}
				}
			}
		}
		throw new NullReferenceException();
		IL_0386:
		return list;
	}

	public ArticlePoolQueueManager()
	{
		List<QueueEntry> list = new List<QueueEntry>();
		queue = list;
		_sequentialNextIndex = new Dictionary<ArticlePoolDefinition, int>();
		_passDecks = new Dictionary<ArticlePoolDefinition, List<GameObject>>();
		base._002Ector();
	}

	static ArticlePoolQueueManager()
	{
		Dictionary<string, ArticlePoolDefinition> articlePools = new Dictionary<string, ArticlePoolDefinition>();
		ArticlePools = articlePools;
	}
}
