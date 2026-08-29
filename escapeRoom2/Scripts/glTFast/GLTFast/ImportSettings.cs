using System;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast
{
	[Serializable]
	public class ImportSettings
	{
		[SerializeField]
		[Tooltip("Controls how node names are created.")]
		private NameImportMethod nodeNameMethod;

		[SerializeField]
		[Tooltip("Target animation system.")]
		private AnimationMethod animationMethod = AnimationMethod.Legacy;

		[SerializeField]
		[Tooltip("Controls if mipmaps are created for imported textures.")]
		private bool generateMipMaps;

		[SerializeField]
		[Tooltip("Create textures readable. Increases memory consumption.")]
		private bool texturesReadable;

		[SerializeField]
		[Tooltip("Minification filter mode fallback if no mode was provided.")]
		private Sampler.MinFilterMode defaultMinFilterMode = Sampler.MinFilterMode.Linear;

		[SerializeField]
		[Tooltip("Magnification filter mode fallback if no mode was provided.")]
		private Sampler.MagFilterMode defaultMagFilterMode = Sampler.MagFilterMode.Linear;

		[SerializeField]
		[Tooltip("Anisotropic filtering level for imported textures.")]
		private int anisotropicFilterLevel = 1;

		public NameImportMethod NodeNameMethod
		{
			get
			{
				return nodeNameMethod;
			}
			set
			{
				nodeNameMethod = value;
			}
		}

		public AnimationMethod AnimationMethod
		{
			get
			{
				return animationMethod;
			}
			set
			{
				animationMethod = value;
			}
		}

		public bool GenerateMipMaps
		{
			get
			{
				return generateMipMaps;
			}
			set
			{
				generateMipMaps = value;
			}
		}

		public bool TexturesReadable
		{
			get
			{
				return texturesReadable;
			}
			set
			{
				texturesReadable = value;
			}
		}

		public Sampler.MinFilterMode DefaultMinFilterMode
		{
			get
			{
				return defaultMinFilterMode;
			}
			set
			{
				defaultMinFilterMode = value;
			}
		}

		public Sampler.MagFilterMode DefaultMagFilterMode
		{
			get
			{
				return defaultMagFilterMode;
			}
			set
			{
				defaultMagFilterMode = value;
			}
		}

		public int AnisotropicFilterLevel
		{
			get
			{
				return anisotropicFilterLevel;
			}
			set
			{
				anisotropicFilterLevel = value;
			}
		}
	}
}
