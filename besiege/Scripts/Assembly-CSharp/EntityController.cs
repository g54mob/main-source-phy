using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Modding;
using UnityEngine;
using cakeslice;

public class EntityController : MonoBehaviour
{
	public class PlaceEntry
	{
		public int prefabID;

		public Vector3 pos;

		public Quaternion rot;

		public Vector3 scale;

		public XDataHolder data;

		public long previousID;

		public PlaceEntry(int entityId, Vector3 entityPos, Quaternion entityRot, Vector3 entityScale, long prevID)
		{
			Init(entityId, entityPos, entityRot, entityScale, prevID);
			data = new XDataHolder();
		}

		public PlaceEntry(int entityId, Vector3 entityPos, Quaternion entityRot, Vector3 entityScale, XDataHolder entityData, long prevID)
		{
			Init(entityId, entityPos, entityRot, entityScale, prevID);
			data = entityData;
		}

		private void Init(int entityId, Vector3 entityPos, Quaternion entityRot, Vector3 entityScale, long prevID)
		{
			prefabID = entityId;
			pos = entityPos;
			rot = entityRot;
			scale = entityScale;
			previousID = prevID;
		}
	}

	private List<LevelEntity> entities;

	private List<LevelEntity> sortedEntities;

	private Dictionary<long, SendEntity> entityBuffer;

	private List<byte[]> entityData;

	private LevelEditor levelEditor;

	private CustomLevel level;

	private ushort ownerId;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private BesiegeNetworkManager networkManager;

	private IEnumerator outlineFadeCoroutine;

	private bool IEFading;

	private LevelEntity prevOutlines;

	public List<LevelEntity> Entities
	{
		get
		{
			return entities;
		}
	}

	public List<LevelEntity> SortedEntities
	{
		get
		{
			return sortedEntities;
		}
	}

	protected void Awake()
	{
		entityData = new List<byte[]>();
		entityBuffer = new Dictionary<long, SendEntity>();
		entities = new List<LevelEntity>();
		sortedEntities = new List<LevelEntity>();
		Clear();
	}

	protected void Start()
	{
		levelEditor = LevelEditor.Instance;
		level = CustomLevel.Instance;
		networkManager = BesiegeNetworkManager.Instance;
		networkAuxAddPiece = NetworkAuxAddPiece.Instance;
	}

	public void Clear()
	{
		if (entityBuffer.Count > 0)
		{
			entityBuffer.Clear();
		}
		if (entities.Count <= 0)
		{
			return;
		}
		foreach (LevelEntity item in new List<LevelEntity>(Entities))
		{
			levelEditor.DestroyEntity(item);
		}
		sortedEntities.Clear();
		entities.Clear();
		if (StatMaster.entityCountChanged != null)
		{
			StatMaster.entityCountChanged(entities.Count);
		}
	}

	public void SetOwner(ushort owner)
	{
		ownerId = owner;
	}

	public void UpdateEntities()
	{
		if (entityBuffer.Count != 0)
		{
			entityData.Clear();
			uint num = 0u;
			Dictionary<long, SendEntity>.Enumerator enumerator = entityBuffer.GetEnumerator();
			while (enumerator.MoveNext())
			{
				KeyValuePair<long, SendEntity> current = enumerator.Current;
				SendEntity value = current.Value;
				int dataSize = value.GetDataSize();
				byte[] array = new byte[LevelEntity.ID_LENGTH + dataSize];
				byte[] bytes = BitConverter.GetBytes(current.Key);
				Buffer.BlockCopy(bytes, 0, array, 0, LevelEntity.ID_LENGTH);
				value.Encode(array, LevelEntity.ID_LENGTH);
				entityData.Add(array);
				value.Clear();
				num += (uint)array.Length;
			}
			entityBuffer.Clear();
			byte[] array2 = new byte[num];
			NetworkCompression.WriteArray(entityData, array2, 0);
			FragmentedRPC.Send(SendHostUpdateEntities, array2, networkManager.ServerMessageHeaderSize + 5, networkAuxAddPiece.FragmentMessageHeaderSize);
		}
	}

	private void SendHostUpdateEntities(ushort current, byte[] data)
	{
		networkAuxAddPiece.SendServerMessage(RPCMessageType.UpdateEntities, networkAuxAddPiece.GetFragmentedMessage(current, data));
	}

	public void SetTransformData(ushort playerId, byte[] data)
	{
		if (StatMaster.isClient)
		{
			byte[] array = new byte[data.Length - 2];
			Buffer.BlockCopy(data, 2, array, 0, array.Length);
			playerId = NetworkCompression.ReadUInt16(data, 0);
			data = array;
		}
		int num = 0;
		while (num < data.Length)
		{
			long id = BitConverter.ToInt64(data, num);
			num += LevelEntity.ID_LENGTH;
			byte entityState = data[num];
			int dataSize = SendEntity.GetDataSize(entityState, false);
			levelEditor.RemoteUpdate(playerId, id, data, num);
			num += dataSize;
		}
		if (StatMaster.isHosting)
		{
			byte[] array = new byte[2 + data.Length];
			NetworkCompression.WriteUInt16(playerId, array, 0);
			Buffer.BlockCopy(data, 0, array, 2, data.Length);
			FragmentedRPC.Send(SendNetworkUpdateEntities, array, networkManager.PlayerMessageHeaderSize + 5, networkAuxAddPiece.FragmentMessageHeaderSize);
		}
	}

	private void SendNetworkUpdateEntities(ushort current, byte[] fragmentData)
	{
		networkAuxAddPiece.SendNetworkMessage(RPCMessageType.UpdateEntities, networkAuxAddPiece.GetFragmentedMessage(current, fragmentData));
	}

	public void UpdateLevelEntities(float delta)
	{
		for (int i = 0; i < entities.Count; i++)
		{
			entities[i].UpdateEntity(delta);
		}
	}

	public void BufferEntity(long id, SendEntity entity)
	{
		if (!entityBuffer.ContainsKey(id))
		{
			entityBuffer.Add(id, entity);
		}
	}

	public byte[] Encode(LevelEntity entity)
	{
		StringWriter stringWriter = new StringWriter();
		XmlWriter xmlWriter = XmlWriter.Create(stringWriter);
		LevelXMLSaver.WriteEntity(xmlWriter, entity);
		xmlWriter.Close();
		string s = stringWriter.ToString();
		stringWriter.Close();
		return Encoding.UTF8.GetBytes(s);
	}

	public bool Decode(byte[] data, out LevelEntity entity)
	{
		string objData = Encoding.UTF8.GetString(data);
		return LevelXMLLoader.ReadObjectFromString(objData, out entity);
	}

	public bool Get(long id, out LevelEntity entity)
	{
		if (id == LevelPrefab.INVALID_ID)
		{
			Debug.LogWarning("Getting entity with invalid ID!");
			entity = null;
			return false;
		}
		for (int i = 0; i < entities.Count; i++)
		{
			entity = entities[i];
			if (id == entity.identifier && entity != null)
			{
				return true;
			}
		}
		entity = null;
		return false;
	}

	public void Add(LevelEntity entity)
	{
		if (!entity.addedToController)
		{
			int i;
			for (i = 0; i < sortedEntities.Count && sortedEntities[i].identifier < entity.identifier; i++)
			{
			}
			sortedEntities.Insert(i, entity);
			entities.Add(entity);
			entity.addedToController = true;
		}
	}

	public void Remove(LevelEntity entity)
	{
		if (entity.addedToController)
		{
			entities.Remove(entity);
			sortedEntities.Remove(entity);
			entity.addedToController = false;
		}
	}

	public void Add(int prefabID, Vector3 pos, Quaternion rot, Vector3 scale, long previousID, bool isUndo, bool isDuplicate, bool showPlacementEffect)
	{
		List<PlaceEntry> list = new List<PlaceEntry>();
		list.Add(new PlaceEntry(prefabID, pos, rot, scale, previousID));
		Add(list, isUndo, isDuplicate, showPlacementEffect);
	}

	public void Add(List<PlaceEntry> entries, bool isUndo, bool isDuplicate, bool showPlacementEffect)
	{
		if (entries.Count == 0)
		{
			return;
		}
		List<byte[]> list = new List<byte[]>();
		int num = 0;
		int count = entries.Count;
		int num2;
		for (int i = 0; i < count; i++)
		{
			PlaceEntry placeEntry = entries[i];
			if (placeEntry != null)
			{
				byte[] outData;
				bool flag = placeEntry.data.Encode(out outData);
				byte[] array = ((!isUndo && !isDuplicate) ? new byte[0] : BitConverter.GetBytes(placeEntry.previousID));
				num2 = 0;
				byte[] array2 = new byte[array.Length + 12 + 16 + 12 + 1 + (flag ? outData.Length : 0) + 2];
				Buffer.BlockCopy(array, 0, array2, num2, array.Length);
				num2 += array.Length;
				NetworkCompression.PackVector(placeEntry.pos, array2, num2);
				num2 += 12;
				NetworkCompression.PackQuaternion(placeEntry.rot, array2, num2);
				num2 += 16;
				NetworkCompression.PackVector(placeEntry.scale, array2, num2);
				num2 += 12;
				array2[num2] = (byte)(flag ? 1u : 0u);
				num2++;
				if (flag)
				{
					Buffer.BlockCopy(outData, 0, array2, num2, outData.Length);
					num2 += outData.Length;
				}
				NetworkCompression.WriteUInt16((ushort)placeEntry.prefabID, array2, num2);
				num2 += 2;
				list.Add(array2);
				num += array2.Length;
			}
		}
		int num3 = NetworkCompression.PackedUIntLength(list.Count, false);
		byte[] array3 = new byte[1 + num3 + num];
		num2 = 0;
		array3[num2] = (byte)((isUndo ? 1 : 0) | (isDuplicate ? 2 : 0) | (showPlacementEffect ? 4 : 0));
		num2++;
		NetworkCompression.PackUInt(list.Count, array3, num2, false, num3);
		num2 += num3;
		NetworkCompression.WriteArray(list, array3, num2);
		if (StatMaster.isClient)
		{
			byte[] data = CLZF2.Compress(array3);
			FragmentedRPC.Send(SendHostAddEntities, data, networkManager.ServerMessageHeaderSize + 5, networkAuxAddPiece.FragmentMessageHeaderSize);
		}
		else
		{
			Add(ownerId, array3);
		}
	}

	private void SendHostAddEntities(ushort current, byte[] data)
	{
		networkAuxAddPiece.SendServerMessage(RPCMessageType.AddEntities, networkAuxAddPiece.GetFragmentedMessage(current, data));
	}

	public void Add(ushort playerId, byte[] messageData)
	{
		if (StatMaster.isClient || playerId != ownerId)
		{
			messageData = CLZF2.Decompress(messageData);
		}
		bool isClient = StatMaster.isClient;
		playerId = ((!isClient) ? playerId : NetworkCompression.ReadUInt16(messageData, 0));
		LevelEntity entity = null;
		int num = (isClient ? 2 : 0);
		long num2 = long.MinValue;
		byte b = messageData[num];
		bool flag = (b & 1) != 0;
		bool flag2 = (b & 2) != 0;
		bool flag3 = (b & 4) != 0;
		num++;
		int count;
		num += NetworkCompression.UnpackUInt(messageData, num, false, out count);
		List<LevelEntity> list = new List<LevelEntity>();
		List<long> list2 = new List<long>();
		List<LevelUndoAction> list3 = new List<LevelUndoAction>();
		Vector3 vec = default(Vector3);
		Quaternion quat = default(Quaternion);
		Vector3 vec2 = default(Vector3);
		bool flag4 = playerId == ownerId;
		bool flag5 = flag || flag2;
		for (int i = 0; i < count; i++)
		{
			if (flag5)
			{
				num2 = BitConverter.ToInt64(messageData, num);
				num += LevelEntity.ID_LENGTH;
			}
			else
			{
				num2 = LevelPrefab.INVALID_ID;
			}
			XDataHolder xDataHolder = null;
			if (StatMaster.isHosting)
			{
				NetworkCompression.UnpackVector(messageData, num, out vec);
				num += 12;
				NetworkCompression.UnpackQuaternion(messageData, num, out quat);
				num += 16;
				NetworkCompression.UnpackVector(messageData, num, out vec2);
				num += 12;
				xDataHolder = new XDataHolder();
				bool flag6 = messageData[num] == 1;
				num++;
				if (flag6)
				{
					num += xDataHolder.Decode(messageData, num);
				}
				ushort num3 = NetworkCompression.ReadUInt16(messageData, num);
				num += 2;
				LevelPrefab prefab;
				if (!levelEditor.GetPrefab(num3, out prefab))
				{
					Debug.Log("Couldn't find prefab " + num3 + "!");
					return;
				}
				entity = levelEditor.InstantiatePrefab(prefab, vec, quat, vec2);
			}
			else
			{
				int count2;
				num += NetworkCompression.UnpackUInt(messageData, num, false, out count2);
				byte[] array = new byte[count2];
				Buffer.BlockCopy(messageData, num, array, 0, count2);
				num += count2;
				if (!Decode(array, out entity))
				{
					Debug.LogWarning("Couldn't create entity from data!");
					return;
				}
			}
			if (StatMaster.isHosting)
			{
				if (flag)
				{
					entity.identifier = num2;
				}
				else
				{
					entity.Place();
				}
			}
			if (flag)
			{
				LevelUndoSystem.ReplaceEntity(entity, num2);
			}
			else if (flag4)
			{
				list3.Add(new LUAPlaceEntity(entity));
			}
			list.Add(entity);
			entity.Init();
			if (StatMaster.isHosting)
			{
				if (xDataHolder.HasData)
				{
					entity.LoadEntityData(xDataHolder);
				}
				if (flag5)
				{
					list2.Add(num2);
				}
				else
				{
					entity.SetupDefault();
				}
			}
			if (StatMaster.levelSimulating && entity.isStatic)
			{
				level.AddStaticEntity(entity);
				level.AddHiddenStatic(entity);
				entity.StartDeactivated();
			}
			if (flag4 && flag3 && (!StatMaster.levelSimulating || entity.isStatic))
			{
				if (levelEditor.useOutline && !entity.behaviour.prefab.ignoreOutline)
				{
					if (levelEditor.SelectionCount == 0 || levelEditor.SelectionContains(entity.identifier) || (flag5 && levelEditor.SelectionContains(num2)))
					{
						if (IEFading)
						{
							StopCoroutine(outlineFadeCoroutine);
							if (prevOutlines != null)
							{
								StopFadeOutline(prevOutlines);
							}
							levelEditor.outlineEffect.ResetFillAmount();
							levelEditor.outlineEffect.lineColor1.a = 1f;
						}
						levelEditor.outlineEffect.ChangeTargetType(0);
						outlineFadeCoroutine = IEFadeOutline(entity);
						ReferenceMaster.Instance.StartCoroutine(outlineFadeCoroutine);
					}
					if (i == 0)
					{
						OutlinePlacementEffect outlinePlacementEffect = ReferenceMaster.Instance.outlinePlacementEffect;
						outlinePlacementEffect.transform.position = entity.Position;
						outlinePlacementEffect.PlaySound();
					}
				}
				else
				{
					GameObject placementEffect = ReferenceMaster.Instance.placementEffect;
					if (placementEffect == null)
					{
						Debug.LogError("Unity has once again decided to unassign this referebce");
					}
					else
					{
						ObjectPlaceAnimation component = (UnityEngine.Object.Instantiate(placementEffect, entity.Position, entity.Rotation) as GameObject).GetComponent<ObjectPlaceAnimation>();
						component.Setup(entity);
						if (i != 0)
						{
							component.DisableSound();
						}
					}
				}
			}
			levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Place);
			SingleInstance<Events>.Instance.EntityPlaced(entity);
		}
		if (StatMaster.isHosting && flag2)
		{
			for (int j = 0; j < list.Count; j++)
			{
				LevelEntity levelEntity = list[j];
				for (int k = 0; k < list.Count; k++)
				{
					levelEntity.ReplaceEntityReference(list2[k], list[k].identifier);
				}
			}
		}
		if (list3.Count > 0)
		{
			if (flag2)
			{
				list3.Add(new LUAReplaceSelection(levelEditor.selectionController.LevelSelection, list));
				levelEditor.Select(list, false, false);
			}
			LevelUndoSystem.Add(list3);
		}
		if (!entity.isBuildZone)
		{
			levelEditor.isDirty = true;
		}
		if (StatMaster.entityCountChanged != null)
		{
			StatMaster.entityCountChanged(entities.Count);
		}
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		if (currentInstance != null && currentInstance.IsLogic)
		{
			currentInstance.RefreshPickFields();
		}
		if (!StatMaster.isHosting || Playerlist.Players.Count <= 1)
		{
			return;
		}
		uint num4 = 0u;
		byte[][] array2 = new byte[list.Count][];
		for (int k = 0; k < array2.Length; k++)
		{
			LevelEntity entity2 = list[k];
			byte[] array = Encode(entity2);
			num = 0;
			int num5 = NetworkCompression.PackedUIntLength(array.Length, false);
			byte[] array3 = new byte[((flag || flag2) ? LevelEntity.ID_LENGTH : 0) + num5 + array.Length];
			if (flag || flag2)
			{
				Buffer.BlockCopy(BitConverter.GetBytes(list2[k]), 0, array3, num, LevelEntity.ID_LENGTH);
				num += LevelEntity.ID_LENGTH;
			}
			NetworkCompression.PackUInt(array.Length, array3, num, false, num5);
			num += num5;
			Buffer.BlockCopy(array, 0, array3, num, array.Length);
			array2[k] = array3;
			num4 += (uint)array3.Length;
		}
		num = 0;
		int num6 = NetworkCompression.PackedUIntLength(array2.Length, false);
		byte[] array4 = new byte[3 + num6 + num4];
		NetworkCompression.WriteUInt16(playerId, array4, num);
		num += 2;
		array4[num] = (byte)((flag ? 1 : 0) | (flag2 ? 2 : 0) | (flag3 ? 4 : 0));
		num++;
		NetworkCompression.PackUInt(array2.Length, array4, num, false, num6);
		num += num6;
		NetworkCompression.WriteArray(array2, array4, num);
		byte[] data = CLZF2.Compress(array4);
		FragmentedRPC.Send(SendNetworkAddEntities, data, networkManager.PlayerMessageHeaderSize + 5, networkAuxAddPiece.FragmentMessageHeaderSize);
	}

	private void StopFadeOutline(LevelEntity entity)
	{
		bool isSelected = entity.IsSelected;
		EntityVisualController visualController = entity.behaviour.visualController;
		for (int i = 0; i < visualController.outlines.Length; i++)
		{
			Outline outline = visualController.outlines[i];
			if (!(outline == null))
			{
				GameObject gameObject = outline.gameObject;
				if (gameObject != null && gameObject.activeInHierarchy)
				{
					outline.color = outline.originalColor;
				}
				if (!isSelected)
				{
					outline.enabled = false;
				}
			}
		}
	}

	private IEnumerator IEFadeOutline(LevelEntity entity)
	{
		prevOutlines = entity;
		EntityVisualController visController = entity.behaviour.visualController;
		Outline[] outlines = visController.outlines;
		IEFading = true;
		float lerpTime = 0.5f;
		float currentTime = lerpTime;
		float progress = 1f;
		for (int i = 0; i < outlines.Length; i++)
		{
			if (outlines[i].gameObject.activeInHierarchy)
			{
				if (!outlines[i].enabled)
				{
					OutlineEffect.ToggleOutline(true);
					outlines[i].enabled = true;
				}
				outlines[i].color = 1;
			}
		}
		bool isSelected = entity.IsSelected;
		while (currentTime > 0f)
		{
			progress = currentTime / lerpTime;
			if (!isSelected)
			{
				levelEditor.outlineEffect.lineColor1.a = progress;
			}
			levelEditor.outlineEffect.fillAmount = progress;
			currentTime -= Time.deltaTime;
			yield return null;
		}
		StopFadeOutline(entity);
		levelEditor.outlineEffect.ResetFillAmount();
		levelEditor.outlineEffect.lineColor1.a = 1f;
		IEFading = false;
	}

	private void SendNetworkAddEntities(ushort current, byte[] data)
	{
		networkAuxAddPiece.SendNetworkMessage(RPCMessageType.AddEntities, networkAuxAddPiece.GetFragmentedMessage(current, data));
	}

	public bool Remove(long id, bool isUndo)
	{
		if (id == LevelPrefab.INVALID_ID)
		{
			return false;
		}
		List<long> list = new List<long>();
		list.Add(id);
		Remove(list, isUndo);
		return true;
	}

	public void Remove(List<long> ids, bool isUndo)
	{
		List<byte[]> list = new List<byte[]>();
		for (int i = 0; i < ids.Count; i++)
		{
			long num = ids[i];
			if (num != LevelPrefab.INVALID_ID)
			{
				list.Add(BitConverter.GetBytes(num));
			}
		}
		int num2 = NetworkCompression.PackedUIntLength(list.Count, false);
		byte[] array = new byte[1 + num2 + list.Count * LevelEntity.ID_LENGTH];
		int num3 = 0;
		array[num3] = (byte)(isUndo ? 1u : 0u);
		num3++;
		NetworkCompression.PackUInt(list.Count, array, num3, false, num2);
		num3 += num2;
		NetworkCompression.WriteArray(list, array, num3);
		if (StatMaster.isClient)
		{
			byte[] data = CLZF2.Compress(array);
			FragmentedRPC.Send(SendHostRemoveEntities, data, networkManager.ServerMessageHeaderSize + 5, networkAuxAddPiece.FragmentMessageHeaderSize);
		}
		else
		{
			Remove(ownerId, array);
		}
	}

	private void SendHostRemoveEntities(ushort current, byte[] data)
	{
		networkAuxAddPiece.SendServerMessage(RPCMessageType.RemoveEntities, networkAuxAddPiece.GetFragmentedMessage(current, data));
	}

	public void Remove(ushort playerId, byte[] messageData)
	{
		if (StatMaster.isClient || playerId != ownerId)
		{
			messageData = CLZF2.Decompress(messageData);
		}
		bool isClient = StatMaster.isClient;
		playerId = ((!isClient) ? playerId : NetworkCompression.ReadUInt16(messageData, 0));
		int num = (isClient ? 2 : 0);
		bool flag = messageData[num] == 1;
		num++;
		int count;
		num += NetworkCompression.UnpackUInt(messageData, num, false, out count);
		List<long> list = new List<long>();
		List<LevelUndoAction> list2 = new List<LevelUndoAction>();
		bool flag2 = playerId == ownerId;
		bool clearCache = !flag2;
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		bool flag3 = false;
		for (int i = 0; i < count; i++)
		{
			long num2 = BitConverter.ToInt64(messageData, num);
			num += LevelEntity.ID_LENGTH;
			LevelEntity entity;
			if (Get(num2, out entity))
			{
				if (entity.CanRemove)
				{
					SingleInstance<Events>.Instance.EntityRemoving(entity);
					if (flag2)
					{
						if (!flag)
						{
							list2.Add(new LUARemoveEntity(entity));
						}
						if (entity.IsSelected)
						{
							flag3 = true;
						}
						entity.OnRemove();
						LevelUndoSystem.CacheEntity(entity);
					}
					else
					{
						if (StatMaster.levelSimulating && entity.isStatic)
						{
							if (entity.hasBehaviour)
							{
								entity.behaviour.StopLogic();
							}
							level.RemoveStaticEntity(entity);
							level.RemoveHiddenStatic(entity);
						}
						levelEditor.DestroyEntity(entity);
						LevelEntity simEntity = entity.simEntity;
						if (!entity.isStatic && simEntity != null)
						{
							if (simEntity.needsTracking)
							{
								level.RemoveSimTrack(simEntity);
								simEntity.needsTracking = false;
							}
							if (simEntity.hasBehaviour)
							{
								simEntity.behaviour.StopLogic();
							}
							for (int j = 0; j < simEntity.children.Length; j++)
							{
								LevelEntity levelEntity = simEntity.children[j] as LevelEntity;
								if (levelEntity.needsTracking)
								{
									level.RemoveSimTrack(entity);
								}
								levelEntity.isDestroyed = true;
							}
							simEntity.isDestroyed = true;
							UnityEngine.Object.DestroyImmediate(simEntity.gameObject);
						}
						levelEditor.DestroyEntity(entity);
					}
					list.Add(num2);
					if (currentInstance != null && currentInstance.Current == entity.behaviour)
					{
						BlockMapper.Close();
					}
				}
				else
				{
					Debug.LogWarning("EntityController::Remove(): Can't remove object " + entity.name + "!");
				}
			}
			else
			{
				Debug.LogWarning("EntityController::Remove(): Couldn't find object " + num2 + "!");
			}
		}
		if (flag3)
		{
			levelEditor.OnSelectionUpdate();
		}
		LevelUndoSystem.Add(list2, clearCache);
		levelEditor.isDirty = true;
		if (currentInstance != null && currentInstance.IsLogic)
		{
			currentInstance.RefreshPickFields();
		}
		if (StatMaster.entityCountChanged != null)
		{
			StatMaster.entityCountChanged(entities.Count);
		}
		if (StatMaster.isHosting && list.Count > 0)
		{
			int count2 = list.Count;
			int num3 = NetworkCompression.PackedUIntLength(count2, false);
			byte[] array = new byte[3 + num3 + count2 * LevelEntity.ID_LENGTH];
			num = 0;
			NetworkCompression.WriteUInt16(playerId, array, num);
			num += 2;
			array[num] = (byte)(flag ? 1u : 0u);
			num++;
			NetworkCompression.PackUInt(count2, array, num, false, num3);
			num += num3;
			for (int k = 0; k < count2; k++)
			{
				long value = list[k];
				Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, num, LevelEntity.ID_LENGTH);
				num += LevelEntity.ID_LENGTH;
			}
			byte[] data = CLZF2.Compress(array);
			FragmentedRPC.Send(SendNetworkRemoveEntities, data, networkManager.PlayerMessageHeaderSize + 5, networkAuxAddPiece.FragmentMessageHeaderSize);
		}
	}

	private void SendNetworkRemoveEntities(ushort current, byte[] data)
	{
		networkAuxAddPiece.SendNetworkMessage(RPCMessageType.RemoveEntities, networkAuxAddPiece.GetFragmentedMessage(current, data));
	}
}
