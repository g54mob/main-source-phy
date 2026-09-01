using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Poly.Base;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	public class SpringAudioListener : ListenerBase, IWorldListener, IEdgeListener
	{
		private List<EdgeHandle> springEdges = new List<EdgeHandle>();

		private FastList<SpringData> springDatas = new FastList<SpringData>(16);

		public float ExpandVelocityThresholdToTriggerSound = 3f;

		public float CompressVelocityThresholdToTriggerSound = -2.5f;

		public float soundCooldownTime = 0.5f;

		[SoundGroup]
		public string expandBig = "[None]";

		[SoundGroup]
		public string expandSmall = "[None]";

		[SoundGroup]
		public string compressBig = "[None]";

		[SoundGroup]
		public string compressSmall = "[None]";

		public bool debugLogSounds;

		public virtual bool OnEdgeBroken(Edge edge)
		{
			return true;
		}

		public void OnEdgeDetachedFromNode(EdgeHandle e, NodeHandle oldNode)
		{
		}

		public void OnEdgeAttachedToNode(EdgeHandle e, NodeHandle newNode)
		{
		}

		public virtual void OnEdgeAdded(EdgeHandle e)
		{
			if (e.material.isSpring)
			{
				springEdges.Add(e);
				springDatas.Add(new SpringData(e));
			}
		}

		public virtual void OnEdgeRemoved(EdgeHandle e)
		{
			if (!e.material.isSpring)
			{
				return;
			}
			springEdges.Remove(e);
			for (int i = 0; i < springDatas.Count; i++)
			{
				if (springDatas[i].edge == e)
				{
					springDatas.RemoveAtAndSwap(i);
					break;
				}
			}
		}

		public void BeforeStep()
		{
		}

		public void AfterWorldCleared()
		{
			EnsureCleared();
		}

		public void AfterWorldFrameUpdate()
		{
		}

		public void AfterWorldFixedUpdate()
		{
			TriggerSpringAudioEvents();
		}

		private void TriggerSpringAudioEvents()
		{
			float fixedDeltaTime = Time.fixedDeltaTime;
			float num = 0f;
			if (0 < springDatas.Count)
			{
				float deltaTimeForVelocityEdge = springDatas[0].edge.world.settings.deltaTimeForVelocityEdge;
				if (1E-06f < deltaTimeForVelocityEdge)
				{
					num = 1f / deltaTimeForVelocityEdge;
				}
			}
			for (int i = 0; i < springDatas.Count; i++)
			{
				ref SpringData reference = ref springDatas.array[i];
				reference.timeSinceCompressionLastTriggered += fixedDeltaTime;
				reference.timeSinceExpansionLastTriggered += fixedDeltaTime;
				Vec2 a = reference.edge.node1.solverNode.vel - reference.edge.node0.solverNode.vel;
				a *= num;
				Vec2 b = reference.edge.node1.solverNode.pos - reference.edge.node0.solverNode.pos;
				b.Normalize();
				float num2 = Vec2.Dot(in a, in b);
				if (ExpandVelocityThresholdToTriggerSound < num2 && soundCooldownTime <= reference.timeSinceExpansionLastTriggered)
				{
					reference.timeSinceExpansionLastTriggered = 0f;
					Vec2 vec = 0.5f * (reference.edge.node1.solverNode.pos + reference.edge.node0.solverNode.pos);
					if (num2 > 4f)
					{
						MasterAudio.PlaySound3DAtVector3AndForget(expandBig, vec);
					}
					else
					{
						MasterAudio.PlaySound3DAtVector3AndForget(expandSmall, vec);
					}
					if (debugLogSounds)
					{
						Vec2 vec2 = vec;
						Debug.Log("Spring Expansion at " + vec2.ToString() + ", speed: " + num2);
					}
				}
				else if (num2 < CompressVelocityThresholdToTriggerSound && soundCooldownTime <= reference.timeSinceCompressionLastTriggered)
				{
					reference.timeSinceCompressionLastTriggered = 0f;
					Vec2 vec3 = 0.5f * (reference.edge.node1.solverNode.pos + reference.edge.node0.solverNode.pos);
					if (num2 < -4.5f)
					{
						MasterAudio.PlaySound3DAtVector3AndForget(compressBig, vec3);
					}
					else
					{
						MasterAudio.PlaySound3DAtVector3AndForget(compressSmall, vec3);
					}
					if (debugLogSounds)
					{
						Vec2 vec2 = vec3;
						Debug.Log("Spring Compression at " + vec2.ToString() + ", speed: " + num2);
					}
				}
			}
		}

		private void EnsureCleared()
		{
			springEdges.Clear();
		}
	}
}
