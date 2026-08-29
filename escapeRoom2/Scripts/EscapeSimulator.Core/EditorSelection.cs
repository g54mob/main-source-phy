using System.Collections.Generic;
using System.Text;

public struct EditorSelection
{
	public PropID propID;

	public HashSet<InstanceID> propInstanceIDs;

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("propID: " + propID.value);
		stringBuilder.AppendLine("propInstanceIDs: " + string.Join(", ", propInstanceIDs));
		return stringBuilder.ToString();
	}
}
