using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Animations;

public class PrefabConstraintSetup : MonoBehaviour
{
	public string sourceTag;

	private unsafe void Start()
	{
		//IL_00cd: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Object obj = default(Object);
		if (obj != null)
		{
			GameObject gameObject = GameObject.FindWithTag(sourceTag);
			if (!(gameObject != null))
			{
				string message = "No GameObject found with tag '" + sourceTag + "' for constraint.";
				Debug.LogWarning(message);
				return;
			}
			List<ConstraintSource> sources = new List<ConstraintSource>();
			((ParentConstraint)obj).SetSources(sources);
			Transform transform = gameObject.transform;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049FA30");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1817A5F10");
			object obj2 = default(object);
			int num = ((ParentConstraint)obj).AddSource((ConstraintSource)(&obj2));
			((ParentConstraint)obj).constraintActive = true;
		}
		else
		{
			Debug.LogWarning("No ParentConstraint on this prefab.");
		}
	}

	public PrefabConstraintSetup()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A3F8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		sourceTag = "Player";
		base._002Ector();
	}
}
