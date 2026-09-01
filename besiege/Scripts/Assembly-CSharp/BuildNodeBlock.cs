using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Surface/BuildNodeBlock")]
public class BuildNodeBlock : BlockBehaviour
{
	public float Radius = 0.18f;

	[HideInInspector]
	public Vector3 boundsCenter;

	protected override void Awake()
	{
		base.Awake();
		if (isSimulating)
		{
			VisualController.renderers[0].enabled = false;
		}
		else
		{
			StatMaster.hudHiddenChanged += OnHudHide;
		}
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.gameObject.SetActive(false);
	}

	private void OnHudHide()
	{
		VisualController.renderers[0].enabled = !StatMaster.hudHidden;
	}

	public override void OnMapperOpen()
	{
		if (StatMaster.KeyMapper.allowSelectingNodes)
		{
			base.OnMapperOpen();
			return;
		}
		List<BuildSurface> surfaces = base.ParentMachine.nodeController.GetSurfaces(this);
		if (surfaces.Count > 0)
		{
			BuildSurface buildSurface = surfaces.FirstOrDefault((BuildSurface x) => x.IsSelected);
			if (!buildSurface)
			{
				buildSurface = surfaces[0];
			}
			buildSurface.SetOutlineForMapper(true);
			BlockMapper.Open(buildSurface);
		}
		else
		{
			base.OnMapperOpen();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (!isSimulating)
		{
			Machine parentMachine = base.ParentMachine;
			parentMachine.UnregisterSurfaceBlock(this);
			StatMaster.hudHiddenChanged -= OnHudHide;
		}
	}

	public override Vector3 GetCenter()
	{
		return base.transform.position;
	}

	public void SetIsPreliminary(bool prelim)
	{
		VisualController.renderers[0].material = ((!prelim) ? VisualController.selectedSkin.material : ReferenceMaster.Instance.surfaceNodeGhost);
	}

	protected void OnBecameVisible()
	{
		base.ParentMachine.RegisterSurfaceBlock(this);
	}

	protected void OnBecameInvisible()
	{
		base.ParentMachine.UnregisterSurfaceBlock(this);
	}

	public bool RayHit(Ray ray, out float dist)
	{
		return NodeController.RaySphereIntersection(ray, base.transform.position, Radius * VisualController.MeshFilter.transform.localScale.x, out dist);
	}

	public List<ISelectable> GetEdgesToSelect(IEnumerable<ISelectable> currentSelection)
	{
		List<ISelectable> list = new List<ISelectable>();
		List<BuildSurface> surfaces = base.ParentMachine.nodeController.GetSurfaces(this);
		for (int i = 0; i < surfaces.Count; i++)
		{
			BuildSurface buildSurface = surfaces[i];
			if (!buildSurface.isValid)
			{
				continue;
			}
			BuildEdgeBlock[] edges = buildSurface.edges;
			foreach (BuildEdgeBlock buildEdgeBlock in edges)
			{
				if (buildEdgeBlock.isValid && ((buildEdgeBlock.startNode == this && currentSelection.Contains(buildEdgeBlock.endNode)) || (buildEdgeBlock.endNode == this && currentSelection.Contains(buildEdgeBlock.startNode))))
				{
					list.Add(buildEdgeBlock);
				}
			}
		}
		return list;
	}

	public override void SetPosition(Vector3 pos)
	{
		Vector3 position = Position;
		base.SetPosition(pos);
		Machine parentMachine = base.ParentMachine;
		if (position != Position || !parentMachine.isLocalMachine)
		{
			List<BuildSurface> surfaces = parentMachine.nodeController.GetSurfaces(this);
			for (int i = 0; i < surfaces.Count; i++)
			{
				BuildSurface buildSurface = surfaces[i];
				buildSurface.SurfaceChanged(this);
			}
		}
	}

	public override void OnLoad(XDataHolder data)
	{
	}

	public override void OnLoad(XDataHolder data, CopyMode mode)
	{
	}

	public override void OnSave(XDataHolder data)
	{
	}

	public override void OnSave(XDataHolder data, CopyMode mode)
	{
	}
}
