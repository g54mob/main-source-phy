using System;
using CodeAnimo.GPGPU;
using CodeAnimo.UnityExtensionMethods;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Wave Sources/Wave Source")]
	public class WaveSource : SimulationOutput
	{
		public static int defaultWaveInputLayer = 10;

		private float m_inputSizeRatio;

		[SerializeField]
		private float m_inputWidth = 20f;

		[Range(-1f, 1f)]
		public float inputIntensity = 0.5f;

		public SimulationOutput previousInput;

		public Texture inputShape;

		protected bool forceUnchangedOutput;

		private RenderTexture emptyInput;

		private float m_inputGizmoScale = 100f;

		public float inputWidth
		{
			get
			{
				return m_inputWidth;
			}
			set
			{
				m_inputWidth = value;
				CalculateRelativeSize();
			}
		}

		protected float estimatedSimWidth
		{
			get
			{
				if (simulationSize != null)
				{
					return simulationSize.localExtends.x;
				}
				return 512f;
			}
		}

		protected override void Reset()
		{
			base.gameObject.layer = defaultWaveInputLayer;
			base.Reset();
		}

		protected override void AddMissingComponents()
		{
			base.AddMissingComponents();
			AddComponentIfMissingAndSetup<SM3Kernel>();
			Collider collider = base.gameObject.AddComponentIfMissing<Collider, SphereCollider>();
			if (collider != null)
			{
				collider.isTrigger = true;
			}
			else
			{
				collider = GetComponent<Collider>();
			}
			if (collider.attachedRigidbody == null)
			{
				Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
				rigidbody.isKinematic = true;
				rigidbody.useGravity = false;
			}
		}

		protected void OnEnable()
		{
			CalculateRelativeSize();
		}

		protected void FeedSerializedDataThroughProperties()
		{
			inputWidth = m_inputWidth;
		}

		protected void OnValidate()
		{
			FeedSerializedDataThroughProperties();
		}

		public override void LoadData()
		{
			FindKernel();
			FindTextureManager();
		}

		protected void CalculateRelativeSize()
		{
			m_inputSizeRatio = inputWidth / estimatedSimWidth;
		}

		protected void OnDrawGizmos()
		{
			if (!forceUnchangedOutput)
			{
				Color color = Gizmos.color;
				SetupGizmoColor();
				Vector3 position = base.transform.position;
				if (simulationSize != null)
				{
					Vector3 to = position;
					to.y = simulationSize.center.y - simulationSize.localExtends.y;
					Gizmos.DrawLine(position, to);
				}
				float num = Mathf.Abs(inputIntensity) * m_inputGizmoScale;
				Vector3 center = position + new Vector3(0f, 0.5f * num, 0f);
				Gizmos.DrawCube(center, new Vector3(5f, num, 5f));
				Gizmos.color = color;
			}
		}

		protected void OnDrawGizmosSelected()
		{
			if (!forceUnchangedOutput)
			{
				Color color = Gizmos.color;
				SetupGizmoColor();
				Vector3 position = base.transform.position;
				Gizmos.DrawWireSphere(position, inputWidth);
				Gizmos.color = color;
			}
		}

		protected void SetupGizmoColor()
		{
			if (inputIntensity > 0f)
			{
				Gizmos.color = Color.green;
			}
			else if (inputIntensity < 0f)
			{
				Gizmos.color = Color.red;
			}
		}

		protected void OnTriggerEnter(Collider other)
		{
			Dimensions component = other.GetComponent<Dimensions>();
			if (component != null)
			{
				simulationSize = component;
			}
		}

		protected void OnTriggerExit(Collider other)
		{
			Dimensions component = other.GetComponent<Dimensions>();
			if (simulationSize == component)
			{
				simulationSize = null;
			}
		}

		public override void RunStep()
		{
			RenderTexture renderTexture = null;
			if (previousInput != null)
			{
				renderTexture = previousInput.outputData;
			}
			if (renderTexture == null)
			{
				if (emptyInput == null)
				{
					SetupEmptyState(simTextureManager);
				}
				renderTexture = emptyInput;
			}
			if (inputShape == null)
			{
				throw new NullReferenceException("Input Shape Texture Missing");
			}
			if (inputIntensity != 0f && simulationSize != null && !forceUnchangedOutput)
			{
				Vector3 vector = base.transform.position - simulationSize.firstCorner;
				vector.x /= simulationSize.localSize.x;
				vector.z /= simulationSize.localSize.z;
				if (vector.x >= 0f && vector.x <= 1f && vector.z >= 0f && vector.z <= 1f)
				{
					renderTexture = AddWaves(renderTexture, vector.x, vector.z);
				}
			}
			UpdateOutput(renderTexture);
		}

		private RenderTexture AddWaves(RenderTexture previousInputTexture, float xLoc, float yLoc)
		{
			RenderTexture renderTexture = simTextureManager.CreateOutputTexture("Wave Input Map");
			simKernel.SetFloat("xLoc", xLoc);
			simKernel.SetFloat("yLoc", yLoc);
			simKernel.SetFloat("SizeRatio", m_inputSizeRatio);
			simKernel.SetFloat("Intensity", inputIntensity);
			simKernel.SetTexture("WaveMapIn", previousInputTexture);
			simKernel.SetTexture("InputShape", inputShape);
			simKernel.SetTexture("WaveHeightOut", renderTexture);
			simKernel.Dispatch();
			return renderTexture;
		}

		private void SetupEmptyState(TextureFactory textureBuilder)
		{
			Texture2D clearTexture = textureBuilder.GetClearTexture();
			emptyInput = textureBuilder.CreateOutputTexture("No Wave Input/Output", false);
			Graphics.Blit(clearTexture, emptyInput);
			UnityEngine.Object.Destroy(clearTexture);
		}
	}
}
