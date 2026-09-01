using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class VolumeBrushRow : DataTableRow
	{
		public string name;

		public int size;

		public float randomness;

		public float densityOffset;

		[Space]
		public bool useTextures;

		public Texture2D xTexture;

		public float xOffset;

		public float xRotation;

		public Texture2D yTexture;

		public float yOffset;

		public float yRotation;

		public Texture2D zTexture;

		public float zOffset;

		public float zRotation;

		public bool textureRandomRotation;

		public Sprite icon;

		[Space]
		public Category category;

		public string group;

		public string slotName;

		public string path => category.ToString() + "/" + ((group != "") ? group : "None") + "/" + ((slotName != "") ? slotName : name);

		public string GetRowName()
		{
			return name;
		}
	}
}
