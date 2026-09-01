using System;
using UnityEngine;

namespace BitCode.Profiles.Rules
{
	[Serializable]
	public abstract class ProfileRules<TProfile>
	{
		internal const string ProfileFieldName = "profile";

		internal const string RulesFieldName = "selectionRules";

		[Tooltip("The profile that will be selected if all the rules match.")]
		[SerializeField]
		private TProfile profile;

		[Tooltip("The rules that must match for the profile to be selected.")]
		[SerializeReference]
		protected ISelectionRule[] selectionRules;

		public TProfile Profile
		{
			get
			{
				return profile;
			}
			set
			{
				profile = value;
			}
		}

		public ISelectionRule[] SelectionRules => selectionRules;

		public bool RulesMatch(IProfileSelectionState state)
		{
			ISelectionRule[] array = SelectionRules;
			int num3 = default(int);
			while (true)
			{
				int num = -886466308;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -960964267)) % 8)
					{
					case 5u:
						break;
					case 4u:
					{
						int num5;
						if (num3 < array.Length)
						{
							num = -1946693211;
							num5 = num;
						}
						else
						{
							num = -1418695793;
							num5 = num;
						}
						continue;
					}
					case 6u:
						return false;
					case 0u:
					{
						int num4;
						if (!array[num3].RuleMatches(state))
						{
							num = -104078365;
							num4 = num;
						}
						else
						{
							num = -288082346;
							num4 = num;
						}
						continue;
					}
					case 7u:
						num = ((int)num2 * -120821543) ^ -1400430482;
						continue;
					case 3u:
						num3++;
						num = -1297414703;
						continue;
					case 1u:
						num3 = 0;
						num = (int)((num2 * 1000000068) ^ 0x5A5037E6);
						continue;
					default:
						return true;
					}
					break;
				}
			}
		}
	}
}
