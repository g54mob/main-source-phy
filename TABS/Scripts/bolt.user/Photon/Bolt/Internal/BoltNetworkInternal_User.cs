namespace Photon.Bolt.Internal
{
	public static class BoltNetworkInternal_User
	{
		public static void EnvironmentSetup()
		{
			Factory.Register(PlayerReadyEvent_Meta.Instance);
			Factory.Register(StartBattleEvent_Meta.Instance);
			Factory.Register(PlaceUnitEvent_Meta.Instance);
			Factory.Register(RemoveUnitEvent_Meta.Instance);
			Factory.Register(UnitDiedEvent_Meta.Instance);
			Factory.Register(EndBattleEvent_Meta.Instance);
			Factory.Register(UnitAttackEvent_Meta.Instance);
			Factory.Register(PlayerQuitEvent_Meta.Instance);
			Factory.Register(RemoveAllUnitsEvent_Meta.Instance);
			Factory.Register(ReplyToRemoveAllUnitsEvent_Meta.Instance);
			Factory.Register(RespondMapChange_Meta.Instance);
			Factory.Register(RequestMapChange_Meta.Instance);
			Factory.Register(StartPlacementEvent_Meta.Instance);
			Factory.Register(FailedToSpawnUnitEvent_Meta.Instance);
			Factory.Register(ReplyPlaceUnitEvent_Meta.Instance);
			Factory.Register(GamePhaseEvent_Meta.Instance);
			Factory.Register(UnitEnterPossessionEvent_Meta.Instance);
			Factory.Register(UnitExitPossessionEvent_Meta.Instance);
			Factory.Register(UnitIdsEvent_Meta.Instance);
			Factory.Register(SpawnUnitEvent_Meta.Instance);
			Factory.Register(InitiatorCancelledMapChangeEvent_Meta.Instance);
			Factory.Register(SpawnProjectileEvent_Meta.Instance);
			Factory.Register(MaxUnitsEvent_Meta.Instance);
			Factory.Register(PlayerInfoEvent_Meta.Instance);
			Factory.Register(FailedToLinkUnitEvent_Meta.Instance);
			Factory.Register(UnitSpecialAttackEvent_Meta.Instance);
			Factory.Register(ProjectileHitUnitEvent_Meta.Instance);
			Factory.Register(UnitTurnOnConditionalEvent_Meta.Instance);
			Factory.Register(UnitTurnOffConditionalEvent_Meta.Instance);
			Factory.Register(DebugEvent_Meta.Instance);
			Factory.Register(RequestRulesChange_Meta.Instance);
			Factory.Register(RespondRuleChange_Meta.Instance);
			Factory.Register(PlayerPlatformInfoEvent_Meta.Instance);
			Factory.Register(SpawnUnitFromPoolEvent_Meta.Instance);
			Factory.Register(FailedToLinkPooledUnitEvent_Meta.Instance);
			Factory.Register(PleaseStayConnectedEvent_Meta.Instance);
			Factory.Register(UnitState_Meta.Instance);
			Factory.Register(PossessedUnitState_Meta.Instance);
		}

		public static void EnvironmentReset()
		{
		}
	}
}
