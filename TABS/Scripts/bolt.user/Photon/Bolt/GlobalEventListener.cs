using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class GlobalEventListener : GlobalEventListenerBase, IPlayerReadyEventListener, IStartBattleEventListener, IPlaceUnitEventListener, IRemoveUnitEventListener, IUnitDiedEventListener, IEndBattleEventListener, IPlayerQuitEventListener, IRemoveAllUnitsEventListener, IReplyToRemoveAllUnitsEventListener, IRespondMapChangeListener, IRequestMapChangeListener, IStartPlacementEventListener, IFailedToSpawnUnitEventListener, IReplyPlaceUnitEventListener, IGamePhaseEventListener, IUnitEnterPossessionEventListener, IUnitExitPossessionEventListener, IUnitIdsEventListener, ISpawnUnitEventListener, IInitiatorCancelledMapChangeEventListener, ISpawnProjectileEventListener, IMaxUnitsEventListener, IPlayerInfoEventListener, IFailedToLinkUnitEventListener, IProjectileHitUnitEventListener, IDebugEventListener, IRequestRulesChangeListener, IRespondRuleChangeListener, IPlayerPlatformInfoEventListener, ISpawnUnitFromPoolEventListener, IFailedToLinkPooledUnitEventListener, IPleaseStayConnectedEventListener
	{
		public virtual void OnEvent(PlayerReadyEvent evnt)
		{
		}

		public virtual void OnEvent(StartBattleEvent evnt)
		{
		}

		public virtual void OnEvent(PlaceUnitEvent evnt)
		{
		}

		public virtual void OnEvent(RemoveUnitEvent evnt)
		{
		}

		public virtual void OnEvent(UnitDiedEvent evnt)
		{
		}

		public virtual void OnEvent(EndBattleEvent evnt)
		{
		}

		public virtual void OnEvent(PlayerQuitEvent evnt)
		{
		}

		public virtual void OnEvent(RemoveAllUnitsEvent evnt)
		{
		}

		public virtual void OnEvent(ReplyToRemoveAllUnitsEvent evnt)
		{
		}

		public virtual void OnEvent(RespondMapChange evnt)
		{
		}

		public virtual void OnEvent(RequestMapChange evnt)
		{
		}

		public virtual void OnEvent(StartPlacementEvent evnt)
		{
		}

		public virtual void OnEvent(FailedToSpawnUnitEvent evnt)
		{
		}

		public virtual void OnEvent(ReplyPlaceUnitEvent evnt)
		{
		}

		public virtual void OnEvent(GamePhaseEvent evnt)
		{
		}

		public virtual void OnEvent(UnitEnterPossessionEvent evnt)
		{
		}

		public virtual void OnEvent(UnitExitPossessionEvent evnt)
		{
		}

		public virtual void OnEvent(UnitIdsEvent evnt)
		{
		}

		public virtual void OnEvent(SpawnUnitEvent evnt)
		{
		}

		public virtual void OnEvent(InitiatorCancelledMapChangeEvent evnt)
		{
		}

		public virtual void OnEvent(SpawnProjectileEvent evnt)
		{
		}

		public virtual void OnEvent(MaxUnitsEvent evnt)
		{
		}

		public virtual void OnEvent(PlayerInfoEvent evnt)
		{
		}

		public virtual void OnEvent(FailedToLinkUnitEvent evnt)
		{
		}

		public virtual void OnEvent(ProjectileHitUnitEvent evnt)
		{
		}

		public virtual void OnEvent(DebugEvent evnt)
		{
		}

		public virtual void OnEvent(RequestRulesChange evnt)
		{
		}

		public virtual void OnEvent(RespondRuleChange evnt)
		{
		}

		public virtual void OnEvent(PlayerPlatformInfoEvent evnt)
		{
		}

		public virtual void OnEvent(SpawnUnitFromPoolEvent evnt)
		{
		}

		public virtual void OnEvent(FailedToLinkPooledUnitEvent evnt)
		{
		}

		public virtual void OnEvent(PleaseStayConnectedEvent evnt)
		{
		}
	}
}
