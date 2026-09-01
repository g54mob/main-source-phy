using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode
{
	public class Initializer : PrefabFactory
	{
		private const string MainPrefabPath = "Main";

		private int deferredInitializers;

		private int initializations;

		public static bool Initialized { get; protected set; }

		[UsedImplicitly]
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Main()
		{
			PerformInitialization();
		}

		private static void PerformInitialization()
		{
			Initializer initializer = Resources.Load<Initializer>("Main");
			while (true)
			{
				int num = 2066882218;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x56145C08)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
					{
						int num3;
						int num4;
						if (!(initializer != null))
						{
							num3 = -350104243;
							num4 = num3;
						}
						else
						{
							num3 = -583028929;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 512346755);
						continue;
					}
					case 1u:
						Object.Instantiate(initializer).Init();
						num = (int)(num2 * 1180888579) ^ -1735093032;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		protected override void Awake()
		{
		}

		protected virtual void Init()
		{
			CreatePrefabs();
			using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator = PrefabFactory.createdObjects.Values.GetEnumerator())
			{
				MonoBehaviour[] componentsInChildren = default(MonoBehaviour[]);
				int num4 = default(int);
				IPreInitializationWorker preInitializationWorker = default(IPreInitializationWorker);
				while (true)
				{
					IL_0083:
					int num;
					int num2;
					if (!enumerator.MoveNext())
					{
						num = -628503919;
						num2 = num;
					}
					else
					{
						num = -2086179431;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ -964577004)) % 12)
						{
						case 0u:
							num = -2086179431;
							continue;
						default:
							goto end_IL_001d;
						case 1u:
							componentsInChildren = enumerator.Current.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
							num = -207903478;
							continue;
						case 10u:
							num4++;
							num = -11526700;
							continue;
						case 7u:
							break;
						case 3u:
						{
							preInitializationWorker = componentsInChildren[num4] as IPreInitializationWorker;
							int num6;
							if (preInitializationWorker != null)
							{
								num = -335847701;
								num6 = num;
							}
							else
							{
								num = -1964930526;
								num6 = num;
							}
							continue;
						}
						case 8u:
							num = (int)((num3 * 1092442797) ^ 0x4B70642C);
							continue;
						case 11u:
						{
							int num7;
							int num8;
							if (preInitializationWorker.Initialized)
							{
								num7 = 2048350293;
								num8 = num7;
							}
							else
							{
								num7 = 1797091086;
								num8 = num7;
							}
							num = num7 ^ (int)(num3 * 215438729);
							continue;
						}
						case 6u:
							preInitializationWorker.InitializationComplete += OnInitializationComplete;
							num = ((int)num3 * -450275202) ^ -1183634762;
							continue;
						case 9u:
							deferredInitializers++;
							num = ((int)num3 * -762162520) ^ -777638486;
							continue;
						case 2u:
							num4 = 0;
							num = (int)((num3 * 2096316883) ^ 0x4721AB76);
							continue;
						case 4u:
						{
							int num5;
							if (num4 >= componentsInChildren.Length)
							{
								num = -1764724237;
								num5 = num;
							}
							else
							{
								num = -1618992325;
								num5 = num;
							}
							continue;
						}
						case 5u:
							goto end_IL_001d;
						}
						goto IL_0083;
						continue;
						end_IL_001d:
						break;
					}
					break;
				}
			}
			if (deferredInitializers != 0)
			{
				return;
			}
			while (true)
			{
				int num9 = -1993131477;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num9 ^ -964577004)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_01a5;
					case 2u:
						return;
					}
					break;
					IL_01a5:
					DeferredInit();
					num9 = ((int)num3 * -1972777924) ^ 0x4A041B40;
				}
			}
		}

		private void OnInitializationComplete(IPreInitializationWorker completedWorker)
		{
			completedWorker.InitializationComplete -= OnInitializationComplete;
			while (true)
			{
				int num = -37034389;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1887973382)) % 5)
					{
					case 3u:
						break;
					default:
						return;
					case 4u:
						initializations++;
						num = ((int)num2 * -1725372634) ^ 0x79900477;
						continue;
					case 0u:
						DeferredInit();
						num = ((int)num2 * -895835880) ^ 0x8D0F8B7;
						continue;
					case 1u:
					{
						int num3;
						int num4;
						if (initializations != deferredInitializers)
						{
							num3 = -1481136216;
							num4 = num3;
						}
						else
						{
							num3 = -917763262;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 18280645);
						continue;
					}
					case 2u:
						return;
					}
					break;
				}
			}
		}

		protected virtual void DeferredInit()
		{
			using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator = PrefabFactory.createdObjects.Values.GetEnumerator())
			{
				int num4 = default(int);
				MonoBehaviour[] componentsInChildren = default(MonoBehaviour[]);
				IPostInitializationWorker postInitializationWorker = default(IPostInitializationWorker);
				while (true)
				{
					IL_00c1:
					int num;
					int num2;
					if (enumerator.MoveNext())
					{
						num = 1573503839;
						num2 = num;
					}
					else
					{
						num = 1244686725;
						num2 = num;
					}
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num ^ 0x2711274)) % 8)
						{
						case 2u:
							num = 1573503839;
							continue;
						default:
							goto end_IL_001a;
						case 0u:
						{
							int num6;
							if (num4 < componentsInChildren.Length)
							{
								num = 527433473;
								num6 = num;
							}
							else
							{
								num = 1347293058;
								num6 = num;
							}
							continue;
						}
						case 5u:
						{
							postInitializationWorker = componentsInChildren[num4] as IPostInitializationWorker;
							int num5;
							if (postInitializationWorker == null)
							{
								num = 723240416;
								num5 = num;
							}
							else
							{
								num = 1282217851;
								num5 = num;
							}
							continue;
						}
						case 4u:
							num4++;
							num = 1432138908;
							continue;
						case 7u:
							postInitializationWorker.PostInitialize();
							num = ((int)num3 * -1835970774) ^ -893603690;
							continue;
						case 3u:
							componentsInChildren = enumerator.Current.GetComponentsInChildren<MonoBehaviour>(includeInactive: true);
							num4 = 0;
							num = 1432138908;
							continue;
						case 6u:
							break;
						case 1u:
							goto end_IL_001a;
						}
						goto IL_00c1;
						continue;
						end_IL_001a:
						break;
					}
					break;
				}
			}
			Initialized = true;
		}
	}
}
