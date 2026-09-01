using System;
using System.Collections.Generic;

[Serializable]
public class RequirementSet
{
	[Serializable]
	public class RequirementPair
	{
		public enum Operation
		{
			Base = 0,
			And = 1,
			Or = 2
		}

		public Operation operation;

		public Requirement requirement;
	}

	public RequirementPair[] requirements;

	public bool Resolve(Dictionary<string, object> variables)
	{
		return false;
	}
}
