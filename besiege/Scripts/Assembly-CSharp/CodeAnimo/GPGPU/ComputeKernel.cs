using UnityEngine;

namespace CodeAnimo.GPGPU
{
	public abstract class ComputeKernel : Kernel
	{
		public int warpWidth = 8;

		public int warpHeight = 8;

		public int warpDepth = 1;

		[SerializeField]
		private ComputeShader m_simulationShader;

		[SerializeField]
		private string m_kernelName;

		[HideInInspector]
		[SerializeField]
		private bool _kernelFound;

		[HideInInspector]
		[SerializeField]
		protected int kernelIndex = -1;

		public ComputeShader simulationShader
		{
			get
			{
				return m_simulationShader;
			}
			set
			{
				m_simulationShader = value;
				UpdateKernelIndex(kernelName);
			}
		}

		public string kernelName
		{
			get
			{
				return m_kernelName;
			}
			set
			{
				m_kernelName = value;
				UpdateKernelIndex(value);
			}
		}

		public bool kernelFound
		{
			get
			{
				return _kernelFound;
			}
		}

		protected virtual void OnValidate()
		{
			kernelName = m_kernelName;
			simulationShader = m_simulationShader;
		}

		public abstract override void Dispatch();

		public void InitializeKernel()
		{
			UpdateKernelIndex(kernelName);
		}

		protected void UpdateKernelIndex(string kernelName)
		{
			if (!SupportedBySystem())
			{
				return;
			}
			if (simulationShader == null)
			{
				_kernelFound = false;
				return;
			}
			if (kernelName == null || kernelName.Length == 0)
			{
				_kernelFound = false;
				return;
			}
			kernelIndex = simulationShader.FindKernel(kernelName);
			if (kernelIndex >= 0)
			{
				_kernelFound = true;
			}
			else
			{
				_kernelFound = false;
			}
		}

		public override void SetFloat(string floatName, float floatValue)
		{
			simulationShader.SetFloat(floatName, floatValue);
		}

		public override void SetTexture(string textureName, Texture simTexture)
		{
			simulationShader.SetTexture(kernelIndex, textureName, simTexture);
		}

		public override bool SupportedBySystem()
		{
			return SystemInfo.supportsComputeShaders && base.SupportedBySystem();
		}

		public override void SetInt(string intName, int intValue)
		{
			simulationShader.SetInt(intName, intValue);
		}

		public void SetBuffer(string bufferName, ComputeBuffer buffer)
		{
			simulationShader.SetBuffer(kernelIndex, bufferName, buffer);
		}

		protected void LogKernelNotFoundWarning()
		{
			if (!_kernelFound)
			{
				string text = "Compute kernel name: ";
				text += kernelName;
				text += " given, but the correct index was not -or could not be- found";
				Debug.LogWarning(text, this);
			}
		}
	}
}
