using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace BitCode.Validation
{
	public static class ExceptionAggregator
	{
		public static void Add(ref IList<Exception> aggregatedExceptions, Exception exception)
		{
			if (exception == null)
			{
				goto IL_0003;
			}
			goto IL_0056;
			IL_0003:
			int num = 1051081042;
			goto IL_0008;
			IL_0008:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x44E1B734)) % 6)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					aggregatedExceptions.Add(exception);
					num = 335462203;
					continue;
				case 4u:
					aggregatedExceptions = new List<Exception>();
					num = (int)(num2 * 1289980168) ^ -504800551;
					continue;
				case 5u:
					goto IL_0056;
				case 2u:
					return;
				case 3u:
					return;
				}
				break;
			}
			goto IL_0003;
			IL_0056:
			int num3;
			if (aggregatedExceptions == null)
			{
				num = 1548946844;
				num3 = num;
			}
			else
			{
				num = 798549913;
				num3 = num;
			}
			goto IL_0008;
		}

		public static void Throw([CanBeNull] IList<Exception> aggregatedExceptions)
		{
			if (aggregatedExceptions == null)
			{
				return;
			}
			while (true)
			{
				int num = 1093943121;
				while (true)
				{
					uint num2;
					int num3;
					switch ((num2 = (uint)(num ^ 0x7675F230)) % 4)
					{
					case 2u:
						break;
					case 1u:
					{
						int num4;
						if (aggregatedExceptions.Count != 0)
						{
							num3 = -1938937696;
							num4 = num3;
						}
						else
						{
							num3 = -1505788645;
							num4 = num3;
						}
						goto IL_003f;
					}
					case 0u:
						return;
					default:
						throw new AggregateException(aggregatedExceptions);
					}
					break;
					IL_003f:
					num = num3 ^ ((int)num2 * -295849921);
				}
			}
		}
	}
}
