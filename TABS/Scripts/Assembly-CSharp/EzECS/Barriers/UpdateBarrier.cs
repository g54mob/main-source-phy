using Unity.Entities;
using UnityEngine.PlayerLoop;

namespace EzECS.Barriers
{
	[UpdateBefore(typeof(Update))]
	public class UpdateBarrier : BarrierSystem
	{
	}
}
