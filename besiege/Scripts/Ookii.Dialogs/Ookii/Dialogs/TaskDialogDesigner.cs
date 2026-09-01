using System;
using System.ComponentModel.Design;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	internal class TaskDialogDesigner : ComponentDesigner
	{
		public override DesignerVerbCollection Verbs
		{
			get
			{
				DesignerVerbCollection designerVerbCollection = new DesignerVerbCollection();
				designerVerbCollection.Add(new DesignerVerb(Resources.Preview, Preview));
				return designerVerbCollection;
			}
		}

		private void Preview(object sender, EventArgs e)
		{
			((TaskDialog)base.Component).ShowDialog();
		}
	}
}
