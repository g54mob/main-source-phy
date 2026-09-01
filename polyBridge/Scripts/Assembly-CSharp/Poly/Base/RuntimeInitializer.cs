using System;
using UnityEngine;

namespace Poly.Base
{
	public static class RuntimeInitializer
	{
		private static Action reinitActions = delegate
		{
		};

		public static void AddReinitAction(Action a)
		{
			reinitActions = (Action)Delegate.Combine(reinitActions, a);
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialize()
		{
			reinitActions();
		}
	}
}
