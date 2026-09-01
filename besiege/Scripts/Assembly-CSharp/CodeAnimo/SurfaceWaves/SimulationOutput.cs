using CodeAnimo.GPGPU;
using CodeAnimo.UnityExtensionMethods;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public abstract class SimulationOutput : SimulationStep
	{
		[SerializeField]
		[HideInInspector]
		protected GameObject standardSettingsPrefab;

		public Dimensions simulationSize;

		[TextureDebug(inputBox = false)]
		[SerializeField]
		protected RenderTexture m_outputData;

		protected StepStateManager stateManager;

		protected Kernel simKernel;

		protected TextureFactory simTextureManager;

		public RenderTexture outputData
		{
			get
			{
				return m_outputData;
			}
		}

		public bool isDataAvailable
		{
			get
			{
				return m_outputData != null;
			}
		}

		protected virtual void Reset()
		{
			if (standardSettingsPrefab != null)
			{
				this.ApplyPrefabSettings(standardSettingsPrefab);
				AddMissingComponents();
				return;
			}
			throw new MissingReferenceException("Somehow, the standard Settings Prefab isn't available, make sure it's set on the script's default references");
		}

		protected virtual void Awake()
		{
		}

		protected virtual void AddMissingComponents()
		{
			if (Application.isPlaying)
			{
				base.gameObject.AddComponentIfMissing<TextureFactory>();
			}
			else
			{
				AddComponentIfMissingAndSetup<TextureFactory>();
			}
		}

		protected T AddComponentIfMissingAndSetup<T>() where T : Component
		{
			return base.gameObject.AddComponentIfMissingAndCopySettings<T>(standardSettingsPrefab);
		}

		protected void FindKernel()
		{
			if (simKernel == null)
			{
				try
				{
					simKernel = Kernel.FindCompatibleKernelOnGameObject(base.gameObject);
				}
				catch (MissingComponentException exception)
				{
					Debug.LogException(exception, this);
				}
			}
		}

		protected void FindTextureManager()
		{
			if (simTextureManager == null)
			{
				simTextureManager = GetComponent<TextureFactory>();
			}
			if (simTextureManager == null)
			{
				throw new MissingComponentException("Missing Texture Factory");
			}
			if (simulationSize == null)
			{
				throw new MissingReferenceException("Missing Dimensions");
			}
			simTextureManager.resolutionU = simulationSize.resolutionX;
			simTextureManager.resolutionV = simulationSize.resolutionZ;
		}

		protected void LoadState()
		{
			if (simTextureManager == null)
			{
				FindTextureManager();
			}
			try
			{
				stateManager = GetComponent<StepStateManager>();
				if (stateManager == null)
				{
					throw new MissingComponentException("StepStateManager Component is missing");
				}
				m_outputData = stateManager.LoadState(simTextureManager);
			}
			catch (MissingComponentException exception)
			{
				Debug.LogException(exception, this);
			}
		}

		protected void UpdateOutput(RenderTexture newData)
		{
			m_outputData = newData;
		}
	}
}
