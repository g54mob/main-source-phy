using System;
using System.Collections.Generic;

namespace BitCode.Debug.TokenResolvers
{
	internal class TypeTokenResolver : TokenResolver<Type>
	{
		protected override Type Resolve(string token)
		{
			Type result = default(Type);
			Type type = default(Type);
			using (List<System.Reflection.Assembly>.Enumerator enumerator = owningConsole.voBRoFFzKynOBYQnDLxHHntxPWlf.GetEnumerator())
			{
				while (true)
				{
					IL_0079:
					int num;
					int num2;
					if (enumerator.MoveNext())
					{
						num = -832963314;
						num2 = num;
					}
					else
					{
						num = -147496299;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ -1015725260)) % 6)
						{
						case 0u:
							num = -832963314;
							continue;
						default:
							goto end_IL_0018;
						case 5u:
							result = type;
							return result;
						case 4u:
						{
							int num4;
							int num5;
							if (type != null)
							{
								num4 = 518232407;
								num5 = num4;
							}
							else
							{
								num4 = 1083475473;
								num5 = num4;
							}
							num = num4 ^ (int)(num3 * 126158469);
							continue;
						}
						case 3u:
							break;
						case 2u:
							type = enumerator.Current.GetType(token);
							num = -606641978;
							continue;
						case 1u:
							goto end_IL_0018;
						}
						goto IL_0079;
						continue;
						end_IL_0018:
						break;
					}
					break;
				}
			}
			type = Type.GetType(token);
			while (true)
			{
				int num6 = -2081838574;
				while (true)
				{
					uint num3;
					int num7;
					switch ((num3 = (uint)(num6 ^ -1015725260)) % 5)
					{
					case 3u:
						break;
					case 1u:
					{
						int num8;
						if (type == null)
						{
							num7 = -802342145;
							num8 = num7;
						}
						else
						{
							num7 = -1036009062;
							num8 = num7;
						}
						goto IL_0103;
					}
					case 4u:
						throw new TokenResolutionException(token, typeof(Type), "Couldn't resolve type with name " + token);
					case 0u:
						return type;
					default:
						return result;
					}
					break;
					IL_0103:
					num6 = num7 ^ (int)(num3 * 1190933058);
				}
			}
		}
	}
}
