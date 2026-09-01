using System;
using UnityEngine;

public class NetworkEditLogicHandler : EditLogicHandler
{
	private NetworkAuxAddPiece auxAddPiece;

	private BlockMapper blockMapper;

	private static int HEADER_SIZE;

	private LevelEditor levelEditor;

	public bool IsAddAction(LogicAction action)
	{
		return action == LogicAction.AddLogic || action == LogicAction.AddTarget || action == LogicAction.AddEvent;
	}

	public bool IsRemoveAction(LogicAction action)
	{
		return action == LogicAction.RemoveLogic || action == LogicAction.RemoveTarget || action == LogicAction.RemoveEvent;
	}

	public bool IsEditAction(LogicAction action)
	{
		return action == LogicAction.EditLogic || action == LogicAction.EditTarget || action == LogicAction.EditEvent;
	}

	public bool IsMoveAction(LogicAction action)
	{
		return action == LogicAction.MoveLogic || action == LogicAction.MoveEvent;
	}

	public bool IsLogicAction(LogicAction action)
	{
		return action == LogicAction.AddLogic || action == LogicAction.RemoveLogic || action == LogicAction.EditLogic || action == LogicAction.MoveLogic;
	}

	public bool IsTargetAction(LogicAction action)
	{
		return action == LogicAction.AddTarget || action == LogicAction.RemoveTarget || action == LogicAction.EditTarget;
	}

	public bool IsEventAction(LogicAction action)
	{
		return action == LogicAction.AddEvent || action == LogicAction.RemoveEvent || action == LogicAction.EditEvent || action == LogicAction.MoveEvent;
	}

	private bool IsLogicOpen(GenericEntity entity)
	{
		return blockMapper != null && blockMapper.IsLogic && blockMapper.Entity == entity;
	}

	public void Awake()
	{
		EditLogicHandler.Instance = this;
	}

	protected void Start()
	{
		auxAddPiece = NetworkAuxAddPiece.Instance;
		HEADER_SIZE = LevelEntity.ID_LENGTH;
		levelEditor = LevelEditor.Instance;
	}

	public override void OnCloseMapper()
	{
		if (!StatMaster.isClient)
		{
			return;
		}
		foreach (EntityLogic logicDatum in BlockMapper.CurrentInstance.Entity.logicData)
		{
			logicDatum.ResetValue();
		}
	}

	public void OnLogicChange(ushort playerId, byte[] data)
	{
		if (LevelEditor.Instance.isActive)
		{
			if (StatMaster.isClient)
			{
				playerId = NetworkCompression.ReadUInt16(data, 0);
				byte[] array = new byte[data.Length - 2];
				Buffer.BlockCopy(data, 2, array, 0, array.Length);
				data = array;
			}
			long id = BitConverter.ToInt64(data, 0);
			LevelEntity entity;
			if (levelEditor.Get(id, out entity))
			{
				byte[] array2 = new byte[data.Length - HEADER_SIZE];
				Buffer.BlockCopy(data, HEADER_SIZE, array2, 0, array2.Length);
				ProcessMessage(playerId, entity.behaviour, array2);
			}
		}
	}

	private void ProcessMessage(ushort playerId, GenericEntity entity, byte[] data)
	{
		int num = 0;
		LogicAction logicAction = (LogicAction)data[num];
		num++;
		EntityLogic logic = null;
		ushort num2 = 0;
		if (!StatMaster.isHosting || logicAction != LogicAction.AddLogic)
		{
			num2 = NetworkCompression.ReadUInt16(data, num);
			num += 2;
		}
		XDataHolder xDataHolder = null;
		bool flag = playerId == BesiegeNetworkManager.Instance.PlayerID;
		if (flag)
		{
			xDataHolder = new XDataHolder();
			entity.OnSaveLogicLoadValue(xDataHolder);
		}
		bool flag2 = false;
		blockMapper = BlockMapper.CurrentInstance;
		byte[] array = new byte[0];
		if (logicAction == LogicAction.AddLogic)
		{
			logic = new EntityLogic(entity.DefaultTriggerType(), entity);
			if (StatMaster.isHosting)
			{
				num2 = logic.ID;
			}
			else
			{
				logic.ID = num2;
			}
			entity.logicData.Add(logic);
			flag2 = true;
			entity.hasLogic = true;
			if (IsLogicOpen(entity))
			{
				blockMapper.IsDirty = true;
			}
		}
		else
		{
			if (!entity.GetLogic(num2, out logic, false))
			{
				return;
			}
			if (IsLogicAction(logicAction))
			{
				switch (logicAction)
				{
				case LogicAction.RemoveLogic:
					entity.logicData.Remove(logic);
					entity.hasLogic = entity.logicData.Count > 0;
					if (IsLogicOpen(entity))
					{
						blockMapper.IsDirty = true;
					}
					flag2 = true;
					break;
				case LogicAction.EditLogic:
				{
					int num3 = logic.Decode(data, num);
					byte[] array2 = new byte[num3];
					Buffer.BlockCopy(data, num, array2, 0, array2.Length);
					array = array2;
					flag2 = true;
					break;
				}
				}
			}
			else
			{
				ushort num4 = 0;
				if (!StatMaster.isHosting || !IsAddAction(logicAction))
				{
					num4 = NetworkCompression.ReadUInt16(data, num);
					num += 2;
					array = BitConverter.GetBytes(num4);
				}
				EntityEvent evt = null;
				TriggerTarget target = null;
				if (IsAddAction(logicAction))
				{
					if (IsEventAction(logicAction))
					{
						evt = new EntityEvent(EventContainer.GetEvents(logic.triggerType)[0]);
						if (StatMaster.isHosting)
						{
							num4 = evt.ID;
							array = BitConverter.GetBytes(num4);
						}
						else
						{
							evt.ID = num4;
						}
						logic.AddEvent(evt);
						flag2 = true;
					}
					else
					{
						target = new TriggerTarget(TriggerTargetType.Anything);
						if (StatMaster.isHosting)
						{
							num4 = target.ID;
							array = BitConverter.GetBytes(num4);
						}
						else
						{
							target.ID = num4;
						}
						logic.AddTarget(target);
						flag2 = true;
					}
				}
				else
				{
					bool flag3 = IsEventAction(logicAction);
					if (flag3)
					{
						if (!logic.GetEvent(num4, out evt))
						{
							return;
						}
					}
					else if (!logic.GetTarget(num4, out target))
					{
						return;
					}
					if (IsMoveAction(logicAction))
					{
						if (flag3)
						{
							int num5 = logic.events.IndexOf(evt);
							int num6 = data[num];
							bool flag4 = num6 == 1;
							bool flag5 = num6 == 0;
							if (flag4 || flag5)
							{
								int num7 = ((!flag4) ? (num5 - 1) : (num5 + 1));
								if (num7 >= 0 && num7 < logic.events.Count)
								{
									logic.events.Remove(evt);
									logic.events.Insert(num7, evt);
									if (logic.events[num7].eventData is EventContainer.EntityBehaviourEvent)
									{
										EventContainer.EntityBehaviourEvent entityBehaviourEvent = logic.events[num7].eventData as EventContainer.EntityBehaviourEvent;
										EventContainer.EntityBehaviourEvent entityBehaviourEvent2 = logic.events[num5].eventData as EventContainer.EntityBehaviourEvent;
										float activationDistance = entityBehaviourEvent.activationDistance;
										entityBehaviourEvent.activationDistance = entityBehaviourEvent2.activationDistance;
										entityBehaviourEvent2.activationDistance = activationDistance;
									}
									flag2 = true;
									array = NetworkCompression.Combine(entryB: new byte[1] { (byte)(flag4 ? 1u : 0u) }, entryA: BitConverter.GetBytes(num4));
									if (IsLogicOpen(entity))
									{
										blockMapper.IsDirty = true;
									}
								}
							}
							else
							{
								logic.events.Remove(evt);
								EventContainer.EntityBehaviourEvent entityBehaviourEvent3 = evt.eventData as EventContainer.EntityBehaviourEvent;
								bool flag6 = false;
								for (int i = 0; i < logic.events.Count; i++)
								{
									EventContainer.EntityBehaviourEvent entityBehaviourEvent4 = logic.events[i].eventData as EventContainer.EntityBehaviourEvent;
									if (entityBehaviourEvent3.activationDistance > entityBehaviourEvent4.activationDistance)
									{
										logic.events.Insert(i, evt);
										flag6 = true;
										break;
									}
								}
								if (!flag6)
								{
									logic.events.Add(evt);
								}
								flag2 = true;
								array = NetworkCompression.Combine(entryB: new byte[1] { (byte)(flag4 ? 1u : 0u) }, entryA: BitConverter.GetBytes(num4));
								if (IsLogicOpen(entity))
								{
									blockMapper.IsDirty = true;
								}
							}
							num++;
						}
					}
					else if (IsRemoveAction(logicAction))
					{
						if (flag3)
						{
							logic.RemoveEvent(evt);
						}
						else
						{
							logic.RemoveTarget(target);
						}
						flag2 = true;
					}
					else if (IsEditAction(logicAction))
					{
						int num8;
						if (flag3)
						{
							int loadEntityCount = evt.LoadEntityCount;
							EventContainer.EventType loadEventType = evt.LoadEventType;
							num8 = evt.Decode(data, num);
							bool flag7 = loadEntityCount != evt.entityList.Count;
							bool flag8 = loadEventType != evt.eventType;
							if ((flag7 || flag8) && IsLogicOpen(entity))
							{
								blockMapper.IsDirty = true;
							}
						}
						else
						{
							num8 = target.Decode(data, num);
							logic.InvokeLogicChanged();
						}
						byte[] array3 = new byte[num8];
						Buffer.BlockCopy(data, num, array3, 0, num8);
						array = NetworkCompression.Combine(BitConverter.GetBytes(num4), array3);
						flag2 = true;
					}
					else
					{
						Debug.LogError(string.Concat("Unidentified actionType (actionType=", logicAction, ")!"));
					}
				}
			}
		}
		if (flag && flag2)
		{
			LevelUndoSystem.Add(new LUAChangeLogic(entity.entity, xDataHolder));
		}
		if (StatMaster.isHosting)
		{
			byte[] array4 = CreateMessageHeader(entity, 2, 3 + array.Length);
			NetworkCompression.WriteUInt16(playerId, array4, 0);
			num = 2 + HEADER_SIZE;
			array4[num] = (byte)logicAction;
			num++;
			NetworkCompression.WriteUInt16(num2, array4, num);
			num += 2;
			Buffer.BlockCopy(array, 0, array4, num, array.Length);
			auxAddPiece.SendNetworkMessage(RPCMessageType.EditLogic, array4);
		}
	}

	private byte[] CreateMessageHeader(GenericEntity entity, int prefixSize, int dataSize)
	{
		byte[] array = new byte[prefixSize + HEADER_SIZE + dataSize];
		Buffer.BlockCopy(entity.GetIdentifierBytes(), 0, array, prefixSize, LevelEntity.ID_LENGTH);
		return array;
	}

	public override void OnAddLogic()
	{
		byte[] array = CreateMessageHeader(BlockMapper.CurrentInstance.Entity, 0, 1);
		array[HEADER_SIZE] = 0;
		auxAddPiece.SendServerMessage(RPCMessageType.EditLogic, array);
	}

	public override void OnRemoveLogic(EntityLogic logic)
	{
		SyncData(LogicAction.RemoveLogic, logic.ID, 0);
	}

	public override void OnAddTarget(EntityLogic logic)
	{
		SyncData(LogicAction.AddTarget, logic.ID, 0);
	}

	public override void OnRemoveTarget(EntityLogic logic, TriggerTarget trigger)
	{
		SyncData(LogicAction.RemoveTarget, logic.ID, trigger.ID);
	}

	public override void OnAddEvent(EntityLogic logic)
	{
		SyncData(LogicAction.AddEvent, logic.ID, 0);
	}

	public override void OnRemoveEvent(EntityLogic logic, EntityEvent evt)
	{
		SyncData(LogicAction.RemoveEvent, logic.ID, evt.ID);
	}

	public override void OnEditLogic(EntityLogic logic)
	{
		byte[] data;
		logic.Encode(false, out data);
		SyncData(LogicAction.EditLogic, logic.ID, 0, data);
	}

	public override void OnEditTarget(EntityLogic logic, TriggerTarget trigger)
	{
		SyncData(LogicAction.EditTarget, logic.ID, trigger.ID, trigger.Encode());
	}

	public override void OnEditEvent(EntityLogic logic, EntityEvent evt)
	{
		SyncData(LogicAction.EditEvent, logic.ID, evt.ID, evt.Encode());
	}

	public override void OnMoveEvent(EntityLogic logic, EntityEvent evt, bool isDown)
	{
		SyncData(data: new byte[1] { (byte)(isDown ? 1u : 0u) }, action: LogicAction.MoveEvent, logicId: logic.ID, id: evt.ID);
	}

	public override void OnSortBehaviour(EntityLogic logic, EntityEvent evt)
	{
		SyncData(data: new byte[1] { 2 }, action: LogicAction.MoveEvent, logicId: logic.ID, id: evt.ID);
	}

	private void SyncData(LogicAction action, ushort logicId, ushort id)
	{
		SyncData(action, logicId, id, new byte[0]);
	}

	private void SyncData(LogicAction action, ushort logicId, ushort id, byte[] data)
	{
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		if (currentInstance == null)
		{
			return;
		}
		GenericEntity entity = currentInstance.Entity;
		if (!(entity == null))
		{
			bool flag = IsLogicAction(action) || IsAddAction(action);
			byte[] array = CreateMessageHeader(entity, 0, 3 + ((!flag) ? 2 : 0) + data.Length);
			int hEADER_SIZE = HEADER_SIZE;
			array[hEADER_SIZE] = (byte)action;
			hEADER_SIZE++;
			NetworkCompression.WriteUInt16(logicId, array, hEADER_SIZE);
			hEADER_SIZE += 2;
			if (!flag)
			{
				NetworkCompression.WriteUInt16(id, array, hEADER_SIZE);
				hEADER_SIZE += 2;
			}
			Buffer.BlockCopy(data, 0, array, hEADER_SIZE, data.Length);
			auxAddPiece.SendServerMessage(RPCMessageType.EditLogic, array);
		}
	}

	public void Init(BlockMapper bm)
	{
		blockMapper = bm;
	}
}
