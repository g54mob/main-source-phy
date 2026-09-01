namespace Modding.Modules
{
	internal interface IModuleBehaviour
	{
		object RawModule { get; set; }

		string ModuleGuid { get; set; }

		void OnReload();
	}
}
