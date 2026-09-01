using System;
using BitCode.Profiles.Rules;
using JetBrains.Annotations;
using KaEqLcVSyVlVsJaabiHCnoqSEeIhA;
using UnityEngine;

namespace BitCode.Profiles
{
	[Serializable]
	public abstract class Selector<TProfile, TProfileRulesType, TProfileRulesContainerType> where TProfile : class where TProfileRulesType : ProfileRules<TProfile> where TProfileRulesContainerType : ProfileRulesContainer<TProfile, TProfileRulesType>
	{
		[Serializable]
		private sealed class XwWPjKkdTBaFtALtATnTIqerqCTh
		{
			public static readonly XwWPjKkdTBaFtALtATnTIqerqCTh _003C_003E9 = new XwWPjKkdTBaFtALtATnTIqerqCTh();

			public static Action<IProfileSelectionEventListener> _003C_003E9__3_0;

			internal void ADxASwfhdwUBarkPXtNqAFpSDntOA(IProfileSelectionEventListener P_0)
			{
				P_0.WillBeSelected();
			}
		}

		[SerializeField]
		protected TProfileRulesContainerType profiles;

		public TProfileRulesContainerType Profiles => profiles;

		public bool Select([NotNull] IProfileSelectionState state, ref TProfile currentlySelectedProfile, Action<TProfile> selectedProfileAction = null, Action<TProfile> unselectedProfileAction = null)
		{
			TProfile val = Profiles.Select(state);
			while (true)
			{
				int num = 420721791;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x6A64D913)) % 11)
					{
					case 10u:
						break;
					case 5u:
					{
						int num8;
						int num9;
						if (selectedProfileAction != null)
						{
							num8 = 991127388;
							num9 = num8;
						}
						else
						{
							num8 = 69469242;
							num9 = num8;
						}
						num = num8 ^ ((int)num2 * -998221492);
						continue;
					}
					case 0u:
					{
						int num4;
						int num5;
						if (!LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(currentlySelectedProfile))
						{
							num4 = 463731888;
							num5 = num4;
						}
						else
						{
							num4 = 165602143;
							num5 = num4;
						}
						num = num4 ^ (int)(num2 * 727866822);
						continue;
					}
					case 2u:
					{
						int num6;
						int num7;
						if (val != currentlySelectedProfile)
						{
							num6 = 294421898;
							num7 = num6;
						}
						else
						{
							num6 = 1688357097;
							num7 = num6;
						}
						num = num6 ^ (int)(num2 * 1500394320);
						continue;
					}
					case 3u:
						ForAllEventListeners(currentlySelectedProfile, XwWPjKkdTBaFtALtATnTIqerqCTh._003C_003E9.ADxASwfhdwUBarkPXtNqAFpSDntOA);
						num = 1407765153;
						continue;
					case 1u:
						ForAllProfilesExcept(currentlySelectedProfile, unselectedProfileAction.Invoke);
						num = (int)(num2 * 114285040) ^ -1945259218;
						continue;
					case 7u:
						currentlySelectedProfile = val;
						num = 525662502;
						continue;
					case 9u:
					{
						int num3;
						if (unselectedProfileAction != null)
						{
							num = 2131306289;
							num3 = num;
						}
						else
						{
							num = 285909710;
							num3 = num;
						}
						continue;
					}
					case 8u:
						return false;
					case 4u:
						selectedProfileAction(currentlySelectedProfile);
						num = (int)((num2 * 1896691429) ^ 0x38613019);
						continue;
					default:
						return true;
					}
					break;
				}
			}
		}

		public void ForAllEventListeners(TProfile profile, Action<IProfileSelectionEventListener> action)
		{
			IProfileSelectionEventListener profileSelectionEventListener = profile as IProfileSelectionEventListener;
			int num3 = default(int);
			IProfileSelectionEventListener[] componentsInChildren = default(IProfileSelectionEventListener[]);
			GameObject gameObject = default(GameObject);
			Component component = default(Component);
			IProfileSelectionEventListener obj = default(IProfileSelectionEventListener);
			while (true)
			{
				int num = 1564664390;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x76EAB8F2)) % 15)
					{
					case 0u:
						break;
					default:
						return;
					case 10u:
					{
						int num8;
						if (num3 < componentsInChildren.Length)
						{
							num = 1469662201;
							num8 = num;
						}
						else
						{
							num = 189253441;
							num8 = num;
						}
						continue;
					}
					case 11u:
						gameObject = component.gameObject;
						num = (int)(num2 * 991569852) ^ -159685137;
						continue;
					case 3u:
						action(obj);
						num = ((int)num2 * -1732242387) ^ 0x25161A6F;
						continue;
					case 6u:
					{
						int num5;
						if (gameObject == null)
						{
							num = 1377398100;
							num5 = num;
						}
						else
						{
							num = 1377352413;
							num5 = num;
						}
						continue;
					}
					case 12u:
						componentsInChildren = gameObject.GetComponentsInChildren<IProfileSelectionEventListener>(includeInactive: true);
						num = 1961968579;
						continue;
					case 9u:
						num3++;
						num = ((int)num2 * -910068411) ^ 0x3CBEF55D;
						continue;
					case 13u:
						return;
					case 1u:
					{
						int num6;
						int num7;
						if (profileSelectionEventListener != null)
						{
							num6 = -82939148;
							num7 = num6;
						}
						else
						{
							num6 = -723926366;
							num7 = num6;
						}
						num = num6 ^ ((int)num2 * -630324853);
						continue;
					}
					case 8u:
					{
						component = profile as Component;
						int num4;
						if ((object)component == null)
						{
							num = 67798601;
							num4 = num;
						}
						else
						{
							num = 783912833;
							num4 = num;
						}
						continue;
					}
					case 2u:
						gameObject = profile as GameObject;
						num = 1531336859;
						continue;
					case 5u:
						obj = componentsInChildren[num3];
						num = 1493980141;
						continue;
					case 7u:
						action(profileSelectionEventListener);
						num = (int)(num2 * 758216988) ^ -636548282;
						continue;
					case 14u:
						num3 = 0;
						num = ((int)num2 * -886183907) ^ -1774298634;
						continue;
					case 4u:
						return;
					}
					break;
				}
			}
		}

		public void ForAllProfilesExcept(TProfile exclude, Action<TProfile> action)
		{
			Profiles.ForAllProfilesExcept(exclude, action);
		}
	}
}
