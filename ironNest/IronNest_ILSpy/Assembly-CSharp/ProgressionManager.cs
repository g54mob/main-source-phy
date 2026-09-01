using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using Cpp2ILInjected;
using Newtonsoft.Json;
using SleepyNodes;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
	private static ProgressionManager _003CInstance_003Ek__BackingField;

	private UserProgression _003CUserProgression_003Ek__BackingField;

	private Dictionary<string, OperationState> operationStates;

	private OperationState _003CCurrentOperation_003Ek__BackingField;

	public static ProgressionManager Instance
	{
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		private set
		{
			_003CInstance_003Ek__BackingField = value;
		}
	}

	public UserProgression UserProgression
	{
		get
		{
			return _003CUserProgression_003Ek__BackingField;
		}
		private set
		{
			_003CUserProgression_003Ek__BackingField = value;
		}
	}

	public IReadOnlyDictionary<string, OperationState> OperationStates => operationStates;

	public OperationState CurrentOperation
	{
		get
		{
			return _003CCurrentOperation_003Ek__BackingField;
		}
		private set
		{
			_003CCurrentOperation_003Ek__BackingField = value;
		}
	}

	private string ProgressionSaveRoot
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180742270");
			object obj = default(object);
			string path;
			string path2;
			if (obj == null)
			{
				string persistentDataPath = Application.persistentDataPath;
				path = "Live";
				path2 = persistentDataPath;
			}
			else
			{
				string persistentDataPath2 = Application.persistentDataPath;
				path = "Editor";
				path2 = persistentDataPath2;
			}
			return Path.Combine(path2, path);
		}
	}

	private string ProgressionPath
	{
		get
		{
			string progressionSaveRoot = ProgressionSaveRoot;
			return Path.Combine(progressionSaveRoot, "progression.dat");
		}
	}

	private string OperationFilePattern
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A2D8]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			return "operation.*.dat";
		}
	}

	private void Awake()
	{
		//IL_0028: Expected O, but got I4
		//IL_005e: Expected O, but got I4
		bool flag = _003CInstance_003Ek__BackingField != null;
		bool flag2 = !flag;
		object obj = 0;
		if (!flag2)
		{
			bool flag3 = _003CInstance_003Ek__BackingField != this;
			bool flag4 = !flag3;
			obj = 0;
			if (!flag4)
			{
				GameObject obj2 = base.gameObject;
				UnityEngine.Object.Destroy(obj2);
				return;
			}
		}
		_003CInstance_003Ek__BackingField = this;
		GameObject target = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(target);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		string progressionSaveRoot = ProgressionSaveRoot;
		object arg = default(object);
		string message = $"[ProgressionManager] Setting up saves for app {arg} at: {progressionSaveRoot}";
		Debug.Log(message);
		string progressionSaveRoot2 = ProgressionSaveRoot;
		DirectoryInfo directoryInfo = Directory.CreateDirectory(progressionSaveRoot2);
		LoadAll();
	}

	public void StartOperation(OperationGraph operation)
	{
		OperationState orCreateOperation = GetOrCreateOperation(operation);
		if (orCreateOperation != null)
		{
			_003CCurrentOperation_003Ek__BackingField = orCreateOperation;
			UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
			userProgression.LastOperationID = operation.OperationID;
			SaveOperation(operation.OperationID);
			SaveProgression();
		}
	}

	public OperationState GetOperation(string id)
	{
		if (operationStates != null)
		{
			bool flag = operationStates.TryGetValue(id, out var value);
			return value;
		}
		return (OperationState)(object)new NullReferenceException();
	}

	public void SaveAll()
	{
		SaveProgression();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
		Dictionary<string, OperationState>.Enumerator enumerator = default(Dictionary<string, OperationState>.Enumerator);
		string operationId = default(string);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
			SaveOperation(operationId);
		}
		enumerator.Dispose();
	}

	public void SaveProgression()
	{
		string progressionSaveRoot = ProgressionSaveRoot;
		string path = Path.Combine(progressionSaveRoot, "progression.dat");
		SaveToFile(_003CUserProgression_003Ek__BackingField, path);
	}

	public void SaveOperation(string operationId)
	{
		if (operationStates.TryGetValue(operationId, out var value))
		{
			string text = operationId.Replace("/", "_");
			string text2 = text.Replace("\\", "_");
			string progressionSaveRoot = ProgressionSaveRoot;
			string path = "operation." + text2 + ".dat";
			string path2 = Path.Combine(progressionSaveRoot, path);
			SaveToFile(value, path2);
		}
	}

	public void LoadAll()
	{
		string progressionSaveRoot = ProgressionSaveRoot;
		string text = Path.Combine(progressionSaveRoot, "progression.dat");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18073ADD0");
		UserProgression userProgression = default(UserProgression);
		bool flag = userProgression != null;
		UserProgression userProgression2 = userProgression;
		if (!flag)
		{
			UserProgression userProgression3 = new UserProgression();
			userProgression2 = userProgression3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 97 Invalid \"Jump target not found in method: 0x180453633\"");
		_003CUserProgression_003Ek__BackingField = userProgression2;
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 136 Invalid \"Jump target not found in method: 0x180453633\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 148 Invalid \"Jump target not found in method: 0x18045352C\"");
		List<string> list = null;
	}

	public bool IsCardUnlocked(string cardId)
	{
		//IL_007f: Expected I4, but got O
		if (!string.IsNullOrEmpty(cardId))
		{
			UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
			if (_003CUserProgression_003Ek__BackingField != null && userProgression.UnlockedCards != null)
			{
				return userProgression.UnlockedCards.Contains(cardId);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public bool IsSceneObjectUnlocked(string objectId)
	{
		//IL_007f: Expected I4, but got O
		if (!string.IsNullOrEmpty(objectId))
		{
			UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
			if (_003CUserProgression_003Ek__BackingField != null && userProgression.UnlockedSceneObjects != null)
			{
				return userProgression.UnlockedSceneObjects.Contains(objectId);
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	public bool UnlockSceneObject(string objectId)
	{
		//IL_00eb: Expected I4, but got O
		if (!string.IsNullOrEmpty(objectId))
		{
			UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
			if (_003CUserProgression_003Ek__BackingField != null && userProgression.UnlockedSceneObjects != null)
			{
				if (userProgression.UnlockedSceneObjects.Contains(objectId))
				{
					goto IL_00d7;
				}
				UserProgression userProgression2 = _003CUserProgression_003Ek__BackingField;
				if (_003CUserProgression_003Ek__BackingField != null && userProgression2.UnlockedSceneObjects != null)
				{
					userProgression2.UnlockedSceneObjects.Add(objectId);
					return true;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_00d7;
		IL_00d7:
		return false;
	}

	public List<PunchcardDefinitionV2> BuildUnlockedPunchcards(Dictionary<string, PunchcardDefinitionV2> allDefinitions)
	{
		List<PunchcardDefinitionV2> list = new List<PunchcardDefinitionV2>();
		if (allDefinitions != null)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.s_ordinalIgnoreCase);
			UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
			bool flag = _003CUserProgression_003Ek__BackingField == null;
			ProgressionManager progressionManager = (ProgressionManager)(object)_003CUserProgression_003Ek__BackingField;
			if (flag || userProgression.UnlockedCards == null)
			{
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			string text = default(string);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				if (hashSet != null)
				{
					if (!hashSet.Contains(text) && allDefinitions.TryGetValue(text, out var value) && value != null)
					{
						PunchcardDefinitionV2 punchcardDefinitionV = UnityEngine.Object.Instantiate(value);
						if ((object)value == null)
						{
							throw new NullReferenceException();
						}
						int remainingUses = GetRemainingUses(text, value.MaxUses);
						if ((object)punchcardDefinitionV == null)
						{
							throw new NullReferenceException();
						}
						punchcardDefinitionV.RemainingUses = remainingUses;
						if (list == null)
						{
							throw new NullReferenceException();
						}
						list.Add(punchcardDefinitionV);
						hashSet.Add(text);
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
		}
		return list;
	}

	public unsafe List<string> UnlockPunchcards(IEnumerable<PunchcardDefinitionV2> punchcards)
	{
		//IL_0017: Expected O, but got Ref
		//IL_0020: Expected O, but got I4
		//IL_0104: Expected O, but got I4
		//IL_00b2: Expected O, but got I
		//IL_012d: Expected I, but got O
		//IL_03df: Expected O, but got I
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_0169: Expected O, but got I
		//IL_017b: Expected I, but got O
		//IL_01ee: Expected O, but got I
		//IL_0244: Expected O, but got I
		//IL_02c8: Expected O, but got I
		//IL_02da: Expected I, but got O
		//IL_025e: Expected O, but got I
		//IL_0352: Expected O, but got I
		//IL_03a8: Expected O, but got I
		//IL_03b1: Expected O, but got I4
		List<string> list = new List<string>();
		if (punchcards != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			object obj = (object)(&obj2);
			object obj3 = 0;
			UnityEngine.Object obj4 = null;
			object obj5 = default(object);
			UnityEngine.Object obj16 = default(UnityEngine.Object);
			object obj17 = default(object);
			while (true)
			{
				object obj8;
				object obj15;
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj5 == null)
					{
						break;
					}
					bool flag = obj2 == null;
					obj4 = null;
					if (!flag)
					{
						object obj6 = obj2;
						object obj7 = obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v5+12E]");
						if ((nint)obj7 >= 0)
						{
							goto IL_00f1;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v5+B0]");
						obj8 = 0;
						object obj9 = obj3;
						while (true)
						{
							object obj10 = obj9 + obj9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v432 @ r8_v9+v371 @ rax_v62*8]");
							if (0 == (nint)typeof(IEnumerator<PunchcardDefinitionV2>))
							{
								break;
							}
							obj9++;
							object obj11 = obj9;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v5+12E]");
							if ((nint)obj11 < 0)
							{
								continue;
							}
							goto IL_00f1;
						}
						object obj12 = obj9 + obj9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v432 @ r8_v9+8+v427 @ rcx_v40*8]");
						object obj13 = (nint)0 << 4;
						object obj14 = obj13 + 312;
						obj15 = obj14 + obj6;
						goto IL_0534;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_0534:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v433 @ rdx_v11] (should have been resolved before IL gen)");
				bool flag2 = obj16 != null;
				nint num = (nint)typeof(IEnumerator<PunchcardDefinitionV2>);
				if (!flag2)
				{
					continue;
				}
				if ((object)obj16 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+18]");
					bool flag3 = string.IsNullOrEmpty((string)0);
					num = (nint)typeof(IEnumerator<PunchcardDefinitionV2>);
					if (flag3)
					{
						continue;
					}
					UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
					if (_003CUserProgression_003Ek__BackingField != null)
					{
						if (userProgression.UnlockedCards != null)
						{
							List<string> unlockedCards = userProgression.UnlockedCards;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+18]");
							if (!unlockedCards.Contains((string)0))
							{
								UserProgression userProgression2 = _003CUserProgression_003Ek__BackingField;
								if (_003CUserProgression_003Ek__BackingField == null)
								{
									throw new NullReferenceException();
								}
								List<string> unlockedCards2 = userProgression2.UnlockedCards;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+18]");
								unlockedCards2.Add((string)0);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+18]");
								list.Add((string)0);
							}
							UserProgression userProgression3 = _003CUserProgression_003Ek__BackingField;
							if (_003CUserProgression_003Ek__BackingField != null)
							{
								if (userProgression3.CardStates != null)
								{
									Dictionary<string, UserProgression.UserCardState> cardStates = userProgression3.CardStates;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+18]");
									bool flag4 = cardStates.ContainsKey((string)0);
									num = (nint)typeof(IEnumerator<PunchcardDefinitionV2>);
									if (!flag4)
									{
										UserProgression userProgression4 = _003CUserProgression_003Ek__BackingField;
										if (_003CUserProgression_003Ek__BackingField == null)
										{
											throw new NullReferenceException();
										}
										UserProgression.UserCardState userCardState = new UserProgression.UserCardState();
										if (userCardState == null)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+18]");
										userCardState.CardID = (string)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+24]");
										userCardState.RemainingUses = 0;
										if (userProgression4.CardStates == null)
										{
											throw new NullReferenceException();
										}
										Dictionary<string, UserProgression.UserCardState> cardStates2 = userProgression4.CardStates;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v450 @ rax_v18 (UnityEngine.Object)+18]");
										cardStates2.set_Item((string)0, userCardState);
										obj3 = 0;
										num = 0;
									}
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
				throw new NullReferenceException();
				IL_00f1:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj8 = 0;
				obj15 = obj17;
				goto IL_0534;
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		return list;
	}

	public unsafe void SaveUnlockedCardStates(IEnumerable<PunchcardRuntime> cards)
	{
		//IL_0017: Expected O, but got Ref
		//IL_0101: Expected O, but got I4
		//IL_00aa: Expected O, but got I
		//IL_0116: Expected O, but got I
		//IL_013e: Expected O, but got I
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_0184: Expected I, but got O
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_01c0: Expected O, but got I
		//IL_01d2: Expected I, but got O
		//IL_01f2: Expected O, but got I
		//IL_020f: Expected I, but got O
		//IL_0287: Expected O, but got I
		//IL_02c0: Expected O, but got I
		if (cards == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj2 = default(object);
		object obj = (object)(&obj2);
		UnityEngine.Object obj3 = null;
		object obj4 = default(object);
		object obj14 = default(object);
		object obj15 = default(object);
		while (true)
		{
			object obj13;
			object obj6;
			if (obj2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					bool flag = obj2 == null;
					obj3 = null;
					if (!flag)
					{
						object obj5 = obj2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ r10_v5+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_00e6;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ r10_v5+B0]");
						obj6 = 0;
						UnityEngine.Object obj7 = null;
						while (true)
						{
							object obj8 = (object)obj7 + (object)obj7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v9+v352 @ rax_v38*8]");
							if (0 == (nint)typeof(IEnumerator<PunchcardRuntime>))
							{
								break;
							}
							obj7 = (UnityEngine.Object)(obj7 + 1);
							UnityEngine.Object obj9 = obj7;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v176 @ r10_v5+12E]");
							if ((nint)obj9 < 0)
							{
								continue;
							}
							goto IL_00e6;
						}
						object obj10 = (object)obj7 + (object)obj7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v414 @ r8_v9+8+v408 @ rcx_v28*8]");
						object obj11 = (nint)0 << 4;
						object obj12 = obj11 + 312;
						obj13 = obj12 + obj5;
						goto IL_0388;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_00e6:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj13 = obj14;
			obj6 = 0;
			goto IL_0388;
			IL_0388:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v413 @ rdx_v10] (should have been resolved before IL gen)");
			UnityEngine.Object obj16;
			if (obj15 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v15+20]");
				obj16 = (UnityEngine.Object)0;
			}
			else
			{
				obj16 = null;
			}
			bool flag2 = obj16 != null;
			nint num = (nint)typeof(IEnumerator<PunchcardRuntime>);
			if (!flag2)
			{
				continue;
			}
			if ((object)obj16 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rdi_v7 (UnityEngine.Object)+18]");
				bool flag3 = string.IsNullOrEmpty((string)0);
				num = (nint)typeof(IEnumerator<PunchcardRuntime>);
				if (flag3)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rdi_v7 (UnityEngine.Object)+18]");
				bool flag4 = IsCardUnlocked((string)0);
				bool flag5 = !flag4;
				num = (nint)typeof(IEnumerator<PunchcardRuntime>);
				if (!flag5)
				{
					UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
					if (_003CUserProgression_003Ek__BackingField == null)
					{
						throw new NullReferenceException();
					}
					UserProgression.UserCardState userCardState = new UserProgression.UserCardState();
					if (userCardState == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rdi_v7 (UnityEngine.Object)+18]");
					userCardState.CardID = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rdi_v7 (UnityEngine.Object)+64]");
					userCardState.RemainingUses = 0;
					Dictionary<string, UserProgression.UserCardState> cardStates = userProgression.CardStates;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ rdi_v7 (UnityEngine.Object)+18]");
					cardStates.set_Item((string)0, userCardState);
					num = 0;
				}
				continue;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public void ResetAllUserProgress()
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		UserProgression userProgression = new UserProgression();
		_003CUserProgression_003Ek__BackingField = userProgression;
		operationStates.Clear();
		_003CCurrentOperation_003Ek__BackingField = null;
		string progressionSaveRoot = ProgressionSaveRoot;
		string path = Path.Combine(progressionSaveRoot, "progression.dat");
		if (File.Exists(path))
		{
			string progressionPath = ProgressionPath;
			File.Delete(progressionPath);
		}
		string progressionSaveRoot2 = ProgressionSaveRoot;
		if (Directory.Exists(progressionSaveRoot2))
		{
			string progressionSaveRoot3 = ProgressionSaveRoot;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A2D8]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			string[] files = Directory.GetFiles(progressionSaveRoot3, "operation.*.dat");
			object obj = files + 32;
			OperationState operationState = null;
			OperationState operationState2 = null;
			while ((nint)operationState2 < files.Length)
			{
				File.Delete((string)obj);
				operationState = (OperationState)(operationState + 1);
				obj += 8;
				operationState2 = operationState;
			}
		}
		string progressionSaveRoot4 = ProgressionSaveRoot;
		DirectoryInfo directoryInfo = Directory.CreateDirectory(progressionSaveRoot4);
		SaveProgression();
	}

	public int ForceCompleteMissions(OperationGraph operation)
	{
		//IL_03c6: Expected I4, but got O
		//IL_00d1: Expected O, but got I
		//IL_0308: Expected I4, but got O
		//IL_0121: Expected O, but got I
		//IL_0166: Expected O, but got I
		//IL_01c5: Expected O, but got I
		//IL_044c: Expected O, but got I
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Expected O, but got Unknown
		//IL_0218: Expected O, but got I
		//IL_02c5: Expected O, but got I
		//IL_0258: Expected O, but got I
		OperationState orCreateOperation = GetOrCreateOperation(operation);
		bool flag = orCreateOperation == null;
		int result = (int)orCreateOperation;
		if (!flag)
		{
			HashSet<string> hashSet = new HashSet<string>();
			bool flag2 = (object)operation == null;
			HashSet<string> hashSet2 = hashSet;
			if (!flag2)
			{
				List<MissionNode> missions = operation.Missions;
				bool flag3 = missions == null;
				hashSet2 = (HashSet<string>)(object)operation;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					OperationState.MissionState value = null;
					HashSet<string> hashSet3 = hashSet;
					int num = 0;
					List<MissionNode>.Enumerator enumerator = default(List<MissionNode>.Enumerator);
					UnityEngine.Object obj = default(UnityEngine.Object);
					object obj3 = default(object);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag4 = (object)obj == null;
						UnityEngine.Object obj2 = obj;
						if (!flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ stack_-60_v11 (UnityEngine.Object)+40]");
							obj2 = (UnityEngine.Object)0;
						}
						if (!(obj2 != null))
						{
							continue;
						}
						if ((object)obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdi_v14 (UnityEngine.Object)+58]");
							if (string.IsNullOrEmpty((string)0))
							{
								continue;
							}
							if (hashSet3 != null)
							{
								HashSet<string> hashSet4 = hashSet3;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdi_v14 (UnityEngine.Object)+58]");
								hashSet4.Add((string)0);
								if (obj3 == null)
								{
									continue;
								}
								if (orCreateOperation.MissionStates != null)
								{
									Dictionary<string, OperationState.MissionState> missionStates = orCreateOperation.MissionStates;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdi_v14 (UnityEngine.Object)+58]");
									if (!missionStates.TryGetValue((string)0, out value))
									{
										OperationState.MissionState missionState = new OperationState.MissionState();
										if (missionState == null)
										{
											throw new NullReferenceException();
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdi_v14 (UnityEngine.Object)+58]");
										missionState.MissionID = (string)0;
										if (orCreateOperation.MissionStates == null)
										{
											throw new NullReferenceException();
										}
										Dictionary<string, OperationState.MissionState> missionStates2 = orCreateOperation.MissionStates;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdi_v14 (UnityEngine.Object)+58]");
										missionStates2.set_Item((string)0, missionState);
										value = missionState;
										hashSet3 = hashSet;
									}
									if (value != null)
									{
										if (!value.Completed)
										{
											num++;
										}
										OperationState.MissionState missionState2 = value;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdi_v14 (UnityEngine.Object)+58]");
										missionState2.MissionID = (string)0;
										OperationState.MissionState missionState3 = (OperationState.MissionState)(value + 16);
										if (value != null)
										{
											value.Completed = true;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdi_v14 (UnityEngine.Object)+D0]");
											List<string> list = UnlockPunchcards((IEnumerable<PunchcardDefinitionV2>)0);
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
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					_003CCurrentOperation_003Ek__BackingField = orCreateOperation;
					hashSet2 = (HashSet<string>)(object)_003CUserProgression_003Ek__BackingField;
					if (_003CUserProgression_003Ek__BackingField != null)
					{
						hashSet2._freeList = (int)operation.OperationID;
						SaveOperation(operation.OperationID);
						string progressionSaveRoot = ProgressionSaveRoot;
						string path = Path.Combine(progressionSaveRoot, "progression.dat");
						SaveToFile(_003CUserProgression_003Ek__BackingField, path);
						result = num;
						goto IL_03d4;
					}
				}
			}
			throw new NullReferenceException();
		}
		goto IL_03d4;
		IL_03d4:
		return result;
	}

	private void LoadAllOperations()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0099: Expected O, but got I4
		//IL_00a2: Expected O, but got I4
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		operationStates.Clear();
		string progressionSaveRoot = ProgressionSaveRoot;
		if (!Directory.Exists(progressionSaveRoot))
		{
			return;
		}
		string progressionSaveRoot2 = ProgressionSaveRoot;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A2D8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string[] files = Directory.GetFiles(progressionSaveRoot2, "operation.*.dat");
		object obj = files + 32;
		object obj2 = 0;
		object obj3 = 0;
		OperationState operationState = default(OperationState);
		while ((nint)obj3 < files.Length)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18073ADD0");
			if (operationState != null && !string.IsNullOrEmpty(operationState.OperationID))
			{
				operationStates.set_Item(operationState.OperationID, operationState);
			}
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
		if (!string.IsNullOrEmpty(userProgression.LastOperationID))
		{
			UserProgression userProgression2 = _003CUserProgression_003Ek__BackingField;
			if (operationStates.TryGetValue(userProgression2.LastOperationID, out var value))
			{
				_003CCurrentOperation_003Ek__BackingField = value;
			}
		}
	}

	private void MigrateCompletedMissionPunchcardUnlocks()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0088: Expected O, but got I4
		//IL_0091: Expected O, but got I4
		//IL_009a: Expected O, but got I4
		//IL_00bf: Expected I, but got O
		//IL_00de: Expected O, but got I
		//IL_00e7: Expected I, but got O
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_010a: Expected O, but got I
		//IL_013f: Expected O, but got I
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015f: Expected I, but got O
		HashSet<string> completedMissionIds = GetCompletedMissionIds();
		if (completedMissionIds._count == 0)
		{
			return;
		}
		MissionGraph[] array = Resources.LoadAll<MissionGraph>("Missions");
		if (array == null || array.Length == 0)
		{
			return;
		}
		object obj = array + 32;
		object obj2 = 0;
		object obj3 = 0;
		object obj4 = 0;
		while ((nint)obj4 < array.Length)
		{
			UnityEngine.Object obj5 = (UnityEngine.Object)obj;
			bool flag = (UnityEngine.Object)obj != null;
			nint num = unchecked((nint)null);
			if (flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v7 (UnityEngine.Object)+58]");
				bool flag2 = string.IsNullOrEmpty((string)0);
				num = unchecked((nint)null);
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v7 (UnityEngine.Object)+58]");
					bool flag3 = completedMissionIds.Contains((string)0);
					bool flag4 = !flag3;
					num = 0;
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v7 (UnityEngine.Object)+D0]");
						List<string> list = UnlockPunchcards((IEnumerable<PunchcardDefinitionV2>)0);
						obj3 += list._size;
						num = unchecked((nint)null);
					}
				}
			}
			obj2++;
			obj += 8;
			obj4 = obj2;
		}
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string message = $"[ProgressionManager] Migrated {arg} punchcard unlocks from completed missions.";
			Debug.Log(message);
			SaveProgression();
		}
	}

	private HashSet<string> GetCompletedMissionIds()
	{
		//IL_002d: Expected O, but got I4
		HashSet<string> hashSet = new HashSet<string>();
		Dictionary<string, OperationState>.ValueCollection values = operationStates.Values;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
		Dictionary<string, OperationState.MissionState>.Enumerator enumerator = (Dictionary<string, OperationState.MissionState>.Enumerator)0;
		Dictionary<string, OperationState>.ValueCollection.Enumerator enumerator2 = default(Dictionary<string, OperationState>.ValueCollection.Enumerator);
		object obj = default(object);
		Dictionary<string, OperationState.MissionState>.Enumerator enumerator3 = default(Dictionary<string, OperationState.MissionState>.Enumerator);
		Dictionary<string, OperationState.MissionState>.Enumerator enumerator4 = default(Dictionary<string, OperationState.MissionState>.Enumerator);
		string text2 = default(string);
		while (enumerator2.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			if (obj == null)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ stack_18_v3+18]");
			if ((nint)0 == 0)
			{
				continue;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
			while (enumerator3.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				KeyValuePair<string, OperationState.MissionState> current = enumerator.Current;
				bool flag = (object)current == null;
				enumerator = enumerator4;
				if (flag)
				{
					continue;
				}
				bool flag2 = current.value == null;
				enumerator = enumerator4;
				if (flag2)
				{
					continue;
				}
				string text;
				if (!string.IsNullOrEmpty(current.key))
				{
					text = current.key;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
					text = text2;
				}
				bool flag3 = string.IsNullOrEmpty(text);
				enumerator = enumerator4;
				if (!flag3)
				{
					if (hashSet == null)
					{
						throw new NullReferenceException();
					}
					hashSet.Add(text);
					enumerator = enumerator4;
				}
			}
			enumerator3.Dispose();
		}
		enumerator2.Dispose();
		return hashSet;
	}

	private OperationState GetOrCreateOperation(OperationGraph operation)
	{
		if (!(operation != null))
		{
			goto IL_0139;
		}
		if ((object)operation != null)
		{
			if (string.IsNullOrEmpty(operation.OperationID))
			{
				goto IL_0139;
			}
			if (operationStates != null)
			{
				OperationState operationState = default(OperationState);
				if (!operationStates.TryGetValue(operation.OperationID, out var _))
				{
					operationState = new OperationState();
					Dictionary<string, OperationState.MissionState> missionStates = new Dictionary<string, OperationState.MissionState>(StringComparer.s_ordinalIgnoreCase);
					operationState.MissionStates = missionStates;
					Dictionary<string, OperationState.CardState> cardStates = new Dictionary<string, OperationState.CardState>(StringComparer.s_ordinalIgnoreCase);
					operationState.CardStates = cardStates;
					operationState.OperationID = operation.OperationID;
					if (operationStates == null)
					{
						goto IL_013b;
					}
					operationStates.set_Item(operation.OperationID, operationState);
				}
				return operationState;
			}
		}
		goto IL_013b;
		IL_0139:
		return null;
		IL_013b:
		return (OperationState)(object)new NullReferenceException();
	}

	private string GetOperationPath(string operationId)
	{
		if (operationId != null)
		{
			string text = operationId.Replace("/", "_");
			if (text != null)
			{
				string text2 = text.Replace("\\", "_");
				string progressionSaveRoot = ProgressionSaveRoot;
				string path = "operation." + text2 + ".dat";
				return Path.Combine(progressionSaveRoot, path);
			}
		}
		return (string)(object)new NullReferenceException();
	}

	private void NormalizeProgression()
	{
		UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
		if (userProgression.UnlockedCards == null)
		{
			List<string> unlockedCards = new List<string>();
			userProgression.UnlockedCards = unlockedCards;
		}
		UserProgression userProgression2 = _003CUserProgression_003Ek__BackingField;
		if (userProgression2.CardStates == null)
		{
			Dictionary<string, UserProgression.UserCardState> cardStates = new Dictionary<string, UserProgression.UserCardState>(StringComparer.s_ordinalIgnoreCase);
			userProgression2.CardStates = cardStates;
		}
		UserProgression userProgression3 = _003CUserProgression_003Ek__BackingField;
		if (userProgression3.UnlockedSceneObjects == null)
		{
			List<string> unlockedSceneObjects = new List<string>();
			userProgression3.UnlockedSceneObjects = unlockedSceneObjects;
		}
	}

	private int GetRemainingUses(string cardId, int fallback)
	{
		//IL_0089: Expected I4, but got O
		UserProgression userProgression = _003CUserProgression_003Ek__BackingField;
		if (_003CUserProgression_003Ek__BackingField != null && userProgression.CardStates != null)
		{
			if (!userProgression.CardStates.TryGetValue(cardId, out var value))
			{
				return fallback;
			}
			if (value != null)
			{
				return value.RemainingUses;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	private unsafe void SaveToFile<T>(T data, string path)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0060: Expected O, but got I
		//IL_00fb: Expected O, but got Ref
		//IL_013c: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rcx_v2 (Il2CppClass<T>)+FC]");
		T val;
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rcx_v3 (Il2CppClass<T>)+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0129;
			}
		}
		val = data;
		goto IL_0129;
		IL_0129:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		object value = (IntPtr)obj2;
		string s = JsonConvert.SerializeObject(value);
		Encoding uTF = Encoding.UTF8;
		byte[] bytes = uTF.GetBytes(s);
		byte[] array = Compress(bytes);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1804523E0");
		byte[] bytes2 = default(byte[]);
		File.WriteAllBytes(path, bytes2);
	}

	private unsafe T LoadFromFile<T>(string path)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0086: Expected O, but got I
		//IL_00a4: Expected O, but got I
		//IL_01b4: Expected O, but got I
		//IL_0139: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ r9_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ r9_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v150 @ r9_v1+38]");
		object obj3 = 0;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v2+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v2+FC]");
		bool flag = default(bool);
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v2+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v2+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			flag = File.Exists(path);
		}
		if (flag)
		{
			byte[] array = File.ReadAllBytes(path);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1804523E0");
			byte[] data = default(byte[]);
			byte[] bytes = Decompress(data);
			Encoding uTF = Encoding.UTF8;
			if (uTF == null)
			{
				return (T)new NullReferenceException();
			}
			nint num = (nint)uTF;
			string text = uTF.GetString(bytes);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	private byte[] Compress(byte[] data)
	{
		//IL_009d: Expected I, but got O
		MemoryStream memoryStream = new MemoryStream();
		GZipStream gZipStream2 = default(GZipStream);
		GZipStream gZipStream = new GZipStream(gZipStream2, CompressionMode.Compress);
		gZipStream._002Ector(gZipStream2, CompressionMode.Compress);
		if (data != null)
		{
			GZipStream gZipStream3 = default(GZipStream);
			if (gZipStream3 != null)
			{
				gZipStream3.Write(data, 0, data.Length);
				if (gZipStream3 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				nint num = (nint)gZipStream2;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v162 @ rdx_v7 (Il2CppClass<System.IO.Compression.GZipStream>)+3E8] (should have been resolved before IL gen)");
				if (gZipStream2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				byte[] result = default(byte[]);
				return result;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private byte[] Decompress(byte[] data)
	{
		//IL_005d: Expected I, but got O
		MemoryStream memoryStream = new MemoryStream(data);
		Stream stream = default(Stream);
		GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress);
		gZipStream._002Ector(stream, CompressionMode.Decompress);
		MemoryStream memoryStream2 = new MemoryStream();
		Stream stream2 = default(Stream);
		if (stream2 != null)
		{
			Stream stream3 = default(Stream);
			stream2.CopyTo(stream3);
			nint num = (nint)stream3;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v115 @ rdx_v6 (Il2CppClass<System.IO.Stream>)+3E8] (should have been resolved before IL gen)");
			if (stream3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			if (stream2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			if (stream != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			byte[] result = default(byte[]);
			return result;
		}
		throw new NullReferenceException();
	}

	private byte[] Encrypt(byte[] data)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		object obj = data + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj2 < data.Length)
			{
				if ((nint)obj3 >= data.Length)
				{
					break;
				}
				obj ^= 0x42;
				obj3++;
				obj++;
				obj2 = obj3;
				continue;
			}
			return data;
		}
		return (byte[])(object)new IndexOutOfRangeException();
	}

	private byte[] Decrypt(byte[] data)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		object obj = data + 32;
		object obj2 = 0;
		object obj3 = 0;
		while (true)
		{
			if ((nint)obj2 < data.Length)
			{
				if ((nint)obj3 >= data.Length)
				{
					break;
				}
				obj ^= 0x42;
				obj3++;
				obj++;
				obj2 = obj3;
				continue;
			}
			return data;
		}
		return (byte[])(object)new IndexOutOfRangeException();
	}

	public ProgressionManager()
	{
		Dictionary<string, OperationState> dictionary = new Dictionary<string, OperationState>();
		operationStates = dictionary;
		base._002Ector();
	}
}
