using System;

namespace Photon.Bolt
{
	public interface IPossessedUnitState : IState, IDisposable
	{
		NetworkTransform MainTransform { get; }
	}
}
