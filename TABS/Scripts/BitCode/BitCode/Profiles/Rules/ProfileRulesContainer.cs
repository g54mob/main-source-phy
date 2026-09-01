using System;
using System.Collections.Generic;
using KaEqLcVSyVlVsJaabiHCnoqSEeIhA;
using UnityEngine;

namespace BitCode.Profiles.Rules
{
	[Serializable]
	public abstract class ProfileRulesContainer<TProfile, TRules> where TProfile : class where TRules : ProfileRules<TProfile>
	{
		internal const string DefaultProfileFieldName = "defaultProfile";

		internal const string ProfileRulesFieldName = "profileRules";

		[Tooltip("The profile to use if no rules match.")]
		[SerializeField]
		private TProfile defaultProfile;

		[SerializeField]
		private List<TRules> profileRules;

		public List<TRules> Rules
		{
			get
			{
				return profileRules;
			}
			protected set
			{
				profileRules = value;
			}
		}

		public TProfile DefaultProfile
		{
			get
			{
				return defaultProfile;
			}
			protected set
			{
				defaultProfile = value;
			}
		}

		public virtual TProfile Select(IProfileSelectionState state)
		{
			using (List<TRules>.Enumerator enumerator = Rules.GetEnumerator())
			{
				TProfile profile = default(TProfile);
				TRules current = default(TRules);
				while (true)
				{
					IL_003c:
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = -1667010672;
						num2 = num;
					}
					else
					{
						num = -404367636;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ -2035133001)) % 6)
						{
						case 3u:
							num = -404367636;
							continue;
						default:
							goto end_IL_0013;
						case 0u:
							break;
						case 4u:
							profile = current.Profile;
							num = (int)((num3 * 996354234) ^ 0x15CAD9A3);
							continue;
						case 1u:
						{
							current = enumerator.Current;
							int num4;
							if (current.RulesMatch(state))
							{
								num = -1308173073;
								num4 = num;
							}
							else
							{
								num = -1122063183;
								num4 = num;
							}
							continue;
						}
						case 5u:
							goto end_IL_0013;
						case 2u:
							return profile;
						}
						goto IL_003c;
						continue;
						end_IL_0013:
						break;
					}
					break;
				}
			}
			return DefaultProfile;
		}

		public virtual bool Remove(TProfile profile, bool removeRules = false)
		{
			bool result = false;
			int num5 = default(int);
			TRules val = default(TRules);
			while (true)
			{
				int num = -895900345;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -522017203)) % 16)
					{
					case 0u:
						break;
					case 14u:
						profileRules.RemoveAt(num5);
						num = (int)((num2 * 1180116767) ^ 0x7C29D23B);
						continue;
					case 4u:
						num = (int)((num2 * 767451461) ^ 0x68519B5E);
						continue;
					case 3u:
					{
						int num12;
						int num13;
						if (!defaultProfile.Equals(profile))
						{
							num12 = 1930406743;
							num13 = num12;
						}
						else
						{
							num12 = 954654031;
							num13 = num12;
						}
						num = num12 ^ (int)(num2 * 792950577);
						continue;
					}
					case 15u:
					{
						val = profileRules[num5];
						int num11;
						if (LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(val.Profile))
						{
							num = -1056466678;
							num11 = num;
						}
						else
						{
							num = -1951733248;
							num11 = num;
						}
						continue;
					}
					case 13u:
					{
						int num7;
						int num8;
						if (val.Profile.Equals(profile))
						{
							num7 = -1008485076;
							num8 = num7;
						}
						else
						{
							num7 = -1282310371;
							num8 = num7;
						}
						num = num7 ^ ((int)num2 * -1418142221);
						continue;
					}
					case 2u:
						num = ((int)num2 * -1276234261) ^ -1597296096;
						continue;
					case 1u:
						result = true;
						num = ((int)num2 * -437159396) ^ -1918247100;
						continue;
					case 5u:
						defaultProfile = null;
						num = ((int)num2 * -1919498149) ^ -308980269;
						continue;
					case 6u:
					{
						result = true;
						int num9;
						int num10;
						if (!removeRules)
						{
							num9 = -32709701;
							num10 = num9;
						}
						else
						{
							num9 = -730321991;
							num10 = num9;
						}
						num = num9 ^ (int)(num2 * 927634023);
						continue;
					}
					case 11u:
					{
						int num6;
						if (num5 >= 0)
						{
							num = -742030686;
							num6 = num;
						}
						else
						{
							num = -840515819;
							num6 = num;
						}
						continue;
					}
					case 7u:
						num5--;
						num = -588105802;
						continue;
					case 9u:
						num5 = profileRules.Count - 1;
						num = -547865841;
						continue;
					case 12u:
						val.Profile = null;
						num = -1056466678;
						continue;
					case 10u:
					{
						int num3;
						int num4;
						if (!LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(defaultProfile))
						{
							num3 = 1683083754;
							num4 = num3;
						}
						else
						{
							num3 = 486152544;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 847232730);
						continue;
					}
					default:
						return result;
					}
					break;
				}
			}
		}

		public virtual void ForAllProfilesExcept(TProfile exclude, Action<TProfile> action)
		{
			if (!LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(defaultProfile))
			{
				goto IL_000d;
			}
			goto IL_006f;
			IL_000d:
			int num = -74627273;
			goto IL_0012;
			IL_0012:
			TProfile profile = default(TProfile);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1485686730)) % 6)
				{
				case 3u:
					break;
				case 1u:
				{
					int num8;
					int num9;
					if (defaultProfile.Equals(exclude))
					{
						num8 = 1311241842;
						num9 = num8;
					}
					else
					{
						num8 = 712134811;
						num9 = num8;
					}
					num = num8 ^ (int)(num2 * 1754833602);
					continue;
				}
				case 0u:
					goto IL_006f;
				case 2u:
					return;
				case 5u:
					action(defaultProfile);
					num = ((int)num2 * -26157111) ^ 0x31B4C697;
					continue;
				default:
				{
					using (List<TRules>.Enumerator enumerator = profileRules.GetEnumerator())
					{
						while (true)
						{
							int num3;
							int num4;
							if (enumerator.MoveNext())
							{
								num3 = -909728427;
								num4 = num3;
							}
							else
							{
								num3 = -717768497;
								num4 = num3;
							}
							while (true)
							{
								switch ((num2 = (uint)(num3 ^ -1485686730)) % 6)
								{
								case 0u:
									num3 = -909728427;
									continue;
								default:
									return;
								case 2u:
									action(profile);
									num3 = (int)((num2 * 864129772) ^ 0x151F4F6C);
									continue;
								case 1u:
								{
									int num6;
									int num7;
									if (profile.Equals(exclude))
									{
										num6 = 775892579;
										num7 = num6;
									}
									else
									{
										num6 = 34037429;
										num7 = num6;
									}
									num3 = num6 ^ ((int)num2 * -213730723);
									continue;
								}
								case 3u:
								{
									profile = enumerator.Current.Profile;
									int num5;
									if (LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(profile))
									{
										num3 = -192737348;
										num5 = num3;
									}
									else
									{
										num3 = -1291495075;
										num5 = num3;
									}
									continue;
								}
								case 4u:
									break;
								case 5u:
									return;
								}
								break;
							}
						}
					}
				}
				}
				break;
			}
			goto IL_000d;
			IL_006f:
			int num10;
			if (LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(profileRules))
			{
				num = -1529723136;
				num10 = num;
			}
			else
			{
				num = -1579875152;
				num10 = num;
			}
			goto IL_0012;
		}
	}
}
