using System;
using JetBrains.Annotations;
using KaEqLcVSyVlVsJaabiHCnoqSEeIhA;
using UnityEngine;

namespace BitCode.Profiles
{
	[CreateAssetMenu(menuName = "BitCode/Profiles/Prefab Selector", fileName = "PrefabSelector")]
	public class PrefabSelector : ScriptableObject, IAssetProcessBuild
	{
		[SerializeField]
		protected SerializedProfileSelectionStateProvider stateProvider;

		[Tooltip("When checked, this selector will make its decision based on the default platform stategiven by the state provider at build time. All other references will be removed.")]
		[SerializeField]
		protected bool selectBeforeBuild;

		[SerializeField]
		protected GameObjectSelector selectorRules;

		[HideInInspector]
		[SerializeField]
		private GameObject buildTimeSelectedPrefab;

		public bool SelectBeforeBuild => selectBeforeBuild;

		public bool RestoreState => SelectBeforeBuild;

		public GameObject Select(IProfileSelectionState state)
		{
			GameObject currentlySelectedProfile = null;
			if (buildTimeSelectedPrefab != null)
			{
				goto IL_0010;
			}
			goto IL_006a;
			IL_0010:
			int num = -1793111933;
			goto IL_0015;
			IL_0015:
			uint num2;
			switch ((num2 = (uint)(num ^ -937534987)) % 5)
			{
			case 2u:
				break;
			case 4u:
				return buildTimeSelectedPrefab;
			case 3u:
				throw new ArgumentNullException("state");
			case 0u:
				goto IL_006a;
			default:
				selectorRules.Select(state, ref currentlySelectedProfile);
				return currentlySelectedProfile;
			}
			goto IL_0010;
			IL_006a:
			int num3;
			if (!LElEEcKKrqbvLpdndKooAnmtVSOgA.nWEhxddjQmIhssJGczboRGDNJiB(state))
			{
				num = -1539707671;
				num3 = num;
			}
			else
			{
				num = -1274791214;
				num3 = num;
			}
			goto IL_0015;
		}

		[NotNull]
		protected virtual IProfileSelectionStateProvider GetStateProvider()
		{
			return stateProvider.Value;
		}

		[NotNull]
		protected virtual IProfileSelectionState GetCurrentProfileState(RuntimePlatform platform)
		{
			return GetStateProvider().GetState(platform);
		}

		protected void OnValidate()
		{
			if (!Application.isEditor)
			{
				goto IL_0007;
			}
			goto IL_0045;
			IL_0007:
			int num = -1380149239;
			goto IL_000c;
			IL_000c:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -143899090)) % 6)
				{
				case 4u:
					break;
				default:
					return;
				case 1u:
					return;
				case 5u:
					goto IL_0045;
				case 3u:
					Debug.LogError("No state provider is set on " + base.name + ", but it is required when selectBeforeBuild is true.", this);
					num = ((int)num2 * -1497742277) ^ -2097950209;
					continue;
				case 2u:
				{
					int num3;
					int num4;
					if (stateProvider.HasValue)
					{
						num3 = 1963008100;
						num4 = num3;
					}
					else
					{
						num3 = 2120259271;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1132132799);
					continue;
				}
				case 0u:
					return;
				}
				break;
			}
			goto IL_0007;
			IL_0045:
			int num5;
			if (selectBeforeBuild)
			{
				num = -408096854;
				num5 = num;
			}
			else
			{
				num = -1283915040;
				num5 = num;
			}
			goto IL_000c;
		}

		private void ProcessAtBuildTime(RuntimePlatform platform)
		{
			IProfileSelectionState currentProfileState = GetCurrentProfileState(platform);
			while (true)
			{
				int num = -544877676;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -218415518)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						buildTimeSelectedPrefab = Select(currentProfileState);
						num = (int)((num2 * 1267662126) ^ 0x2665ED5F);
						continue;
					case 1u:
						selectorRules.ForAllProfilesExcept(buildTimeSelectedPrefab, delegate(GameObject unselected)
						{
							selectorRules.Profiles.Remove(unselected);
						});
						num = ((int)num2 * -334072351) ^ 0xE8A5714;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public bool Preprocess(RuntimePlatform platform)
		{
			if (!SelectBeforeBuild)
			{
				while (true)
				{
					uint num;
					switch ((num = 1232404883u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						buildTimeSelectedPrefab = null;
						return false;
					}
					break;
				}
			}
			ProcessAtBuildTime(platform);
			return true;
		}

		public bool Postprocess(RuntimePlatform platform)
		{
			buildTimeSelectedPrefab = null;
			return SelectBeforeBuild;
		}
	}
}
