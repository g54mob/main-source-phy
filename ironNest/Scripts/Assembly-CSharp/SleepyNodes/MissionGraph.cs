using System;
using System.Collections.Generic;
using Localisation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.Serialization;

namespace SleepyNodes
{
	[CreateAssetMenu(fileName = "MissionGraph_", menuName = "Graphs/Mission Graph")]
	public class MissionGraph : StateGraph
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public enum MissionTypes
		{
			Tutorial = 0,
			Campaign = 1,
			Challange = 2,
			Chill = 3
		}

		[Header("Data")]
		public string MissionID;

		public TextIdentifier MissionName;

		public TextIdentifier MissionDescription;

		public MissionTypes MissionType;

		public Sprite MapOverride;

		public Sprite MapTopographyOverride;

		public bool ShowCreditsAfterSummary;

		public AchievementType achievementForClearing;

		public AchievementType achievementForGolding;

		public bool ResetTurretPlacement;

		[Header("Medals")]
		public List<MedalCategoryDefinition> Medals;

		[Header("Zones")]
		public List<Zone> Zones;

		[Header("Scene")]
		public MissionSceneReference SceneReference;

		[Header("Passives")]
		public MissionPassiveGraph[] PassiveGraphs;

		[Header("Mutators")]
		[Tooltip("Mutators that become ACTIVE for the duration of this operation.\n- All mutators in this list are active simultaneously while the operation is running.\n- Use MutatorRelay in your scenes/prefabs to toggle visuals/behaviors per mutator.\nExamples:\n- [Exact Distance Readout]\n- [Wide Direction Error]")]
		public List<MutatorDefinition> mutators;

		[Header("Minimum Requirements")]
		public int RequisitionPoints;

		public int PowderCharges;

		[FormerlySerializedAs("Punchcards")]
		public List<PunchcardDefinitionV2> RequiredPunchcards;

		public List<PunchcardDefinitionV2> UnlockedPunchcards;

		public override List<Type> NodeRestriction => null;

		public override List<Type> NodeTypeExludes => null;

		public void OnMissionLoaded()
		{
		}

		public void OnMissionUnloaded()
		{
		}

		public void OnNotification(string notifID)
		{
		}

		public void CheckEvents(EventNode.EventData evt)
		{
		}

		public virtual void ResetNodes()
		{
		}

		public override void Run()
		{
		}

		public override void Update()
		{
		}
	}
}
