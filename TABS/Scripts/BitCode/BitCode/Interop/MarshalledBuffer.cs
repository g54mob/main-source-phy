using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace BitCode.Interop
{
	public sealed class MarshalledBuffer : IDisposable
	{
		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		private readonly Action<IntPtr> MFvgVgHLupgmivzMDhqVQOYFnHBr;

		private bool zYmZTcKqHzekxyfRMpatewFQhlQk;

		[CompilerGenerated]
		private IntPtr MCBChBJOSaaCmyfQfhjwtxOcDuyAA;

		[CompilerGenerated]
		private int iRZqpkNoQRdEmkBEHnwKewIiALKqc;

		public IntPtr UnmanagedBuffer
		{
			[CompilerGenerated]
			get
			{
				return MCBChBJOSaaCmyfQfhjwtxOcDuyAA;
			}
			[CompilerGenerated]
			private set
			{
				MCBChBJOSaaCmyfQfhjwtxOcDuyAA = mCBChBJOSaaCmyfQfhjwtxOcDuyAA;
			}
		}

		public int BufferLength
		{
			[CompilerGenerated]
			get
			{
				return iRZqpkNoQRdEmkBEHnwKewIiALKqc;
			}
			[CompilerGenerated]
			private set
			{
				iRZqpkNoQRdEmkBEHnwKewIiALKqc = num;
			}
		}

		public MarshalledBuffer(IntPtr unmanagedBuffer, int bufferLength, [NotNull] Action<IntPtr> freeUnmanagedBuffer)
		{
			while (true)
			{
				int num = -87363000;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1526315143)) % 4)
					{
					case 2u:
						break;
					case 1u:
						dJoGqTjFxQiUJXzVPraOoLIZojHA(bufferLength, "bufferLength");
						num = ((int)num2 * -755911642) ^ 0xBDF94DF;
						continue;
					case 0u:
						MFvgVgHLupgmivzMDhqVQOYFnHBr = freeUnmanagedBuffer ?? throw new ArgumentNullException("freeUnmanagedBuffer");
						BufferLength = bufferLength;
						num = ((int)num2 * -971891321) ^ 0x37AAA92;
						continue;
					default:
						UnmanagedBuffer = unmanagedBuffer;
						return;
					}
					break;
				}
			}
		}

		public MarshalledBuffer(int bufferLength)
		{
			while (true)
			{
				int num = 1487070759;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x25D7CE30)) % 6)
					{
					case 2u:
						break;
					default:
						return;
					case 5u:
						dJoGqTjFxQiUJXzVPraOoLIZojHA(bufferLength, "bufferLength");
						num = ((int)num2 * -1688383921) ^ -344029579;
						continue;
					case 0u:
						BufferLength = bufferLength;
						num = ((int)num2 * -856247806) ^ -1658180759;
						continue;
					case 3u:
					{
						int num3;
						int num4;
						if (bufferLength > 0)
						{
							num3 = 978049785;
							num4 = num3;
						}
						else
						{
							num3 = 173600414;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -617851958);
						continue;
					}
					case 1u:
						CsfBxAhkaNReJSVDjVJPJHmwpHkX(BufferLength);
						num = (int)(num2 * 609343890) ^ -2041349854;
						continue;
					case 4u:
						return;
					}
					break;
				}
			}
		}

		public void CopyTo(ref byte[] buffer, out int numCopiedBytes)
		{
			CopyTo(ref buffer, 0, out numCopiedBytes);
		}

		public void CopyTo(ref byte[] buffer, int offset, out int numCopiedBytes)
		{
			uKkqroDPGVuBLDXDuYOHnazLJuOK();
			LQHmgVvpDxFlYfQIBmsIwAtdkRJF(buffer, offset, "offset");
			while (true)
			{
				int num = 1941656596;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x7BD6B5A4)) % 9)
					{
					case 3u:
						break;
					default:
						return;
					case 5u:
						Marshal.Copy(UnmanagedBuffer, buffer, offset, numCopiedBytes);
						num = ((int)num2 * -520894026) ^ -23902025;
						continue;
					case 0u:
					{
						int num5;
						if (buffer != null)
						{
							num = 1376226028;
							num5 = num;
						}
						else
						{
							num = 37934059;
							num5 = num;
						}
						continue;
					}
					case 7u:
					{
						int num6;
						int num7;
						if (BufferLength == 0)
						{
							num6 = -1587548855;
							num7 = num6;
						}
						else
						{
							num6 = -1688244641;
							num7 = num6;
						}
						num = num6 ^ (int)(num2 * 431095030);
						continue;
					}
					case 6u:
						buffer = new byte[BufferLength + offset];
						num = ((int)num2 * -1892038755) ^ -2044285537;
						continue;
					case 8u:
						numCopiedBytes = 0;
						return;
					case 1u:
						numCopiedBytes = Math.Min(buffer.Length - offset, BufferLength);
						num = 890763396;
						continue;
					case 2u:
					{
						int num3;
						int num4;
						if (!(UnmanagedBuffer == IntPtr.Zero))
						{
							num3 = 1279854628;
							num4 = num3;
						}
						else
						{
							num3 = 1418370857;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -169605986);
						continue;
					}
					case 4u:
						return;
					}
					break;
				}
			}
		}

		public void CopyFrom(byte[] buffer, out int numCopiedBytes)
		{
			CopyFrom(buffer, 0, out numCopiedBytes);
		}

		public void CopyFrom(byte[] buffer, int offset, out int numCopiedBytes)
		{
			uKkqroDPGVuBLDXDuYOHnazLJuOK();
			int num3 = default(int);
			while (true)
			{
				int num = 492816842;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x320B4C8C)) % 12)
					{
					case 8u:
						break;
					default:
						return;
					case 4u:
					{
						int num5;
						int num6;
						if (BufferLength != 0)
						{
							num5 = 2109013813;
							num6 = num5;
						}
						else
						{
							num5 = 835371334;
							num6 = num5;
						}
						num = num5 ^ ((int)num2 * -959295452);
						continue;
					}
					case 0u:
					{
						int num9;
						int num10;
						if (buffer == null)
						{
							num9 = -296696777;
							num10 = num9;
						}
						else
						{
							num9 = -1919222242;
							num10 = num9;
						}
						num = num9 ^ (int)(num2 * 769250502);
						continue;
					}
					case 7u:
						numCopiedBytes = 0;
						num = 1124335433;
						continue;
					case 6u:
					{
						int num7;
						int num8;
						if (buffer.Length != 0)
						{
							num7 = -1302694483;
							num8 = num7;
						}
						else
						{
							num7 = -1188265261;
							num8 = num7;
						}
						num = num7 ^ (int)(num2 * 1417744734);
						continue;
					}
					case 10u:
						LQHmgVvpDxFlYfQIBmsIwAtdkRJF(buffer, offset, "offset");
						num = (int)((num2 * 80594752) ^ 0x5A815E10);
						continue;
					case 2u:
						CsfBxAhkaNReJSVDjVJPJHmwpHkX(num3);
						num = 541220357;
						continue;
					case 1u:
					{
						num3 = buffer.Length - offset;
						int num4;
						if (UnmanagedBuffer == IntPtr.Zero)
						{
							num = 1815896182;
							num4 = num;
						}
						else
						{
							num = 151845984;
							num4 = num;
						}
						continue;
					}
					case 5u:
						return;
					case 9u:
						numCopiedBytes = Math.Min(num3, BufferLength);
						num = 1584986711;
						continue;
					case 3u:
						Marshal.Copy(buffer, offset, UnmanagedBuffer, numCopiedBytes);
						num = ((int)num2 * -432154838) ^ 0x2A9DCF2D;
						continue;
					case 11u:
						return;
					}
					break;
				}
			}
		}

		private void CsfBxAhkaNReJSVDjVJPJHmwpHkX(int P_0)
		{
			PWMELsONuRdFSNIXZGNRMATjgTan();
			while (true)
			{
				int num = 1092281428;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x4D169E6E)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						BufferLength = P_0;
						num = ((int)num2 * -1948496950) ^ -886420931;
						continue;
					case 3u:
						UnmanagedBuffer = Marshal.AllocHGlobal(P_0);
						zYmZTcKqHzekxyfRMpatewFQhlQk = true;
						num = (int)((num2 * 130603589) ^ 0xB8B8420);
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
		}

		private void PWMELsONuRdFSNIXZGNRMATjgTan()
		{
			if (UnmanagedBuffer == IntPtr.Zero)
			{
				goto IL_0012;
			}
			goto IL_0071;
			IL_0012:
			int num = 23017818;
			goto IL_0017;
			IL_0017:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x2700B3A)) % 6)
				{
				case 0u:
					break;
				case 4u:
					return;
				case 5u:
					Marshal.FreeHGlobal(UnmanagedBuffer);
					zYmZTcKqHzekxyfRMpatewFQhlQk = false;
					num = (int)(num2 * 1041715385) ^ -2144361130;
					continue;
				case 1u:
					goto IL_0071;
				case 2u:
					MFvgVgHLupgmivzMDhqVQOYFnHBr(UnmanagedBuffer);
					num = 541055747;
					continue;
				default:
					UnmanagedBuffer = IntPtr.Zero;
					return;
				}
				break;
			}
			goto IL_0012;
			IL_0071:
			int num3;
			if (zYmZTcKqHzekxyfRMpatewFQhlQk)
			{
				num = 154976071;
				num3 = num;
			}
			else
			{
				num = 631302312;
				num3 = num;
			}
			goto IL_0017;
		}

		private static void dJoGqTjFxQiUJXzVPraOoLIZojHA(int P_0, string P_1)
		{
			if (P_0 >= 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 672535595u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ArgumentException("Buffer length must be greater than or equal to zero.", P_1);
				case 1u:
					return;
				}
			}
		}

		private static void LQHmgVvpDxFlYfQIBmsIwAtdkRJF(byte[] P_0, int P_1, string P_2)
		{
			if (P_1 < 0)
			{
				goto IL_0004;
			}
			goto IL_0068;
			IL_0004:
			int num = -1459238927;
			goto IL_0009;
			IL_0009:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -619393904)) % 6)
				{
				case 4u:
					break;
				default:
					return;
				case 5u:
					throw new ArgumentException("Offset into array cannot be negative.", P_2);
				case 0u:
					throw new ArgumentException("Offset exceeds array length.", P_2);
				case 1u:
					goto IL_0068;
				case 3u:
				{
					int num3;
					int num4;
					if (P_1 < P_0.Length)
					{
						num3 = 992354962;
						num4 = num3;
					}
					else
					{
						num3 = 898856624;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -214260880);
					continue;
				}
				case 2u:
					return;
				}
				break;
			}
			goto IL_0004;
			IL_0068:
			int num5;
			if (P_0 != null)
			{
				num = -1845059899;
				num5 = num;
			}
			else
			{
				num = -1013709150;
				num5 = num;
			}
			goto IL_0009;
		}

		public void Dispose()
		{
			if (!tlRiOzSchvnYldbxynOFisSYraBV)
			{
				goto IL_0008;
			}
			goto IL_0043;
			IL_0008:
			int num = 728525250;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x2E40B34C)) % 4)
				{
				case 3u:
					break;
				default:
					return;
				case 2u:
					PWMELsONuRdFSNIXZGNRMATjgTan();
					num = ((int)num2 * -600402740) ^ 0x515E69D1;
					continue;
				case 1u:
					goto IL_0043;
				case 0u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0043:
			tlRiOzSchvnYldbxynOFisSYraBV = true;
			num = 1974185876;
			goto IL_000d;
		}

		private void uKkqroDPGVuBLDXDuYOHnazLJuOK()
		{
			if (!tlRiOzSchvnYldbxynOFisSYraBV)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1881399563u) % 3)
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
