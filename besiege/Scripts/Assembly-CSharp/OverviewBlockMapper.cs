using System;
using System.Collections.Generic;
using System.Linq;
using BlockMapperInternal;
using Localisation;
using Selectors;
using UnityEngine;

[AddComponentMenu("UI/Overview BlockMapper")]
public class OverviewBlockMapper : MonoBehaviour, IWidgetContainer
{
	public enum KeyModifyType
	{
		Add = 0,
		Remove = 1,
		Change = 2,
		IgnoreChanged = 3
	}

	public enum BlockFilter
	{
		All = 0,
		Locomotion = 1,
		Mechanical = 2,
		Weaponry = 3,
		Hidden = 4
	}

	public class RebindEntry
	{
		public int buildIndex;

		public XData newKey;
	}

	private const float COMPONENT_Z = 0.1f;

	private const float CONTAINER_OFFSET_Y = 1f;

	private const float ENTITY_CUSTOMIZE_OFFSET_Y = -50f;

	private const float CANNOT_CUSTOMIZE_Y = 0.9f;

	private const string GROUP_KEY_PREFIX = "input-group";

	[SerializeField]
	private DynamicText categoryText;

	[SerializeField]
	private int mask = 1;

	public LayerMask hoverMask;

	private static Transform hudTransform;

	private static Vector3? previousPosition;

	private bool hovering;

	public bool childHovering;

	private float disableTimer;

	private static AudioSource _audioSource;

	public static BlockFilter Filter;

	private Camera hudCam;

	private InputGroup currentHover;

	private Transform currentHoverTransform;

	private int currentHoverIndex = -1;

	[SerializeField]
	private UIButton prevButton;

	[SerializeField]
	private UIButton nextButton;

	public static Action onMapperClose;

	private bool _isDirty;

	[SerializeField]
	private DynamicText menuTitleText;

	[SerializeField]
	private AudioClip buttonClickSound;

	[SerializeField]
	private UIDrag dragWindow;

	[SerializeField]
	private Transform background;

	[SerializeField]
	private MeshRenderer voidspace;

	[SerializeField]
	private Transform cannotCustomizeText;

	[SerializeField]
	private UIButton closeButton;

	[SerializeField]
	private ContainerDetails container;

	[SerializeField]
	private UIScrollbar scrollbar;

	private string machineName;

	public List<InputGroup> inputGroups;

	private WidgetController inputGroupController;

	private Vector3 closeStartPos;

	private GameObject cannotCustomizeGO;

	private Vector3 nameStartPos;

	private bool lastScrollbarActive;

	protected static int openFrames;

	public static Transform lowerRight;

	public static Transform upperLeft;

	public static bool IsOpen
	{
		get
		{
			return CurrentInstance != null;
		}
	}

	public static AudioSource AudioSource
	{
		get
		{
			if (_audioSource != null)
			{
				return _audioSource;
			}
			_audioSource = new GameObject("OverviewBlockMapperSound").AddComponent<AudioSource>();
			_audioSource.outputAudioMixerGroup = ReferenceMaster.GetMixer("UI");
			_audioSource.gameObject.hideFlags = HideFlags.HideInHierarchy;
			_audioSource.volume = 0.45f;
			_audioSource.pitch = 1.8f;
			_audioSource.clip = CurrentInstance.buttonClickSound;
			return _audioSource;
		}
	}

	public static OverviewBlockMapper CurrentInstance { get; private set; }

	public Machine Current { get; private set; }

	public bool IsDirty
	{
		get
		{
			return _isDirty;
		}
		set
		{
			_isDirty = value;
		}
	}

	public IWidgetContainer Container
	{
		get
		{
			object result;
			if (container != null)
			{
				IWidgetContainer widgetContainer = container;
				result = widgetContainer;
			}
			else
			{
				result = this;
			}
			return (IWidgetContainer)result;
		}
	}

	public UIButton CloseButton
	{
		get
		{
			return closeButton;
		}
	}

	private string TranslateBlockFilter(BlockFilter filter)
	{
		int id = 1637;
		switch (filter)
		{
		case BlockFilter.Locomotion:
			id = 551;
			break;
		case BlockFilter.Mechanical:
			id = 546;
			break;
		case BlockFilter.Weaponry:
			id = 547;
			break;
		case BlockFilter.Hidden:
			id = 3419;
			break;
		}
		return LocalisationManager.GetTranslation(id);
	}

	public float TopValue()
	{
		return CurrentInstance.transform.position.y;
	}

	public float HeightValue()
	{
		return background.lossyScale.y;
	}

	public float ZValue()
	{
		return CurrentInstance.transform.position.z - 0.1f;
	}

	private void Awake()
	{
		if (!lowerRight)
		{
			lowerRight = GameObject.FindWithTag("lowerRight").transform;
		}
		if (!upperLeft)
		{
			upperLeft = GameObject.FindWithTag("upperLeft").transform;
		}
		if (hudTransform == null)
		{
			hudTransform = GameObject.Find("HUD").transform;
		}
		cannotCustomizeGO = cannotCustomizeText.gameObject;
		if (hudCam == null)
		{
			hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		}
		base.transform.SetParent(hudTransform, false);
		Vector3? vector = previousPosition;
		if (vector.HasValue)
		{
			Transform obj = base.transform;
			Vector3? vector2 = previousPosition;
			obj.position = vector2.Value;
		}
		closeStartPos = closeButton.transform.localPosition;
		nameStartPos = menuTitleText.transform.localPosition;
		lastScrollbarActive = false;
		string text = "Prefabs/BlockMapper/Input/";
		inputGroupController = new WidgetController(text + "InputGroupContainer");
		dragWindow.DragEnded += UpdateBackground;
		closeButton.Click += Close;
		prevButton.Click += OnPrev;
		nextButton.Click += OnNext;
	}

	private void OnPrev()
	{
		if (Filter == BlockFilter.All)
		{
			Filter = BlockFilter.Hidden;
		}
		else
		{
			Filter--;
		}
		Rebuild();
	}

	private void OnNext()
	{
		if (Filter == BlockFilter.Hidden)
		{
			Filter = BlockFilter.All;
		}
		else
		{
			Filter++;
		}
		Rebuild();
	}

	private void UpdateBackground()
	{
		float num = 0.2f;
		float num2 = num * base.transform.localScale.x * 0.75f;
		float num3 = base.transform.localScale.x * background.localScale.x / 2f;
		Vector3 position = base.transform.position;
		bool flag = false;
		if (position.x + 0.01f >= lowerRight.position.x - num3 - num2 && position.y > Mathf.Lerp(lowerRight.position.y, upperLeft.position.y, 0.7f))
		{
			flag = true;
		}
		if (flag)
		{
			float num4 = upperLeft.position.y - lowerRight.position.y;
			num4 = Mathf.Abs(num4 / base.transform.localScale.y);
			SetScrollHeight(num4 - 1f);
			background.localScale = new Vector3(background.localScale.x, num4, background.localScale.z);
			background.localPosition = new Vector3(background.localPosition.x, (0f - background.localScale.y) * 0.5f, background.localPosition.z);
			position = base.transform.position;
			position.y = upperLeft.position.y;
			base.transform.position = position;
			scrollbar.UpdateBounds();
			position = base.transform.position;
			position.x = lowerRight.position.x - num3 - ((!scrollbar.active) ? 0f : num2);
			base.transform.position = position;
			if (!scrollbar.active)
			{
				voidspace.gameObject.SetActive(true);
				float start = TopValue() - inputGroupController.EndPosition - 1f * base.transform.localScale.y;
				float end = TopValue() - num4 * base.transform.localScale.y + 0.01f;
				SetVoid(start, end);
			}
			else
			{
				voidspace.gameObject.SetActive(false);
			}
		}
		else
		{
			SetScrollHeight(8f);
			voidspace.gameObject.SetActive(false);
			float y = base.transform.lossyScale.y;
			float num5 = 1f * y;
			float a = ((!cannotCustomizeGO.activeInHierarchy) ? inputGroupController.EndPosition : (0.9f * y)) + num5;
			float b = scrollbar.contentMask.localScale.y * y + num5;
			background.localScale = new Vector3(background.localScale.x, Mathf.Min(a, b) / y, background.localScale.z);
			background.position = new Vector3(background.position.x, TopValue() - HeightValue() / 2f, background.position.z);
			scrollbar.UpdateBounds();
		}
		SetLayerDepth(flag);
		bool active = scrollbar.active;
		if (active != lastScrollbarActive)
		{
			closeButton.transform.localPosition = closeStartPos + ((!active) ? 0f : 0.2f) * Vector3.right;
			menuTitleText.transform.localPosition = nameStartPos + ((!active) ? 0f : 0.1f) * Vector3.right;
			lastScrollbarActive = active;
		}
	}

	private void SetScrollHeight(float height)
	{
		scrollbar.contentMask.localPosition = new Vector3(scrollbar.contentMask.localPosition.x, (0f - height) / 2f, scrollbar.contentMask.localPosition.z);
		scrollbar.contentMask.localScale = new Vector3(scrollbar.contentMask.localScale.x, height, scrollbar.contentMask.localScale.z);
		scrollbar.transform.localPosition = new Vector3(scrollbar.transform.localPosition.x, (0f - height) / 2f, scrollbar.transform.localPosition.z);
		scrollbar.scrollBG.localScale = new Vector3(scrollbar.scrollBG.localScale.x, height, scrollbar.scrollBG.localScale.z);
		BoxCollider component = scrollbar.GetComponent<BoxCollider>();
		component.size = new Vector3(component.size.x, height, component.size.z);
		Transform child = scrollbar.transform.GetChild(0);
		child.localPosition = new Vector3(child.localPosition.x, (height + child.localScale.y) / 2f, child.localPosition.z);
	}

	private void SetVoid(float start, float end)
	{
		Transform transform = voidspace.transform;
		float num = Mathf.Abs(end - start) / base.transform.localScale.y;
		transform.position = new Vector3(transform.position.x, (start + end) / 2f, transform.position.z);
		transform.localScale = new Vector3(transform.localScale.x, num, transform.localScale.z);
		num /= transform.localScale.x;
		voidspace.sharedMaterial.mainTextureScale = voidspace.sharedMaterial.mainTextureScale.x * new Vector2(1f, num);
	}

	private void SetLayerDepth(bool snap)
	{
		AssignBlurTest component = GetComponent<AssignBlurTest>();
		component.camera.depth = ((!snap) ? 1f : 0.5f);
		EnableCam component2 = GetComponent<EnableCam>();
		component2.target.depth = ((!snap) ? 1.05f : 0.55f);
	}

	private void ToggleCannotCustomizeText(bool toggle)
	{
		cannotCustomizeGO.SetActive(toggle);
	}

	public static OverviewBlockMapper Open(Machine m)
	{
		if (CurrentInstance != null)
		{
			Close();
		}
		BlockMapper.Close();
		if (m == null)
		{
			Debug.LogWarning("Trying to open OverviewBlockMapper, but machine is null!");
			return null;
		}
		CurrentInstance = UnityEngine.Object.Instantiate(Resources.Load<OverviewBlockMapper>("Prefabs/BlockMapper/OverviewBlockMapper"));
		if (CurrentInstance == null)
		{
			Debug.LogWarning("Couldn't instantiate OverviewBlockMapper, CurrentInstance is null!");
			return null;
		}
		CurrentInstance.name = "OverviewBM - " + m.Name;
		CurrentInstance.Current = m;
		CurrentInstance.GenerateGroups(false);
		CurrentInstance.UpdateDynamicTextImmediately();
		CurrentInstance.UpdateBackground();
		StatMaster.allowScrollRebind = true;
		if (ToggleKeyMapVis.Instance != null)
		{
			ToggleKeyMapVis.Instance.Set();
		}
		return CurrentInstance;
	}

	public static void OnEditMachineData(ushort playerId, byte[] data)
	{
		PlayerData player;
		if (Playerlist.GetPlayer(playerId, out player) && !player.isSpectator)
		{
			if (data[0] == 1)
			{
				player.machine.MachineData.Decode(data, 1);
			}
			else
			{
				player.machine.MachineData.Clear();
			}
		}
	}

	private static int GetKeyChangeIndex(KeyModifyType modifyType, MKey oldKey, MKey newKey)
	{
		switch (modifyType)
		{
		case KeyModifyType.Change:
		{
			for (int j = 0; j < Mathf.Min(oldKey.KeysCount, newKey.KeysCount); j++)
			{
				if (oldKey.GetKey(j) != newKey.GetKey(j))
				{
					return j;
				}
			}
			break;
		}
		case KeyModifyType.Remove:
		{
			for (int i = 0; i < oldKey.KeysCount; i++)
			{
				if (!newKey.HasKey(oldKey.GetKey(i)))
				{
					return i;
				}
			}
			break;
		}
		case KeyModifyType.Add:
			return newKey.KeysCount - 1;
		}
		return -1;
	}

	public static void OnRebindGroup(Machine m, MKey oldKey, MKey key, List<RebindEntry> entries)
	{
		List<UndoAction> list = new List<UndoAction>();
		if (OnRebindGroupName(m, oldKey, key))
		{
			list.Add(new UndoActionRebindGroup(m, oldKey, key));
		}
		UndoAction undoAction;
		if (OnSyncKeys(m, false, entries, out undoAction))
		{
			list.Add(undoAction);
		}
		for (int i = 0; i < list.Count; i++)
		{
			undoAction = list[i];
		}
		m.UndoSystem.AddActions(list);
	}

	public static bool OnRebindGroupName(Machine m, MKey oldKey, MKey newKey)
	{
		if (!m.isLocalMachine)
		{
			return false;
		}
		OverviewBlockMapper overviewBlockMapper = CurrentInstance;
		if (overviewBlockMapper == null)
		{
			overviewBlockMapper = Open(m);
		}
		int num = overviewBlockMapper.inputGroups.FindIndex((InputGroup x) => x.key.CompareLoad(oldKey) && x.IsChanged());
		if (num != -1)
		{
			MKey key = overviewBlockMapper.inputGroups[num].key;
			key.MatchKeys(newKey);
			SaveInputGroups(m, overviewBlockMapper.inputGroups);
			return true;
		}
		return false;
	}

	public static void OnRebindGroupRemote(ushort playerId, byte[] compressedData)
	{
		byte[] array = CLZF2.Decompress(compressedData);
		int num = 0;
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(array, num);
			num += 2;
		}
		PlayerData player;
		if (Playerlist.GetPlayer(playerId, out player) && !player.isSpectator)
		{
			ServerMachine machine = player.machine;
			int count;
			num += NetworkCompression.UnpackUInt(array, num, true, out count);
			XData xData;
			XDataHolder.DecodeXData(array, num, out xData);
			MKey mKey = new MKey(string.Empty, string.Empty, KeyCode.None);
			mKey.DeSerialize(xData);
			num += count;
			num += NetworkCompression.UnpackUInt(array, num, true, out count);
			XData xData2;
			XDataHolder.DecodeXData(array, num, out xData2);
			MKey mKey2 = new MKey(string.Empty, string.Empty, KeyCode.None);
			mKey2.DeSerialize(xData2);
			num += count;
			byte b = array[num];
			bool flag = (b & 1) != 0;
			num++;
			if (!flag)
			{
				List<RebindEntry> changedKeys;
				num += SyncKeyDataRemote(array, num, out changedKeys);
				OnRebindGroup(machine, mKey, mKey2, changedKeys);
			}
			else
			{
				OnRebindGroupName(machine, mKey, mKey2);
			}
			if (StatMaster.isHosting)
			{
				byte[] array2 = new byte[2 + array.Length];
				num = 0;
				NetworkCompression.WriteUInt16(playerId, array2, num);
				num += 2;
				Buffer.BlockCopy(array, 0, array2, num, array.Length);
				NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.MapperRebindGroup, CLZF2.Compress(array2));
			}
		}
	}

	protected static bool AdjustKey(int buildIndex, MKey key, MKey groupKey, KeyModifyType modifyType, KeyCode oldKey, KeyCode newKey, out RebindEntry entry)
	{
		entry = null;
		switch (modifyType)
		{
		case KeyModifyType.IgnoreChanged:
			key.SetIgnored(groupKey.Ignored);
			break;
		case KeyModifyType.Change:
		{
			int num = key.IndexOf(oldKey);
			if (num != -1)
			{
				key.AddOrReplaceKey(num, newKey);
			}
			break;
		}
		case KeyModifyType.Remove:
		{
			int num = key.IndexOf(oldKey);
			if (num != -1)
			{
				key.RemoveKey(num);
			}
			break;
		}
		case KeyModifyType.Add:
		{
			if (key.KeysCount < KeySelector.MaxKeys)
			{
				key.AddKey(newKey);
				break;
			}
			for (int i = 0; i < key.KeysCount; i++)
			{
				if (!groupKey.HasKey(key.GetKey(i)))
				{
					key.AddOrReplaceKey(i, newKey);
					break;
				}
			}
			break;
		}
		}
		key.RemoveRedundant();
		if (key.IsChanged())
		{
			entry = new RebindEntry
			{
				buildIndex = buildIndex,
				newKey = key.Serialize()
			};
			return true;
		}
		return false;
	}

	public static void OnRebindKeyRemote(ushort playerId, byte[] compressedData)
	{
		byte[] array = CLZF2.Decompress(compressedData);
		int num = 0;
		if (StatMaster.isClient)
		{
			playerId = NetworkCompression.ReadUInt16(array, num);
			num += 2;
		}
		PlayerData player;
		if (Playerlist.GetPlayer(playerId, out player) && !player.isSpectator)
		{
			ServerMachine machine = player.machine;
			byte b = array[num];
			bool isUndo = (b & 1) != 0;
			num++;
			List<RebindEntry> changedKeys;
			num += SyncKeyDataRemote(array, num, out changedKeys);
			UndoAction undoAction;
			if (OnSyncKeys(machine, isUndo, changedKeys, out undoAction))
			{
				machine.UndoSystem.AddAction(undoAction);
			}
			if (StatMaster.isHosting)
			{
				byte[] array2 = new byte[2 + array.Length];
				num = 0;
				NetworkCompression.WriteUInt16(playerId, array2, num);
				num += 2;
				Buffer.BlockCopy(array, 0, array2, num, array.Length);
				compressedData = CLZF2.Compress(array2);
				NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.MapperRebindKeys, compressedData);
			}
		}
	}

	public void OnEditOtherKey(InputGroup group, int index, KeyCode newKey)
	{
		KeyCode oldKey = group.otherKeys[index];
		int num = inputGroups.FindIndex((InputGroup x) => x.key.HasKey(oldKey) && x.key.Ignored == group.key.Ignored);
		if (num == -1)
		{
			return;
		}
		InputGroup inputGroup = inputGroups[num];
		if (group.ContainsGroup(inputGroup))
		{
			int index2 = inputGroup.key.IndexOf(oldKey);
			inputGroup.key.AddOrReplaceKey(index2, newKey);
			OnEditGroupKey(inputGroup, false);
			return;
		}
		List<RebindEntry> list = new List<RebindEntry>();
		for (int num2 = 0; num2 < group.blockList.Count; num2++)
		{
			InputGroup.BlockEntry blockEntry = group.blockList[num2];
			MKey key = blockEntry.key;
			int num3 = key.IndexOf(oldKey);
			if (num3 != -1)
			{
				key.AddOrReplaceKey(num3, newKey);
				key.RemoveRedundant();
				list.Add(new RebindEntry
				{
					buildIndex = blockEntry.block.BuildIndex,
					newKey = blockEntry.key.Serialize()
				});
			}
		}
		UndoAction undoAction;
		if (SyncRebindEntries(list, false, out undoAction))
		{
			Machine current = CurrentInstance.Current;
			current.UndoSystem.AddAction(undoAction);
		}
	}

	public void OnEditGroupKey(InputGroup group, bool isUndo)
	{
		List<RebindEntry> list = new List<RebindEntry>();
		XData xData = group.key.SerializeLoadValue(string.Empty);
		MKey mKey = new MKey(string.Empty, string.Empty, KeyCode.None);
		mKey.DeSerialize(xData);
		int keysCount = mKey.KeysCount;
		int keysCount2 = group.key.KeysCount;
		KeyModifyType keyModifyType = ((mKey.Ignored != group.key.Ignored) ? KeyModifyType.IgnoreChanged : ((keysCount2 == keysCount) ? KeyModifyType.Change : ((keysCount2 <= keysCount) ? KeyModifyType.Remove : KeyModifyType.Add)));
		int keyChangeIndex = GetKeyChangeIndex(keyModifyType, mKey, group.key);
		KeyCode oldKey = KeyCode.None;
		KeyCode newKey = KeyCode.None;
		switch (keyModifyType)
		{
		case KeyModifyType.Change:
			oldKey = mKey.GetKey(keyChangeIndex);
			newKey = group.key.GetKey(keyChangeIndex);
			break;
		case KeyModifyType.Remove:
			oldKey = mKey.GetKey(keyChangeIndex);
			break;
		case KeyModifyType.Add:
			newKey = group.key.GetKey(keyChangeIndex);
			break;
		}
		byte[] data;
		if (isUndo)
		{
			data = new byte[1] { (byte)(isUndo ? 1u : 0u) };
		}
		else
		{
			for (int i = 0; i < group.blockList.Count; i++)
			{
				InputGroup.BlockEntry blockEntry = group.blockList[i];
				RebindEntry entry;
				if (AdjustKey(blockEntry.block.BuildIndex, blockEntry.key, group.key, keyModifyType, oldKey, newKey, out entry))
				{
					list.Add(entry);
				}
			}
			SyncRebindEntries(list, false, out data);
		}
		if (StatMaster.isMP)
		{
			byte[] array = XDataHolder.EncodeXData(xData);
			byte[] array2 = XDataHolder.EncodeXData(group.key.Serialize());
			int num = NetworkCompression.PackedUIntLength(array.Length, true);
			int num2 = NetworkCompression.PackedUIntLength(array2.Length, true);
			byte[] array3 = new byte[num + array.Length + num2 + array2.Length + data.Length];
			int num3 = 0;
			NetworkCompression.PackUInt(array.Length, array3, num3, true, num);
			num3 += num;
			Buffer.BlockCopy(array, 0, array3, num3, array.Length);
			num3 += array.Length;
			NetworkCompression.PackUInt(array2.Length, array3, num3, true, num2);
			num3 += num2;
			Buffer.BlockCopy(array2, 0, array3, num3, array2.Length);
			num3 += array2.Length;
			Buffer.BlockCopy(data, 0, array3, num3, data.Length);
			NetworkAuxAddPiece.Instance.SendFragmentedServerMessage(RPCMessageType.MapperRebindGroup, CLZF2.Compress(array3));
			return;
		}
		Machine machine = Machine.Active();
		if (machine != null)
		{
			MKey mKey2 = new MKey(string.Empty, string.Empty, KeyCode.None);
			mKey2.DeSerialize(group.key.Serialize());
			if (!isUndo)
			{
				OnRebindGroup(machine, mKey, mKey2, list);
			}
			else
			{
				OnRebindGroupName(machine, mKey, mKey2);
			}
		}
	}

	public void OnEditBlockKey(BlockBehaviour block, MKey key)
	{
		GenerateGroups(true);
		UpdateDynamicTextImmediately();
		UpdateBackground();
	}

	private List<Machine.InputGroupEntry> LoadInputGroups()
	{
		List<Machine.InputGroupEntry> list = new List<Machine.InputGroupEntry>();
		XDataHolder machineData = Current.MachineData;
		int num = 0;
		string key = "input-group" + num;
		while (machineData.HasKey(key))
		{
			XStringArray xStringArray = machineData.Read(key) as XStringArray;
			if (xStringArray != null)
			{
				string[] value = xStringArray.Value;
				if (value.Length >= 3)
				{
					int result;
					if (!int.TryParse(value[1], out result))
					{
						result = 0;
					}
					int num2 = 2;
					string[] array = new string[value.Length - num2];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = value[i + num2];
					}
					MKey mKey = new MKey("key", "key", KeyCode.None);
					mKey.DeSerialize(new XStringArray(key, array));
					list.Add(new Machine.InputGroupEntry
					{
						Key = mKey,
						Name = value[0],
						State = result
					});
				}
			}
			num++;
			key = "input-group" + num;
		}
		return list;
	}

	public static void SaveInputGroups(Machine m, List<InputGroup> inputGroups)
	{
		XDataHolder machineData = m.MachineData;
		int num = 0;
		string key = "input-group" + num;
		while (machineData.HasKey(key))
		{
			machineData.Remove(key);
			num++;
			key = "input-group" + num;
		}
		num = 0;
		key = "input-group" + num;
		for (int i = 0; i < inputGroups.Count; i++)
		{
			InputGroup inputGroup = inputGroups[i];
			if (inputGroup.IsChanged())
			{
				XStringArray xStringArray = inputGroup.key.Serialize(key);
				string[] value = xStringArray.Value;
				int num2 = 2;
				string[] array = new string[num2 + value.Length];
				array[0] = inputGroup.CustomName;
				array[1] = inputGroup.State.ToString();
				for (int j = 0; j < value.Length; j++)
				{
					array[j + num2] = value[j];
				}
				XStringArray data = new XStringArray(key, array);
				machineData.Write(data);
				num++;
				key = "input-group" + num;
			}
		}
		if (StatMaster.isMP)
		{
			byte[] outData = new byte[0];
			bool flag = machineData.Encode(out outData);
			byte[] array2 = new byte[1 + (flag ? outData.Length : 0)];
			array2[0] = (byte)(flag ? 1u : 0u);
			if (flag)
			{
				Buffer.BlockCopy(outData, 0, array2, 1, outData.Length);
			}
			NetworkAuxAddPiece.Instance.SendFragmentedNetworkMessage(RPCMessageType.EditMachineData, array2);
		}
	}

	public bool FilterContains(BlockType t)
	{
		if (Filter == BlockFilter.All || Filter == BlockFilter.Hidden)
		{
			return true;
		}
		int[] array = null;
		switch (Filter)
		{
		case BlockFilter.Locomotion:
			array = new int[2] { 5, 2 };
			break;
		case BlockFilter.Mechanical:
			array = new int[1] { 3 };
			break;
		case BlockFilter.Weaponry:
			array = new int[1] { 4 };
			break;
		default:
			array = new int[1];
			break;
		}
		for (int i = 0; i < array.Length; i++)
		{
			BlockMenuControl blockMenuControl = BlockMenuControl.Menus[array[i]];
			BlockButtonControl[] blockButtons = blockMenuControl.BlockButtons;
			foreach (BlockButtonControl blockButtonControl in blockButtons)
			{
				if (blockButtonControl.myIndex == (int)t)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool PlaceInGroup(MKey groupKey, MKey blockKey)
	{
		for (int i = 0; i < Mathf.Min(groupKey.KeysCount, blockKey.KeysCount); i++)
		{
			if (groupKey.GetKey(i) != blockKey.GetKey(i))
			{
				return false;
			}
		}
		return true;
	}

	public void GenerateGroups(bool restoreDropdown)
	{
		List<MKey> list = new List<MKey>();
		InputGroup currentGroup;
		if (restoreDropdown)
		{
			for (int i = 0; i < inputGroups.Count; i++)
			{
				currentGroup = inputGroups[i];
				if (currentGroup.dropdownOpen)
				{
					list.Add(currentGroup.key);
				}
			}
		}
		inputGroups = new List<InputGroup>();
		List<BlockBehaviour> buildingBlocks = Current.BuildingBlocks;
		int j;
		MKey currentKey;
		KeyCode key;
		for (int i = 0; i < buildingBlocks.Count; i++)
		{
			BlockBehaviour blockBehaviour = buildingBlocks[i];
			for (j = 0; j < blockBehaviour.KeyList.Count; j++)
			{
				currentKey = blockBehaviour.KeyList[j];
				if (currentKey.useMessage)
				{
					continue;
				}
				for (int k = 0; k < currentKey.KeysCount; k++)
				{
					key = currentKey.GetKey(k);
					InputGroup inputGroup = inputGroups.Find((InputGroup x) => x.key.HasKey(key) && x.key.Ignored == currentKey.Ignored);
					if (inputGroup != null)
					{
						if (!inputGroup.ContainsEntry(blockBehaviour, currentKey))
						{
							inputGroup.blockList.Add(new InputGroup.BlockEntry
							{
								block = blockBehaviour,
								key = currentKey
							});
						}
					}
					else
					{
						MKey mKey = new MKey(string.Empty, string.Empty, key);
						mKey.Ignored = currentKey.Ignored;
						InputGroup inputGroup2 = new InputGroup();
						inputGroup2.blockList = new List<InputGroup.BlockEntry>
						{
							new InputGroup.BlockEntry
							{
								block = blockBehaviour,
								key = currentKey
							}
						};
						inputGroup2.key = mKey;
						inputGroup = inputGroup2;
						inputGroups.Add(inputGroup);
					}
					inputGroup.AddOtherKeys(currentKey);
				}
			}
		}
		List<Machine.InputGroupEntry> list2 = LoadInputGroups();
		List<InputGroup> removedGroups = new List<InputGroup>();
		for (int i = 0; i < inputGroups.Count; i++)
		{
			currentGroup = inputGroups[i];
			if (removedGroups.Contains(currentGroup))
			{
				continue;
			}
			currentKey = currentGroup.key;
			for (j = 0; j < currentGroup.otherKeys.Count; j++)
			{
				InputGroup inputGroup3 = inputGroups.Find((InputGroup x) => !removedGroups.Contains(x) && x.key.Ignored == currentGroup.key.Ignored && x.key.HasKey(currentGroup.otherKeys[j]));
				if (inputGroup3 == null || removedGroups.Contains(inputGroup3) || !currentGroup.EqualGroup(inputGroup3))
				{
					continue;
				}
				for (int k = 0; k < inputGroup3.key.KeysCount; k++)
				{
					KeyCode key2 = inputGroup3.key.GetKey(k);
					if (!currentKey.HasKey(key2))
					{
						currentKey.AddKey(key2);
					}
				}
				bool flag = false;
				for (j = 0; j < currentGroup.blockList.Count; j++)
				{
					InputGroup.BlockEntry blockEntry = currentGroup.blockList[j];
					for (int k = 0; k < blockEntry.block.KeyList.Count; k++)
					{
						MKey mKey2 = blockEntry.block.KeyList[k];
						if (currentGroup.ContainsEntry(blockEntry.block, mKey2))
						{
							continue;
						}
						for (int num = 0; num < mKey2.KeysCount; num++)
						{
							key = mKey2.GetKey(num);
							if (currentGroup.key.HasKey(key))
							{
								currentGroup.blockList.Add(new InputGroup.BlockEntry
								{
									block = blockEntry.block,
									key = mKey2
								});
								flag = true;
							}
						}
					}
				}
				if (flag)
				{
					currentGroup.blockList = currentGroup.blockList.OrderBy((InputGroup.BlockEntry x) => x.block.BuildIndex).ToList();
				}
				removedGroups.Add(inputGroup3);
			}
			currentKey.ApplyValue();
			for (j = 0; j < currentKey.KeysCount; j++)
			{
				currentGroup.otherKeys.Remove(currentGroup.key.GetKey(j));
			}
			Machine.InputGroupEntry inputGroupEntry = list2.Find((Machine.InputGroupEntry x) => x.Key.Compare(currentKey));
			if (inputGroupEntry != null)
			{
				currentGroup.CustomName = inputGroupEntry.Name;
				currentGroup.State = inputGroupEntry.State;
			}
			if (restoreDropdown && currentGroup.blockList.Count > 1)
			{
				currentGroup.dropdownOpen = list.FindIndex((MKey x) => x.Compare(currentKey)) != -1;
			}
		}
		for (int i = 0; i < removedGroups.Count; i++)
		{
			inputGroups.Remove(removedGroups[i]);
		}
		Rebuild();
	}

	public void Rebuild()
	{
		HoverGroup(null);
		ReferenceMaster.SetDynamicText(categoryText, TranslateBlockFilter(Filter));
		ClearWidgets();
		int num = 0;
		bool flag = Filter == BlockFilter.Hidden;
		for (int i = 0; i < inputGroups.Count; i++)
		{
			InputGroup inputGroup = inputGroups[i];
			bool flag2 = inputGroup.HasEmptyKey();
			if ((flag && (flag2 || inputGroup.key.Ignored)) || (!flag && !flag2 && !inputGroup.key.Ignored))
			{
				InputGroup.BlockEntry blockEntry = inputGroup.blockList.Find((InputGroup.BlockEntry x) => FilterContains(x.block.Prefab.Type));
				if (blockEntry != null)
				{
					inputGroupController.RegisterToggle(inputGroups[i], num++);
				}
			}
		}
		bool flag3 = num > 0;
		ToggleCannotCustomizeText(!flag3);
		inputGroupController.Display(Container, 0f);
		UpdateDynamicTextImmediately();
		UpdateBackground();
	}

	private void OnKeyChange(int index, KeyCode keyCode)
	{
		Rebuild();
	}

	private void UpdateDynamicTextImmediately()
	{
		DynamicText[] componentsInChildren = GetComponentsInChildren<DynamicText>();
		DynamicText[] array = componentsInChildren;
		foreach (DynamicText dynamicText in array)
		{
			dynamicText.GenerateMesh();
			if (dynamicText.cam != hudCam)
			{
				dynamicText.cam = hudCam;
			}
		}
	}

	public static void Close()
	{
		if (!(CurrentInstance == null))
		{
			if (OptionsMaster.scrollBindingEnabled)
			{
				StatMaster.allowScrollRebind = true;
			}
			EditFieldHandler instance = EditFieldHandler.Instance;
			if ((bool)instance)
			{
				instance.OnCloseOverviewMapper();
			}
			CurrentInstance.HoverGroup(null);
			CurrentInstance.inputGroups.Clear();
			CurrentInstance.ClearWidgets();
			previousPosition = CurrentInstance.transform.position;
			if (openFrames > 2)
			{
				AudioSource.Play();
			}
			openFrames = 0;
			CurrentInstance.SetLayerDepth(false);
			UnityEngine.Object.Destroy(CurrentInstance.gameObject);
			CurrentInstance = null;
			StatMaster.allowScrollRebind = true;
			if (ToggleKeyMapVis.Instance != null)
			{
				ToggleKeyMapVis.Instance.Set();
			}
		}
	}

	private void ToggleHoverBlock(InputGroup.BlockEntry blockEntry, bool toggle)
	{
		if (blockEntry == null || blockEntry.block == null)
		{
			return;
		}
		BlockBehaviour block = blockEntry.block;
		if (block.Prefab.hasBVC && block.VisualController.Highlighted != toggle)
		{
			if (toggle)
			{
				block.VisualController.SetHighlighted(true);
			}
			else
			{
				block.VisualController.SetNoOutline();
			}
		}
	}

	private void HoverGroup(InputGroup group, int index = -1, Transform hoverTransform = null)
	{
		if (currentHover == group && currentHoverIndex == index)
		{
			return;
		}
		if (currentHover != null)
		{
			if (currentHoverIndex == -1)
			{
				for (int i = 0; i < currentHover.blockList.Count; i++)
				{
					ToggleHoverBlock(currentHover.blockList[i], false);
				}
			}
			else if (currentHoverIndex < currentHover.blockList.Count)
			{
				ToggleHoverBlock(currentHover.blockList[currentHoverIndex], false);
			}
		}
		currentHoverIndex = index;
		currentHover = group;
		currentHoverTransform = hoverTransform;
		if (currentHover == null)
		{
			return;
		}
		if (currentHoverIndex == -1)
		{
			for (int j = 0; j < currentHover.blockList.Count; j++)
			{
				ToggleHoverBlock(currentHover.blockList[j], true);
			}
		}
		else if (currentHoverIndex < currentHover.blockList.Count)
		{
			ToggleHoverBlock(currentHover.blockList[currentHoverIndex], true);
		}
	}

	private bool IsOverChildWidget(Vector3 mousePosVec, Transform groupTransform)
	{
		RaycastHit[] array = Physics.RaycastAll(hudCam.ScreenPointToRay(mousePosVec), float.MaxValue, hoverMask);
		for (int i = 0; i < array.Length; i++)
		{
			Transform transform = array[i].collider.transform;
			if (transform.IsChildOf(groupTransform))
			{
				return true;
			}
		}
		return false;
	}

	private void LateUpdate()
	{
		openFrames++;
		if (Current == null)
		{
			Close();
			return;
		}
		Vector2 vector = InputManager.CursorPosition();
		Vector3 position = new Vector3(vector.x, vector.y, 10f);
		Vector3 position2 = hudCam.ScreenToWorldPoint(position);
		if (currentHover != null && currentHoverIndex == -1 && currentHover.blockList.Count > 1 && InputManager.LeftMouseButton() && !IsOverChildWidget(vector, currentHoverTransform))
		{
			currentHover.dropdownOpen = !currentHover.dropdownOpen;
			Rebuild();
			return;
		}
		InputGroup inputGroup = null;
		Transform hoverTransform = null;
		int index = -1;
		for (int i = 0; i < inputGroupController.ContainerCount; i++)
		{
			InputGroupWidget inputGroupWidget = inputGroupController.containers[i].widget as InputGroupWidget;
			Bounds bounds = inputGroupWidget.backgroundRenderer.bounds;
			InputGroup inputGroup2 = inputGroupWidget.group;
			hovering = UIMask.InsideMask(mask, position2) && position2.x > bounds.min.x && position2.x < bounds.max.x && position2.y > bounds.min.y && position2.y < bounds.max.y;
			if (hovering)
			{
				InputChildWidget inputChildWidget = null;
				if (inputGroup2.dropdownOpen && position2.y < bounds.max.y - inputGroupWidget.WidgetHeight())
				{
					for (int j = 0; j < inputGroupWidget.childController.containers.Count; j++)
					{
						InputChildWidget inputChildWidget2 = inputGroupWidget.childController.containers[j].widget as InputChildWidget;
						if (inputChildWidget2 != null)
						{
							Bounds bounds2 = inputChildWidget2.backgroundRenderer.bounds;
							if (position2.y > bounds2.min.y && position2.y < bounds2.max.y)
							{
								inputChildWidget = inputChildWidget2;
								break;
							}
						}
					}
				}
				inputGroup = inputGroup2;
				hoverTransform = inputGroupWidget.transform;
				if (inputChildWidget != null)
				{
					index = inputChildWidget.Index;
				}
			}
			if (!childHovering)
			{
				inputGroupWidget.ToggleHover(hovering, index);
			}
		}
		if (OptionsMaster.scrollBindingEnabled)
		{
			if (Input.mouseScrollDelta.y != 0f)
			{
				if (StatMaster.allowScrollRebind)
				{
					StatMaster.allowScrollRebind = false;
				}
				disableTimer = 0f;
			}
			else if (!StatMaster.allowScrollRebind)
			{
				disableTimer += Time.unscaledDeltaTime;
				if (disableTimer >= OptionsMaster.scrollDisableTime)
				{
					StatMaster.allowScrollRebind = true;
				}
			}
		}
		HoverGroup(inputGroup, index, hoverTransform);
	}

	public void ClearWidgets()
	{
		inputGroupController.Clear();
	}

	public static bool OnSyncKeys(Machine m, bool isUndo, List<RebindEntry> entries, out UndoAction undoAction)
	{
		XData prevKey = null;
		List<UndoActionRebindKeys.UndoKeyEntry> list = new List<UndoActionRebindKeys.UndoKeyEntry>();
		bool flag = !isUndo && m.isLocalMachine;
		for (int i = 0; i < entries.Count; i++)
		{
			RebindEntry rebindEntry = entries[i];
			BlockBehaviour block;
			if (!m.GetBlockFromIndex(rebindEntry.buildIndex, out block))
			{
				continue;
			}
			MKey mKey = block.GetMapperType(rebindEntry.newKey.Key) as MKey;
			if (mKey != null)
			{
				if (flag)
				{
					prevKey = mKey.SerializeLoadValue();
				}
				mKey.DeSerialize(rebindEntry.newKey);
				if (flag)
				{
					list.Add(new UndoActionRebindKeys.UndoKeyEntry
					{
						buildIndex = rebindEntry.buildIndex,
						prevKey = prevKey,
						newKey = rebindEntry.newKey
					});
				}
				block.OnSave(new XDataHolder());
			}
		}
		bool flag2 = flag && list.Count > 0;
		undoAction = ((!flag2) ? null : new UndoActionRebindKeys(m, list));
		if (m.isLocalMachine)
		{
			OverviewBlockMapper overviewBlockMapper = CurrentInstance;
			if (overviewBlockMapper == null)
			{
				overviewBlockMapper = Open(m);
			}
			overviewBlockMapper.GenerateGroups(true);
		}
		return flag2;
	}

	public bool SyncRebindEntries(List<RebindEntry> entries, bool isUndo, out UndoAction undoAction)
	{
		byte[] data;
		SyncRebindEntries(entries, isUndo, out data);
		if (StatMaster.isMP)
		{
			byte[] messageData = CLZF2.Compress(data);
			NetworkAuxAddPiece.Instance.SendFragmentedServerMessage(RPCMessageType.MapperRebindKeys, messageData);
		}
		else
		{
			Machine machine = Machine.Active();
			if (machine != null)
			{
				OnSyncKeys(machine, isUndo, entries, out undoAction);
				return true;
			}
		}
		undoAction = null;
		return false;
	}

	public bool SyncRebindEntries(List<RebindEntry> entries, bool isUndo, out byte[] data)
	{
		data = null;
		if (!StatMaster.isMP)
		{
			return false;
		}
		byte[] array = SyncKeyData(entries);
		data = new byte[1 + array.Length];
		int num = 0;
		data[num] = (byte)(isUndo ? 1u : 0u);
		num++;
		Buffer.BlockCopy(array, 0, data, num, array.Length);
		return true;
	}

	public byte[] SyncKeyData(List<RebindEntry> entries)
	{
		List<byte[]> list = new List<byte[]>();
		int num = 0;
		int num2;
		for (int i = 0; i < entries.Count; i++)
		{
			RebindEntry rebindEntry = entries[i];
			num2 = 0;
			byte[] array = XDataHolder.EncodeXData(rebindEntry.newKey);
			int num3 = NetworkCompression.PackedUIntLength(rebindEntry.buildIndex, true);
			int num4 = NetworkCompression.PackedUIntLength(array.Length, true);
			num2 = 0;
			byte[] array2 = new byte[num3 + num4 + array.Length];
			NetworkCompression.PackUInt(rebindEntry.buildIndex, array2, num2, true, num3);
			num2 += num3;
			NetworkCompression.PackUInt(array.Length, array2, num2, true, num4);
			num2 += num4;
			Buffer.BlockCopy(array, 0, array2, num2, array.Length);
			list.Add(array2);
			num += array2.Length;
		}
		num2 = 0;
		int num5 = NetworkCompression.PackedUIntLength(list.Count, true);
		byte[] array3 = new byte[num5 + num];
		NetworkCompression.PackUInt(list.Count, array3, num2, true, num5);
		num2 += num5;
		NetworkCompression.WriteArray(list, array3, num2);
		return array3;
	}

	public static int SyncKeyDataRemote(byte[] data, int offset, out List<RebindEntry> changedKeys)
	{
		int num = offset;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, true, out count);
		changedKeys = new List<RebindEntry>();
		for (int i = 0; i < count; i++)
		{
			int count2;
			offset += NetworkCompression.UnpackUInt(data, offset, true, out count2);
			int count3;
			offset += NetworkCompression.UnpackUInt(data, offset, true, out count3);
			XData xData;
			XDataHolder.DecodeXData(data, offset, out xData);
			offset += count3;
			changedKeys.Add(new RebindEntry
			{
				buildIndex = count2,
				newKey = xData
			});
		}
		return offset - num;
	}
}
