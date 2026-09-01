using Poly.Base;
using Poly.Collide;
using Poly.Solver;
using UnityEngine;

namespace Poly.Physics.Unity
{
	[RequireComponent(typeof(Rigidbody))]
	public class DefaultCollisionListener : PolyBehaviour, ICollisionListener
	{
		public bool enableVisualizationSelf;

		public bool useEmitters;

		private Rigidbody body;

		private bool receivedCallbacks;

		private bool isPlaying = true;

		private float accumulatedImpulse;

		private float timeSinceLastImpact;

		public ParticleSystem smokeEmitter;

		public ParticleSystem sparksEmitter;

		private void Awake()
		{
			body = GetComponent<Rigidbody>();
		}

		private void OnDestroy()
		{
		}

		private void Start()
		{
			if (useEmitters)
			{
				smokeEmitter = Object.Instantiate(smokeEmitter);
				sparksEmitter = Object.Instantiate(sparksEmitter);
			}
			else
			{
				smokeEmitter = null;
				sparksEmitter = null;
			}
		}

		private void OnEnable()
		{
			body.collisionListeners.Add(this);
		}

		private void OnDisable()
		{
			body.collisionListeners.Remove(this);
		}

		public void OnPolyCollisionEnter(in CollisionEvent e)
		{
			UpdateParticleEmitters(in e);
		}

		public void OnPolyCollisionStay(in CollisionEvent e)
		{
			UpdateParticleEmitters(in e);
		}

		public void OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
		}

		public void VerifyReset()
		{
		}

		public void OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
		}

		private void UpdateParticleEmitters(in CollisionEvent e)
		{
			GameObject[] array = new GameObject[2]
			{
				e.a.Value.GetUnityComponent().gameObject,
				e.b.Value.GetUnityComponent().gameObject
			};
			int num = ((!(base.gameObject != array[0])) ? 1 : 0);
			_ = base.gameObject;
			_ = array[num];
			switch ((new Layer[2]
			{
				e.a.Value.layer,
				e.b.Value.layer
			})[num])
			{
			}
			if ((bool)smokeEmitter)
			{
				smokeEmitter.transform.position = e.avgPosition;
			}
			if ((bool)sparksEmitter)
			{
				float num2 = Vector2.Dot(e.relativeLinearVelocityBeforeCollision, e.point0.normal);
				if (e.numPoints > 1)
				{
					Vector2.Dot(e.relativeLinearVelocityBeforeCollision, e.point1.normal);
				}
				if (num2 < -0.01f && e.point0.isNewImpact)
				{
					timeSinceLastImpact = 0f;
					sparksEmitter.transform.position = e.point0.position;
					sparksEmitter.Play();
				}
				else if (num2 < -0.01f && e.point0.isNewImpact)
				{
					timeSinceLastImpact = 0f;
					sparksEmitter.transform.position = e.point1.position;
					sparksEmitter.Play();
				}
			}
			receivedCallbacks = true;
			accumulatedImpulse += e.sumNormalImpulsesApplied;
		}

		private void Update()
		{
			timeSinceLastImpact += Time.deltaTime;
			if (useEmitters && (receivedCallbacks ^ isPlaying))
			{
				if (receivedCallbacks)
				{
					smokeEmitter.Play();
				}
				else
				{
					smokeEmitter.Stop();
				}
				isPlaying = receivedCallbacks;
			}
			if (useEmitters && 0.5f < timeSinceLastImpact && !sparksEmitter.isStopped)
			{
				sparksEmitter.Stop();
			}
			receivedCallbacks = false;
			accumulatedImpulse = 0f;
		}

		void ICollisionListener.OnPolyCollisionEnter(in CollisionEvent e)
		{
			OnPolyCollisionEnter(in e);
		}

		void ICollisionListener.OnPolyCollisionStay(in CollisionEvent e)
		{
			OnPolyCollisionStay(in e);
		}

		void ICollisionListener.OnPolyCollisionExit(ShapeHandleIndex a, ShapeHandleIndex b, ReceivingHandle receivingHandle, in CollisionCache cache)
		{
			OnPolyCollisionExit(a, b, receivingHandle, in cache);
		}

		void ICollisionListener.OnPolyCollisionProcess_Internal(in CollisionEvent ePartial, ref CollisionInfo info)
		{
			OnPolyCollisionProcess_Internal(in ePartial, ref info);
		}
	}
}
