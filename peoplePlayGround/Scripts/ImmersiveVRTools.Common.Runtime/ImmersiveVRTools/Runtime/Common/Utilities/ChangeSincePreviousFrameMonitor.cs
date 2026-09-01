using System;
using System.Collections;
using UnityEngine;

namespace ImmersiveVRTools.Runtime.Common.Utilities
{
	public class ChangeSincePreviousFrameMonitor<T>
	{
		private readonly Func<T> _getValue;

		private readonly MonoBehaviour _coroutineRunner;

		protected T _previousValue;

		public bool IsValueUpdatedSinceLastUpdateCall { get; private set; }

		public T CurrentValue { get; private set; }

		public ChangeSincePreviousFrameMonitor(Func<T> getValue, MonoBehaviour coroutineRunner)
			: this(getValue, coroutineRunner, true)
		{
		}

		protected ChangeSincePreviousFrameMonitor(Func<T> getValue, MonoBehaviour coroutineRunner, bool startCoroutineInCtor)
		{
			_getValue = getValue;
			_coroutineRunner = coroutineRunner;
			if (startCoroutineInCtor)
			{
				_coroutineRunner.StartCoroutine(Update());
			}
		}

		protected virtual IEnumerator Update()
		{
			while (true)
			{
				IsValueUpdatedSinceLastUpdateCall = false;
				CurrentValue = _getValue();
				ref T previousValue = ref _previousValue;
				object obj = CurrentValue;
				if (!previousValue.Equals(obj))
				{
					_previousValue = CurrentValue;
					IsValueUpdatedSinceLastUpdateCall = true;
				}
				yield return null;
			}
		}
	}
}
