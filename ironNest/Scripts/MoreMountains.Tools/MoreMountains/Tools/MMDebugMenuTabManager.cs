using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMDebugMenuTabManager : MonoBehaviour
	{
		public List<MMDebugMenuTab> Tabs;

		public List<MMDebugMenuTabContents> TabsContents;

		public virtual void Select(int selected)
		{
		}
	}
}
