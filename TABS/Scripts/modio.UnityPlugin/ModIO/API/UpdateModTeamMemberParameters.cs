namespace ModIO.API
{
	public class UpdateModTeamMemberParameters : RequestParameters
	{
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
