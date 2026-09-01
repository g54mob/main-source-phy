using System;

namespace Photon.Bolt
{
	internal class PossessedUnitState : NetworkState, IPossessedUnitState, IState, IDisposable
	{
		public NetworkTransform MainTransform => Storage.Values[OffsetStorage].Transform;

		public PossessedUnitState()
			: base(PossessedUnitState_Meta.Instance)
		{
		}
	}
}
