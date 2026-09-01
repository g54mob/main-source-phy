using System;
using System.Runtime.CompilerServices;
using System.Threading;
using BitCode.Extensions;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Profiles
{
	public class PrefabSpawner : GameObjectSelectorBase
	{
		[SerializeField]
		protected PrefabSelector selector;

		[CompilerGenerated]
		private Action<GameObject> m_SpawnedGameObjectChanged;

		public GameObject SelectedPrefab { get; protected set; }

		public event Action<GameObject> SpawnedGameObjectChanged
		{
			[CompilerGenerated]
			add
			{
				Action<GameObject> action = this.m_SpawnedGameObjectChanged;
				Action<GameObject> action2 = default(Action<GameObject>);
				Action<GameObject> value2 = default(Action<GameObject>);
				while (true)
				{
					int num = -914359885;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -796189190)) % 4)
						{
						case 3u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							value2 = (Action<GameObject>)Delegate.Combine(action2, value);
							num = -1904843756;
							continue;
						case 2u:
						{
							action = Interlocked.CompareExchange(ref this.m_SpawnedGameObjectChanged, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 1313662228;
								num4 = num3;
							}
							else
							{
								num3 = 559606813;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 809797921);
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
				Action<GameObject> action = this.m_SpawnedGameObjectChanged;
				Action<GameObject> action2 = default(Action<GameObject>);
				Action<GameObject> value2 = default(Action<GameObject>);
				while (true)
				{
					int num = -1445385705;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1203021597)) % 5)
						{
						case 0u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							num = -1706075593;
							continue;
						case 2u:
							value2 = (Action<GameObject>)Delegate.Remove(action2, value);
							num = ((int)num2 * -1201145214) ^ 0x60A0302D;
							continue;
						case 4u:
						{
							action = Interlocked.CompareExchange(ref this.m_SpawnedGameObjectChanged, value2, action2);
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 505515329;
								num4 = num3;
							}
							else
							{
								num3 = 783118499;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1161662423);
							continue;
						}
						case 3u:
							return;
						}
						break;
					}
				}
			}
		}

		public void Spawn([NotNull] IProfileSelectionState state)
		{
			if (base.SelectedGameObject != null)
			{
				goto IL_000e;
			}
			goto IL_007c;
			IL_000e:
			int num = -965626699;
			goto IL_0013;
			IL_0013:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1112030671)) % 6)
				{
				case 3u:
					break;
				default:
					return;
				case 4u:
					UnityEngine.Object.Destroy(base.SelectedGameObject);
					num = (int)((num2 * 703044763) ^ 0x96B0C34);
					continue;
				case 1u:
					base.SelectedGameObject = UnityEngine.Object.Instantiate(SelectedPrefab, base.transform);
					num = ((int)num2 * -802089318) ^ -1637968137;
					continue;
				case 5u:
					goto IL_007c;
				case 2u:
				{
					int num3;
					int num4;
					if (!(SelectedPrefab != null))
					{
						num3 = -682165327;
						num4 = num3;
					}
					else
					{
						num3 = -831261524;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1217915803);
					continue;
				}
				case 0u:
					return;
				}
				break;
			}
			goto IL_000e;
			IL_007c:
			RefreshPrefabToSpawn(state);
			num = -1647385059;
			goto IL_0013;
		}

		public override bool Select(IProfileSelectionState newState)
		{
			if (RefreshPrefabToSpawn(newState))
			{
				while (true)
				{
					int num = -201189781;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1235273804)) % 4)
						{
						case 0u:
							break;
						case 3u:
							Spawn(newState);
							num = ((int)num2 * -1661597477) ^ -35810337;
							continue;
						case 2u:
							return true;
						default:
							goto end_IL_0009;
						}
						break;
					}
					continue;
					end_IL_0009:
					break;
				}
			}
			return false;
		}

		protected virtual void OnSpawnedGameObjectChanged(GameObject newSelectedObject)
		{
			this.SpawnedGameObjectChanged?.SafelyInvoke(newSelectedObject);
		}

		protected override void OnStateChanged(IProfileSelectionState state)
		{
			if (selector.SelectBeforeBuild)
			{
				goto IL_000d;
			}
			goto IL_0047;
			IL_000d:
			int num = 541697377;
			goto IL_0012;
			IL_0012:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1DB9C979)) % 5)
				{
				case 0u:
					break;
				default:
					return;
				case 3u:
					return;
				case 1u:
					goto IL_0047;
				case 2u:
					OnSpawnedGameObjectChanged(base.SelectedGameObject);
					num = (int)((num2 * 1205831657) ^ 0x7D41EBA5);
					continue;
				case 4u:
					return;
				}
				break;
			}
			goto IL_000d;
			IL_0047:
			int num3;
			if (!Select(state))
			{
				num = 1785103220;
				num3 = num;
			}
			else
			{
				num = 1373305296;
				num3 = num;
			}
			goto IL_0012;
		}

		private bool RefreshPrefabToSpawn([NotNull] IProfileSelectionState state)
		{
			GameObject selectedPrefab = SelectedPrefab;
			SelectedPrefab = selector.Select(state);
			return SelectedPrefab != selectedPrefab;
		}
	}
}
