using System;
using System.Runtime.CompilerServices;
using System.Threading;
using BitCode.Extensions;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Profiles
{
	public class SceneObjectSelector : GameObjectSelectorBase
	{
		[Serializable]
		private sealed class XwWPjKkdTBaFtALtATnTIqerqCTh
		{
			public static readonly XwWPjKkdTBaFtALtATnTIqerqCTh _003C_003E9 = new XwWPjKkdTBaFtALtATnTIqerqCTh();

			public static Action<GameObject> _003C_003E9__5_0;

			internal void ysYyBIoMKubvlQZRsYKGHxMceNRj(GameObject P_0)
			{
				P_0.SetActive(value: true);
			}
		}

		[Tooltip("Controls how to handle objects that are not selected.")]
		[SerializeField]
		protected UnselectedObjectBehaviour defaultUnselectedObjectBehaviour;

		[SerializeField]
		protected GameObjectSelector selectorRules;

		[CompilerGenerated]
		private Action<GameObject> m_SelectedGameObjectChanged;

		public event Action<GameObject> SelectedGameObjectChanged
		{
			[CompilerGenerated]
			add
			{
				Action<GameObject> action = this.m_SelectedGameObjectChanged;
				Action<GameObject> action2 = default(Action<GameObject>);
				Action<GameObject> value2 = default(Action<GameObject>);
				while (true)
				{
					int num = 1151452104;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x78548E15)) % 4)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							value2 = (Action<GameObject>)Delegate.Combine(action2, value);
							num = 2017797206;
							continue;
						case 3u:
						{
							action = Interlocked.CompareExchange(ref this.m_SelectedGameObjectChanged, value2, action2);
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = -274627903;
								num4 = num3;
							}
							else
							{
								num3 = -1141279112;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1064644803);
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
				Action<GameObject> action = this.m_SelectedGameObjectChanged;
				Action<GameObject> action2 = default(Action<GameObject>);
				Action<GameObject> value2 = default(Action<GameObject>);
				while (true)
				{
					int num = -262185482;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -315180860)) % 6)
						{
						case 3u:
							break;
						default:
							return;
						case 4u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = -1967643490;
								num4 = num3;
							}
							else
							{
								num3 = -578151979;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1212450401);
							continue;
						}
						case 5u:
							value2 = (Action<GameObject>)Delegate.Remove(action2, value);
							num = ((int)num2 * -1809954546) ^ 0x3275056C;
							continue;
						case 2u:
							action2 = action;
							num = -1949707103;
							continue;
						case 0u:
							action = Interlocked.CompareExchange(ref this.m_SelectedGameObjectChanged, value2, action2);
							num = (int)(num2 * 1501195092) ^ -693899196;
							continue;
						case 1u:
							return;
						}
						break;
					}
				}
			}
		}

		public override bool Select([NotNull] IProfileSelectionState state)
		{
			GameObject currentlySelectedProfile = base.SelectedGameObject;
			bool num = selectorRules.Select(state, ref currentlySelectedProfile, XwWPjKkdTBaFtALtATnTIqerqCTh._003C_003E9.ysYyBIoMKubvlQZRsYKGHxMceNRj, delegate(GameObject unselected)
			{
				UnselectedObjectBehaviour unselectedObjectBehaviour = defaultUnselectedObjectBehaviour;
				while (true)
				{
					int num2 = 1482564845;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num2 ^ 0x6D09E4B1)) % 7)
						{
						case 3u:
							break;
						case 6u:
							num2 = (int)(num3 * 861154937) ^ -1067344031;
							continue;
						case 0u:
							UnityEngine.Object.DestroyImmediate(unselected);
							return;
						case 2u:
						{
							int num6;
							int num7;
							if (unselectedObjectBehaviour == UnselectedObjectBehaviour.Disable)
							{
								num6 = -1797604884;
								num7 = num6;
							}
							else
							{
								num6 = -54584766;
								num7 = num6;
							}
							num2 = num6 ^ ((int)num3 * -584226327);
							continue;
						}
						case 5u:
							unselected.SetActive(value: false);
							return;
						case 1u:
						{
							int num4;
							int num5;
							if (unselectedObjectBehaviour != UnselectedObjectBehaviour.Destroy)
							{
								num4 = -678151056;
								num5 = num4;
							}
							else
							{
								num4 = -1001798770;
								num5 = num4;
							}
							num2 = num4 ^ ((int)num3 * -1910175583);
							continue;
						}
						default:
							throw new ArgumentOutOfRangeException();
						}
						break;
					}
				}
			});
			if (num)
			{
				base.SelectedGameObject = currentlySelectedProfile;
			}
			return num;
		}

		protected virtual void OnSelectedGameObjectChanged(GameObject newSelectedObject)
		{
			this.SelectedGameObjectChanged?.SafelyInvoke(newSelectedObject);
		}

		protected override void OnStateChanged(IProfileSelectionState state)
		{
			if (defaultUnselectedObjectBehaviour == UnselectedObjectBehaviour.Destroy)
			{
				goto IL_0009;
			}
			goto IL_004e;
			IL_0009:
			int num = 1991536026;
			goto IL_000e;
			IL_000e:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x78CB366F)) % 5)
				{
				case 0u:
					break;
				default:
					return;
				case 4u:
					OnSelectedGameObjectChanged(base.SelectedGameObject);
					num = (int)(num2 * 1428553899) ^ -1975864751;
					continue;
				case 1u:
					goto IL_004e;
				case 2u:
					return;
				case 3u:
					return;
				}
				break;
			}
			goto IL_0009;
			IL_004e:
			int num3;
			if (Select(state))
			{
				num = 978494745;
				num3 = num;
			}
			else
			{
				num = 69455235;
				num3 = num;
			}
			goto IL_000e;
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (!Application.isEditor)
			{
				goto IL_000d;
			}
			goto IL_0043;
			IL_000d:
			int num = -1992695077;
			goto IL_0012;
			IL_0012:
			uint num2;
			switch ((num2 = (uint)(num ^ -115491275)) % 4)
			{
			case 0u:
				break;
			default:
				return;
			case 2u:
				return;
			case 3u:
				goto IL_0043;
			case 1u:
				return;
			}
			goto IL_000d;
			IL_0043:
			selectorRules.Profiles.ForAllProfilesExcept(null, delegate(GameObject referencedGameObject)
			{
				if (referencedGameObject.transform.IsChildOf(base.transform))
				{
					goto IL_0013;
				}
				goto IL_0066;
				IL_0013:
				int num3 = -65007770;
				goto IL_0018;
				IL_0018:
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num3 ^ -1874635556)) % 5)
					{
					case 3u:
						break;
					default:
						return;
					case 0u:
						Debug.LogError("All profiles handled by a SceneObjectSelector must be children of it. " + $"Removed {referencedGameObject}.");
						num3 = (int)(num4 * 1763592710) ^ -498210354;
						continue;
					case 1u:
						goto IL_0066;
					case 4u:
						return;
					case 2u:
						return;
					}
					break;
				}
				goto IL_0013;
				IL_0066:
				int num5;
				if (!selectorRules.Profiles.Remove(referencedGameObject))
				{
					num3 = -993565604;
					num5 = num3;
				}
				else
				{
					num3 = -1425372769;
					num5 = num3;
				}
				goto IL_0018;
			});
			num = -1504493968;
			goto IL_0012;
		}
	}
}
