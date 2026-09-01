using System;

namespace ModIO
{
	[Serializable]
	public struct ModCommentPosition
	{
		public int depth;

		public int mainThread;

		public int replyThread;

		public int subReplyThread;
	}
}
