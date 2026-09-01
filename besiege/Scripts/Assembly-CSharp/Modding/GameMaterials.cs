using System;
using System.Collections.Generic;
using InternalModding.Misc;
using UnityEngine;

namespace Modding
{
	public class GameMaterials : MonoBehaviour
	{
		public static class Shaders
		{
			public static BlockShaders Blocks
			{
				get
				{
					return Instance.blockShaders;
				}
			}

			public static BlockGhostShaders BlockGhosts
			{
				get
				{
					return Instance.blockGhostShaders;
				}
			}

			public static EntityShaders Entities
			{
				get
				{
					return Instance.entityShaders;
				}
			}

			public static ParticleShaders Particles
			{
				get
				{
					return Instance.particleShaders;
				}
			}

			public static MiscShaders Misc
			{
				get
				{
					return Instance.miscShaders;
				}
			}
		}

		[Serializable]
		public class BlockShaders
		{
			[SerializeField]
			private Shader _main;

			[SerializeField]
			private Shader _fireball;

			[SerializeField]
			private Shader _pin;

			[SerializeField]
			private Shader _camera;

			public Shader Main
			{
				get
				{
					return _main;
				}
			}

			public Shader Fireball
			{
				get
				{
					return _fireball;
				}
			}

			public Shader Pin
			{
				get
				{
					return _pin;
				}
			}

			public Shader Camera
			{
				get
				{
					return _camera;
				}
			}
		}

		[Serializable]
		public class BlockGhostShaders
		{
			[SerializeField]
			private Shader _transparentWithRim;

			[SerializeField]
			private Shader _transparentDiffuse;

			[SerializeField]
			private Shader _transparentDiffuseRim;

			[SerializeField]
			private Shader _transparentDiffuseRimBump;

			[SerializeField]
			private Shader _transparentSpecular;

			[SerializeField]
			private Shader _transparentSpecularBumped;

			public Shader TransparentWithRim
			{
				get
				{
					return _transparentWithRim;
				}
			}

			public Shader TransparentDiffuse
			{
				get
				{
					return _transparentDiffuse;
				}
			}

			public Shader TransparentDiffuseRim
			{
				get
				{
					return _transparentDiffuseRim;
				}
			}

			public Shader TransparentDiffuseRimBump
			{
				get
				{
					return _transparentDiffuseRimBump;
				}
			}

			public Shader TransparentSpecular
			{
				get
				{
					return _transparentSpecular;
				}
			}

			public Shader TransparentSpecularBumped
			{
				get
				{
					return _transparentSpecularBumped;
				}
			}
		}

		[Serializable]
		public class EntityShaders
		{
			[SerializeField]
			private Shader _pbs;

			[SerializeField]
			private Shader _block;

			[SerializeField]
			private Shader _foliage;

			[SerializeField]
			private Shader _diffuse;

			[SerializeField]
			private Shader _vertexLit;

			[SerializeField]
			private Shader _alphaBlended;

			public Shader PBS
			{
				get
				{
					return _pbs;
				}
			}

			public Shader BlockShader
			{
				get
				{
					return _block;
				}
			}

			public Shader Foliage
			{
				get
				{
					return _foliage;
				}
			}

			public Shader Diffuse
			{
				get
				{
					return _diffuse;
				}
			}

			public Shader VertexLit
			{
				get
				{
					return _vertexLit;
				}
			}

			public Shader AlphaBlended
			{
				get
				{
					return _alphaBlended;
				}
			}
		}

		[Serializable]
		public class ParticleShaders
		{
			[SerializeField]
			private Shader _additive;

			[SerializeField]
			private Shader _alphaBlended;

			[SerializeField]
			private Shader _stainedBumpDistort;

			public Shader Additive
			{
				get
				{
					return _additive;
				}
			}

			public Shader AlphaBlended
			{
				get
				{
					return _alphaBlended;
				}
			}

			public Shader StainedBumpDistort
			{
				get
				{
					return _stainedBumpDistort;
				}
			}
		}

		[Serializable]
		public class MiscShaders
		{
			[SerializeField]
			private Shader _loading;

			[SerializeField]
			private Shader _renTex;

			public Shader Loading
			{
				get
				{
					return _loading;
				}
			}

			public Shader RenderTextureCutout
			{
				get
				{
					return _renTex;
				}
			}
		}

		public enum BlockMaterial
		{
			WoodenBlock = 0,
			Wheel = 1,
			Fireball = 2,
			Pin = 3,
			Camera = 4
		}

		public enum BlockGhostMaterial
		{
			SmallWood = 0,
			Wheel = 1,
			Blade = 2,
			Decoupler = 3,
			WoodPanel = 4,
			Cannon = 5,
			Spike = 6,
			Bomb = 7,
			Fireball = 8
		}

		public enum EntityMaterial
		{
			Cottage = 0,
			Windmill = 1,
			Barrel = 2,
			DangerSign = 3,
			Bush = 4,
			IvyLeaves = 5,
			Cube = 6,
			StaticCloud = 7,
			BuildZone = 8,
			Trigger = 9
		}

		public enum ParticleMaterial
		{
			Fire = 0,
			VacuumDust = 1,
			WaterRefract = 2
		}

		public enum MiscMaterial
		{
			Loading = 0
		}

		[SerializeField]
		private BlockShaders blockShaders;

		[SerializeField]
		private BlockGhostShaders blockGhostShaders;

		[SerializeField]
		private EntityShaders entityShaders;

		[SerializeField]
		private ParticleShaders particleShaders;

		[SerializeField]
		private MiscShaders miscShaders;

		[SerializeField]
		private List<Material> blockMaterials;

		[SerializeField]
		private List<Material> blockGhostMaterials;

		[SerializeField]
		private List<Material> entityMaterials;

		[SerializeField]
		private List<Material> particleMaterials;

		[SerializeField]
		private List<Material> miscMaterials;

		private static GameMaterials Instance;

		public static Material GetMaterial(BlockMaterial mat)
		{
			return new Material(Instance.blockMaterials[(int)mat]);
		}

		public static Material GetMaterial(BlockGhostMaterial mat)
		{
			return new Material(Instance.blockGhostMaterials[(int)mat]);
		}

		public static Material GetMaterial(EntityMaterial mat)
		{
			return new Material(Instance.entityMaterials[(int)mat]);
		}

		public static Material GetMaterial(ParticleMaterial mat)
		{
			return new Material(Instance.particleMaterials[(int)mat]);
		}

		public static Material GetMaterial(MiscMaterial mat)
		{
			return new Material(Instance.miscMaterials[(int)mat]);
		}

		private void Awake()
		{
			if (Instance != null)
			{
				MLog.Error("Tried to construct second GameMaterials instance!");
				UnityEngine.Object.Destroy(this);
			}
			else
			{
				Instance = this;
			}
		}
	}
}
