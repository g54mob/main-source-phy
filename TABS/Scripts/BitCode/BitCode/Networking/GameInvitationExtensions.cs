namespace BitCode.Networking
{
	public static class GameInvitationExtensions
	{
		public static bool HasApplicationData(this IGameInvitation invitation)
		{
			if (invitation.ApplicationData != null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1458913982u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						return invitation.ApplicationData.Length != 0;
					}
					break;
				}
			}
			return false;
		}
	}
}
