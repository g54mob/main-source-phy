using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class LevelUtil
	{
		public const int historyMaxCount = 100;

		public const int maxObjectCount = 2000;

		public const int maxHiearchyDepth = 5;

		public const float positionMargin = 16f;

		public const float minPosition = -16f;

		public const float maxPosition = 144f;

		public const float minScale = 0.1f;

		public const float maxScale = 40f;

		private static void PopulateChilds(List<EntityTreeNode> targetEntities, List<Level.FlatEntity> sourceFlatEntities, Dictionary<Guid, List<int>> childsLookup)
		{
			for (int i = 0; i < targetEntities.Count; i++)
			{
				if (!childsLookup.TryGetValue(targetEntities[i].entity.guid, out var value))
				{
					continue;
				}
				List<EntityTreeNode> list = new List<EntityTreeNode>();
				targetEntities[i] = new EntityTreeNode
				{
					entity = targetEntities[i].entity,
					childs = list
				};
				foreach (int item in value)
				{
					targetEntities[i].childs.Add(new EntityTreeNode
					{
						entity = sourceFlatEntities[item].entity
					});
				}
				PopulateChilds(list, sourceFlatEntities, childsLookup);
			}
		}

		public static List<EntityTreeNode> BuildEntityTrees(List<Level.FlatEntity> flatEntities)
		{
			List<EntityTreeNode> list = new List<EntityTreeNode>();
			Dictionary<Guid, List<int>> dictionary = new Dictionary<Guid, List<int>>();
			for (int i = 0; i < flatEntities.Count; i++)
			{
				if (flatEntities[i].parentGuid == Guid.Empty)
				{
					list.Add(new EntityTreeNode
					{
						entity = flatEntities[i].entity
					});
					continue;
				}
				if (!dictionary.TryGetValue(flatEntities[i].parentGuid, out var value))
				{
					value = new List<int>();
					dictionary.Add(flatEntities[i].parentGuid, value);
				}
				value.Add(i);
			}
			PopulateChilds(list, flatEntities, dictionary);
			return list;
		}

		public static EntityTreeNode BuildEntityTree(DMEditorComponent editorObject)
		{
			List<EntityTreeNode> list = null;
			foreach (Transform item in editorObject.transform)
			{
				if (!item.gameObject.activeSelf)
				{
					continue;
				}
				DMEditorComponent component = item.GetComponent<DMEditorComponent>();
				if ((bool)component)
				{
					if (list == null)
					{
						list = new List<EntityTreeNode>();
					}
					list.Add(BuildEntityTree(component));
				}
			}
			return new EntityTreeNode
			{
				entity = new Level.Entity
				{
					guid = editorObject.entity.guid,
					objectTypeId = editorObject.entity.objectTypeId,
					position = editorObject.entity.position,
					slope = editorObject.entity.slope,
					rotation = editorObject.entity.rotation,
					scale = editorObject.entity.scale,
					heightOffset = editorObject.entity.heightOffset
				},
				childs = list
			};
		}

		public static void AddChildEntities(List<Level.FlatEntity> entities, Guid parentGuid, GameObject gameObject)
		{
			foreach (Transform item in gameObject.transform)
			{
				if (item.gameObject.activeSelf)
				{
					DMEditorComponent component = item.GetComponent<DMEditorComponent>();
					if ((bool)component)
					{
						entities.Add(new Level.FlatEntity
						{
							entity = component.entity.Clone(),
							parentGuid = parentGuid
						});
						AddChildEntities(entities, component.entity.guid, component.gameObject);
					}
				}
			}
		}

		public static AnalyzeLevelResult AnalyzeLevel(GameObject rootObject)
		{
			int objectCount = 0;
			return AnalyzeLevelRecursively(rootObject, 0);
			AnalyzeLevelResult AnalyzeLevelRecursively(GameObject gameObject, int currentHiearchyDepth)
			{
				if (++objectCount > 2000)
				{
					return AnalyzeLevelResult.ObjectCountExceeded;
				}
				if (currentHiearchyDepth >= 5)
				{
					return AnalyzeLevelResult.HiearchyDepthExceeded;
				}
				foreach (Transform item in gameObject.transform)
				{
					if (item.gameObject.activeSelf)
					{
						DMEditorComponent component = item.GetComponent<DMEditorComponent>();
						if ((bool)component)
						{
							EntityTransformation globalEntityTransform = component.GetGlobalEntityTransform();
							Vector3 position = globalEntityTransform.position;
							if (position.x < -16f || position.x > 144f || position.y < -16f || position.y > 144f || position.z < -16f || position.z > 144f)
							{
								return AnalyzeLevelResult.PositionLimitsExceeded;
							}
							Vector3 scale = globalEntityTransform.scale;
							if (scale.x > 40f || scale.y > 40f || scale.z > 40f)
							{
								return AnalyzeLevelResult.MaxScaleExceeded;
							}
							if (scale.x < 0.1f || scale.y < 0.1f || scale.z < 0.1f)
							{
								return AnalyzeLevelResult.MinScaleExceeded;
							}
							AnalyzeLevelResult analyzeLevelResult = AnalyzeLevelRecursively(component.gameObject, currentHiearchyDepth + 1);
							if (analyzeLevelResult != AnalyzeLevelResult.Approved)
							{
								return analyzeLevelResult;
							}
						}
					}
				}
				return AnalyzeLevelResult.Approved;
			}
		}

		public static string LevelResultToErrorMessage(AnalyzeLevelResult analyzeLevelResult)
		{
			switch (analyzeLevelResult)
			{
			case AnalyzeLevelResult.Approved:
				return null;
			case AnalyzeLevelResult.ObjectCountExceeded:
				return "LC_OBJECT_COUNT_EXCEEDED";
			case AnalyzeLevelResult.HiearchyDepthExceeded:
				return "LC_HIERARCHY_DEPTH_EXCEEDED";
			case AnalyzeLevelResult.PositionLimitsExceeded:
				return "LC_POSITION_LIMITS_EXCEEDED";
			case AnalyzeLevelResult.MaxScaleExceeded:
				return "LC_MAX_SCALE_EXCEEDED";
			case AnalyzeLevelResult.MinScaleExceeded:
				return "LC_MIN_SCALE_EXCEEDED";
			default:
				return "LC_UNKNOWN_LEVEL_ERROR";
			}
		}
	}
}
