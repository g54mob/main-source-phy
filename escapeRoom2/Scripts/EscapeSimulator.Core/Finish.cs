using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour, ISaveable
{
	[DontSave]
	public FinishLevelShot levelShot;

	public virtual void save(FastBinaryWriter writer)
	{
	}

	public virtual void load(FastBinaryReader reader)
	{
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
	}
}
