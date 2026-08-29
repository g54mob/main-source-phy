using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[Serializable]
public class GridCustomPass : CustomPass
{
	[Serializable]
	public class Grid
	{
		[Tooltip("Is false, this grid is not rendered.")]
		public bool isShown = true;

		[Tooltip("Name of this grid.")]
		public string name = "Grid";

		[Tooltip("Distance between 2 neighbouring grid lines, defined for each axis.")]
		public Vector2 step = new Vector2(1f, 1f);

		[Tooltip("Number of steps from origin, defined for each axis.")]
		public Vector2Int size = new Vector2Int(10, 10);

		[Tooltip("Color of this grid.")]
		public Color color = Color.white;

		[Tooltip("Distance at which lines will start to fade.")]
		public float fadeStartDistance = 10f;

		[Tooltip("Distance at which lines will be completely faded.")]
		public float fadeEndDistance = 100f;

		[Tooltip("Material used to render lines of this grid.")]
		public Material lineMaterial;

		[NonSerialized]
		public Mesh mesh;

		[NonSerialized]
		public Material lineMaterialCopy;

		public Mesh generateMesh(PlaneSystem planeSystem, float scale)
		{
			int num = size.x * 2 + 1 + size.y * 2 + 1;
			Vector3[] array = new Vector3[num * 2];
			int[] array2 = new int[num * 2];
			int num2 = 0;
			for (int i = -size.x; i <= size.x; i++)
			{
				array[num2] = gridToPlanePosition(new Vector2((float)i * step.x * scale, (float)(-size.y) * step.y * scale), planeSystem);
				array2[num2] = num2;
				num2++;
				array[num2] = gridToPlanePosition(new Vector2((float)i * step.x * scale, (float)size.y * step.y * scale), planeSystem);
				array2[num2] = num2;
				num2++;
			}
			for (int j = -size.y; j <= size.y; j++)
			{
				array[num2] = gridToPlanePosition(new Vector2((float)(-size.x) * step.x * scale, (float)j * step.y * scale), planeSystem);
				array2[num2] = num2;
				num2++;
				array[num2] = gridToPlanePosition(new Vector2((float)size.x * step.x * scale, (float)j * step.y * scale), planeSystem);
				array2[num2] = num2;
				num2++;
			}
			Mesh obj = new Mesh();
			obj.name = name;
			obj.vertices = array;
			obj.SetIndices(array2, MeshTopology.Lines, 0);
			return obj;
		}

		private Vector3 gridToPlanePosition(Vector2 gridPosition, PlaneSystem planeSystem)
		{
			return planeSystem switch
			{
				PlaneSystem.XY => new Vector3(gridPosition.x, gridPosition.y, 0f), 
				PlaneSystem.XZ => new Vector3(gridPosition.x, 0f, gridPosition.y), 
				PlaneSystem.YZ => new Vector3(0f, gridPosition.x, gridPosition.y), 
				_ => Vector3.zero, 
			};
		}
	}

	[Tooltip("Describes all the grids that need to be rendered.")]
	public List<Grid> grids;

	[Header("Positioning")]
	[Tooltip("Defines where the viewer is and this information is used to render the grid around the viewer.")]
	public Camera camera;

	[Tooltip("Defines around which object should grid be rendered. If null, it is drawn around world origin.")]
	public Transform target;

	[Tooltip("Defines in which plane the grid is rendered.")]
	public PlaneSystem planeSystem = PlaneSystem.XZ;

	[Tooltip("Defines the scaling factor from default size of 1.")]
	[Min(0.001f)]
	public float scale = 1f;

	private Transform targetLastFrame;

	private PlaneSystem planeSystemLastFrame;

	private float scaleLastFrame;

	protected override void Execute(CustomPassContext context)
	{
		if ((target != targetLastFrame) | (planeSystem != planeSystemLastFrame) | (scale != scaleLastFrame))
		{
			generateGridMeshes();
			targetLastFrame = target;
			planeSystemLastFrame = planeSystem;
			scaleLastFrame = scale;
		}
		Matrix4x4 matrix = ((target != null) ? Matrix4x4.TRS(target.position, target.rotation, Vector3.one) : Matrix4x4.identity);
		foreach (Grid grid in grids)
		{
			if (grid.isShown && !(grid.mesh == null) && !(grid.lineMaterial == null))
			{
				if (grid.lineMaterialCopy == null)
				{
					grid.lineMaterialCopy = new Material(grid.lineMaterial);
				}
				grid.lineMaterialCopy.color = grid.color;
				grid.lineMaterialCopy.SetFloat("_FadeStart", grid.fadeStartDistance);
				grid.lineMaterialCopy.SetFloat("_FadeEnd", grid.fadeEndDistance);
				context.cmd.DrawMesh(grid.mesh, matrix, grid.lineMaterialCopy);
			}
		}
	}

	private void generateGridMeshes()
	{
		foreach (Grid grid in grids)
		{
			grid.mesh = grid.generateMesh(planeSystem, scale);
		}
	}

	protected override void Cleanup()
	{
		foreach (Grid grid in grids)
		{
			if (!(grid.mesh == null))
			{
				UnityEngine.Object.DestroyImmediate(grid.mesh);
			}
		}
	}
}
