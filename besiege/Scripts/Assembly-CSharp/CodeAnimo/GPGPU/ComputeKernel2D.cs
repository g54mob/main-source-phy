using System;
using UnityEngine;

namespace CodeAnimo.GPGPU
{
	[AddComponentMenu("GPGPU/Compute Kernel 2D")]
	public class ComputeKernel2D : ComputeKernel
	{
		public bool forceCustomResolution;

		[SerializeField]
		private int m_customResolutionU = 512;

		[SerializeField]
		private int m_customResolutionV = 512;

		private TextureFactory m_outputCreator;

		public int resolutionU
		{
			get
			{
				if (willUseCustomResolution)
				{
					return m_customResolutionU;
				}
				return m_outputCreator.resolutionU;
			}
			set
			{
				if (value > 0)
				{
					m_customResolutionU = value;
					return;
				}
				m_customResolutionU = 1;
				throw new ArgumentOutOfRangeException("Kernel can't have a negative resolution");
			}
		}

		public int resolutionV
		{
			get
			{
				if (willUseCustomResolution)
				{
					return m_customResolutionV;
				}
				return m_outputCreator.resolutionV;
			}
			set
			{
				if (value > 0)
				{
					m_customResolutionV = value;
					return;
				}
				m_customResolutionV = 1;
				throw new ArgumentOutOfRangeException("Kernel can't have a negative resolution");
			}
		}

		public bool willUseCustomResolution
		{
			get
			{
				return forceCustomResolution || m_outputCreator == null;
			}
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			resolutionU = m_customResolutionU;
			resolutionV = m_customResolutionV;
		}

		protected void Reset()
		{
			m_outputCreator = GetComponent<TextureFactory>();
		}

		protected void OnEnable()
		{
			m_outputCreator = GetComponent<TextureFactory>();
		}

		public override void Dispatch()
		{
			if (kernelIndex < 0)
			{
				LogKernelNotFoundWarning();
				return;
			}
			int threadGroupsX = resolutionU / warpWidth;
			int threadGroupsY = resolutionV / warpHeight;
			base.simulationShader.Dispatch(kernelIndex, threadGroupsX, threadGroupsY, 1);
		}
	}
}
