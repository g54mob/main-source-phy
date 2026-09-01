using UnityEngine;

public class FollowWheel : MonoBehaviour
{
	public WheelFollower[] followers;

	public AnimationCurve wheelBumpCurve;

	private void LateUpdate()
	{
		for (int i = 0; i < followers.Length; i++)
		{
			if (!(followers[i].collider == null) && !(followers[i].model == null))
			{
				followers[i].collider.GetWorldPose(out var pos, out var quat);
				followers[i].model.transform.position = pos;
				followers[i].model.transform.rotation = quat;
				followers[i].collider.radius = wheelBumpCurve.Evaluate(followers[i].model.localEulerAngles.x);
			}
		}
	}
}
