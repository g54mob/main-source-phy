using Unity.Entities;
using UnityEngine.PlayerLoop;

namespace EzECS.Barriers
{
	[UpdateBefore(typeof(FixedUpdate))]
	public class FixedUpdateBarrier : BarrierSystem
	{
	}
}
