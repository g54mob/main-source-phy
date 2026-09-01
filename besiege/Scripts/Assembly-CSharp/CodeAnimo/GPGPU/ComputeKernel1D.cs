using UnityEngine;

namespace CodeAnimo.GPGPU
{
	[AddComponentMenu("GPGPU/Compute Kernel 1D")]
	public class ComputeKernel1D : ComputeKernel
	{
		[HideInInspector]
		public int elementCount = 1;

		public override void Dispatch()
		{
			if (!base.kernelFound)
			{
				LogKernelNotFoundWarning();
				return;
			}
			int threadGroupsX = CalculateWarpGroupCount();
			base.simulationShader.Dispatch(kernelIndex, threadGroupsX, 1, 1);
		}

		public int CalculateWarpGroupCount()
		{
			int num = warpWidth * warpHeight * warpDepth;
			return Mathf.CeilToInt((float)elementCount / (float)num);
		}
	}
}
