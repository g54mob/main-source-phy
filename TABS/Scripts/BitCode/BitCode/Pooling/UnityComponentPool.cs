using System;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Pooling
{
	public class UnityComponentPool<T> : Pool<T> where T : IPoolable, Component
	{
		public UnityComponentPool([NotNull] Func<T> factory, [CanBeNull] Action<T> reset, int initialCapacity)
			: base(factory, reset, initialCapacity)
		{
		}

		public UnityComponentPool([NotNull] Func<T> factory)
			: base(factory, (Action<T>)null, 0)
		{
		}

		public UnityComponentPool([NotNull] Func<T> factory, int initialCapacity)
			: base(factory, initialCapacity)
		{
		}

		public override T Get(Action<T> resetOverride = null)
		{
			T val = base.Get(resetOverride);
			val.gameObject.SetActive(value: true);
			return val;
		}

		protected override void ReturnToPoolInternal([NotNull] T element)
		{
			element.gameObject.SetActive(value: false);
			while (true)
			{
				int num = -334248126;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -507162507)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0033;
					case 0u:
						return;
					}
					break;
					IL_0033:
					base.ReturnToPoolInternal(element);
					num = ((int)num2 * -1766032983) ^ 0x259D55A0;
				}
			}
		}

		protected override T CreateAndAddNewElement()
		{
			T val = base.CreateAndAddNewElement();
			val.gameObject.SetActive(value: false);
			return val;
		}
	}
}
