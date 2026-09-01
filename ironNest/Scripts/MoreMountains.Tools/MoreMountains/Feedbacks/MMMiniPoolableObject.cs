using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public class MMMiniPoolableObject : MonoBehaviour
	{
		public delegate void Events();

		public float LifeTime;

		public event Events OnSpawnComplete
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public virtual void Destroy()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		public virtual void TriggerOnSpawnComplete()
		{
		}
	}
}
