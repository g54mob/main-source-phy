using System;
using System.Collections.Generic;
using BitCode.Extensions;
using JetBrains.Annotations;

namespace BitCode.Pooling
{
	public class Pool<T> : IPool<T>, IPool where T : IPoolable
	{
		protected Func<T> factory;

		protected readonly Action<T> reset;

		protected readonly List<T> availableItems;

		protected readonly List<T> allItems;

		public int TotalCount => allItems.Count;

		public int AvailableCount => availableItems.Count;

		public Pool([NotNull] Func<T> factory, Action<T> reset = null, int initialCapacity = 0)
		{
			while (true)
			{
				int num = -1726709698;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1891381069)) % 7)
					{
					case 0u:
						break;
					default:
						return;
					case 6u:
						this.factory = factory ?? throw new ArgumentNullException("factory");
						this.reset = reset;
						num = ((int)num2 * -1272765440) ^ -576375279;
						continue;
					case 5u:
						availableItems = new List<T>();
						num = (int)((num2 * 1617788811) ^ 0x49CC3688);
						continue;
					case 1u:
						allItems = new List<T>();
						num = (int)((num2 * 454809889) ^ 0x13DFA6BC);
						continue;
					case 4u:
					{
						int num3;
						int num4;
						if (initialCapacity <= 0)
						{
							num3 = -204192117;
							num4 = num3;
						}
						else
						{
							num3 = -1652011741;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 1095715331);
						continue;
					}
					case 3u:
						Grow(initialCapacity);
						num = ((int)num2 * -1189521314) ^ 0x34BE4379;
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}

		public Pool([NotNull] Func<T> factory, int initialCapacity)
			: this(factory, (Action<T>)null, initialCapacity)
		{
		}

		public virtual T Get(Action<T> resetOverride = null)
		{
			if (resetOverride == null)
			{
				goto IL_0003;
			}
			goto IL_0048;
			IL_0003:
			int num = 1854209085;
			goto IL_0008;
			IL_0008:
			T val = default(T);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x4A362218)) % 6)
				{
				case 4u:
					break;
				case 5u:
					resetOverride = reset;
					num = ((int)num2 * -2007155610) ^ -1486879755;
					continue;
				case 1u:
					goto IL_0048;
				case 0u:
				{
					int num3;
					int num4;
					if (resetOverride == null)
					{
						num3 = 1324267610;
						num4 = num3;
					}
					else
					{
						num3 = 888014533;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1762998033);
					continue;
				}
				case 3u:
					resetOverride.SafelyInvoke(val);
					num = ((int)num2 * -1878670675) ^ 0x11B08283;
					continue;
				default:
					return val;
				}
				break;
			}
			goto IL_0003;
			IL_0048:
			val = BgPmhumXdYSMAmdWUbkuGCGrvqxx();
			num = 1802102886;
			goto IL_0008;
		}

		public virtual bool Contains([NotNull] T pooledItem)
		{
			return allItems.Contains(pooledItem);
		}

		public virtual void Return([NotNull] T pooledItem)
		{
			ReturnToPoolInternal(pooledItem);
		}

		public virtual void ReturnAll()
		{
			ReturnAll(null);
		}

		public virtual void ReturnAll([CanBeNull] Action<T> preReturn)
		{
			int num = 0;
			T val = default(T);
			while (true)
			{
				int num2 = 1954825824;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ 0x612B244F)) % 9)
					{
					case 7u:
						break;
					default:
						return;
					case 2u:
						num2 = (int)((num3 * 1274730170) ^ 0x3D439BE5);
						continue;
					case 6u:
						ReturnToPoolInternal(val);
						num2 = 1041072442;
						continue;
					case 5u:
					{
						val = allItems[num];
						int num6;
						if (availableItems.Contains(val))
						{
							num2 = 1041072442;
							num6 = num2;
						}
						else
						{
							num2 = 1528388484;
							num6 = num2;
						}
						continue;
					}
					case 0u:
					{
						int num7;
						if (num >= allItems.Count)
						{
							num2 = 1371166147;
							num7 = num2;
						}
						else
						{
							num2 = 1165370325;
							num7 = num2;
						}
						continue;
					}
					case 3u:
					{
						int num4;
						int num5;
						if (preReturn == null)
						{
							num4 = -1126918748;
							num5 = num4;
						}
						else
						{
							num4 = -287300446;
							num5 = num4;
						}
						num2 = num4 ^ ((int)num3 * -416425600);
						continue;
					}
					case 1u:
						preReturn.SafelyInvoke(val);
						num2 = ((int)num3 * -961573628) ^ -1978169456;
						continue;
					case 4u:
						num++;
						num2 = 958741443;
						continue;
					case 8u:
						return;
					}
					break;
				}
			}
		}

		public void Grow(int amount)
		{
			int num = 0;
			while (true)
			{
				int num2;
				int num3;
				if (num >= amount)
				{
					num2 = -2013502035;
					num3 = num2;
				}
				else
				{
					num2 = -703334220;
					num3 = num2;
				}
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num2 ^ -313278751)) % 4)
					{
					case 3u:
						num2 = -703334220;
						continue;
					default:
						return;
					case 1u:
						CreateAndAddNewElement();
						num++;
						num2 = -91319797;
						continue;
					case 2u:
						break;
					case 0u:
						return;
					}
					break;
				}
			}
		}

		protected virtual void ReturnToPoolInternal(T element)
		{
			availableItems.Add(element);
		}

		protected virtual T CreateAndAddNewElement()
		{
			T val = factory();
			val.Owner = this;
			allItems.Add(val);
			availableItems.Add(val);
			return val;
		}

		protected static T DummyFactory()
		{
			return default(T);
		}

		IPoolable IPool.Get(Action<IPoolable> resetOverride)
		{
			T val = BgPmhumXdYSMAmdWUbkuGCGrvqxx();
			if (resetOverride != null)
			{
				goto IL_000a;
			}
			goto IL_004f;
			IL_000a:
			int num = 685804220;
			goto IL_000f;
			IL_000f:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x268F79C8)) % 5)
				{
				case 3u:
					break;
				case 4u:
					resetOverride.SafelyInvoke(val);
					num = (int)(num2 * 1570473322) ^ -1846039932;
					continue;
				case 0u:
					goto IL_004f;
				case 1u:
					num = (int)((num2 * 2020353569) ^ 0x313E9D30);
					continue;
				default:
					goto IL_0077;
				}
				break;
			}
			goto IL_000a;
			IL_004f:
			Action<T> action = reset;
			if (action == null)
			{
				goto IL_0077;
			}
			action.SafelyInvoke(val);
			num = 1444781300;
			goto IL_000f;
			IL_0077:
			return val;
		}

		bool IPool.Contains(IPoolable pooledItem)
		{
			return Contains((T)pooledItem);
		}

		void IPool.Return(IPoolable pooledItem)
		{
			Return((T)pooledItem);
		}

		private T BgPmhumXdYSMAmdWUbkuGCGrvqxx()
		{
			if (availableItems.Count == 0)
			{
				goto IL_000d;
			}
			goto IL_0080;
			IL_000d:
			int num = -1064370008;
			goto IL_0012;
			IL_0012:
			int index = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -359465439)) % 6)
				{
				case 5u:
					break;
				case 3u:
					Grow(1);
					num = (int)(num2 * 1409082172) ^ -684742751;
					continue;
				case 1u:
					throw new InvalidOperationException("Failed to grow pool");
				case 2u:
					index = availableItems.Count - 1;
					num = -577181225;
					continue;
				case 0u:
					goto IL_0080;
				default:
				{
					T result = availableItems[index];
					availableItems.RemoveAt(index);
					return result;
				}
				}
				break;
			}
			goto IL_000d;
			IL_0080:
			int num3;
			if (availableItems.Count != 0)
			{
				num = -1297843761;
				num3 = num;
			}
			else
			{
				num = -1969589720;
				num3 = num;
			}
			goto IL_0012;
		}
	}
}
