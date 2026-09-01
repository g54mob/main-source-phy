using Unity.Entities;
using UnityEngine.PlayerLoop;

namespace EzECS.Barriers
{
	[UpdateBefore(typeof(PreLateUpdate))]
	public class PreLateUpdateBarrier : BarrierSystem
	{
	}
}
