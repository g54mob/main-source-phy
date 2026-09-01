using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BitCode
{
	public abstract class QueueProcessor<T> : LinkedList<T>, IDisposable, IUpdateableService where T : class
	{
		private readonly IServiceUpdater UeSkHcBnUDZDTHkgfGCVbjKIjUXtA;

		[CompilerGenerated]
		private readonly bool KraWclYwrkOXtqINvWBsnCCmsFNx;

		[CompilerGenerated]
		private T yUmEAlPBAOTOIChUcODmADnfeieA;

		public bool AllowMultipleItemsPerFrame
		{
			[CompilerGenerated]
			get
			{
				return KraWclYwrkOXtqINvWBsnCCmsFNx;
			}
		}

		public T CurrentlyProcessingItem
		{
			[CompilerGenerated]
			get
			{
				return yUmEAlPBAOTOIChUcODmADnfeieA;
			}
			[CompilerGenerated]
			private set
			{
				yUmEAlPBAOTOIChUcODmADnfeieA = val;
			}
		}

		protected QueueProcessor([NotNull] IServiceUpdater P_0, bool P_1 = false)
		{
			UeSkHcBnUDZDTHkgfGCVbjKIjUXtA = P_0 ?? throw new ArgumentNullException("serviceUpdater");
			KraWclYwrkOXtqINvWBsnCCmsFNx = P_1;
			P_0.RegisterService(this);
		}

		void IUpdateableService.Update()
		{
			if (CurrentlyProcessingItem != null)
			{
				goto IL_000d;
			}
			goto IL_0087;
			IL_000d:
			int num = 1870146072;
			goto IL_0012;
			IL_0012:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x764A84A4)) % 7)
				{
				case 5u:
					break;
				case 1u:
				{
					int num3;
					int num4;
					if (Tick(CurrentlyProcessingItem))
					{
						num3 = -693197016;
						num4 = num3;
					}
					else
					{
						num3 = -1499975100;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -904790401);
					continue;
				}
				case 0u:
					CurrentlyProcessingItem = null;
					num = ((int)num2 * -1717934503) ^ 0x20595B91;
					continue;
				case 4u:
					goto IL_0087;
				case 2u:
					return;
				case 6u:
					CompleteItem(CurrentlyProcessingItem);
					num = (int)((num2 * 778978700) ^ 0x330C09BE);
					continue;
				default:
					biavTnDoIJEqrMqtmqwEkGmjIpZn();
					return;
				}
				break;
			}
			goto IL_000d;
			IL_0087:
			int num5;
			if (CurrentlyProcessingItem != null)
			{
				num = 324541343;
				num5 = num;
			}
			else
			{
				num = 789758271;
				num5 = num;
			}
			goto IL_0012;
		}

		protected abstract void CompleteItem(T completedItem);

		protected abstract bool Tick(T item);

		protected abstract void StartProcessingItem(T newItem);

		private void biavTnDoIJEqrMqtmqwEkGmjIpZn()
		{
			if (CurrentlyProcessingItem != null)
			{
				goto IL_0010;
			}
			goto IL_00e0;
			IL_0010:
			int num = 1771682975;
			goto IL_0015;
			IL_0015:
			T value = default(T);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x37356363)) % 10)
				{
				case 2u:
					break;
				default:
					return;
				case 9u:
				{
					int num5;
					int num6;
					if (!Tick(CurrentlyProcessingItem))
					{
						num5 = -257013594;
						num6 = num5;
					}
					else
					{
						num5 = -1316095502;
						num6 = num5;
					}
					num = num5 ^ ((int)num2 * -1559879567);
					continue;
				}
				case 3u:
					return;
				case 1u:
					value = base.First.Value;
					num = 637962292;
					continue;
				case 7u:
				{
					int num3;
					int num4;
					if (AllowMultipleItemsPerFrame)
					{
						num3 = -1942637170;
						num4 = num3;
					}
					else
					{
						num3 = -1111433093;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 489994482);
					continue;
				}
				case 6u:
					throw new InvalidOperationException("Cannot start processing a new item while another is still in progress");
				case 0u:
					goto IL_00e0;
				case 5u:
					RemoveFirst();
					StartProcessingItem(value);
					CurrentlyProcessingItem = value;
					num = (int)((num2 * 1420502587) ^ 0x78DB68D);
					continue;
				case 4u:
					((IUpdateableService)this).Update();
					num = ((int)num2 * -1282273591) ^ 0x18B325C7;
					continue;
				case 8u:
					return;
				}
				break;
			}
			goto IL_0010;
			IL_00e0:
			int num7;
			if (base.Count <= 0)
			{
				num = 1520406382;
				num7 = num;
			}
			else
			{
				num = 909721696;
				num7 = num;
			}
			goto IL_0015;
		}

		public void Dispose()
		{
			UeSkHcBnUDZDTHkgfGCVbjKIjUXtA.DeregisterService(this);
		}
	}
}
