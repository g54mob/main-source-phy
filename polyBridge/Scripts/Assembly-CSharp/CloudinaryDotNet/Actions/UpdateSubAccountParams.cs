namespace CloudinaryDotNet.Actions
{
	public class UpdateSubAccountParams : BaseSubAccountParams
	{
		public string SubAccountId { get; set; }

		public UpdateSubAccountParams(string subAccountId)
		{
			SubAccountId = subAccountId;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => SubAccountId);
		}
	}
}
