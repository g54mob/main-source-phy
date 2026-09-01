using UnityEngine;

namespace Dreamteck.Splines
{
	[ExecuteInEditMode]
	[AddComponentMenu("Dreamteck/Splines/Particle Controller")]
	public class ParticleController : SplineUser
	{
		public enum EmitPoint
		{
			Beginning = 0,
			Ending = 1,
			Random = 2,
			Ordered = 3
		}

		public enum MotionType
		{
			None = 0,
			UseParticleSystem = 1,
			FollowForward = 2,
			FollowBackward = 3,
			ByNormal = 4,
			ByNormalRandomized = 5
		}

		public enum Wrap
		{
			Default = 0,
			Loop = 1
		}

		public class Particle
		{
			internal Vector2 startOffset = Vector2.zero;

			internal Vector2 endOffset = Vector2.zero;

			internal float cycleSpeed;

			internal float startLifetime;

			internal Color startColor = Color.white;

			internal float remainingLifetime;

			internal double startPercent;

			internal double GetSplinePercent(Wrap wrap)
			{
				switch (wrap)
				{
				case Wrap.Default:
					return DMath.Clamp01(startPercent + (double)((1f - remainingLifetime / startLifetime) * cycleSpeed));
				case Wrap.Loop:
				{
					double num = startPercent + (1.0 - (double)(remainingLifetime / startLifetime)) * (double)cycleSpeed;
					if (num > 1.0)
					{
						num -= (double)Mathf.FloorToInt((float)num);
					}
					return num;
				}
				default:
					return 0.0;
				}
			}
		}

		[HideInInspector]
		public ParticleSystem _particleSystem;

		[HideInInspector]
		public bool volumetric;

		[HideInInspector]
		public bool emitFromShell;

		[HideInInspector]
		public Vector2 scale = Vector2.one;

		[HideInInspector]
		public EmitPoint emitPoint;

		[HideInInspector]
		public MotionType motionType = MotionType.UseParticleSystem;

		[HideInInspector]
		public Wrap wrapMode;

		[HideInInspector]
		public float minCycles = 1f;

		[HideInInspector]
		public float maxCycles = 2f;

		private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[0];

		private Particle[] controllers = new Particle[0];

		private float[] lifetimes = new float[0];

		private int particleCount;

		private int birthIndex;

		private SplineResult evaluateResult = new SplineResult();

		protected override void Awake()
		{
			base.Awake();
			updateMethod = UpdateMethod.LateUpdate;
		}

		protected override void LateRun()
		{
			if (_particleSystem == null)
			{
				return;
			}
			int maxParticles = _particleSystem.main.maxParticles;
			if (particles.Length != maxParticles)
			{
				particles = new ParticleSystem.Particle[maxParticles];
				Particle[] array = new Particle[maxParticles];
				float[] array2 = new float[maxParticles];
				for (int i = 0; i < array.Length && i < controllers.Length; i++)
				{
					array[i] = controllers[i];
					array2[i] = lifetimes[i];
				}
				controllers = array;
				lifetimes = array2;
			}
			particleCount = _particleSystem.GetParticles(particles);
			bool flag = _particleSystem.main.simulationSpace == ParticleSystemSimulationSpace.Local;
			Transform transform = _particleSystem.transform;
			for (int j = 0; j < particleCount; j++)
			{
				if (flag)
				{
					particles[j].position = transform.TransformPoint(particles[j].position);
					particles[j].velocity = transform.TransformDirection(particles[j].velocity);
				}
				if (controllers[j] == null || particles[j].remainingLifetime >= particles[j].startLifetime - Time.deltaTime)
				{
					OnParticleBorn(j);
				}
				HandleParticle(j);
				if (flag)
				{
					particles[j].position = transform.InverseTransformPoint(particles[j].position);
					particles[j].velocity = transform.InverseTransformDirection(particles[j].velocity);
				}
			}
			_particleSystem.SetParticles(particles, particleCount);
			for (int k = particleCount; k < controllers.Length && controllers[k] != null; k++)
			{
				controllers[k] = null;
			}
			int num = 0;
			for (int l = 0; l < particleCount; l++)
			{
				if (particles[l].remainingLifetime - Time.deltaTime <= 0f)
				{
					controllers[l] = controllers[particleCount - 1 - num];
					controllers[particleCount - 1 - num] = null;
					num++;
				}
			}
		}

		protected override void Reset()
		{
			base.Reset();
			if (_particleSystem == null)
			{
				_particleSystem = GetComponent<ParticleSystem>();
			}
		}

		private void HandleParticle(int index)
		{
			float num = particles[index].remainingLifetime / particles[index].startLifetime;
			if (motionType == MotionType.FollowBackward || motionType == MotionType.FollowForward || motionType == MotionType.None)
			{
				Evaluate(evaluateResult, UnclipPercent(controllers[index].GetSplinePercent(wrapMode)));
				particles[index].position = evaluateResult.position;
				if (volumetric)
				{
					Vector3 vector = -Vector3.Cross(evaluateResult.direction, evaluateResult.normal);
					Vector2 vector2 = controllers[index].startOffset;
					if (motionType != MotionType.None)
					{
						vector2 = Vector2.Lerp(controllers[index].startOffset, controllers[index].endOffset, 1f - num);
					}
					particles[index].position += vector * vector2.x * scale.x * evaluateResult.size + evaluateResult.normal * vector2.y * scale.y * evaluateResult.size;
				}
				particles[index].velocity = evaluateResult.direction;
				particles[index].startColor = controllers[index].startColor * evaluateResult.color;
			}
			controllers[index].remainingLifetime -= Time.deltaTime;
			particles[index].remainingLifetime = controllers[index].remainingLifetime;
		}

		private void OnParticleDie(int index)
		{
		}

		private void OnParticleBorn(int index)
		{
			birthIndex++;
			double num = 0.0;
			float num2 = Mathf.Lerp(_particleSystem.emission.rateOverTime.constantMin, _particleSystem.emission.rateOverTime.constantMax, 0.5f) * _particleSystem.main.startLifetime.constantMax;
			if ((float)birthIndex > num2)
			{
				birthIndex = 0;
			}
			switch (emitPoint)
			{
			case EmitPoint.Beginning:
				num = 0.0;
				break;
			case EmitPoint.Ending:
				num = 1.0;
				break;
			case EmitPoint.Random:
				num = Random.Range(0f, 1f);
				break;
			case EmitPoint.Ordered:
				num = ((num2 > 0f) ? ((float)birthIndex / num2) : 0f);
				break;
			}
			Evaluate(evaluateResult, UnclipPercent(num));
			if (controllers[index] == null)
			{
				controllers[index] = new Particle();
			}
			controllers[index].startColor = particles[index].startColor;
			controllers[index].startPercent = num;
			controllers[index].startLifetime = particles[index].startLifetime;
			controllers[index].remainingLifetime = particles[index].remainingLifetime;
			controllers[index].cycleSpeed = Random.Range(minCycles, maxCycles);
			Vector2 vector = Vector2.zero;
			if (volumetric)
			{
				vector = ((!emitFromShell) ? Random.insideUnitCircle : ((Vector2)(Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector2.right)));
			}
			controllers[index].startOffset = vector * 0.5f;
			controllers[index].endOffset = Random.insideUnitCircle * 0.5f;
			Vector3 vector2 = Vector3.Cross(evaluateResult.direction, evaluateResult.normal);
			particles[index].position = evaluateResult.position + vector2 * controllers[index].startOffset.x * evaluateResult.size * scale.x + evaluateResult.normal * controllers[index].startOffset.y * evaluateResult.size * scale.y;
			float x = _particleSystem.forceOverLifetime.x.constantMax;
			float y = _particleSystem.forceOverLifetime.y.constantMax;
			float z = _particleSystem.forceOverLifetime.z.constantMax;
			if (_particleSystem.forceOverLifetime.randomized)
			{
				x = Random.Range(_particleSystem.forceOverLifetime.x.constantMin, _particleSystem.forceOverLifetime.x.constantMax);
				y = Random.Range(_particleSystem.forceOverLifetime.y.constantMin, _particleSystem.forceOverLifetime.y.constantMax);
				z = Random.Range(_particleSystem.forceOverLifetime.z.constantMin, _particleSystem.forceOverLifetime.z.constantMax);
			}
			float num3 = particles[index].startLifetime - particles[index].remainingLifetime;
			Vector3 vector3 = new Vector3(x, y, z) * 0.5f * (num3 * num3);
			float constantMax = _particleSystem.main.startSpeed.constantMax;
			if (motionType == MotionType.ByNormal)
			{
				particles[index].position += evaluateResult.normal * constantMax * (particles[index].startLifetime - particles[index].remainingLifetime);
				particles[index].position += vector3;
				particles[index].velocity = evaluateResult.normal * constantMax + new Vector3(x, y, z) * num3;
			}
			else if (motionType == MotionType.ByNormalRandomized)
			{
				Vector3 vector4 = Quaternion.AngleAxis(Random.Range(0f, 360f), evaluateResult.direction) * evaluateResult.normal;
				particles[index].position += vector4 * constantMax * (particles[index].startLifetime - particles[index].remainingLifetime);
				particles[index].position += vector3;
				particles[index].velocity = vector4 * constantMax + new Vector3(x, y, z) * num3;
			}
		}
	}
}
