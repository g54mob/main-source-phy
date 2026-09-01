namespace WatsonTcp
{
	internal class SyncResponseReceivedEventArgs
	{
		public WatsonMessage Message { get; set; }

		public byte[] Data { get; set; }

		public SyncResponseReceivedEventArgs(WatsonMessage msg, byte[] data)
		{
			Message = msg;
			Data = data;
		}
	}
}
