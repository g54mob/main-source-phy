using Unity.Entities;
using UnityEngine.PlayerLoop;

namespace EzECS.Barriers
{
	[UpdateBefore(typeof(PreUpdate))]
	public class PreUpdateBarrier : BarrierSystem
	{
	}
}
