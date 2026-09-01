using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using BitCode.Dlc;
using BitCode.ErrorHandling;
using BitCode.Extensions;
using BitCode.IO;
using BitCode.L10n;
using BitCode.Networking;
using BitCode.Platform;
using BitCode.Users;

namespace BitCode
{
	public abstract class DisposablePlatformServices : IDisposable, IPlatformServices
	{
		private readonly List<IPlatformService> services = new List<IPlatformService>();

		protected bool disposed;

		[CompilerGenerated]
		private Action<IPlatformService, Exception> m_InternalErrorOccurred;

		public virtual ILocalAccountManager LocalAccountManager { get; }

		public virtual ISaveDataManager SaveDataManager { get; }

		public virtual IDlcManager DlcManager { get; }

		public virtual IMultiplayerSessionManager MultiplayerSessionManager { get; }

		public virtual IFriendManager FriendManager { get; }

		public virtual IGameInvitationManager GameInvitationManager { get; }

		public virtual IAchievementManager AchievementManager { get; }

		public ISystemLanguageProvider LanguageProvider { get; }

		public IProfanityFilter ProfanityFilter { get; }

		public IPopupDialog PopupDialog { get; }

		public IVirtualKeyboard VirtualKeyboard { get; }

		public ExceptionHandlingService ExceptionHandlingService { get; }

		public event Action<IPlatformService, Exception> InternalErrorOccurred
		{
			[CompilerGenerated]
			add
			{
				Action<IPlatformService, Exception> action = this.m_InternalErrorOccurred;
				Action<IPlatformService, Exception> action2 = default(Action<IPlatformService, Exception>);
				Action<IPlatformService, Exception> value2 = default(Action<IPlatformService, Exception>);
				while (true)
				{
					int num = -1702661266;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -2006384433)) % 6)
						{
						case 5u:
							break;
						default:
							return;
						case 4u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -1143947573;
								num4 = num3;
							}
							else
							{
								num3 = -1039429290;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1497746414);
							continue;
						}
						case 0u:
							value2 = (Action<IPlatformService, Exception>)Delegate.Combine(action2, value);
							num = (int)((num2 * 1693896997) ^ 0x13E31424);
							continue;
						case 3u:
							action2 = action;
							num = -1332208365;
							continue;
						case 1u:
							action = Interlocked.CompareExchange(ref this.m_InternalErrorOccurred, value2, action2);
							num = ((int)num2 * -986981611) ^ -500819912;
							continue;
						case 2u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<IPlatformService, Exception> action = this.m_InternalErrorOccurred;
				Action<IPlatformService, Exception> action2 = default(Action<IPlatformService, Exception>);
				Action<IPlatformService, Exception> value2 = default(Action<IPlatformService, Exception>);
				while (true)
				{
					int num = 160277032;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x5AF068FF)) % 6)
						{
						case 0u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							num = 128801283;
							continue;
						case 2u:
							value2 = (Action<IPlatformService, Exception>)Delegate.Remove(action2, value);
							num = ((int)num2 * -1107149885) ^ 0x5C22B52;
							continue;
						case 3u:
							action = Interlocked.CompareExchange(ref this.m_InternalErrorOccurred, value2, action2);
							num = (int)((num2 * 20235282) ^ 0x47D552C6);
							continue;
						case 5u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 580778544;
								num4 = num3;
							}
							else
							{
								num3 = 1830472653;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 398985416);
							continue;
						}
						case 4u:
							return;
						}
						break;
					}
				}
			}
		}

		public abstract IPermissionRuleManager<TGameFeature> GetPermissionRulesManager<TGameFeature>();

		internal DisposablePlatformServices(ILocalAccountManager localAccountManager = null, ISaveDataManager saveDataManager = null, IDlcManager dlcManager = null, IFriendManager friendManager = null, IMultiplayerSessionManager multiplayerSessionManager = null, IGameInvitationManager gameInvitationManager = null, IAchievementManager achievementManager = null, ISystemLanguageProvider languageProvider = null, IProfanityFilter profanityFilter = null, IPopupDialog popupDialog = null, IVirtualKeyboard virtualKeyboard = null, ExceptionHandlingService exceptionHandlingService = null)
		{
			LocalAccountManager = localAccountManager;
			AddService(localAccountManager);
			SaveDataManager = saveDataManager;
			AddService(saveDataManager);
			DlcManager = dlcManager;
			AddService(dlcManager);
			FriendManager = friendManager;
			AddService(friendManager);
			MultiplayerSessionManager = multiplayerSessionManager;
			AddService(multiplayerSessionManager);
			GameInvitationManager = gameInvitationManager;
			AddService(gameInvitationManager);
			AchievementManager = achievementManager;
			AddService(achievementManager);
			LanguageProvider = languageProvider;
			AddService(LanguageProvider);
			ProfanityFilter = profanityFilter;
			AddService(ProfanityFilter);
			PopupDialog = popupDialog;
			AddService(PopupDialog);
			VirtualKeyboard = virtualKeyboard;
			AddService(VirtualKeyboard);
			ExceptionHandlingService = exceptionHandlingService;
			AddService(ExceptionHandlingService);
		}

		protected void AddService(IPlatformService service)
		{
			if (service == null)
			{
				return;
			}
			while (true)
			{
				int num = -1717616857;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -364539717)) % 5)
					{
					case 3u:
						break;
					case 4u:
					{
						int num3;
						int num4;
						if (!services.Contains(service))
						{
							num3 = 350323929;
							num4 = num3;
						}
						else
						{
							num3 = 1702304085;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -1560041643);
						continue;
					}
					case 0u:
						services.Add(service);
						num = -177325385;
						continue;
					case 2u:
						return;
					default:
						service.InternalErrorOccurred += OnServiceInternalErrorOccurred;
						return;
					}
					break;
				}
			}
		}

		private void OnServiceInternalErrorOccurred(IPlatformService service, Exception obj)
		{
			this.InternalErrorOccurred?.Invoke(service, obj);
		}

		public virtual void Dispose()
		{
			if (disposed)
			{
				goto IL_000b;
			}
			goto IL_00b9;
			IL_000b:
			int num = 1006802502;
			goto IL_0010;
			IL_0010:
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x70412378)) % 8)
				{
				case 2u:
					break;
				case 4u:
					goto IL_0044;
				case 3u:
					services.Clear();
					num = ((int)num2 * -2144750106) ^ -1788709025;
					continue;
				case 0u:
					services[num3].InternalErrorOccurred -= OnServiceInternalErrorOccurred;
					services[num3].TryDispose();
					num3++;
					num = 337922596;
					continue;
				case 7u:
					goto IL_00b9;
				case 6u:
					return;
				case 1u:
					num = ((int)num2 * -1199326908) ^ -263564832;
					continue;
				default:
					disposed = true;
					return;
				}
				break;
				IL_0044:
				int num4;
				if (num3 >= services.Count)
				{
					num = 1274381771;
					num4 = num;
				}
				else
				{
					num = 531555648;
					num4 = num;
				}
			}
			goto IL_000b;
			IL_00b9:
			num3 = 0;
			num = 1768089049;
			goto IL_0010;
		}
	}
}
