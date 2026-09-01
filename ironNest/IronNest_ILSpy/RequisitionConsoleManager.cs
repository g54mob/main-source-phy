using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class RequisitionConsoleManager : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<PunchcardDefinitionV2, PunchcardDefinitionV2> _003C_003E9__14_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal PunchcardDefinitionV2 _003CAddNewCardsToDeck_003Eb__14_0(PunchcardDefinitionV2 d)
		{
			PunchcardDefinitionV2 punchcardDefinitionV = UnityEngine.Object.Instantiate(d);
			if ((object)punchcardDefinitionV != null)
			{
				punchcardDefinitionV.RemainingUses = punchcardDefinitionV.MaxUses;
				return punchcardDefinitionV;
			}
			return (PunchcardDefinitionV2)(object)new NullReferenceException();
		}
	}

	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public HashSet<string> present;

		internal bool _003CFilterNewDefinitions_003Eb__0(PunchcardDefinitionV2 d)
		{
			//IL_00c7: Expected I4, but got O
			if (d != null)
			{
				if ((object)d != null)
				{
					if (string.IsNullOrEmpty(d.ID))
					{
						goto IL_00b3;
					}
					if (present != null)
					{
						bool flag = present.Contains(d.ID);
						return (byte)((flag ? 1u : 0u) ^ 1u) != 0;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_00b3;
			IL_00b3:
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public PunchcardDefinitionV2 captured;

		internal void _003CSpawnIntoDeck_003Eb__0(DraggableItem instance)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (obj != null)
			{
				_ = captured;
				((PunchcardRuntime)obj).UpdateVisuals();
			}
		}
	}

	public static RequisitionConsoleManager Instance;

	public DragSurface DragSurface;

	public DraggableItemDeckArea DeckArea;

	public RequisitionSlot RequisitionSlot;

	private Dictionary<string, PunchcardDefinitionV2> _003CAllDefinitions_003Ek__BackingField;

	private bool initialized;

	public Dictionary<string, PunchcardDefinitionV2> AllDefinitions
	{
		get
		{
			return _003CAllDefinitions_003Ek__BackingField;
		}
		private set
		{
			_003CAllDefinitions_003Ek__BackingField = value;
		}
	}

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		InitializeConsole();
	}

	public void InitializeConsole()
	{
		//IL_0022: Expected O, but got I4
		//IL_0048: Expected O, but got I4
		//IL_0082: Expected O, but got I4
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_011f: Expected O, but got I4
		//IL_0128: Expected O, but got I4
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		if (initialized)
		{
			return;
		}
		bool flag = DragSurface;
		object obj = 1;
		if (!flag)
		{
			Debug.LogError("[RequisitionConsoleManager] DragSurface not assigned.");
			obj = 0;
		}
		if (!DeckArea)
		{
			Debug.LogError("[RequisitionConsoleManager] DeckArea not assigned.");
			obj = 0;
		}
		if (!RequisitionSlot)
		{
			Debug.LogError("[RequisitionConsoleManager] RequisitionSlot not assigned.");
		}
		else if (obj != null)
		{
			Instance = this;
			Dictionary<string, PunchcardDefinitionV2> dictionary = new Dictionary<string, PunchcardDefinitionV2>();
			_003CAllDefinitions_003Ek__BackingField = dictionary;
			PunchcardDefinitionV2[] array = Resources.LoadAll<PunchcardDefinitionV2>("Punchcards");
			object obj2 = array + 32;
			object obj3 = 0;
			object obj4 = 0;
			while ((nint)obj4 < array.Length)
			{
				PunchcardDefinitionV2 punchcardDefinitionV = (PunchcardDefinitionV2)obj2;
				_003CAllDefinitions_003Ek__BackingField.set_Item(punchcardDefinitionV.ID, (PunchcardDefinitionV2)obj2);
				obj3++;
				obj2 += 8;
				obj4 = obj3;
			}
			RequisitionSlot requisitionSlot = RequisitionSlot;
			if ((object)requisitionSlot.lever != null)
			{
				requisitionSlot.lever.SetActive(active: false);
			}
			initialized = true;
		}
	}

	private bool ValidateRefs()
	{
		bool flag = DragSurface;
		bool result = true;
		if (!flag)
		{
			Debug.LogError("[RequisitionConsoleManager] DragSurface not assigned.");
			result = false;
		}
		if (!DeckArea)
		{
			Debug.LogError("[RequisitionConsoleManager] DeckArea not assigned.");
			result = false;
		}
		if (!RequisitionSlot)
		{
			Debug.LogError("[RequisitionConsoleManager] RequisitionSlot not assigned.");
			return false;
		}
		return result;
	}

	public void EnsureCards(List<PunchcardDefinitionV2> cards)
	{
		List<PunchcardDefinitionV2> list = FilterNewDefinitions(cards);
		if (list._size == 0)
		{
			return;
		}
		Func<PunchcardDefinitionV2, PunchcardDefinitionV2> selector = _003C_003Ec._003C_003E9__14_0;
		if (_003C_003Ec._003C_003E9__14_0 == null)
		{
			selector = (_003C_003Ec._003C_003E9__14_0 = delegate(PunchcardDefinitionV2 d)
			{
				PunchcardDefinitionV2 punchcardDefinitionV = UnityEngine.Object.Instantiate(d);
				if ((object)punchcardDefinitionV != null)
				{
					punchcardDefinitionV.RemainingUses = punchcardDefinitionV.MaxUses;
					return punchcardDefinitionV;
				}
				return (PunchcardDefinitionV2)(object)new NullReferenceException();
			});
		}
		IEnumerable<PunchcardDefinitionV2> source = Enumerable.Select(list, selector);
		List<PunchcardDefinitionV2> defs = Enumerable.ToList(source);
		SpawnIntoDeck(defs);
	}

	public void AddNewCardsToDeck(List<PunchcardDefinitionV2> newCards)
	{
		List<PunchcardDefinitionV2> list = FilterNewDefinitions(newCards);
		if (list._size == 0)
		{
			return;
		}
		Func<PunchcardDefinitionV2, PunchcardDefinitionV2> selector = _003C_003Ec._003C_003E9__14_0;
		if (_003C_003Ec._003C_003E9__14_0 == null)
		{
			selector = (_003C_003Ec._003C_003E9__14_0 = delegate(PunchcardDefinitionV2 d)
			{
				PunchcardDefinitionV2 punchcardDefinitionV = UnityEngine.Object.Instantiate(d);
				if ((object)punchcardDefinitionV != null)
				{
					punchcardDefinitionV.RemainingUses = punchcardDefinitionV.MaxUses;
					return punchcardDefinitionV;
				}
				return (PunchcardDefinitionV2)(object)new NullReferenceException();
			});
		}
		IEnumerable<PunchcardDefinitionV2> source = Enumerable.Select(list, selector);
		List<PunchcardDefinitionV2> defs = Enumerable.ToList(source);
		SpawnIntoDeck(defs);
	}

	public void AddSetCardsToDeck(List<PunchcardDefinitionV2> newCards)
	{
		List<PunchcardDefinitionV2> list = FilterNewDefinitions(newCards);
		if (list._size != 0)
		{
			SpawnIntoDeck(list);
		}
	}

	public void RebuildDeck(List<PunchcardDefinitionV2> exactCards)
	{
		ClearAllCards();
		Func<PunchcardDefinitionV2, PunchcardDefinitionV2> selector = UnityEngine.Object.Instantiate;
		IEnumerable<PunchcardDefinitionV2> source = Enumerable.Select(exactCards, selector);
		List<PunchcardDefinitionV2> list = Enumerable.ToList(source);
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 54 Invalid \"Jump target not found in method: 0x18045C6C0\"");
	}

	public PunchcardRuntime[] GetAllCards()
	{
		return UnityEngine.Object.FindObjectsByType<PunchcardRuntime>(FindObjectsInactive.Include, FindObjectsSortMode.None);
	}

	public void ClearAllCards()
	{
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Expected O, but got Unknown
		DraggableItemDeckArea deckArea = DeckArea;
		List<DraggableItem> items = deckArea.items;
		int version = items._version + 1;
		items._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj = default(object);
		if (obj == null)
		{
			items._size = 0;
		}
		else
		{
			items._size = 0;
			if (items._size > 0)
			{
				Array.Clear(items._items, 0, items._size);
			}
		}
		DragSurface dragSurface = DragSurface;
		List<DraggableItem> items2 = dragSurface.items;
		int version2 = items2._version + 1;
		items2._version = version2;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<DraggableItem>())
		{
			items2._size = 0;
		}
		else
		{
			items2._size = 0;
			if (items2._size > 0)
			{
				Array.Clear(items2._items, 0, items2._size);
			}
		}
		RequisitionSlot requisitionSlot = RequisitionSlot;
		requisitionSlot.itemSlot.ClearSlot();
		PunchcardRuntime[] array = UnityEngine.Object.FindObjectsByType<PunchcardRuntime>(FindObjectsInactive.Include, FindObjectsSortMode.None);
		object obj2 = array + 32;
		int num = 0;
		for (int num2 = 0; num2 < array.Length; num2 = num)
		{
			GameObject obj3 = ((Component)obj2).gameObject;
			UnityEngine.Object.Destroy(obj3);
			num++;
			obj2 += 8;
		}
	}

	private List<PunchcardDefinitionV2> FilterNewDefinitions(List<PunchcardDefinitionV2> source)
	{
		//IL_00f4: Expected O, but got I4
		//IL_0157: Expected O, but got I
		//IL_01f9: Expected O, but got I4
		//IL_02b8: Expected O, but got I
		_003C_003Ec__DisplayClass19_0 CS_0024_003C_003E8__locals9 = new _003C_003Ec__DisplayClass19_0();
		if (source != null)
		{
			HashSet<string> present = new HashSet<string>();
			if (CS_0024_003C_003E8__locals9 != null)
			{
				CS_0024_003C_003E8__locals9.present = present;
				DraggableItemDeckArea deckArea = DeckArea;
				if ((object)DeckArea != null && deckArea.items != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<DraggableItem>.Enumerator enumerator = default(List<DraggableItem>.Enumerator);
					HashSet<string> hashSet = default(HashSet<string>);
					HashSet<string> hashSet2 = default(HashSet<string>);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (hashSet != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
							UnityEngine.Object obj = ((hashSet2 == null) ? null : ((UnityEngine.Object)hashSet2._count));
							if (obj != null)
							{
								int count = hashSet2._count;
								HashSet<string> present2 = CS_0024_003C_003E8__locals9.present;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v696 @ rax_v56 (System.Int32)+18]");
								present2.Add((string)0);
							}
							continue;
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					DragSurface dragSurface = DragSurface;
					if ((object)DragSurface != null && dragSurface.items != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						List<DraggableItem>.Enumerator enumerator2 = default(List<DraggableItem>.Enumerator);
						while (enumerator2.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							if (hashSet2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
								UnityEngine.Object obj2 = ((hashSet == null) ? null : ((UnityEngine.Object)hashSet._count));
								if (obj2 != null)
								{
									if (hashSet == null)
									{
										throw new NullReferenceException();
									}
									int count2 = hashSet._count;
									if (hashSet._count == 0)
									{
										throw new NullReferenceException();
									}
									if (CS_0024_003C_003E8__locals9.present == null)
									{
										throw new NullReferenceException();
									}
									HashSet<string> present3 = CS_0024_003C_003E8__locals9.present;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rax_v44 (System.Int32)+18]");
									present3.Add((string)0);
								}
								continue;
							}
							throw new NullReferenceException();
						}
						enumerator2.Dispose();
						RequisitionSlot requisitionSlot = RequisitionSlot;
						if ((object)RequisitionSlot != null)
						{
							PunchcardRuntime currentCard = requisitionSlot.CurrentCard;
							bool flag = (object)requisitionSlot.CurrentCard == null;
							UnityEngine.Object obj3 = null;
							if (!flag)
							{
								obj3 = currentCard.CurrentDefinition;
							}
							if (!(obj3 != null))
							{
								goto IL_03ca;
							}
							if ((object)requisitionSlot.CurrentCard != null)
							{
								PunchcardDefinitionV2 currentDefinition = currentCard.CurrentDefinition;
								if ((object)currentCard.CurrentDefinition != null && CS_0024_003C_003E8__locals9.present != null)
								{
									CS_0024_003C_003E8__locals9.present.Add(currentDefinition.ID);
									goto IL_03ca;
								}
							}
						}
					}
				}
			}
			return (List<PunchcardDefinitionV2>)(object)new NullReferenceException();
		}
		return new List<PunchcardDefinitionV2>();
		IL_03ca:
		Func<PunchcardDefinitionV2, bool> predicate = delegate(PunchcardDefinitionV2 d)
		{
			//IL_00c7: Expected I4, but got O
			if (d != null)
			{
				if ((object)d != null)
				{
					if (string.IsNullOrEmpty(d.ID))
					{
						goto IL_00b3;
					}
					if (CS_0024_003C_003E8__locals9.present != null)
					{
						bool flag2 = CS_0024_003C_003E8__locals9.present.Contains(d.ID);
						return (byte)((flag2 ? 1u : 0u) ^ 1u) != 0;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_00b3;
			IL_00b3:
			return false;
		};
		IEnumerable<PunchcardDefinitionV2> source2 = Enumerable.Where(source, predicate);
		return Enumerable.ToList(source2);
	}

	private void SpawnIntoDeck(List<PunchcardDefinitionV2> defs)
	{
		//IL_0109: Expected O, but got I
		List<DeckSpawnEntry> list = new List<DeckSpawnEntry>();
		if ((object)RequisitionSlot != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			if (defs != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				List<PunchcardDefinitionV2>.Enumerator enumerator = default(List<PunchcardDefinitionV2>.Enumerator);
				UnityEngine.Object obj = default(UnityEngine.Object);
				DraggableItem draggableItem = default(DraggableItem);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					_003C_003Ec__DisplayClass20_0 CS_0024_003C_003E8__locals3 = new _003C_003Ec__DisplayClass20_0();
					if (!obj)
					{
						continue;
					}
					if (CS_0024_003C_003E8__locals3 != null)
					{
						CS_0024_003C_003E8__locals3.captured = (PunchcardDefinitionV2)obj;
						DeckSpawnEntry deckSpawnEntry = new DeckSpawnEntry();
						if ((object)obj != null)
						{
							if (deckSpawnEntry != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ stack_-58_v7 (UnityEngine.Object)+18]");
								deckSpawnEntry.Label = (string)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ stack_-58_v7 (UnityEngine.Object)+40]");
								DraggableItem prefabOverride;
								if ((nint)0 != 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
									prefabOverride = draggableItem;
								}
								else
								{
									prefabOverride = null;
								}
								deckSpawnEntry.PrefabOverride = prefabOverride;
								Action<DraggableItem> onSpawned = delegate
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
									UnityEngine.Object obj2 = default(UnityEngine.Object);
									if (obj2 != null)
									{
										_ = CS_0024_003C_003E8__locals3.captured;
										((PunchcardRuntime)obj2).UpdateVisuals();
									}
								};
								deckSpawnEntry.OnSpawned = onSpawned;
								if (list != null)
								{
									list.Add(deckSpawnEntry);
									continue;
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				if ((object)DeckArea != null)
				{
					ItemSlot slot = default(ItemSlot);
					DeckArea.AddItems(list, DragSurface, slot);
					return;
				}
			}
		}
		throw new NullReferenceException();
	}
}
