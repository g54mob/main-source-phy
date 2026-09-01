using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Particles/MMAutoDestroyParticleSystem")]
	public class MMAutoDestroyParticleSystem : MonoBehaviour
	{
		public bool DestroyParent;

		public float DestroyDelay;

		protected ParticleSystem _particleSystem;

		protected float _startTime;

		protected bool _started;

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void DestroyParticleSystem()
		{
		}
	}
}
