using System;
using GLTFast.FakeSchema;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast
{
	internal class GltfJsonUtilityParser
	{
		public RootBase ParseJson(string json)
		{
			GLTFast.Schema.Root root;
			try
			{
				root = JsonUtility.FromJson<GLTFast.Schema.Root>(json);
				if (root == null)
				{
					return null;
				}
			}
			catch (ArgumentException)
			{
				return null;
			}
			finally
			{
			}
			if (root.JsonUtilitySecondParseRequired())
			{
				GLTFast.FakeSchema.Root fakeRoot = JsonUtility.FromJson<GLTFast.FakeSchema.Root>(json);
				root.JsonUtilityCleanupAgainstSecondParse(fakeRoot);
			}
			root.JsonUtilityCleanup();
			return root;
		}
	}
}
