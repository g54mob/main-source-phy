namespace CloudinaryDotNet.Actions
{
	public class CreateUserParams : BaseUserParams
	{
		public CreateUserParams(string name, string email, Role role)
		{
			base.Name = name;
			base.Email = email;
			base.Role = role;
		}

		public override void Check()
		{
			Utils.ShouldNotBeEmpty(() => Name);
			Utils.ShouldNotBeEmpty(() => Email);
			Utils.ShouldBeSpecified(() => Role);
		}
	}
}
