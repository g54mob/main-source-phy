using BitCode.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CySSvAMKspxRBYRzSLeYjkLjCHJQ
{
	internal class pPRNDTtcgXIHRSOpCFfdUCDtqZIJ : ILoadTask
	{
		private readonly string ZfaPTDQScotQlBWMPrtaTeOHHrsEA;

		private bool DmMgMyjYefCJRoAlejpEMZwScAQEb;

		private bool zogHddslRUUFGPVeqSnTEAimuLxD;

		private AsyncOperation mOrloiSyEyGTWulGlwqrqDqpSPjV;

		public float TaskProgress
		{
			get
			{
				if (zogHddslRUUFGPVeqSnTEAimuLxD)
				{
					goto IL_0008;
				}
				goto IL_0057;
				IL_0008:
				int num = 594077566;
				goto IL_000d;
				IL_000d:
				uint num2;
				switch ((num2 = (uint)(num ^ 0x62EF7BB0)) % 5)
				{
				case 0u:
					break;
				case 2u:
					goto IL_0032;
				case 4u:
					goto IL_0057;
				case 1u:
					return 0f;
				default:
					return 1f;
				}
				goto IL_0008;
				IL_0057:
				int num3;
				if (!DmMgMyjYefCJRoAlejpEMZwScAQEb)
				{
					num = 980929972;
					num3 = num;
				}
				else
				{
					num = 1621619700;
					num3 = num;
				}
				goto IL_000d;
				IL_0032:
				return mOrloiSyEyGTWulGlwqrqDqpSPjV?.progress ?? 0f;
			}
		}

		public bool IsDone
		{
			get
			{
				if (zogHddslRUUFGPVeqSnTEAimuLxD)
				{
					while (true)
					{
						int num = -1387441630;
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num ^ -313791304)) % 5)
							{
							case 3u:
								break;
							case 2u:
								goto end_IL_0008;
							case 0u:
							{
								int num5;
								int num6;
								if (mOrloiSyEyGTWulGlwqrqDqpSPjV.isDone)
								{
									num5 = -870957607;
									num6 = num5;
								}
								else
								{
									num5 = -1520858033;
									num6 = num5;
								}
								num = num5 ^ (int)(num2 * 221633871);
								continue;
							}
							case 4u:
							{
								int num3;
								int num4;
								if (mOrloiSyEyGTWulGlwqrqDqpSPjV != null)
								{
									num3 = 375806060;
									num4 = num3;
								}
								else
								{
									num3 = 2125387335;
									num4 = num3;
								}
								num = num3 ^ ((int)num2 * -1349580805);
								continue;
							}
							default:
								return true;
							}
							break;
						}
						continue;
						end_IL_0008:
						break;
					}
				}
				return DmMgMyjYefCJRoAlejpEMZwScAQEb;
			}
		}

		public pPRNDTtcgXIHRSOpCFfdUCDtqZIJ(string P_0)
		{
			ZfaPTDQScotQlBWMPrtaTeOHHrsEA = P_0;
		}

		public void Start(bool async)
		{
			zogHddslRUUFGPVeqSnTEAimuLxD = async;
			if (async)
			{
				while (true)
				{
					uint num;
					switch ((num = 82185953u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						mOrloiSyEyGTWulGlwqrqDqpSPjV = SceneManager.LoadSceneAsync(ZfaPTDQScotQlBWMPrtaTeOHHrsEA);
						return;
					}
					break;
				}
			}
			SceneManager.LoadScene(ZfaPTDQScotQlBWMPrtaTeOHHrsEA);
			DmMgMyjYefCJRoAlejpEMZwScAQEb = true;
		}

		public void Complete()
		{
		}
	}
}
