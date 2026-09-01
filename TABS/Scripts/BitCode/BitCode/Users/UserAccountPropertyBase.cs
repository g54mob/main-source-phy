using System;
using System.Runtime.CompilerServices;
using System.Threading;
using BitCode.Extensions;

namespace BitCode.Users
{
	public abstract class UserAccountPropertyBase<T> : IUserAccountProperty<T>, IUserAccountPropertyInternal<T>
	{
		private readonly IUserAccount wUefnDBicTsKktekVvfIvITebIaGb;

		private T wfiGxoNcvnyzQlfLAGwHJXFacDti;

		private bool GHUXxmTsaKSRogbFHUQErFtjODcy;

		[CompilerGenerated]
		private Action<IUserAccount> OIVbNPhmlYwnMjPzmFeAsAOhYRZi;

		[CompilerGenerated]
		private Action DehUqAoamnugQDSMOEMtMiAwcLaY;

		[CompilerGenerated]
		private Action OFNYyLtcjMfNcjSznCNPEhjjYUWb;

		[CompilerGenerated]
		private readonly string lYFHqpdsRBJgNTBlQappPiucOEoi;

		[CompilerGenerated]
		private UserAccountPropertyStatus SQbAKiQpoklFNPYcfNCwefwrQDWy;

		[CompilerGenerated]
		private Exception SIGARpdRzORBaJmoGTSXHOtvqWIkA;

		public string Name
		{
			[CompilerGenerated]
			get
			{
				return lYFHqpdsRBJgNTBlQappPiucOEoi;
			}
		}

		public T Value
		{
			get
			{
				if (Status == UserAccountPropertyStatus.Loaded)
				{
					while (true)
					{
						uint num;
						switch ((num = 1611005371u) % 3)
						{
						case 2u:
							continue;
						case 1u:
							return wfiGxoNcvnyzQlfLAGwHJXFacDti;
						}
						break;
					}
				}
				throw new InvalidOperationException("Trying to access an UserAccountProperty that isn't initialized[" + Name + "].");
			}
		}

		public UserAccountPropertyStatus Status
		{
			[CompilerGenerated]
			get
			{
				return SQbAKiQpoklFNPYcfNCwefwrQDWy;
			}
			[CompilerGenerated]
			set
			{
				SQbAKiQpoklFNPYcfNCwefwrQDWy = value;
			}
		}

		public bool Tracked => GHUXxmTsaKSRogbFHUQErFtjODcy;

		public Exception LastException
		{
			[CompilerGenerated]
			get
			{
				return SIGARpdRzORBaJmoGTSXHOtvqWIkA;
			}
			[CompilerGenerated]
			private set
			{
				SIGARpdRzORBaJmoGTSXHOtvqWIkA = sIGARpdRzORBaJmoGTSXHOtvqWIkA;
			}
		}

		public event Action<IUserAccount> ValueChanged
		{
			[CompilerGenerated]
			add
			{
				Action<IUserAccount> action = OIVbNPhmlYwnMjPzmFeAsAOhYRZi;
				Action<IUserAccount> action2 = default(Action<IUserAccount>);
				Action<IUserAccount> value2 = default(Action<IUserAccount>);
				while (true)
				{
					int num = -1025069178;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1020792273)) % 5)
						{
						case 4u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							num = -1135020701;
							continue;
						case 2u:
							value2 = (Action<IUserAccount>)Delegate.Combine(action2, value);
							num = ((int)num2 * -363344620) ^ 0x29EC6B31;
							continue;
						case 0u:
						{
							action = Interlocked.CompareExchange(ref OIVbNPhmlYwnMjPzmFeAsAOhYRZi, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 2042871361;
								num4 = num3;
							}
							else
							{
								num3 = 873842376;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1849527471);
							continue;
						}
						case 3u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<IUserAccount> action = OIVbNPhmlYwnMjPzmFeAsAOhYRZi;
				Action<IUserAccount> action2 = default(Action<IUserAccount>);
				while (true)
				{
					int num = 398711163;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x3CD5CA54)) % 5)
						{
						case 0u:
							break;
						default:
							return;
						case 1u:
							action2 = action;
							num = 1797093880;
							continue;
						case 2u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 1398911131;
								num4 = num3;
							}
							else
							{
								num3 = 250649727;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1797296884);
							continue;
						}
						case 3u:
						{
							Action<IUserAccount> value2 = (Action<IUserAccount>)Delegate.Remove(action2, value);
							action = Interlocked.CompareExchange(ref OIVbNPhmlYwnMjPzmFeAsAOhYRZi, value2, action2);
							num = (int)(num2 * 892583250) ^ -2063926169;
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

		public event Action TrackingStarted
		{
			[CompilerGenerated]
			add
			{
				Action action = DehUqAoamnugQDSMOEMtMiAwcLaY;
				Action action2 = default(Action);
				Action value2 = default(Action);
				while (true)
				{
					int num = 136945097;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x7E40769D)) % 5)
						{
						case 3u:
							break;
						default:
							return;
						case 2u:
							action2 = action;
							num = 1459145062;
							continue;
						case 1u:
							value2 = (Action)Delegate.Combine(action2, value);
							num = (int)(num2 * 233206658) ^ -572321312;
							continue;
						case 4u:
						{
							action = Interlocked.CompareExchange(ref DehUqAoamnugQDSMOEMtMiAwcLaY, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -1772880015;
								num4 = num3;
							}
							else
							{
								num3 = -367583091;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1633790540);
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
				Action action = DehUqAoamnugQDSMOEMtMiAwcLaY;
				Action action2 = default(Action);
				Action value2 = default(Action);
				while (true)
				{
					int num = 1116357436;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x404E8E9E)) % 6)
						{
						case 0u:
							break;
						default:
							return;
						case 4u:
							action2 = action;
							num = 1880115914;
							continue;
						case 1u:
							action = Interlocked.CompareExchange(ref DehUqAoamnugQDSMOEMtMiAwcLaY, value2, action2);
							num = ((int)num2 * -1184735228) ^ 0x2064C2A5;
							continue;
						case 2u:
							value2 = (Action)Delegate.Remove(action2, value);
							num = (int)((num2 * 752989398) ^ 0x11CE8193);
							continue;
						case 5u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -1015162002;
								num4 = num3;
							}
							else
							{
								num3 = -1982387985;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -2057813155);
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

		public event Action TrackingStopped
		{
			[CompilerGenerated]
			add
			{
				Action action = OFNYyLtcjMfNcjSznCNPEhjjYUWb;
				Action action2 = default(Action);
				Action value2 = default(Action);
				while (true)
				{
					int num = -1288917543;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -573548384)) % 5)
						{
						case 0u:
							break;
						default:
							return;
						case 3u:
							action2 = action;
							num = -988666224;
							continue;
						case 2u:
							value2 = (Action)Delegate.Combine(action2, value);
							num = ((int)num2 * -552177522) ^ -211705458;
							continue;
						case 4u:
						{
							action = Interlocked.CompareExchange(ref OFNYyLtcjMfNcjSznCNPEhjjYUWb, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 2068275189;
								num4 = num3;
							}
							else
							{
								num3 = 1574865405;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 690870622);
							continue;
						}
						case 1u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action action = OFNYyLtcjMfNcjSznCNPEhjjYUWb;
				Action value2 = default(Action);
				Action action2 = default(Action);
				while (true)
				{
					int num = 1368778127;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x5981DCCD)) % 6)
						{
						case 0u:
							break;
						default:
							return;
						case 2u:
							action = Interlocked.CompareExchange(ref OFNYyLtcjMfNcjSznCNPEhjjYUWb, value2, action2);
							num = ((int)num2 * -1525733081) ^ -886221182;
							continue;
						case 3u:
						{
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = 955225821;
								num4 = num3;
							}
							else
							{
								num3 = 1929171722;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 106334563);
							continue;
						}
						case 4u:
							action2 = action;
							num = 1092932742;
							continue;
						case 5u:
							value2 = (Action)Delegate.Remove(action2, value);
							num = (int)(num2 * 1514555075) ^ -714132124;
							continue;
						case 1u:
							return;
						}
						break;
					}
				}
			}
		}

		internal UserAccountPropertyBase(string P_0, IUserAccount P_1, Action P_2 = null, Action P_3 = null)
		{
			while (true)
			{
				int num = -146476146;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -960902057)) % 5)
					{
					case 3u:
						break;
					default:
						return;
					case 2u:
						lYFHqpdsRBJgNTBlQappPiucOEoi = P_0;
						wUefnDBicTsKktekVvfIvITebIaGb = P_1;
						TrackingStarted += P_2;
						num = ((int)num2 * -1392249692) ^ 0x35EFAAA3;
						continue;
					case 4u:
						GHUXxmTsaKSRogbFHUQErFtjODcy = false;
						num = (int)(num2 * 1831339071) ^ -963996971;
						continue;
					case 1u:
						TrackingStopped += P_3;
						Status = UserAccountPropertyStatus.NotLoaded;
						num = (int)((num2 * 1089806478) ^ 0x7E9D3C37);
						continue;
					case 0u:
						return;
					}
					break;
				}
			}
		}

		public void SetValue(T val)
		{
			T val2 = wfiGxoNcvnyzQlfLAGwHJXFacDti;
			wfiGxoNcvnyzQlfLAGwHJXFacDti = val;
			Status = UserAccountPropertyStatus.Loaded;
			while (true)
			{
				int num = 1927019148;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x50C793DE)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
					{
						int num3;
						int num4;
						if (wfiGxoNcvnyzQlfLAGwHJXFacDti.Equals(val2))
						{
							num3 = -757628039;
							num4 = num3;
						}
						else
						{
							num3 = -637649661;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 910258412);
						continue;
					}
					case 1u:
					{
						Action<IUserAccount> action = OIVbNPhmlYwnMjPzmFeAsAOhYRZi;
						if (action == null)
						{
							return;
						}
						action.SafelyInvoke(wUefnDBicTsKktekVvfIvITebIaGb);
						num = (int)(num2 * 682970258) ^ -1114959429;
						continue;
					}
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void SetTracked(bool track)
		{
			bool gHUXxmTsaKSRogbFHUQErFtjODcy = GHUXxmTsaKSRogbFHUQErFtjODcy;
			while (true)
			{
				int num = 1863639965;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5C03DA2)) % 9)
					{
					case 5u:
						break;
					default:
						return;
					case 7u:
						Status = UserAccountPropertyStatus.NotLoaded;
						num = 1283661044;
						continue;
					case 0u:
					{
						int num5;
						int num6;
						if (GHUXxmTsaKSRogbFHUQErFtjODcy != gHUXxmTsaKSRogbFHUQErFtjODcy)
						{
							num5 = 1186251705;
							num6 = num5;
						}
						else
						{
							num5 = 1294192078;
							num6 = num5;
						}
						num = num5 ^ (int)(num2 * 1079514825);
						continue;
					}
					case 6u:
					{
						Action action = DehUqAoamnugQDSMOEMtMiAwcLaY;
						if (action == null)
						{
							return;
						}
						action.SafelyInvoke();
						num = (int)(num2 * 1945463783) ^ -16027696;
						continue;
					}
					case 2u:
						return;
					case 4u:
					{
						Action action2 = OFNYyLtcjMfNcjSznCNPEhjjYUWb;
						if (action2 == null)
						{
							return;
						}
						action2.SafelyInvoke();
						num = ((int)num2 * -855518009) ^ -1979623884;
						continue;
					}
					case 3u:
						GHUXxmTsaKSRogbFHUQErFtjODcy = track;
						num = ((int)num2 * -1802196850) ^ -1897072016;
						continue;
					case 1u:
					{
						int num3;
						int num4;
						if (!GHUXxmTsaKSRogbFHUQErFtjODcy)
						{
							num3 = -1861212185;
							num4 = num3;
						}
						else
						{
							num3 = -940992701;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 2013325451);
						continue;
					}
					case 8u:
						return;
					}
					break;
				}
			}
		}

		public void SetError(Exception e)
		{
			Status = UserAccountPropertyStatus.ErrorLoading;
			while (true)
			{
				int num = 1971235200;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x21E6C4D5)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0029;
					case 0u:
						return;
					}
					break;
					IL_0029:
					LastException = e;
					num = ((int)num2 * -533932600) ^ 0x122B37D7;
				}
			}
		}
	}
}
