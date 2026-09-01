using System;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace MeshBrush
{
	public static class TemplateMigrationUtility
	{
		public static bool TryMigrate(string filePath)
		{
			if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
			{
				Debug.LogError("MeshBrush: The specified template file path is invalid or doesn't exist! Cancelling migration process...");
				return false;
			}
			try
			{
				XDocument xDocument = XDocument.Load(filePath);
				MeshBrush meshBrush = new GameObject("MeshBrush Template Migration Utility")
				{
					hideFlags = HideFlags.HideAndDontSave
				}.AddComponent<MeshBrush>();
				foreach (XElement item in xDocument.Descendants())
				{
					switch (item.Name.LocalName)
					{
					case "meshBrushTemplate":
					{
						XAttribute xAttribute = item.Attribute("version");
						if (xAttribute != null && 1.9f <= float.Parse(xAttribute.Value))
						{
							Debug.LogWarning("MeshBrush: The template you tried to migrate actually is already up to date with the current format. Cancelling process...");
							return false;
						}
						break;
					}
					case "active":
					case "isActive":
						meshBrush.active = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "groupName":
						meshBrush.groupName = item.Value;
						break;
					case "classicUI":
						meshBrush.classicUI = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "previewIconSize":
						meshBrush.previewIconSize = float.Parse(item.Value);
						break;
					case "lockSceneView":
						meshBrush.lockSceneView = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "trisCounter":
						meshBrush.stats = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "globalPaintingLayers":
					{
						int num7 = 0;
						foreach (XElement item2 in item.Elements())
						{
							meshBrush.layerMask[num7] = string.CompareOrdinal(item2.Value, "false") != 0;
							num7++;
						}
						break;
					}
					case "paintKey":
						meshBrush.paintKey = (KeyCode)Enum.Parse(typeof(KeyCode), item.Value);
						break;
					case "deleteKey":
						meshBrush.deleteKey = (KeyCode)Enum.Parse(typeof(KeyCode), item.Value);
						break;
					case "combineAreaKey":
						meshBrush.combineKey = (KeyCode)Enum.Parse(typeof(KeyCode), item.Value);
						break;
					case "increaseRadiusKey":
						meshBrush.increaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), item.Value);
						break;
					case "decreaseRadiusKey":
						meshBrush.decreaseRadiusKey = (KeyCode)Enum.Parse(typeof(KeyCode), item.Value);
						break;
					case "brushRadius":
						meshBrush.radius = float.Parse(item.Value);
						break;
					case "color":
					case "brushColor":
						meshBrush.color = new Color(float.Parse(item.Element("r").Value), float.Parse(item.Element("g").Value), float.Parse(item.Element("b").Value), float.Parse(item.Element("a").Value));
						break;
					case "useMeshDensity":
						meshBrush.useDensity = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "minMeshDensity":
						meshBrush.densityRange.x = float.Parse(item.Value);
						break;
					case "maxMeshDensity":
						meshBrush.densityRange.y = float.Parse(item.Value);
						break;
					case "minNrOfMeshes":
						meshBrush.quantityRange.x = float.Parse(item.Value);
						break;
					case "maxNrOfMeshes":
						meshBrush.quantityRange.y = float.Parse(item.Value);
						break;
					case "delay":
						meshBrush.delay = float.Parse(item.Value);
						break;
					case "verticalOffset":
					{
						float num6 = float.Parse(item.Value);
						meshBrush.offsetRange = new Vector2(num6, num6);
						break;
					}
					case "alignWithStroke":
						meshBrush.strokeAlignment = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "slopeInfluence":
					{
						float num5 = float.Parse(item.Value);
						meshBrush.slopeInfluenceRange = new Vector2(num5, num5);
						break;
					}
					case "useSlopeFilter":
						meshBrush.useSlopeFilter = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "maxSlopeFilterAngle":
					{
						float num4 = float.Parse(item.Value);
						meshBrush.angleThresholdRange = new Vector2(num4, num4);
						break;
					}
					case "inverseSlopeFilter":
						meshBrush.inverseSlopeFilter = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "manualReferenceVectorSampling":
						meshBrush.manualReferenceVectorSampling = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "showReferenceVectorInSceneGUI":
						meshBrush.showReferenceVectorInSceneView = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "slopeReferenceVector":
						meshBrush.slopeReferenceVector = new Vector3(float.Parse(item.Element("x").Value), float.Parse(item.Element("y").Value), float.Parse(item.Element("z").Value));
						break;
					case "slopeReferenceVector_HandleLocation":
						meshBrush.slopeReferenceVectorSampleLocation = new Vector3(float.Parse(item.Element("x").Value), float.Parse(item.Element("y").Value), float.Parse(item.Element("z").Value));
						break;
					case "yAxisIsTangent":
						meshBrush.yAxisTangent = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "scattering":
					{
						float num3 = float.Parse(item.Value);
						meshBrush.scatteringRange = new Vector2(num3, num3);
						break;
					}
					case "autoStatic":
						meshBrush.autoStatic = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "useOverlapFilter":
						meshBrush.useOverlapFilter = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "randomAbsMinDist":
						meshBrush.minimumAbsoluteDistanceRange = new Vector2(float.Parse(item.Element("x").Value), float.Parse(item.Element("y").Value));
						break;
					case "uniformScale":
						meshBrush.uniformRandomScale = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "constantUniformScale":
						meshBrush.uniformAdditiveScale = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_SetOfMeshesToPaint":
						meshBrush.meshesFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_Templates":
						meshBrush.templatesFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_CustomizeKeyboardShortcuts":
						meshBrush.keyBindingsFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_BrushSettings":
						meshBrush.brushFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_Slopes":
						meshBrush.slopesFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_Randomizers":
						meshBrush.randomizersFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_OverlapFilter":
						meshBrush.overlapFilterFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_ApplyAdditiveScale":
						meshBrush.additiveScaleFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "foldoutState_Optimize":
						meshBrush.optimizationFoldout = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					case "randomUniformRange":
						meshBrush.randomScaleRange = new Vector2(float.Parse(item.Element("x").Value), float.Parse(item.Element("y").Value));
						break;
					case "randomNonUniformRange":
						meshBrush.randomScaleRangeX = (meshBrush.randomScaleRangeZ = new Vector2(float.Parse(item.Element("x").Value), float.Parse(item.Element("y").Value)));
						meshBrush.randomScaleRangeY = new Vector2(float.Parse(item.Element("z").Value), float.Parse(item.Element("w").Value));
						break;
					case "constantAdditiveScale":
					{
						float num2 = float.Parse(item.Value);
						meshBrush.additiveScaleRange = new Vector2(num2, num2);
						break;
					}
					case "constantScaleXYZ":
						meshBrush.additiveScaleNonUniform = new Vector3(float.Parse(item.Element("x").Value), float.Parse(item.Element("y").Value), float.Parse(item.Element("z").Value));
						break;
					case "randomRotation":
					{
						float num = float.Parse(item.Value);
						meshBrush.randomRotationRange = new Vector2(num, num);
						break;
					}
					case "autoSelectOnCombine":
						meshBrush.autoSelectOnCombine = string.CompareOrdinal(item.Value, "true") == 0;
						break;
					}
				}
				meshBrush.SaveTemplate(filePath.Replace(".meshbrush", "__migrated.xml"));
			}
			catch (Exception ex)
			{
				Debug.LogError("MeshBrush: Failed to migrate template file \"" + filePath + "\". Perhaps the file is corrupted? " + ex.ToString());
				return false;
			}
			return true;
		}
	}
}
