using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILeaderboardEntry : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass34_0
	{
		public UILeaderboardEntry _003C_003E4__this;

		public int pending;

		internal void _003CLoadMapImagesRoutine_003Eb__0(Texture2D texture)
		{
			if (texture != null)
			{
				UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
				uILeaderboardEntry.mapTextures.Add(texture);
				UILeaderboardEntry uILeaderboardEntry2 = _003C_003E4__this;
				if (uILeaderboardEntry2.Image_Map != null)
				{
					UILeaderboardEntry uILeaderboardEntry3 = _003C_003E4__this;
					uILeaderboardEntry3.Image_Map.texture = texture;
				}
			}
			int num = pending - 1;
			pending = num;
		}
	}

	private sealed class _003C_003Ec__DisplayClass35_0
	{
		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		public LeaderboardImageThrottle.ZipFramesRequest handle;

		public int pending;

		internal void _003CLoadZipFramesRoutine_003Eb__0(Texture2D preview)
		{
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			if (version == uILeaderboardEntry.mapLoadVersion && preview != null)
			{
				UILeaderboardEntry uILeaderboardEntry2 = _003C_003E4__this;
				if (uILeaderboardEntry2.Image_Map != null)
				{
					UILeaderboardEntry uILeaderboardEntry3 = _003C_003E4__this;
					uILeaderboardEntry3.Image_Map.texture = preview;
				}
			}
		}

		internal void _003CLoadZipFramesRoutine_003Eb__1(Texture2D[] frames)
		{
			//IL_009b: Expected O, but got I
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Expected O, but got Unknown
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Expected O, but got Unknown
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			bool flag = uILeaderboardEntry.activeZipRequests.Remove(handle);
			UILeaderboardEntry uILeaderboardEntry2 = _003C_003E4__this;
			if (version == uILeaderboardEntry2.mapLoadVersion)
			{
				UILeaderboardEntry uILeaderboardEntry3 = _003C_003E4__this;
				List<Texture2D> mapTextures = uILeaderboardEntry3.mapTextures;
				int num = mapTextures._version + 1;
				mapTextures._version = num;
				if (!((List<LeaderboardImageThrottle.ZipFramesRequest>)0).Remove(handle))
				{
					mapTextures._size = 0;
				}
				else
				{
					mapTextures._size = 0;
					if (mapTextures._size > 0)
					{
						Array.Clear(mapTextures._items, 0, mapTextures._size);
					}
				}
				if (frames != null)
				{
					object obj = frames + 32;
					int num2 = 0;
					while (num2 < frames.Length)
					{
						if ((UnityEngine.Object)obj != null)
						{
							UILeaderboardEntry uILeaderboardEntry4 = _003C_003E4__this;
							uILeaderboardEntry4.mapTextures.Add((Texture2D)obj);
						}
						num2++;
						obj += 8;
					}
				}
			}
			int num3 = pending - 1;
			pending = num3;
		}
	}

	private sealed class _003C_003Ec__DisplayClass36_0
	{
		public string replayPath;

		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		public LeaderboardImageThrottle.ZipFramesRequest handle;

		public int pending;

		internal byte[] _003CLoadLocalZipFramesRoutine_003Eb__0()
		{
			return File.ReadAllBytes(replayPath);
		}

		internal void _003CLoadLocalZipFramesRoutine_003Eb__1(Texture2D preview)
		{
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			if (version == uILeaderboardEntry.mapLoadVersion && preview != null)
			{
				UILeaderboardEntry uILeaderboardEntry2 = _003C_003E4__this;
				if (uILeaderboardEntry2.Image_Map != null)
				{
					UILeaderboardEntry uILeaderboardEntry3 = _003C_003E4__this;
					uILeaderboardEntry3.Image_Map.texture = preview;
				}
			}
		}

		internal void _003CLoadLocalZipFramesRoutine_003Eb__2(Texture2D[] frames)
		{
			//IL_009b: Expected O, but got I
			//IL_013a: Unknown result type (might be due to invalid IL or missing references)
			//IL_013f: Expected O, but got Unknown
			//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bb: Expected O, but got Unknown
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			bool flag = uILeaderboardEntry.activeZipRequests.Remove(handle);
			UILeaderboardEntry uILeaderboardEntry2 = _003C_003E4__this;
			if (version == uILeaderboardEntry2.mapLoadVersion)
			{
				UILeaderboardEntry uILeaderboardEntry3 = _003C_003E4__this;
				List<Texture2D> mapTextures = uILeaderboardEntry3.mapTextures;
				int num = mapTextures._version + 1;
				mapTextures._version = num;
				if (!((List<LeaderboardImageThrottle.ZipFramesRequest>)0).Remove(handle))
				{
					mapTextures._size = 0;
				}
				else
				{
					mapTextures._size = 0;
					if (mapTextures._size > 0)
					{
						Array.Clear(mapTextures._items, 0, mapTextures._size);
					}
				}
				if (frames != null)
				{
					object obj = frames + 32;
					int num2 = 0;
					while (num2 < frames.Length)
					{
						if ((UnityEngine.Object)obj != null)
						{
							UILeaderboardEntry uILeaderboardEntry4 = _003C_003E4__this;
							uILeaderboardEntry4.mapTextures.Add((Texture2D)obj);
						}
						num2++;
						obj += 8;
					}
				}
			}
			int num3 = pending - 1;
			pending = num3;
		}
	}

	private sealed class _003C_003Ec__DisplayClass37_0
	{
		public UILeaderboardEntry _003C_003E4__this;

		public LeaderboardImageThrottle.TextureRequest handle;

		public int version;

		public Action<Texture2D> onComplete;

		internal void _003CQueueTextureRequest_003Eb__0(Texture2D texture)
		{
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			bool flag = uILeaderboardEntry.activeTextureRequests.Remove(handle);
			UILeaderboardEntry uILeaderboardEntry2 = _003C_003E4__this;
			if (version == uILeaderboardEntry2.mapLoadVersion)
			{
				Action<Texture2D> action = onComplete;
				if (onComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v126 @ rcx_v5 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
		}
	}

	private sealed class _003C_003Ec__DisplayClass45_0
	{
		public string dir;

		public TimeSpan maxAge;

		internal void _003CCleanupImageCacheOlderThan_003Eb__0()
		{
			//IL_0070: Expected O, but got I4
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Expected O, but got Unknown
			if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = utcNow - maxAge;
			string[] files = Directory.GetFiles(dir, "*.img");
			object obj = 0;
			while ((nint)obj < files.Length)
			{
				DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(files[obj]);
				if (lastWriteTimeUtc < dateTime)
				{
					File.Delete(files[obj]);
				}
				obj++;
			}
		}
	}

	private sealed class _003CCycleMapImagesRoutine_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UILeaderboardEntry _003C_003E4__this;

		private int _003Cindex_003E5__2;

		private WaitForSeconds _003Cwait_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCycleMapImagesRoutine_003Ed__38(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0097: Expected I4, but got I8
			//IL_01df: Expected I4, but got O
			//IL_01aa: Expected O, but got I4
			//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_01bc: Expected I4, but got Unknown
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003Cindex_003E5__2 = _003C_003E1__state;
				if ((object)_003C_003E4__this != null)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(uILeaderboardEntry.MapImageCycleInterval);
					_003Cwait_003E5__3 = waitForSeconds;
					goto IL_020b;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					if (!(uILeaderboardEntry.Image_Map != null))
					{
						goto IL_020b;
					}
					List<Texture2D> mapTextures = uILeaderboardEntry.mapTextures;
					if (uILeaderboardEntry.mapTextures != null)
					{
						if (mapTextures._size == 0)
						{
							goto IL_020b;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((object)uILeaderboardEntry.Image_Map != null)
						{
							Texture texture = default(Texture);
							uILeaderboardEntry.Image_Map.texture = texture;
							List<Texture2D> mapTextures2 = uILeaderboardEntry.mapTextures;
							if (uILeaderboardEntry.mapTextures != null)
							{
								object obj = _003Cindex_003E5__2 + 1;
								int num = obj % mapTextures2._size;
								_003Cindex_003E5__2 = num;
								goto IL_020b;
							}
						}
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_020b:
			_003C_003E2__current = _003Cwait_003E5__3;
			_003C_003E1__state = 1;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CLoadLocalZipFramesRoutine_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string replayPath;

		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		private _003C_003Ec__DisplayClass36_0 _003C_003E8__1;

		private Task<byte[]> _003CreadTask_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLoadLocalZipFramesRoutine_003Ed__36(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0084: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_0861: Expected I4, but got O
			//IL_006b: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_06da: Expected O, but got I4
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			object obj2;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						goto IL_0845;
					}
					_003C_003E1__state = -1;
					obj2 = null;
					goto IL_088a;
				}
				_003C_003E1__state = -1;
				obj2 = null;
				goto IL_08b3;
			}
			_003C_003E1__state = -1;
			_003C_003Ec__DisplayClass36_0 obj3 = new _003C_003Ec__DisplayClass36_0();
			_003C_003E8__1 = obj3;
			_003C_003Ec__DisplayClass36_0 obj4 = _003C_003E8__1;
			if (_003C_003E8__1 != null)
			{
				obj4.replayPath = replayPath;
				_003C_003Ec__DisplayClass36_0 obj5 = _003C_003E8__1;
				if (_003C_003E8__1 != null)
				{
					obj5._003C_003E4__this = _003C_003E4__this;
					_003C_003Ec__DisplayClass36_0 obj6 = _003C_003E8__1;
					if (_003C_003E8__1 != null)
					{
						obj6.version = version;
						Func<byte[]> function = () => File.ReadAllBytes(_003C_003E8__1.replayPath);
						Task<byte[]> task = Task.Run(function);
						_003CreadTask_003E5__2 = task;
						obj2 = null;
						goto IL_08b3;
					}
				}
			}
			goto IL_0853;
			IL_0853:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0833:
			uILeaderboardEntry.mapLoadRoutine = (Coroutine)obj2;
			goto IL_0845;
			IL_071d:
			if (uILeaderboardEntry.CycleMapImages)
			{
				List<Texture2D> mapTextures = uILeaderboardEntry.mapTextures;
				if (uILeaderboardEntry.mapTextures == null)
				{
					goto IL_0853;
				}
				if (mapTextures._size > 1)
				{
					IEnumerator routine = _003C_003E4__this.CycleMapImagesRoutine();
					Coroutine mapCycleRoutine = _003C_003E4__this.StartCoroutine(routine);
					uILeaderboardEntry.mapCycleRoutine = mapCycleRoutine;
				}
			}
			goto IL_0833;
			IL_0845:
			return false;
			IL_088a:
			_003C_003Ec__DisplayClass36_0 obj7 = _003C_003E8__1;
			Texture texture = default(Texture);
			if (_003C_003E8__1 != null)
			{
				if (obj7.pending > 0)
				{
					if ((object)_003C_003E4__this != null)
					{
						if (obj7.version != uILeaderboardEntry.mapLoadVersion)
						{
							goto IL_0845;
						}
						_003C_003E2__current = obj2;
						_003C_003E1__state = 2;
						goto IL_08d2;
					}
				}
				else if ((object)_003C_003E4__this != null)
				{
					List<Texture2D> mapTextures2 = uILeaderboardEntry.mapTextures;
					if (uILeaderboardEntry.mapTextures != null)
					{
						if (mapTextures2._size == 0)
						{
							if (uILeaderboardEntry.Image_Map != null)
							{
								if ((object)uILeaderboardEntry.Image_Map == null)
								{
									goto IL_0853;
								}
								uILeaderboardEntry.Image_Map.texture = null;
							}
							goto IL_0833;
						}
						if (!(uILeaderboardEntry.Image_Map != null))
						{
							goto IL_071d;
						}
						RawImage image_Map = uILeaderboardEntry.Image_Map;
						if ((object)uILeaderboardEntry.Image_Map != null)
						{
							if (!(image_Map.m_Texture == null))
							{
								goto IL_071d;
							}
							List<Texture2D> mapTextures3 = uILeaderboardEntry.mapTextures;
							if (uILeaderboardEntry.mapTextures != null)
							{
								object obj8 = mapTextures3._size - 1;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								if ((object)uILeaderboardEntry.Image_Map != null)
								{
									uILeaderboardEntry.Image_Map.texture = texture;
									goto IL_071d;
								}
							}
						}
					}
				}
			}
			goto IL_0853;
			IL_08d2:
			return true;
			IL_08b3:
			if (_003CreadTask_003E5__2 != null)
			{
				bool isCompleted = _003CreadTask_003E5__2.IsCompleted;
				_003C_003Ec__DisplayClass36_0 obj9 = _003C_003E8__1;
				if (_003C_003E8__1 != null)
				{
					if (!isCompleted)
					{
						if ((object)_003C_003E4__this != null)
						{
							if (obj9.version != uILeaderboardEntry.mapLoadVersion)
							{
								goto IL_0845;
							}
							_003C_003E2__current = obj2;
							_003C_003E1__state = 1;
							goto IL_08d2;
						}
					}
					else if ((object)_003C_003E4__this != null)
					{
						if (obj9.version != uILeaderboardEntry.mapLoadVersion)
						{
							goto IL_0833;
						}
						if (_003CreadTask_003E5__2 != null)
						{
							TaskStatus status = _003CreadTask_003E5__2.Status;
							if (status != TaskStatus.RanToCompletion)
							{
								goto IL_0833;
							}
							if (_003CreadTask_003E5__2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180922A20");
								if ((object)texture == null)
								{
									goto IL_0833;
								}
								if (_003CreadTask_003E5__2 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180922A20");
									if ((object)texture != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v355 @ stack_8_v8 (UnityEngine.Texture)+18]");
										if ((nint)0 == 0)
										{
											goto IL_0833;
										}
										_003C_003Ec__DisplayClass36_0 obj10 = _003C_003E8__1;
										if (_003C_003E8__1 != null)
										{
											obj10.pending = 1;
											_003C_003Ec__DisplayClass36_0 obj11 = _003C_003E8__1;
											if (_003C_003E8__1 != null)
											{
												obj11.handle = (LeaderboardImageThrottle.ZipFramesRequest)obj2;
												_003C_003Ec__DisplayClass36_0 obj12 = _003C_003E8__1;
												if (_003C_003E8__1 != null && _003CreadTask_003E5__2 != null)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180922A20");
													Action<Texture2D> onPreview = delegate(Texture2D preview)
													{
														UILeaderboardEntry uILeaderboardEntry2 = _003C_003E8__1._003C_003E4__this;
														if (_003C_003E8__1.version == uILeaderboardEntry2.mapLoadVersion && preview != null)
														{
															UILeaderboardEntry uILeaderboardEntry3 = _003C_003E8__1._003C_003E4__this;
															if (uILeaderboardEntry3.Image_Map != null)
															{
																UILeaderboardEntry uILeaderboardEntry4 = _003C_003E8__1._003C_003E4__this;
																uILeaderboardEntry4.Image_Map.texture = preview;
															}
														}
													};
													Action<Texture2D[]> onComplete = delegate(Texture2D[] frames)
													{
														//IL_009b: Expected O, but got I
														//IL_013a: Unknown result type (might be due to invalid IL or missing references)
														//IL_013f: Expected O, but got Unknown
														//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
														//IL_01bb: Expected O, but got Unknown
														UILeaderboardEntry uILeaderboardEntry2 = _003C_003E8__1._003C_003E4__this;
														bool flag2 = uILeaderboardEntry2.activeZipRequests.Remove(_003C_003E8__1.handle);
														UILeaderboardEntry uILeaderboardEntry3 = _003C_003E8__1._003C_003E4__this;
														if (_003C_003E8__1.version == uILeaderboardEntry3.mapLoadVersion)
														{
															UILeaderboardEntry uILeaderboardEntry4 = _003C_003E8__1._003C_003E4__this;
															List<Texture2D> mapTextures4 = uILeaderboardEntry4.mapTextures;
															int num = mapTextures4._version + 1;
															mapTextures4._version = num;
															if (!((List<LeaderboardImageThrottle.ZipFramesRequest>)0).Remove(_003C_003E8__1.handle))
															{
																mapTextures4._size = 0;
															}
															else
															{
																mapTextures4._size = 0;
																if (mapTextures4._size > 0)
																{
																	Array.Clear(mapTextures4._items, 0, mapTextures4._size);
																}
															}
															if (frames != null)
															{
																object obj14 = frames + 32;
																int num2 = 0;
																while (num2 < frames.Length)
																{
																	if ((UnityEngine.Object)obj14 != null)
																	{
																		UILeaderboardEntry uILeaderboardEntry5 = _003C_003E8__1._003C_003E4__this;
																		uILeaderboardEntry5.mapTextures.Add((Texture2D)obj14);
																	}
																	num2++;
																	obj14 += 8;
																}
															}
														}
														int pending = _003C_003E8__1.pending - 1;
														_003C_003E8__1.pending = pending;
													};
													byte[] zipBytes = default(byte[]);
													LeaderboardImageThrottle.ZipFramesRequest handle = LeaderboardImageThrottle.RequestZipFrameTexturesFromBytes(obj12.replayPath, zipBytes, onPreview, onComplete);
													obj12.handle = handle;
													_003C_003Ec__DisplayClass36_0 obj13 = _003C_003E8__1;
													if (_003C_003E8__1 != null && uILeaderboardEntry.activeZipRequests != null)
													{
														uILeaderboardEntry.activeZipRequests.Add(obj13.handle);
														obj2 = null;
														goto IL_088a;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_0853;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CLoadMapImagesRoutine_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UILeaderboardEntry _003C_003E4__this;

		public LeaderboardEntryResponse entry;

		public int version;

		private _003C_003Ec__DisplayClass34_0 _003C_003E8__1;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLoadMapImagesRoutine_003Ed__34(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_007a: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_076d: Expected I4, but got O
			//IL_0066: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_055c: Expected O, but got I4
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						_003C_003E1__state = -1;
						goto IL_0368;
					}
					goto IL_06c4;
				}
				_003C_003E1__state = -1;
				goto IL_011f;
			}
			_003C_003E1__state = -1;
			_003C_003Ec__DisplayClass34_0 obj2 = new _003C_003Ec__DisplayClass34_0();
			_003C_003E8__1 = obj2;
			_003C_003Ec__DisplayClass34_0 obj3 = _003C_003E8__1;
			if (_003C_003E8__1 != null)
			{
				obj3._003C_003E4__this = _003C_003E4__this;
				LeaderboardEntryResponse leaderboardEntryResponse = entry;
				if (entry != null)
				{
					if (string.IsNullOrWhiteSpace(leaderboardEntryResponse._003CZipUrl_003Ek__BackingField))
					{
						goto IL_011f;
					}
					LeaderboardEntryResponse leaderboardEntryResponse2 = entry;
					if (entry != null && (object)_003C_003E4__this != null)
					{
						_003CLoadZipFramesRoutine_003Ed__35 obj4 = new _003CLoadZipFramesRoutine_003Ed__35(0);
						obj4._003C_003E1__state = 0;
						obj4._003C_003E4__this = _003C_003E4__this;
						obj4.zipUrl = leaderboardEntryResponse2._003CZipUrl_003Ek__BackingField;
						obj4.version = version;
						_003C_003E2__current = obj4;
						_003C_003E1__state = 1;
						goto IL_07f3;
					}
				}
			}
			goto IL_075f;
			IL_06c4:
			return false;
			IL_059f:
			if (uILeaderboardEntry.CycleMapImages)
			{
				List<Texture2D> mapTextures = uILeaderboardEntry.mapTextures;
				if (uILeaderboardEntry.mapTextures == null)
				{
					goto IL_075f;
				}
				if (mapTextures._size > 1)
				{
					IEnumerator routine = _003C_003E4__this.CycleMapImagesRoutine();
					Coroutine mapCycleRoutine = _003C_003E4__this.StartCoroutine(routine);
					uILeaderboardEntry.mapCycleRoutine = mapCycleRoutine;
				}
			}
			goto IL_06b5;
			IL_07f3:
			return true;
			IL_0412:
			if (version != uILeaderboardEntry.mapLoadVersion)
			{
				goto IL_06c4;
			}
			List<Texture2D> mapTextures2 = uILeaderboardEntry.mapTextures;
			if (uILeaderboardEntry.mapTextures != null)
			{
				if (mapTextures2._size == 0)
				{
					if (uILeaderboardEntry.Image_Map != null)
					{
						if ((object)uILeaderboardEntry.Image_Map == null)
						{
							goto IL_075f;
						}
						uILeaderboardEntry.Image_Map.texture = null;
					}
					goto IL_06b5;
				}
				if (!(uILeaderboardEntry.Image_Map != null))
				{
					goto IL_059f;
				}
				RawImage image_Map = uILeaderboardEntry.Image_Map;
				if ((object)uILeaderboardEntry.Image_Map != null)
				{
					if (!(image_Map.m_Texture == null))
					{
						goto IL_059f;
					}
					List<Texture2D> mapTextures3 = uILeaderboardEntry.mapTextures;
					if (uILeaderboardEntry.mapTextures != null)
					{
						object obj5 = mapTextures3._size - 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if ((object)uILeaderboardEntry.Image_Map != null)
						{
							Texture texture = default(Texture);
							uILeaderboardEntry.Image_Map.texture = texture;
							goto IL_059f;
						}
					}
				}
			}
			goto IL_075f;
			IL_06b5:
			uILeaderboardEntry.mapLoadRoutine = null;
			goto IL_06c4;
			IL_075f:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_011f:
			if ((object)_003C_003E4__this != null)
			{
				if (version != uILeaderboardEntry.mapLoadVersion)
				{
					goto IL_0412;
				}
				List<Texture2D> mapTextures4 = uILeaderboardEntry.mapTextures;
				if (uILeaderboardEntry.mapTextures != null)
				{
					if (mapTextures4._size != 0)
					{
						goto IL_0412;
					}
					LeaderboardEntryResponse leaderboardEntryResponse3 = entry;
					if (entry != null)
					{
						if (string.IsNullOrWhiteSpace(leaderboardEntryResponse3._003CImageUrl_003Ek__BackingField))
						{
							goto IL_0412;
						}
						_003C_003Ec__DisplayClass34_0 obj6 = _003C_003E8__1;
						if (_003C_003E8__1 != null)
						{
							obj6.pending = 1;
							LeaderboardEntryResponse leaderboardEntryResponse4 = entry;
							if (entry != null)
							{
								Action<Texture2D> onComplete = delegate(Texture2D texture2D)
								{
									if (texture2D != null)
									{
										UILeaderboardEntry uILeaderboardEntry2 = _003C_003E8__1._003C_003E4__this;
										uILeaderboardEntry2.mapTextures.Add(texture2D);
										UILeaderboardEntry uILeaderboardEntry3 = _003C_003E8__1._003C_003E4__this;
										if (uILeaderboardEntry3.Image_Map != null)
										{
											UILeaderboardEntry uILeaderboardEntry4 = _003C_003E8__1._003C_003E4__this;
											uILeaderboardEntry4.Image_Map.texture = texture2D;
										}
									}
									int pending = _003C_003E8__1.pending - 1;
									_003C_003E8__1.pending = pending;
								};
								_003C_003Ec__DisplayClass37_0 CS_0024_003C_003E8__locals13 = new _003C_003Ec__DisplayClass37_0();
								if (CS_0024_003C_003E8__locals13 != null)
								{
									CS_0024_003C_003E8__locals13._003C_003E4__this = _003C_003E4__this;
									CS_0024_003C_003E8__locals13.version = version;
									CS_0024_003C_003E8__locals13.onComplete = onComplete;
									CS_0024_003C_003E8__locals13.handle = null;
									TimeSpan cacheMaxAge = TimeSpan.FromDays(uILeaderboardEntry.CacheMaxAgeDays);
									Action<Texture2D> action = delegate
									{
										UILeaderboardEntry uILeaderboardEntry2 = CS_0024_003C_003E8__locals13._003C_003E4__this;
										bool flag2 = uILeaderboardEntry2.activeTextureRequests.Remove(CS_0024_003C_003E8__locals13.handle);
										UILeaderboardEntry uILeaderboardEntry3 = CS_0024_003C_003E8__locals13._003C_003E4__this;
										if (CS_0024_003C_003E8__locals13.version == uILeaderboardEntry3.mapLoadVersion)
										{
											Action<Texture2D> onComplete3 = CS_0024_003C_003E8__locals13.onComplete;
											if (CS_0024_003C_003E8__locals13.onComplete != null)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v126 @ rcx_v5 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
											}
										}
									};
									Action<Texture2D> onComplete2 = default(Action<Texture2D>);
									LeaderboardImageThrottle.TextureRequest handle = LeaderboardImageThrottle.RequestUrlTexture(leaderboardEntryResponse4._003CImageUrl_003Ek__BackingField, priority: true, cacheMaxAge, uILeaderboardEntry.RequestTimeoutSeconds, onComplete2);
									CS_0024_003C_003E8__locals13.handle = handle;
									if (uILeaderboardEntry.activeTextureRequests != null)
									{
										uILeaderboardEntry.activeTextureRequests.Add(CS_0024_003C_003E8__locals13.handle);
										goto IL_0368;
									}
								}
							}
						}
					}
				}
			}
			goto IL_075f;
			IL_0368:
			_003C_003Ec__DisplayClass34_0 obj7 = _003C_003E8__1;
			if (_003C_003E8__1 == null || (object)_003C_003E4__this == null)
			{
				goto IL_075f;
			}
			if (obj7.pending <= 0)
			{
				goto IL_0412;
			}
			if (version != uILeaderboardEntry.mapLoadVersion)
			{
				goto IL_06c4;
			}
			_003C_003E2__current = null;
			_003C_003E1__state = 2;
			goto IL_07f3;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private sealed class _003CLoadZipFramesRoutine_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UILeaderboardEntry _003C_003E4__this;

		public int version;

		public string zipUrl;

		private _003C_003Ec__DisplayClass35_0 _003C_003E8__1;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CLoadZipFramesRoutine_003Ed__35(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_026f: Expected I4, but got I8
			//IL_0325: Expected I4, but got O
			UILeaderboardEntry uILeaderboardEntry = _003C_003E4__this;
			object obj8;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003Ec__DisplayClass35_0 obj = new _003C_003Ec__DisplayClass35_0();
				_003C_003E8__1 = obj;
				_003C_003Ec__DisplayClass35_0 obj2 = _003C_003E8__1;
				if (_003C_003E8__1 != null)
				{
					obj2._003C_003E4__this = _003C_003E4__this;
					_003C_003Ec__DisplayClass35_0 obj3 = _003C_003E8__1;
					if (_003C_003E8__1 != null)
					{
						obj3.version = version;
						_003C_003Ec__DisplayClass35_0 obj4 = _003C_003E8__1;
						if (_003C_003E8__1 != null)
						{
							obj4.pending = 1;
							_003C_003Ec__DisplayClass35_0 obj5 = _003C_003E8__1;
							if (_003C_003E8__1 != null)
							{
								obj5.handle = null;
								_003C_003Ec__DisplayClass35_0 obj6 = _003C_003E8__1;
								if ((object)_003C_003E4__this != null)
								{
									TimeSpan cacheMaxAge = TimeSpan.FromDays(uILeaderboardEntry.CacheMaxAgeDays);
									Action<Texture2D> onPreview = delegate(Texture2D preview)
									{
										UILeaderboardEntry uILeaderboardEntry2 = _003C_003E8__1._003C_003E4__this;
										if (_003C_003E8__1.version == uILeaderboardEntry2.mapLoadVersion && preview != null)
										{
											UILeaderboardEntry uILeaderboardEntry3 = _003C_003E8__1._003C_003E4__this;
											if (uILeaderboardEntry3.Image_Map != null)
											{
												UILeaderboardEntry uILeaderboardEntry4 = _003C_003E8__1._003C_003E4__this;
												uILeaderboardEntry4.Image_Map.texture = preview;
											}
										}
									};
									Action<Texture2D[]> action = delegate(Texture2D[] frames)
									{
										//IL_009b: Expected O, but got I
										//IL_013a: Unknown result type (might be due to invalid IL or missing references)
										//IL_013f: Expected O, but got Unknown
										//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
										//IL_01bb: Expected O, but got Unknown
										UILeaderboardEntry uILeaderboardEntry2 = _003C_003E8__1._003C_003E4__this;
										bool flag = uILeaderboardEntry2.activeZipRequests.Remove(_003C_003E8__1.handle);
										UILeaderboardEntry uILeaderboardEntry3 = _003C_003E8__1._003C_003E4__this;
										if (_003C_003E8__1.version == uILeaderboardEntry3.mapLoadVersion)
										{
											UILeaderboardEntry uILeaderboardEntry4 = _003C_003E8__1._003C_003E4__this;
											List<Texture2D> mapTextures = uILeaderboardEntry4.mapTextures;
											int num = mapTextures._version + 1;
											mapTextures._version = num;
											if (!((List<LeaderboardImageThrottle.ZipFramesRequest>)0).Remove(_003C_003E8__1.handle))
											{
												mapTextures._size = 0;
											}
											else
											{
												mapTextures._size = 0;
												if (mapTextures._size > 0)
												{
													Array.Clear(mapTextures._items, 0, mapTextures._size);
												}
											}
											if (frames != null)
											{
												object obj10 = frames + 32;
												int num2 = 0;
												while (num2 < frames.Length)
												{
													if ((UnityEngine.Object)obj10 != null)
													{
														UILeaderboardEntry uILeaderboardEntry5 = _003C_003E8__1._003C_003E4__this;
														uILeaderboardEntry5.mapTextures.Add((Texture2D)obj10);
													}
													num2++;
													obj10 += 8;
												}
											}
										}
										int pending = _003C_003E8__1.pending - 1;
										_003C_003E8__1.pending = pending;
									};
									Action<Texture2D[]> onComplete = default(Action<Texture2D[]>);
									LeaderboardImageThrottle.ZipFramesRequest handle = LeaderboardImageThrottle.RequestZipFrameTextures(zipUrl, cacheMaxAge, uILeaderboardEntry.RequestTimeoutSeconds, onPreview, onComplete);
									if (_003C_003E8__1 != null)
									{
										obj6.handle = handle;
										_003C_003Ec__DisplayClass35_0 obj7 = _003C_003E8__1;
										if (_003C_003E8__1 != null && uILeaderboardEntry.activeZipRequests != null)
										{
											uILeaderboardEntry.activeZipRequests.Add(obj7.handle);
											obj8 = null;
											goto IL_0351;
										}
									}
								}
							}
						}
					}
				}
				goto IL_0317;
			}
			if (_003C_003E1__state != 1)
			{
				goto IL_0309;
			}
			_003C_003E1__state = -1;
			obj8 = null;
			goto IL_0351;
			IL_0317:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0309:
			return false;
			IL_0351:
			_003C_003Ec__DisplayClass35_0 obj9 = _003C_003E8__1;
			if (_003C_003E8__1 != null)
			{
				if (obj9.pending > 0)
				{
					if ((object)_003C_003E4__this == null)
					{
						goto IL_0317;
					}
					if (obj9.version == uILeaderboardEntry.mapLoadVersion)
					{
						_003C_003E2__current = obj8;
						_003C_003E1__state = 1;
						return true;
					}
				}
				goto IL_0309;
			}
			goto IL_0317;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public TMP_Text Text_Position;

	public TMP_Text Text_Name;

	public TMP_Text Text_Description;

	public RawImage Image_ProfileIcon;

	public RawImage Image_Map;

	public float MapImageCycleInterval = 0.333f;

	public bool CycleMapImages = true;

	public float CacheMaxAgeDays = 2f;

	public bool CleanCacheOnAwake = true;

	public int RequestTimeoutSeconds = 15;

	public int GlobalMaxConcurrentDownloads = 1;

	public int GlobalMaxTextureCreatesPerFrame = 1;

	public int GlobalMaxActiveImageJobs = 4;

	public float GlobalDownloadStartSpacing = 0.15f;

	public float GlobalTextureCreateSpacing = 0.05f;

	public LeaderboardEntryResponse Entry;

	private readonly List<Texture2D> mapTextures;

	private readonly List<LeaderboardImageThrottle.TextureRequest> activeTextureRequests;

	private readonly List<LeaderboardImageThrottle.ZipFramesRequest> activeZipRequests;

	private Coroutine mapLoadRoutine;

	private Coroutine mapCycleRoutine;

	private Texture2D profileTexture;

	private int mapLoadVersion;

	private static Task cacheCleanupTask;

	private void Awake()
	{
		//IL_0221: Invalid comparison between I4 and F4
		//IL_0233: Expected F4, but got I4
		//IL_01d0: Invalid comparison between I4 and F4
		//IL_01e2: Expected F4, but got I4
		int maxConcurrentDownloads = GlobalMaxConcurrentDownloads;
		int num = GlobalMaxTextureCreatesPerFrame;
		int num2 = GlobalMaxActiveImageJobs;
		if (GlobalMaxConcurrentDownloads < 1)
		{
			maxConcurrentDownloads = 1;
		}
		if (num < 1)
		{
			num = 1;
		}
		LeaderboardImageThrottle.MaxConcurrentDownloads = maxConcurrentDownloads;
		if (num2 < 1)
		{
			num2 = 1;
		}
		LeaderboardImageThrottle.MaxTextureCreatesPerFrame = num;
		LeaderboardImageThrottle.MaxActiveJobs = num2;
		bool flag = !(0f < GlobalDownloadStartSpacing);
		float downloadStartSpacing = 0f;
		if (!flag)
		{
			downloadStartSpacing = GlobalDownloadStartSpacing;
		}
		LeaderboardImageThrottle.DownloadStartSpacing = downloadStartSpacing;
		bool flag2 = !(0f < GlobalTextureCreateSpacing);
		float textureCreateSpacing = 0f;
		if (!flag2)
		{
			textureCreateSpacing = GlobalTextureCreateSpacing;
		}
		LeaderboardImageThrottle.TextureCreateSpacing = textureCreateSpacing;
		LeaderboardImageThrottle.EnsureRunner();
		if (!CleanCacheOnAwake)
		{
			return;
		}
		TimeSpan maxAge = TimeSpan.FromDays(CacheMaxAgeDays);
		_003C_003Ec__DisplayClass45_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass45_0();
		CS_0024_003C_003E8__locals6.maxAge = maxAge;
		if (cacheCleanupTask != null && !cacheCleanupTask.IsCompleted)
		{
			return;
		}
		string cacheFile = LeaderboardImageThrottle.GetCacheFile("cache_test");
		string directoryName = Path.GetDirectoryName(cacheFile);
		CS_0024_003C_003E8__locals6.dir = directoryName;
		Action action = delegate
		{
			//IL_0070: Expected O, but got I4
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Expected O, but got Unknown
			if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals6.dir) && Directory.Exists(CS_0024_003C_003E8__locals6.dir))
			{
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = utcNow - CS_0024_003C_003E8__locals6.maxAge;
				string[] files = Directory.GetFiles(CS_0024_003C_003E8__locals6.dir, "*.img");
				object obj = 0;
				while ((nint)obj < files.Length)
				{
					DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(files[obj]);
					if (lastWriteTimeUtc < dateTime)
					{
						File.Delete(files[obj]);
					}
					obj++;
				}
			}
		};
		Task task = Task.Run(action);
		cacheCleanupTask = task;
	}

	private void OnDisable()
	{
		StopMapLoading();
		if (mapCycleRoutine != null)
		{
			StopCoroutine(mapCycleRoutine);
			mapCycleRoutine = null;
		}
	}

	private void OnDestroy()
	{
		StopMapLoading();
		if (mapCycleRoutine != null)
		{
			StopCoroutine(mapCycleRoutine);
			mapCycleRoutine = null;
		}
		ClearMapTextures();
		ClearProfileTexture();
	}

	public static void UnloadUnusedCachedTextures(IEnumerable<string> imageUrls, IEnumerable<string> avatarBase64s = null, IEnumerable<string> zipUrls = null)
	{
		LeaderboardImageThrottle.UnloadUnusedCachedTextures(imageUrls, avatarBase64s, zipUrls);
	}

	public unsafe static void UnloadUnusedCachedTexturesForEntries(IEnumerable<LeaderboardEntryResponse> entries)
	{
		//IL_004c: Expected O, but got Ref
		//IL_00a7: Expected I, but got O
		//IL_0132: Expected O, but got I4
		//IL_043a: Expected I, but got O
		//IL_00df: Expected O, but got I
		//IL_00e8: Expected O, but got I4
		//IL_0150: Expected O, but got I
		//IL_02bd: Expected O, but got I
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_018c: Expected O, but got I
		//IL_019d: Expected O, but got I
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_01f6: Expected O, but got I
		//IL_0207: Expected O, but got I
		//IL_0177: Expected O, but got I
		//IL_0235: Expected O, but got I
		//IL_0247: Expected I, but got O
		//IL_030f: Expected O, but got I
		//IL_031d: Expected I, but got O
		//IL_01e1: Expected O, but got I
		//IL_0287: Expected O, but got I
		//IL_0295: Expected I, but got O
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		list2._002Ector();
		List<string> list3 = new List<string>();
		if (entries != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			string text = default(string);
			object obj = (object)(&text);
			string text2 = null;
			object obj2 = default(object);
			object obj11 = default(object);
			object obj12 = default(object);
			while (true)
			{
				object obj3;
				object obj10;
				if (text != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 == null)
					{
						break;
					}
					bool flag = text == null;
					text2 = null;
					if (!flag)
					{
						nint num = (nint)text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ r10_v5 (Il2CppClass<System.String>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_011f;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ r10_v5 (Il2CppClass<System.String>)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r8_v10+v372 @ rcx_v37*8]");
							if (0 == (nint)typeof(IEnumerator<LeaderboardEntryResponse>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ r10_v5 (Il2CppClass<System.String>)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_011f;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ r8_v10+8+v426 @ rcx_v39*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + num;
						goto IL_0413;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_011f:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj3 = 0;
				obj10 = obj11;
				goto IL_0413;
				IL_0413:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v431 @ rdx_v14] (should have been resolved before IL gen)");
				bool flag2 = obj12 == null;
				nint num2 = (nint)typeof(IEnumerator<LeaderboardEntryResponse>);
				if (flag2)
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+40]");
				if (!string.IsNullOrWhiteSpace((string)0))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+40]");
					list.Add((string)0);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+30]");
				text2 = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+30]");
				if (!string.IsNullOrWhiteSpace((string)0))
				{
					if (list2 == null)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+30]");
					list2.Add((string)0);
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+50]");
				text2 = (string)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+50]");
				if (string.IsNullOrWhiteSpace((string)0))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+68]");
					bool flag3 = string.IsNullOrWhiteSpace((string)0);
					num2 = (nint)typeof(IEnumerator<LeaderboardEntryResponse>);
					if (!flag3)
					{
						if (list3 == null)
						{
							throw new NullReferenceException();
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+68]");
						list3.Add((string)0);
						num2 = (nint)typeof(IEnumerator<LeaderboardEntryResponse>);
					}
				}
				else
				{
					if (list3 == null)
					{
						throw new NullReferenceException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v25+50]");
					list3.Add((string)0);
					num2 = (nint)typeof(IEnumerator<LeaderboardEntryResponse>);
				}
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		LeaderboardImageThrottle.UnloadUnusedCachedTextures(list, list2, list3);
	}

	public static void ClearMemoryCache()
	{
		LeaderboardImageThrottle.ClearMemoryCache();
	}

	public void Init(int index, LeaderboardEntryResponse entry)
	{
		Entry = entry;
		if (Text_Position != null)
		{
			int num = index + 1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = $"#{arg}";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
			int num2 = num;
		}
		if (Text_Name != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
		}
		if (Text_Description != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj = default(object);
			object arg2 = (DateTime)obj;
			object arg3 = default(object);
			string text2 = $"Points: {arg3} @ ({arg2:yyyy-MM-dd HH:mm})";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
			int num2 = entry._003CScore_003Ek__BackingField;
		}
		ClearProfileTexture();
		if (Image_ProfileIcon != null)
		{
			Image_ProfileIcon.texture = null;
			if (!string.IsNullOrWhiteSpace(entry._003CAvatarBase64_003Ek__BackingField))
			{
				Texture2D texture2D = LoadProfileTextureImmediate(entry._003CAvatarBase64_003Ek__BackingField);
				profileTexture = texture2D;
				if (profileTexture != null)
				{
					Image_ProfileIcon.texture = profileTexture;
				}
			}
		}
		StopMapLoading();
		if (mapCycleRoutine != null)
		{
			StopCoroutine(mapCycleRoutine);
			mapCycleRoutine = null;
		}
		ClearMapTextures();
		if (Image_Map != null)
		{
			Image_Map.texture = null;
			int version = ++mapLoadVersion;
			_003CLoadMapImagesRoutine_003Ed__34 obj2 = new _003CLoadMapImagesRoutine_003Ed__34(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = this;
			obj2.entry = entry;
			obj2.version = version;
			Coroutine coroutine = StartCoroutine(obj2);
			mapLoadRoutine = coroutine;
		}
	}

	public void InitLocal(int index, LeaderboardEntryResponse entry)
	{
		Entry = entry;
		if (Text_Position != null)
		{
			int num = index + 1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			string text = $"#{arg}";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
			int num2 = num;
		}
		if (Text_Name != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
		}
		if (Text_Description != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj = default(object);
			object arg2 = (DateTime)obj;
			object arg3 = default(object);
			string text2 = $"Points: {arg3} @ ({arg2:yyyy-MM-dd HH:mm}) [Local]";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181CA2AE0");
			int num2 = entry._003CScore_003Ek__BackingField;
		}
		StopMapLoading();
		if (mapCycleRoutine != null)
		{
			StopCoroutine(mapCycleRoutine);
			mapCycleRoutine = null;
		}
		ClearMapTextures();
		ClearProfileTexture();
		if (Image_ProfileIcon != null)
		{
			Image_ProfileIcon.texture = null;
		}
		if (Image_Map != null)
		{
			Image_Map.texture = null;
		}
		if (!string.IsNullOrWhiteSpace(entry._003CLocalReplayPath_003Ek__BackingField))
		{
			int version = ++mapLoadVersion;
			_003CLoadLocalZipFramesRoutine_003Ed__36 obj2 = new _003CLoadLocalZipFramesRoutine_003Ed__36(0);
			obj2._003C_003E1__state = 0;
			obj2._003C_003E4__this = this;
			obj2.replayPath = entry._003CLocalReplayPath_003Ek__BackingField;
			obj2.version = version;
			Coroutine coroutine = StartCoroutine(obj2);
			mapLoadRoutine = coroutine;
		}
	}

	private void SetupProfileImage(LeaderboardEntryResponse entry)
	{
		ClearProfileTexture();
		if (!(Image_ProfileIcon != null))
		{
			return;
		}
		Image_ProfileIcon.texture = null;
		if (!string.IsNullOrWhiteSpace(entry._003CAvatarBase64_003Ek__BackingField))
		{
			Texture2D texture2D = LoadProfileTextureImmediate(entry._003CAvatarBase64_003Ek__BackingField);
			profileTexture = texture2D;
			if (profileTexture != null)
			{
				Image_ProfileIcon.texture = profileTexture;
			}
		}
	}

	private void SetupMapImages(LeaderboardEntryResponse entry)
	{
		StopMapLoading();
		if (mapCycleRoutine != null)
		{
			StopCoroutine(mapCycleRoutine);
			mapCycleRoutine = null;
		}
		ClearMapTextures();
		if (Image_Map != null)
		{
			Image_Map.texture = null;
			int version = ++mapLoadVersion;
			_003CLoadMapImagesRoutine_003Ed__34 obj = new _003CLoadMapImagesRoutine_003Ed__34(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			obj.entry = entry;
			obj.version = version;
			Coroutine coroutine = StartCoroutine(obj);
			mapLoadRoutine = coroutine;
		}
	}

	private IEnumerator LoadMapImagesRoutine(LeaderboardEntryResponse entry, int version)
	{
		_003CLoadMapImagesRoutine_003Ed__34 obj = new _003CLoadMapImagesRoutine_003Ed__34(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.entry = entry;
		obj.version = version;
		return obj;
	}

	private IEnumerator LoadZipFramesRoutine(string zipUrl, int version)
	{
		_003CLoadZipFramesRoutine_003Ed__35 obj = new _003CLoadZipFramesRoutine_003Ed__35(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.zipUrl = zipUrl;
		obj.version = version;
		return obj;
	}

	private IEnumerator LoadLocalZipFramesRoutine(string replayPath, int version)
	{
		_003CLoadLocalZipFramesRoutine_003Ed__36 obj = new _003CLoadLocalZipFramesRoutine_003Ed__36(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.replayPath = replayPath;
		obj.version = version;
		return obj;
	}

	private void QueueTextureRequest(string url, bool priority, int version, Action<Texture2D> onComplete)
	{
		_003C_003Ec__DisplayClass37_0 CS_0024_003C_003E8__locals12 = new _003C_003Ec__DisplayClass37_0();
		CS_0024_003C_003E8__locals12._003C_003E4__this = this;
		Action<Texture2D> onComplete2 = default(Action<Texture2D>);
		CS_0024_003C_003E8__locals12.onComplete = onComplete2;
		CS_0024_003C_003E8__locals12.version = version;
		CS_0024_003C_003E8__locals12.handle = null;
		TimeSpan cacheMaxAge = TimeSpan.FromDays(CacheMaxAgeDays);
		Action<Texture2D> action = delegate
		{
			UILeaderboardEntry uILeaderboardEntry = CS_0024_003C_003E8__locals12._003C_003E4__this;
			bool flag = uILeaderboardEntry.activeTextureRequests.Remove(CS_0024_003C_003E8__locals12.handle);
			UILeaderboardEntry uILeaderboardEntry2 = CS_0024_003C_003E8__locals12._003C_003E4__this;
			if (CS_0024_003C_003E8__locals12.version == uILeaderboardEntry2.mapLoadVersion)
			{
				Action<Texture2D> onComplete4 = CS_0024_003C_003E8__locals12.onComplete;
				if (CS_0024_003C_003E8__locals12.onComplete != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v126 @ rcx_v5 (System.Action`1<UnityEngine.Texture2D>)+18] (should have been resolved before IL gen)");
				}
			}
		};
		Action<Texture2D> onComplete3 = default(Action<Texture2D>);
		LeaderboardImageThrottle.TextureRequest handle = LeaderboardImageThrottle.RequestUrlTexture(url, priority, cacheMaxAge, RequestTimeoutSeconds, onComplete3);
		CS_0024_003C_003E8__locals12.handle = handle;
		activeTextureRequests.Add(CS_0024_003C_003E8__locals12.handle);
	}

	private IEnumerator CycleMapImagesRoutine()
	{
		_003CCycleMapImagesRoutine_003Ed__38 obj = new _003CCycleMapImagesRoutine_003Ed__38(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void StopMapLoading()
	{
		//IL_0309: Expected O, but got Ref
		//IL_00ec: Expected I4, but got O
		//IL_00da: Expected I4, but got O
		//IL_036b: Expected O, but got Ref
		//IL_020c: Expected I4, but got O
		//IL_01fa: Expected I4, but got O
		int num = mapLoadVersion + 1;
		mapLoadVersion = num;
		Coroutine coroutine;
		if (mapLoadRoutine != null)
		{
			StopCoroutine(mapLoadRoutine);
			mapLoadRoutine = null;
			coroutine = null;
		}
		else
		{
			coroutine = null;
		}
		List<LeaderboardImageThrottle.TextureRequest>.Enumerator enumerator = (List<LeaderboardImageThrottle.TextureRequest>.Enumerator)activeTextureRequests;
		if (activeTextureRequests != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<LeaderboardImageThrottle.TextureRequest>.Enumerator enumerator2 = default(List<LeaderboardImageThrottle.TextureRequest>.Enumerator);
			LeaderboardImageThrottle.TextureRequest textureRequest = default(LeaderboardImageThrottle.TextureRequest);
			while (enumerator2.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				textureRequest?.Cancel();
			}
			enumerator2.Dispose();
			List<LeaderboardImageThrottle.TextureRequest> list = activeTextureRequests;
			bool flag = activeTextureRequests == null;
			enumerator = (List<LeaderboardImageThrottle.TextureRequest>.Enumerator)(&enumerator2);
			if (!flag)
			{
				int version = list._version + 1;
				list._version = version;
				((List<LeaderboardImageThrottle.TextureRequest>.Enumerator*)null)->Dispose();
				object obj = default(object);
				if (obj == null)
				{
					list._size = (int)coroutine;
				}
				else
				{
					list._size = (int)coroutine;
					if (list._size > 0)
					{
						Array.Clear(list._items, 0, list._size);
					}
				}
				if (activeZipRequests != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					List<LeaderboardImageThrottle.ZipFramesRequest>.Enumerator enumerator3 = default(List<LeaderboardImageThrottle.ZipFramesRequest>.Enumerator);
					LeaderboardImageThrottle.ZipFramesRequest zipFramesRequest = default(LeaderboardImageThrottle.ZipFramesRequest);
					while (enumerator3.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						zipFramesRequest?.Cancel();
					}
					enumerator3.Dispose();
					List<LeaderboardImageThrottle.ZipFramesRequest> list2 = activeZipRequests;
					bool flag2 = activeZipRequests == null;
					enumerator = (List<LeaderboardImageThrottle.TextureRequest>.Enumerator)(&enumerator3);
					if (!flag2)
					{
						int version2 = list2._version + 1;
						list2._version = version2;
						((List<LeaderboardImageThrottle.ZipFramesRequest>.Enumerator*)null)->Dispose();
						object obj2 = default(object);
						if (obj2 == null)
						{
							list2._size = (int)coroutine;
							return;
						}
						list2._size = (int)coroutine;
						if (list2._size > 0)
						{
							Array.Clear(list2._items, 0, list2._size);
						}
						return;
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private void StopMapCycle()
	{
		if (mapCycleRoutine != null)
		{
			StopCoroutine(mapCycleRoutine);
			mapCycleRoutine = null;
		}
	}

	private void ClearMapTextures()
	{
		if (Image_Map != null)
		{
			Image_Map.texture = null;
		}
		List<Texture2D> list = mapTextures;
		int version = list._version + 1;
		list._version = version;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<Texture2D>())
		{
			list._size = 0;
			return;
		}
		list._size = 0;
		if (list._size > 0)
		{
			Array.Clear(list._items, 0, list._size);
		}
	}

	private void ClearProfileTexture()
	{
		if (Image_ProfileIcon != null)
		{
			RawImage image_ProfileIcon = Image_ProfileIcon;
			if (image_ProfileIcon.m_Texture == profileTexture)
			{
				Image_ProfileIcon.texture = null;
			}
		}
		profileTexture = null;
	}

	private bool IsMapLoadValid(int version)
	{
		//IL_000f: Expected O, but got I4
		object obj = version - mapLoadVersion;
		return obj == null;
	}

	private static Texture2D LoadProfileTextureImmediate(string base64)
	{
		if (!string.IsNullOrWhiteSpace(base64))
		{
			string s = LeaderboardImageThrottle.NormalizeBase64Payload(base64);
			byte[] bytes = Convert.FromBase64String(s);
			return LeaderboardImageThrottle.CreateTexture(bytes, markNonReadable: false);
		}
		return null;
	}

	private static void CleanupImageCacheOlderThan(TimeSpan maxAge)
	{
		_003C_003Ec__DisplayClass45_0 CS_0024_003C_003E8__locals6 = new _003C_003Ec__DisplayClass45_0();
		CS_0024_003C_003E8__locals6.maxAge = maxAge;
		if (cacheCleanupTask != null && !cacheCleanupTask.IsCompleted)
		{
			return;
		}
		string cacheFile = LeaderboardImageThrottle.GetCacheFile("cache_test");
		string directoryName = Path.GetDirectoryName(cacheFile);
		CS_0024_003C_003E8__locals6.dir = directoryName;
		Action action = delegate
		{
			//IL_0070: Expected O, but got I4
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Expected O, but got Unknown
			if (!string.IsNullOrEmpty(CS_0024_003C_003E8__locals6.dir) && Directory.Exists(CS_0024_003C_003E8__locals6.dir))
			{
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = utcNow - CS_0024_003C_003E8__locals6.maxAge;
				string[] files = Directory.GetFiles(CS_0024_003C_003E8__locals6.dir, "*.img");
				object obj = 0;
				while ((nint)obj < files.Length)
				{
					DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(files[obj]);
					if (lastWriteTimeUtc < dateTime)
					{
						File.Delete(files[obj]);
					}
					obj++;
				}
			}
		};
		Task task = Task.Run(action);
		cacheCleanupTask = task;
	}

	public UILeaderboardEntry()
	{
		List<Texture2D> list = new List<Texture2D>();
		mapTextures = list;
		activeTextureRequests = new List<LeaderboardImageThrottle.TextureRequest>();
		activeZipRequests = new List<LeaderboardImageThrottle.ZipFramesRequest>();
		base._002Ector();
	}
}
