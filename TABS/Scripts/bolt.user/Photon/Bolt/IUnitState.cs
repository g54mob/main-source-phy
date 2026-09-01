using System;

namespace Photon.Bolt
{
	public interface IUnitState : IState, IDisposable
	{
		NetworkTransform MainTransform { get; }

		int MovementSpeed { get; set; }

		int TargetShortNetworkId { get; set; }

		float LookDirectionAngle { get; set; }
	}
}
