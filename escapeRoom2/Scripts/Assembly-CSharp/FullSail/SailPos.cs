using System;
using UnityEngine;

namespace FullSail
{
	[ExecuteAlways]
	public class SailPos : MonoBehaviour
	{
		private const float pi2 = MathF.PI * 2f;

		private const float hpi = MathF.PI / 2f;

		public Sail sail;

		public SailGroup group;

		[Range(0f, 1f)]
		public float sailPosX;

		[Range(0f, 1f)]
		public float sailPosY;

		public float sailWidth = 10f;

		public float sailHeight = 10f;

		private Material mat;

		private Vector3 vertPos;

		private Vector3 sailPos;

		private float _Speed;

		private Vector3 _WindDir;

		private float _SailWind;

		private Vector3 _SailForward;

		private float _SailReverse;

		private float _FillPercent;

		private float _SailSideArch;

		private Texture2D _MaskMap;

		private Texture2D _MaskMapRev;

		private float _VorSeed;

		private float _VorSpeed;

		private float _VorScale;

		private float _VorStrength;

		private float _SailSideways;

		private float _SailLift;

		private float _SailArchStart;

		private float _SailLiftSideArch;

		private float _SailArch;

		private float _SailTopArch;

		private float _SailLiftArch;

		private float _SailTilt;

		private float _SailTiltTop;

		private Texture2D _FurlMap;

		private Texture2D _FurledMap;

		private float _SailTaper;

		private float _SailShear;

		private Vector4 _ImpactLocation;

		private float _Time;

		private float _FurledRadius;

		private float _Furl;

		private Texture2D _ImpactRipple;

		private float _FullRipple;

		private Texture2D _RippleNoise;

		private float SimpleNoise1(Vector2 UV, float Scale)
		{
			if ((bool)_RippleNoise)
			{
				return _RippleNoise.GetPixelBilinear(UV.x * Scale * 0.01f, UV.y * Scale * 0.01f).r - 0.5f;
			}
			return 0f;
		}

		private float wave(Vector2 position, Vector2 origin, float time)
		{
			float num = Vector2.Distance(position, origin);
			float u = time - num * 0.5f;
			Color color = Color.gray;
			if ((bool)_ImpactRipple)
			{
				color = _ImpactRipple.GetPixelBilinear(u, 0f);
			}
			return (color.r - 0.5f) * 2f * Mathf.Clamp01(1f - num);
		}

		private Vector3 Deform(Vector2 uvin)
		{
			Vector3 windDir = _WindDir;
			Vector3 lhs = (windDir = sail.transform.InverseTransformVector(_WindDir));
			float sailWind = _SailWind;
			float num = Vector3.Dot(lhs, _SailForward);
			float num2 = 1f;
			float num3 = 0f;
			if (num < 0f)
			{
				float num4 = sailWind * _SailReverse * Mathf.Abs(num);
				sailWind = ((!(num4 > sailWind * _SailReverse)) ? num4 : (sailWind * _SailReverse));
				num3 = ((!_MaskMapRev) ? _MaskMap.GetPixelBilinear(uvin.x, uvin.y).r : _MaskMapRev.GetPixelBilinear(uvin.x, uvin.y).r);
			}
			else
			{
				if ((bool)_MaskMap)
				{
					num3 = _MaskMap.GetPixelBilinear(uvin.x, uvin.y).r;
				}
				sailWind *= num;
			}
			Vector3 vector = Vector3.Lerp(_SailForward * num, windDir, _SailSideways);
			Vector2 uV = default(Vector2);
			uV.x = uvin.x + (_Time + _VorSeed) * _VorSpeed;
			uV.y = uvin.y + (_Time + _VorSeed) * _VorSpeed;
			float num5 = SimpleNoise1(uV, _VorScale);
			Vector3 result = sailWind * vector * _Speed * num3 * num2;
			Vector3 vector2 = num3 * _SailForward * (num2 * num5 * _VorStrength * _FillPercent * (1f - sailWind * _FullRipple));
			result += vector2;
			float num6 = sailWind * _Speed;
			float num7 = num6 * (1f - uvin.y);
			result.y += num7 * _SailLift;
			float num8 = Mathf.Abs(uvin.y * 2f - 1f);
			float num9 = num6 * _SailLiftSideArch;
			if ((double)uvin.x < 0.5)
			{
				result.x += (_SailSideArch + num9) * Mathf.Cos(num8 * (MathF.PI / 2f)) * (1f - uvin.x / 0.5f);
			}
			else
			{
				result.x -= (_SailSideArch + num9) * Mathf.Cos(num8 * (MathF.PI / 2f)) * (1f - (1f - uvin.x) / 0.5f);
			}
			float num10 = 1f - Mathf.Abs(uvin.x * 2f - 1f);
			float num11 = 0f;
			if (_SailArchStart > 0f)
			{
				num11 = 1f - uvin.y / _SailArchStart;
			}
			float num12 = Mathf.Sin(num10 * (MathF.PI / 2f));
			if (num11 < 0f)
			{
				num11 = 0f;
			}
			if (uvin.y < _SailArchStart)
			{
				result.y += (_SailArch + num7 * _SailLiftArch) * num12 * num11;
			}
			result.y += _SailTopArch * num12 * (1f - num11);
			result.z += wave(uvin, _ImpactLocation, _Time - _ImpactLocation.z) * _ImpactLocation.w * num3;
			return result;
		}

		public Vector3 GetPos(Vector3 vertex, Vector2 texcoord1)
		{
			Vector3 vector = Deform(texcoord1);
			float num = 1f;
			Vector2 vector2 = texcoord1;
			float num2 = ((!_FurlMap) ? 1f : _FurlMap.GetPixelBilinear(vector2.x, vector2.y).r);
			float furl = _Furl;
			float t = 0f;
			float num3 = 1f - texcoord1.y;
			float num4 = Mathf.Lerp(_SailTiltTop, _SailTilt, num3);
			if (num2 < furl)
			{
				num = Mathf.Clamp01(1f - (furl - num2));
				if (furl > 1f)
				{
					t = furl - 1f;
				}
				float num5 = ((!_FurledMap) ? _FurledRadius : (_FurledRadius * _FurledMap.GetPixelBilinear(vector2.x, 0f).r));
				Vector3 b = default(Vector3);
				b.z = Mathf.Sin(MathF.PI * 2f - MathF.PI * 2f * texcoord1.y) * num5;
				b.y = Mathf.Cos(MathF.PI * 2f - MathF.PI * 2f * texcoord1.y) * num5 - num5;
				b.x = vertex.x;
				Vector3 a = vertex + vector * (num * num);
				a.y += num4 * (texcoord1.x * 2f - 1f);
				a.y *= num;
				a.x *= 1f + _SailTaper * num3 * num;
				a.x += num3 * num * _SailShear;
				return Vector3.Lerp(a, b, t);
			}
			Vector3 result = vertex + vector;
			result.y += num4 * (texcoord1.x * 2f - 1f);
			result.y *= num;
			result.x *= 1f + _SailTaper * num3 * num;
			result.x += num3 * num * _SailShear;
			return result;
		}

		private float GetFloat(SailParamID id, Material mat)
		{
			if ((bool)mat)
			{
				string paramName = Sail.GetParamName(id);
				return mat.GetFloat(paramName);
			}
			return 0f;
		}

		private Vector4 GetVector(SailParamID id, Material mat)
		{
			if ((bool)mat)
			{
				string paramName = Sail.GetParamName(id);
				return mat.GetVector(paramName);
			}
			return Vector4.zero;
		}

		private Texture2D GetTexture(SailParamID id, Material mat)
		{
			if ((bool)mat)
			{
				string paramName = Sail.GetParamName(id);
				return (Texture2D)mat.GetTexture(paramName);
			}
			return null;
		}

		private float GetFloat(SailParamID id, Sail s, SailGroup g, Material m)
		{
			if ((bool)s)
			{
				SailParam param = s.GetParam(id);
				if (param != null && param.active)
				{
					return param.fval;
				}
			}
			if ((bool)g)
			{
				SailParam param2 = g.GetParam(id);
				if (param2 != null && param2.active)
				{
					return param2.fval;
				}
			}
			return GetFloat(id, m);
		}

		private Vector4 GetVector(SailParamID id, Sail s, SailGroup g, Material m)
		{
			if ((bool)s)
			{
				SailParam param = s.GetParam(id);
				if (param != null && param.active)
				{
					return param.vval;
				}
			}
			if ((bool)g)
			{
				SailParam param2 = g.GetParam(id);
				if (param2 != null && param2.active)
				{
					return param2.vval;
				}
			}
			return GetVector(id, m);
		}

		private Texture2D GetTexture(SailParamID id, Sail s, SailGroup g, Material m)
		{
			if ((bool)s)
			{
				SailParam param = s.GetParam(id);
				if (param != null && param.active)
				{
					return param.tval;
				}
			}
			if ((bool)g)
			{
				SailParam param2 = g.GetParam(id);
				if (param2 != null && param2.active)
				{
					return param2.tval;
				}
			}
			return GetTexture(id, m);
		}

		private void GetParams()
		{
			mat = null;
			if ((bool)sail)
			{
				mat = sail.GetComponent<MeshRenderer>()?.sharedMaterial;
			}
			if ((bool)mat)
			{
				_Speed = GetFloat(SailParamID.Speed, sail, group, mat);
				_Furl = GetFloat(SailParamID.Furl, sail, group, mat);
				_SailWind = GetFloat(SailParamID.SailWind, sail, group, mat);
				_WindDir = GetVector(SailParamID.WindDir, sail, group, mat);
				_ImpactLocation = GetVector(SailParamID.ImpactLocation, sail, group, mat);
				_SailReverse = GetFloat(SailParamID.SailReverse, sail, group, mat);
				_FillPercent = GetFloat(SailParamID.FillPercent, sail, group, mat);
				_SailSideArch = GetFloat(SailParamID.SailSideArch, sail, group, mat);
				_VorSeed = GetFloat(SailParamID.RippleSeed, sail, group, mat);
				_VorSpeed = GetFloat(SailParamID.RippleSpeed, sail, group, mat);
				_VorScale = GetFloat(SailParamID.RippleScale, sail, group, mat);
				_VorStrength = GetFloat(SailParamID.RippleStrength, sail, group, mat);
				_SailSideways = GetFloat(SailParamID.SailSideways, sail, group, mat);
				_SailLift = GetFloat(SailParamID.SailLift, sail, group, mat);
				_SailArchStart = GetFloat(SailParamID.SailArchStart, sail, group, mat);
				_SailLiftSideArch = GetFloat(SailParamID.SailLiftSideArch, sail, group, mat);
				_SailArch = GetFloat(SailParamID.SailArch, sail, group, mat);
				_SailTopArch = GetFloat(SailParamID.SailTopArch, sail, group, mat);
				_SailLiftArch = GetFloat(SailParamID.SailLiftArch, sail, group, mat);
				_SailTilt = GetFloat(SailParamID.SailTilt, sail, group, mat);
				_SailTiltTop = GetFloat(SailParamID.SailTiltTop, sail, group, mat);
				_SailTaper = GetFloat(SailParamID.SailTaper, sail, group, mat);
				_SailShear = GetFloat(SailParamID.SailShear, sail, group, mat);
				_FurledRadius = GetFloat(SailParamID.FurledRadius, sail, group, mat);
				_FullRipple = GetFloat(SailParamID.FullRipple, sail, group, mat);
				_SailForward = GetVector(SailParamID.SailForward, sail, group, mat);
				_MaskMap = GetTexture(SailParamID.MaskMap, sail, group, mat);
				_MaskMapRev = GetTexture(SailParamID.MaskMapRev, sail, group, mat);
				_FurlMap = GetTexture(SailParamID.FurlMap, sail, group, mat);
				_FurledMap = GetTexture(SailParamID.FurledMap, sail, group, mat);
				_ImpactRipple = GetTexture(SailParamID.ImpactRipple, sail, group, mat);
				_RippleNoise = GetTexture(SailParamID.RippleNoise, sail, group, mat);
			}
		}

		private void Start()
		{
			if (!sail)
			{
				sail = GetComponent<Sail>();
				if (!sail)
				{
					sail = GetComponentInParent<Sail>();
				}
			}
			if (!group && (bool)sail)
			{
				group = sail.GetComponentInParent<SailGroup>();
			}
			GetParams();
		}

		public Vector3 GetPos()
		{
			return sailPos;
		}

		private void LateUpdate()
		{
			if ((bool)sail)
			{
				if (mat == null)
				{
					GetParams();
				}
				_Time = Time.timeSinceLevelLoad;
				_Speed = GetFloat(SailParamID.Speed, sail, group, mat);
				_Furl = GetFloat(SailParamID.Furl, sail, group, mat);
				_SailWind = GetFloat(SailParamID.SailWind, sail, group, mat);
				_WindDir = GetVector(SailParamID.WindDir, sail, group, mat);
				_ImpactLocation = GetVector(SailParamID.ImpactLocation, sail, group, mat);
				_VorSeed = sail.GetAnimOffset();
				vertPos.x = sailWidth * 0.5f + 0f - sailPosX * sailWidth;
				vertPos.y = 0f - sailHeight + 0f + sailPosY * sailHeight;
				vertPos.z = 0f;
				sailPos = sail.transform.TransformPoint(GetPos(vertPos, new Vector2(sailPosX, sailPosY)));
				base.transform.position = sailPos;
			}
		}
	}
}
