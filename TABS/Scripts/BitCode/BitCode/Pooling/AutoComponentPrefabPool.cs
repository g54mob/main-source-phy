using System;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Pooling
{
	public class AutoComponentPrefabPool<T> : UnityComponentPool<T> where T : Component, IPoolable
	{
		protected readonly T prefab;

		protected readonly Action<T> initalize;

		private T VuPaRCfTQtbPASjwAltBkGAhNPhG()
		{
			T val = UnityEngine.Object.Instantiate(prefab);
			while (true)
			{
				int num = -1500450120;
				while (true)
				{
					uint num2;
					Action<T> action;
					switch ((num2 = (uint)(num ^ -598107183)) % 3)
					{
					case 0u:
						break;
					case 2u:
						action = initalize;
						if (action != null)
						{
							goto IL_003a;
						}
						goto default;
					default:
						return val;
					}
					break;
					IL_003a:
					action(val);
					num = (int)(num2 * 2014632171) ^ -1263378683;
				}
			}
		}

		public AutoComponentPrefabPool([NotNull] T prefab, int initialCapacity)
			: this(prefab, (Action<T>)null, (Action<T>)null, initialCapacity)
		{
		}

		public AutoComponentPrefabPool([NotNull] T prefab, Action<T> initialize = null, Action<T> reset = null, int initialCapacity = 0)
			: base((Func<T>)Pool<T>.DummyFactory, reset, 0)
		{
			while (true)
			{
				int num = 1096910231;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4E29660F)) % 7)
					{
					case 6u:
						break;
					default:
						return;
					case 4u:
						this.prefab = prefab;
						factory = VuPaRCfTQtbPASjwAltBkGAhNPhG;
						num = 487940623;
						continue;
					case 0u:
						throw new ArgumentNullException("prefab");
					case 5u:
						Grow(initialCapacity);
						num = (int)((num2 * 1650607685) ^ 0xECD2D95);
						continue;
					case 2u:
					{
						int num5;
						int num6;
						if (initialCapacity > 0)
						{
							num5 = 1121300495;
							num6 = num5;
						}
						else
						{
							num5 = 2060252053;
							num6 = num5;
						}
						num = num5 ^ ((int)num2 * -1871088547);
						continue;
					}
					case 1u:
					{
						initalize = initialize;
						int num3;
						int num4;
						if (!(prefab == null))
						{
							num3 = 1021313169;
							num4 = num3;
						}
						else
						{
							num3 = 74858707;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -1601246966);
						continue;
					}
					case 3u:
						return;
					}
					break;
				}
			}
		}
	}
}
