using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cpp2ILInjected;
using Localisation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SleepyNodes;
using UnityEngine;

public static class MissionImporter
{
	[Serializable]
	public class NodeReference
	{
		[Serializable]
		public class NodeConnection
		{
			public string SourceNodeID;

			public string SourceFieldName;

			public string DestinationNodeID;

			public string DestinationFieldName;

			public bool IsPrimary
			{
				get
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A17A]");
					if ((nint)0 == 0)
					{
						_ = 1;
					}
					bool flag = SourceFieldName == "To";
					if (!flag)
					{
						return flag;
					}
					return DestinationFieldName == "From";
				}
			}
		}

		public string ID;

		public List<NodeConnection> Connections;

		public string NextID;

		public string PrevID;

		public Dictionary<string, string> Outputs;

		public double PosX;

		public double PosY;

		public string NodeType;

		public object NodeData;

		public NodeReference()
		{
			List<NodeConnection> connections = new List<NodeConnection>();
			Connections = connections;
			Dictionary<string, string> outputs = new Dictionary<string, string>();
			Outputs = outputs;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	[Serializable]
	public class MissionDefinition
	{
		public string MissionID;

		public string MissionName;

		public MissionGraph.MissionTypes MissionType;

		public string MapImage;

		public string MapTopoImage;

		public List<Zone> Zones;

		public int RequisitionPoints;

		public int PowderCharges;

		public Dictionary<string, NodeReference> Nodes;

		public MissionDefinition()
		{
			//IL_006c: Expected O, but got I
			//IL_007c: Expected O, but got I
			//IL_0096: Expected O, but got I
			//IL_00a6: Expected O, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rax_v2+B8]");
			object mapImage = 0;
			MapImage = (string)mapImage;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v4+B8]");
			object mapTopoImage = 0;
			MapTopoImage = (string)mapTopoImage;
			List<Zone> list = new List<Zone>();
			list._002Ector();
			Zones = list;
			RequisitionPoints = 100;
			PowderCharges = 100;
			Dictionary<string, NodeReference> nodes = new Dictionary<string, NodeReference>();
			Nodes = nodes;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public static class NodeTypeRegistry
	{
		public static readonly Dictionary<string, Type> Types;

		static NodeTypeRegistry()
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SpawnMapEntity));
			dictionary.Add("State_SpawnMapEntity", typeFromHandle);
			Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_MoveMapEntity));
			dictionary.Add("State_MoveMapEntity", typeFromHandle2);
			Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SetEntityState));
			dictionary.Add("State_SetEntityState", typeFromHandle3);
			Type typeFromHandle4 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_DamageEntity));
			dictionary.Add("State_DamageEntity", typeFromHandle4);
			Type typeFromHandle5 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SpawnScoutPlane));
			dictionary.Add("State_SpawnScoutPlane", typeFromHandle5);
			Type typeFromHandle6 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_EntitySelector));
			dictionary.Add("State_EntitySelector", typeFromHandle6);
			Type typeFromHandle7 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_Start));
			dictionary.Add("State_Start", typeFromHandle7);
			Type typeFromHandle8 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_End));
			dictionary.Add("State_End", typeFromHandle8);
			Type typeFromHandle9 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_MissionFailed));
			dictionary.Add("State_MissionFailed", typeFromHandle9);
			Type typeFromHandle10 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_Newspaper));
			dictionary.Add("State_Newspaper", typeFromHandle10);
			Type typeFromHandle11 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_QueueArticle));
			dictionary.Add("State_QueueArticle", typeFromHandle11);
			Type typeFromHandle12 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_AddMedals));
			dictionary.Add("State_AddMedals", typeFromHandle12);
			Type typeFromHandle13 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SetCustomMedalValue));
			dictionary.Add("State_SetCustomMedalValue", typeFromHandle13);
			Type typeFromHandle14 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_UnlockSceneObject));
			dictionary.Add("State_UnlockSceneObject", typeFromHandle14);
			Type typeFromHandle15 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_ClearTeleprinter));
			dictionary.Add("State_ClearTeleprinter", typeFromHandle15);
			Type typeFromHandle16 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_TeleprinterText));
			dictionary.Add("State_TeleprinterText", typeFromHandle16);
			Type typeFromHandle17 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_ClearSignalAlarm));
			dictionary.Add("State_ClearSignalAlarm", typeFromHandle17);
			Type typeFromHandle18 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_AddPowderCharge));
			dictionary.Add("State_AddPowderCharge", typeFromHandle18);
			Type typeFromHandle19 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_AddPunchcard));
			dictionary.Add("State_AddPunchcard", typeFromHandle19);
			Type typeFromHandle20 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_AddShell));
			dictionary.Add("State_AddShell", typeFromHandle20);
			Type typeFromHandle21 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_AddRequisitionPoints));
			dictionary.Add("State_AddRequisitionPoints", typeFromHandle21);
			Type typeFromHandle22 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_EntityDestroyed));
			dictionary.Add("Event_EntityDestroyed", typeFromHandle22);
			Type typeFromHandle23 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnNotification));
			dictionary.Add("Event_OnNotification", typeFromHandle23);
			Type typeFromHandle24 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_ShellLanded));
			dictionary.Add("Event_ShellLanded", typeFromHandle24);
			Type typeFromHandle25 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_TimeInterval));
			dictionary.Add("Event_TimeInterval", typeFromHandle25);
			Type typeFromHandle26 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnTimerExpired));
			dictionary.Add("Event_OnTimerExpired", typeFromHandle26);
			Type typeFromHandle27 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_TurretMovement));
			dictionary.Add("Event_TurretMovement", typeFromHandle27);
			Type typeFromHandle28 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnMedalsChanged));
			dictionary.Add("Event_OnMedalsChanged", typeFromHandle28);
			Type typeFromHandle29 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnMissionCompleted));
			dictionary.Add("Event_OnMissionCompleted", typeFromHandle29);
			Type typeFromHandle30 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnMissionFailed));
			dictionary.Add("Event_OnMissionFailed", typeFromHandle30);
			Type typeFromHandle31 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnGenericTimerReachedTime));
			dictionary.Add("Event_OnGenericTimerReachedTime", typeFromHandle31);
			Type typeFromHandle32 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnGenericTimerStarted));
			dictionary.Add("Event_OnGenericTimerStarted", typeFromHandle32);
			Type typeFromHandle33 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Event_OnCounterBatteryEvent));
			dictionary.Add("Event_OnCounterBatteryEvent", typeFromHandle33);
			Type typeFromHandle34 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_WaitEntityDestroyed));
			dictionary.Add("State_WaitEntityDestroyed", typeFromHandle34);
			Type typeFromHandle35 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_WaitSeconds));
			dictionary.Add("State_WaitSeconds", typeFromHandle35);
			Type typeFromHandle36 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_WaitForNotification));
			dictionary.Add("State_WaitForNotification", typeFromHandle36);
			Type typeFromHandle37 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_WaitBarrier));
			dictionary.Add("State_WaitBarrier", typeFromHandle37);
			Type typeFromHandle38 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_Blocker));
			dictionary.Add("State_Blocker", typeFromHandle38);
			Type typeFromHandle39 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_WaitShellLanded));
			dictionary.Add("State_WaitShellLanded", typeFromHandle39);
			Type typeFromHandle40 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_TriggerImpact));
			dictionary.Add("State_TriggerImpact", typeFromHandle40);
			Type typeFromHandle41 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_StartTimer));
			dictionary.Add("State_StartTimer", typeFromHandle41);
			Type typeFromHandle42 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_TimerAddTime));
			dictionary.Add("State_TimerAddTime", typeFromHandle42);
			Type typeFromHandle43 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_PauseTimer));
			dictionary.Add("State_PauseTimer", typeFromHandle43);
			Type typeFromHandle44 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_UnpauseTimer));
			dictionary.Add("State_UnpauseTimer", typeFromHandle44);
			Type typeFromHandle45 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_StopTimer));
			dictionary.Add("State_StopTimer", typeFromHandle45);
			Type typeFromHandle46 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_GenericTimer));
			dictionary.Add("State_GenericTimer", typeFromHandle46);
			Type typeFromHandle47 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_ConditionBranch));
			dictionary.Add("State_ConditionBranch", typeFromHandle47);
			Type typeFromHandle48 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_RandomBranch));
			dictionary.Add("State_RandomBranch", typeFromHandle48);
			Type typeFromHandle49 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SplitBranch));
			dictionary.Add("State_SplitBranch", typeFromHandle49);
			Type typeFromHandle50 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_MoveTurret));
			dictionary.Add("State_MoveTurret", typeFromHandle50);
			Type typeFromHandle51 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SetTurretLocation));
			dictionary.Add("State_SetTurretLocation", typeFromHandle51);
			Type typeFromHandle52 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SendSceneNotification));
			dictionary.Add("State_SendSceneNotification", typeFromHandle52);
			Type typeFromHandle53 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(State_SendUINotification));
			dictionary.Add("State_SendUINotification", typeFromHandle53);
			Types = dictionary;
		}
	}

	public class ExportPackage
	{
		private string _003CMissionName_003Ek__BackingField = "";

		private string _003CMissionJson_003Ek__BackingField = "";

		private List<ExportFile> _003CFiles_003Ek__BackingField;

		public string MissionName
		{
			get
			{
				return _003CMissionName_003Ek__BackingField;
			}
			set
			{
				_003CMissionName_003Ek__BackingField = value;
			}
		}

		public string MissionJson
		{
			get
			{
				return _003CMissionJson_003Ek__BackingField;
			}
			set
			{
				_003CMissionJson_003Ek__BackingField = value;
			}
		}

		public List<ExportFile> Files
		{
			get
			{
				return _003CFiles_003Ek__BackingField;
			}
			set
			{
				_003CFiles_003Ek__BackingField = value;
			}
		}

		public ExportPackage()
		{
			List<ExportFile> list = new List<ExportFile>();
			list._002Ector();
			_003CFiles_003Ek__BackingField = list;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	public class ExportFile
	{
		private string _003CName_003Ek__BackingField;

		private string _003CData_003Ek__BackingField;

		public string Name
		{
			get
			{
				return _003CName_003Ek__BackingField;
			}
			set
			{
				_003CName_003Ek__BackingField = value;
			}
		}

		public string Data
		{
			get
			{
				return _003CData_003Ek__BackingField;
			}
			set
			{
				_003CData_003Ek__BackingField = value;
			}
		}

		public ExportFile()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A17E]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			_003CName_003Ek__BackingField = "";
			_003CData_003Ek__BackingField = "";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public MissionDefinition missionDef;

		internal bool _003CImportMission_003Eb__0(ExportFile x)
		{
			//IL_0074: Expected I4, but got O
			if (x != null)
			{
				MissionDefinition missionDefinition = missionDef;
				if (missionDef != null)
				{
					return x._003CName_003Ek__BackingField == missionDefinition.MapImage;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CImportMission_003Eb__1(ExportFile x)
		{
			//IL_0074: Expected I4, but got O
			if (x != null)
			{
				MissionDefinition missionDefinition = missionDef;
				if (missionDef != null)
				{
					return x._003CName_003Ek__BackingField == missionDefinition.MapTopoImage;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public string icon;

		public string spriteName;

		internal bool _003CApplyData_003Eb__0(MapEntityIcon x)
		{
			//IL_0048: Expected I4, but got O
			if ((object)x != null)
			{
				return x.ID == icon;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		internal bool _003CApplyData_003Eb__1(ExportFile x)
		{
			//IL_0048: Expected I4, but got O
			if (x != null)
			{
				return x._003CName_003Ek__BackingField == spriteName;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	private static List<TextIdentifier> ImportedText;

	public unsafe static MissionGraph ImportMission(string json, MissionGraph missionGraph = null, bool updateMissionData = true, bool isPerminant = false, bool inlineLoc = false, bool importPunchcards = true, bool importZones = true)
	{
		//IL_0044: Expected I, but got O
		//IL_179d: Expected O, but got I
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_17d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d5: Expected O, but got Unknown
		//IL_1848: Expected I, but got O
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_0a03: Expected O, but got I
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		//IL_0a22: Expected O, but got I
		//IL_0a45: Expected O, but got I
		//IL_06b0: Expected O, but got I
		//IL_0854: Expected O, but got I
		//IL_06cf: Expected I4, but got O
		//IL_0a83: Expected I, but got O
		//IL_0873: Expected I4, but got O
		//IL_0cf2: Expected O, but got I
		//IL_0729: Expected O, but got I4
		//IL_0d11: Expected O, but got I
		//IL_0d34: Expected O, but got I
		//IL_08cd: Expected O, but got I4
		//IL_0ada: Expected O, but got I
		//IL_1804: Expected I4, but got O
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Expected O, but got Unknown
		//IL_0c76: Expected O, but got I
		//IL_0c88: Expected I, but got O
		//IL_07a9: Expected O, but got Ref
		//IL_094c: Expected O, but got Ref
		//IL_0b25: Expected I4, but got O
		//IL_0b53: Expected O, but got I
		//IL_0b53: Expected O, but got I4
		//IL_0ba0: Expected I, but got O
		//IL_0c4b: Expected O, but got I4
		//IL_0bd0: Expected O, but got I
		//IL_0c16: Expected I, but got O
		//IL_0e12: Expected O, but got Ref
		//IL_1919: Expected O, but got I4
		//IL_0ea4: Expected O, but got I4
		//IL_0eb1: Expected I4, but got O
		//IL_19ed: Expected O, but got I
		//IL_0ee1: Expected O, but got I
		//IL_0ef9: Expected O, but got I4
		//IL_0f24: Expected O, but got I4
		//IL_1043: Expected O, but got I4
		//IL_0f72: Expected O, but got I
		//IL_0f8d: Expected O, but got I4
		//IL_1150: Unknown result type (might be due to invalid IL or missing references)
		//IL_1155: Expected O, but got Unknown
		//IL_1405: Unknown result type (might be due to invalid IL or missing references)
		//IL_140a: Expected O, but got Unknown
		//IL_1192: Unknown result type (might be due to invalid IL or missing references)
		//IL_1197: Expected O, but got Unknown
		//IL_1448: Unknown result type (might be due to invalid IL or missing references)
		//IL_144d: Expected O, but got Unknown
		//IL_1481: Expected O, but got I4
		//IL_148a: Unknown result type (might be due to invalid IL or missing references)
		//IL_148f: Expected O, but got Unknown
		//IL_1233: Unknown result type (might be due to invalid IL or missing references)
		//IL_1238: Expected O, but got Unknown
		//IL_128d: Expected I, but got O
		//IL_152b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1530: Expected O, but got Unknown
		//IL_156e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1573: Expected O, but got Unknown
		//IL_15c7: Expected I, but got O
		_003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals34 = new _003C_003Ec__DisplayClass6_0();
		bool flag = missionGraph == null;
		bool flag2 = !flag;
		nint num = (nint)missionGraph;
		MissionGraph missionGraph2 = missionGraph;
		if (!flag2)
		{
			MissionGraph missionGraph3 = ScriptableObject.CreateInstance<MissionGraph>();
			num = 0;
			missionGraph2 = missionGraph3;
		}
		bool flag3 = CS_0024_003C_003E8__locals34 == null;
		TextIdentifier textIdentifier = (TextIdentifier)num;
		ExportPackage exportPackage = default(ExportPackage);
		TextIdentifier textIdentifier2;
		string key;
		string text3;
		bool flag7;
		if (!flag3)
		{
			CS_0024_003C_003E8__locals34.missionDef = null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
			bool flag4 = exportPackage == null;
			string text = json;
			MissionDefinition missionDef = default(MissionDefinition);
			if (!flag4)
			{
				text = exportPackage._003CMissionJson_003Ek__BackingField;
				if (!string.IsNullOrEmpty(exportPackage._003CMissionJson_003Ek__BackingField))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
					CS_0024_003C_003E8__locals34.missionDef = missionDef;
					text = (string)(CS_0024_003C_003E8__locals34 + 16);
				}
			}
			bool flag5 = CS_0024_003C_003E8__locals34.missionDef != null;
			textIdentifier = (TextIdentifier)(object)text;
			if (!flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
				CS_0024_003C_003E8__locals34.missionDef = missionDef;
				textIdentifier = (TextIdentifier)(CS_0024_003C_003E8__locals34 + 16);
				if (CS_0024_003C_003E8__locals34.missionDef == null)
				{
					goto IL_17db;
				}
			}
			object obj = default(object);
			if (obj != null)
			{
				MissionDefinition missionDef2 = CS_0024_003C_003E8__locals34.missionDef;
				if ((object)missionGraph2 == null)
				{
					goto IL_1618;
				}
				missionGraph2.Zones = missionDef2.Zones;
				textIdentifier = (TextIdentifier)(missionGraph2 + 160);
			}
			bool flag6 = !updateMissionData;
			flag7 = updateMissionData;
			if (flag6)
			{
				goto IL_0611;
			}
			MissionDefinition missionDef3 = CS_0024_003C_003E8__locals34.missionDef;
			if (CS_0024_003C_003E8__locals34.missionDef != null && (object)missionGraph2 != null)
			{
				missionGraph2.MissionID = missionDef3.MissionID;
				textIdentifier = (TextIdentifier)(missionGraph2 + 88);
				MissionDefinition missionDef4 = CS_0024_003C_003E8__locals34.missionDef;
				if (CS_0024_003C_003E8__locals34.missionDef != null)
				{
					missionGraph2.MissionType = missionDef4.MissionType;
					MissionDefinition missionDef5 = CS_0024_003C_003E8__locals34.missionDef;
					if (CS_0024_003C_003E8__locals34.missionDef != null)
					{
						missionGraph2.RequisitionPoints = missionDef5.RequisitionPoints;
						MissionDefinition missionDef6 = CS_0024_003C_003E8__locals34.missionDef;
						if (CS_0024_003C_003E8__locals34.missionDef != null)
						{
							missionGraph2.PowderCharges = missionDef6.PowderCharges;
							textIdentifier2 = new TextIdentifier();
							object obj2 = default(object);
							if (obj2 != null)
							{
								textIdentifier = textIdentifier2;
								flag7 = updateMissionData;
								key = null;
								goto IL_03f4;
							}
							MissionDefinition missionDef7 = CS_0024_003C_003E8__locals34.missionDef;
							bool flag8 = CS_0024_003C_003E8__locals34.missionDef == null;
							textIdentifier = textIdentifier2;
							if (!flag8)
							{
								if (missionDef7.MissionID != null)
								{
									string text2 = missionDef7.MissionID.ToUpper();
									if (text2 != null)
									{
										text3 = text2.Replace(" ", "_");
										bool flag9 = false;
										goto IL_17e0;
									}
								}
								text3 = null;
								goto IL_17e0;
							}
						}
					}
				}
			}
		}
		goto IL_1618;
		IL_03f4:
		if (textIdentifier2 != null)
		{
			textIdentifier2.Key = key;
			textIdentifier = (TextIdentifier)(textIdentifier2 + 16);
			MissionDefinition missionDef8 = CS_0024_003C_003E8__locals34.missionDef;
			if (CS_0024_003C_003E8__locals34.missionDef != null)
			{
				textIdentifier2.Raw = missionDef8.MissionName;
				missionGraph2.MissionName = textIdentifier2;
				bool flag10 = ImportedText == null;
				textIdentifier = (TextIdentifier)(object)ImportedText;
				if (!flag10)
				{
					ImportedText.Add(missionGraph2.MissionName);
					MissionSceneReference missionSceneReference = new MissionSceneReference();
					bool flag11 = missionSceneReference == null;
					textIdentifier = (TextIdentifier)(object)missionSceneReference;
					if (!flag11)
					{
						missionSceneReference.sceneName = "MissionBase";
						missionGraph2.SceneReference = missionSceneReference;
						missionGraph2.PassiveGraphs = System.EmptyArray<MissionPassiveGraph>.Value;
						object obj3 = default(object);
						if (obj3 != null)
						{
							List<PunchcardDefinitionV2> requiredPunchcards = new List<PunchcardDefinitionV2>();
							missionGraph2.RequiredPunchcards = requiredPunchcards;
							List<PunchcardDefinitionV2> unlockedPunchcards = new List<PunchcardDefinitionV2>();
							missionGraph2.UnlockedPunchcards = unlockedPunchcards;
						}
						MissionGraph missionGraph4 = Resources.Load<MissionGraph>("Missions/MissionGraph_Base");
						if (missionGraph4 != null)
						{
							bool flag12 = (object)missionGraph4 == null;
							textIdentifier = (TextIdentifier)(object)missionGraph4;
							if (flag12)
							{
								goto IL_1618;
							}
							if (obj3 != null)
							{
								missionGraph2.RequiredPunchcards = missionGraph4.RequiredPunchcards;
							}
							missionGraph2.PassiveGraphs = missionGraph4.PassiveGraphs;
						}
						goto IL_0611;
					}
				}
			}
		}
		goto IL_1618;
		IL_1830:
		List<TextIdentifier> importedText = new List<TextIdentifier>();
		nint num2 = (nint)typeof(MissionImporter);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1699 @ rcx_v53 (Il2CppClass<MissionImporter>)+E4]");
		bool flag13 = (nint)0 != 0;
		ImportedText = importedText;
		Dictionary<string, Node> dictionary = new Dictionary<string, Node>();
		textIdentifier = (TextIdentifier)(object)CS_0024_003C_003E8__locals34.missionDef;
		MissionDefinition missionDefinition = default(MissionDefinition);
		if (CS_0024_003C_003E8__locals34.missionDef != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
			bool flag14 = (nint)0 == 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
			textIdentifier = (TextIdentifier)0;
			if (!flag14)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
				Dictionary<string, NodeReference>.ValueCollection values = ((Dictionary<string, NodeReference>)0).Values;
				bool flag15 = values == null;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
				textIdentifier = (TextIdentifier)0;
				if (!flag15)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
					_003C_003Ec__DisplayClass6_0 obj4 = CS_0024_003C_003E8__locals34;
					Dictionary<string, NodeReference>.ValueCollection.Enumerator enumerator = default(Dictionary<string, NodeReference>.ValueCollection.Enumerator);
					List<TextIdentifier> list = default(List<TextIdentifier>);
					_003C_003Ec__DisplayClass6_0 obj5 = default(_003C_003Ec__DisplayClass6_0);
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						nint num3 = (nint)typeof(NodeTypeRegistry);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2860 @ rax_v168 (Il2CppClass<MissionImporter+NodeTypeRegistry>)+E4]");
						flag13 = (nint)0 != 0;
						if (list != null)
						{
							if (NodeTypeRegistry.Types != null)
							{
								Dictionary<string, Type> types = NodeTypeRegistry.Types;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v973 @ stack_-1C8_v44 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+48]");
								if (types.TryGetValue((string)0, out var value))
								{
									if ((object)missionGraph2 == null)
									{
										throw new NullReferenceException();
									}
									bool flag16 = (byte)(int)missionGraph2.AddNode(value) != 0;
									if (!flag16)
									{
										throw new NullReferenceException();
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v973 @ stack_-1C8_v44 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+48]");
									((UnityEngine.Object)flag16).name = (string)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v973 @ stack_-1C8_v44 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+38]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v973 @ stack_-1C8_v44 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+40]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v973 @ stack_-1C8_v44 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+50]");
									if ((nint)0 != 0)
									{
										nint num4 = (nint)typeof(JsonConvert);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3673 @ rcx_v136 (Il2CppClass<Newtonsoft.Json.JsonConvert>)+E4]");
										flag13 = (nint)0 != 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v973 @ stack_-1C8_v44 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+50]");
										string text4 = JsonConvert.SerializeObject(0);
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
										bool flag17 = obj5 == null;
										obj4 = obj5;
										if (!flag17)
										{
											nint num5 = (nint)typeof(MissionImporter);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4016 @ rcx_v139 (Il2CppClass<MissionImporter>)+E4]");
											flag13 = (nint)0 != 0;
											ApplyData(flag16, (Dictionary<string, object>)(object)obj5, exportPackage, missionGraph2.MissionID);
											missionDefinition = null;
											obj4 = obj5;
										}
									}
									if (dictionary == null)
									{
										throw new NullReferenceException();
									}
									dictionary.set_Item((string)(object)list._items, (Node)flag16);
									bool flag9 = false;
									flag7 = flag16;
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v973 @ stack_-1C8_v44 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+48]");
									string message = "Node type not found: " + (string)0;
									nint num6 = (nint)typeof(Debug);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3346 @ rcx_v130 (Il2CppClass<UnityEngine.Debug>)+E4]");
									flag13 = (nint)0 != 0;
									Debug.LogWarning(message);
									bool flag9 = false;
								}
								continue;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					textIdentifier = (TextIdentifier)(object)CS_0024_003C_003E8__locals34.missionDef;
					if (CS_0024_003C_003E8__locals34.missionDef != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
						bool flag18 = (nint)0 == 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
						textIdentifier = (TextIdentifier)0;
						if (!flag18)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
							Dictionary<string, NodeReference>.ValueCollection values2 = ((Dictionary<string, NodeReference>)0).Values;
							bool flag19 = values2 == null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+48]");
							textIdentifier = (TextIdentifier)0;
							if (!flag19)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807D9820");
								int result = 0;
								Node value2 = null;
								Node value3 = null;
								ExportPackage exportPackage2 = exportPackage;
								Dictionary<string, NodeReference>.ValueCollection.Enumerator enumerator2 = default(Dictionary<string, NodeReference>.ValueCollection.Enumerator);
								string text5 = default(string);
								List<NodeReference.NodeConnection>.Enumerator enumerator3 = default(List<NodeReference.NodeConnection>.Enumerator);
								List<TextIdentifier> list2 = default(List<TextIdentifier>);
								object arg = default(object);
								string fieldName = default(string);
								while (enumerator2.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									if (text5 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1798 @ stack_-150_v29 (System.String)+18]");
										if ((nint)0 == 0)
										{
											continue;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
										string text6 = (string)(object)missionDefinition;
										while (enumerator3.MoveNext())
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
											if (list2 == null)
											{
												continue;
											}
											bool flag20 = dictionary == null;
											string text7 = (string)(&enumerator3);
											NodePort nodePort2;
											if (!flag20)
											{
												bool flag21 = dictionary.TryGetValue((string)(object)list2._items, out value3);
												bool flag22 = !flag21;
												bool flag9 = false;
												if (flag22)
												{
													continue;
												}
												bool flag23 = dictionary.TryGetValue((string)list2._syncRoot, out value2);
												bool flag24 = !flag23;
												flag9 = false;
												if (flag24)
												{
													continue;
												}
												bool flag25 = string.IsNullOrWhiteSpace((string)list2._size);
												flag7 = (byte)(int)"To" != 0;
												if (!flag25)
												{
													flag7 = (byte)list2._size != 0;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1789 @ stack_-1C8_v43 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+28]");
												bool flag26 = string.IsNullOrWhiteSpace((string)0);
												obj4 = (_003C_003Ec__DisplayClass6_0)(object)"From";
												if (!flag26)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1789 @ stack_-1C8_v43 (System.Collections.Generic.List`1<Localisation.TextIdentifier>)+28]");
													obj4 = (_003C_003Ec__DisplayClass6_0)0;
												}
												if (flag7)
												{
													if (((string)flag7).StartsWith("Out", StringComparison.OrdinalIgnoreCase))
													{
														string s = ((string)flag7).Substring(3);
														if (int.TryParse(s, out result))
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
															RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
															Type typeFromHandle = Type.GetTypeFromHandle(handle);
															ExportPackage exportPackage3 = (ExportPackage)(result + -2);
															Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
															string text8 = $"To {arg}";
															bool flag27 = (object)value3 == null;
															text7 = "To {0}";
															if (!flag27)
															{
																NodePort nodePort = value3.AddDynamicOutput(typeFromHandle, Node.ConnectionType.Override, Node.TypeConstraint.None, fieldName);
																text6 = text8;
																flag9 = false;
																exportPackage2 = exportPackage3;
																nodePort2 = nodePort;
																goto IL_1a2c;
															}
															throw new NullReferenceException();
														}
													}
													if ((object)value3 != null)
													{
														NodePort outputPort = value3.GetOutputPort((string)flag7);
														flag9 = false;
														nodePort2 = outputPort;
														goto IL_1a2c;
													}
													throw new NullReferenceException();
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
											IL_1a2c:
											if ((object)value2 != null)
											{
												NodePort inputPort = value2.GetInputPort((string)(object)obj4);
												flag13 = inputPort != null;
												NodePort nodePort3 = inputPort;
												if (!flag13)
												{
													bool flag28 = (string)(object)obj4 != "From";
													bool flag29 = !flag28;
													nodePort3 = inputPort;
													if (!flag29)
													{
														string[] array = new string[5];
														bool flag30 = array == null;
														text7 = (string)(object)typeof(string[]);
														if (flag30)
														{
															throw new NullReferenceException();
														}
														bool flag31 = array.Length <= 0;
														text7 = (string)(object)typeof(string[]);
														if (flag31)
														{
															throw new IndexOutOfRangeException();
														}
														array[0] = "Input port '";
														text7 = (string)(array + 32);
														if (array.Length <= 1)
														{
															throw new IndexOutOfRangeException();
														}
														array[1] = (string)(object)obj4;
														text7 = (string)(array + 40);
														if (array.Length <= 2)
														{
															throw new IndexOutOfRangeException();
														}
														array[2] = "' not found on ";
														if ((object)value2 == null)
														{
															throw new NullReferenceException();
														}
														string name = value2.name;
														if (array.Length <= 3)
														{
															throw new IndexOutOfRangeException();
														}
														array[3] = name;
														text7 = (string)(array + 56);
														if (array.Length <= 4)
														{
															throw new IndexOutOfRangeException();
														}
														array[4] = ", falling back to 'From'.";
														string message2 = string.Concat(array);
														nint num7 = (nint)typeof(Debug);
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4387 @ rcx_v112 (Il2CppClass<UnityEngine.Debug>)+E4]");
														flag13 = (nint)0 != 0;
														Debug.LogWarning(message2);
														if ((object)value2 == null)
														{
															throw new NullReferenceException();
														}
														NodePort inputPort2 = value2.GetInputPort("From");
														nodePort3 = inputPort2;
													}
												}
												if (nodePort2 != null && nodePort3 != null)
												{
													nodePort2.Connect(nodePort3);
													continue;
												}
												string[] array2 = new string[8];
												bool flag32 = array2 == null;
												text7 = (string)(object)typeof(string[]);
												if (!flag32)
												{
													bool flag33 = array2.Length <= 0;
													text7 = (string)(object)typeof(string[]);
													if (!flag33)
													{
														array2[0] = "Failed to connect ports: ";
														if ((object)value3 != null)
														{
															string name2 = value3.name;
															if (array2.Length > 1)
															{
																array2[1] = name2;
																text7 = (string)(array2 + 40);
																if (array2.Length > 2)
																{
																	array2[2] = ".";
																	text7 = (string)(array2 + 48);
																	if (array2.Length > 3)
																	{
																		array2[3] = (string)flag7;
																		text7 = (string)(array2 + 56);
																		if (array2.Length > 4)
																		{
																			array2[4] = " -> ";
																			if ((object)value2 != null)
																			{
																				string name3 = value2.name;
																				if (array2.Length > 5)
																				{
																					array2[5] = name3;
																					text7 = (string)(array2 + 72);
																					if (array2.Length > 6)
																					{
																						array2[6] = ".";
																						text7 = (string)(array2 + 80);
																						if (array2.Length > 7)
																						{
																							array2[7] = (string)(object)obj4;
																							string message3 = string.Concat(array2);
																							nint num8 = (nint)typeof(Debug);
																							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4413 @ rcx_v100 (Il2CppClass<UnityEngine.Debug>)+E4]");
																							flag13 = (nint)0 != 0;
																							Debug.LogWarning(message3);
																							continue;
																						}
																						throw new IndexOutOfRangeException();
																					}
																					throw new IndexOutOfRangeException();
																				}
																				throw new IndexOutOfRangeException();
																			}
																			throw new NullReferenceException();
																		}
																		throw new IndexOutOfRangeException();
																	}
																	throw new IndexOutOfRangeException();
																}
																throw new IndexOutOfRangeException();
															}
															throw new IndexOutOfRangeException();
														}
														throw new NullReferenceException();
													}
													throw new IndexOutOfRangeException();
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										enumerator3.Dispose();
										missionDefinition = (MissionDefinition)(object)text6;
										continue;
									}
									throw new NullReferenceException();
								}
								enumerator2.Dispose();
								goto IL_17db;
							}
						}
					}
				}
			}
		}
		goto IL_1618;
		IL_1618:
		throw new NullReferenceException();
		IL_17db:
		return missionGraph2;
		IL_1880:
		Dictionary<string, NodeReference>.ValueCollection.Enumerator enumerator4 = default(Dictionary<string, NodeReference>.ValueCollection.Enumerator);
		Vector2 pivot = default(Vector2);
		float num9;
		if (exportPackage._003CFiles_003Ek__BackingField != null)
		{
			textIdentifier = (TextIdentifier)(object)CS_0024_003C_003E8__locals34.missionDef;
			if (CS_0024_003C_003E8__locals34.missionDef == null)
			{
				goto IL_1618;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+30]");
			if (!string.IsNullOrEmpty((string)0))
			{
				flag7 = (byte)(int)exportPackage._003CFiles_003Ek__BackingField != 0;
				Func<ExportFile, bool> func = delegate(ExportFile x)
				{
					//IL_0074: Expected I4, but got O
					if (x != null)
					{
						MissionDefinition missionDef9 = CS_0024_003C_003E8__locals34.missionDef;
						if (CS_0024_003C_003E8__locals34.missionDef != null)
						{
							return x._003CName_003Ek__BackingField == missionDef9.MapTopoImage;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				};
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
				List<TextIdentifier> list3 = default(List<TextIdentifier>);
				bool flag34 = list3 == null;
				bool flag9 = false;
				if (!flag34)
				{
					byte[] data = Convert.FromBase64String((string)list3._size);
					Texture2D texture2D = new Texture2D(2, 2);
					bool flag35 = ImageConversion.LoadImage(texture2D, data);
					if ((object)texture2D == null)
					{
						throw new NullReferenceException();
					}
					bool width = (byte)texture2D.width != 0;
					int height = texture2D.height;
					Sprite mapTopographyOverride = Sprite.Create(texture2D, (Rect)(&enumerator4), pivot, num9);
					if ((object)missionGraph2 == null)
					{
						throw new NullReferenceException();
					}
					missionGraph2.MapTopographyOverride = mapTopographyOverride;
					Debug.Log("Loaded map topo image");
					float num10 = num9;
					missionDefinition = null;
					flag9 = false;
					flag7 = width;
				}
			}
		}
		goto IL_1830;
		IL_17e0:
		key = "STR_MISSION_TITLE_" + text3;
		textIdentifier = (TextIdentifier)(object)"STR_MISSION_TITLE_";
		flag7 = (byte)(int)"STR_MISSION_TITLE_" != 0;
		goto IL_03f4;
		IL_0611:
		object obj6 = default(object);
		if (obj6 != null || exportPackage == null)
		{
			goto IL_1830;
		}
		if (exportPackage._003CFiles_003Ek__BackingField != null)
		{
			textIdentifier = (TextIdentifier)(object)CS_0024_003C_003E8__locals34.missionDef;
			if (CS_0024_003C_003E8__locals34.missionDef == null)
			{
				goto IL_1618;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ rcx_v41 (Localisation.TextIdentifier)+28]");
			if (!string.IsNullOrEmpty((string)0))
			{
				flag7 = (byte)(int)exportPackage._003CFiles_003Ek__BackingField != 0;
				Func<ExportFile, bool> func2 = delegate(ExportFile x)
				{
					//IL_0074: Expected I4, but got O
					if (x != null)
					{
						MissionDefinition missionDef9 = CS_0024_003C_003E8__locals34.missionDef;
						if (CS_0024_003C_003E8__locals34.missionDef != null)
						{
							return x._003CName_003Ek__BackingField == missionDef9.MapImage;
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				};
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
				List<TextIdentifier> list4 = default(List<TextIdentifier>);
				bool flag36 = list4 == null;
				bool flag9 = false;
				if (!flag36)
				{
					byte[] data2 = Convert.FromBase64String((string)list4._size);
					Texture2D texture2D2 = new Texture2D(2, 2);
					bool flag37 = ImageConversion.LoadImage(texture2D2, data2);
					if ((object)texture2D2 != null)
					{
						bool width2 = (byte)texture2D2.width != 0;
						int height2 = texture2D2.height;
						Sprite mapOverride = Sprite.Create(texture2D2, (Rect)(&enumerator4), pivot, 100f);
						if ((object)missionGraph2 != null)
						{
							missionGraph2.MapOverride = mapOverride;
							Debug.Log("Loaded map image");
							float num10 = 100f;
							num9 = 100f;
							missionDefinition = null;
							flag9 = false;
							flag7 = width2;
							goto IL_1880;
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
			}
		}
		num9 = 100f;
		goto IL_1880;
	}

	private unsafe static void ApplyData(object target, Dictionary<string, object> data, ExportPackage package = null, string missionId = null)
	{
		//IL_0087: Expected O, but got I
		//IL_009d: Expected O, but got I
		//IL_1cc1: Expected O, but got I4
		//IL_0113: Expected O, but got I
		//IL_0124: Expected I, but got O
		//IL_0144: Expected O, but got I
		//IL_029f: Expected O, but got I
		//IL_1cd6: Expected I, but got O
		//IL_05ee: Expected O, but got I
		//IL_0307: Expected I, but got O
		//IL_0739: Expected O, but got I
		//IL_074a: Expected I, but got O
		//IL_1df6: Expected O, but got I
		//IL_0238: Expected O, but got I
		//IL_1b13: Expected O, but got I
		//IL_0b87: Expected O, but got I
		//IL_0688: Expected O, but got I
		//IL_0583: Expected I, but got O
		//IL_05ab: Expected O, but got I
		//IL_05b3: Expected I, but got O
		//IL_05c0: Expected I, but got O
		//IL_1b47: Expected O, but got I
		//IL_1b4c: Expected I, but got O
		//IL_1b59: Expected I, but got O
		//IL_1a70: Expected O, but got I
		//IL_0ba6: Expected O, but got I
		//IL_0274: Expected O, but got I
		//IL_0274: Expected O, but got I
		//IL_0289: Expected I, but got O
		//IL_06f6: Expected O, but got I
		//IL_06fe: Expected I, but got O
		//IL_070b: Expected I, but got O
		//IL_1ab3: Expected I, but got O
		//IL_1ac0: Expected I, but got O
		//IL_0be8: Expected O, but got I
		//IL_1ae8: Expected O, but got I
		//IL_1af0: Expected I, but got O
		//IL_1afd: Expected I, but got O
		//IL_1a11: Expected O, but got I
		//IL_1a21: Expected I, but got O
		//IL_1a2e: Expected I, but got O
		//IL_0c07: Expected O, but got I
		//IL_0839: Expected O, but got I
		//IL_0b47: Expected I, but got O
		//IL_0440: Expected O, but got I
		//IL_0c49: Expected O, but got I
		//IL_0aff: Expected O, but got I
		//IL_0b0d: Expected I, but got O
		//IL_0b12: Expected I, but got O
		//IL_19b5: Expected O, but got I
		//IL_19c5: Expected I, but got O
		//IL_19d2: Expected I, but got O
		//IL_0c68: Expected O, but got I
		//IL_048a: Expected I, but got O
		//IL_048f: Expected I, but got O
		//IL_0899: Expected I, but got O
		//IL_08a9: Expected O, but got I
		//IL_08b9: Expected O, but got I
		//IL_08ca: Expected O, but got I
		//IL_08e5: Expected I, but got O
		//IL_08fd: Expected O, but got I
		//IL_04ec: Expected O, but got F4
		//IL_04ec: Expected O, but got Ref
		//IL_0509: Expected O, but got I
		//IL_0caa: Expected O, but got I
		//IL_093b: Expected O, but got I
		//IL_0520: Expected O, but got I4
		//IL_0528: Expected I, but got O
		//IL_0535: Expected I, but got O
		//IL_1961: Expected O, but got I
		//IL_1969: Expected I, but got O
		//IL_1976: Expected I, but got O
		//IL_0cd9: Expected O, but got I
		//IL_096a: Expected I, but got O
		//IL_097a: Expected O, but got I
		//IL_098b: Expected O, but got I
		//IL_09a6: Expected I, but got O
		//IL_09b6: Expected O, but got I
		//IL_0d1b: Expected O, but got I
		//IL_1f4f: Expected O, but got I
		//IL_1f5c: Expected I, but got O
		//IL_0a06: Expected I, but got O
		//IL_0a16: Expected O, but got I
		//IL_0a27: Expected O, but got I
		//IL_0a42: Expected I, but got O
		//IL_0a57: Expected O, but got I
		//IL_0ad4: Expected O, but got I
		//IL_0adc: Expected I, but got O
		//IL_0ae1: Expected I, but got O
		//IL_0d78: Expected I, but got O
		//IL_0aac: Expected I, but got O
		//IL_0ab4: Expected I, but got O
		//IL_187f: Expected I, but got O
		//IL_0d9f: Expected I, but got O
		//IL_0dad: Expected I, but got O
		//IL_0dbd: Expected O, but got I
		//IL_1395: Expected I, but got O
		//IL_0df9: Expected O, but got I
		//IL_1838: Expected I, but got O
		//IL_183d: Expected I, but got O
		//IL_13b4: Expected I, but got O
		//IL_13c2: Expected I, but got O
		//IL_13d2: Expected O, but got I
		//IL_13fe: Expected I, but got O
		//IL_160d: Expected O, but got I
		//IL_1864: Expected I, but got O
		//IL_1869: Expected I, but got O
		//IL_1425: Expected O, but got I
		//IL_1452: Expected I, but got O
		//IL_18db: Expected I, but got O
		//IL_18e0: Expected I, but got O
		//IL_162c: Expected O, but got I
		//IL_146e: Expected I, but got O
		//IL_17a1: Expected O, but got I
		//IL_17ba: Expected O, but got I
		//IL_17ba: Expected O, but got I
		//IL_1666: Expected O, but got I
		//IL_147b: Expected I, but got O
		//IL_0e4e: Expected O, but got I
		//IL_17cc: Expected I, but got O
		//IL_0e6c: Expected I, but got O
		//IL_0e7c: Expected O, but got I
		//IL_0e8c: Expected O, but got I
		//IL_1696: Expected O, but got I
		//IL_0f28: Expected O, but got I
		//IL_1491: Expected O, but got I
		//IL_0eb6: Expected O, but got I
		//IL_0ed7: Expected I, but got O
		//IL_0eef: Expected O, but got I
		//IL_16d2: Expected I, but got O
		//IL_16e8: Expected I, but got O
		//IL_175d: Expected O, but got I
		//IL_1369: Expected I, but got O
		//IL_1521: Expected O, but got I
		//IL_153a: Expected O, but got I
		//IL_1550: Expected I, but got O
		//IL_103e: Expected I, but got O
		//IL_1ba7: Expected O, but got I
		//IL_0fd2: Expected O, but got I
		//IL_0fe8: Expected O, but got I
		//IL_0fff: Expected I, but got O
		//IL_1078: Expected I, but got O
		//IL_1088: Expected O, but got I
		//IL_1106: Expected O, but got I4
		//IL_110e: Expected I, but got O
		//IL_1116: Expected O, but got I
		//IL_114f: Expected O, but got I
		//IL_1ea1: Expected O, but got I
		//IL_1276: Expected O, but got I
		//IL_12be: Expected O, but got I
		//IL_12cb: Expected I, but got O
		//IL_123c: Expected O, but got I4
		//IL_1340: Expected O, but got I
		//IL_1349: Expected I, but got O
		//IL_134e: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
		object obj2 = default(object);
		object obj = obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v293 @ r8_v2+6D8] (should have been resolved before IL gen)");
		object result = null;
		int result2 = 0;
		ExportPackage exportPackage = null;
		Type type2 = default(Type);
		Type type = type2;
		object obj3 = target;
		string text = null;
		Dictionary<string, object> dictionary = default(Dictionary<string, object>);
		IntPtr intPtr = default(IntPtr);
		ExportPackage exportPackage2 = default(ExportPackage);
		object obj10 = default(object);
		nint num4;
		object obj13 = default(object);
		float num7 = default(float);
		_003C_003Ec__DisplayClass7_0 obj14 = default(_003C_003Ec__DisplayClass7_0);
		object obj19 = default(object);
		object obj21 = default(object);
		Texture2D texture2D4 = default(Texture2D);
		JToken jToken = default(JToken);
		object obj27 = default(object);
		object obj29 = default(object);
		string text6 = default(string);
		int length = default(int);
		IntPtr intPtr2 = default(IntPtr);
		IntPtr intPtr3 = default(IntPtr);
		object value4 = default(object);
		object value5 = default(object);
		object value6 = default(object);
		object arg3 = default(object);
		object arg4 = default(object);
		nint num6;
		while (true)
		{
			string text2 = text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+18]");
			if ((nint)text2 >= 0)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			FieldInfo fieldInfo = (FieldInfo)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			string name = ((MemberInfo)0).Name;
			bool flag = dictionary.TryGetValue(name, out var value);
			bool flag2 = !flag;
			nint num = 0;
			if (flag2)
			{
				goto IL_1cb2;
			}
			_003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals16 = new _003C_003Ec__DisplayClass7_0();
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(MapEntityIcon));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			Type fieldType = ((FieldInfo)0).FieldType;
			nint num2 = (nint)typeFromHandle;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1313 @ r8_v39 (Il2CppClass<System.Type>)+298]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1313 @ r8_v39 (Il2CppClass<System.Type>)+2A0]");
			object obj4 = 0;
			nint num5;
			_003C_003Ec__DisplayClass7_0 obj7;
			if (typeFromHandle.IsAssignableFrom(fieldType))
			{
				if (CS_0024_003C_003E8__locals16 == null)
				{
					num4 = (nint)typeFromHandle;
					byte[] array = (byte[])value;
					Texture2D texture2D = (Texture2D)(object)typeFromHandle;
					throw new NullReferenceException();
				}
				if (value == null)
				{
					CS_0024_003C_003E8__locals16.icon = null;
				}
				else
				{
					object obj5 = value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					bool flag3 = obj5 != null;
					object icon = null;
					if (!flag3)
					{
						icon = value;
					}
					CS_0024_003C_003E8__locals16.icon = (string)icon;
					object obj6 = value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					if (obj6 == null)
					{
						goto IL_1d42;
					}
				}
				if (CS_0024_003C_003E8__locals16.icon != null)
				{
					MapEntityIcon[] array2 = Resources.LoadAll<MapEntityIcon>("MapEntityIcons");
					Func<MapEntityIcon, bool> func = delegate(MapEntityIcon x)
					{
						//IL_0048: Expected I4, but got O
						if ((object)x == null)
						{
							NullReferenceException ex = new NullReferenceException();
							return (byte)(int)ex != 0;
						}
						return x.ID == CS_0024_003C_003E8__locals16.icon;
					};
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
					bool flag4 = (UnityEngine.Object)(nint)intPtr != null;
					bool flag5 = !flag4;
					num3 = 0;
					if (!flag5)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
						((FieldInfo)0).SetValue(obj3, (nint)intPtr);
						num5 = intPtr;
						obj7 = CS_0024_003C_003E8__locals16;
						num = unchecked((nint)null);
						goto IL_1cb2;
					}
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			MapEntityIcon[] fieldType2 = (MapEntityIcon[])(object)((FieldInfo)0).FieldType;
			Type typeFromHandle2 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Sprite));
			if (fieldType2.Equals(typeFromHandle2))
			{
				bool flag6 = CS_0024_003C_003E8__locals16 == null;
				num6 = (nint)typeof(Sprite);
				obj4 = null;
				byte[] array = (byte[])value;
				Texture2D texture2D = (Texture2D)(object)fieldType2;
				if (flag6)
				{
					goto IL_1d42;
				}
				if (value == null)
				{
					CS_0024_003C_003E8__locals16.spriteName = null;
				}
				else
				{
					object obj8 = value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					bool flag7 = obj8 != null;
					byte[] spriteName = null;
					if (!flag7)
					{
						spriteName = (byte[])value;
					}
					CS_0024_003C_003E8__locals16.spriteName = (string)(object)spriteName;
					object obj9 = value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					if (obj9 == null)
					{
						goto IL_1d9e;
					}
				}
				if (CS_0024_003C_003E8__locals16.spriteName != null)
				{
					bool flag8 = exportPackage2 == null;
					num = num3;
					if (!flag8)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2166 @ stack_18 (MissionImporter+ExportPackage)+20]");
						bool flag9 = (nint)0 == 0;
						num = num3;
						if (!flag9)
						{
							Func<ExportFile, bool> func2 = delegate(ExportFile x)
							{
								//IL_0048: Expected I4, but got O
								if (x == null)
								{
									NullReferenceException ex = new NullReferenceException();
									return (byte)(int)ex != 0;
								}
								return x._003CName_003Ek__BackingField == CS_0024_003C_003E8__locals16.spriteName;
							};
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
							bool flag10 = obj10 == null;
							num = 0;
							if (!flag10)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ stack_-C0+18]");
								byte[] array3 = Convert.FromBase64String((string)0);
								Texture2D texture2D2 = new Texture2D(2, 2);
								bool flag11 = ImageConversion.LoadImage(texture2D2, array3);
								bool flag12 = (object)texture2D2 == null;
								num4 = (nint)texture2D2;
								num3 = unchecked((nint)null);
								obj4 = null;
								array = array3;
								texture2D = texture2D2;
								if (flag12)
								{
									break;
								}
								object obj11 = texture2D2;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v3263 @ rdx_v230+188] (should have been resolved before IL gen)");
								object obj12 = texture2D2;
								Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v3268 @ rdx_v232+1A8] (should have been resolved before IL gen)");
								Sprite value2 = Sprite.Create(texture2D2, (Rect)(&obj13), (Vector2)num7, 100f);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
								((FieldInfo)0).SetValue(obj3, value2);
								float num8 = num7;
								obj13 = 0;
								num5 = (nint)texture2D2;
								obj7 = obj14;
								num = unchecked((nint)null);
								goto IL_1cb2;
							}
						}
					}
					goto IL_1d9e;
				}
			}
			Type typeFromHandle3 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ScriptableObject));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			Type fieldType3 = ((FieldInfo)0).FieldType;
			if (typeFromHandle3.IsAssignableFrom(fieldType3) && value != null)
			{
				object obj15 = value;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				bool flag13 = obj15 != null;
				object obj16 = null;
				if (!flag13)
				{
					obj16 = value;
				}
				if (obj16 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
					Type fieldType4 = ((FieldInfo)0).FieldType;
					UnityEngine.Object obj17 = Resources.Load((string)obj16, fieldType4);
					if (obj17 == null)
					{
						Debug.LogWarning("Asset load was null");
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
					((FieldInfo)0).SetValue(obj3, obj17);
					num5 = (nint)obj17;
					obj7 = CS_0024_003C_003E8__locals16;
					num = unchecked((nint)null);
					goto IL_1cb2;
				}
			}
			Type typeFromHandle4 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(UnityEngine.Object));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			Type fieldType5 = ((FieldInfo)0).FieldType;
			nint num9 = (nint)typeFromHandle4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ r8_v48 (Il2CppClass<System.Type>)+298]");
			num = 0;
			bool flag14 = typeFromHandle4.IsAssignableFrom(fieldType5);
			bool flag15 = !flag14;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ r8_v48 (Il2CppClass<System.Type>)+298]");
			num3 = 0;
			if (!flag15)
			{
				bool flag16 = value == null;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ r8_v48 (Il2CppClass<System.Type>)+298]");
				num3 = 0;
				if (!flag16)
				{
					object obj18 = value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					bool flag17 = obj18 != null;
					obj7 = null;
					if (!flag17)
					{
						obj7 = (_003C_003Ec__DisplayClass7_0)value;
					}
					bool flag18 = obj7 == null;
					num3 = num;
					if (!flag18)
					{
						GameObject gameObject = Resources.Load<GameObject>((string)(object)obj7);
						if (!(gameObject != null))
						{
							string text3 = "Prefab not found at path: " + (string)(object)obj7;
							Debug.LogWarning(text3);
							num5 = (nint)text3;
							goto IL_1cb2;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
						object fieldType6 = ((FieldInfo)0).FieldType;
						Type typeFromHandle5 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(GameObject));
						if (fieldType6.Equals(typeFromHandle5))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
							((FieldInfo)0).SetValue(obj19, gameObject);
							num5 = (nint)typeof(GameObject);
							num = unchecked((nint)null);
							obj3 = obj19;
							goto IL_1cb2;
						}
						Type typeFromHandle6 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(Component));
						nint num10 = (nint)fieldInfo;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3364 @ rdx_v195 (Il2CppClass<System.Reflection.FieldInfo>)+248]");
						obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3364 @ rdx_v195 (Il2CppClass<System.Reflection.FieldInfo>)+250]");
						byte[] array = (byte[])0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
						Type fieldType7 = ((FieldInfo)0).FieldType;
						bool flag19 = (object)typeFromHandle6 == null;
						num6 = (nint)typeFromHandle6;
						num3 = num;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
						Texture2D texture2D = (Texture2D)0;
						obj3 = fieldType6;
						if (flag19)
						{
							throw new NullReferenceException();
						}
						object obj20 = typeFromHandle6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3378 @ r8_v117+298]");
						num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3378 @ r8_v117+2A0]");
						obj4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v3378 @ r8_v117+298] (should have been resolved before IL gen)");
						if (obj21 != null)
						{
							nint num11 = (nint)fieldInfo;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3399 @ rdx_v198 (Il2CppClass<System.Reflection.FieldInfo>)+250]");
							array = (byte[])0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
							Type fieldType8 = ((FieldInfo)0).FieldType;
							bool flag20 = (object)gameObject == null;
							num6 = (nint)typeFromHandle6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
							texture2D = (Texture2D)0;
							obj3 = fieldType6;
							if (!flag20)
							{
								Component component = gameObject.GetComponent(fieldType8);
								if (component == null)
								{
									nint num12 = (nint)fieldInfo;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3498 @ rdx_v204 (Il2CppClass<System.Reflection.FieldInfo>)+250]");
									array = (byte[])0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
									Type fieldType9 = ((FieldInfo)0).FieldType;
									bool flag21 = (object)fieldType9 == null;
									num6 = (nint)component;
									obj4 = null;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
									texture2D = (Texture2D)0;
									obj3 = fieldType6;
									if (flag21)
									{
										throw new NullReferenceException();
									}
									string name2 = fieldType9.Name;
									string text4 = "Component " + name2 + " not found on prefab " + (string)(object)obj7;
									Debug.LogWarning(text4);
									num5 = (nint)text4;
									num = (nint)obj7;
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
									((FieldInfo)0).SetValue(obj19, component);
									num5 = (nint)component;
									num = unchecked((nint)null);
								}
								obj3 = obj19;
								goto IL_1cb2;
							}
							throw new NullReferenceException();
						}
						obj3 = obj19;
					}
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			Type fieldType10 = ((FieldInfo)0).FieldType;
			string arg;
			TextIdentifier textIdentifier;
			string arg2;
			if (!fieldType10.IsEnum)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
				_003C_003Ec__DisplayClass7_0 fieldType11 = (_003C_003Ec__DisplayClass7_0)(object)((FieldInfo)0).FieldType;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
				RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
				Type typeFromHandle7 = Type.GetTypeFromHandle(handle);
				if (!fieldType11.Equals(typeFromHandle7))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
					_003C_003Ec__DisplayClass7_0 fieldType12 = (_003C_003Ec__DisplayClass7_0)(object)((FieldInfo)0).FieldType;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
					RuntimeTypeHandle handle2 = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle8 = Type.GetTypeFromHandle(handle2);
					if (!fieldType12.Equals(typeFromHandle8))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
						_003C_003Ec__DisplayClass7_0 fieldType13 = (_003C_003Ec__DisplayClass7_0)(object)((FieldInfo)0).FieldType;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
						RuntimeTypeHandle handle3 = (RuntimeTypeHandle)((nint)0 + (nint)32);
						Type typeFromHandle9 = Type.GetTypeFromHandle(handle3);
						if (!fieldType13.Equals(typeFromHandle9))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
							_003C_003Ec__DisplayClass7_0 fieldType14 = (_003C_003Ec__DisplayClass7_0)(object)((FieldInfo)0).FieldType;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
							RuntimeTypeHandle handle4 = (RuntimeTypeHandle)((nint)0 + (nint)32);
							Type typeFromHandle10 = Type.GetTypeFromHandle(handle4);
							if (!fieldType14.Equals(typeFromHandle10))
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
								_003C_003Ec__DisplayClass7_0 fieldType15 = (_003C_003Ec__DisplayClass7_0)(object)((FieldInfo)0).FieldType;
								Type typeFromHandle11 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(TextIdentifier));
								if (!fieldType15.Equals(typeFromHandle11))
								{
									bool flag22 = value == null;
									num5 = (nint)typeof(TextIdentifier);
									obj7 = fieldType15;
									num = num3;
									if (!flag22)
									{
										num = (nint)value;
										nint num13 = (nint)typeof(JArray);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2454 @ r8_v86 (Il2CppClass<Newtonsoft.Json.Linq.JArray>)+130]");
										object obj22 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v476 @ r9_v36 (Il2CppMethodInfo)+130]");
										nint num14 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2454 @ r8_v86 (Il2CppClass<Newtonsoft.Json.Linq.JArray>)+130]");
										if (num14 >= 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v476 @ r9_v36 (Il2CppMethodInfo)+C8]");
											object obj23 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3699 @ rax_v235+FFFFFFF8+v3572 @ rax_v223*8]");
											if (0 == (nint)typeof(JArray))
											{
												object obj24 = null;
												obj24 = value;
												if (obj24 != null)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
													Type fieldType16 = ((FieldInfo)0).FieldType;
													bool isArray = fieldType16.IsArray;
													nint num15 = (nint)fieldInfo;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3898 @ rcx_v164 (Il2CppClass<System.Reflection.FieldInfo>)+248]");
													obj4 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3898 @ rcx_v164 (Il2CppClass<System.Reflection.FieldInfo>)+250]");
													byte[] array = (byte[])0;
													Texture2D texture2D3;
													if (isArray)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
														Type fieldType17 = ((FieldInfo)0).FieldType;
														bool flag23 = (object)fieldType17 == null;
														num6 = (nint)typeof(TextIdentifier);
														num3 = num;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
														Texture2D texture2D = (Texture2D)0;
														if (flag23)
														{
															throw new NullReferenceException();
														}
														Texture2D elementType = (Texture2D)(object)fieldType17.GetElementType();
														texture2D3 = elementType;
													}
													else
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
														Type fieldType18 = ((FieldInfo)0).FieldType;
														Type[] genericTypeArguments = fieldType18.GenericTypeArguments;
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF410");
														texture2D3 = texture2D4;
													}
													if (!((object)texture2D3).Equals((object)null))
													{
														Type typeFromHandle12 = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(List<>));
														Type[] array4 = new Type[1];
														if ((object)texture2D3 != null)
														{
															object obj25 = array4;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4196 @ rdx_v178+40]");
															array = (byte[])0;
															Texture2D texture2D5 = texture2D3;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4196 @ rdx_v178+40]");
															bool flag24 = ((Dictionary<string, object>)(object)texture2D5).TryGetValue((string)0, out *(object*)null);
															bool flag25 = !flag24;
															num6 = (nint)array4;
															num3 = num;
															obj4 = null;
															Texture2D texture2D = texture2D3;
															if (flag25)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
																throw jToken;
															}
														}
														bool flag26 = array4.Length <= 0;
														num6 = (nint)array4;
														num3 = num;
														obj4 = null;
														string text5;
														ExportPackage exportPackage3;
														if (flag26)
														{
															text5 = (string)num3;
															exportPackage3 = (ExportPackage)obj4;
															throw new IndexOutOfRangeException();
														}
														array4[0] = (Type)(object)texture2D3;
														nint num16 = (nint)typeFromHandle12;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4211 @ r8_v94 (Il2CppClass<System.Type>)+920]");
														exportPackage3 = (ExportPackage)0;
														Type type3 = typeFromHandle12.MakeGenericType(array4);
														object obj26 = Activator.CreateInstance(type3);
														if (obj26 == null)
														{
															obj7 = null;
														}
														else
														{
															Type typeFromHandle13 = typeof(IList);
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4211 @ r8_v94 (Il2CppClass<System.Type>)+920]");
															bool flag27 = ((Dictionary<string, object>)obj26).TryGetValue((string)(object)typeFromHandle13, out *(object*)null);
															bool flag28 = !flag27;
															obj7 = (_003C_003Ec__DisplayClass7_0)flag27;
															num6 = (nint)obj26;
															text5 = (string)num;
															obj3 = typeof(IList);
															if (flag28)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
																throw new NullReferenceException();
															}
														}
														IEnumerator<JToken> enumerator = ((JArray)obj24).GetEnumerator();
														nint num17 = (nint)(&exportPackage);
														text5 = (string)num;
														obj3 = typeof(IList);
														while (true)
														{
															bool flag29 = exportPackage == null;
															num6 = (nint)(&exportPackage);
															exportPackage3 = exportPackage;
															if (!flag29)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
																if (obj27 == null)
																{
																	break;
																}
																bool flag30 = exportPackage == null;
																num6 = (nint)(&exportPackage);
																exportPackage3 = exportPackage;
																JToken jToken2 = null;
																if (!flag30)
																{
																	Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
																	object obj28 = ConvertValue((JToken)obj29, (Type)(object)texture2D3, exportPackage2, text6);
																	bool flag31 = obj7 == null;
																	num6 = (nint)(&exportPackage);
																	text5 = text6;
																	exportPackage3 = exportPackage2;
																	obj3 = obj29;
																	if (!flag31)
																	{
																		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
																		text5 = (string)obj28;
																		jToken2 = (JToken)2;
																		obj3 = obj29;
																		continue;
																	}
																	throw new NullReferenceException();
																}
																throw new NullReferenceException();
															}
															throw new NullReferenceException();
														}
														bool flag32 = num17 == 0;
														exportPackage3 = exportPackage;
														if (!flag32)
														{
															exportPackage3 = (ExportPackage)num17;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
														}
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
														Type fieldType19 = ((FieldInfo)0).FieldType;
														bool flag33 = (object)fieldType19 == null;
														num6 = (nint)(&exportPackage);
														if (flag33)
														{
															throw new NullReferenceException();
														}
														if (!fieldType19.IsArray)
														{
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
															((FieldInfo)0).SetValue(obj19, obj7);
															num5 = (nint)(&exportPackage);
															num = unchecked((nint)null);
															obj3 = obj19;
														}
														else
														{
															bool flag34 = obj7 == null;
															num6 = (nint)(&exportPackage);
															if (flag34)
															{
																throw new NullReferenceException();
															}
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
															Array array5 = Array.CreateInstance((Type)(object)texture2D3, length);
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003F60");
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
															((FieldInfo)0).SetValue(obj19, array5);
															num5 = (nint)array5;
															num = unchecked((nint)null);
															obj3 = obj19;
														}
													}
													else
													{
														num5 = (nint)typeof(TextIdentifier);
														obj7 = fieldType15;
													}
													goto IL_1cb2;
												}
											}
										}
										bool flag35 = value == null;
										num5 = (nint)typeof(TextIdentifier);
										obj7 = fieldType15;
										if (!flag35)
										{
											num = (nint)value;
											nint num18 = (nint)typeof(JObject);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3593 @ r8_v87 (Il2CppClass<Newtonsoft.Json.Linq.JObject>)+130]");
											object obj30 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v476 @ r9_v36 (Il2CppMethodInfo)+130]");
											nint num19 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3593 @ r8_v87 (Il2CppClass<Newtonsoft.Json.Linq.JObject>)+130]");
											bool flag36 = num19 < 0;
											num5 = (nint)typeof(TextIdentifier);
											obj7 = fieldType15;
											if (!flag36)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v476 @ r9_v36 (Il2CppMethodInfo)+C8]");
												object obj31 = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3679 @ rax_v226+FFFFFFF8+v3678 @ rax_v225*8]");
												bool flag37 = 0 != (nint)typeof(JObject);
												num5 = (nint)typeof(TextIdentifier);
												obj7 = fieldType15;
												if (!flag37)
												{
													num5 = unchecked((nint)null);
													num5 = (nint)value;
													bool flag38 = num5 == 0;
													obj7 = fieldType15;
													if (!flag38)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
														Type fieldType20 = ((FieldInfo)0).FieldType;
														object obj32 = Activator.CreateInstance(fieldType20);
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070A930");
														bool flag39 = obj32 == null;
														obj7 = (_003C_003Ec__DisplayClass7_0)obj32;
														if (!flag39)
														{
															bool flag40 = intPtr2 == (IntPtr)0;
															num5 = intPtr2;
															obj7 = (_003C_003Ec__DisplayClass7_0)obj32;
															if (!flag40)
															{
																ApplyData(obj32, (Dictionary<string, object>)(nint)intPtr2, exportPackage2, text6);
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
																((FieldInfo)0).SetValue(obj3, obj32);
																num5 = intPtr2;
																obj7 = (_003C_003Ec__DisplayClass7_0)obj32;
																num = unchecked((nint)null);
															}
														}
													}
												}
											}
										}
									}
									goto IL_1cb2;
								}
								if (value != null)
								{
									string text7 = value.ToString();
									bool flag41 = text7 == null;
									obj7 = (_003C_003Ec__DisplayClass7_0)(object)text7;
									object obj33 = null;
									if (!flag41)
									{
										bool flag42 = text7.StartsWith("{");
										bool flag43 = !flag42;
										obj7 = (_003C_003Ec__DisplayClass7_0)(object)text7;
										obj33 = null;
										if (!flag43)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070B3E0");
											bool flag44 = intPtr3 == (IntPtr)0;
											obj7 = (_003C_003Ec__DisplayClass7_0)(object)text7;
											obj33 = 0;
											if (!flag44)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ stack_-90 (Il2CppClass<UnityEngine.GameObject>)+10]");
												bool flag45 = string.IsNullOrEmpty((string)0);
												bool flag46 = !flag45;
												obj7 = (_003C_003Ec__DisplayClass7_0)(object)text7;
												if (!flag46)
												{
													bool flag47 = text6 == null;
													string text8 = (string)0;
													nint num20;
													string text11;
													if (!flag47)
													{
														string text9 = text6.ToUpper();
														bool flag48 = text9 == null;
														text8 = (string)0;
														if (!flag48)
														{
															string text10 = text9.Replace(" ", "_");
															bool flag49 = text10 != null;
															num3 = unchecked((nint)null);
															text8 = "_";
															arg = text10;
															num20 = unchecked((nint)null);
															text11 = "_";
															if (flag49)
															{
																goto IL_1f8d;
															}
														}
													}
													arg = "MISSION";
													num20 = num3;
													text11 = text8;
													goto IL_1f8d;
												}
												goto IL_1793;
											}
										}
									}
								}
								else
								{
									obj7 = null;
									object obj33 = null;
								}
								textIdentifier = new TextIdentifier();
								nint num21;
								if (text6 != null)
								{
									string text12 = text6.ToUpper();
									if (text12 != null)
									{
										string text13 = text12.Replace(" ", "_");
										bool flag50 = text13 == null;
										num21 = (nint)textIdentifier;
										num3 = unchecked((nint)null);
										object obj33 = "_";
										if (flag50)
										{
											goto IL_1f23;
										}
										arg2 = text13;
										num21 = (nint)textIdentifier;
										num3 = unchecked((nint)null);
										obj33 = "_";
										goto IL_1f9b;
									}
								}
								num21 = (nint)textIdentifier;
								goto IL_1f23;
							}
							object value3;
							if (value != null)
							{
								string text14 = value.ToString();
								value3 = text14;
							}
							else
							{
								value3 = null;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
							((FieldInfo)0).SetValue(obj3, value3);
							obj7 = fieldType14;
							num = unchecked((nint)null);
							goto IL_1cb2;
						}
						bool flag51 = Convert.ToBoolean(value);
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
						((FieldInfo)0).SetValue(obj3, value4);
						num5 = (nint)value;
						obj7 = fieldType13;
						num = unchecked((nint)null);
						goto IL_1cb2;
					}
					float num8 = Convert.ToSingle(value);
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
					((FieldInfo)0).SetValue(obj3, value5);
					float num22 = num8;
					num5 = (nint)value;
					obj7 = fieldType12;
					num = unchecked((nint)null);
					goto IL_1cb2;
				}
				int num23 = Convert.ToInt32(value);
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
				((FieldInfo)0).SetValue(obj3, value6);
				int num24 = num23;
				num5 = (nint)value;
				obj7 = fieldType11;
				num = unchecked((nint)null);
				goto IL_1cb2;
			}
			string s = value.ToString();
			if (!int.TryParse(s, out result2))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
				Type fieldType21 = ((FieldInfo)0).FieldType;
				string text15 = value.ToString();
				bool flag52 = Enum.TryParse(fieldType21, text15, out result);
				bool flag53 = !flag52;
				num5 = (nint)text15;
				obj7 = (_003C_003Ec__DisplayClass7_0)(object)fieldType21;
				num = unchecked((nint)null);
				if (!flag53)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
					((FieldInfo)0).SetValue(obj3, result);
					num5 = (nint)text15;
					obj7 = (_003C_003Ec__DisplayClass7_0)(object)fieldType21;
					num = unchecked((nint)null);
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
				Type fieldType22 = ((FieldInfo)0).FieldType;
				object value7 = Enum.ToObject(fieldType22, 0);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
				((FieldInfo)0).SetValue(obj3, value7);
				num5 = unchecked((nint)null);
				obj7 = (_003C_003Ec__DisplayClass7_0)(object)fieldType22;
				num = unchecked((nint)null);
			}
			goto IL_1cb2;
			IL_1d9e:
			Sprite sprite = Resources.Load<Sprite>(CS_0024_003C_003E8__locals16.spriteName);
			if (sprite == null)
			{
				string text16 = "Sprite not found: " + CS_0024_003C_003E8__locals16.spriteName;
				Debug.LogWarning(text16);
				num5 = (nint)text16;
				obj7 = CS_0024_003C_003E8__locals16;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
				((FieldInfo)0).SetValue(obj3, sprite);
				num5 = (nint)sprite;
				obj7 = CS_0024_003C_003E8__locals16;
				num = unchecked((nint)null);
			}
			goto IL_1cb2;
			IL_1d42:
			throw new NullReferenceException();
			IL_1793:
			ImportedText.Add((TextIdentifier)(nint)intPtr3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v7 (System.Type)+20+v393 @ rax_v10 (System.String)*8]");
			((FieldInfo)0).SetValue(obj3, (nint)intPtr3);
			num5 = intPtr3;
			num = unchecked((nint)null);
			goto IL_1cb2;
			IL_1f9b:
			List<TextIdentifier> importedText = ImportedText;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string text17 = $"STR_TELEPRINTER_{arg2}_{arg3}";
			ImportedText.Add(textIdentifier);
			int size = importedText._size;
			num5 = (nint)textIdentifier;
			num = unchecked((nint)null);
			goto IL_1cb2;
			IL_1cb2:
			text = (string)(0 + 1);
			type = type2;
			continue;
			IL_1f8d:
			List<TextIdentifier> importedText2 = ImportedText;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			string text18 = $"STR_TELEPRINTER_{arg}_{arg4}";
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ stack_-90 (Il2CppClass<UnityEngine.GameObject>)+10]");
			string text19 = "Updated to: " + (string)0;
			Debug.Log(text19);
			int size2 = importedText2._size;
			obj7 = (_003C_003Ec__DisplayClass7_0)(object)text19;
			goto IL_1793;
			IL_1f23:
			arg2 = "MISSION";
			goto IL_1f9b;
		}
		num6 = num4;
		throw new NullReferenceException();
	}

	private static object ConvertValue(JToken token, Type targetType, ExportPackage package, string missionId)
	{
		//IL_001b: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_00b3: Expected O, but got I
		//IL_00ff: Expected O, but got I
		//IL_0153: Expected I, but got O
		//IL_0161: Expected I, but got O
		//IL_0171: Expected O, but got I
		//IL_01ad: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
		RuntimeTypeHandle handle = (RuntimeTypeHandle)((nint)0 + (nint)32);
		Type typeFromHandle = Type.GetTypeFromHandle(handle);
		if (!((object)targetType).Equals((object)typeFromHandle))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502B8]");
			RuntimeTypeHandle handle2 = (RuntimeTypeHandle)((nint)0 + (nint)32);
			Type typeFromHandle2 = Type.GetTypeFromHandle(handle2);
			if (!((object)targetType).Equals((object)typeFromHandle2))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50268]");
				RuntimeTypeHandle handle3 = (RuntimeTypeHandle)((nint)0 + (nint)32);
				Type typeFromHandle3 = Type.GetTypeFromHandle(handle3);
				if (!((object)targetType).Equals((object)typeFromHandle3))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
					RuntimeTypeHandle handle4 = (RuntimeTypeHandle)((nint)0 + (nint)32);
					Type typeFromHandle4 = Type.GetTypeFromHandle(handle4);
					if (!((object)targetType).Equals((object)typeFromHandle4))
					{
						if (!targetType.IsEnum)
						{
							nint num = (nint)token;
							nint num2 = (nint)typeof(JObject);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v516 @ rdx_v26 (Il2CppClass<Newtonsoft.Json.Linq.JObject>)+130]");
							object obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v515 @ r8_v15 (Il2CppClass<Newtonsoft.Json.Linq.JToken>)+130]");
							nint num3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v516 @ rdx_v26 (Il2CppClass<Newtonsoft.Json.Linq.JObject>)+130]");
							if (num3 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v515 @ r8_v15 (Il2CppClass<Newtonsoft.Json.Linq.JToken>)+C8]");
								object obj2 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v537 @ rax_v47+FFFFFFF8+v517 @ rax_v44*8]");
								if (0 == (nint)typeof(JObject))
								{
									object obj3 = Activator.CreateInstance(targetType);
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070A930");
									Dictionary<string, object> dictionary = default(Dictionary<string, object>);
									if (obj3 != null && dictionary != null)
									{
										ApplyData(obj3, dictionary, package, missionId);
									}
									return obj3;
								}
							}
							return token.ToObject(targetType);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070A930");
						int value = default(int);
						return Enum.ToObject(targetType, value);
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070A930");
					object result = default(object);
					return result;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070A930");
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object result2 = default(object);
				return result2;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070A930");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object result3 = default(object);
			return result3;
		}
		if (token != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070A930");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object result4 = default(object);
			return result4;
		}
		return new NullReferenceException();
	}

	static MissionImporter()
	{
		List<TextIdentifier> importedText = new List<TextIdentifier>();
		ImportedText = importedText;
	}
}
