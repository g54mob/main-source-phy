using System.Collections.Generic;

public interface ISaveable
{
	void save(FastBinaryWriter writer);

	void load(FastBinaryReader reader);

	void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties);
}
