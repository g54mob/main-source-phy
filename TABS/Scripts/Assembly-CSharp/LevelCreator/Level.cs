using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class Level
	{
		public class VoxelChunk
		{
			public static Vector3Int noOfCells = new Vector3Int(16, 16, 16);

			public static BoundsInt voxelBounds = new BoundsInt(Vector3Int.one, chunkCount * noOfCells - Vector3Int.one);

			public float[,,] densities;

			public int version;
		}

		public class MaterialChunk
		{
			public static Vector3Int noOfCells = new Vector3Int(8, 2, 8);

			public static BoundsInt materialBounds = new BoundsInt(Vector3Int.zero, chunkCount * noOfCells + Vector3Int.one);

			public float[,,] densities;

			public int version;
		}

		public class FoliageChunk
		{
			public static Vector3Int noOfCells = new Vector3Int(8, 2, 8);

			public static BoundsInt foliageBounds = new BoundsInt(Vector3Int.zero, chunkCount * noOfCells);

			public float[,,] densities;

			public int version;
		}

		public class VolumeChunk
		{
			public VoxelChunk voxelChunk;

			public MaterialChunk materialChunk;

			public FoliageChunk foliageChunk;

			public bool HasSameVersions(VolumeChunk other)
			{
				if (voxelChunk.version == other.voxelChunk.version && materialChunk.version == other.materialChunk.version)
				{
					return foliageChunk.version == other.foliageChunk.version;
				}
				return false;
			}
		}

		public struct Entity
		{
			public Guid guid;

			public string objectTypeId;

			public Vector3 position;

			public Quaternion slope;

			public Quaternion rotation;

			public Vector3 scale;

			public Dictionary<string, string> customData;

			public float heightOffset;

			public Entity Clone()
			{
				return new Entity
				{
					guid = guid,
					objectTypeId = objectTypeId,
					position = position,
					slope = slope,
					rotation = rotation,
					scale = scale,
					customData = customData,
					heightOffset = heightOffset
				};
			}

			public override string ToString()
			{
				string text = "";
				if (customData == null)
				{
					text = "null";
				}
				else
				{
					text += "{";
					bool flag = true;
					foreach (KeyValuePair<string, string> customDatum in customData)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							text += " ";
						}
						text = text + customDatum.Key + ": " + customDatum.Value;
					}
					text += "}";
				}
				return string.Concat("{ guid: ", guid, ", type: ", objectTypeId, " position: ", position, ", slope: ", slope, ", rotation: ", rotation, ", scale: ", scale, ", customData = ", text, ", heightOffset: ", heightOffset, " }");
			}
		}

		public struct FlatEntity
		{
			public Entity entity;

			public Guid parentGuid;
		}

		public class Scene
		{
			public List<FlatEntity> flatEntities = new List<FlatEntity>();
		}

		public class Volume
		{
			public Dictionary<Vector3Int, VolumeChunk> volumeChunks = new Dictionary<Vector3Int, VolumeChunk>();
		}

		public class Settings
		{
			public bool showWater = true;

			public float waterLevel;

			public int weatherIndex;

			public int musicIndex;

			public string presetName = "";

			public Quaternion timeOfDay = Quaternion.Euler(80f, 0f, 0f);

			public Color sunColor = Color.white;

			public float sunIntensity = 0.8f;

			public Color ambientSkyColor = Color.white;

			public Color ambientEquatorColor = Color.white;

			public Color ambientGroundColor = Color.white;

			public float skyboxDayBlend;

			public float skyboxNightBlend;
		}

		public static Vector3Int chunkCount = new Vector3Int(8, 2, 8);

		public Settings settings = new Settings();

		public Scene scene = new Scene();

		public Volume volume = new Volume();
	}
}
