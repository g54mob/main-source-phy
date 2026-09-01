using Unity.Entities;
using UnityEngine.PlayerLoop;

namespace EzECS.Barriers
{
	[UpdateBefore(typeof(PostLateUpdate))]
	public class PostLateUpdateBarrier : BarrierSystem
	{
	}
}
