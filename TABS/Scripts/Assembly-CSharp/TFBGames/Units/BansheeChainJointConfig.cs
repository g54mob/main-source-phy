using UnityEngine;

namespace TFBGames.Units
{
	public class BansheeChainJointConfig : MonoBehaviour
	{
		private ConfigurableJoint joint;

		private void Awake()
		{
			joint = base.gameObject.GetComponent<ConfigurableJoint>();
			SetJointsMotions(ConfigurableJointMotion.Free);
		}

		private void Start()
		{
			SetJointsMotions(ConfigurableJointMotion.Locked);
		}

		private void SetJointsMotions(ConfigurableJointMotion value)
		{
			if (joint != null)
			{
				joint.xMotion = value;
				joint.yMotion = value;
				joint.zMotion = value;
			}
			else
			{
				Debug.LogError("Expected Configurable Joint but none found on object.");
			}
		}
	}
}
