using CodeAnimo.GPGPU;
using UnityEngine;
using UnityEngine.Rendering;

namespace CodeAnimo.SurfaceWaves
{
	public class SimulationLoad : SimulationStep
	{
		public SimulationSave dataSource;

		private ComputeKernel2D simKernel;

		private void Reset()
		{
			simKernel = GetComponent<ComputeKernel2D>();
			if (simKernel == null)
			{
				simKernel = base.gameObject.AddComponent<ComputeKernel2D>();
			}
			simKernel.kernelName = "loadTexture";
		}

		private void Awake()
		{
			simKernel = GetComponent<ComputeKernel2D>();
		}

		public override void LoadData()
		{
			LoadAll();
		}

		public override void RunStep()
		{
		}

		public void LoadAll()
		{
			if (simKernel.SupportedBySystem())
			{
				moveToGPU(dataSource.flowData.pixels, dataSource.flowStep.outputData);
				moveToGPU(dataSource.heightData.pixels, dataSource.heightStep.outputData);
			}
			else
			{
				Debug.Log("Compute shaders not supported", this);
			}
		}

		public void moveToGPU(Vector4[] pixels, RenderTexture target)
		{
			if (target.dimension != TextureDimension.Tex2D)
			{
				Debug.Log("Only 2D texture are supported", this);
				return;
			}
			int count = target.width * target.height;
			ComputeBuffer computeBuffer = new ComputeBuffer(count, 16);
			computeBuffer.SetData(pixels);
			simKernel.SetBuffer("PixelIn", computeBuffer);
			simKernel.SetTexture("TargetTexture", target);
			simKernel.Dispatch();
			computeBuffer.SetData(pixels);
			computeBuffer.Release();
		}
	}
}
