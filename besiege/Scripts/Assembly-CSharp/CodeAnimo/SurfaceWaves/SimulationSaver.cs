using System;
using System.IO;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public class SimulationSaver : MonoBehaviour
	{
		public string saveName = string.Empty;

		private string nameExtension = ".png";

		public bool attach_date;

		public ComputeShader simCompute;

		public Texture2D savedWaves;

		private int warpWidth = 32;

		private int warpHeight = 32;

		public void saveWaveLevel()
		{
		}

		public void restoreWaveLevel()
		{
		}

		private RenderTexture convertToWritableTexture(string textureName, Texture input)
		{
			int kernelIndex = simCompute.FindKernel("basicBlit");
			RenderTexture renderTexture = createStorageTexture(input.width, input.height, textureName);
			simCompute.SetTexture(kernelIndex, "BlitIn", input);
			simCompute.SetTexture(kernelIndex, "BlitOut", renderTexture);
			simCompute.Dispatch(kernelIndex, input.width / warpWidth, input.height / warpHeight, 1);
			return renderTexture;
		}

		private RenderTexture createStorageTexture(int width, int height, string name)
		{
			RenderTexture renderTexture = new RenderTexture(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
			renderTexture.name = name;
			renderTexture.enableRandomWrite = true;
			renderTexture.anisoLevel = 0;
			renderTexture.filterMode = FilterMode.Point;
			renderTexture.Create();
			return renderTexture;
		}

		public Texture2D renderTextureToTexture2D(RenderTexture original)
		{
			Texture2D texture2D = new Texture2D(original.width, original.height, TextureFormat.ARGB32, false, true);
			texture2D.name = original.name;
			Rect source = new Rect(0f, 0f, original.width, original.height);
			RenderTexture active = RenderTexture.active;
			if (original.format == RenderTextureFormat.ARGB32)
			{
				RenderTexture.active = original;
				texture2D.ReadPixels(source, 0, 0, true);
				texture2D.Apply();
				RenderTexture.active = active;
				return texture2D;
			}
			throw new FormatException(string.Concat("The RenderTexture needs to have the ARGB32 RenderTextureFormat. ", original.format, " found instead."));
		}

		public void writeTex2D(Texture2D texture)
		{
			if (!texture)
			{
				Debug.LogWarning("While trying to write a texture to disk, it turned out the texture wasn't there", this);
				return;
			}
			byte[] buffer = texture.EncodeToPNG();
			string text = "textureDumps/" + saveName;
			if (attach_date)
			{
				text += DateTime.Now.ToFileTime();
			}
			text += nameExtension;
			FileStream fileStream = new FileStream(text, FileMode.Create);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(buffer);
			binaryWriter.Close();
			fileStream.Close();
			Debug.Log("I successfully wrote \"" + saveName + nameExtension + "\" to disk. Location: " + text, this);
		}

		private void OnGUI()
		{
			GUILayout.BeginArea(new Rect(400f, 200f, 200f, 200f));
			if (GUILayout.Button("Save Sim"))
			{
				saveWaveLevel();
			}
			if (GUILayout.Button("Restore Sim"))
			{
				restoreWaveLevel();
			}
			GUILayout.EndArea();
		}
	}
}
