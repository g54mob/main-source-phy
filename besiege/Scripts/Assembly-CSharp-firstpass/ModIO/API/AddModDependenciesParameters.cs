namespace ModIO.API
{
	public class AddModDependenciesParameters : RequestParameters
	{
		public int[] dependencies
		{
			set
			{
				SetStringArrayValue("dependencies[]", value);
			}
		}
	}
}
