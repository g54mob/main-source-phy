using CodeAnimo.GPGPU;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Graphics/Wave Mesh Offset Creator")]
	public class WaveMeshOffsetCreator : SimulationOutput
	{
		public SimulationOutput waveData;

		public SimulationOutput terrainData;

		public WaveMeshGroup selectedWave;

		protected override void AddMissingComponents()
		{
			base.AddMissingComponents();
			AddComponentIfMissingAndSetup<ComputeKernel2D>();
			AddComponentIfMissingAndSetup<SM3Kernel>();
		}

		public override void LoadData()
		{
			FindKernel();
			FindTextureManager();
		}

		public override void RunStep()
		{
			updateGraphics();
		}

		private void updateGraphics()
		{
			if (!(waveData == null) && waveData.isDataAvailable && !(terrainData == null) && terrainData.isDataAvailable)
			{
				RenderTexture renderTexture = waveData.outputData;
				updateHeightMap(renderTexture, terrainData.outputData, renderTexture.width, renderTexture.height);
				renderTexture.filterMode = FilterMode.Trilinear;
				Material selectedMaterial = selectedWave.selectedMaterial;
				selectedMaterial.SetTexture("_HeightTex", base.outputData);
				selectedMaterial.SetTexture("_WaterData", renderTexture);
			}
		}

		private void updateHeightMap(RenderTexture waveHeight, RenderTexture terrainHeight, int textureWidth, int textureHeight)
		{
			RenderTexture renderTexture = simTextureManager.CreateOutputTexture("WaveMesh Displacement", true);
			simKernel.SetTexture("DisplacementTextureOut", renderTexture);
			simKernel.SetTexture("WaveMapIn", waveHeight);
			simKernel.SetTexture("TerrainMapIn", terrainHeight);
			simKernel.Dispatch();
			UpdateOutput(renderTexture);
		}
	}
}
