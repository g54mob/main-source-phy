using System;
using CodeAnimo.GPGPU;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public class BufferSaveDepthMap : DepthmapSaver
	{
		public ComputeKernel textureToBuffer;

		public ComputeKernel bufferToTexture;

		private Vector4[] pixelArray;

		public override bool dataStored
		{
			get
			{
				if (pixelArray == null)
				{
					return false;
				}
				return true;
			}
		}

		public override void ReadDepthMap(RenderTexture depthMap)
		{
			int num = depthMap.width * depthMap.height;
			ComputeBuffer computeBuffer = new ComputeBuffer(num, 16);
			textureToBuffer.SetTexture("DepthTextureIn", depthMap);
			textureToBuffer.SetBuffer("DepthBufferOut", computeBuffer);
			textureToBuffer.Dispatch();
			Vector4[] data = new Vector4[num];
			computeBuffer.GetData(data);
			computeBuffer.Dispose();
			pixelArray = data;
		}

		public override void WriteDepthMap(RenderTexture depthMap)
		{
			if (!dataStored)
			{
				throw new NullReferenceException("Trying to set a depth map, but no pixel data is stored.");
			}
			ComputeBuffer computeBuffer = new ComputeBuffer(pixelArray.Length, 16);
			computeBuffer.SetData(pixelArray);
			bufferToTexture.SetBuffer("DepthBufferIn", computeBuffer);
			bufferToTexture.SetTexture("DepthTextureOut", depthMap);
			bufferToTexture.Dispatch();
			computeBuffer.Dispose();
		}
	}
}
