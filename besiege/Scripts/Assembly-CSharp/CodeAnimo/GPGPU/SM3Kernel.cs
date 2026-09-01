using System;
using UnityEngine;

namespace CodeAnimo.GPGPU
{
	[AddComponentMenu("GPGPU/Shader Model 3 Kernel")]
	public class SM3Kernel : Kernel
	{
		public Shader simulationShader;

		public string outputTextureName;

		public int pass = -1;

		protected RenderTexture targetTexture;

		[HideInInspector]
		[SerializeField]
		protected Material m_simulationMaterial;

		protected void OnEnable()
		{
			if (m_simulationMaterial == null)
			{
				CreateSimulationMaterial();
			}
		}

		public override void Dispatch()
		{
			if (targetTexture == null)
			{
				string text = "No output texture has been declared matching the name " + outputTextureName + ".";
				text = text + " On the Object called: " + base.gameObject.name;
				throw new NullReferenceException(text);
			}
			RenderTexture.active = targetTexture;
			Graphics.Blit(null, m_simulationMaterial, pass);
		}

		public override void SetFloat(string floatName, float floatValue)
		{
			if (m_simulationMaterial.HasProperty(floatName))
			{
				m_simulationMaterial.SetFloat(floatName, floatValue);
				return;
			}
			throw new InvalidProgramException("Material with the name '" + m_simulationMaterial.name + "', does not contain a property called " + floatName);
		}

		public override void SetInt(string intName, int intValue)
		{
			if (m_simulationMaterial.HasProperty(intName))
			{
				m_simulationMaterial.SetInt(intName, intValue);
				return;
			}
			throw new InvalidProgramException("Material with the name '" + m_simulationMaterial.name + "', does not contain a property called " + intName);
		}

		public override void SetTexture(string textureName, Texture simTexture)
		{
			if (textureName.Equals(outputTextureName))
			{
				targetTexture = simTexture as RenderTexture;
				return;
			}
			if (m_simulationMaterial.HasProperty(textureName))
			{
				m_simulationMaterial.SetTexture(textureName, simTexture);
				return;
			}
			throw new InvalidOperationException("Material with the name '" + m_simulationMaterial.name + "', does not contain a property called " + textureName);
		}

		public override bool SupportedBySystem()
		{
			bool flag = base.SupportedBySystem();
			int num = 30;
			return SystemInfo.graphicsShaderLevel >= num && flag;
		}

		protected void CreateSimulationMaterial()
		{
			m_simulationMaterial = new Material(simulationShader);
		}
	}
}
