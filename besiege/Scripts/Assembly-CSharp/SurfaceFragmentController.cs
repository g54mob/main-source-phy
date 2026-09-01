using System;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceFragmentController : MonoBehaviour
{
	public BuildSurface mySurface;

	public Fragment[] fragments;

	private bool fragmentsActivated;

	private bool hasBreakImpulses;

	private Vector3 breakVelocityToSet;

	private bool hasBreakExplosion;

	private Vector3 explosionPos;

	private float power;

	private float torque;

	private float upPower;

	private float explosionRadius;

	public bool FragmentsActivated
	{
		get
		{
			return fragmentsActivated;
		}
	}

	protected Transform ParentMachine
	{
		get
		{
			return base.transform.parent;
		}
	}

	public event Action OnBreak;

	public void Initialize()
	{
		for (int i = 0; i < fragments.Length; i++)
		{
			fragments[i].OriginalSurface = mySurface;
		}
	}

	public void UpdateMass(float totalMass)
	{
		if (fragments != null && fragments.Length != 0)
		{
			float mass = totalMass / (float)fragments.Length;
			for (int i = 0; i < fragments.Length; i++)
			{
				fragments[i].Mass = mass;
			}
		}
	}

	public void OnConnectionEstablished(int index, List<BlockBehaviour> allConsideredAttachments)
	{
		if (index >= fragments.Length)
		{
			return;
		}
		Fragment fragment = fragments[index];
		fragment.PossibleAttachments = allConsideredAttachments;
		fragment.AttachmentRelativePositions = new Vector3[allConsideredAttachments.Count];
		fragment.AttachmentRelativeRotations = new Quaternion[allConsideredAttachments.Count];
		for (int i = 0; i < allConsideredAttachments.Count; i++)
		{
			Transform parentingTransform = allConsideredAttachments[i].ParentingTransform;
			fragment.AttachmentRelativePositions[i] = parentingTransform.InverseTransformPoint(fragment.Object.transform.position);
			fragment.AttachmentRelativeRotations[i] = Quaternion.Inverse(parentingTransform.rotation) * fragment.Object.transform.rotation;
		}
		AttachFragmentSomewhere(fragment);
		BuildNodeBlock buildNodeBlock = mySurface.nodes[index];
		BuildEdgeBlock buildEdgeBlock = null;
		BuildEdgeBlock buildEdgeBlock2 = null;
		int edgeIndex = -1;
		int edgeIndex2 = -1;
		for (int j = 0; j < mySurface.edges.Length; j++)
		{
			BuildEdgeBlock buildEdgeBlock3 = mySurface.edges[j];
			if (buildEdgeBlock3.isValid && (buildEdgeBlock3.startNode == buildNodeBlock || buildEdgeBlock3.endNode == buildNodeBlock))
			{
				if (!(buildEdgeBlock == null))
				{
					buildEdgeBlock2 = buildEdgeBlock3;
					edgeIndex2 = j;
					break;
				}
				buildEdgeBlock = buildEdgeBlock3;
				edgeIndex = j;
			}
		}
		for (int k = 0; k < allConsideredAttachments.Count; k++)
		{
			BuildSurface buildSurface = allConsideredAttachments[k] as BuildSurface;
			if (buildSurface == null || !buildSurface.isValid)
			{
				continue;
			}
			for (int l = 0; l < buildSurface.edges.Length; l++)
			{
				if (buildSurface.edges[l] == buildEdgeBlock)
				{
					AssignFragmentStickyParticles(fragment, edgeIndex, buildSurface);
				}
				else if (buildSurface.edges[l] == buildEdgeBlock2)
				{
					AssignFragmentStickyParticles(fragment, edgeIndex2, buildSurface);
				}
			}
		}
	}

	public void CalculateBreakImpulses(Collision collision)
	{
		Vector3 lastVelocity = mySurface.lastVelocity;
		if (collision.rigidbody == null)
		{
			breakVelocityToSet = lastVelocity;
			hasBreakImpulses = true;
			return;
		}
		Vector3 velocity = collision.rigidbody.velocity;
		Vector3 vector = velocity - lastVelocity;
		Vector3 vector2 = vector * collision.rigidbody.mass;
		Vector3 vector3 = vector2 / fragments.Length;
		Vector3 vector4 = vector3 / fragments[0].Mass;
		breakVelocityToSet = lastVelocity + vector4;
		hasBreakImpulses = true;
		hasBreakExplosion = false;
	}

	public void CalculateBreakImpulses(float power, float upPower, float torquePower, Vector3 explosionPos, float radius)
	{
		hasBreakExplosion = true;
		hasBreakImpulses = false;
		this.power = power * mySurface.fragmentExplosionForceMultiplier;
		this.upPower = upPower * mySurface.fragmentExplosionForceMultiplier;
		torque = torquePower * mySurface.fragmentExplosionForceMultiplier;
		this.explosionPos = explosionPos;
		explosionRadius = radius;
	}

	public void OnRemoteBreak()
	{
		if (fragmentsActivated)
		{
			return;
		}
		fragmentsActivated = true;
		for (int i = 0; i < fragments.Length; i++)
		{
			Fragment fragment = fragments[i];
			if (!fragment.IsIndependent && ValidateFragment(fragment))
			{
				fragment.Object.SetActive(true);
			}
		}
		if (this.OnBreak != null)
		{
			this.OnBreak();
		}
	}

	private bool ValidateFragment(Fragment fragment)
	{
		bool flag = fragment.AttachedTo == null || fragment.AttachedTo.IsDestroyed;
		if (!flag)
		{
			Vector3 attachedRelativePosition = fragment.AttachedRelativePosition;
			Vector3 vector = fragment.AttachedTo.transform.InverseTransformPoint(mySurface.transform.position);
			if ((attachedRelativePosition - vector).sqrMagnitude > 0.2f)
			{
				flag = true;
			}
		}
		else if (!mySurface.SimPhysics)
		{
			SurfaceFragmentController component = fragment.Object.transform.parent.GetComponent<SurfaceFragmentController>();
			if (component != null && component.fragmentsActivated)
			{
				return false;
			}
		}
		if (flag && !AttachFragmentSomewhere(fragment))
		{
			return false;
		}
		return true;
	}

	public void OnSurfaceBreak()
	{
		if (fragmentsActivated)
		{
			return;
		}
		fragmentsActivated = true;
		for (int i = 0; i < fragments.Length; i++)
		{
			if (!fragments[i].IsIndependent && !ValidateFragment(fragments[i]))
			{
				MakeFragmentIndependent(fragments[i]);
			}
			AddParticleTrigger(fragments[i]);
			fragments[i].Object.SetActive(true);
			if (fragments[i].IsIndependent && fragments[i].HasOwnBody)
			{
				SpawnFragmentAsProjectile(i);
				ApplyFragmentInheritedVelocity(fragments[i], this);
			}
		}
		if (this.OnBreak != null)
		{
			this.OnBreak();
		}
	}

	private void SpawnFragmentAsProjectile(int i)
	{
		if (StatMaster.isMP && StatMaster.isHosting && !StatMaster.isLocalSim)
		{
			ProjectileManager instance = ProjectileManager.Instance;
			byte[] array = new byte[18];
			int num = 0;
			Transform transform = fragments[i].Object.transform;
			NetworkCompression.CompressPosition(transform.position, array, num);
			num += 6;
			NetworkCompression.CompressRotation(transform.rotation, array, num);
			num += 7;
			NetworkCompression.WriteUInt((uint)mySurface.BuildIndex, false, array, num);
			num += 4;
			array[num] = (byte)i;
			instance.Spawn(NetworkProjectileType.SurfaceFragment, (SingleInstanceFindOnly<AddPiece>.Instance as NetworkAddPiece).frame, mySurface.ParentMachine.PlayerID, array);
		}
	}

	private int GetFragmentIndex(Fragment f)
	{
		for (int i = 0; i < fragments.Length; i++)
		{
			if (fragments[i] == f)
			{
				return i;
			}
		}
		return -1;
	}

	private void OnSurfaceWithFragmentBreak(Fragment fragment)
	{
		if (fragment.IsBroken || fragment.Object == null)
		{
			return;
		}
		BuildSurface buildSurface = (BuildSurface)fragment.AttachedTo;
		bool simPhysics = buildSurface.SimPhysics;
		buildSurface.visAddedToMe.Remove(fragment.Renderer);
		if (!AttachFragmentSomewhere(fragment))
		{
			if (fragment.Object.activeSelf)
			{
				if (simPhysics)
				{
					MakeFragmentIndependent(fragment);
					int fragmentIndex = GetFragmentIndex(fragment);
					if (fragmentIndex != -1)
					{
						SpawnFragmentAsProjectile(fragmentIndex);
					}
				}
			}
			else if (!fragment.IsIndependent)
			{
				TakeFragmentBack(fragment);
			}
		}
		if (simPhysics)
		{
			if (fragment.Object.activeSelf)
			{
				AddParticleTrigger(fragment);
			}
			if (fragment.IsIndependent && fragment.Object.activeSelf && fragment.HasOwnBody)
			{
				ApplyFragmentInheritedVelocity(fragment, buildSurface.FragmentController);
			}
		}
		else if (fragment.Object.activeSelf)
		{
			fragment.Object.SetActive(false);
		}
	}

	private void ApplyFragmentInheritedVelocity(Fragment fragment, SurfaceFragmentController fragParent)
	{
		if (fragParent.hasBreakImpulses)
		{
			fragment.Body.velocity = fragParent.breakVelocityToSet;
			fragment.Body.angularVelocity = fragParent.mySurface.lastAngularVelocity;
		}
		else if (fragParent.hasBreakExplosion)
		{
			fragment.Body.AddExplosionForce(fragParent.power, fragParent.explosionPos, fragParent.explosionRadius, fragParent.upPower);
			fragment.Body.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * fragParent.torque);
		}
		else
		{
			fragment.Body.velocity = fragParent.mySurface.lastVelocity;
			fragment.Body.angularVelocity = fragParent.mySurface.lastAngularVelocity;
		}
	}

	private void OnSurfaceWithStickyParticleBreak(Particle particle)
	{
		if (!(particle.Object == null) && !particle.HasRigidbody)
		{
			BuildSurface stickyAttachment = particle.StickyAttachment;
			particle.Object.transform.SetParent(ParentMachine);
			particle.CreateRigidbody();
			particle.Body.velocity = stickyAttachment.lastVelocity;
			particle.Body.angularVelocity = stickyAttachment.lastAngularVelocity;
			particle.StartAnimation(this);
		}
	}

	public void OnRemoteFragmentBreak(int index)
	{
		OnFragmentBreak(fragments[index]);
	}

	private void OnFragmentBreak(Fragment fragment)
	{
		Transform parentMachine = ParentMachine;
		bool simPhysics = mySurface.SimPhysics;
		if (StatMaster.isMP && simPhysics)
		{
			int num = -1;
			for (int i = 0; i < fragments.Length; i++)
			{
				if (fragments[i] == fragment)
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				if (fragment.Projectile != null)
				{
					ProjectileManager.Instance.Despawn(fragment.Projectile);
				}
				else
				{
					NetworkBlock netBlock = mySurface.NetBlock;
					if (netBlock != null)
					{
						netBlock.Event(NetworkEntity.EntityEvent.SurfaceFragmentBreak, (byte)num);
					}
				}
			}
		}
		for (int j = 0; j < fragment.Particles.Length; j++)
		{
			Particle particle = fragment.Particles[j];
			if (particle.IsStickySide)
			{
				BuildSurface stickyAttachment = particle.StickyAttachment;
				if (stickyAttachment != null && !stickyAttachment.IsDestroyed && (stickyAttachment.FragmentController == null || !stickyAttachment.FragmentController.fragmentsActivated))
				{
					particle.Object.transform.SetParent(stickyAttachment.ParentingTransform, true);
					particle.Object.SetActive(true);
					if (stickyAttachment.FragmentController != null)
					{
						SurfaceFragmentController fragmentController = stickyAttachment.FragmentController;
						fragmentController.OnBreak = (Action)Delegate.Combine(fragmentController.OnBreak, (Action)delegate
						{
							OnSurfaceWithStickyParticleBreak(particle);
						});
					}
					continue;
				}
				particle.CreateRigidbody();
			}
			particle.Object.transform.SetParent(parentMachine, true);
			particle.Object.SetActive(true);
			if (simPhysics)
			{
				particle.Body.velocity = fragment.Body.velocity;
				particle.Body.angularVelocity = fragment.Body.angularVelocity;
			}
			else
			{
				Collider[] componentsInChildren = particle.Object.GetComponentsInChildren<Collider>();
				for (int num2 = 0; num2 < componentsInChildren.Length; num2++)
				{
					componentsInChildren[num2].enabled = true;
				}
				if (particle.Body == null)
				{
					particle.HasRigidbody = false;
					particle.CreateRigidbody();
				}
				NetworkBlock networkBlock = ((!(fragment.Projectile != null)) ? fragment.Object.GetComponentInParent<NetworkBlock>() : fragment.Projectile);
				particle.Body.velocity = networkBlock.Velocity;
			}
			particle.StartAnimation(this);
		}
		UnityEngine.Object.Destroy(fragment.Object);
		UnityEngine.Object.Destroy(fragment.Trigger);
		fragment.IsBroken = true;
	}

	private bool AttachFragmentSomewhere(Fragment fragment)
	{
		if (fragment.PossibleAttachments == null)
		{
			return false;
		}
		BlockBehaviour blockBehaviour = null;
		Vector3 localPosition = Vector3.zero;
		Quaternion localRotation = Quaternion.identity;
		for (int i = 0; i < fragment.PossibleAttachments.Count; i++)
		{
			BlockBehaviour blockBehaviour2 = fragment.PossibleAttachments[i];
			if (blockBehaviour2 == null || blockBehaviour2.IsDestroyed)
			{
				continue;
			}
			SurfaceFragmentController component = blockBehaviour2.GetComponent<SurfaceFragmentController>();
			if (!(component != null) || !component.fragmentsActivated)
			{
				Vector3 vector = fragment.AttachmentRelativePositions[i];
				Quaternion quaternion = fragment.AttachmentRelativeRotations[i];
				Vector3 vector2 = blockBehaviour2.transform.InverseTransformPoint(mySurface.transform.position);
				if (!((vector - vector2).sqrMagnitude > 0.2f))
				{
					blockBehaviour = blockBehaviour2;
					localPosition = vector;
					localRotation = quaternion;
					break;
				}
			}
		}
		if (blockBehaviour == null)
		{
			return false;
		}
		AttachFragmentTo(fragment, blockBehaviour, localPosition, localRotation);
		return true;
	}

	private void AttachFragmentTo(Fragment fragment, BlockBehaviour attachment, Vector3 localPosition, Quaternion localRotation)
	{
		if (fragment.AttachedTo != null && fragment.AttachedBreakCallback != null)
		{
			SurfaceFragmentController component = fragment.AttachedTo.GetComponent<SurfaceFragmentController>();
			component.OnBreak = (Action)Delegate.Remove(component.OnBreak, fragment.AttachedBreakCallback);
			fragment.AttachedBreakCallback = null;
		}
		Transform parentingTransform = attachment.ParentingTransform;
		fragment.AttachedTo = attachment;
		fragment.Object.transform.SetParent(parentingTransform);
		fragment.Object.transform.localPosition = localPosition;
		fragment.Object.transform.localRotation = localRotation;
		fragment.AttachedRelativePosition = localPosition;
		if (!attachment.SimPhysics)
		{
			attachment.CreateSimLists();
		}
		attachment.visAddedToMe.Add(fragment.Renderer);
		SurfaceFragmentController component2 = attachment.GetComponent<SurfaceFragmentController>();
		if (component2 != null)
		{
			fragment.AttachedBreakCallback = delegate
			{
				OnSurfaceWithFragmentBreak(fragment);
			};
			component2.OnBreak = (Action)Delegate.Combine(component2.OnBreak, fragment.AttachedBreakCallback);
		}
	}

	private void TakeFragmentBack(Fragment fragment)
	{
		if (fragment.AttachedTo != null && fragment.AttachedBreakCallback != null)
		{
			SurfaceFragmentController component = fragment.AttachedTo.GetComponent<SurfaceFragmentController>();
			component.OnBreak = (Action)Delegate.Remove(component.OnBreak, fragment.AttachedBreakCallback);
			fragment.AttachedBreakCallback = null;
		}
		fragment.AttachedTo = null;
		fragment.Object.transform.SetParent(base.transform, true);
	}

	private void AddParticleTrigger(Fragment fragment)
	{
		if (fragment.Particles == null || fragment.Particles.Length == 0)
		{
			return;
		}
		if (fragment.Trigger != null)
		{
			UnityEngine.Object.Destroy(fragment.Trigger);
		}
		if (fragment.IsIndependent)
		{
			fragment.Trigger = fragment.Object.AddComponent<FragmentParticleTrigger>();
			fragment.Trigger.Fragment = fragment;
			fragment.Trigger.OnActivateParticles += delegate
			{
				OnFragmentBreak(fragment);
			};
		}
		else if (!fragment.AttachedTo.noRigidbody)
		{
			fragment.Trigger = fragment.AttachedTo.Rigidbody.gameObject.AddComponent<FragmentParticleTrigger>();
			fragment.Trigger.Fragment = fragment;
			fragment.Trigger.OnActivateParticles += delegate
			{
				OnFragmentBreak(fragment);
			};
			fragment.Body = fragment.AttachedTo.Rigidbody;
		}
	}

	private void MakeFragmentIndependent(Fragment fragment)
	{
		if (fragment.AttachedTo != null && fragment.AttachedBreakCallback != null)
		{
			SurfaceFragmentController component = fragment.AttachedTo.GetComponent<SurfaceFragmentController>();
			component.OnBreak = (Action)Delegate.Remove(component.OnBreak, fragment.AttachedBreakCallback);
			fragment.AttachedBreakCallback = null;
		}
		fragment.IsIndependent = true;
		fragment.AttachedTo = null;
		fragment.Object.transform.SetParent(ParentMachine, true);
		fragment.HasOwnBody = true;
		fragment.Body = fragment.Object.AddComponent<Rigidbody>();
		fragment.Body.solverIterations = 5;
		fragment.Body.interpolation = RigidbodyInterpolation.Interpolate;
		fragment.Body.drag = 0f;
		fragment.Body.mass = fragment.Mass;
	}

	private void AssignFragmentStickyParticles(Fragment fragment, int edgeIndex, BuildSurface attachment)
	{
		for (int i = 0; i < fragment.Particles.Length; i++)
		{
			Particle particle = fragment.Particles[i];
			if (particle.IsStickySide)
			{
				int num = (int)particle.Side;
				if (mySurface.nodes.Length == 3)
				{
					num = FracturePiece.SideToEdgeIndexTri(particle.Side);
				}
				if (num == edgeIndex)
				{
					particle.StickyAttachment = attachment;
				}
			}
		}
	}
}
