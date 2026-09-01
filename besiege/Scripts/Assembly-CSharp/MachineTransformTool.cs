using UnityEngine;

[AddComponentMenu("UI/Tools/Machine Tranform Tool")]
public class MachineTransformTool : ClickBehaviour
{
	protected float currentInterval;

	private Vector3 lastSentPosition = Vector3.zero;

	private Quaternion lastSentRotation = Quaternion.identity;

	protected bool hasNetworkedTransform;

	protected Machine startMachine;

	public void UpdateTransformInfo(Machine m)
	{
		if (StatMaster.isMP)
		{
			currentInterval += TimeSlider.Instance.deltaTime;
			if (currentInterval >= OptionsMaster.networkTransformInterval)
			{
				SendTransformInfo(m);
				currentInterval = 0f;
			}
		}
	}

	public void SendTransformInfo(Machine m)
	{
		if (StatMaster.isMP)
		{
			Vector3 position = m.Position;
			if (!hasNetworkedTransform || lastSentPosition != position)
			{
				NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.Translate, NetworkCompression.PackVector(position));
				lastSentPosition = position;
			}
			Quaternion rotation = m.Rotation;
			if (!hasNetworkedTransform || lastSentRotation != rotation)
			{
				NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.Rotate, NetworkCompression.PackQuaternion(rotation));
				lastSentRotation = rotation;
			}
			hasNetworkedTransform = true;
		}
	}
}
