using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BitCode.UI
{
	public class RadialMenu<TItem, TData> : MonoBehaviour where TItem : UnityEngine.Object, IRadialMenuItem<TData>
	{
		[SerializeField]
		private TItem itemPrefab;

		[SerializeField]
		private float inputDeadzone = 0.4f;

		[Header("Arrow")]
		[SerializeField]
		private RectTransform arrow;

		[Header("Ring")]
		[Tooltip("Once the the number of items is more than this value a spiral menu will be created.")]
		[SerializeField]
		private int maxItemsInRing;

		[SerializeField]
		private float ringOffsetStartAngle;

		[SerializeField]
		[Tooltip("If value is zero the angle will be calculated based on the max items in a ring.")]
		[Header("Spiral")]
		private float angleBetweenItems;

		[Tooltip("The number of items that will be shown anticlockwise from the selected item.")]
		[SerializeField]
		private int backWindow;

		[SerializeField]
		[Tooltip("The number of items that will be shown clockwise from the selected item.")]
		private int frontWindow;

		private IRadialMenuElementPlacer<TData> elementPlacer;

		private IRadialMenuInputProvider<TItem> inputProvider;

		private float deltaAngle;

		private float invDeltaAngle;

		private int selectedGraphicIndex;

		private int selectedDataIndex;

		private Vector2 selectedVector;

		private Vector2 inputVector;

		public bool Initialized { get; private set; }

		public bool IsSpiral { get; private set; }

		protected TItem[] GraphicItems { get; private set; }

		protected TData[] DataItems { get; private set; }

		public IRadialMenuElementPlacer<TData> ElementPlacer => elementPlacer;

		public IRadialMenuInputProvider<TItem> InputProvider => inputProvider;

		protected virtual void Awake()
		{
			elementPlacer = GetVisualItemPlacer();
			while (true)
			{
				int num = -1788739582;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1266551997)) % 4)
					{
					case 3u:
						break;
					default:
						return;
					case 1u:
						inputProvider = GetInputProvider();
						num = ((int)num2 * -1086204818) ^ 0x156EA28F;
						continue;
					case 2u:
						inputVector = RadialMenuHelpers.VectorFromAngle(ringOffsetStartAngle);
						num = ((int)num2 * -386050722) ^ -761542281;
						continue;
					case 0u:
						return;
					}
					break;
				}
			}
		}

		protected virtual IRadialMenuElementPlacer<TData> GetVisualItemPlacer()
		{
			return GetComponent<IRadialMenuElementPlacer<TData>>();
		}

		protected virtual IRadialMenuInputProvider<TItem> GetInputProvider()
		{
			return GetComponent<IRadialMenuInputProvider<TItem>>();
		}

		protected virtual void Update()
		{
			if (!Initialized)
			{
				goto IL_0008;
			}
			goto IL_003e;
			IL_0008:
			int num = 1761409953;
			goto IL_000d;
			IL_000d:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x423AD5F3)) % 4)
			{
			case 0u:
				break;
			default:
				return;
			case 2u:
				return;
			case 3u:
				goto IL_003e;
			case 1u:
				return;
			}
			goto IL_0008;
			IL_003e:
			SelectionMovement();
			num = 1412018914;
			goto IL_000d;
		}

		protected void UpdateRingItems()
		{
			if (GraphicItems == null)
			{
				return;
			}
			TItem item = default(TItem);
			int num5 = default(int);
			while (true)
			{
				int num = 281043176;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x17388E68)) % 9)
					{
					case 4u:
						break;
					default:
						return;
					case 2u:
						item = GraphicItems[num5];
						num = 1709310497;
						continue;
					case 3u:
						return;
					case 8u:
						num5 = 0;
						num = 1300694219;
						continue;
					case 7u:
					{
						int num6;
						if (num5 < GraphicItems.Length)
						{
							num = 2047254571;
							num6 = num;
						}
						else
						{
							num = 1867731639;
							num6 = num;
						}
						continue;
					}
					case 5u:
						elementPlacer.UpdateItemInRing(item, num5, base.transform.position, deltaAngle, ringOffsetStartAngle);
						num5++;
						num = ((int)num2 * -1393369590) ^ -2018412072;
						continue;
					case 6u:
					{
						int num3;
						int num4;
						if (GraphicItems.Length != 0)
						{
							num3 = -1265873255;
							num4 = num3;
						}
						else
						{
							num3 = -1565386006;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 819547290);
						continue;
					}
					case 0u:
						num = (int)((num2 * 1796728968) ^ 0x4D89EF9A);
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
		}

		protected void UpdateSpiralItems(bool selectionChanged, int dataIndex, int graphicIndex, float amountBetween)
		{
			if (GraphicItems == null)
			{
				return;
			}
			TItem item = default(TItem);
			int num7 = default(int);
			int num5 = default(int);
			int num12 = default(int);
			int num10 = default(int);
			while (true)
			{
				int num = 1607898635;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x65026BB5)) % 16)
					{
					case 0u:
						break;
					default:
						return;
					case 4u:
						elementPlacer.UpdateItemInSpiral(item, num7, selectedGraphicIndex, GraphicItems.Length, selectedVector, frontWindow, backWindow, amountBetween, deltaAngle);
						num7++;
						num = ((int)num2 * -699820952) ^ -888798498;
						continue;
					case 6u:
					{
						int num6;
						if (num5 <= frontWindow)
						{
							num = 518579239;
							num6 = num;
						}
						else
						{
							num = 1937604713;
							num6 = num;
						}
						continue;
					}
					case 13u:
						item = GraphicItems[num7];
						num = 1466248529;
						continue;
					case 7u:
						num7 = 0;
						num = 2042619540;
						continue;
					case 14u:
					{
						int num8;
						int num9;
						if (GraphicItems.Length != 0)
						{
							num8 = 1937274446;
							num9 = num8;
						}
						else
						{
							num8 = 1621574012;
							num9 = num8;
						}
						num = num8 ^ (int)(num2 * 1943876794);
						continue;
					}
					case 1u:
						num = (int)(num2 * 923010870) ^ -1129761400;
						continue;
					case 10u:
						num = ((int)num2 * -1065431079) ^ 0x279BB4C9;
						continue;
					case 15u:
						GraphicItems[num12].UpdateData(DataItems[num10]);
						num5++;
						num = (int)(num2 * 555059899) ^ -660908570;
						continue;
					case 2u:
						num12 = RadialMenuHelpers.Wrap(graphicIndex + num5, 0, GraphicItems.Length - 1);
						num = 337318205;
						continue;
					case 3u:
						num5 = -backWindow;
						num = ((int)num2 * -1335920549) ^ 0x5D28EA6E;
						continue;
					case 11u:
					{
						int num11;
						if (num7 < GraphicItems.Length)
						{
							num = 1145763896;
							num11 = num;
						}
						else
						{
							num = 2019332076;
							num11 = num;
						}
						continue;
					}
					case 8u:
						num10 = RadialMenuHelpers.Wrap(dataIndex + num5, 0, DataItems.Length - 1);
						num = (int)((num2 * 1867034168) ^ 0xFB0833A);
						continue;
					case 5u:
						return;
					case 9u:
					{
						int num3;
						int num4;
						if (selectionChanged)
						{
							num3 = 1982726870;
							num4 = num3;
						}
						else
						{
							num3 = 848540585;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -60794432);
						continue;
					}
					case 12u:
						return;
					}
					break;
				}
			}
		}

		public void Initialize(IList<TData> data)
		{
			if (Initialized)
			{
				goto IL_0008;
			}
			goto IL_006e;
			IL_0008:
			int num = -1439243622;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1314976598)) % 9)
				{
				case 6u:
					break;
				default:
					return;
				case 8u:
				{
					selectedGraphicIndex = 0;
					int num3;
					int num4;
					if (IsSpiral)
					{
						num3 = -408062841;
						num4 = num3;
					}
					else
					{
						num3 = -2116499539;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1221059590);
					continue;
				}
				case 2u:
					goto IL_006e;
				case 7u:
					CreateRing();
					num = ((int)num2 * -701128261) ^ -547176065;
					continue;
				case 5u:
					throw new InvalidOperationException("Menu has already been initialized. Call Clear() first.");
				case 0u:
					CreateSpiral();
					num = -755023561;
					continue;
				case 4u:
					num = (int)((num2 * 1923178420) ^ 0x846E3D7);
					continue;
				case 3u:
					Initialized = true;
					num = -852851692;
					continue;
				case 1u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_006e:
			IsSpiral = data.Count > maxItemsInRing;
			DataItems = data.ToArray();
			selectedDataIndex = 0;
			num = -953435598;
			goto IL_000d;
		}

		public void Clear()
		{
			if (!Initialized)
			{
				goto IL_0008;
			}
			goto IL_0057;
			IL_0008:
			int num = -1212701391;
			goto IL_000d;
			IL_000d:
			TItem[] graphicItems = default(TItem[]);
			int num3 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -661452253)) % 8)
				{
				case 4u:
					break;
				default:
					return;
				case 7u:
					Initialized = false;
					num = ((int)num2 * -344208967) ^ 0x3C80FFCD;
					continue;
				case 6u:
					goto IL_0057;
				case 3u:
					GraphicItems = null;
					DataItems = null;
					num = (int)((num2 * 194568050) ^ 0x4D7BDF2A);
					continue;
				case 0u:
					goto IL_0084;
				case 2u:
					throw new InvalidOperationException("Menu has not been initialized.");
				case 5u:
					UnityEngine.Object.Destroy(graphicItems[num3].transform.gameObject);
					num3++;
					num = -1678833637;
					continue;
				case 1u:
					return;
				}
				break;
				IL_0084:
				int num4;
				if (num3 < graphicItems.Length)
				{
					num = -2103486130;
					num4 = num;
				}
				else
				{
					num = -1582089832;
					num4 = num;
				}
			}
			goto IL_0008;
			IL_0057:
			graphicItems = GraphicItems;
			num3 = 0;
			num = -1678833637;
			goto IL_000d;
		}

		public void Select(int dataIndex)
		{
			if (GraphicItems == null)
			{
				return;
			}
			int num3 = default(int);
			while (true)
			{
				int num = 540881049;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x511C0368)) % 15)
					{
					case 5u:
						break;
					default:
						return;
					case 9u:
						selectedVector = Quaternion.AngleAxis(deltaAngle * (float)dataIndex + ringOffsetStartAngle, Vector3.back) * selectedVector;
						num = 1537166678;
						continue;
					case 6u:
						num = ((int)num2 * -1217529243) ^ -1451943305;
						continue;
					case 4u:
					{
						int num8;
						if (!IsSpiral)
						{
							num = 463805177;
							num8 = num;
						}
						else
						{
							num = 737386835;
							num8 = num;
						}
						continue;
					}
					case 1u:
						elementPlacer.UpdateArrow(arrow, selectedVector);
						num = ((int)num2 * -738650000) ^ 0x5A2A0631;
						continue;
					case 12u:
						num3 = RadialMenuHelpers.WrappedDistance(selectedDataIndex, dataIndex, 0, DataItems.Length);
						num = (int)((num2 * 539945675) ^ 0x4EF62E03);
						continue;
					case 14u:
						return;
					case 11u:
						selectedGraphicIndex = dataIndex;
						selectedDataIndex = dataIndex;
						num = ((int)num2 * -929574928) ^ -1150346970;
						continue;
					case 7u:
						GraphicItems[dataIndex].Select();
						num = 993405536;
						continue;
					case 3u:
					{
						int num6;
						int num7;
						if (GraphicItems.Length == 0)
						{
							num6 = 1290610829;
							num7 = num6;
						}
						else
						{
							num6 = 622249978;
							num7 = num6;
						}
						num = num6 ^ ((int)num2 * -1237195767);
						continue;
					}
					case 0u:
						selectedGraphicIndex = RadialMenuHelpers.Wrap(selectedGraphicIndex + num3, 0, GraphicItems.Length - 1);
						num = (int)((num2 * 1397274666) ^ 0xE3D5C5E);
						continue;
					case 2u:
					{
						int num4;
						int num5;
						if (!(arrow != null))
						{
							num4 = -1541617847;
							num5 = num4;
						}
						else
						{
							num4 = -1114714536;
							num5 = num4;
						}
						num = num4 ^ ((int)num2 * -1425544705);
						continue;
					}
					case 13u:
						selectedVector = Quaternion.AngleAxis(deltaAngle * (float)num3, Vector3.back) * selectedVector;
						UpdateSpiralItems(selectionChanged: true, dataIndex, selectedGraphicIndex, 0f);
						num = ((int)num2 * -100217226) ^ 0x674735A5;
						continue;
					case 8u:
						selectedDataIndex = RadialMenuHelpers.Wrap(selectedDataIndex + num3, 0, DataItems.Length - 1);
						num = ((int)num2 * -1235156391) ^ 0x6F82FC82;
						continue;
					case 10u:
						return;
					}
					break;
				}
			}
		}

		public void Select(TData item)
		{
			int num = Array.IndexOf(DataItems, item);
			while (true)
			{
				int num2 = -2091919491;
				while (true)
				{
					uint num3;
					int num4;
					switch ((num3 = (uint)(num2 ^ -141730057)) % 4)
					{
					case 3u:
						break;
					case 2u:
					{
						int num5;
						if (num == -1)
						{
							num4 = 1025895854;
							num5 = num4;
						}
						else
						{
							num4 = 903697607;
							num5 = num4;
						}
						goto IL_0045;
					}
					case 1u:
						return;
					default:
						Select(num);
						return;
					}
					break;
					IL_0045:
					num2 = num4 ^ (int)(num3 * 1056611638);
				}
			}
		}

		private void CreateRing()
		{
			GraphicItems = new TItem[0];
			if (DataItems == null)
			{
				return;
			}
			int num3 = default(int);
			TItem component = default(TItem);
			while (true)
			{
				int num = -282240906;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1930122962)) % 14)
					{
					case 2u:
						break;
					default:
						return;
					case 0u:
						GraphicItems = new TItem[DataItems.Length];
						num = -1072832079;
						continue;
					case 11u:
						return;
					case 7u:
						invDeltaAngle = 1f / deltaAngle;
						num = (int)(num2 * 732841682) ^ -1622465599;
						continue;
					case 10u:
					{
						int num5;
						int num6;
						if (DataItems.Length == 0)
						{
							num5 = 634383111;
							num6 = num5;
						}
						else
						{
							num5 = 1815808446;
							num6 = num5;
						}
						num = num5 ^ (int)(num2 * 461969372);
						continue;
					}
					case 5u:
						num3++;
						num = ((int)num2 * -1916651874) ^ 0x777D1289;
						continue;
					case 3u:
					{
						int num4;
						if (num3 >= GraphicItems.Length)
						{
							num = -1685427326;
							num4 = num;
						}
						else
						{
							num = -2113114083;
							num4 = num;
						}
						continue;
					}
					case 9u:
						selectedVector = RadialMenuHelpers.VectorFromAngle(ringOffsetStartAngle);
						num3 = 0;
						num = ((int)num2 * -1195754484) ^ 0x2F12605A;
						continue;
					case 1u:
					{
						GameObject obj = UnityEngine.Object.Instantiate(itemPrefab.transform.gameObject, base.transform.position, Quaternion.identity);
						obj.transform.SetParent(base.transform, worldPositionStays: false);
						component = obj.GetComponent<TItem>();
						elementPlacer.UpdateItemInRing(component, num3, base.transform.position, deltaAngle, ringOffsetStartAngle);
						num = -1003300132;
						continue;
					}
					case 12u:
						GraphicItems[selectedGraphicIndex].Select();
						num = (int)(num2 * 47402677) ^ -1814128082;
						continue;
					case 4u:
						num = ((int)num2 * -396568034) ^ 0xE50C8C7;
						continue;
					case 8u:
						component.UpdateData(DataItems[num3]);
						GraphicItems[num3] = component;
						num = ((int)num2 * -376770836) ^ -399987545;
						continue;
					case 13u:
						deltaAngle = 360f / (float)GraphicItems.Length;
						num = (int)((num2 * 530044737) ^ 0x676EA6B8);
						continue;
					case 6u:
						return;
					}
					break;
				}
			}
		}

		private void CreateSpiral()
		{
			deltaAngle = ((angleBetweenItems <= Mathf.Epsilon) ? (360f / (float)maxItemsInRing) : angleBetweenItems);
			int num6 = default(int);
			int num4 = default(int);
			int num3 = default(int);
			while (true)
			{
				int num = 659540712;
				while (true)
				{
					uint num2;
					int num5;
					switch ((num2 = (uint)(num ^ 0x293B84D4)) % 10)
					{
					case 9u:
						break;
					case 5u:
					{
						GameObject obj = UnityEngine.Object.Instantiate(itemPrefab.transform.gameObject, base.transform.position, Quaternion.identity);
						obj.transform.SetParent(base.transform, worldPositionStays: false);
						TItem component = obj.GetComponent<TItem>();
						elementPlacer.UpdateItemInSpiral(component, num6, selectedGraphicIndex, GraphicItems.Length, selectedVector, frontWindow, backWindow, 0f, deltaAngle);
						component.UpdateData(DataItems[RadialMenuHelpers.Wrap(num6, 0, DataItems.Length - 1)]);
						GraphicItems[num6] = component;
						num6++;
						num = 1906577818;
						continue;
					}
					case 7u:
						num6 = 0;
						num = (int)((num2 * 228888009) ^ 0x5FAB7007);
						continue;
					case 8u:
						invDeltaAngle = 1f / deltaAngle;
						if (!(angleBetweenItems < Mathf.Epsilon))
						{
							num = ((int)num2 * -169413016) ^ 0x54A5B32F;
							continue;
						}
						num5 = maxItemsInRing;
						goto IL_01a0;
					case 4u:
					{
						int num7;
						if (num6 >= num4)
						{
							num = 2113181714;
							num7 = num;
						}
						else
						{
							num = 662705533;
							num7 = num;
						}
						continue;
					}
					case 1u:
						num5 = 360 / (int)deltaAngle;
						goto IL_01a0;
					case 2u:
						GraphicItems[selectedGraphicIndex].Select();
						num = ((int)num2 * -136955502) ^ -1667874828;
						continue;
					case 0u:
						num4 *= num3;
						GraphicItems = new TItem[num4];
						num = ((int)num2 * -1495958558) ^ 0x6CA9BCD;
						continue;
					case 3u:
						num3 = Mathf.Max(2, Mathf.CeilToInt((float)(backWindow + frontWindow) / (float)num4));
						num = ((int)num2 * -525172157) ^ -302820229;
						continue;
					default:
						{
							selectedVector = RadialMenuHelpers.VectorFromAngle(ringOffsetStartAngle);
							UpdateSpiralItems(selectionChanged: false, 0, 0, 0f);
							return;
						}
						IL_01a0:
						num4 = num5;
						num = 1753606503;
						continue;
					}
					break;
				}
			}
		}

		private void SelectionMovement()
		{
			if (inputProvider.InputState == RadialMenuInputState.Relative)
			{
				goto IL_0011;
			}
			goto IL_026b;
			IL_0011:
			int num = 2073797354;
			goto IL_0016;
			IL_0016:
			int num3 = default(int);
			bool flag = default(bool);
			float sqrMagnitude = default(float);
			float num5 = default(float);
			float num4 = default(float);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x429F80B)) % 19)
				{
				case 14u:
					break;
				default:
					return;
				case 15u:
				{
					int num8;
					int num9;
					if (!(arrow != null))
					{
						num8 = 1863551641;
						num9 = num8;
					}
					else
					{
						num8 = 129101121;
						num9 = num8;
					}
					num = num8 ^ ((int)num2 * -477760836);
					continue;
				}
				case 8u:
					goto IL_00a3;
				case 5u:
					num3 = Mathf.RoundToInt(Vector3.SignedAngle(selectedVector, inputVector, Vector3.back) * invDeltaAngle);
					flag = num3 != 0;
					num = 512984385;
					continue;
				case 16u:
					goto IL_00f8;
				case 10u:
					inputVector = inputProvider.GetAbsoluteInput();
					num = (int)(num2 * 757636230) ^ -1238918492;
					continue;
				case 1u:
				{
					int num10;
					int num11;
					if (sqrMagnitude >= num5)
					{
						num10 = -1538034317;
						num11 = num10;
					}
					else
					{
						num10 = -2114211738;
						num11 = num10;
					}
					num = num10 ^ (int)(num2 * 993329699);
					continue;
				}
				case 12u:
				{
					int num6;
					int num7;
					if (!flag)
					{
						num6 = 900266905;
						num7 = num6;
					}
					else
					{
						num6 = 370821048;
						num7 = num6;
					}
					num = num6 ^ ((int)num2 * -671866761);
					continue;
				}
				case 9u:
					inputProvider.SelectItem(GraphicItems[selectedGraphicIndex]);
					num = ((int)num2 * -1211954504) ^ 0x1FC24C77;
					continue;
				case 2u:
					throw new NotImplementedException();
				case 0u:
					selectedGraphicIndex = RadialMenuHelpers.Wrap(selectedGraphicIndex + num3, 0, GraphicItems.Length - 1);
					num = ((int)num2 * -1663639442) ^ -1379456481;
					continue;
				case 3u:
					sqrMagnitude = inputVector.sqrMagnitude;
					num5 = inputDeadzone * inputDeadzone;
					num = ((int)num2 * -1630241968) ^ 0x7286F0F9;
					continue;
				case 18u:
					inputVector /= Mathf.Sqrt(sqrMagnitude);
					num = 1842394073;
					continue;
				case 11u:
					elementPlacer.UpdateArrow(arrow, inputVector);
					num = (int)((num2 * 1756295625) ^ 0x6E46F223);
					continue;
				case 17u:
					goto IL_026b;
				case 13u:
					selectedDataIndex = RadialMenuHelpers.Wrap(selectedDataIndex + num3, 0, DataItems.Length - 1);
					selectedVector = Quaternion.AngleAxis(deltaAngle * (float)num3 + num4, Vector3.back) * selectedVector;
					num = (int)(num2 * 2047731661) ^ -1143405962;
					continue;
				case 6u:
					return;
				case 4u:
				{
					float amountBetween = Vector3.SignedAngle(selectedVector, inputVector, Vector3.back) * invDeltaAngle;
					UpdateSpiralItems(flag, selectedDataIndex, selectedGraphicIndex, amountBetween);
					num = (int)((num2 * 891542975) ^ 0x3971F355);
					continue;
				}
				case 7u:
					return;
				}
				break;
				IL_00f8:
				int num12;
				if (IsSpiral)
				{
					num = 493149130;
					num12 = num;
				}
				else
				{
					num = 1774172842;
					num12 = num;
				}
			}
			goto IL_0011;
			IL_026b:
			float num13;
			if (IsSpiral)
			{
				num13 = 0f;
				goto IL_00b0;
			}
			num = 2020429712;
			goto IL_0016;
			IL_00b0:
			num4 = num13;
			num = 414210407;
			goto IL_0016;
			IL_00a3:
			num13 = ringOffsetStartAngle;
			goto IL_00b0;
		}
	}
}
