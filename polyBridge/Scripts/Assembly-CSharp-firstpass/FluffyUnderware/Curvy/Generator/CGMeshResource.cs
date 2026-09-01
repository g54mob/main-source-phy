using System;
using FluffyUnderware.DevTools;
using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class CGMeshResource : DuplicateEditorMesh, IPoolable
	{
		private MeshRenderer mRenderer;

		private Collider mCollider;

		public MeshRenderer Renderer
		{
			get
			{
				if (mRenderer == null)
				{
					mRenderer = GetComponent<MeshRenderer>();
				}
				return mRenderer;
			}
		}

		public Collider Collider
		{
			get
			{
				if (mCollider == null)
				{
					mCollider = GetComponent<Collider>();
				}
				return mCollider;
			}
		}

		public Mesh Prepare()
		{
			return base.Filter.PrepareNewShared();
		}

		public bool ColliderMatches(CGColliderEnum type)
		{
			if (Collider == null && type == CGColliderEnum.None)
			{
				return true;
			}
			if (Collider is MeshCollider && type == CGColliderEnum.Mesh)
			{
				return true;
			}
			if (Collider is BoxCollider && type == CGColliderEnum.Box)
			{
				return true;
			}
			if (Collider is SphereCollider && type == CGColliderEnum.Sphere)
			{
				return true;
			}
			return false;
		}

		public void RemoveCollider()
		{
			if ((bool)Collider)
			{
				if (Application.isPlaying)
				{
					UnityEngine.Object.Destroy(mCollider);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(mCollider);
				}
				mCollider = null;
			}
		}

		public bool UpdateCollider(CGColliderEnum mode, bool convex, PhysicMaterial material, MeshColliderCookingOptions meshCookingOptions = MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices)
		{
			if (Collider == null)
			{
				switch (mode)
				{
				case CGColliderEnum.Mesh:
					mCollider = base.gameObject.AddComponent<MeshCollider>();
					break;
				case CGColliderEnum.Box:
					mCollider = base.gameObject.AddComponent<BoxCollider>();
					break;
				case CGColliderEnum.Sphere:
					mCollider = base.gameObject.AddComponent<SphereCollider>();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				case CGColliderEnum.None:
					break;
				}
			}
			switch (mode)
			{
			case CGColliderEnum.Mesh:
			{
				MeshCollider meshCollider = Collider as MeshCollider;
				if (meshCollider != null)
				{
					meshCollider.sharedMesh = null;
					meshCollider.convex = convex;
					meshCollider.cookingOptions = meshCookingOptions;
					try
					{
						meshCollider.sharedMesh = base.Filter.sharedMesh;
					}
					catch
					{
						return false;
					}
				}
				else
				{
					DTLog.LogError("[Curvy] Collider of wrong type");
				}
				goto IL_01ad;
			}
			case CGColliderEnum.Box:
			{
				BoxCollider boxCollider = Collider as BoxCollider;
				if (boxCollider != null)
				{
					boxCollider.center = base.Filter.sharedMesh.bounds.center;
					boxCollider.size = base.Filter.sharedMesh.bounds.size;
				}
				else
				{
					DTLog.LogError("[Curvy] Collider of wrong type");
				}
				goto IL_01ad;
			}
			case CGColliderEnum.Sphere:
			{
				SphereCollider sphereCollider = Collider as SphereCollider;
				if (sphereCollider != null)
				{
					sphereCollider.center = base.Filter.sharedMesh.bounds.center;
					sphereCollider.radius = base.Filter.sharedMesh.bounds.extents.magnitude;
				}
				else
				{
					DTLog.LogError("[Curvy] Collider of wrong type");
				}
				goto IL_01ad;
			}
			default:
				throw new ArgumentOutOfRangeException();
			case CGColliderEnum.None:
				break;
				IL_01ad:
				Collider.material = material;
				break;
			}
			return true;
		}

		public void OnBeforePush()
		{
		}

		public void OnAfterPop()
		{
		}
	}
}
