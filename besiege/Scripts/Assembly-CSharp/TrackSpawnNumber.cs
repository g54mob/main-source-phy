using System;
using UnityEngine;

public class TrackSpawnNumber : MonoBehaviour
{
	private static int count;

	public int max = 1;

	public TextMesh text;

	public static Action CompletedSecondary;

	private void Start()
	{
		if (count == 0)
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(Toggle));
		}
		count++;
		text.text = count + "/" + max;
		if (count == max && CompletedSecondary != null)
		{
			CompletedSecondary();
		}
	}

	private void Toggle(bool sim)
	{
		count = 0;
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(Toggle));
	}
}
