using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMStateMachine<T> : MMIStateMachine where T : struct, IComparable, IConvertible, IFormattable
	{
		public delegate void OnStateChangeDelegate();

		public GameObject Target;

		public OnStateChangeDelegate OnStateChange;

		public virtual bool TriggerEvents { get; set; }

		public virtual T CurrentState { get; protected set; }

		public virtual T PreviousState { get; protected set; }

		public MMStateMachine(GameObject target, bool triggerEvents)
		{
		}

		public virtual void ChangeState(T newState)
		{
		}

		public virtual void RestorePreviousState()
		{
		}
	}
}
