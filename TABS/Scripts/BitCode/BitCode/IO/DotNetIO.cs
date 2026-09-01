using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BitCode.IO
{
	public class DotNetIO : IIOWrapper
	{
		[StructLayout(LayoutKind.Auto)]
		private struct BGrpLvRoNQymXPFZbSDsdmhVGhDU : IAsyncStateMachine
		{
			public int dagGWVjAonzlEQhnHtJbFGTLQUwi;

			public AsyncTaskMethodBuilder VVEGRPiIETvwaByBfDgZFHzJPzZZ;

			public DotNetIO vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public string jyNKWByJQfDzIMQqKDNyCQesbfuN;

			public byte[] rZKAWAthtGScTPcLspnVRctHaZCw;

			public int seNhChApuVtHuVyqbVnPjWxLiDSZ;

			public int jUwhDMdnUFfyHoPnyjvMdaluUSKBA;

			private FileStream lVUvzcxcObVCIMqWtSCEvkqTjrUV;

			private TaskAwaiter ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;

			private void MoveNext()
			{
				int num = dagGWVjAonzlEQhnHtJbFGTLQUwi;
				DotNetIO dotNetIO = vvKNDIxiYKTrRTKAccPPCIdmGSFtA;
				try
				{
					if (num != 0)
					{
						while (true)
						{
							int num2 = 1969302670;
							while (true)
							{
								uint num3;
								switch ((num3 = (uint)(num2 ^ 0x7CD11378)) % 4)
								{
								case 0u:
									break;
								case 2u:
									dotNetIO.TBDgzroaMrwyuzpKmuzHCZSOcaWo(jyNKWByJQfDzIMQqKDNyCQesbfuN);
									num2 = (int)((num3 * 898245374) ^ 0x78520A7);
									continue;
								case 3u:
									lVUvzcxcObVCIMqWtSCEvkqTjrUV = new FileStream(jyNKWByJQfDzIMQqKDNyCQesbfuN, FileMode.Create, FileAccess.ReadWrite);
									num2 = ((int)num3 * -728354105) ^ 0x37341178;
									continue;
								default:
									goto end_IL_0011;
								}
								break;
							}
							continue;
							end_IL_0011:
							break;
						}
					}
					try
					{
						if (num != 0)
						{
							goto IL_007e;
						}
						goto IL_0144;
						IL_007e:
						int num4 = 368281394;
						goto IL_0083;
						IL_0083:
						TaskAwaiter awaiter = default(TaskAwaiter);
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num4 ^ 0x7CD11378)) % 9)
							{
							case 3u:
								break;
							default:
								goto end_IL_0078;
							case 2u:
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = default(TaskAwaiter);
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = -1);
								num4 = (int)(num3 * 1797767166) ^ -70961818;
								continue;
							case 0u:
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = 0);
								num4 = ((int)num3 * -802759842) ^ 0x34A2D823;
								continue;
							case 1u:
							{
								awaiter = lVUvzcxcObVCIMqWtSCEvkqTjrUV.WriteAsync(rZKAWAthtGScTPcLspnVRctHaZCw, seNhChApuVtHuVyqbVnPjWxLiDSZ, jUwhDMdnUFfyHoPnyjvMdaluUSKBA).GetAwaiter();
								int num5;
								int num6;
								if (!awaiter.IsCompleted)
								{
									num5 = -1464910470;
									num6 = num5;
								}
								else
								{
									num5 = -154171672;
									num6 = num5;
								}
								num4 = num5 ^ ((int)num3 * -602723427);
								continue;
							}
							case 7u:
								goto IL_0144;
							case 4u:
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = awaiter;
								num4 = ((int)num3 * -1067006358) ^ -1253386389;
								continue;
							case 5u:
								VVEGRPiIETvwaByBfDgZFHzJPzZZ.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							case 6u:
								awaiter.GetResult();
								num4 = 1540894917;
								continue;
							case 8u:
								goto end_IL_0078;
							}
							break;
						}
						goto IL_007e;
						IL_0144:
						awaiter = ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;
						num4 = 404544242;
						goto IL_0083;
						end_IL_0078:;
					}
					finally
					{
						if (num < 0)
						{
							while (true)
							{
								IL_01ac:
								int num7 = 617277505;
								while (true)
								{
									uint num3;
									switch ((num3 = (uint)(num7 ^ 0x7CD11378)) % 4)
									{
									case 2u:
										break;
									default:
										goto end_IL_01b1;
									case 1u:
									{
										int num8;
										int num9;
										if (lVUvzcxcObVCIMqWtSCEvkqTjrUV == null)
										{
											num8 = 837542303;
											num9 = num8;
										}
										else
										{
											num8 = 595729604;
											num9 = num8;
										}
										num7 = num8 ^ (int)(num3 * 709924015);
										continue;
									}
									case 3u:
										((IDisposable)lVUvzcxcObVCIMqWtSCEvkqTjrUV).Dispose();
										num7 = (int)((num3 * 638062584) ^ 0x47A1E3C0);
										continue;
									case 0u:
										goto end_IL_01b1;
									}
									goto IL_01ac;
									continue;
									end_IL_01b1:
									break;
								}
								break;
							}
						}
					}
					lVUvzcxcObVCIMqWtSCEvkqTjrUV = null;
				}
				catch (Exception exception)
				{
					while (true)
					{
						int num10 = 1790158853;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num10 ^ 0x7CD11378)) % 4)
							{
							case 2u:
								break;
							default:
								return;
							case 1u:
								dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
								num10 = ((int)num3 * -1070161723) ^ 0x7C05196D;
								continue;
							case 0u:
								VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetException(exception);
								num10 = (int)(num3 * 1589852235) ^ -1704256661;
								continue;
							case 3u:
								return;
							}
							break;
						}
					}
				}
				dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
				while (true)
				{
					int num11 = 2025043143;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num11 ^ 0x7CD11378)) % 3)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
							goto IL_02a3;
						case 0u:
							return;
						}
						break;
						IL_02a3:
						VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetResult();
						num11 = ((int)num3 * -763511132) ^ -2061661897;
					}
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout(LayoutKind.Auto)]
		private struct QyyRUNBBRtsqHDvfDSDqZGDbbGMaA : IAsyncStateMachine
		{
			public int dagGWVjAonzlEQhnHtJbFGTLQUwi;

			public AsyncTaskMethodBuilder<(long bytesRead, byte[] readBuffer)> VVEGRPiIETvwaByBfDgZFHzJPzZZ;

			public string jyNKWByJQfDzIMQqKDNyCQesbfuN;

			public byte[] rZKAWAthtGScTPcLspnVRctHaZCw;

			public int seNhChApuVtHuVyqbVnPjWxLiDSZ;

			private FileStream sxmodmLdwXGkBaRLHZeplGurOmcz;

			private TaskAwaiter<int> ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;

			private void MoveNext()
			{
				int num = dagGWVjAonzlEQhnHtJbFGTLQUwi;
				(long, byte[]) result2;
				try
				{
					int count = default(int);
					if (num != 0)
					{
						long length = default(long);
						while (true)
						{
							int num2 = -793399034;
							while (true)
							{
								uint num3;
								switch ((num3 = (uint)(num2 ^ -1619935555)) % 6)
								{
								case 5u:
									break;
								case 3u:
								{
									length = new FileInfo(jyNKWByJQfDzIMQqKDNyCQesbfuN).Length;
									int num4;
									int num5;
									if (rZKAWAthtGScTPcLspnVRctHaZCw == null)
									{
										num4 = -596054720;
										num5 = num4;
									}
									else
									{
										num4 = -915958179;
										num5 = num4;
									}
									num2 = num4 ^ ((int)num3 * -1903015325);
									continue;
								}
								case 2u:
									sxmodmLdwXGkBaRLHZeplGurOmcz = new FileStream(jyNKWByJQfDzIMQqKDNyCQesbfuN, FileMode.Open, FileAccess.Read);
									num2 = (int)((num3 * 333039191) ^ 0x9DA8579);
									continue;
								case 1u:
									count = (int)Math.Min(rZKAWAthtGScTPcLspnVRctHaZCw.Length - seNhChApuVtHuVyqbVnPjWxLiDSZ, length);
									num2 = -1110173229;
									continue;
								case 0u:
									rZKAWAthtGScTPcLspnVRctHaZCw = new byte[length + seNhChApuVtHuVyqbVnPjWxLiDSZ];
									num2 = ((int)num3 * -344176557) ^ -717729080;
									continue;
								default:
									goto end_IL_000d;
								}
								break;
							}
							continue;
							end_IL_000d:
							break;
						}
					}
					try
					{
						if (num != 0)
						{
							goto IL_00e9;
						}
						goto IL_01bc;
						IL_00e9:
						int num6 = -1847450701;
						goto IL_00ee;
						IL_00ee:
						TaskAwaiter<int> awaiter = default(TaskAwaiter<int>);
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num6 ^ -1619935555)) % 6)
							{
							case 0u:
								break;
							case 4u:
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = default(TaskAwaiter<int>);
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = -1);
								num6 = ((int)num3 * -1492247603) ^ -1406587696;
								continue;
							case 5u:
								num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = 0);
								ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = awaiter;
								VVEGRPiIETvwaByBfDgZFHzJPzZZ.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							case 2u:
							{
								awaiter = sxmodmLdwXGkBaRLHZeplGurOmcz.ReadAsync(rZKAWAthtGScTPcLspnVRctHaZCw, seNhChApuVtHuVyqbVnPjWxLiDSZ, count).GetAwaiter();
								int num7;
								int num8;
								if (!awaiter.IsCompleted)
								{
									num7 = -1484169702;
									num8 = num7;
								}
								else
								{
									num7 = -1110217146;
									num8 = num7;
								}
								num6 = num7 ^ ((int)num3 * -390160040);
								continue;
							}
							case 3u:
								goto IL_01bc;
							default:
							{
								int result = awaiter.GetResult();
								result2 = (result, rZKAWAthtGScTPcLspnVRctHaZCw);
								goto end_IL_00e3;
							}
							}
							break;
						}
						goto IL_00e9;
						IL_01bc:
						awaiter = ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;
						num6 = -215604765;
						goto IL_00ee;
						end_IL_00e3:;
					}
					finally
					{
						if (num < 0)
						{
							while (true)
							{
								IL_01ef:
								int num9 = -515879914;
								while (true)
								{
									uint num3;
									switch ((num3 = (uint)(num9 ^ -1619935555)) % 4)
									{
									case 0u:
										break;
									default:
										goto end_IL_01f4;
									case 3u:
									{
										int num10;
										int num11;
										if (sxmodmLdwXGkBaRLHZeplGurOmcz == null)
										{
											num10 = -102393441;
											num11 = num10;
										}
										else
										{
											num10 = -1547764588;
											num11 = num10;
										}
										num9 = num10 ^ ((int)num3 * -949908464);
										continue;
									}
									case 1u:
										((IDisposable)sxmodmLdwXGkBaRLHZeplGurOmcz).Dispose();
										num9 = (int)(num3 * 865082774) ^ -1623342711;
										continue;
									case 2u:
										goto end_IL_01f4;
									}
									goto IL_01ef;
									continue;
									end_IL_01f4:
									break;
								}
								break;
							}
						}
					}
				}
				catch (Exception exception)
				{
					while (true)
					{
						int num12 = -602226656;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num12 ^ -1619935555)) % 3)
							{
							case 2u:
								break;
							default:
								return;
							case 1u:
								goto IL_0279;
							case 0u:
								return;
							}
							break;
							IL_0279:
							dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
							VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetException(exception);
							num12 = ((int)num3 * -668091214) ^ 0x66C0096;
						}
					}
				}
				dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
				while (true)
				{
					int num13 = -376354760;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num13 ^ -1619935555)) % 3)
						{
						case 2u:
							break;
						default:
							return;
						case 1u:
							goto IL_02cb;
						case 0u:
							return;
						}
						break;
						IL_02cb:
						VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetResult(result2);
						num13 = ((int)num3 * -961121980) ^ -2133516860;
					}
				}
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private sealed class pIYWguDmZCloYILqODpXfIyPIkhOA
		{
			public DotNetIO vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public string jyNKWByJQfDzIMQqKDNyCQesbfuN;

			internal bool koCSMJzrxwMJMjdmPNKmrHaAcQyDA()
			{
				return vvKNDIxiYKTrRTKAccPPCIdmGSFtA.FileExists(jyNKWByJQfDzIMQqKDNyCQesbfuN);
			}
		}

		private sealed class mBLYRPxTWbAqtaVxnBZAGmgOaOYq
		{
			public DotNetIO vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public string jyNKWByJQfDzIMQqKDNyCQesbfuN;

			internal void tcgacUqtToGRZhqFbYSKGIsroRCoA()
			{
				vvKNDIxiYKTrRTKAccPPCIdmGSFtA.DeleteFile(jyNKWByJQfDzIMQqKDNyCQesbfuN);
			}
		}

		public void WriteToFile(string path, byte[] buffer)
		{
			WriteToFile(path, buffer, 0, buffer.Length);
		}

		public void WriteToFile(string path, byte[] buffer, int offset, int length)
		{
			TBDgzroaMrwyuzpKmuzHCZSOcaWo(path);
			FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
			try
			{
				fileStream.Write(buffer, offset, length);
			}
			finally
			{
				if (fileStream != null)
				{
					while (true)
					{
						IL_001f:
						int num = 474260172;
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num ^ 0x5CB4C711)) % 3)
							{
							case 0u:
								break;
							default:
								goto end_IL_0024;
							case 2u:
								goto IL_0041;
							case 1u:
								goto end_IL_0024;
							}
							goto IL_001f;
							IL_0041:
							((IDisposable)fileStream).Dispose();
							num = ((int)num2 * -1594230467) ^ -1576194561;
							continue;
							end_IL_0024:
							break;
						}
						break;
					}
				}
			}
		}

		public Task WriteToFileAsync(string path, byte[] buffer)
		{
			return WriteToFileAsync(path, buffer, 0, buffer.Length);
		}

		[AsyncStateMachine(typeof(_003CWriteToFileAsync_003Ed__3))]
		public Task WriteToFileAsync(string path, byte[] buffer, int offset, int length)
		{
			BGrpLvRoNQymXPFZbSDsdmhVGhDU stateMachine = default(BGrpLvRoNQymXPFZbSDsdmhVGhDU);
			stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ = AsyncTaskMethodBuilder.Create();
			while (true)
			{
				int num = -453219609;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -2126869213)) % 8)
					{
					case 7u:
						break;
					case 6u:
						stateMachine.seNhChApuVtHuVyqbVnPjWxLiDSZ = offset;
						num = ((int)num2 * -1171859430) ^ -147385462;
						continue;
					case 1u:
						stateMachine.rZKAWAthtGScTPcLspnVRctHaZCw = buffer;
						num = (int)(num2 * 763017014) ^ -246756101;
						continue;
					case 4u:
						stateMachine.vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this;
						stateMachine.jyNKWByJQfDzIMQqKDNyCQesbfuN = path;
						num = (int)((num2 * 1100247742) ^ 0x5F364402);
						continue;
					case 3u:
						stateMachine.dagGWVjAonzlEQhnHtJbFGTLQUwi = -1;
						num = (int)(num2 * 1611919521) ^ -525804094;
						continue;
					case 5u:
						stateMachine.jUwhDMdnUFfyHoPnyjvMdaluUSKBA = length;
						num = ((int)num2 * -1790404852) ^ 0x3BAF9C6C;
						continue;
					case 2u:
						stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Start(ref stateMachine);
						num = ((int)num2 * -553729936) ^ -1203924797;
						continue;
					default:
						return stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Task;
					}
					break;
				}
			}
		}

		public void ReadFromFile(string path, ref byte[] buffer, out long numReadBytes)
		{
			ReadFromFile(path, ref buffer, 0, out numReadBytes);
		}

		public void ReadFromFile(string path, ref byte[] buffer, int offset, out long numReadBytes)
		{
			long length = new FileInfo(path).Length;
			if (buffer == null)
			{
				while (true)
				{
					int num = 2117970406;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x7E56348E)) % 3)
						{
						case 0u:
							break;
						case 2u:
							buffer = new byte[length + offset];
							num = (int)((num2 * 2134475883) ^ 0x4965BCE);
							continue;
						default:
							goto end_IL_0010;
						}
						break;
					}
					continue;
					end_IL_0010:
					break;
				}
			}
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			try
			{
				numReadBytes = (int)Math.Min(buffer.Length - offset, length);
				fileStream.Read(buffer, offset, (int)numReadBytes);
			}
			finally
			{
				if (fileStream != null)
				{
					while (true)
					{
						IL_007b:
						int num3 = 1255500424;
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num3 ^ 0x7E56348E)) % 3)
							{
							case 2u:
								break;
							default:
								goto end_IL_0080;
							case 1u:
								goto IL_009d;
							case 0u:
								goto end_IL_0080;
							}
							goto IL_007b;
							IL_009d:
							((IDisposable)fileStream).Dispose();
							num3 = (int)(num2 * 1260658666) ^ -2012276728;
							continue;
							end_IL_0080:
							break;
						}
						break;
					}
				}
			}
		}

		public Task<(long bytesRead, byte[] readBuffer)> ReadFromFileAsync(string path, byte[] buffer)
		{
			return ReadFromFileAsync(path, buffer, 0);
		}

		[AsyncStateMachine(typeof(_003CReadFromFileAsync_003Ed__7))]
		public Task<(long bytesRead, byte[] readBuffer)> ReadFromFileAsync(string path, byte[] buffer, int offset)
		{
			QyyRUNBBRtsqHDvfDSDqZGDbbGMaA stateMachine = default(QyyRUNBBRtsqHDvfDSDqZGDbbGMaA);
			stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ = AsyncTaskMethodBuilder<(long, byte[])>.Create();
			while (true)
			{
				int num = -1651674492;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -28907603)) % 6)
					{
					case 0u:
						break;
					case 3u:
						stateMachine.jyNKWByJQfDzIMQqKDNyCQesbfuN = path;
						num = ((int)num2 * -2096485767) ^ -897495030;
						continue;
					case 4u:
						stateMachine.rZKAWAthtGScTPcLspnVRctHaZCw = buffer;
						num = ((int)num2 * -1566446692) ^ -1408963848;
						continue;
					case 5u:
						stateMachine.seNhChApuVtHuVyqbVnPjWxLiDSZ = offset;
						num = (int)((num2 * 1518833175) ^ 0x4759D359);
						continue;
					case 1u:
						stateMachine.dagGWVjAonzlEQhnHtJbFGTLQUwi = -1;
						stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Start(ref stateMachine);
						num = (int)((num2 * 45954651) ^ 0x6E6CC5CA);
						continue;
					default:
						return stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Task;
					}
					break;
				}
			}
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public Task<bool> FileExistsAsync(string path)
		{
			return Task.Run((Func<bool>)new pIYWguDmZCloYILqODpXfIyPIkhOA
			{
				vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this,
				jyNKWByJQfDzIMQqKDNyCQesbfuN = path
			}.koCSMJzrxwMJMjdmPNKmrHaAcQyDA);
		}

		public void DeleteFile(string path)
		{
			File.Delete(path);
		}

		public Task DeleteFileAsync(string path)
		{
			return Task.Run((Action)new mBLYRPxTWbAqtaVxnBZAGmgOaOYq
			{
				vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this,
				jyNKWByJQfDzIMQqKDNyCQesbfuN = path
			}.tcgacUqtToGRZhqFbYSKGIsroRCoA);
		}

		private void TBDgzroaMrwyuzpKmuzHCZSOcaWo(string P_0)
		{
			string directoryName = Path.GetDirectoryName(P_0);
			if (directoryName == null)
			{
				return;
			}
			while (true)
			{
				int num = 210423234;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x3EA1C99B)) % 4)
					{
					case 3u:
						break;
					default:
						return;
					case 1u:
					{
						int num3;
						int num4;
						if (!Directory.Exists(directoryName))
						{
							num3 = 92977289;
							num4 = num3;
						}
						else
						{
							num3 = 1379764119;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -897819258);
						continue;
					}
					case 0u:
						Directory.CreateDirectory(directoryName);
						num = ((int)num2 * -189831383) ^ 0x1DBAB725;
						continue;
					case 2u:
						return;
					}
					break;
				}
			}
		}
	}
}
