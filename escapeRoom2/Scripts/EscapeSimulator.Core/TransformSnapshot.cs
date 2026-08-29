using System.Text;
using UnityEngine;

public class TransformSnapshot
{
	public GameObject targetObject;

	public Vector3 position;

	public Quaternion rotation;

	public bool teleport;

	public float ownerTimestamp;

	public float receivedTimestamp;

	public byte localTimeResetIndicator;

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine(string.Format("{0}: {1}", "targetObject", targetObject));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "position", position));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "rotation", rotation));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "teleport", teleport));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "ownerTimestamp", ownerTimestamp));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "localTimeResetIndicator", localTimeResetIndicator));
		return stringBuilder.ToString();
	}
}
