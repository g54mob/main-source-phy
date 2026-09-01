namespace CloudinaryDotNet.Actions
{
	public class UpdateUserGroupParams : BaseUserGroupParams
	{
		public string UserGroupId { get; set; }

		public UpdateUserGroupParams(string userGroupId, string name)
			: base(name)
		{
			UserGroupId = userGroupId;
		}

		public override void Check()
		{
			base.Check();
			Utils.ShouldNotBeEmpty(() => UserGroupId);
		}
	}
}
