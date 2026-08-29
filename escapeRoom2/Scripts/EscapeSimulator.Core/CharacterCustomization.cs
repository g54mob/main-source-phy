using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterCustomization : ISaveable
{
	public CharacterBuild.Outfit outfit = CharacterBuild.Outfit.Dracula;

	public CharacterBuild.Hair hair = CharacterBuild.Hair.Hair1;

	public CharacterBuild.Gender gender = CharacterBuild.Gender.Female;

	public int skinType = 1;

	public int hairColor;

	public CharacterBuild.EyeColor eyeColor;

	public Dictionary<string, int> getCategoryToSavedVariant()
	{
		return new Dictionary<string, int>
		{
			{
				"Outfit",
				(int)outfit
			},
			{
				"Hair",
				(int)hair
			},
			{
				"Gender",
				(int)gender
			},
			{ "SkinColor", skinType },
			{ "HairColor", hairColor },
			{
				"EyeColor",
				(int)eyeColor
			}
		};
	}

	public void randomize()
	{
		outfit = (CharacterBuild.Outfit)UnityEngine.Random.Range(1, 7);
		if (CharacterBuild.isPremiumOutfit(outfit))
		{
			outfit--;
		}
		hair = (CharacterBuild.Hair)UnityEngine.Random.Range(1, 6);
		gender = (CharacterBuild.Gender)UnityEngine.Random.Range(0, 2);
		skinType = UnityEngine.Random.Range(0, 5);
		hairColor = UnityEngine.Random.Range(0, 5);
		eyeColor = (CharacterBuild.EyeColor)UnityEngine.Random.Range(0, Enum.GetValues(typeof(CharacterBuild.EyeColor)).Length);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		int value = (int)outfit;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)hair;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)gender;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in skinType, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in hairColor, default(FastBinaryWriter.ForPrimitives));
		value = (int)eyeColor;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		outfit = (CharacterBuild.Outfit)reader.ReadInt32();
		hair = (CharacterBuild.Hair)reader.ReadInt32();
		gender = (CharacterBuild.Gender)reader.ReadInt32();
		skinType = reader.ReadInt32();
		hairColor = reader.ReadInt32();
		eyeColor = (CharacterBuild.EyeColor)reader.ReadInt32();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		CharacterBuild.Outfit outfit = (CharacterBuild.Outfit)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "outfit",
			fieldValue = $"{outfit}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		CharacterBuild.Hair hair = (CharacterBuild.Hair)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hair",
			fieldValue = $"{hair}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		CharacterBuild.Gender gender = (CharacterBuild.Gender)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gender",
			fieldValue = $"{gender}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "skinType",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "hairColor",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		CharacterBuild.EyeColor eyeColor = (CharacterBuild.EyeColor)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeColor",
			fieldValue = $"{eyeColor}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
