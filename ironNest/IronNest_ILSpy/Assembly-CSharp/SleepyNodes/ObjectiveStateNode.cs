using System;

namespace SleepyNodes;

[Serializable]
public abstract class ObjectiveStateNode : StateNode
{
	protected ObjectiveStateNode()
	{
		Guid guid = Guid.NewGuid();
		Guid guid2 = default(Guid);
		NodeID = guid2.ToString();
		((Node)this)._002Ector();
	}
}
