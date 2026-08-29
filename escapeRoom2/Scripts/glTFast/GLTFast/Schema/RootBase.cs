using System;
using System.Collections.Generic;
using System.IO;
using GLTFast.FakeSchema;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class RootBase<TAccessor, TAnimation, TAsset, TBuffer, TBufferView, TCamera, TExtensions, TImage, TMaterial, TMesh, TNode, TSampler, TScene, TSkin, TTexture> : RootBase where TAccessor : AccessorBase where TAnimation : AnimationBase where TAsset : Asset where TBuffer : Buffer where TBufferView : BufferViewBase where TCamera : CameraBase where TExtensions : RootExtensions where TImage : Image where TMaterial : MaterialBase where TMesh : MeshBase where TNode : NodeBase where TSampler : Sampler where TScene : Scene where TSkin : Skin where TTexture : TextureBase
	{
		public TAccessor[] accessors;

		public TAnimation[] animations;

		public TAsset asset;

		public TBuffer[] buffers;

		public TBufferView[] bufferViews;

		public TCamera[] cameras;

		public TImage[] images;

		public TMaterial[] materials;

		public TNode[] nodes;

		public TSampler[] samplers;

		public TScene[] scenes;

		public TSkin[] skins;

		public TTexture[] textures;

		public TExtensions extensions;

		public TMesh[] meshes;

		public override IReadOnlyList<AccessorBase> Accessors => accessors;

		public override IReadOnlyList<AnimationBase> Animations => animations;

		public override Asset Asset => asset;

		public override IReadOnlyList<Buffer> Buffers => buffers;

		public override IReadOnlyList<BufferViewBase> BufferViews => bufferViews;

		public override IReadOnlyList<CameraBase> Cameras => cameras;

		public override IReadOnlyList<Image> Images => images;

		public override IReadOnlyList<MaterialBase> Materials => materials;

		public override IReadOnlyList<NodeBase> Nodes => nodes;

		public override IReadOnlyList<Sampler> Samplers => samplers;

		public override IReadOnlyList<Scene> Scenes => scenes;

		public override IReadOnlyList<Skin> Skins => skins;

		public override IReadOnlyList<TextureBase> Textures => textures;

		public override RootExtensions Extensions => extensions;

		public override IReadOnlyList<MeshBase> Meshes => meshes;

		internal override void UnsetExtensions()
		{
			extensions = null;
		}
	}
	[Serializable]
	public abstract class RootBase
	{
		public string[] extensionsUsed;

		public string[] extensionsRequired;

		public int scene = -1;

		public abstract IReadOnlyList<AccessorBase> Accessors { get; }

		public abstract IReadOnlyList<AnimationBase> Animations { get; }

		public abstract Asset Asset { get; }

		public abstract IReadOnlyList<Buffer> Buffers { get; }

		public abstract IReadOnlyList<BufferViewBase> BufferViews { get; }

		public abstract IReadOnlyList<CameraBase> Cameras { get; }

		public abstract IReadOnlyList<Image> Images { get; }

		public abstract IReadOnlyList<MaterialBase> Materials { get; }

		public abstract IReadOnlyList<MeshBase> Meshes { get; }

		public abstract IReadOnlyList<NodeBase> Nodes { get; }

		public abstract IReadOnlyList<Sampler> Samplers { get; }

		public abstract IReadOnlyList<Scene> Scenes { get; }

		public abstract IReadOnlyList<Skin> Skins { get; }

		public abstract IReadOnlyList<TextureBase> Textures { get; }

		public abstract RootExtensions Extensions { get; }

		public bool HasAnimation
		{
			get
			{
				if (Animations != null)
				{
					return Animations.Count > 0;
				}
				return false;
			}
		}

		public int MaterialsVariantsCount => (Extensions?.KHR_materials_variants?.variants?.Count).GetValueOrDefault();

		internal abstract void UnsetExtensions();

		public bool IsAccessorInterleaved(int accessorIndex)
		{
			AccessorBase accessorBase = Accessors[accessorIndex];
			BufferViewBase bufferViewBase = BufferViews[accessorBase.bufferView];
			if (bufferViewBase.byteStride < 0)
			{
				return false;
			}
			return bufferViewBase.byteStride > accessorBase.ElementByteSize;
		}

		public void GltfSerialize(StreamWriter stream)
		{
			JsonWriter jsonWriter = new JsonWriter(stream);
			if (Asset != null)
			{
				jsonWriter.AddProperty("asset");
				Asset.GltfSerialize(jsonWriter);
			}
			if (Nodes != null)
			{
				jsonWriter.AddArray("nodes");
				foreach (NodeBase node in Nodes)
				{
					node.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (extensionsRequired != null)
			{
				jsonWriter.AddArrayProperty("extensionsRequired", extensionsRequired);
			}
			if (extensionsUsed != null)
			{
				jsonWriter.AddArrayProperty("extensionsUsed", extensionsUsed);
			}
			if (Animations != null)
			{
				jsonWriter.AddArray("animations");
				foreach (AnimationBase animation in Animations)
				{
					animation.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Buffers != null)
			{
				jsonWriter.AddArray("buffers");
				foreach (Buffer buffer in Buffers)
				{
					buffer.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (BufferViews != null)
			{
				jsonWriter.AddArray("bufferViews");
				foreach (BufferViewBase bufferView in BufferViews)
				{
					bufferView.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Accessors != null)
			{
				jsonWriter.AddArray("accessors");
				foreach (AccessorBase accessor in Accessors)
				{
					accessor.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Cameras != null)
			{
				jsonWriter.AddArray("cameras");
				foreach (CameraBase camera in Cameras)
				{
					camera.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Images != null)
			{
				jsonWriter.AddArray("images");
				foreach (Image image in Images)
				{
					image?.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Materials != null)
			{
				jsonWriter.AddArray("materials");
				foreach (MaterialBase material in Materials)
				{
					material.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Meshes != null)
			{
				jsonWriter.AddArray("meshes");
				foreach (MeshBase mesh in Meshes)
				{
					mesh.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Samplers != null)
			{
				jsonWriter.AddArray("samplers");
				foreach (Sampler sampler in Samplers)
				{
					sampler.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (scene >= 0)
			{
				jsonWriter.AddProperty("scene", scene);
			}
			if (Scenes != null)
			{
				jsonWriter.AddArray("scenes");
				foreach (Scene scene in Scenes)
				{
					scene.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Skins != null)
			{
				jsonWriter.AddArray("skins");
				foreach (Skin skin in Skins)
				{
					skin.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Textures != null)
			{
				jsonWriter.AddArray("textures");
				foreach (TextureBase texture in Textures)
				{
					texture.GltfSerialize(jsonWriter);
				}
				jsonWriter.CloseArray();
			}
			if (Extensions != null)
			{
				jsonWriter.AddProperty("extensions");
				Extensions.GltfSerialize(jsonWriter);
			}
			jsonWriter.Close();
		}

		internal bool JsonUtilitySecondParseRequired()
		{
			bool result = false;
			if (Materials != null)
			{
				foreach (MaterialBase material in Materials)
				{
					if (material.Extensions.KHR_materials_unlit != null)
					{
						result = true;
					}
					else
					{
						material.UnsetExtensions();
					}
				}
			}
			if (Accessors != null)
			{
				foreach (AccessorBase accessor in Accessors)
				{
					if (accessor.Sparse.Indices == null || accessor.Sparse.Values == null)
					{
						accessor.UnsetSparse();
					}
				}
			}
			return result;
		}

		internal void JsonUtilityCleanupAgainstSecondParse(GLTFast.FakeSchema.Root fakeRoot)
		{
			if (Materials != null)
			{
				for (int i = 0; i < Materials.Count; i++)
				{
					MaterialBase materialBase = Materials[i];
					if (materialBase.Extensions != null)
					{
						MaterialExtension extensions = fakeRoot.materials[i].extensions;
						if (extensions.KHR_materials_unlit == null)
						{
							materialBase.Extensions.KHR_materials_unlit = null;
						}
						if (extensions.KHR_materials_pbrSpecularGlossiness == null)
						{
							materialBase.Extensions.KHR_materials_pbrSpecularGlossiness = null;
						}
						if (extensions.KHR_materials_transmission == null)
						{
							materialBase.Extensions.KHR_materials_transmission = null;
						}
						if (extensions.KHR_materials_clearcoat == null)
						{
							materialBase.Extensions.KHR_materials_clearcoat = null;
						}
						if (extensions.KHR_materials_sheen == null)
						{
							materialBase.Extensions.KHR_materials_sheen = null;
						}
						if (extensions.KHR_materials_ior == null)
						{
							materialBase.Extensions.KHR_materials_ior = null;
						}
						if (extensions.KHR_materials_specular == null)
						{
							materialBase.Extensions.KHR_materials_specular = null;
						}
					}
				}
			}
			if (Meshes == null)
			{
				return;
			}
			for (int j = 0; j < Meshes.Count; j++)
			{
				MeshBase meshBase = Meshes[j];
				for (int k = 0; k < meshBase.Primitives.Count; k++)
				{
					MeshPrimitiveBase meshPrimitiveBase = meshBase.Primitives[k];
					if (meshPrimitiveBase.Extensions != null && fakeRoot.meshes[j].primitives[k].extensions.KHR_materials_variants == null)
					{
						meshPrimitiveBase.Extensions.KHR_materials_variants = null;
					}
				}
			}
		}

		public virtual void JsonUtilityCleanup()
		{
			if (Nodes != null)
			{
				foreach (NodeBase node in Nodes)
				{
					node.JsonUtilityCleanup();
				}
			}
			if (Extensions != null && !Extensions.JsonUtilityCleanup())
			{
				UnsetExtensions();
			}
			if (Textures == null)
			{
				return;
			}
			foreach (TextureBase texture in Textures)
			{
				texture.JsonUtilityCleanup();
			}
		}

		public string GetMaterialsVariantName(int index)
		{
			List<MaterialsVariant> list = Extensions?.KHR_materials_variants?.variants;
			if (list != null && index >= 0 && index < list.Count)
			{
				return list[index].name;
			}
			return null;
		}
	}
}
