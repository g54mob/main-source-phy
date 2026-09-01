namespace Photon.Bolt
{
	public static class BoltAssets
	{
		public static class UnitState
		{
			public static readonly string MainTransform = "MainTransform";

			public static readonly string MovementSpeed = "MovementSpeed";

			public static readonly string TargetShortNetworkId = "TargetShortNetworkId";

			public static readonly string LookDirectionAngle = "LookDirectionAngle";

			public new static string ToString()
			{
				return "UnitState";
			}
		}

		public static class PossessedUnitState
		{
			public static readonly string MainTransform = "MainTransform";

			public new static string ToString()
			{
				return "PossessedUnitState";
			}
		}

		public static class PlayerReadyEvent
		{
			public static readonly string Team = "Team";

			public static readonly string IsReady = "IsReady";

			public new static string ToString()
			{
				return "PlayerReadyEvent";
			}
		}

		public static class StartBattleEvent
		{
			public new static string ToString()
			{
				return "StartBattleEvent";
			}
		}

		public static class PlaceUnitEvent
		{
			public static readonly string UnitId = "UnitId";

			public static readonly string UnitModId = "UnitModId";

			public static readonly string Position = "Position";

			public static readonly string Rotation = "Rotation";

			public static readonly string IsCampaignUnit = "IsCampaignUnit";

			public static readonly string UnitInstanceId = "UnitInstanceId";

			public new static string ToString()
			{
				return "PlaceUnitEvent";
			}
		}

		public static class RemoveUnitEvent
		{
			public static readonly string UnitSmallNetworkId = "UnitSmallNetworkId";

			public static readonly string UnitInstanceId = "UnitInstanceId";

			public new static string ToString()
			{
				return "RemoveUnitEvent";
			}
		}

		public static class UnitDiedEvent
		{
			public static readonly string UnitSmallNetworkId = "UnitSmallNetworkId";

			public new static string ToString()
			{
				return "UnitDiedEvent";
			}
		}

		public static class EndBattleEvent
		{
			public static readonly string WinningTeam = "WinningTeam";

			public new static string ToString()
			{
				return "EndBattleEvent";
			}
		}

		public static class UnitAttackEvent
		{
			public static readonly string TargetUnitSmallNetworkId = "TargetUnitSmallNetworkId";

			public static readonly string Position = "Position";

			public static readonly string ForceDirection = "ForceDirection";

			public static readonly string ForceWeapon = "ForceWeapon";

			public new static string ToString()
			{
				return "UnitAttackEvent";
			}
		}

		public static class PlayerQuitEvent
		{
			public static readonly string Team = "Team";

			public new static string ToString()
			{
				return "PlayerQuitEvent";
			}
		}

		public static class RemoveAllUnitsEvent
		{
			public static readonly string Team = "Team";

			public new static string ToString()
			{
				return "RemoveAllUnitsEvent";
			}
		}

		public static class ReplyToRemoveAllUnitsEvent
		{
			public static readonly string Team = "Team";

			public new static string ToString()
			{
				return "ReplyToRemoveAllUnitsEvent";
			}
		}

		public static class RespondMapChange
		{
			public static readonly string MapType = "MapType";

			public static readonly string MapIndex = "MapIndex";

			public static readonly string Status = "Status";

			public new static string ToString()
			{
				return "RespondMapChange";
			}
		}

		public static class RequestMapChange
		{
			public static readonly string MapType = "MapType";

			public static readonly string MapIndex = "MapIndex";

			public new static string ToString()
			{
				return "RequestMapChange";
			}
		}

		public static class StartPlacementEvent
		{
			public new static string ToString()
			{
				return "StartPlacementEvent";
			}
		}

		public static class FailedToSpawnUnitEvent
		{
			public static readonly string UnitInstanceId = "UnitInstanceId";

			public new static string ToString()
			{
				return "FailedToSpawnUnitEvent";
			}
		}

		public static class ReplyPlaceUnitEvent
		{
			public static readonly string UnitInstanceId = "UnitInstanceId";

			public new static string ToString()
			{
				return "ReplyPlaceUnitEvent";
			}
		}

		public static class GamePhaseEvent
		{
			public static readonly string Phase = "Phase";

			public new static string ToString()
			{
				return "GamePhaseEvent";
			}
		}

		public static class UnitEnterPossessionEvent
		{
			public static readonly string UnitInstanceId = "UnitInstanceId";

			public new static string ToString()
			{
				return "UnitEnterPossessionEvent";
			}
		}

		public static class UnitExitPossessionEvent
		{
			public static readonly string UnitInstanceId = "UnitInstanceId";

			public new static string ToString()
			{
				return "UnitExitPossessionEvent";
			}
		}

		public static class UnitIdsEvent
		{
			public static readonly string UnitInstanceId = "UnitInstanceId";

			public static readonly string UnitRemoteInstanceId = "UnitRemoteInstanceId";

			public new static string ToString()
			{
				return "UnitIdsEvent";
			}
		}

		public static class SpawnUnitEvent
		{
			public static readonly string UnitId = "UnitId";

			public static readonly string UnitModId = "UnitModId";

			public static readonly string Position = "Position";

			public static readonly string Rotation = "Rotation";

			public static readonly string UnitInstanceId = "UnitInstanceId";

			public new static string ToString()
			{
				return "SpawnUnitEvent";
			}
		}

		public static class InitiatorCancelledMapChangeEvent
		{
			public new static string ToString()
			{
				return "InitiatorCancelledMapChangeEvent";
			}
		}

		public static class SpawnProjectileEvent
		{
			public static readonly string SpawnToken = "SpawnToken";

			public new static string ToString()
			{
				return "SpawnProjectileEvent";
			}
		}

		public static class MaxUnitsEvent
		{
			public static readonly string MaxUnits = "MaxUnits";

			public static readonly string HasMaxUnits = "HasMaxUnits";

			public new static string ToString()
			{
				return "MaxUnitsEvent";
			}
		}

		public static class PlayerInfoEvent
		{
			public static readonly string PlayerName = "PlayerName";

			public static readonly string MultiplayerPlatform = "MultiplayerPlatform";

			public new static string ToString()
			{
				return "PlayerInfoEvent";
			}
		}

		public static class FailedToLinkUnitEvent
		{
			public static readonly string UnitInstanceId = "UnitInstanceId";

			public static readonly string Team = "Team";

			public new static string ToString()
			{
				return "FailedToLinkUnitEvent";
			}
		}

		public static class UnitSpecialAttackEvent
		{
			public static readonly string AttackType = "AttackType";

			public static readonly string AttackToken = "AttackToken";

			public new static string ToString()
			{
				return "UnitSpecialAttackEvent";
			}
		}

		public static class ProjectileHitUnitEvent
		{
			public static readonly string ProjectileNetworkId = "ProjectileNetworkId";

			public static readonly string UnitSmallNetworkId = "UnitSmallNetworkId";

			public new static string ToString()
			{
				return "ProjectileHitUnitEvent";
			}
		}

		public static class UnitTurnOnConditionalEvent
		{
			public static readonly string ConditionalEventId = "ConditionalEventId";

			public static readonly string InstanceEventId = "InstanceEventId";

			public new static string ToString()
			{
				return "UnitTurnOnConditionalEvent";
			}
		}

		public static class UnitTurnOffConditionalEvent
		{
			public static readonly string ConditionalEventId = "ConditionalEventId";

			public static readonly string InstanceEventId = "InstanceEventId";

			public new static string ToString()
			{
				return "UnitTurnOffConditionalEvent";
			}
		}

		public static class DebugEvent
		{
			public static readonly string DebugEventType = "DebugEventType";

			public static readonly string DebugToken = "DebugToken";

			public new static string ToString()
			{
				return "DebugEvent";
			}
		}

		public static class RequestRulesChange
		{
			public static readonly string MaxUnits = "MaxUnits";

			public static readonly string MaxBudget = "MaxBudget";

			public static readonly string BlindMode = "BlindMode";

			public new static string ToString()
			{
				return "RequestRulesChange";
			}
		}

		public static class RespondRuleChange
		{
			public static readonly string Status = "Status";

			public static readonly string MaxUnits = "MaxUnits";

			public static readonly string MaxBudget = "MaxBudget";

			public static readonly string BlindMode = "BlindMode";

			public new static string ToString()
			{
				return "RespondRuleChange";
			}
		}

		public static class PlayerPlatformInfoEvent
		{
			public static readonly string PlatformInfo = "PlatformInfo";

			public new static string ToString()
			{
				return "PlayerPlatformInfoEvent";
			}
		}

		public static class SpawnUnitFromPoolEvent
		{
			public static readonly string SpawnSource = "SpawnSource";

			public static readonly string UnitSmallNetworkId = "UnitSmallNetworkId";

			public static readonly string UnitSpawnPosition = "UnitSpawnPosition";

			public static readonly string CopyOfSmallNetworkId = "CopyOfSmallNetworkId";

			public new static string ToString()
			{
				return "SpawnUnitFromPoolEvent";
			}
		}

		public static class FailedToLinkPooledUnitEvent
		{
			public static readonly string UnitInstanceId = "UnitInstanceId";

			public static readonly string PoolIndex = "PoolIndex";

			public static readonly string PoolId = "PoolId";

			public new static string ToString()
			{
				return "FailedToLinkPooledUnitEvent";
			}
		}

		public static class PleaseStayConnectedEvent
		{
			public new static string ToString()
			{
				return "PleaseStayConnectedEvent";
			}
		}

		public static string Combine(string asset1, string asset2)
		{
			return $"{asset1}.{asset2}";
		}

		public static string Combine(string asset1, string asset2, string asset3)
		{
			return $"{asset1}.{asset2}.{asset3}";
		}
	}
}
