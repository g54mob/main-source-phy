using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fragment
{
	public GameObject Object;

	public MeshRenderer Renderer;

	public float Mass;

	public Particle[] Particles;

	public BuildSurface OriginalSurface;

	public List<BlockBehaviour> PossibleAttachments;

	public Vector3[] AttachmentRelativePositions;

	public Quaternion[] AttachmentRelativeRotations;

	public BlockBehaviour AttachedTo;

	public Action AttachedBreakCallback;

	public Vector3 AttachedRelativePosition;

	public FragmentParticleTrigger Trigger;

	public bool IsBroken;

	public bool IsIndependent;

	public bool HasOwnBody;

	public Rigidbody Body;

	public SurfaceFragmentProjectile Projectile;
}
