namespace ModModels
{
	public struct ModCompileInstructions
	{
		public int ID;

		public string OutputFileName;

		public string[] Paths;

		public string[] AssemblyReferenceLocations;

		public string MainClass;

		public string Directory;

		public bool RejectShadyCode;

		public bool FromWorkshop;

		public string InsertSourceB64;

		public string AssemblyName;
	}
}
