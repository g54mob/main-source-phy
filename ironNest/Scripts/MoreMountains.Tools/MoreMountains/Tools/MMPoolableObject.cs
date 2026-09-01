using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Object Pool/MMPoolableObject")]
	public class MMPoolableObject : MMObjectBounds
	{
		public delegate void Events();

		[Header("Events")]
		public UnityEvent ExecuteOnEnable;

		public UnityEvent ExecuteOnDisable;

		[Header("Poolable Object")]
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

		protected virtual void Update()
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
