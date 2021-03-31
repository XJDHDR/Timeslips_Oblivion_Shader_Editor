// Decompiled with JetBrains decompiler
// Type: ShaderEdit.MainForm
// Assembly: ShaderEdit, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1518E5EC-D0FE-421C-9947-B6E80B5CE6F0

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ShaderEdit
{
	public class MainForm : Form
	{
		private uint unknown;
		private readonly List<MainForm.Shader> shaders = new List<MainForm.Shader>();
		private bool ChangedShader;
		private bool ChangedFile;
		private int Editing = -1;
		private string FileName = "";
		private HLSLImporter HLSLImporterForm = new HLSLImporter();
		private IContainer components;
		private TextBox tbEdit;
		private Button bSave;
		private Button bOpen;
		private ComboBox cmbShaderSelect;
		private OpenFileDialog openFileDialog1;
		private SaveFileDialog saveFileDialog1;
		private Button bCompile;
		private Button bClose;
		private ContextMenuStrip DudMenu;
		private Button bImport;
		private ContextMenuStrip ImportMenu;
		private ToolStripMenuItem importHLSLToolStripMenuItem;
		private ToolStripMenuItem importBinaryToolStripMenuItem;
		private ToolStripMenuItem exportBinaryToolStripMenuItem;

		[DllImport("ShaderDisasm", CharSet = CharSet.Ansi)]
		private static extern unsafe sbyte* Disasm(byte[] data, int len, byte Color);

		[DllImport("ShaderDisasm", CharSet = CharSet.Ansi)]
		private static extern unsafe byte* Asm(byte[] data, int len);

		[DllImport("ShaderDisasm", CharSet = CharSet.Ansi)]
		private static extern unsafe byte* Compile(
			string data,
			int len,
			string EntryPoint,
			string Profile,
			byte Debug);

		public MainForm() => this.InitializeComponent();

		private void cmbShaderSelect_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = true;

		private void bOpen_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Filter = "Oblivion shader package (*.sdp)|*.sdp";
			this.openFileDialog1.Title = "Select Shader package to edit";
			if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
				return;
			this.FileName = Path.GetFileName(this.openFileDialog1.FileName);
			this.Text = "SDP Editor (" + this.FileName + ")";
			BinaryReader binaryReader = new BinaryReader((Stream) File.OpenRead(this.openFileDialog1.FileName), Encoding.Default);
			this.unknown = binaryReader.ReadUInt32();
			int num = binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			for (int index1 = 0; index1 < num; ++index1)
			{
				MainForm.Shader shader = new MainForm.Shader();
				char[] chArray = binaryReader.ReadChars(256);
				shader.name = "";
				shader.name2 = chArray;
				for (int index2 = 0; index2 < 100 && chArray[index2] != char.MinValue; ++index2)
					shader.name += (string) (object) chArray[index2].ToString();
				int count = binaryReader.ReadInt32();
				shader.data = binaryReader.ReadBytes(count);
				this.shaders.Add(shader);
				this.cmbShaderSelect.Items.Add((object) shader.name);
			}
			binaryReader.Close();
			this.bOpen.Enabled = false;
			this.bClose.Enabled = true;
			this.cmbShaderSelect.Enabled = true;
			this.bSave.Enabled = true;
		}

		private unsafe void cmbShaderSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cmbShaderSelect.SelectedIndex == this.Editing || this.ChangedShader && !this.Compile() && MessageBox.Show("The current shader could not be compiled. Do you want to discard your changes?", "Question", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;
			this.Editing = this.cmbShaderSelect.SelectedIndex;
			this.tbEdit.Text = new string(MainForm.Disasm(this.shaders[this.Editing].data, this.shaders[this.Editing].data.Length, (byte) 0)).Replace("" + (object) '\n', Environment.NewLine);
			this.bCompile.Enabled = true;
			this.tbEdit.Enabled = true;
			this.tbEdit.TextChanged += new EventHandler(this.tbEdit_TextChanged);
			this.bImport.Enabled = true;
		}

		private void tbEdit_TextChanged(object sender, EventArgs e)
		{
			this.ChangedShader = true;
			this.Text = "SDP Editor (" + this.FileName + " *)";
			this.tbEdit.TextChanged -= new EventHandler(this.tbEdit_TextChanged);
		}

		private bool Save()
		{
			this.saveFileDialog1.Title = "Select file name to save as";
			this.saveFileDialog1.Filter = "Oblivion shader package (*.sdp)|*.sdp";
			if (this.ChangedShader && !this.Compile() && MessageBox.Show("One of your shaders did not compile. Do you want to save anyway?", "Question", MessageBoxButtons.YesNo) != DialogResult.Yes || this.saveFileDialog1.ShowDialog() != DialogResult.OK)
				return false;
			BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Create(this.saveFileDialog1.FileName), Encoding.Default);
			binaryWriter.Write(this.unknown);
			binaryWriter.Write(this.shaders.Count);
			int num = 0;
			foreach (MainForm.Shader shader in this.shaders)
				num += 260 + shader.data.Length;
			binaryWriter.Write(num);
			foreach (MainForm.Shader shader in this.shaders)
			{
				binaryWriter.Write(shader.name2);
				binaryWriter.Write(shader.data.Length);
				binaryWriter.Write(shader.data);
			}
			binaryWriter.Close();
			this.ChangedFile = false;
			return true;
		}

		private void bSave_Click(object sender, EventArgs e) => this.Save();

		private unsafe bool Compile()
		{
			byte[] data = new byte[this.tbEdit.Text.Length];
			for (int index = 0; index < this.tbEdit.Text.Length; ++index)
				data[index] = (byte) this.tbEdit.Text[index];
			byte* numPtr = MainForm.Asm(data, data.Length);
			int length = ((int) numPtr[3] << 24) + ((int) numPtr[2] << 16) + ((int) numPtr[1] << 8) + (int) *numPtr;
			if (length == 0)
			{
				string str = "";
				for (int index = 4; index < 65536 && numPtr[index] != (byte) 0; ++index)
					str += (string) (object) (char) numPtr[index];
				int num = (int) MessageBox.Show("Shader assembly failed: " + Environment.NewLine + str.Replace("" + (object) '\n', Environment.NewLine));
				return false;
			}
			this.shaders[this.Editing].data = new byte[length];
			byte[] numArray = new byte[length];
			for (int index = 0; index < length; ++index)
				numArray[index] = numPtr[index + 4];
			Array.Copy((Array) numArray, 0, (Array) this.shaders[this.Editing].data, 0, length);
			this.ChangedFile = true;
			this.ChangedShader = false;
			this.Text = "SDP Editor (" + this.FileName + ")";
			return true;
		}

		private void bCompile_Click(object sender, EventArgs e) => this.Compile();

		private void bClose_Click(object sender, EventArgs e)
		{
			if (this.ChangedShader || this.ChangedFile)
			{
				switch (MessageBox.Show("Do you want to save your changes?", "Question", MessageBoxButtons.YesNoCancel))
				{
					case DialogResult.Yes:
						if (!this.Save())
							return;
						break;
					case DialogResult.No:
						break;
					default:
						return;
				}
			}
			this.unknown = 0U;
			this.shaders.Clear();
			this.cmbShaderSelect.Enabled = false;
			this.bOpen.Enabled = true;
			this.bClose.Enabled = false;
			this.bSave.Enabled = false;
			this.bCompile.Enabled = false;
			this.ChangedFile = false;
			this.ChangedShader = false;
			this.Editing = -1;
			this.cmbShaderSelect.Items.Clear();
			this.cmbShaderSelect.Text = "";
			this.tbEdit.Text = "";
			this.tbEdit.Enabled = false;
			this.bImport.Enabled = false;
		}

		private void bImport_Click(object sender, EventArgs e) => this.ImportMenu.Show(this.PointToScreen(this.bImport.Location));

		private unsafe void importHLSLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Filter = "HLSL text file|*.*";
			this.openFileDialog1.Title = "Select HLSL file to import";
			if (this.openFileDialog1.ShowDialog() != DialogResult.OK || this.HLSLImporterForm.ShowDialog() != DialogResult.OK)
				return;
			string data = File.ReadAllText(this.openFileDialog1.FileName, Encoding.Default);
			byte* numPtr = MainForm.Compile(data, data.Length, this.HLSLImporterForm.EntryPoint, this.HLSLImporterForm.Profile, this.HLSLImporterForm.Debug);
			int length = ((int) numPtr[3] << 24) + ((int) numPtr[2] << 16) + ((int) numPtr[1] << 8) + (int) *numPtr;
			if (length == 0)
			{
				string str = "";
				for (int index = 4; index < 65536 && numPtr[index] != (byte) 0; ++index)
					str += (string) (object) (char) numPtr[index];
				int num = (int) MessageBox.Show("Shader compilation failed: " + Environment.NewLine + str.Replace("" + (object) '\n', Environment.NewLine));
			}
			else
			{
				this.shaders[this.Editing].data = new byte[length];
				byte[] numArray = new byte[length];
				for (int index = 0; index < length; ++index)
					numArray[index] = numPtr[index + 4];
				Array.Copy((Array) numArray, 0, (Array) this.shaders[this.Editing].data, 0, length);
				this.ChangedFile = true;
				this.ChangedShader = false;
				this.Text = "SDP Editor (" + this.FileName + ")";
				this.Editing = -1;
				this.cmbShaderSelect_SelectedIndexChanged((object) null, (EventArgs) null);
			}
		}

		private unsafe void importBinaryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Filter = "Compiled shader (*.vso,*.pso)|*.vso;*.pso";
			this.openFileDialog1.Title = "Select HLSL file to import";
			if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
				return;
			byte[] data = File.ReadAllBytes(this.openFileDialog1.FileName);
			sbyte* numPtr = MainForm.Disasm(data, data.Length, (byte) 0);
			if (new IntPtr((void*) numPtr) == IntPtr.Zero)
			{
				int num = (int) MessageBox.Show("An error occured during shader disassembly", "Error");
			}
			else
			{
				this.shaders[this.Editing].data = data;
				this.tbEdit.Text = new string(numPtr).Replace("" + (object) '\n', Environment.NewLine);
			}
		}

		private void exportBinaryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.ChangedShader && !this.Compile())
				return;
			this.saveFileDialog1.Title = "Select file name to save as";
			this.saveFileDialog1.Filter = "Compiled shader (*.vso,*.pso)|*.vso;*.pso";
			if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
				return;
			File.WriteAllBytes(this.saveFileDialog1.FileName, this.shaders[this.Editing].data);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
				this.components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = (IContainer) new Container();
			this.tbEdit = new TextBox();
			this.bSave = new Button();
			this.bOpen = new Button();
			this.cmbShaderSelect = new ComboBox();
			this.DudMenu = new ContextMenuStrip(this.components);
			this.openFileDialog1 = new OpenFileDialog();
			this.saveFileDialog1 = new SaveFileDialog();
			this.bCompile = new Button();
			this.bClose = new Button();
			this.bImport = new Button();
			this.ImportMenu = new ContextMenuStrip(this.components);
			this.importHLSLToolStripMenuItem = new ToolStripMenuItem();
			this.importBinaryToolStripMenuItem = new ToolStripMenuItem();
			this.exportBinaryToolStripMenuItem = new ToolStripMenuItem();
			this.ImportMenu.SuspendLayout();
			this.SuspendLayout();
			this.tbEdit.AcceptsReturn = true;
			this.tbEdit.AcceptsTab = true;
			this.tbEdit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tbEdit.Enabled = false;
			this.tbEdit.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
			this.tbEdit.Location = new Point(12, 12);
			this.tbEdit.Multiline = true;
			this.tbEdit.Name = "tbEdit";
			this.tbEdit.ScrollBars = ScrollBars.Vertical;
			this.tbEdit.Size = new Size(580, 350);
			this.tbEdit.TabIndex = 0;
			this.bSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.bSave.Enabled = false;
			this.bSave.Location = new Point(355, 368);
			this.bSave.Name = "bSave";
			this.bSave.Size = new Size(75, 23);
			this.bSave.TabIndex = 1;
			this.bSave.Text = "Save";
			this.bSave.UseVisualStyleBackColor = true;
			this.bSave.Click += new EventHandler(this.bSave_Click);
			this.bOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.bOpen.Location = new Point(517, 368);
			this.bOpen.Name = "bOpen";
			this.bOpen.Size = new Size(75, 23);
			this.bOpen.TabIndex = 2;
			this.bOpen.Text = "Open";
			this.bOpen.UseVisualStyleBackColor = true;
			this.bOpen.Click += new EventHandler(this.bOpen_Click);
			this.cmbShaderSelect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.cmbShaderSelect.ContextMenuStrip = this.DudMenu;
			this.cmbShaderSelect.Enabled = false;
			this.cmbShaderSelect.FormattingEnabled = true;
			this.cmbShaderSelect.Location = new Point(12, 370);
			this.cmbShaderSelect.MaxDropDownItems = 20;
			this.cmbShaderSelect.Name = "cmbShaderSelect";
			this.cmbShaderSelect.Size = new Size(155, 21);
			this.cmbShaderSelect.TabIndex = 3;
			this.cmbShaderSelect.SelectedIndexChanged += new EventHandler(this.cmbShaderSelect_SelectedIndexChanged);
			this.cmbShaderSelect.KeyPress += new KeyPressEventHandler(this.cmbShaderSelect_KeyPress);
			this.DudMenu.Name = "DudMenu";
			this.DudMenu.Size = new Size(61, 4);
			this.openFileDialog1.RestoreDirectory = true;
			this.saveFileDialog1.RestoreDirectory = true;
			this.bCompile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.bCompile.Enabled = false;
			this.bCompile.Location = new Point(173, 368);
			this.bCompile.Name = "bCompile";
			this.bCompile.Size = new Size(75, 23);
			this.bCompile.TabIndex = 4;
			this.bCompile.Text = "Compile";
			this.bCompile.UseVisualStyleBackColor = true;
			this.bCompile.Click += new EventHandler(this.bCompile_Click);
			this.bClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.bClose.Enabled = false;
			this.bClose.Location = new Point(436, 368);
			this.bClose.Name = "bClose";
			this.bClose.Size = new Size(75, 23);
			this.bClose.TabIndex = 5;
			this.bClose.Text = "Close";
			this.bClose.UseVisualStyleBackColor = true;
			this.bClose.Click += new EventHandler(this.bClose_Click);
			this.bImport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.bImport.Enabled = false;
			this.bImport.Location = new Point(254, 368);
			this.bImport.Name = "bImport";
			this.bImport.Size = new Size(83, 23);
			this.bImport.TabIndex = 6;
			this.bImport.Text = "Import/Export";
			this.bImport.UseVisualStyleBackColor = true;
			this.bImport.Click += new EventHandler(this.bImport_Click);
			this.ImportMenu.Items.AddRange(new ToolStripItem[3]
			{
				(ToolStripItem) this.importHLSLToolStripMenuItem,
				(ToolStripItem) this.importBinaryToolStripMenuItem,
				(ToolStripItem) this.exportBinaryToolStripMenuItem
			});
			this.ImportMenu.Name = "ImportMenu";
			this.ImportMenu.Size = new Size(151, 70);
			this.importHLSLToolStripMenuItem.Name = "importHLSLToolStripMenuItem";
			this.importHLSLToolStripMenuItem.Size = new Size(150, 22);
			this.importHLSLToolStripMenuItem.Text = "Import HLSL";
			this.importHLSLToolStripMenuItem.Click += new EventHandler(this.importHLSLToolStripMenuItem_Click);
			this.importBinaryToolStripMenuItem.Name = "importBinaryToolStripMenuItem";
			this.importBinaryToolStripMenuItem.Size = new Size(152, 22);
			this.importBinaryToolStripMenuItem.Text = "Import binary";
			this.importBinaryToolStripMenuItem.Click += new EventHandler(this.importBinaryToolStripMenuItem_Click);
			this.exportBinaryToolStripMenuItem.Name = "exportBinaryToolStripMenuItem";
			this.exportBinaryToolStripMenuItem.Size = new Size(152, 22);
			this.exportBinaryToolStripMenuItem.Text = "Export binary";
			this.exportBinaryToolStripMenuItem.Click += new EventHandler(this.exportBinaryToolStripMenuItem_Click);
			this.AutoScaleDimensions = new SizeF(6f, 13f);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new Size(604, 403);
			this.Controls.Add((Control) this.cmbShaderSelect);
			this.Controls.Add((Control) this.bClose);
			this.Controls.Add((Control) this.bImport);
			this.Controls.Add((Control) this.tbEdit);
			this.Controls.Add((Control) this.bCompile);
			this.Controls.Add((Control) this.bOpen);
			this.Controls.Add((Control) this.bSave);
			this.Name = nameof (MainForm);
			this.Text = "SDP editor";
			this.ImportMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private class Shader
		{
			internal string name;
			internal char[] name2;
			internal byte[] data;
		}
	}
}
