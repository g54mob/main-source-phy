using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FactionsController : SingleInstance<FactionsController>
{
	public enum AttackOnlyEnum
	{
		Ai = 0,
		Machine = 1,
		Both = 2
	}

	public enum DiscriminantEnum
	{
		Ai = 0,
		Machine = 1,
		Indiscriminant = 2
	}

	public enum FactionEnum
	{
		Ipsilon = 0,
		Tolbrynd = 1,
		Viking = 2,
		PlayerUnit = 3,
		Animal = 4,
		Peasant = 5,
		None = 6,
		Krolmar = 7,
		Seafarer = 8
	}

	private static Vector3 zero = Vector3.zero;

	private static NetworkAddPiece NAddPiece;

	public static bool setupComplete = false;

	public static Dictionary<string, Faction> Factions = new Dictionary<string, Faction>();

	public static List<string> AvailableFactions = new List<string>();

	private static Dictionary<Transform, EntityAI> AiTransDict = new Dictionary<Transform, EntityAI>();

	private static EntityAI[] AiUnits;

	public static int targetLimit = 2;

	private bool wasSimulating;

	private static int _simpleAiCount = -1;

	private static EnemyAISimple[] _simpleAiArray;

	private static BesiegeNetworkManager networkManager;

	private static int _advancedAiCount = -1;

	public override string Name
	{
		get
		{
			return "FactionsController";
		}
	}

	public static EnemyAISimple[] SimpleAiArray()
	{
		if (StatMaster.levelSimulating && (object.ReferenceEquals(_simpleAiArray, null) || _simpleAiCount != _simpleAiArray.Length))
		{
			_simpleAiArray = ReferenceMaster.physicsGoalInstance.GetComponentsInChildren<EnemyAISimple>();
			_simpleAiCount = _simpleAiArray.Length;
		}
		return _simpleAiArray;
	}

	public static EntityAI GetAiFromTransform(Transform trans)
	{
		EntityAI value;
		if (AiTransDict.TryGetValue(trans, out value))
		{
			return value;
		}
		return null;
	}

	public static void AddToAITransformDictionary(Transform trans, EntityAI ai)
	{
		AiTransDict.Add(trans, ai);
	}

	public static EntityAI[] AdvancedAiArray()
	{
		if (StatMaster.levelSimulating && (object.ReferenceEquals(AiUnits, null) || _advancedAiCount != AiUnits.Length))
		{
			AiUnits = ReferenceMaster.physicsGoalInstance.GetComponentsInChildren<EntityAI>();
			_advancedAiCount = AiUnits.Length;
		}
		return AiUnits;
	}

	private void Awake()
	{
		if (StatMaster.isClient && !StatMaster.isLocalSim)
		{
			base.enabled = false;
		}
		else
		{
			SingleInstance<FactionsController>.Initialize(this);
		}
	}

	private void Update()
	{
		if (StatMaster.levelSimulating)
		{
			if (!wasSimulating)
			{
				wasSimulating = StatMaster.levelSimulating;
				OnSimulateStart();
			}
			foreach (Faction value in Factions.Values)
			{
				value.UpdateLoss();
			}
		}
		else if (wasSimulating)
		{
			CancelInvoke("ClearFactionLoss");
			CancelInvoke("UpdateFactionCenters");
			setupComplete = false;
			AiUnits = new EntityAI[0];
			_simpleAiArray = new EnemyAISimple[0];
		}
		wasSimulating = StatMaster.levelSimulating;
	}

	protected void UpdateFactionCenters()
	{
		if (!StatMaster.levelSimulating)
		{
			CancelInvoke("UpdateFactionCenters");
		}
		foreach (Faction value in Factions.Values)
		{
			value.UpdateCenter();
		}
	}

	private void OnSimulateStart()
	{
		if (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim)
		{
			NAddPiece = SingleInstanceFindOnly<AddPiece>.Instance as NetworkAddPiece;
			SetupFactions();
			InvokeRepeating("ClearFactionLoss", 3f, 3f);
			InvokeRepeating("UpdateFactionCenters", 0f, 0.25f);
		}
	}

	private void SetupFactions()
	{
		setupComplete = false;
		AiUnits = ReferenceMaster.physicsGoalInstance.GetComponentsInChildren<EntityAI>();
		AiTransDict.Clear();
		Factions = new Dictionary<string, Faction>();
		AvailableFactions = new List<string>();
		for (int i = 0; i < AiUnits.Length; i++)
		{
			EntityAI entityAI = AiUnits[i];
			if (StatMaster.isMP && !entityAI.my.aiGenEntity.PhysicsEnabled)
			{
				continue;
			}
			AiTransDict.Add(entityAI.transform, entityAI);
			if (!entityAI.isDead)
			{
				if (Factions.ContainsKey(entityAI.faction.Name))
				{
					Factions[entityAI.faction.Name].Infantry.Add(entityAI);
					entityAI.faction = Factions[entityAI.faction.Name];
					continue;
				}
				Factions.Add(entityAI.faction.Name, new Faction(entityAI.faction.Name, entityAI.faction.Preference, entityAI.faction.Discrimination, entityAI.faction.AttackOnlyTypeOf));
				AvailableFactions.Add(entityAI.faction.Name);
				Factions[entityAI.faction.Name].Infantry.Add(entityAI);
				entityAI.faction = Factions[entityAI.faction.Name];
			}
		}
		foreach (string key in Factions.Keys)
		{
			if (Factions[key].Preference != null)
			{
				if (Factions.ContainsKey(Factions[key].Preference))
				{
					Factions[key].TargetFaction = Factions[Factions[key].Preference];
				}
				else
				{
					Factions[key].TargetFaction = null;
				}
			}
			else
			{
				Factions[key].AttackOnlyTypeOf = AttackOnlyEnum.Both;
				Factions[key].Discrimination = DiscriminantEnum.Indiscriminant;
				Factions[key].TargetFaction = null;
			}
			Factions[key].MaxInfantry = Factions[key].Infantry.Count;
		}
		setupComplete = true;
	}

	public static void AddNewAIToFaction(EntityAI me)
	{
		if (!Factions.ContainsKey(me.faction.Name))
		{
			AddSingleFaction(me);
		}
		else
		{
			Factions[me.faction.Name].Infantry.Add(me);
			if (me.DebugAI)
			{
				Debug.Log("AddNewAIToFaction");
			}
			me.faction = Factions[me.faction.Name];
		}
		if (!AiTransDict.ContainsKey(me.transform))
		{
			AiTransDict.Add(me.transform, me);
		}
		if (StatMaster.levelSimulating)
		{
			me.faction.UpdateLoss();
			me.faction.UpdateCenter();
		}
	}

	public static void AddSingleFaction(EntityAI me)
	{
		if (me.DebugAI)
		{
			Debug.Log("AddSingleFaction");
		}
		if (Factions.ContainsKey(me.faction.Name))
		{
			AddNewAIToFaction(me);
			return;
		}
		Factions.Add(me.faction.Name, new Faction(me.faction.Name, me.faction.Preference, me.faction.Discrimination, me.faction.AttackOnlyTypeOf));
		AvailableFactions.Add(me.faction.Name);
		Factions[me.faction.Name].Infantry.Add(me);
		me.faction = Factions[me.faction.Name];
		Factions[me.faction.Name].MaxInfantry += 1f;
	}

	public static void ChangeFaction(EntityAI me, Faction newFaction)
	{
		if (Factions.ContainsKey(me.faction.Name))
		{
			Faction faction = Factions[me.faction.Name];
			if (faction.Infantry.Contains(me))
			{
				faction.Infantry.Remove(me);
				faction.MaxInfantry -= 1f;
			}
			if (faction.Infantry.Count <= 0)
			{
				AvailableFactions.Remove(me.faction.Name);
			}
		}
		me.faction = Factions[newFaction.Name];
		me.faction.Infantry.Add(me);
		if (me.DebugAI)
		{
			Debug.Log("ChangeFaction");
		}
		me.faction.MaxInfantry += 1f;
		me.faction.Preference = newFaction.Preference;
		me.faction.AttackOnlyTypeOf = newFaction.AttackOnlyTypeOf;
		me.faction.Discrimination = newFaction.Discrimination;
		if (!AvailableFactions.Contains(me.faction.Name))
		{
			AvailableFactions.Add(me.faction.Name);
		}
		me.Remove();
	}

	protected void ClearFactionLoss()
	{
		foreach (string key in Factions.Keys)
		{
			Factions[key].suddenLoss = 0f;
		}
	}

	public static Faction GetClosestFaction(Faction myFaction)
	{
		float num = float.MaxValue;
		string key = string.Empty;
		foreach (string availableFaction in AvailableFactions)
		{
			Factions[availableFaction].UpdateCenter();
			if (string.Compare(myFaction.Name, availableFaction) != 0)
			{
				float sqrMagnitude = (Factions[availableFaction].Center - Factions[myFaction.Name].Center).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					key = availableFaction;
				}
			}
		}
		if (Factions.ContainsKey(key))
		{
			return Factions[key];
		}
		return null;
	}

	public static Transform GetNewTargetFromFaction(EntityAI me)
	{
		if (me.faction.TargetFaction == me.faction)
		{
			return GetNewDiscriminantTarget(me);
		}
		Faction targetFaction = me.faction.TargetFaction;
		float num = float.MaxValue;
		EntityAI entityAI = null;
		EntityAI entityAI2 = null;
		int num2 = int.MaxValue;
		for (int i = 0; i < targetFaction.Infantry.Count; i++)
		{
			EntityAI entityAI3 = targetFaction.Infantry[i];
			float sqrMagnitude = (targetFaction.Infantry[i].transform.position - me.transform.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				entityAI = entityAI3;
				if (entityAI.TargetedBy.Count < targetLimit && entityAI.disposition.canAttack && entityAI.TargetedBy.Count < num2)
				{
					entityAI2 = entityAI;
					num2 = entityAI.TargetedBy.Count;
				}
			}
		}
		if (me.BehavioursMaxDistance < num || !entityAI)
		{
			return GetNewDiscriminantTarget(me);
		}
		if (entityAI2 != null && (entityAI2.transform.position - me.transform.position).sqrMagnitude < me.BehavioursMaxDistance)
		{
			entityAI = entityAI2;
		}
		AddToTargetedBy(entityAI, me);
		return entityAI.transform;
	}

	private static Transform GetRandomBlock(bool smartTargeting, uint closest)
	{
		BlockBehaviour blockBehaviour = null;
		blockBehaviour = ((!smartTargeting) ? ReferenceMaster.GetRandomBlock(closest) : ReferenceMaster.GetRandomIntactBlock(closest));
		if (blockBehaviour == null)
		{
			return null;
		}
		return blockBehaviour.transform;
	}

	public static Transform GetNewDiscriminantTarget(EntityAI me)
	{
		float num = float.MaxValue;
		bool flag = false;
		float num2 = float.MaxValue;
		bool flag2 = false;
		EntityAI entityAI = null;
		EntityAI entityAI2 = null;
		int num3 = int.MaxValue;
		int num4 = -1;
		if (me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Machine || me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Both)
		{
			num4 = (StatMaster.isMP ? GetClosestMachine(me) : 0);
		}
		if (me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Ai || me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Both)
		{
			for (int i = 0; i < AvailableFactions.Count; i++)
			{
				if (me.faction == Factions[AvailableFactions[i]])
				{
					continue;
				}
				for (int j = 0; j < Factions[AvailableFactions[i]].Infantry.Count; j++)
				{
					EntityAI entityAI3 = Factions[AvailableFactions[i]].Infantry[j];
					if (entityAI3.isDead)
					{
						continue;
					}
					float sqrMagnitude = (entityAI3.transform.position - me.transform.position).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						num = sqrMagnitude;
						entityAI = entityAI3;
						if (entityAI3.TargetedBy.Count < targetLimit && entityAI3.disposition.canAttack && entityAI3.TargetedBy.Count < num3)
						{
							entityAI2 = entityAI3;
							num3 = entityAI3.TargetedBy.Count;
						}
					}
				}
			}
			if (me.BehavioursMaxDistance > num)
			{
				if (entityAI2 != null && (entityAI2.transform.position - me.transform.position).sqrMagnitude < me.BehavioursMaxDistance)
				{
					entityAI = entityAI2;
				}
				flag = true;
				if (me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Ai)
				{
					if (entityAI != null)
					{
						AddToTargetedBy(entityAI, me);
					}
					return entityAI.transform;
				}
			}
		}
		if ((me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Machine || me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Both) && (!StatMaster.isMP || num4 != -1))
		{
			Vector3 machineCenterPos;
			if (StatMaster.isMP)
			{
				ServerMachine machine;
				NAddPiece.GetActiveMachine((uint)num4, out machine);
				machineCenterPos = machine.MachineCenterPos;
			}
			else
			{
				machineCenterPos = Machine.Active().MachineCenterPos;
			}
			num2 = (machineCenterPos - me.transform.position).sqrMagnitude;
			if (me.DebugAI)
			{
				Debug.Log("Machine Distance: " + num2);
			}
			if (me.BehavioursMaxDistance > num2)
			{
				flag2 = true;
				if (me.faction.AttackOnlyTypeOf == AttackOnlyEnum.Machine)
				{
					return GetRandomBlock(me.disposition.SmartTargeting, (uint)num4);
				}
			}
		}
		if (flag2 && flag)
		{
			switch (me.faction.Discrimination)
			{
			case DiscriminantEnum.Ai:
				if (entityAI != null)
				{
					AddToTargetedBy(entityAI, me);
				}
				return entityAI.transform;
			case DiscriminantEnum.Machine:
				return GetRandomBlock(me.disposition.SmartTargeting, (uint)num4);
			case DiscriminantEnum.Indiscriminant:
				if (num2 < num)
				{
					Transform randomBlock = GetRandomBlock(me.disposition.SmartTargeting, (uint)num4);
					if (me.DebugAI && randomBlock != null)
					{
						Debug.Log(randomBlock);
					}
					return randomBlock;
				}
				if (me.DebugAI)
				{
					Debug.Log("Machine " + num2 + " Ai " + num);
				}
				if (entityAI != null)
				{
					AddToTargetedBy(entityAI, me);
				}
				return entityAI.transform;
			default:
				return null;
			}
		}
		if (flag2)
		{
			Transform randomBlock2 = GetRandomBlock(me.disposition.SmartTargeting, (uint)num4);
			if (me.DebugAI && randomBlock2 != null)
			{
				Debug.Log(randomBlock2);
			}
			return randomBlock2;
		}
		if (me.DebugAI)
		{
			Debug.Log(entityAI);
		}
		if (entityAI != null)
		{
			AddToTargetedBy(entityAI, me);
			return entityAI.transform;
		}
		return null;
	}

	public static void AddToTargetedBy(EntityAI target, EntityAI me)
	{
		if (target != null && !target.TargetedBy.Contains(me))
		{
			target.TargetedBy.Add(me);
		}
	}

	public static bool CheckDistance(EntityAI target, EntityAI me)
	{
		float sqrMagnitude = (target.transform.position - me.transform.position).sqrMagnitude;
		return sqrMagnitude < me.BehavioursMaxDistance;
	}

	public static Transform GetFleeFromTarget(EntityAI me)
	{
		List<EntityAI> infantry = me.faction.Infantry;
		float num = float.MaxValue;
		EntityAI entityAI = null;
		if (infantry.Count <= 1)
		{
			return null;
		}
		for (int i = 0; i < infantry.Count; i++)
		{
			EntityAI entityAI2 = infantry[i];
			if (!(entityAI2 == me) && entityAI2.disposition.myState == EntityAI.EntityState.Fleeing)
			{
				Vector3 vector = entityAI2.transform.position - me.transform.position;
				float sqrMagnitude = vector.sqrMagnitude;
				sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					entityAI = entityAI2;
				}
			}
		}
		if (num < me.BehavioursMaxDistance)
		{
			return entityAI.TargetBlock.trans;
		}
		return null;
	}

	public static int GetClosestMachine(EntityAI ai)
	{
		int result = -1;
		ServerMachine machine = null;
		float num = float.MaxValue;
		if (StatMaster.isLocalSim)
		{
			result = BesiegeNetworkManager.Instance.PlayerID;
			if (!NAddPiece.GetActiveMachine((uint)result, out machine) || (ai.factionSystem.team != MPTeam.None && ai.factionSystem.team == machine.player.team))
			{
				return -1;
			}
			return result;
		}
		for (uint num2 = 0u; num2 < Playerlist.Players.Count; num2++)
		{
			if ((!ai.disposition.SmartTargeting || !ReferenceMaster.IntactBlocks.ContainsKey(num2) || ReferenceMaster.IntactBlocks[num2].Count > 0) && NAddPiece.GetActiveMachine(num2, out machine) && (ai.factionSystem.team == MPTeam.None || ai.factionSystem.team != machine.player.team))
			{
				float sqrMagnitude = (machine.MachineCenterPos - ai.transform.position).sqrMagnitude;
				if (num > sqrMagnitude)
				{
					num = sqrMagnitude;
					result = (int)num2;
				}
			}
		}
		return result;
	}

	public static int GetClosestMachine(Vector3 pos)
	{
		int num = -1;
		ServerMachine machine = null;
		float num2 = float.MaxValue;
		if (StatMaster.isLocalSim)
		{
			num = BesiegeNetworkManager.Instance.PlayerID;
			if (!NAddPiece.GetActiveMachine((uint)num, out machine))
			{
				return -1;
			}
			return num;
		}
		for (uint num3 = 0u; num3 < Playerlist.Players.Count; num3++)
		{
			if (ReferenceMaster.SimulationBlocks.ContainsKey(num3) && NAddPiece.GetActiveMachine(num3, out machine))
			{
				float sqrMagnitude = (machine.MachineCenterPos - pos).sqrMagnitude;
				if (num2 > sqrMagnitude)
				{
					num2 = sqrMagnitude;
					num = (int)num3;
				}
			}
		}
		return (Playerlist.Players.Count != 0) ? num : 0;
	}

	public static Vector3 GetMiddleOfClosestMachine(EntityAI ai)
	{
		int closestMachine = GetClosestMachine(ai);
		if (closestMachine == -1)
		{
			return zero;
		}
		ServerMachine machine;
		if (NAddPiece.GetActiveMachine((uint)closestMachine, out machine))
		{
			return machine.MachineCenterPos;
		}
		return zero;
	}

	public static void LineUp(List<EntityAI> AiList, Transform centerTransform)
	{
		float num = Mathf.Sqrt(AiList.Count);
		int num2 = (int)(num + 2f);
		int num3 = num2;
		int num4 = (int)num;
		float num5 = 2f;
		float num6 = 2f;
		int num7 = 0;
		float num8 = 0f;
		Vector3 position = centerTransform.position;
		Vector3 lookdir = ((AvailableFactions.Count() <= 1) ? centerTransform.forward : (position - GetClosestFaction(AiList[num7].faction).Center));
		AiList.Sort(delegate(EntityAI x, EntityAI z)
		{
			if (x.LineUpLayer == z.LineUpLayer)
			{
				return 0;
			}
			return (x.LineUpLayer > z.LineUpLayer) ? 1 : (-1);
		});
		float currentLayer = AiList[num7].LineUpLayer;
		IEnumerable<IGrouping<float, EntityAI>> source = from x in AiList
			group x by x.LineUpLayer;
		Vector3 size = AiList[num7].my.Collider.bounds.size;
		num5 = size.x * 2f;
		num6 = size.z * 2f;
		float num9 = (float)num3 * num5;
		num2 = (int)(num9 / num5);
		float num10 = position.x - (float)(num3 / 2) * num5;
		float num11 = position.z - (float)(num4 / 2) * num6;
		float num12 = num10;
		float num13 = num11;
		int num14 = source.First((IGrouping<float, EntityAI> x) => x.Key == currentLayer).Count();
		for (int num15 = 0; num15 < num4; num15++)
		{
			for (int num16 = 0; num16 < num2; num16++)
			{
				if (currentLayer != AiList[num7].LineUpLayer)
				{
					size = AiList[num7].my.Collider.bounds.size;
					num5 = size.x * 2f;
					num6 = size.z * 2f;
					num2 = (int)(num9 / num5);
					currentLayer = AiList[num7].LineUpLayer;
					num14 = source.First((IGrouping<float, EntityAI> xe) => xe.Key == currentLayer).Count();
					num13 += num6;
					if (num16 != 0)
					{
						num4++;
						num13 += num6;
						num16 = 0;
						num12 = num10;
					}
				}
				num8 = ((num14 <= num2) ? (num9 - (float)num14 * num5) : (num9 - (float)num2 * num5));
				Vector3 vector = RotatePointAroundPivot(new Vector3(num12 + num8 / 2f, position.y, num13), position, lookdir);
				AiList[num7].movement.lineUpPos = vector;
				if (AiList[num7].LineUpLayer == 1f)
				{
					Debug.DrawLine(vector, new Vector3(vector.x, position.y + 2f, vector.z), Color.red, 10f);
				}
				else if (AiList[num7].LineUpLayer == 2f)
				{
					Debug.DrawLine(vector, new Vector3(vector.x, position.y + 2f, vector.z), Color.blue, 10f);
				}
				else
				{
					Debug.DrawLine(vector, new Vector3(vector.x, position.y + 2f, vector.z), Color.green, 10f);
				}
				num12 += num5;
				num7++;
				if (num7 >= AiList.Count)
				{
					return;
				}
			}
			num14 -= num2;
			num13 += num6;
			num12 = num10;
		}
	}

	private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 lookdir)
	{
		Vector3 vector = point - pivot;
		vector = Quaternion.LookRotation(lookdir, Vector3.up) * vector;
		point = vector + pivot;
		return point;
	}

	private void OnDestroy()
	{
		setupComplete = false;
		Factions.Clear();
		AvailableFactions.Clear();
		AiTransDict.Clear();
		AiUnits = new EntityAI[0];
		_simpleAiCount = -1;
		_simpleAiArray = new EnemyAISimple[0];
	}
}
