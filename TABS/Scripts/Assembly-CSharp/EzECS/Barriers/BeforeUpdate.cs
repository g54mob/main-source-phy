using Unity.Entities;
using UnityEngine.PlayerLoop;

namespace EzECS.Barriers
{
	[UpdateBefore(typeof(EarlyUpdate))]
	public class BeforeUpdate : BarrierSystem
	{
	}
}
