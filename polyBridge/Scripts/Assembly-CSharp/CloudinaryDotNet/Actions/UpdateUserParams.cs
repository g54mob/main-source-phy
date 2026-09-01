namespace CloudinaryDotNet.Actions
{
	public class UpdateUserParams : BaseUserParams
	{
		public string UserId { get; set; }

		public UpdateUserParams(string userId)
		{
			UserId = userId;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => UserId);
		}
	}
}
