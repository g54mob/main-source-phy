using UnityEngine;

[AddComponentMenu("Blocks/SailGhostController")]
public class SailGhostController : GhostMaterialController
{
	public Mesh defMesh;

	public Mesh sideMesh;

	private bool isSidePlacing;

	private void OnEnable()
	{
		if ((bool)visFilter)
		{
			SetGhostVis(visFilter.sharedMesh);
		}
	}

	public override void LateUpdate()
	{
		if (isSidePlacing != SailBlock.SidePlacing)
		{
			OnEnable();
		}
		base.LateUpdate();
	}

	public override void SetGhostVis(Mesh mesh)
	{
		if (mesh == defMesh || mesh == sideMesh)
		{
			mesh = ((!SailBlock.SidePlacing) ? defMesh : sideMesh);
			isSidePlacing = SailBlock.SidePlacing;
		}
		if ((bool)visFilter)
		{
			visFilter.sharedMesh = mesh;
		}
	}
}
