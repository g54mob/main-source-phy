namespace ModIO.API
{
	public class DeleteModDependenciesParameters : RequestParameters
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
