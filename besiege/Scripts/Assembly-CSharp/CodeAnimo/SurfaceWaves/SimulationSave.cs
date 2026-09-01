using CodeAnimo.GPGPU;
using UnityEngine;
using UnityEngine.Rendering;

namespace CodeAnimo.SurfaceWaves
{
	public class SimulationSave : MonoBehaviour
	{
		public WaveFlowCompute flowStep;

		public WaveHeightCompute heightStep;

		private ComputeKernel2D simKernel;

		public SimulationTextureData flowData;

		public SimulationTextureData heightData;

		private void Reset()
		{
			simKernel = GetComponent<ComputeKernel2D>();
			if (simKernel == null)
			{
				simKernel = base.gameObject.AddComponent<ComputeKernel2D>();
			}
			simKernel.kernelName = "readTexture";
		}

		private void Awake()
		{
			simKernel = GetComponent<ComputeKernel2D>();
		}

		public void SaveAll()
		{
			if (simKernel.SupportedBySystem())
			{
				flowData.pixels = moveTextureToCPU(flowStep.outputData);
				heightData.pixels = moveTextureToCPU(heightStep.outputData);
				if (flowData.pixels != null && heightData.pixels != null)
				{
					Debug.Log("Simulation successfully saved to assets.");
				}
			}
			else
			{
				Debug.Log("Compute shaders not supported", this);
			}
		}

		public Vector4[] moveTextureToCPU(RenderTexture textureToSave)
		{
			if (textureToSave.dimension != TextureDimension.Tex2D)
			{
				Debug.Log("Only 2D texture are supported", this);
				return null;
			}
			int num = textureToSave.width * textureToSave.height;
			Vector4[] array = new Vector4[num];
			ComputeBuffer computeBuffer = new ComputeBuffer(num, 16);
			simKernel.SetBuffer("PixelOut", computeBuffer);
			simKernel.SetTexture("TextureToSave", textureToSave);
			simKernel.Dispatch();
			computeBuffer.GetData(array);
			computeBuffer.Release();
			return array;
		}
	}
}
