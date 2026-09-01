using System.IO;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "UnitEditorColorCatagoryGenerator", menuName = "Landfall/UnitEditor/ColorCatagoryGenerator", order = 99999999)]
public class UnitEditorColorCataogryGenerator : ScriptableObject
{
	private struct ColorStruct
	{
		private Vector3 color;

		public ColorStruct(Color color)
		{
			this.color.x = color.r;
			this.color.y = color.g;
			this.color.z = color.b;
		}
	}

	public ComputeShader ComputeShader;

	public UnitEditorColorPalette colorPalette;

	public Sprite shard;

	public Color[] ManualColors;

	private string imageName = "TestImage";

	public void GenerateCatagories()
	{
		for (int i = 0; i < colorPalette.ColorPaletteParentCatagories.Length; i++)
		{
			for (int j = 0; j < colorPalette.ColorPaletteParentCatagories[i].colorPaletteCatagories.Length; j++)
			{
				GenerateCatagory(colorPalette.ColorPaletteParentCatagories[i].colorPaletteCatagories[j]);
			}
		}
	}

	public void GenerateManualColors()
	{
		int num = Mathf.Min(ManualColors.Length, 4);
		ColorStruct[] array = new ColorStruct[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = new ColorStruct(ManualColors[i]);
		}
		RenderTexture renderTexture = CreateTexture(shard.texture.width, shard.texture.height, FilterMode.Point);
		int kernelIndex = ComputeShader.FindKernel("RenderColors");
		ComputeBuffer computeBuffer = new ComputeBuffer(array.Length, 12);
		computeBuffer.SetData(array);
		ComputeShader.SetBuffer(kernelIndex, "colorBuffer", computeBuffer);
		ComputeShader.SetTexture(kernelIndex, "Result", renderTexture);
		ComputeShader.SetTexture(kernelIndex, "Shard", shard.texture);
		ComputeShader.SetInt("rez", shard.texture.width);
		ComputeShader.SetInt("colorAmounts", num);
		ComputeShader.Dispatch(kernelIndex, shard.texture.width, shard.texture.height, 1);
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D(shard.texture.width, shard.texture.height);
		texture2D.ReadPixels(new Rect(0f, 0f, shard.texture.width, shard.texture.height), 0, 0);
		texture2D.Apply();
		byte[] bytes = texture2D.EncodeToPNG();
		string text = Application.dataPath + "/9 Sprites/UnitEditor/GeneratedColors.png";
		File.WriteAllBytes(text, bytes);
		Debug.Log("saved to: " + text);
	}

	public void GenerateCatagory(UnitEditorColorPalette.ColorPaletteCatagory catagory)
	{
		bool flag = catagory.Cataogry == UnitEditorColorPalette.ColorPaletteCatagory.CatagoryType.TeamColors;
		string text = "Catagory-" + catagory.name;
		int num = Mathf.Min(catagory.Colors.Length, 4);
		if (flag)
		{
			num = Mathf.Min(catagory.TeamColors.Length, 4);
		}
		ColorStruct[] array = new ColorStruct[num];
		int num2 = 1;
		if (catagory.Colors.Length >= 8 || catagory.TeamColors.Length >= 8)
		{
			num2 = 2;
		}
		for (int i = 0; i < num; i++)
		{
			if (!flag)
			{
				array[i] = new ColorStruct(catagory.Colors[i * num2].m_color);
			}
			else
			{
				array[i] = new ColorStruct(catagory.TeamColors[i * num2].GetColor(Team.Red));
			}
		}
		RenderTexture renderTexture = CreateTexture(shard.texture.width, shard.texture.height, FilterMode.Point);
		int kernelIndex = ComputeShader.FindKernel("RenderColors");
		ComputeBuffer computeBuffer = new ComputeBuffer(array.Length, 12);
		computeBuffer.SetData(array);
		ComputeShader.SetBuffer(kernelIndex, "colorBuffer", computeBuffer);
		ComputeShader.SetTexture(kernelIndex, "Result", renderTexture);
		ComputeShader.SetTexture(kernelIndex, "Shard", shard.texture);
		ComputeShader.SetInt("rez", shard.texture.width);
		ComputeShader.SetInt("colorAmounts", num);
		ComputeShader.Dispatch(kernelIndex, shard.texture.width, shard.texture.height, 1);
		RenderTexture.active = renderTexture;
		Texture2D texture2D = new Texture2D(shard.texture.width, shard.texture.height);
		texture2D.ReadPixels(new Rect(0f, 0f, shard.texture.width, shard.texture.height), 0, 0);
		texture2D.Apply();
		byte[] bytes = texture2D.EncodeToPNG();
		string text2 = Application.dataPath + "/9 Sprites/UnitEditor/" + text + ".png";
		File.WriteAllBytes(text2, bytes);
		Debug.Log("saved to: " + text2);
	}

	private RenderTexture CreateTexture(int resolutionx, int resolutiony, FilterMode filterMode)
	{
		RenderTexture renderTexture = new RenderTexture(resolutionx, resolutiony, 1, RenderTextureFormat.ARGBFloat);
		renderTexture.name = "ComputeRenderTexture";
		renderTexture.enableRandomWrite = true;
		renderTexture.dimension = TextureDimension.Tex2D;
		renderTexture.volumeDepth = 1;
		renderTexture.filterMode = filterMode;
		renderTexture.wrapMode = TextureWrapMode.Repeat;
		renderTexture.autoGenerateMips = false;
		renderTexture.useMipMap = false;
		renderTexture.Create();
		return renderTexture;
	}
}
