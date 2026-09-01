using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using BitCode.Debug;
using BitCode.Debug.TokenResolvers;

namespace IfbHfNncEbjZtVkjvFPZBBYcpxLmA
{
	internal class BfQDjKlCtjVjTdbJCDBiEejLgcxN : IParameterResolver, QEaanedCObJjKPrcXjEAOXtRjqeO
	{
		private readonly Dictionary<Type, ITokenResolver> cpffJvFUrCKwoLfJwyWhdylCVyPHA = new Dictionary<Type, ITokenResolver>();

		private IEnumResolver tFMicWbObmDadlGLAuElYKNYIVgB;

		private IStringParamsResolver UrBbNrYaHPGFSgKLmoRSkpeFGazJA;

		public object ResolveParameter(ParameterInfo parameter, IReadOnlyList<string> tokens, ref int lastUsedTokenIndex)
		{
			ITokenResolver tokenResolver = pUbEOrArnHKCxjNnbDYMZnnOVuJpB(parameter);
			if (tokenResolver == null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1818632632u) % 3)
					{
					case 0u:
						continue;
					case 1u:
						throw new ParameterResolverException(parameter, $"Couldn't resolve parameter {parameter.Name}. No resolver registered for type {parameter.ParameterType}.");
					}
					break;
				}
			}
			object result = default(object);
			try
			{
				if (!tokenResolver.TryResolve(tokens, ref lastUsedTokenIndex, out var resolvedToken))
				{
					goto IL_0068;
				}
				goto IL_009b;
				IL_0068:
				int num2 = -930648148;
				goto IL_006d;
				IL_006d:
				while (true)
				{
					uint num;
					switch ((num = (uint)(num2 ^ -626783227)) % 7)
					{
					case 4u:
						break;
					default:
						goto end_IL_005c;
					case 5u:
						goto IL_009b;
					case 3u:
						result = parameter.DefaultValue;
						num2 = (int)(num * 1336891624) ^ -19494922;
						continue;
					case 1u:
						goto end_IL_005c;
					case 2u:
					{
						int num3;
						int num4;
						if (parameter.HasDefaultValue)
						{
							num3 = 1543864994;
							num4 = num3;
						}
						else
						{
							num3 = 266646577;
							num4 = num3;
						}
						num2 = num3 ^ (int)(num * 1184387078);
						continue;
					}
					case 6u:
						throw new ParameterResolverException(parameter, "Insufficient parameters provided to command.");
					case 0u:
						goto end_IL_005c;
					}
					break;
				}
				goto IL_0068;
				IL_009b:
				result = resolvedToken;
				num2 = -1863133698;
				goto IL_006d;
				end_IL_005c:;
			}
			catch (TokenResolutionException)
			{
				throw;
			}
			catch (ParameterResolverException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new ParameterResolverException(parameter, "Resolver for parameter " + parameter.Name + " threw exception. Check inner exception for more info.", innerException);
			}
			return result;
		}

		public ITokenResolver GetResolverForType(Type type)
		{
			if (type.IsEnum)
			{
				goto IL_0008;
			}
			goto IL_004a;
			IL_0008:
			int num = 185997531;
			goto IL_000d;
			IL_000d:
			uint num2;
			ITokenResolver value = default(ITokenResolver);
			switch ((num2 = (uint)(num ^ 0x113F8BF8)) % 4)
			{
			case 0u:
				break;
			case 3u:
				return tFMicWbObmDadlGLAuElYKNYIVgB.GetEnumResolverForType(type);
			case 1u:
				goto IL_004a;
			default:
				return value;
			}
			goto IL_0008;
			IL_004a:
			cpffJvFUrCKwoLfJwyWhdylCVyPHA.TryGetValue(type, out value);
			num = 897241546;
			goto IL_000d;
		}

		public bool HasResolverForType(Type type)
		{
			return cpffJvFUrCKwoLfJwyWhdylCVyPHA.ContainsKey(type);
		}

		public bool HasResolverForType<T>()
		{
			return HasResolverForType(typeof(T));
		}

		public void QSIoYPRpaNAbzpKoxxBOddnQfdoB(ITokenResolver P_0)
		{
			if (HasResolverForType(P_0.ResolverType))
			{
				goto IL_0011;
			}
			goto IL_0278;
			IL_0011:
			int num = 180472035;
			goto IL_0016;
			IL_0016:
			IStringParamsResolver urBbNrYaHPGFSgKLmoRSkpeFGazJA = default(IStringParamsResolver);
			IEnumResolver enumResolver = default(IEnumResolver);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1B7038F1)) % 25)
				{
				case 4u:
					break;
				default:
					return;
				case 12u:
					return;
				case 11u:
					urBbNrYaHPGFSgKLmoRSkpeFGazJA = (IStringParamsResolver)P_0;
					num = 1101380953;
					continue;
				case 23u:
					num = (int)((num2 * 1749456259) ^ 0x5FF64F53);
					continue;
				case 18u:
					goto IL_00c5;
				case 14u:
					goto IL_00e1;
				case 13u:
					goto IL_00fd;
				case 7u:
					tFMicWbObmDadlGLAuElYKNYIVgB = enumResolver;
					return;
				case 8u:
					num = ((int)num2 * -1219768998) ^ 0x3876BEE4;
					continue;
				case 22u:
					goto IL_013d;
				case 2u:
					throw new InvalidOperationException($"Already registered a resolver for type {P_0.ResolverType}.");
				case 0u:
					cpffJvFUrCKwoLfJwyWhdylCVyPHA.Add(P_0.ResolverType, P_0);
					num = 1617992426;
					continue;
				case 3u:
					num = ((int)num2 * -252140132) ^ 0x20A0D31B;
					continue;
				case 6u:
					num = ((int)num2 * -1195628499) ^ -1423232648;
					continue;
				case 5u:
					num = ((int)num2 * -1361580462) ^ 0x7F1202C0;
					continue;
				case 24u:
					num = (int)((num2 * 1545362845) ^ 0x1E3C57DC);
					continue;
				case 10u:
					enumResolver = (IEnumResolver)P_0;
					num = 307271979;
					continue;
				case 20u:
					num = (int)((num2 * 110650720) ^ 0x53199DF4);
					continue;
				case 21u:
					UrBbNrYaHPGFSgKLmoRSkpeFGazJA = urBbNrYaHPGFSgKLmoRSkpeFGazJA;
					num = 1042139874;
					continue;
				case 19u:
					throw new InvalidOperationException("Already registered a params resolver.");
				case 9u:
					goto IL_022e;
				case 1u:
					enumResolver = (IEnumResolver)P_0;
					num = 272047044;
					continue;
				case 15u:
					throw new InvalidOperationException("Already registered an enum resolver.");
				case 16u:
					goto IL_0278;
				case 17u:
					return;
				}
				break;
				IL_022e:
				int num3;
				if (!(P_0 is IStringParamsResolver))
				{
					num = 1430495498;
					num3 = num;
				}
				else
				{
					num = 1272612926;
					num3 = num;
				}
				continue;
				IL_00c5:
				int num4;
				if (tFMicWbObmDadlGLAuElYKNYIVgB != null)
				{
					num = 826003762;
					num4 = num;
				}
				else
				{
					num = 717071358;
					num4 = num;
				}
				continue;
				IL_00fd:
				int num5;
				if (UrBbNrYaHPGFSgKLmoRSkpeFGazJA != null)
				{
					num = 578824527;
					num5 = num;
				}
				else
				{
					num = 862947067;
					num5 = num;
				}
				continue;
				IL_013d:
				int num6;
				if (!(P_0 is IStringParamsResolver))
				{
					num = 1315701958;
					num6 = num;
				}
				else
				{
					num = 329547383;
					num6 = num;
				}
				continue;
				IL_00e1:
				int num7;
				if (UrBbNrYaHPGFSgKLmoRSkpeFGazJA == null)
				{
					num = 2030976694;
					num7 = num;
				}
				else
				{
					num = 578824527;
					num7 = num;
				}
			}
			goto IL_0011;
			IL_0278:
			int num8;
			if (P_0 is IEnumResolver)
			{
				num = 923480001;
				num8 = num;
			}
			else
			{
				num = 1574891022;
				num8 = num;
			}
			goto IL_0016;
		}

		void QEaanedCObJjKPrcXjEAOXtRjqeO.QSIoYPRpaNAbzpKoxxBOddnQfdoB(ITokenResolver P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in QSIoYPRpaNAbzpKoxxBOddnQfdoB
			this.QSIoYPRpaNAbzpKoxxBOddnQfdoB(P_0);
		}

		public bool fyvYQTrRmQvhxIUOLoLpfTyocRkI(ParameterInfo P_0)
		{
			ITokenResolver tokenResolver = pUbEOrArnHKCxjNnbDYMZnnOVuJpB(P_0);
			if (tokenResolver != null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1877722756u) % 3)
					{
					case 2u:
						continue;
					case 1u:
						return tokenResolver.NeedsUserToken;
					}
					break;
				}
			}
			return false;
		}

		bool QEaanedCObJjKPrcXjEAOXtRjqeO.fyvYQTrRmQvhxIUOLoLpfTyocRkI(ParameterInfo P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in fyvYQTrRmQvhxIUOLoLpfTyocRkI
			return this.fyvYQTrRmQvhxIUOLoLpfTyocRkI(P_0);
		}

		public bool VbMKGqtUAZLbQXJHWXHrppnVIlLz(ParameterInfo P_0)
		{
			if (P_0.Position == 0)
			{
				while (true)
				{
					uint num;
					switch ((num = 625606906u) % 3)
					{
					case 2u:
						continue;
					case 1u:
						return ((MethodInfo)P_0.Member).IsDefined(typeof(ExtensionAttribute));
					}
					break;
				}
			}
			return false;
		}

		bool QEaanedCObJjKPrcXjEAOXtRjqeO.VbMKGqtUAZLbQXJHWXHrppnVIlLz(ParameterInfo P_0)
		{
			//ILSpy generated this explicit interface implementation from .override directive in VbMKGqtUAZLbQXJHWXHrppnVIlLz
			return this.VbMKGqtUAZLbQXJHWXHrppnVIlLz(P_0);
		}

		private ITokenResolver pUbEOrArnHKCxjNnbDYMZnnOVuJpB(ParameterInfo P_0)
		{
			if (VbMKGqtUAZLbQXJHWXHrppnVIlLz(P_0))
			{
				goto IL_0009;
			}
			goto IL_0048;
			IL_0009:
			int num = -1365652247;
			goto IL_000e;
			IL_000e:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1595401700)) % 6)
				{
				case 0u:
					break;
				case 1u:
					return null;
				case 4u:
					goto IL_0048;
				case 3u:
					return UrBbNrYaHPGFSgKLmoRSkpeFGazJA;
				case 2u:
				{
					int num3;
					int num4;
					if (!(P_0.ParameterType == typeof(string[])))
					{
						num3 = -393428025;
						num4 = num3;
					}
					else
					{
						num3 = -2038078679;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 284170024);
					continue;
				}
				default:
					return GetResolverForType(P_0.ParameterType);
				}
				break;
			}
			goto IL_0009;
			IL_0048:
			int num5;
			if (!P_0.IsDefined(typeof(ParamArrayAttribute)))
			{
				num = -1260934297;
				num5 = num;
			}
			else
			{
				num = -1342678632;
				num5 = num;
			}
			goto IL_000e;
		}
	}
}
