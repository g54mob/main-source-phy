using System;
using UnityEngine.EventSystems;

namespace BitCode.SceneManagement
{
	public class ImmediateSceneTransition : ISceneTransition
	{
		private readonly bool RSIAhBKAibisCZzCuxuLhogONnzyA;

		private EventSystem jRkDzHhcVjywvMlWsxcqVtUEkcHRA;

		public ImmediateSceneTransition(bool controlEventSystems)
		{
			while (true)
			{
				int num = 206919470;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x469F6668)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0028;
					case 1u:
						return;
					}
					break;
					IL_0028:
					RSIAhBKAibisCZzCuxuLhogONnzyA = controlEventSystems;
					num = (int)(num2 * 15265698) ^ -902739840;
				}
			}
		}

		public void StartTransition(Action sceneSwitch, bool willEnterLoadingScene)
		{
			if (RSIAhBKAibisCZzCuxuLhogONnzyA)
			{
				while (true)
				{
					int num = 1288610790;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x202988E7)) % 4)
						{
						case 2u:
							break;
						case 1u:
						{
							jRkDzHhcVjywvMlWsxcqVtUEkcHRA = EventSystem.current;
							int num3;
							int num4;
							if (jRkDzHhcVjywvMlWsxcqVtUEkcHRA != null)
							{
								num3 = -1279126377;
								num4 = num3;
							}
							else
							{
								num3 = -829993636;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -555147057);
							continue;
						}
						case 3u:
							jRkDzHhcVjywvMlWsxcqVtUEkcHRA.enabled = false;
							num = ((int)num2 * -139870496) ^ -1622220365;
							continue;
						default:
							goto end_IL_0008;
						}
						break;
					}
					continue;
					end_IL_0008:
					break;
				}
			}
			sceneSwitch();
		}

		public void EnteredLoadingScene(Action queueSceneLoad)
		{
			queueSceneLoad();
		}

		public void EnteredFinalScene(Action transitionComplete)
		{
			if (RSIAhBKAibisCZzCuxuLhogONnzyA)
			{
				goto IL_0008;
			}
			goto IL_0059;
			IL_0008:
			int num = -1156125156;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1911330147)) % 5)
				{
				case 0u:
					break;
				default:
					return;
				case 4u:
				{
					int num3;
					int num4;
					if (!(jRkDzHhcVjywvMlWsxcqVtUEkcHRA != null))
					{
						num3 = -530566346;
						num4 = num3;
					}
					else
					{
						num3 = -1242744762;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1660818602);
					continue;
				}
				case 2u:
					goto IL_0059;
				case 1u:
					jRkDzHhcVjywvMlWsxcqVtUEkcHRA.enabled = true;
					num = ((int)num2 * -1368283510) ^ 0x7EFA8276;
					continue;
				case 3u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0059:
			transitionComplete();
			num = -467393601;
			goto IL_000d;
		}
	}
}
