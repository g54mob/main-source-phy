using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ModIO
{
	[Serializable]
	public class ModComment
	{
		[JsonProperty("id")]
		public int id;

		[JsonProperty("mod_id")]
		public int modId;

		[JsonProperty("submitted_by")]
		public UserProfile submittedBy;

		[JsonProperty("date_added")]
		public int dateAdded;

		[JsonProperty("reply_id")]
		public int replyId;

		[JsonProperty("position")]
		public ModCommentPosition position;

		[JsonProperty("karma")]
		public int karma;

		[JsonProperty("karma_guest")]
		public int karmaGuest;

		[JsonProperty("content")]
		public string content;

		[JsonProperty("thread_position")]
		private string _threadPositionString;

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (string.IsNullOrEmpty(_threadPositionString))
			{
				return;
			}
			position = default(ModCommentPosition);
			string[] array = _threadPositionString.Split('.');
			position.depth = 0;
			position.mainThread = -1;
			position.replyThread = -1;
			position.subReplyThread = -1;
			if (array.Length > 0)
			{
				position.depth = 1;
				if (int.TryParse(array[0], out position.mainThread) && array.Length > 1)
				{
					position.depth = 2;
					if (int.TryParse(array[1], out position.replyThread) && array.Length > 2)
					{
						position.depth = 3;
						int.TryParse(array[2], out position.subReplyThread);
					}
				}
			}
			_threadPositionString = null;
		}
	}
}
