using System.Collections.Generic;

namespace LevelCreator
{
	public struct EntityTreeNode
	{
		public Level.Entity entity;

		public List<EntityTreeNode> childs;
	}
}
