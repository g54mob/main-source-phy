using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using BitCode.Users;
using JetBrains.Annotations;

namespace BitCode.Networking
{
	public class ReceivedGameInvitationBuffer : IDisposable
	{
		private struct vxmdaHMrihBVzrdMZpedLvizHdzu
		{
			public IGameInvitation mRcxsvjQoyQPrcNuXfIgZHwuQQOT;

			public ILocalAccount nTOqKeJIqwuUwWSBqEHxjjPuWjZE;
		}

		[CompilerGenerated]
		private Action<IGameInvitation, ILocalAccount> zCZXLRABgzBlRZahaDbTClFarOnib;

		private bool ZXEEXdzxiSfgnJSZyVDGOjLAdlLab;

		private readonly IGameInvitationManager dwhPLYLAdtTLZWDPnSttKVPgZTnA;

		private List<vxmdaHMrihBVzrdMZpedLvizHdzu> BcaQnxyYTjNCqYEynDTLZEDKdsnU = new List<vxmdaHMrihBVzrdMZpedLvizHdzu>();

		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		public bool BufferInvites => ZXEEXdzxiSfgnJSZyVDGOjLAdlLab;

		public event Action<IGameInvitation, ILocalAccount> InvitationReceived
		{
			[CompilerGenerated]
			add
			{
				Action<IGameInvitation, ILocalAccount> action = zCZXLRABgzBlRZahaDbTClFarOnib;
				Action<IGameInvitation, ILocalAccount> action2 = default(Action<IGameInvitation, ILocalAccount>);
				Action<IGameInvitation, ILocalAccount> value2 = default(Action<IGameInvitation, ILocalAccount>);
				while (true)
				{
					int num = 1843332722;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x680BDD73)) % 5)
						{
						case 0u:
							break;
						default:
							return;
						case 1u:
						{
							int num3;
							int num4;
							if ((object)action != action2)
							{
								num3 = 1105023946;
								num4 = num3;
							}
							else
							{
								num3 = 318365023;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 477550084);
							continue;
						}
						case 3u:
							action = Interlocked.CompareExchange(ref zCZXLRABgzBlRZahaDbTClFarOnib, value2, action2);
							num = (int)((num2 * 670670705) ^ 0x13AA9A65);
							continue;
						case 2u:
							action2 = action;
							value2 = (Action<IGameInvitation, ILocalAccount>)Delegate.Combine(action2, value);
							num = 1041684363;
							continue;
						case 4u:
							return;
						}
						break;
					}
				}
			}
			[CompilerGenerated]
			remove
			{
				Action<IGameInvitation, ILocalAccount> action = zCZXLRABgzBlRZahaDbTClFarOnib;
				Action<IGameInvitation, ILocalAccount> action2 = default(Action<IGameInvitation, ILocalAccount>);
				Action<IGameInvitation, ILocalAccount> value2 = default(Action<IGameInvitation, ILocalAccount>);
				while (true)
				{
					int num = -661363466;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -2079835245)) % 5)
						{
						case 3u:
							break;
						default:
							return;
						case 4u:
							action2 = action;
							num = -767366564;
							continue;
						case 1u:
							value2 = (Action<IGameInvitation, ILocalAccount>)Delegate.Remove(action2, value);
							num = ((int)num2 * -2074122433) ^ 0x4ED83239;
							continue;
						case 0u:
						{
							action = Interlocked.CompareExchange(ref zCZXLRABgzBlRZahaDbTClFarOnib, value2, action2);
							int num3;
							int num4;
							if ((object)action == action2)
							{
								num3 = -1608363065;
								num4 = num3;
							}
							else
							{
								num3 = -378482460;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -1234513194);
							continue;
						}
						case 2u:
							return;
						}
						break;
					}
				}
			}
		}

		public ReceivedGameInvitationBuffer([NotNull] IGameInvitationManager inviteManager, bool bufferInvites = true)
		{
			while (true)
			{
				int num = 1139287708;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x335650B9)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						if (inviteManager != null)
						{
							goto IL_0044;
						}
						throw new ArgumentNullException("inviteManager");
					case 2u:
						return;
					}
					break;
					IL_0044:
					dwhPLYLAdtTLZWDPnSttKVPgZTnA = inviteManager;
					ZXEEXdzxiSfgnJSZyVDGOjLAdlLab = bufferInvites;
					inviteManager.InvitationReceived += YAoZyhSEzuppKhehMCPELQXKeXCO;
					num = (int)((num2 * 1645705593) ^ 0x3268BDCD);
				}
			}
		}

		public void SetBufferInvites(bool buffer)
		{
			UrWwwkEVqlsCwuqAxyNaOnyUzodO();
			bool zXEEXdzxiSfgnJSZyVDGOjLAdlLab = default(bool);
			while (true)
			{
				int num = -402558464;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1331606573)) % 5)
					{
					case 3u:
						break;
					default:
						return;
					case 1u:
						zXEEXdzxiSfgnJSZyVDGOjLAdlLab = ZXEEXdzxiSfgnJSZyVDGOjLAdlLab;
						ZXEEXdzxiSfgnJSZyVDGOjLAdlLab = buffer;
						num = ((int)num2 * -1902744963) ^ 0x6BEB5155;
						continue;
					case 4u:
						OVtNQxNzsiHyWAyeLMCGDXkcpmdv();
						num = (int)(num2 * 680865287) ^ -1230933566;
						continue;
					case 2u:
					{
						int num3;
						int num4;
						if (!ZXEEXdzxiSfgnJSZyVDGOjLAdlLab && zXEEXdzxiSfgnJSZyVDGOjLAdlLab)
						{
							num3 = -1029120992;
							num4 = num3;
						}
						else
						{
							num3 = -232365951;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 1588717931);
						continue;
					}
					case 0u:
						return;
					}
					break;
				}
			}
		}

		private void YAoZyhSEzuppKhehMCPELQXKeXCO(IGameInvitation P_0, ILocalAccount P_1)
		{
			if (BufferInvites)
			{
				goto IL_0008;
			}
			goto IL_0078;
			IL_0008:
			int num = 725758274;
			goto IL_000d;
			IL_000d:
			vxmdaHMrihBVzrdMZpedLvizHdzu item = default(vxmdaHMrihBVzrdMZpedLvizHdzu);
			vxmdaHMrihBVzrdMZpedLvizHdzu vxmdaHMrihBVzrdMZpedLvizHdzu2 = default(vxmdaHMrihBVzrdMZpedLvizHdzu);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x4D320806)) % 9)
				{
				case 6u:
					break;
				default:
					return;
				case 1u:
					BcaQnxyYTjNCqYEynDTLZEDKdsnU.Add(item);
					num = (int)((num2 * 1727647787) ^ 0x1ADA6056);
					continue;
				case 8u:
					vxmdaHMrihBVzrdMZpedLvizHdzu2.nTOqKeJIqwuUwWSBqEHxjjPuWjZE = P_1;
					num = ((int)num2 * -589825698) ^ -1954294686;
					continue;
				case 0u:
					goto IL_0078;
				case 2u:
					return;
				case 4u:
					item = vxmdaHMrihBVzrdMZpedLvizHdzu2;
					num = ((int)num2 * -907234111) ^ -1001739222;
					continue;
				case 3u:
					vxmdaHMrihBVzrdMZpedLvizHdzu2 = default(vxmdaHMrihBVzrdMZpedLvizHdzu);
					num = (int)(num2 * 865827128) ^ -505744086;
					continue;
				case 7u:
					vxmdaHMrihBVzrdMZpedLvizHdzu2.mRcxsvjQoyQPrcNuXfIgZHwuQQOT = P_0;
					num = (int)(num2 * 1423162862) ^ -1394158979;
					continue;
				case 5u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0078:
			Action<IGameInvitation, ILocalAccount> action = zCZXLRABgzBlRZahaDbTClFarOnib;
			if (action == null)
			{
				return;
			}
			action.SafelyInvoke(P_0, P_1);
			num = 1782423356;
			goto IL_000d;
		}

		private void OVtNQxNzsiHyWAyeLMCGDXkcpmdv()
		{
			int num = 0;
			while (true)
			{
				int num2 = 642463115;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ 0x2F03538C)) % 7)
					{
					case 6u:
						break;
					default:
						return;
					case 1u:
						num2 = (int)(num3 * 784329744) ^ -396107921;
						continue;
					case 5u:
						BcaQnxyYTjNCqYEynDTLZEDKdsnU.Clear();
						num2 = (int)((num3 * 1563147303) ^ 0x9C2CB87);
						continue;
					case 3u:
					{
						vxmdaHMrihBVzrdMZpedLvizHdzu vxmdaHMrihBVzrdMZpedLvizHdzu2 = BcaQnxyYTjNCqYEynDTLZEDKdsnU[num];
						Action<IGameInvitation, ILocalAccount> action = zCZXLRABgzBlRZahaDbTClFarOnib;
						if (action == null)
						{
							goto case 4u;
						}
						action.SafelyInvoke(vxmdaHMrihBVzrdMZpedLvizHdzu2.mRcxsvjQoyQPrcNuXfIgZHwuQQOT, vxmdaHMrihBVzrdMZpedLvizHdzu2.nTOqKeJIqwuUwWSBqEHxjjPuWjZE);
						num2 = 190055890;
						continue;
					}
					case 2u:
					{
						int num4;
						if (num >= BcaQnxyYTjNCqYEynDTLZEDKdsnU.Count)
						{
							num2 = 1460809123;
							num4 = num2;
						}
						else
						{
							num2 = 645562629;
							num4 = num2;
						}
						continue;
					}
					case 4u:
						num++;
						num2 = 1538132767;
						continue;
					case 0u:
						return;
					}
					break;
				}
			}
		}

		public void Dispose()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				goto IL_0008;
			}
			goto IL_0068;
			IL_0008:
			int num = 1777675208;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x2C0E6748)) % 5)
				{
				case 0u:
					break;
				case 4u:
					return;
				case 2u:
					dwhPLYLAdtTLZWDPnSttKVPgZTnA.InvitationReceived -= YAoZyhSEzuppKhehMCPELQXKeXCO;
					num = (int)(num2 * 744214736) ^ -274918720;
					continue;
				case 1u:
					goto IL_0068;
				default:
					tlRiOzSchvnYldbxynOFisSYraBV = true;
					GC.SuppressFinalize(this);
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0068:
			int num3;
			if (dwhPLYLAdtTLZWDPnSttKVPgZTnA != null)
			{
				num = 1098839813;
				num3 = num;
			}
			else
			{
				num = 126868560;
				num3 = num;
			}
			goto IL_000d;
		}

		private void UrWwwkEVqlsCwuqAxyNaOnyUzodO()
		{
			if (!tlRiOzSchvnYldbxynOFisSYraBV)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1571500811u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ObjectDisposedException(GetType().FullName);
				case 1u:
					return;
				}
			}
		}
	}
}
