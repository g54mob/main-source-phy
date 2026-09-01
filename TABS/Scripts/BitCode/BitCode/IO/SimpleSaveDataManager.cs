using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BitCode.Users;
using JetBrains.Annotations;

namespace BitCode.IO
{
	public class SimpleSaveDataManager : IPlatformService, ISaveDataManager
	{
		[StructLayout(LayoutKind.Auto)]
		private struct WeKJeyCOEvhZoNlDkiLxdLTSSIv : IAsyncStateMachine
		{
			public int dagGWVjAonzlEQhnHtJbFGTLQUwi;

			public AsyncTaskMethodBuilder<byte[]> VVEGRPiIETvwaByBfDgZFHzJPzZZ;

			public SimpleSaveDataManager vvKNDIxiYKTrRTKAccPPCIdmGSFtA;

			public ILocalAccount fBrBfxJbxIQeYFohUegeVFySqyMWA;

			public string jyNKWByJQfDzIMQqKDNyCQesbfuN;

			private TaskAwaiter<(long bytesRead, byte[] readBuffer)> ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;

			private void MoveNext()
			{
				int num = dagGWVjAonzlEQhnHtJbFGTLQUwi;
				SimpleSaveDataManager simpleSaveDataManager = vvKNDIxiYKTrRTKAccPPCIdmGSFtA;
				byte[] item;
				try
				{
					if (num != 0)
					{
						goto IL_0014;
					}
					goto IL_00b4;
					IL_0014:
					int num2 = 658806176;
					goto IL_0019;
					IL_0019:
					TaskAwaiter<(long, byte[])> awaiter = default(TaskAwaiter<(long, byte[])>);
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num2 ^ 0x7984508F)) % 10)
						{
						case 6u:
							break;
						case 5u:
						{
							string destinationPath = simpleSaveDataManager.GetDestinationPath(fBrBfxJbxIQeYFohUegeVFySqyMWA, jyNKWByJQfDzIMQqKDNyCQesbfuN);
							awaiter = simpleSaveDataManager.IOWrapper.ReadFromFileAsync(destinationPath, null).GetAwaiter();
							num2 = (int)((num3 * 118774741) ^ 0x6D17884C);
							continue;
						}
						case 0u:
						{
							int num4;
							int num5;
							if (!awaiter.IsCompleted)
							{
								num4 = -1162978631;
								num5 = num4;
							}
							else
							{
								num4 = -1598295252;
								num5 = num4;
							}
							num2 = num4 ^ (int)(num3 * 1315765050);
							continue;
						}
						case 7u:
							goto IL_00b4;
						case 2u:
							ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = default(TaskAwaiter<(long, byte[])>);
							num2 = ((int)num3 * -1914805353) ^ -923880576;
							continue;
						case 8u:
							VVEGRPiIETvwaByBfDgZFHzJPzZZ.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							num2 = (int)((num3 * 77090261) ^ 0x60096364);
							continue;
						case 4u:
							num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = 0);
							ZXmTiyGvYGuRDpIPfYHtBgPoRgDC = awaiter;
							num2 = (int)((num3 * 1245533112) ^ 0x48670635);
							continue;
						case 1u:
							num = (dagGWVjAonzlEQhnHtJbFGTLQUwi = -1);
							num2 = ((int)num3 * -2085402587) ^ -349509353;
							continue;
						case 9u:
							return;
						default:
							item = awaiter.GetResult().Item2;
							goto end_IL_000e;
						}
						break;
					}
					goto IL_0014;
					IL_00b4:
					awaiter = ZXmTiyGvYGuRDpIPfYHtBgPoRgDC;
					num2 = 1653945199;
					goto IL_0019;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					while (true)
					{
						int num6 = 123564162;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num6 ^ 0x7984508F)) % 3)
							{
							case 0u:
								break;
							default:
								return;
							case 1u:
								goto IL_0192;
							case 2u:
								return;
							}
							break;
							IL_0192:
							dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
							VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetException(exception);
							num6 = (int)((num3 * 1903173403) ^ 0x3CACF772);
						}
					}
				}
				dagGWVjAonzlEQhnHtJbFGTLQUwi = -2;
				while (true)
				{
					int num7 = 1831068755;
					while (true)
					{
						uint num3;
						switch ((num3 = (uint)(num7 ^ 0x7984508F)) % 3)
						{
						case 0u:
							break;
						default:
							return;
						case 2u:
							goto IL_01e4;
						case 1u:
							return;
						}
						break;
						IL_01e4:
						VVEGRPiIETvwaByBfDgZFHzJPzZZ.SetResult(item);
						num7 = (int)((num3 * 1557183700) ^ 0x7703EE6E);
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

		protected readonly IIOWrapper IOWrapper;

		protected readonly string BasePath;

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		public SimpleSaveDataManager([NotNull] IIOWrapper ioWrapper, [NotNull] string basePath)
		{
			if (string.IsNullOrWhiteSpace(basePath))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", "basePath");
			}
			IOWrapper = ioWrapper ?? throw new ArgumentNullException("ioWrapper");
			BasePath = basePath;
		}

		public virtual void SaveData([CanBeNull] ILocalAccount userAccount, string path, byte[] data)
		{
			string destinationPath = GetDestinationPath(userAccount, path);
			IOWrapper.WriteToFile(destinationPath, data);
		}

		public virtual Task SaveDataAsync([CanBeNull] ILocalAccount userAccount, string path, byte[] data)
		{
			string destinationPath = GetDestinationPath(userAccount, path);
			return IOWrapper.WriteToFileAsync(destinationPath, data);
		}

		public virtual byte[] LoadData([CanBeNull] ILocalAccount userAccount, string path)
		{
			string destinationPath = GetDestinationPath(userAccount, path);
			byte[] buffer = null;
			while (true)
			{
				int num = -312513692;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1947783076)) % 3)
					{
					case 2u:
						break;
					case 1u:
						goto IL_002d;
					default:
						return buffer;
					}
					break;
					IL_002d:
					IOWrapper.ReadFromFile(destinationPath, ref buffer, out var _);
					num = (int)(num2 * 1376113965) ^ -16766094;
				}
			}
		}

		[AsyncStateMachine(typeof(_003CLoadDataAsync_003Ed__9))]
		public virtual Task<byte[]> LoadDataAsync([CanBeNull] ILocalAccount userAccount, string path)
		{
			WeKJeyCOEvhZoNlDkiLxdLTSSIv stateMachine = default(WeKJeyCOEvhZoNlDkiLxdLTSSIv);
			stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ = AsyncTaskMethodBuilder<byte[]>.Create();
			while (true)
			{
				int num = -221246173;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -2141388410)) % 4)
					{
					case 0u:
						break;
					case 1u:
						stateMachine.vvKNDIxiYKTrRTKAccPPCIdmGSFtA = this;
						stateMachine.fBrBfxJbxIQeYFohUegeVFySqyMWA = userAccount;
						stateMachine.jyNKWByJQfDzIMQqKDNyCQesbfuN = path;
						stateMachine.dagGWVjAonzlEQhnHtJbFGTLQUwi = -1;
						num = ((int)num2 * -1950490535) ^ -806252868;
						continue;
					case 3u:
						stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Start(ref stateMachine);
						num = (int)((num2 * 2031358096) ^ 0x51849734);
						continue;
					default:
						return stateMachine.VVEGRPiIETvwaByBfDgZFHzJPzZZ.Task;
					}
					break;
				}
			}
		}

		public virtual bool DataExists(ILocalAccount userAccount, string path)
		{
			string destinationPath = GetDestinationPath(userAccount, path);
			return IOWrapper.FileExists(destinationPath);
		}

		public virtual Task<bool> DataExistsAsync(ILocalAccount userAccount, string path)
		{
			string destinationPath = GetDestinationPath(userAccount, path);
			return IOWrapper.FileExistsAsync(destinationPath);
		}

		public virtual void DeleteData(ILocalAccount userAccount, string path)
		{
			string destinationPath = GetDestinationPath(userAccount, path);
			IOWrapper.DeleteFile(destinationPath);
		}

		public virtual Task DeleteDataAsync(ILocalAccount userAccount, string path)
		{
			string destinationPath = GetDestinationPath(userAccount, path);
			return IOWrapper.DeleteFileAsync(destinationPath);
		}

		protected virtual string GetDestinationPath(ILocalAccount userAccount, string path)
		{
			if (userAccount != null)
			{
				while (true)
				{
					int num = 1231988681;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x717E17BA)) % 5)
						{
						case 0u:
							break;
						case 4u:
							goto end_IL_0003;
						case 2u:
						{
							int num5;
							int num6;
							if (!userAccount.Name.NeedsLoading())
							{
								num5 = -1283934636;
								num6 = num5;
							}
							else
							{
								num5 = -2051470089;
								num6 = num5;
							}
							num = num5 ^ (int)(num2 * 751745152);
							continue;
						}
						case 3u:
						{
							int num3;
							int num4;
							if (userAccount.Name.Tracked)
							{
								num3 = 1686397810;
								num4 = num3;
							}
							else
							{
								num3 = 75514252;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 67942233);
							continue;
						}
						default:
							return Path.Combine(BasePath, Utilities.SanitizeFileName(userAccount.Name.Value), path);
						}
						break;
					}
					continue;
					end_IL_0003:
					break;
				}
			}
			return Path.Combine(BasePath, path);
		}
	}
}
