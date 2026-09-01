using System;
using UnityEngine;

namespace Shapes;

internal static class ShapesMeshUtils
{
	private static Mesh quadMesh;

	private static Mesh triangleMesh;

	private static Mesh sphereMesh;

	private static Mesh cuboidMesh;

	private static Mesh torusMesh;

	private static Mesh coneMesh;

	private static Mesh coneMeshUncapped;

	private static Mesh cylinderMesh;

	private static Mesh capsuleMesh;

	public static Mesh[] QuadMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshQuad;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] TriangleMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshTriangle;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] SphereMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshSphere;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] CuboidMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshCube;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] TorusMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshTorus;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] ConeMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshCone;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] ConeMeshUncapped
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshConeUncapped;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] CylinderMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshCylinder;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	public static Mesh[] CapsuleMesh
	{
		get
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				return instance.meshCapsule;
			}
			return (Mesh[])(object)new NullReferenceException();
		}
	}

	private unsafe static Mesh EnsureValidMeshBounds(Mesh mesh, Bounds bounds)
	{
		//IL_0038: Expected O, but got Ref
		if ((object)mesh != null)
		{
			mesh.hideFlags = HideFlags.HideInInspector;
			object obj = default(object);
			mesh.bounds = (Bounds)(&obj);
			return mesh;
		}
		return (Mesh)(object)new NullReferenceException();
	}

	public static Mesh GetLineMesh(LineGeometry geometry, LineEndCap endCaps, DetailLevel detail)
	{
		Mesh[] array;
		if (geometry <= LineGeometry.Billboard)
		{
			ShapesAssets instance = ShapesAssets.Instance;
			if ((object)instance != null)
			{
				Mesh[] meshQuad = instance.meshQuad;
				if (instance.meshQuad != null)
				{
					return meshQuad[0];
				}
			}
		}
		else
		{
			if (geometry != LineGeometry.Volumetric3D)
			{
				return null;
			}
			if (endCaps == LineEndCap.Round)
			{
				ShapesAssets instance2 = ShapesAssets.Instance;
				if ((object)instance2 != null)
				{
					array = instance2.meshCapsule;
					if (instance2.meshCapsule != null)
					{
						goto IL_0116;
					}
				}
			}
			else
			{
				ShapesAssets instance3 = ShapesAssets.Instance;
				if ((object)instance3 != null && instance3.meshCylinder != null)
				{
					array = instance3.meshCylinder;
					goto IL_0116;
				}
			}
		}
		return (Mesh)(object)new NullReferenceException();
		IL_0116:
		return array[(int)detail];
	}
}
