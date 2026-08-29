using System;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class MaterialBase<TExtensions, TNormalTextureInfo, TOcclusionTextureInfo, TPbrMetallicRoughness, TTextureInfo, TTextureInfoExtensions> : MaterialBase where TExtensions : MaterialExtensions where TNormalTextureInfo : NormalTextureInfoBase where TOcclusionTextureInfo : OcclusionTextureInfoBase where TPbrMetallicRoughness : PbrMetallicRoughnessBase where TTextureInfo : TextureInfoBase where TTextureInfoExtensions : TextureInfoExtensions
	{
		public TTextureInfo emissiveTexture;

		public TExtensions extensions;

		public TNormalTextureInfo normalTexture;

		public TOcclusionTextureInfo occlusionTexture;

		public TPbrMetallicRoughness pbrMetallicRoughness;

		public override MaterialExtensions Extensions => extensions;

		public override PbrMetallicRoughnessBase PbrMetallicRoughness => pbrMetallicRoughness;

		public override NormalTextureInfoBase NormalTexture => normalTexture;

		public override OcclusionTextureInfoBase OcclusionTexture => occlusionTexture;

		public override TextureInfoBase EmissiveTexture => emissiveTexture;

		internal override void UnsetExtensions()
		{
			extensions = null;
		}
	}
	[Serializable]
	public abstract class MaterialBase : NamedObject
	{
		public enum AlphaMode
		{
			Opaque = 0,
			Mask = 1,
			Blend = 2
		}

		[Obsolete("Use Emissive for access.")]
		public float[] emissiveFactor = new float[3];

		[Obsolete("Use GetAlphaMode and SetAlphaMode for access.")]
		public string alphaMode;

		private AlphaMode? m_AlphaModeEnum;

		public float alphaCutoff = 0.5f;

		public bool doubleSided;

		public abstract MaterialExtensions Extensions { get; }

		public abstract PbrMetallicRoughnessBase PbrMetallicRoughness { get; }

		public abstract NormalTextureInfoBase NormalTexture { get; }

		public abstract OcclusionTextureInfoBase OcclusionTexture { get; }

		public abstract TextureInfoBase EmissiveTexture { get; }

		public Color Emissive
		{
			get
			{
				return new Color(emissiveFactor[0], emissiveFactor[1], emissiveFactor[2]);
			}
			set
			{
				emissiveFactor = new float[3] { value.r, value.g, value.b };
			}
		}

		public bool RequiresNormals => Extensions?.KHR_materials_unlit == null;

		public bool RequiresTangents
		{
			get
			{
				if (NormalTexture != null)
				{
					return NormalTexture.index >= 0;
				}
				return false;
			}
		}

		internal abstract void UnsetExtensions();

		public AlphaMode GetAlphaMode()
		{
			if (m_AlphaModeEnum.HasValue)
			{
				return m_AlphaModeEnum.Value;
			}
			m_AlphaModeEnum = (Enum.TryParse<AlphaMode>(alphaMode, ignoreCase: true, out var result) ? result : AlphaMode.Opaque);
			alphaMode = null;
			return m_AlphaModeEnum.Value;
		}

		public void SetAlphaMode(AlphaMode mode)
		{
			m_AlphaModeEnum = mode;
			alphaMode = null;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			if (PbrMetallicRoughness != null)
			{
				writer.AddProperty("pbrMetallicRoughness");
				PbrMetallicRoughness.GltfSerialize(writer);
			}
			if (NormalTexture != null)
			{
				writer.AddProperty("normalTexture");
				NormalTexture.GltfSerialize(writer);
			}
			if (OcclusionTexture != null)
			{
				writer.AddProperty("occlusionTexture");
				OcclusionTexture.GltfSerialize(writer);
			}
			if (EmissiveTexture != null)
			{
				writer.AddProperty("emissiveTexture");
				EmissiveTexture.GltfSerialize(writer);
			}
			if (emissiveFactor != null && (emissiveFactor[0] > 0.001f || emissiveFactor[1] > 0.001f || emissiveFactor[2] > 0.001f))
			{
				writer.AddArrayProperty("emissiveFactor", emissiveFactor);
			}
			if (m_AlphaModeEnum.HasValue && m_AlphaModeEnum.Value != AlphaMode.Opaque)
			{
				writer.AddProperty("alphaMode", m_AlphaModeEnum.Value.ToString().ToUpperInvariant());
			}
			if (math.abs(alphaCutoff - 0.5f) > 0.001f)
			{
				writer.AddProperty("alphaCutoff", alphaCutoff);
			}
			if (doubleSided)
			{
				writer.AddProperty("doubleSided", doubleSided);
			}
			if (Extensions != null)
			{
				writer.AddProperty("extensions");
				Extensions.GltfSerialize(writer);
			}
			writer.Close();
		}
	}
}
