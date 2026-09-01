using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding;

namespace InternalModding.Assemblies
{
	public class AssemblyLoader : SingleInstanceFindOnly<AssemblyLoader>, IComponentProvider
	{
		private List<ModAssembly> LoadedAssemblies = new List<ModAssembly>();

		public override string Name
		{
			get
			{
				return "ModMaster";
			}
		}

		public bool ActiveInSingleplayer
		{
			get
			{
				return true;
			}
		}

		public bool LoadMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModInfo.AssemblyInfo assembly in mod.Info.Assemblies)
			{
				assembly.Mod = mod;
				int changeset = VersionNumber.GetChangeset();
				int result2;
				int result3;
				if ((!int.TryParse(assembly.MinChangeset, out result2) || changeset >= result2) && (!int.TryParse(assembly.MaxChangeset, out result3) || changeset <= result3))
				{
					assembly.Resolve();
					ModAssembly mAssembly;
					if (!ValidateAssembly(assembly, out mAssembly))
					{
						MLog.Error("Assembly for " + mod.Info.Name + " did not validate, not loading it! (" + assembly.Path + ")");
						result = false;
					}
					else
					{
						mod.Assemblies.Add(mAssembly);
						LoadedAssemblies.Add(mAssembly);
					}
				}
			}
			return result;
		}

		public bool ActivateMod(ModContainer mod)
		{
			bool result = true;
			foreach (ModAssembly assembly in mod.Assemblies)
			{
				if (!LoadAssembly(assembly))
				{
					result = false;
				}
				else if (assembly.HasModEntryPoint)
				{
					assembly.ModEntryPoint.ModContainer = mod;
					if (!InitializeAssembly(assembly))
					{
						result = false;
						MLog.Error("There was an error activating assembly: " + assembly.Assembly.GetName().Name);
					}
				}
			}
			return result;
		}

		private bool ValidateAssembly(ModInfo.AssemblyInfo info, out ModAssembly mAssembly)
		{
			List<Assembly> list = new List<Assembly>();
			if (!AssemblyScanner.Scan(info, list))
			{
				mAssembly = null;
				return false;
			}
			mAssembly = new ModAssembly(info, list);
			if (info.Mod.Assemblies.Any((ModAssembly a) => a.Info.Path == info.Path))
			{
				MLog.Error("The same assembly is listed in the manifest more than once!");
				mAssembly = null;
				return false;
			}
			return true;
		}

		private bool LoadAssembly(ModAssembly mAssembly)
		{
			try
			{
				Assembly assembly = (mAssembly.Assembly = Assembly.LoadFrom(mAssembly.Info.Path));
				List<Type> list = (from t in assembly.GetTypes()
					where t.IsSubclassOf(typeof(ModEntryPoint)) && !t.IsAbstract
					select t).ToList();
				if (list.Count > 1)
				{
					throw new Exception("Too many types extending ModEntryPoint!");
				}
				if (list.Count == 1)
				{
					Type type = list[0];
					ModEntryPoint modEntryPoint = (ModEntryPoint)Activator.CreateInstance(type);
					mAssembly.HasModEntryPoint = true;
					mAssembly.ModEntryPoint = modEntryPoint;
				}
				else
				{
					mAssembly.HasModEntryPoint = false;
				}
				return true;
			}
			catch (Exception ex)
			{
				MLog.Error("Error loading assembly: " + mAssembly.Info.Path);
				MLog.Error(ex.ToString());
				return false;
			}
		}

		private bool InitializeAssembly(ModAssembly assembly)
		{
			return ModdingUtil.PerformCallback(assembly.ModEntryPoint.OnLoad);
		}

		public static bool IsModType(string typeName)
		{
			try
			{
				foreach (ModAssembly loadedAssembly in SingleInstanceFindOnly<AssemblyLoader>.Instance.LoadedAssemblies)
				{
					if (loadedAssembly.Assembly == null)
					{
						continue;
					}
					Type type = loadedAssembly.Assembly.GetType(typeName, false);
					if (type != null)
					{
						return true;
					}
					foreach (Assembly additionalReference in loadedAssembly.AdditionalReferences)
					{
						type = additionalReference.GetType(typeName, false);
						if (type != null)
						{
							return true;
						}
					}
				}
				return false;
			}
			catch (ReflectionTypeLoadException)
			{
				return true;
			}
			catch (TypeLoadException)
			{
				return true;
			}
			catch (FileNotFoundException)
			{
				return true;
			}
		}

		public static ModContainer GetModByAssembly(Assembly assembly)
		{
			return ModManager.Mods.FirstOrDefault((ModContainer c) => c.Assemblies.Any((ModAssembly mA) => mA.Assembly == assembly));
		}

		public void RegisterPrefabs(ModContainer mod)
		{
		}

		public void PostRegisterPrefabs()
		{
		}

		public void UnregisterPrefabs(ModContainer mod)
		{
		}
	}
}
