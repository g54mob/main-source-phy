using System;
using System.Runtime.CompilerServices;
using System.Threading;
using BitCode.Extensions;
using JetBrains.Annotations;
using KaEqLcVSyVlVsJaabiHCnoqSEeIhA;
using UnityEngine;

namespace BitCode.Profiles
{
	public abstract class StateProvider<TProfileState> : ScriptableObject, IProfileSelectionStateProvider<TProfileState>, IProfileSelectionStateProvider where TProfileState : class, IProfileSelectionState
	{
		[SerializeField]
		protected bool editorSimulate;

		[SerializeField]
		protected RuntimePlatform editorSimulationPlatform;

		[SerializeField]
		protected DefaultCapabilityProvider defaults;

		public event Action<TProfileState> StateChanged
		{
			[CompilerGenerated]
			add
			{
				Action<TProfileState> action = this.StateChanged;
				Action<TProfileState> action2 = default(Action<TProfileState>);
				Action<TProfileState> value2 = default(Action<TProfileState>);
				while (true)
				{
					int num = 1651573453;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x3B4CF15C)) % 6)
						{
						case 4u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							num = 14866760;
							continue;
						case 0u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -1080499019;
								num4 = num3;
							}
							else
							{
								num3 = -1388324485;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -355770221);
							continue;
						}
						case 3u:
							action = Interlocked.CompareExchange(ref this.StateChanged, value2, action2);
							num = (int)((num2 * 114552082) ^ 0x2115A40C);
							continue;
						case 2u:
							value2 = (Action<TProfileState>)Delegate.Combine(action2, value);
							num = (int)((num2 * 1388475136) ^ 0x732A2735);
							continue;
						case 5u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<TProfileState> action = this.StateChanged;
				Action<TProfileState> action2 = default(Action<TProfileState>);
				while (true)
				{
					int num = -1672511969;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1847888732)) % 5)
						{
						case 2u:
							break;
						default:
							return;
						case 3u:
							action2 = action;
							num = -1241070753;
							continue;
						case 1u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 1930962483;
								num4 = num3;
							}
							else
							{
								num3 = 281459396;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 875789708);
							continue;
						}
						case 4u:
						{
							Action<TProfileState> value2 = (Action<TProfileState>)Delegate.Remove(action2, value);
							action = Interlocked.CompareExchange(ref this.StateChanged, value2, action2);
							num = (int)((num2 * 1325661711) ^ 0x4D1BF628);
							continue;
						}
						case 0u:
							return;
						}
						break;
					}
				}
			}
		}

		private event Action<IProfileSelectionState> nonGenericStateChanged
		{
			[CompilerGenerated]
			add
			{
				Action<IProfileSelectionState> action = this.nonGenericStateChanged;
				Action<IProfileSelectionState> action2 = default(Action<IProfileSelectionState>);
				while (true)
				{
					int num = -1115201879;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -357343897)) % 4)
						{
						case 3u:
							break;
						default:
							return;
						case 2u:
							action2 = action;
							num = -1770393950;
							continue;
						case 1u:
						{
							Action<IProfileSelectionState> value2 = (Action<IProfileSelectionState>)Delegate.Combine(action2, value);
							action = Interlocked.CompareExchange(ref this.nonGenericStateChanged, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -1942357416;
								num4 = num3;
							}
							else
							{
								num3 = -11673686;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1536198183);
							continue;
						}
						case 0u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<IProfileSelectionState> action = this.nonGenericStateChanged;
				Action<IProfileSelectionState> action2 = default(Action<IProfileSelectionState>);
				Action<IProfileSelectionState> value2 = default(Action<IProfileSelectionState>);
				while (true)
				{
					int num = 700738010;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x665397C9)) % 6)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = -414142885;
								num4 = num3;
							}
							else
							{
								num3 = -836229983;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -972101911);
							continue;
						}
						case 0u:
							value2 = (Action<IProfileSelectionState>)Delegate.Remove(action2, value);
							num = (int)(num2 * 758853927) ^ -1934253757;
							continue;
						case 5u:
							action2 = action;
							num = 2141685053;
							continue;
						case 4u:
							action = Interlocked.CompareExchange(ref this.nonGenericStateChanged, value2, action2);
							num = (int)((num2 * 1317383070) ^ 0x74F91D64);
							continue;
						case 3u:
							return;
						}
						break;
					}
				}
			}
		}

		event Action<IProfileSelectionState> IProfileSelectionStateProvider.StateChanged
		{
			add
			{
				nonGenericStateChanged += value;
			}
			remove
			{
				nonGenericStateChanged -= value;
			}
		}

		public TProfileState GetState(RuntimePlatform platform)
		{
			TProfileState runtimeState = GetRuntimeState();
			while (true)
			{
				int num = 1606831796;
				while (true)
				{
					uint num2;
					int num3;
					switch ((num2 = (uint)(num ^ 0x2C167A12)) % 4)
					{
					case 0u:
						break;
					case 2u:
					{
						int num4;
						if (!LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(runtimeState))
						{
							num3 = -563013023;
							num4 = num3;
						}
						else
						{
							num3 = -1206830817;
							num4 = num3;
						}
						goto IL_0043;
					}
					case 3u:
						return GetDefaults(platform);
					default:
						return runtimeState;
					}
					break;
					IL_0043:
					num = num3 ^ ((int)num2 * -1345467719);
				}
			}
		}

		[NotNull]
		public TProfileState GetDefaults(RuntimePlatform platform)
		{
			if (Application.isEditor)
			{
				goto IL_0007;
			}
			goto IL_0040;
			IL_0007:
			int num = -1589284360;
			goto IL_000c;
			IL_000c:
			PlatformCapabilities capabilities = default(PlatformCapabilities);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -121429654)) % 8)
				{
				case 3u:
					break;
				case 5u:
					goto IL_0040;
				case 4u:
				{
					int num5;
					int num6;
					if (!editorSimulate)
					{
						num5 = -1972497669;
						num6 = num5;
					}
					else
					{
						num5 = -1203124575;
						num6 = num5;
					}
					num = num5 ^ ((int)num2 * -1932679597);
					continue;
				}
				case 6u:
					capabilities = defaults.GetDefaultsForPlatform(platform);
					num = ((int)num2 * -1232663838) ^ -1224362874;
					continue;
				case 2u:
				{
					int num7;
					int num8;
					if (!Application.isPlaying)
					{
						num7 = 1087249027;
						num8 = num7;
					}
					else
					{
						num7 = 947851866;
						num8 = num7;
					}
					num = num7 ^ ((int)num2 * -2021736790);
					continue;
				}
				case 1u:
				{
					int num3;
					int num4;
					if (defaults != null)
					{
						num3 = 644005355;
						num4 = num3;
					}
					else
					{
						num3 = 512788997;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1472803209);
					continue;
				}
				case 7u:
					platform = editorSimulationPlatform;
					num = (int)(num2 * 235414710) ^ -1069304307;
					continue;
				default:
					return CreateForDefaults(capabilities, platform);
				}
				break;
			}
			goto IL_0007;
			IL_0040:
			capabilities = null;
			num = -682588653;
			goto IL_000c;
		}

		[NotNull]
		protected abstract TProfileState CreateForDefaults([CanBeNull] PlatformCapabilities capabilities, RuntimePlatform platform);

		[CanBeNull]
		protected abstract TProfileState GetRuntimeState();

		protected virtual void OnStateChanged(TProfileState newState)
		{
			Action<TProfileState> action = this.StateChanged;
			if (action == null)
			{
				goto IL_0034;
			}
			action.SafelyInvoke(newState);
			goto IL_0012;
			IL_0017:
			uint num;
			int num2;
			switch ((num = (uint)(num2 ^ -2105960170)) % 3)
			{
			case 0u:
				break;
			default:
				return;
			case 2u:
				goto IL_0034;
			case 1u:
				return;
			}
			goto IL_0012;
			IL_0034:
			Action<IProfileSelectionState> action2 = this.nonGenericStateChanged;
			if (action2 == null)
			{
				return;
			}
			action2.SafelyInvoke(newState);
			num2 = -429151478;
			goto IL_0017;
			IL_0012:
			num2 = -277400665;
			goto IL_0017;
		}

		IProfileSelectionState IProfileSelectionStateProvider.GetState(RuntimePlatform platform)
		{
			return GetState(platform);
		}
	}
}
