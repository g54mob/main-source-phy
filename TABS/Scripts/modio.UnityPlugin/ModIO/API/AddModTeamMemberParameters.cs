namespace ModIO.API
{
	public class AddModTeamMemberParameters : RequestParameters
	{
		public string email
		{
			set
			{
				SetStringValue("email", value);
			}
		}

		public ModTeamMemberAccessLevel accessLevel
		{
			set
			{
				SetStringValue("level", (int)value);
			}
		}

		public string title
		{
			set
			{
				SetStringValue("position", value);
			}
		}
	}
}
