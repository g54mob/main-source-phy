using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BitCode.Collections
{
	public class RingBuffer<T> : IEnumerable, IList<T>, ICollection<T>, IEnumerable<T>
	{
		private struct nEEhUmuJidovEpSwtWDAuzHBIvtc<_0002> : IDisposable, IEnumerator<_0002>, IEnumerator
		{
			private readonly uint yLQZFAXXHKPpRjYlDwcMyhYVhMeC;

			private readonly RingBuffer<_0002> rZKAWAthtGScTPcLspnVRctHaZCw;

			private int nznMDOjTVRaFYmwVgRodRWJflUrI;

			private bool tlRiOzSchvnYldbxynOFisSYraBV;

			public _0002 Current
			{
				get
				{
					UrWwwkEVqlsCwuqAxyNaOnyUzodO();
					while (true)
					{
						int num = 290451414;
						while (true)
						{
							uint num2;
							switch ((num2 = (uint)(num ^ 0x1BDFD5C8)) % 6)
							{
							case 0u:
								break;
							case 3u:
							{
								int num5;
								int num6;
								if (nznMDOjTVRaFYmwVgRodRWJflUrI < rZKAWAthtGScTPcLspnVRctHaZCw.Count)
								{
									num5 = 932139916;
									num6 = num5;
								}
								else
								{
									num5 = 1046825020;
									num6 = num5;
								}
								num = num5 ^ (int)(num2 * 2112405369);
								continue;
							}
							case 1u:
								throw new InvalidOperationException();
							case 2u:
								hOWLyRzRRIzEQdjejWkUddoAiLwf();
								num = (int)(num2 * 1420914238) ^ -32581982;
								continue;
							case 4u:
							{
								int num3;
								int num4;
								if (nznMDOjTVRaFYmwVgRodRWJflUrI >= 0)
								{
									num3 = 1148748245;
									num4 = num3;
								}
								else
								{
									num3 = 1956027897;
									num4 = num3;
								}
								num = num3 ^ ((int)num2 * -1627172252);
								continue;
							}
							default:
								return rZKAWAthtGScTPcLspnVRctHaZCw[nznMDOjTVRaFYmwVgRodRWJflUrI];
							}
							break;
						}
					}
				}
			}

			object IEnumerator.Current => Current;

			public nEEhUmuJidovEpSwtWDAuzHBIvtc(RingBuffer<_0002> P_0)
			{
				rZKAWAthtGScTPcLspnVRctHaZCw = P_0;
				yLQZFAXXHKPpRjYlDwcMyhYVhMeC = rZKAWAthtGScTPcLspnVRctHaZCw.acyKSgdXdwbDeVjeukgvZsJzMDVR;
				nznMDOjTVRaFYmwVgRodRWJflUrI = -1;
				tlRiOzSchvnYldbxynOFisSYraBV = false;
			}

			public bool MoveNext()
			{
				UrWwwkEVqlsCwuqAxyNaOnyUzodO();
				nznMDOjTVRaFYmwVgRodRWJflUrI++;
				return nznMDOjTVRaFYmwVgRodRWJflUrI < rZKAWAthtGScTPcLspnVRctHaZCw.Count;
			}

			public void Reset()
			{
				UrWwwkEVqlsCwuqAxyNaOnyUzodO();
				nznMDOjTVRaFYmwVgRodRWJflUrI = -1;
			}

			public void Dispose()
			{
				tlRiOzSchvnYldbxynOFisSYraBV = true;
			}

			private void hOWLyRzRRIzEQdjejWkUddoAiLwf()
			{
				if (yLQZFAXXHKPpRjYlDwcMyhYVhMeC == rZKAWAthtGScTPcLspnVRctHaZCw.acyKSgdXdwbDeVjeukgvZsJzMDVR)
				{
					return;
				}
				while (true)
				{
					uint num;
					switch ((num = 1193996521u) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						throw new InvalidOperationException("Underlying collection has changed.");
					case 0u:
						return;
					}
				}
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
					switch ((num = 789247493u) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						throw new ObjectDisposedException(GetType().Name);
					case 1u:
						return;
					}
				}
			}
		}

		private uint acyKSgdXdwbDeVjeukgvZsJzMDVR;

		private int HhAQwfUXfYRNwTlXjrnsPEBkCwnR;

		private int TgAzEwfALmJSWbSmjvJgUtqbLEFX;

		private T[] ofHBnmbKsBdEfOfompRScgChwotbB;

		[CompilerGenerated]
		private int MSWGLkSfumHlOHqDPcpMUHgbtXSD;

		public int Count
		{
			[CompilerGenerated]
			get
			{
				return MSWGLkSfumHlOHqDPcpMUHgbtXSD;
			}
			[CompilerGenerated]
			private set
			{
				MSWGLkSfumHlOHqDPcpMUHgbtXSD = mSWGLkSfumHlOHqDPcpMUHgbtXSD;
			}
		}

		public int Capacity => ofHBnmbKsBdEfOfompRScgChwotbB.Length;

		public bool IsFull => Count == Capacity;

		public T Head
		{
			get
			{
				VxbNUIumBjCpvzGVwWMaudqvSZPP();
				return ofHBnmbKsBdEfOfompRScgChwotbB[HhAQwfUXfYRNwTlXjrnsPEBkCwnR];
			}
		}

		public T Tail
		{
			get
			{
				VxbNUIumBjCpvzGVwWMaudqvSZPP();
				int num = TgAzEwfALmJSWbSmjvJgUtqbLEFX - 1;
				if (num < 0)
				{
					while (true)
					{
						int num2 = 2097925122;
						while (true)
						{
							uint num3;
							switch ((num3 = (uint)(num2 ^ 0x7E1C98BE)) % 3)
							{
							case 2u:
								break;
							case 1u:
								num += ofHBnmbKsBdEfOfompRScgChwotbB.Length;
								num2 = ((int)num3 * -700125895) ^ -247082389;
								continue;
							default:
								goto end_IL_0013;
							}
							break;
						}
						continue;
						end_IL_0013:
						break;
					}
				}
				return ofHBnmbKsBdEfOfompRScgChwotbB[num];
			}
		}

		public T this[int index]
		{
			get
			{
				if (index < 0)
				{
					goto IL_0004;
				}
				goto IL_0053;
				IL_0004:
				int num = -1891906013;
				goto IL_0009;
				IL_0009:
				uint num2;
				switch ((num2 = (uint)(num ^ -814427568)) % 5)
				{
				case 3u:
					break;
				case 1u:
					throw new IndexOutOfRangeException($"Provided index ({index}) cannot be negative.");
				case 4u:
					goto IL_0053;
				case 0u:
					throw new IndexOutOfRangeException($"Index {index} is out of range. Buffer contains {Count} items.");
				default:
					return ofHBnmbKsBdEfOfompRScgChwotbB[HgFZBCNBBpIndarWNvDvScpMaJAZA(index)];
				}
				goto IL_0004;
				IL_0053:
				int num3;
				if (index >= Count)
				{
					num = -684984151;
					num3 = num;
				}
				else
				{
					num = -171841224;
					num3 = num;
				}
				goto IL_0009;
			}
			set
			{
				if (index < 0)
				{
					goto IL_0007;
				}
				goto IL_0094;
				IL_0007:
				int num = 1655489270;
				goto IL_000c;
				IL_000c:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5FCF47DD)) % 7)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						throw new IndexOutOfRangeException($"Provided index ({index}) cannot be negative.");
					case 3u:
						throw new IndexOutOfRangeException($"Index {index} is out of range. Buffer contains {Count} items.");
					case 0u:
						goto IL_0094;
					case 5u:
						ofHBnmbKsBdEfOfompRScgChwotbB[HgFZBCNBBpIndarWNvDvScpMaJAZA(index)] = value;
						num = 1249629349;
						continue;
					case 4u:
						rkfGkphndiWKXAnfLWMUCduvcgcR();
						num = (int)((num2 * 1735296841) ^ 0x57A01847);
						continue;
					case 6u:
						return;
					}
					break;
				}
				goto IL_0007;
				IL_0094:
				int num3;
				if (index < Count)
				{
					num = 364461248;
					num3 = num;
				}
				else
				{
					num = 213068703;
					num3 = num;
				}
				goto IL_000c;
			}
		}

		bool ICollection<T>.IsReadOnly => false;

		public RingBuffer(int capacity)
		{
			while (true)
			{
				int num = 1896434884;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x1B70359B)) % 6)
					{
					case 0u:
						break;
					case 1u:
						HhAQwfUXfYRNwTlXjrnsPEBkCwnR = 0;
						num = ((int)num2 * -1546704236) ^ -1303704883;
						continue;
					case 3u:
						throw new ArgumentOutOfRangeException("capacity", "Ring buffer cannot have zero or negative capacity.");
					case 4u:
						ofHBnmbKsBdEfOfompRScgChwotbB = new T[capacity];
						num = 622861424;
						continue;
					case 5u:
					{
						int num3;
						int num4;
						if (capacity > 0)
						{
							num3 = -299814869;
							num4 = num3;
						}
						else
						{
							num3 = -1912623252;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -11858350);
						continue;
					}
					default:
						Count = 0;
						TgAzEwfALmJSWbSmjvJgUtqbLEFX = 0;
						return;
					}
					break;
				}
			}
		}

		public RingBuffer(IEnumerable<T> startingItems)
		{
			ofHBnmbKsBdEfOfompRScgChwotbB = startingItems.ToArray();
			HhAQwfUXfYRNwTlXjrnsPEBkCwnR = 0;
			Count = ofHBnmbKsBdEfOfompRScgChwotbB.Length;
			TgAzEwfALmJSWbSmjvJgUtqbLEFX = 0;
		}

		public RingBuffer(IEnumerable<T> startingItems, int capacity)
		{
			if (capacity <= 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Ring buffer cannot have zero or negative capacity.");
			}
			T[] array = startingItems.ToArray();
			if (array.Length > capacity)
			{
				throw new ArgumentException("Too many items to fit in the ring buffer.", "startingItems");
			}
			ofHBnmbKsBdEfOfompRScgChwotbB = new T[capacity];
			Array.Copy(array, ofHBnmbKsBdEfOfompRScgChwotbB, array.Length);
			HhAQwfUXfYRNwTlXjrnsPEBkCwnR = 0;
			Count = array.Length;
			TgAzEwfALmJSWbSmjvJgUtqbLEFX = Count % ofHBnmbKsBdEfOfompRScgChwotbB.Length;
		}

		public void PushBack(T newItem)
		{
			ofHBnmbKsBdEfOfompRScgChwotbB[TgAzEwfALmJSWbSmjvJgUtqbLEFX] = newItem;
			while (true)
			{
				int num = -143674275;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -159013583)) % 7)
					{
					case 6u:
						break;
					case 3u:
						num = ((int)num2 * -1275451844) ^ -157451837;
						continue;
					case 2u:
					{
						int num3;
						int num4;
						if (!IsFull)
						{
							num3 = 1484401951;
							num4 = num3;
						}
						else
						{
							num3 = 686118668;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -186814246);
						continue;
					}
					case 4u:
						HhAQwfUXfYRNwTlXjrnsPEBkCwnR = TgAzEwfALmJSWbSmjvJgUtqbLEFX;
						num = (int)(num2 * 62010011) ^ -1011258071;
						continue;
					case 1u:
						TgAzEwfALmJSWbSmjvJgUtqbLEFX = (TgAzEwfALmJSWbSmjvJgUtqbLEFX + 1) % ofHBnmbKsBdEfOfompRScgChwotbB.Length;
						num = ((int)num2 * -513374436) ^ 0x543A144B;
						continue;
					case 0u:
						Count++;
						num = -1093233513;
						continue;
					default:
						rkfGkphndiWKXAnfLWMUCduvcgcR();
						return;
					}
					break;
				}
			}
		}

		public void PushFront(T newItem)
		{
			HhAQwfUXfYRNwTlXjrnsPEBkCwnR--;
			if (HhAQwfUXfYRNwTlXjrnsPEBkCwnR < 0)
			{
				goto IL_0017;
			}
			goto IL_0065;
			IL_0017:
			int num = 1827877173;
			goto IL_001c;
			IL_001c:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1D39DD33)) % 8)
				{
				case 2u:
					break;
				case 4u:
					Count++;
					num = 427893046;
					continue;
				case 0u:
					goto IL_0065;
				case 6u:
					HhAQwfUXfYRNwTlXjrnsPEBkCwnR += ofHBnmbKsBdEfOfompRScgChwotbB.Length;
					num = ((int)num2 * -693641254) ^ -912634393;
					continue;
				case 3u:
					num = ((int)num2 * -443043558) ^ -1357329000;
					continue;
				case 7u:
					TgAzEwfALmJSWbSmjvJgUtqbLEFX = HhAQwfUXfYRNwTlXjrnsPEBkCwnR;
					num = ((int)num2 * -356309487) ^ 0x1CB9B1EF;
					continue;
				case 1u:
				{
					int num3;
					int num4;
					if (!IsFull)
					{
						num3 = 2114922871;
						num4 = num3;
					}
					else
					{
						num3 = 2138364540;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 646995440);
					continue;
				}
				default:
					rkfGkphndiWKXAnfLWMUCduvcgcR();
					return;
				}
				break;
			}
			goto IL_0017;
			IL_0065:
			ofHBnmbKsBdEfOfompRScgChwotbB[HhAQwfUXfYRNwTlXjrnsPEBkCwnR] = newItem;
			num = 832898882;
			goto IL_001c;
		}

		public T PopFront()
		{
			VxbNUIumBjCpvzGVwWMaudqvSZPP();
			T result = ofHBnmbKsBdEfOfompRScgChwotbB[HhAQwfUXfYRNwTlXjrnsPEBkCwnR];
			ofHBnmbKsBdEfOfompRScgChwotbB[HhAQwfUXfYRNwTlXjrnsPEBkCwnR] = default(T);
			Count--;
			HhAQwfUXfYRNwTlXjrnsPEBkCwnR = (HhAQwfUXfYRNwTlXjrnsPEBkCwnR + 1) % ofHBnmbKsBdEfOfompRScgChwotbB.Length;
			rkfGkphndiWKXAnfLWMUCduvcgcR();
			return result;
		}

		public T PopBack()
		{
			VxbNUIumBjCpvzGVwWMaudqvSZPP();
			int num = TgAzEwfALmJSWbSmjvJgUtqbLEFX - 1;
			while (true)
			{
				int num2 = -1574038916;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ -1432503419)) % 4)
					{
					case 0u:
						break;
					case 1u:
					{
						int num4;
						int num5;
						if (num < 0)
						{
							num4 = 1496860403;
							num5 = num4;
						}
						else
						{
							num4 = 1738105558;
							num5 = num4;
						}
						num2 = num4 ^ ((int)num3 * -1878731392);
						continue;
					}
					case 2u:
						num += ofHBnmbKsBdEfOfompRScgChwotbB.Length;
						num2 = ((int)num3 * -1964658527) ^ 0x75C145E0;
						continue;
					default:
					{
						T result = ofHBnmbKsBdEfOfompRScgChwotbB[num];
						ofHBnmbKsBdEfOfompRScgChwotbB[num] = default(T);
						Count--;
						TgAzEwfALmJSWbSmjvJgUtqbLEFX--;
						if (TgAzEwfALmJSWbSmjvJgUtqbLEFX < 0)
						{
							TgAzEwfALmJSWbSmjvJgUtqbLEFX += ofHBnmbKsBdEfOfompRScgChwotbB.Length;
						}
						rkfGkphndiWKXAnfLWMUCduvcgcR();
						return result;
					}
					}
					break;
				}
			}
		}

		public void Clear()
		{
			HhAQwfUXfYRNwTlXjrnsPEBkCwnR = 0;
			TgAzEwfALmJSWbSmjvJgUtqbLEFX = 0;
			Count = 0;
			int num3 = default(int);
			while (true)
			{
				int num = -1500824626;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -898636854)) % 5)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						num3 = 0;
						num = (int)((num2 * 2128853445) ^ 0xCFBA976);
						continue;
					case 3u:
					{
						int num4;
						if (num3 >= ofHBnmbKsBdEfOfompRScgChwotbB.Length)
						{
							num = -2057459243;
							num4 = num;
						}
						else
						{
							num = -1840219292;
							num4 = num;
						}
						continue;
					}
					case 2u:
						ofHBnmbKsBdEfOfompRScgChwotbB[num3] = default(T);
						num3++;
						num = -1556433310;
						continue;
					case 4u:
						return;
					}
					break;
				}
			}
		}

		public bool Contains(T item)
		{
			int num = 0;
			while (true)
			{
				int num2;
				int num3;
				if (num < Count)
				{
					num2 = 1965600593;
					num3 = num2;
				}
				else
				{
					num2 = 1125543598;
					num3 = num2;
				}
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num2 ^ 0x5146B86A)) % 6)
					{
					case 5u:
						num2 = 1965600593;
						continue;
					case 2u:
						num++;
						num2 = 1682937130;
						continue;
					case 1u:
						return true;
					case 3u:
					{
						int num5;
						if (object.Equals(item, ofHBnmbKsBdEfOfompRScgChwotbB[HgFZBCNBBpIndarWNvDvScpMaJAZA(num)]))
						{
							num2 = 1130542957;
							num5 = num2;
						}
						else
						{
							num2 = 1191389102;
							num5 = num2;
						}
						continue;
					}
					case 0u:
						break;
					default:
						return false;
					}
					break;
				}
			}
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				goto IL_0006;
			}
			goto IL_0146;
			IL_0006:
			int num = 1813143887;
			goto IL_000b;
			IL_000b:
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x4407C5FD)) % 11)
				{
				case 9u:
					break;
				case 10u:
					throw new ArgumentException("Destination array is not large enough to contain all items.", "array");
				case 7u:
					num3 = ofHBnmbKsBdEfOfompRScgChwotbB.Length - HhAQwfUXfYRNwTlXjrnsPEBkCwnR;
					num = 1304167793;
					continue;
				case 4u:
					goto IL_0082;
				case 3u:
					throw new ArgumentNullException("array");
				case 8u:
					goto IL_00c0;
				case 1u:
					Array.Copy(ofHBnmbKsBdEfOfompRScgChwotbB, 0, array, arrayIndex, Count);
					return;
				case 6u:
					throw new ArgumentOutOfRangeException("arrayIndex");
				case 2u:
					Array.Copy(ofHBnmbKsBdEfOfompRScgChwotbB, HhAQwfUXfYRNwTlXjrnsPEBkCwnR, array, arrayIndex, num3);
					num = (int)(num2 * 85706519) ^ -1633647811;
					continue;
				case 0u:
					goto IL_0146;
				default:
					Array.Copy(ofHBnmbKsBdEfOfompRScgChwotbB, 0, array, arrayIndex + num3, Count - num3);
					return;
				}
				break;
				IL_00c0:
				int num4;
				if (HhAQwfUXfYRNwTlXjrnsPEBkCwnR == 0)
				{
					num = 1713615616;
					num4 = num;
				}
				else
				{
					num = 1436183531;
					num4 = num;
				}
				continue;
				IL_0082:
				int num5;
				if (Count <= array.Length - arrayIndex)
				{
					num = 1827793524;
					num5 = num;
				}
				else
				{
					num = 601806940;
					num5 = num;
				}
			}
			goto IL_0006;
			IL_0146:
			int num6;
			if (arrayIndex < 0)
			{
				num = 1755049084;
				num6 = num;
			}
			else
			{
				num = 952998527;
				num6 = num;
			}
			goto IL_000b;
		}

		public int IndexOf(T item)
		{
			int num = 0;
			int num5 = default(int);
			while (true)
			{
				int num2;
				int num3;
				if (num < Count)
				{
					num2 = 306681173;
					num3 = num2;
				}
				else
				{
					num2 = 259323012;
					num3 = num2;
				}
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num2 ^ 0x682D974B)) % 7)
					{
					case 3u:
						num2 = 306681173;
						continue;
					case 1u:
						num5 = HgFZBCNBBpIndarWNvDvScpMaJAZA(num);
						num2 = 678627241;
						continue;
					case 4u:
					{
						int num6;
						int num7;
						if (object.Equals(item, ofHBnmbKsBdEfOfompRScgChwotbB[num5]))
						{
							num6 = 583410746;
							num7 = num6;
						}
						else
						{
							num6 = 608306834;
							num7 = num6;
						}
						num2 = num6 ^ (int)(num4 * 1080119245);
						continue;
					}
					case 0u:
						return num;
					case 6u:
						num++;
						num2 = 1417802948;
						continue;
					case 5u:
						break;
					default:
						return -1;
					}
					break;
				}
			}
		}

		private int HgFZBCNBBpIndarWNvDvScpMaJAZA(int P_0)
		{
			return (P_0 + HhAQwfUXfYRNwTlXjrnsPEBkCwnR) % ofHBnmbKsBdEfOfompRScgChwotbB.Length;
		}

		private void VxbNUIumBjCpvzGVwWMaudqvSZPP()
		{
			if (Count != 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1956034259u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new InvalidOperationException("The ring buffer is empty.");
				case 1u:
					return;
				}
			}
		}

		private void rkfGkphndiWKXAnfLWMUCduvcgcR()
		{
			acyKSgdXdwbDeVjeukgvZsJzMDVR++;
		}

		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		void ICollection<T>.Add(T item)
		{
			PushBack(item);
		}

		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new nEEhUmuJidovEpSwtWDAuzHBIvtc<T>(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
