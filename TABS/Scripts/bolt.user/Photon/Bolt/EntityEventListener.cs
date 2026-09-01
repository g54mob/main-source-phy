using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	public class EntityEventListener : EntityEventListenerBase, IUnitAttackEventListener, IUnitSpecialAttackEventListener, IUnitTurnOnConditionalEventListener, IUnitTurnOffConditionalEventListener, IPlayerPlatformInfoEventListener, ISpawnUnitFromPoolEventListener, IFailedToLinkPooledUnitEventListener, IPleaseStayConnectedEventListener
	{
		public virtual void OnEvent(UnitAttackEvent evnt)
		{
		}

		public virtual void OnEvent(UnitSpecialAttackEvent evnt)
		{
		}

		public virtual void OnEvent(UnitTurnOnConditionalEvent evnt)
		{
		}

		public virtual void OnEvent(UnitTurnOffConditionalEvent evnt)
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
	public class EntityEventListener<TState> : EntityEventListenerBase<TState>, IUnitAttackEventListener, IUnitSpecialAttackEventListener, IUnitTurnOnConditionalEventListener, IUnitTurnOffConditionalEventListener, IPlayerPlatformInfoEventListener, ISpawnUnitFromPoolEventListener, IFailedToLinkPooledUnitEventListener, IPleaseStayConnectedEventListener
	{
		public virtual void OnEvent(UnitAttackEvent evnt)
		{
		}

		public virtual void OnEvent(UnitSpecialAttackEvent evnt)
		{
		}

		public virtual void OnEvent(UnitTurnOnConditionalEvent evnt)
		{
		}

		public virtual void OnEvent(UnitTurnOffConditionalEvent evnt)
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
