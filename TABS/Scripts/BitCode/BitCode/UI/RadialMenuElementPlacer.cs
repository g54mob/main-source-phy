using System;
using UnityEngine;

namespace BitCode.UI
{
	public class RadialMenuElementPlacer<TData> : MonoBehaviour, IRadialMenuElementPlacer<TData>
	{
		[SerializeField]
		private float arrowRadius;

		[SerializeField]
		private float ringRadius;

		[SerializeField]
		private float spiralRadius;

		[Tooltip("This curve is used to fade the transparency of the items in the spiral menu. Remember that 0.5 is the currently selected object.")]
		[SerializeField]
		private AnimationCurve fadeCurve;

		[Tooltip("The maximum and minimum value from the spiral radius that the spiral will curl inwards.")]
		[SerializeField]
		private float spiralRadiusModifier;

		[SerializeField]
		[Tooltip("This curve is used to fine tune the way the curl moves based on spiralRadiusMultiplier. Remember that 0.5 is the currently selected object.")]
		private AnimationCurve curlCurve;

		[Tooltip("The factor that is multiplied by the scale curve. ")]
		[SerializeField]
		private float scaleMultiplier;

		[Tooltip("The scale curve of the scaleMultiplier. Remember that 0.5 is the currently selected object.")]
		[SerializeField]
		private AnimationCurve scaleCurve;

		private float resolutionScaleFactor = 1f;

		private float layoutScaleFactor = 1f;

		public float Radius => Mathf.Max(spiralRadius, ringRadius);

		public float ResolutionScaleFactor => resolutionScaleFactor;

		public float LayoutScaleFactor => layoutScaleFactor;

		public void UpdateArrow(RectTransform arrowTransform, Vector2 inputVector)
		{
			arrowTransform.up = inputVector;
			arrowTransform.position = base.transform.position + layoutScaleFactor * arrowRadius * (Vector3)inputVector;
		}

		public void UpdateItemInRing(IRadialMenuItem<TData> item, int index, Vector2 origin, float deltaAngle, float ringOffsetStartAngle)
		{
			Vector3 localPosition = RadialMenuHelpers.VectorFromAngle((float)index * deltaAngle + ringOffsetStartAngle) * (ringRadius * layoutScaleFactor);
			while (true)
			{
				int num = 895603584;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x54C5CFB1)) % 4)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						item.transform.localPosition = localPosition;
						num = ((int)num2 * -1150630048) ^ -1270049155;
						continue;
					case 0u:
						item.transform.localScale = Vector3.one * layoutScaleFactor;
						num = (int)(num2 * 955618983) ^ -843579234;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void UpdateItemInSpiral(IRadialMenuItem<TData> item, int index, int selectedIndex, int numItems, Vector2 selectedVector, int frontWindow, int backWindow, float amountBetween, float angleBetween)
		{
			int num = RadialMenuHelpers.SignedWrappedDistance(selectedIndex, index, 0, numItems - 1);
			if (num <= 0)
			{
				goto IL_0014;
			}
			int num2 = frontWindow;
			goto IL_01dd;
			IL_01dd:
			int num3 = num2;
			int num4 = Mathf.Abs(num);
			int num5 = -1884089189;
			goto IL_0019;
			IL_0014:
			num5 = -1840738984;
			goto IL_0019;
			IL_0019:
			float time = default(float);
			float num7 = default(float);
			Vector3 localScale = default(Vector3);
			float num8 = default(float);
			while (true)
			{
				uint num6;
				switch ((num6 = (uint)(num5 ^ -1488187437)) % 13)
				{
				case 8u:
					break;
				default:
					return;
				case 1u:
				{
					float alpha = fadeCurve.Evaluate(time);
					item.SetAlpha(alpha);
					num7 = Mathf.Lerp(spiralRadius - spiralRadiusModifier, spiralRadius + spiralRadiusModifier, curlCurve.Evaluate(time));
					num5 = (int)(num6 * 1383155579) ^ -10364523;
					continue;
				}
				case 7u:
					item.transform.localScale = localScale;
					num5 = (int)(num6 * 1941662360) ^ -1443523648;
					continue;
				case 6u:
					goto IL_00db;
				case 2u:
					goto IL_0108;
				case 11u:
					num8 = ((float)num - amountBetween) / (float)num3;
					num5 = -641794133;
					continue;
				case 12u:
					time = 0.5f + num8 * 0.5f;
					num5 = ((int)num6 * -1885485968) ^ -2128999976;
					continue;
				case 10u:
				{
					Vector2 vector = Quaternion.AngleAxis(angleBetween * (float)num, Vector3.back) * selectedVector;
					item.transform.localPosition = vector.normalized * (num7 * layoutScaleFactor);
					localScale = scaleMultiplier * scaleCurve.Evaluate(time) * layoutScaleFactor * Vector3.one;
					num5 = (int)((num6 * 1864417703) ^ 0x39DD4C51);
					continue;
				}
				case 9u:
					goto IL_01d7;
				case 3u:
					return;
				case 0u:
					item.SetAlpha(0f);
					num5 = (int)((num6 * 649434767) ^ 0x126A3E8A);
					continue;
				case 5u:
					item.transform.localScale = Vector3.zero;
					num5 = ((int)num6 * -169457414) ^ -1821737418;
					continue;
				case 4u:
					return;
				}
				break;
				IL_0108:
				int num9;
				if (num <= 0)
				{
					num9 = 0;
					goto IL_00e3;
				}
				num5 = ((int)num6 * -1459321427) ^ -1038291196;
				continue;
				IL_00db:
				num9 = ((num4 > frontWindow) ? 1 : 0);
				goto IL_00e3;
				IL_00e3:
				bool flag = num < 0 && num4 > backWindow;
				int num10;
				if (((uint)num9 | (flag ? 1u : 0u)) != 0)
				{
					num5 = -688077509;
					num10 = num5;
				}
				else
				{
					num5 = -203508048;
					num10 = num5;
				}
			}
			goto IL_0014;
			IL_01d7:
			num2 = backWindow;
			goto IL_01dd;
		}

		public void SetScaleFactor(float resolutionScale, float layoutScale)
		{
			if (layoutScale > 0f)
			{
				while (true)
				{
					int num = -1505335101;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1299988589)) % 6)
						{
						case 5u:
							break;
						case 2u:
						{
							int num3;
							int num4;
							if (resolutionScale <= 0f)
							{
								num3 = 1760387677;
								num4 = num3;
							}
							else
							{
								num3 = 174283471;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1079023875);
							continue;
						}
						case 1u:
							return;
						case 4u:
							resolutionScaleFactor = resolutionScale;
							num = (int)(num2 * 485628114) ^ -557904808;
							continue;
						case 3u:
							layoutScaleFactor = layoutScale;
							num = ((int)num2 * -2123467404) ^ -1319194298;
							continue;
						default:
							goto end_IL_000b;
						}
						break;
					}
					continue;
					end_IL_000b:
					break;
				}
			}
			throw new ArgumentOutOfRangeException("Arguments \"resolutionScale\", \"layoutScale\" must be greater than 0.");
		}
	}
}
