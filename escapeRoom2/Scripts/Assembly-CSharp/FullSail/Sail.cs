using System.Collections.Generic;
using UnityEngine;

namespace FullSail
{
	[ExecuteAlways]
	[HelpURL("http://www.macspeedee.com/full-sail/")]
	public class Sail : MonoBehaviour
	{
		public List<SailParam> sailParams = new List<SailParam>();

		public bool dirty;

		private MaterialPropertyBlock pblock;

		private MeshRenderer mr;

		private LODGroup lodGroup;

		private LOD[] lods;

		private float animOffset;

		private bool visible = true;

		public const int maxImpacts = 16;

		public bool showImpacts;

		public int impactCount;

		public Vector4[] impactPoints = new Vector4[16];

		public Vector3 boundsSize = new Vector3(10f, 10f, 2f);

		public Vector3 boundsOffset = new Vector3(0f, -5f, 0f);

		private void OnBecameVisible()
		{
			visible = true;
		}

		private void OnBecameInvisible()
		{
			visible = false;
		}

		public void AddRipple(float x, float y, float size)
		{
			Vector4 val = new Vector4(x, y, Time.timeSinceLevelLoad, size);
			SetValue(SailParamID.ImpactLocation, val);
		}

		public void AddImpact(float x, float y, float size, int type, bool remove = false)
		{
			if (remove && impactCount >= 16)
			{
				RemoveImpact();
			}
			if (impactCount < 16)
			{
				impactPoints[impactCount] = new Vector4(x, y, size, type);
				impactCount++;
			}
		}

		public void PatchImpact(int index)
		{
			if (index < 16 && impactPoints[index].w < 8f)
			{
				impactPoints[index].w += 8f;
			}
		}

		public void UnPatchImpact(int index)
		{
			if (index < 16 && impactPoints[index].w >= 7.9f)
			{
				impactPoints[index].w -= 8f;
			}
		}

		public void PatchImpact()
		{
			for (int i = 0; i < 16; i++)
			{
				if (impactPoints[i].w < 8f)
				{
					impactPoints[i].w += 8f;
					break;
				}
			}
		}

		public void RemoveImpact(int index = 0)
		{
			if (index >= 15 || index >= impactCount)
			{
				return;
			}
			for (int i = index; i < 16; i++)
			{
				if (i > 0)
				{
					impactPoints[i - 1] = impactPoints[i];
				}
			}
			impactCount--;
		}

		public void SetAnimOffset(float off)
		{
			animOffset = off;
		}

		public float GetAnimOffset()
		{
			return animOffset;
		}

		public static void SetParam(MaterialPropertyBlock pblock, SailParam sp)
		{
			if (!sp.active)
			{
				return;
			}
			switch (sp.id)
			{
			case SailParamID.Color:
				pblock.SetColor("_Color", sp.cval);
				break;
			case SailParamID.Albedo:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_MainTex", sp.tval);
				}
				break;
			case SailParamID.Speed:
				pblock.SetFloat("_Speed", sp.fval);
				break;
			case SailParamID.RippleNoise:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_RippleNoise", sp.tval);
				}
				break;
			case SailParamID.Emblem:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_EmblemTex", sp.tval);
				}
				break;
			case SailParamID.EmblemColor:
				pblock.SetColor("_EmblemColor", sp.cval);
				break;
			case SailParamID.EmblemEMColor:
				pblock.SetColor("_EmblemEMColor", sp.cval);
				break;
			case SailParamID.EmblemBackface:
				pblock.SetFloat("_EmblemBackface", sp.fval);
				break;
			case SailParamID.Damage:
				pblock.SetFloat("_Damage", sp.fval);
				break;
			case SailParamID.DamageTex:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_DamageTex", sp.tval);
				}
				break;
			case SailParamID.DamageTex1:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_DamageTex1", sp.tval);
				}
				break;
			case SailParamID.DamageTex2:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_DamageTex2", sp.tval);
				}
				break;
			case SailParamID.BumpMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_BumpMap", sp.tval);
				}
				break;
			case SailParamID.SpecColor:
				pblock.SetColor("_SpecColor", sp.cval);
				break;
			case SailParamID.Smoothness:
				pblock.SetFloat("_Smoothness", sp.fval);
				break;
			case SailParamID.AlphaCutoff:
				pblock.SetFloat("_AlphaCutoff", sp.fval);
				break;
			case SailParamID.WindDir:
				pblock.SetVector("_WindDir", sp.vval);
				break;
			case SailParamID.SailForward:
				pblock.SetVector("_SailForward", sp.vval);
				break;
			case SailParamID.MaskMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_MaskMap", sp.tval);
				}
				break;
			case SailParamID.MaskMapRev:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_MaskMapRev", sp.tval);
				}
				break;
			case SailParamID.SailWind:
				pblock.SetFloat("_SailWind", sp.fval);
				break;
			case SailParamID.FillPercent:
				pblock.SetFloat("_FillPercent", sp.fval);
				break;
			case SailParamID.SailLift:
				pblock.SetFloat("_SailLift", sp.fval);
				break;
			case SailParamID.SailLiftArch:
				pblock.SetFloat("_SailLiftArch", sp.fval);
				break;
			case SailParamID.SailLiftSideArch:
				pblock.SetFloat("_SailLiftSideArch", sp.fval);
				break;
			case SailParamID.SailReverse:
				pblock.SetFloat("_SailReverse", sp.fval);
				break;
			case SailParamID.SailTaper:
				pblock.SetFloat("_SailTaper", sp.fval);
				break;
			case SailParamID.SailShear:
				pblock.SetFloat("_SailShear", sp.fval);
				break;
			case SailParamID.SailTilt:
				pblock.SetFloat("_SailTilt", sp.fval);
				break;
			case SailParamID.SailTiltTop:
				pblock.SetFloat("_SailTiltTop", sp.fval);
				break;
			case SailParamID.SailArch:
				pblock.SetFloat("_SailArch", sp.fval);
				break;
			case SailParamID.SailTopArch:
				pblock.SetFloat("_SailTopArch", sp.fval);
				break;
			case SailParamID.SailArchStart:
				pblock.SetFloat("_SailArchStart", sp.fval);
				break;
			case SailParamID.SailSideArch:
				pblock.SetFloat("_SailSideArch", sp.fval);
				break;
			case SailParamID.FullRipple:
				pblock.SetFloat("_FullRipple", sp.fval);
				break;
			case SailParamID.Furl:
				pblock.SetFloat("_Furl", sp.fval);
				break;
			case SailParamID.FurlOrder:
				pblock.SetInt("_FurlOrder", sp.ival);
				break;
			case SailParamID.FurlMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_FurlMap", sp.tval);
				}
				break;
			case SailParamID.FurledMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_FurledMap", sp.tval);
				}
				break;
			case SailParamID.FurledRadius:
				pblock.SetFloat("_FurledRadius", sp.fval);
				break;
			case SailParamID.RippleStrength:
				pblock.SetFloat("_VorStrength", sp.fval);
				break;
			case SailParamID.RippleSpeed:
				pblock.SetFloat("_VorSpeed", sp.fval);
				break;
			case SailParamID.RippleSeed:
				pblock.SetFloat("_VorSeed", sp.fval);
				break;
			case SailParamID.RippleScale:
				pblock.SetFloat("_VorScale", sp.fval);
				break;
			case SailParamID.RippleSmooth:
				pblock.SetFloat("_VorSmoothness", sp.fval);
				break;
			case SailParamID.Thickness:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_Thickness", sp.tval);
				}
				break;
			case SailParamID.Power:
				pblock.SetFloat("_Power", sp.fval);
				break;
			case SailParamID.Distortion:
				pblock.SetFloat("_Distortion", sp.fval);
				break;
			case SailParamID.Scale:
				pblock.SetFloat("_Scale", sp.fval);
				break;
			case SailParamID.SubColor:
				pblock.SetColor("_SubColor", sp.cval);
				break;
			case SailParamID.SailSideways:
				pblock.SetFloat("_SailSideways", sp.fval);
				break;
			case SailParamID.ImpactCount:
				pblock.SetInt("_ImpactCount", sp.ival);
				break;
			case SailParamID.ImpactTex:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_ImpactTex", sp.tval);
				}
				break;
			case SailParamID.ImpactRipple:
				if ((bool)sp.tval)
				{
					pblock.SetTexture("_ImpactRipple", sp.tval);
				}
				break;
			case SailParamID.ImpactLocation:
				pblock.SetVector("_ImpactLocation", sp.vval);
				break;
			case SailParamID.YScale:
				pblock.SetFloat("_YScale", sp.fval);
				break;
			case SailParamID.EmblemTexST:
				pblock.SetVector("_EmblemTex_ST", sp.vval);
				break;
			case SailParamID.Transparent:
				pblock.SetFloat("_Transparent", sp.fval);
				break;
			case SailParamID.ImpactPoints:
				break;
			}
		}

		public static void SetParamFromID(MaterialPropertyBlock pblock, SailParam sp)
		{
			if (!sp.active)
			{
				return;
			}
			switch (sp.id)
			{
			case SailParamID.Color:
				pblock.SetColor(SailShaderIDs._Color, sp.cval);
				break;
			case SailParamID.Albedo:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._MainTex, sp.tval);
				}
				break;
			case SailParamID.Speed:
				pblock.SetFloat(SailShaderIDs._Speed, sp.fval);
				break;
			case SailParamID.RippleNoise:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._RippleNoise, sp.tval);
				}
				break;
			case SailParamID.Emblem:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._Emblem, sp.tval);
				}
				break;
			case SailParamID.EmblemColor:
				pblock.SetColor(SailShaderIDs._EmblemColor, sp.cval);
				break;
			case SailParamID.EmblemEMColor:
				pblock.SetColor(SailShaderIDs._EmblemEMColor, sp.cval);
				break;
			case SailParamID.EmblemBackface:
				pblock.SetFloat(SailShaderIDs._EmblemBackface, sp.fval);
				break;
			case SailParamID.Damage:
				pblock.SetFloat(SailShaderIDs._Damage, sp.fval);
				break;
			case SailParamID.DamageTex:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._DamageTex, sp.tval);
				}
				break;
			case SailParamID.DamageTex1:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._DamageTex1, sp.tval);
				}
				break;
			case SailParamID.DamageTex2:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._DamageTex2, sp.tval);
				}
				break;
			case SailParamID.BumpMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._BumpMap, sp.tval);
				}
				break;
			case SailParamID.SpecColor:
				pblock.SetColor(SailShaderIDs._SpecColor, sp.cval);
				break;
			case SailParamID.Smoothness:
				pblock.SetFloat(SailShaderIDs._Smoothness, sp.fval);
				break;
			case SailParamID.AlphaCutoff:
				pblock.SetFloat(SailShaderIDs._AlphaCutoff, sp.fval);
				break;
			case SailParamID.WindDir:
				pblock.SetVector(SailShaderIDs._WindDir, sp.vval);
				break;
			case SailParamID.SailForward:
				pblock.SetVector(SailShaderIDs._SailForward, sp.vval);
				break;
			case SailParamID.MaskMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._MaskMap, sp.tval);
				}
				break;
			case SailParamID.MaskMapRev:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._MaskMapRev, sp.tval);
				}
				break;
			case SailParamID.SailWind:
				pblock.SetFloat(SailShaderIDs._SailWind, sp.fval);
				break;
			case SailParamID.FillPercent:
				pblock.SetFloat(SailShaderIDs._FillPercent, sp.fval);
				break;
			case SailParamID.SailLift:
				pblock.SetFloat(SailShaderIDs._SailLift, sp.fval);
				break;
			case SailParamID.SailLiftArch:
				pblock.SetFloat(SailShaderIDs._SailLiftArch, sp.fval);
				break;
			case SailParamID.SailLiftSideArch:
				pblock.SetFloat(SailShaderIDs._SailLiftSideArch, sp.fval);
				break;
			case SailParamID.SailReverse:
				pblock.SetFloat(SailShaderIDs._SailReverse, sp.fval);
				break;
			case SailParamID.SailTaper:
				pblock.SetFloat(SailShaderIDs._SailTaper, sp.fval);
				break;
			case SailParamID.SailShear:
				pblock.SetFloat(SailShaderIDs._SailShear, sp.fval);
				break;
			case SailParamID.SailTilt:
				pblock.SetFloat(SailShaderIDs._SailTilt, sp.fval);
				break;
			case SailParamID.SailTiltTop:
				pblock.SetFloat(SailShaderIDs._SailTiltTop, sp.fval);
				break;
			case SailParamID.SailArch:
				pblock.SetFloat(SailShaderIDs._SailArch, sp.fval);
				break;
			case SailParamID.SailTopArch:
				pblock.SetFloat(SailShaderIDs._SailTopArch, sp.fval);
				break;
			case SailParamID.SailArchStart:
				pblock.SetFloat(SailShaderIDs._SailArchStart, sp.fval);
				break;
			case SailParamID.SailSideArch:
				pblock.SetFloat(SailShaderIDs._SailSideArch, sp.fval);
				break;
			case SailParamID.FullRipple:
				pblock.SetFloat(SailShaderIDs._FullRipple, sp.fval);
				break;
			case SailParamID.Furl:
				pblock.SetFloat(SailShaderIDs._Furl, sp.fval);
				break;
			case SailParamID.FurlOrder:
				pblock.SetInt(SailShaderIDs._FurlOrder, sp.ival);
				break;
			case SailParamID.FurlMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._FurlMap, sp.tval);
				}
				break;
			case SailParamID.FurledMap:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._FurledMap, sp.tval);
				}
				break;
			case SailParamID.FurledRadius:
				pblock.SetFloat(SailShaderIDs._FurledRadius, sp.fval);
				break;
			case SailParamID.RippleStrength:
				pblock.SetFloat(SailShaderIDs._RippleStrength, sp.fval);
				break;
			case SailParamID.RippleSpeed:
				pblock.SetFloat(SailShaderIDs._RippleSpeed, sp.fval);
				break;
			case SailParamID.RippleSeed:
				pblock.SetFloat(SailShaderIDs._RippleSeed, sp.fval);
				break;
			case SailParamID.RippleScale:
				pblock.SetFloat(SailShaderIDs._RippleScale, sp.fval);
				break;
			case SailParamID.RippleSmooth:
				pblock.SetFloat(SailShaderIDs._RippleSmooth, sp.fval);
				break;
			case SailParamID.Thickness:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._Thickness, sp.tval);
				}
				break;
			case SailParamID.Power:
				pblock.SetFloat(SailShaderIDs._Power, sp.fval);
				break;
			case SailParamID.Distortion:
				pblock.SetFloat(SailShaderIDs._Distortion, sp.fval);
				break;
			case SailParamID.Scale:
				pblock.SetFloat(SailShaderIDs._Scale, sp.fval);
				break;
			case SailParamID.SubColor:
				pblock.SetColor(SailShaderIDs._SubColor, sp.cval);
				break;
			case SailParamID.SailSideways:
				pblock.SetFloat(SailShaderIDs._SailSideways, sp.fval);
				break;
			case SailParamID.ImpactCount:
				pblock.SetInt(SailShaderIDs._ImpactCount, sp.ival);
				break;
			case SailParamID.ImpactTex:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._ImpactTex, sp.tval);
				}
				break;
			case SailParamID.ImpactRipple:
				if ((bool)sp.tval)
				{
					pblock.SetTexture(SailShaderIDs._ImpactRipple, sp.tval);
				}
				break;
			case SailParamID.ImpactLocation:
				pblock.SetVector(SailShaderIDs._ImpactLocation, sp.vval);
				break;
			case SailParamID.YScale:
				pblock.SetFloat(SailShaderIDs._YScale, sp.fval);
				break;
			case SailParamID.EmblemTexST:
				pblock.SetVector(SailShaderIDs._EmblemTex_ST, sp.vval);
				break;
			case SailParamID.Transparent:
				pblock.SetFloat(SailShaderIDs._Transparent, sp.fval);
				break;
			case SailParamID.ImpactPoints:
				break;
			}
		}

		public static string GetParamName(SailParamID id)
		{
			return id switch
			{
				SailParamID.Color => "_Color", 
				SailParamID.Albedo => "_MainTex", 
				SailParamID.Speed => "_Speed", 
				SailParamID.RippleNoise => "_RippleNoise", 
				SailParamID.Emblem => "_EmblemTex", 
				SailParamID.EmblemColor => "_EmblemColor", 
				SailParamID.EmblemEMColor => "_EmblemEMColor", 
				SailParamID.EmblemBackface => "_EmblemBackface", 
				SailParamID.Damage => "_Damage", 
				SailParamID.DamageTex => "_DamageTex", 
				SailParamID.DamageTex1 => "_DamageTex1", 
				SailParamID.DamageTex2 => "_DamageTex2", 
				SailParamID.BumpMap => "_BumpMap", 
				SailParamID.SpecColor => "_SpecColor", 
				SailParamID.Smoothness => "_Smoothness", 
				SailParamID.AlphaCutoff => "_AlphaCutoff", 
				SailParamID.WindDir => "_WindDir", 
				SailParamID.SailForward => "_SailForward", 
				SailParamID.MaskMap => "_MaskMap", 
				SailParamID.MaskMapRev => "_MaskMapRev", 
				SailParamID.SailWind => "_SailWind", 
				SailParamID.FillPercent => "_FillPercent", 
				SailParamID.SailLift => "_SailLift", 
				SailParamID.SailLiftArch => "_SailLiftArch", 
				SailParamID.SailLiftSideArch => "_SailLiftSideArch", 
				SailParamID.SailReverse => "_SailReverse", 
				SailParamID.SailTaper => "_SailTaper", 
				SailParamID.SailShear => "_SailShear", 
				SailParamID.SailTilt => "_SailTilt", 
				SailParamID.SailTiltTop => "_SailTiltTop", 
				SailParamID.SailArch => "_SailArch", 
				SailParamID.SailTopArch => "_SailTopArch", 
				SailParamID.SailArchStart => "_SailArchStart", 
				SailParamID.SailSideArch => "_SailSideArch", 
				SailParamID.FullRipple => "_FullRipple", 
				SailParamID.Furl => "_Furl", 
				SailParamID.FurlOrder => "_FurlOrder", 
				SailParamID.FurlMap => "_FurlMap", 
				SailParamID.FurledMap => "_FurledMap", 
				SailParamID.FurledRadius => "_FurledRadius", 
				SailParamID.RippleStrength => "_VorStrength", 
				SailParamID.RippleSpeed => "_VorSpeed", 
				SailParamID.RippleSeed => "_VorSeed", 
				SailParamID.RippleScale => "_VorScale", 
				SailParamID.RippleSmooth => "_VorSmoothness", 
				SailParamID.Thickness => "_Thickness", 
				SailParamID.Power => "_Power", 
				SailParamID.Distortion => "_Distortion", 
				SailParamID.Scale => "_Scale", 
				SailParamID.SubColor => "_SubColor", 
				SailParamID.SailSideways => "_SailSideways", 
				SailParamID.ImpactCount => "_ImpactCount", 
				SailParamID.ImpactPoints => "_ImpactPoints", 
				SailParamID.ImpactTex => "_ImpactTex", 
				SailParamID.ImpactRipple => "_ImpactRipple", 
				SailParamID.ImpactLocation => "_ImpactLocation", 
				SailParamID.EmblemTexST => "_EmblemTex_ST", 
				SailParamID.Transparent => "_Transparent", 
				_ => "", 
			};
		}

		public void SetValue(SailParamID id, float val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.fval = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, Texture2D val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.tval = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, int val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.ival = val;
				dirty = true;
			}
		}

		public void SetColor(SailParamID id, Color val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.cval = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, Vector4 val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.vval = val;
				dirty = true;
			}
		}

		public void SetDirty(bool _dirty)
		{
			dirty = _dirty;
		}

		public SailParam GetParam(SailParamID id)
		{
			for (int i = 0; i < sailParams.Count; i++)
			{
				if (sailParams[i].id == id)
				{
					return sailParams[i];
				}
			}
			return null;
		}

		private void Start()
		{
			visible = true;
			if (impactPoints == null || impactPoints.Length < 16)
			{
				impactPoints = new Vector4[16];
			}
			lodGroup = GetComponent<LODGroup>();
			if (!lodGroup)
			{
				mr = GetComponent<MeshRenderer>();
			}
			else
			{
				lods = lodGroup.GetLODs();
			}
			UpdateBounds();
		}

		public void UpdateBounds()
		{
			MeshFilter component = GetComponent<MeshFilter>();
			if (!component)
			{
				return;
			}
			Mesh sharedMesh = component.sharedMesh;
			if ((bool)sharedMesh)
			{
				Bounds bounds = sharedMesh.bounds;
				if (boundsSize.x == 0f)
				{
					boundsOffset = bounds.center;
					boundsSize = bounds.size;
				}
				bounds.size = boundsSize;
				bounds.center = boundsOffset;
				sharedMesh.bounds = bounds;
			}
		}

		public void DoUpdate(List<SailParam> groupParams, bool groupdirty)
		{
			if (!visible)
			{
				return;
			}
			if (pblock == null)
			{
				pblock = new MaterialPropertyBlock();
			}
			if (groupdirty || dirty)
			{
				pblock.Clear();
				for (int i = 0; i < groupParams.Count; i++)
				{
					SetParamFromID(pblock, groupParams[i]);
				}
				if (sailParams != null)
				{
					for (int j = 0; j < sailParams.Count; j++)
					{
						SetParamFromID(pblock, sailParams[j]);
					}
				}
				pblock.SetFloat(SailShaderIDs._RippleSeed, animOffset);
				Vector3 localScale = base.transform.localScale;
				pblock.SetFloat(SailShaderIDs._YScale, localScale.y);
				pblock.SetInt(SailShaderIDs._ImpactCount, impactCount);
				if (impactPoints.Length != 0)
				{
					pblock.SetVectorArray(SailShaderIDs._ImpactPoints, impactPoints);
				}
			}
			dirty = false;
			if ((bool)mr)
			{
				mr.SetPropertyBlock(pblock);
			}
			else
			{
				if (!lodGroup)
				{
					return;
				}
				if (lods == null)
				{
					lods = lodGroup.GetLODs();
				}
				for (int k = 0; k < lods.Length; k++)
				{
					for (int l = 0; l < lods[k].renderers.Length; l++)
					{
						lods[k].renderers[l].SetPropertyBlock(pblock);
					}
				}
			}
		}

		public Vector3 GetPointOnSail(float x, float y)
		{
			return Vector3.zero;
		}
	}
}
