using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poly.Base;
using Poly.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace Poly.Physics.Viewers
{
	public class NodeMassViewer : MonoBehaviour
	{
		private struct MaterialInfo
		{
			public EdgeMaterial material;

			public int numEdges;

			public float totalMass;

			public float oldTotalMass;
		}

		public Font font;

		[Range(1f, 24f)]
		public int fontSize = 24;

		public Color backgroundColor = new Color32(34, 140, 34, 77);

		public GameObject canvas;

		public Text text;

		public Node nodePrefab;

		public EdgeMaterial[] materialsToDisplay;

		private MaterialInfo[] materialInfos;

		private void OnGUI()
		{
			DrawGuiTextUtil.InitGuiStyle(font, backgroundColor, fontSize);
			foreach (NodeHandle nodeHandle in SingletonBehaviour<World>.instance.nodeHandles)
			{
				if (!nodeHandle.isAnchor)
				{
					Vec2 vec = nodeHandle.pos - 0.3f * Vec2.up;
					DrawGuiTextUtil.DisplayGuiLabel_Slow(text: $"{nodeHandle.massWhenDynamic:0.00}", posInWorld: vec - 1.5f * Vector3.forward);
				}
			}
			if (!Bridge.m_Simulating)
			{
				int num = BridgeJoints.m_Joints.Where((BridgeJoint j) => j.isActiveAndEnabled && !j.m_IsAnchor).Count();
				int num2 = BridgeEdges.m_Edges.Where((BridgeEdge j) => j.isActiveAndEnabled).Count();
				for (int num3 = 0; num3 < materialInfos.Length; num3++)
				{
					ref MaterialInfo reference = ref materialInfos[num3];
					reference.numEdges = 0;
					reference.totalMass = 0f;
					reference.oldTotalMass = 0f;
				}
				List<EdgeMaterial> list = materialsToDisplay.ToList();
				foreach (BridgeEdge edge in BridgeEdges.m_Edges)
				{
					if (edge.isActiveAndEnabled)
					{
						EdgeMaterial edgeMaterial = edge.m_Material.m_EdgeMaterial;
						int num4 = list.IndexOf(edgeMaterial);
						ref MaterialInfo reference2 = ref materialInfos[num4];
						reference2.numEdges++;
						float num5 = edgeMaterial.baseMass + edge.GetLength() * edgeMaterial.massPerMeter;
						float num6 = edgeMaterial.temp_old_baseMass + edge.GetLength() * edgeMaterial.temp_old_massPerMeter;
						reference2.totalMass += num5;
						reference2.oldTotalMass += num6;
					}
				}
				float num7 = (float)num * nodePrefab.define.mass;
				float num8 = 0f;
				float num9 = materialInfos.Sum((MaterialInfo i) => i.totalMass) + num7;
				float num10 = materialInfos.Sum((MaterialInfo i) => i.oldTotalMass) + num8;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Bridge mass statistics, at build or simulation start");
				stringBuilder.AppendLine("----------------------------------------------------");
				stringBuilder.AppendLine("                     new               old");
				stringBuilder.AppendLine(" Material     %      mass |     %      mass | ratio count");
				stringBuilder.AppendLine("");
				if (num9 != 0f || num10 != 0f)
				{
					string text = $"({num})";
					stringBuilder.AppendLine($"     Node {num7 / num9 * 100f,4:#0.0}%{num7,7:###0.00} kg | {num8 / num10 * 100f,4:#0.0}%{num8,7:###0.00} kg |       {text,-6}");
				}
				MaterialInfo[] array = materialInfos;
				for (int num11 = 0; num11 < array.Length; num11++)
				{
					MaterialInfo materialInfo = array[num11];
					if (materialInfo.totalMass != 0f || materialInfo.oldTotalMass != 0f)
					{
						float num12 = materialInfo.totalMass / num9 * 100f;
						float totalMass = materialInfo.totalMass;
						float num13 = materialInfo.oldTotalMass / num10 * 100f;
						float oldTotalMass = materialInfo.oldTotalMass;
						float num14 = ((oldTotalMass != 0f) ? (totalMass / oldTotalMass) : 0f);
						string text2 = $"({materialInfo.numEdges})";
						stringBuilder.AppendLine(string.Format($"{materialInfo.material.name,9}{num12,5:#0.0}%{materialInfo.totalMass,7:###0.00} kg |{num13,5:#0.0}%{materialInfo.oldTotalMass,7:###0.00} kg |  {num14,4:0.00} {text2,-6}"));
					}
				}
				if (num9 != 0f || num10 != 0f)
				{
					string text3 = $"({num2})";
					stringBuilder.AppendLine("");
					stringBuilder.AppendLine($"    Total     {num9,8:###0.00} kg |     {num10,8:###0.00} kg |  {num9 / num10,4:0.00} {text3,-6}");
				}
				this.text.text = stringBuilder.ToString();
			}
			PerformanceTimerDisplay.ResizeTimersPanel(this.text);
		}

		private void OnEnable()
		{
			canvas?.SetActive(value: true);
			materialInfos = new MaterialInfo[materialsToDisplay.Length];
			for (int i = 0; i < materialsToDisplay.Length; i++)
			{
				materialInfos[i].material = materialsToDisplay[i];
			}
			text.text = "Bridge mass statistics only in build mode and at simulation start";
		}

		private void OnDisable()
		{
			canvas?.SetActive(value: false);
		}
	}
}
