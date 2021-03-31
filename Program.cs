// Decompiled with JetBrains decompiler
// Type: ShaderEdit.Program
// Assembly: ShaderEdit, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1518E5EC-D0FE-421C-9947-B6E80B5CE6F0

using System;
using System.Windows.Forms;

namespace ShaderEdit
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run((Form) new MainForm());
		}
	}
}
