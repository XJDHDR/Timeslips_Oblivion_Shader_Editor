// Decompiled with JetBrains decompiler
// Type: ShaderEdit.HLSLImporter
// Assembly: ShaderEdit, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1518E5EC-D0FE-421C-9947-B6E80B5CE6F0

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace ShaderEdit
{
  public class HLSLImporter : Form
  {
    private string profile;
    private string entryPoint;
    private bool debug;
    private IContainer components;
    private RadioButton radioButton1;
    private RadioButton radioButton2;
    private RadioButton radioButton3;
    private RadioButton radioButton4;
    private RadioButton radioButton5;
    private RadioButton radioButton6;
    private RadioButton radioButton7;
    private RadioButton radioButton8;
    private RadioButton radioButton9;
    private RadioButton radioButton10;
    private RadioButton radioButton11;
    private RadioButton radioButton12;
    private TextBox tbEntry;
    private Button bImport;
    private Button bCancel;
    private Label label1;
    private Label label2;
    private CheckBox cbDebug;

    public HLSLImporter()
    {
      this.InitializeComponent();
      this.DialogResult = DialogResult.Cancel;
    }

    public string Profile => this.profile;

    public string EntryPoint => this.entryPoint;

    public byte Debug => this.debug ? (byte) 1 : (byte) 0;

    private void bCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void bImport_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control is RadioButton && ((RadioButton) control).Checked)
        {
          this.profile = control.Text;
          break;
        }
      }
      this.entryPoint = this.tbEntry.Text;
      this.debug = this.cbDebug.Checked;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radioButton1 = new RadioButton();
      this.radioButton2 = new RadioButton();
      this.radioButton3 = new RadioButton();
      this.radioButton4 = new RadioButton();
      this.radioButton5 = new RadioButton();
      this.radioButton6 = new RadioButton();
      this.radioButton7 = new RadioButton();
      this.radioButton8 = new RadioButton();
      this.radioButton9 = new RadioButton();
      this.radioButton10 = new RadioButton();
      this.radioButton11 = new RadioButton();
      this.radioButton12 = new RadioButton();
      this.tbEntry = new TextBox();
      this.bImport = new Button();
      this.bCancel = new Button();
      this.label1 = new Label();
      this.label2 = new Label();
      this.cbDebug = new CheckBox();
      this.SuspendLayout();
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new Point(12, 26);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(60, 17);
      this.radioButton1.TabIndex = 0;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "ps_1_1";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(78, 26);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(60, 17);
      this.radioButton2.TabIndex = 1;
      this.radioButton2.Text = "ps_1_2";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new Point(144, 26);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(60, 17);
      this.radioButton3.TabIndex = 2;
      this.radioButton3.Text = "ps_1_3";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.radioButton4.AutoSize = true;
      this.radioButton4.Location = new Point(210, 26);
      this.radioButton4.Name = "radioButton4";
      this.radioButton4.Size = new Size(60, 17);
      this.radioButton4.TabIndex = 3;
      this.radioButton4.Text = "ps_1_4";
      this.radioButton4.UseVisualStyleBackColor = true;
      this.radioButton5.AutoSize = true;
      this.radioButton5.Location = new Point(12, 49);
      this.radioButton5.Name = "radioButton5";
      this.radioButton5.Size = new Size(60, 17);
      this.radioButton5.TabIndex = 4;
      this.radioButton5.Text = "ps_2_0";
      this.radioButton5.UseVisualStyleBackColor = true;
      this.radioButton6.AutoSize = true;
      this.radioButton6.Location = new Point(78, 49);
      this.radioButton6.Name = "radioButton6";
      this.radioButton6.Size = new Size(60, 17);
      this.radioButton6.TabIndex = 5;
      this.radioButton6.Text = "ps_2_a";
      this.radioButton6.UseVisualStyleBackColor = true;
      this.radioButton7.AutoSize = true;
      this.radioButton7.Location = new Point(144, 49);
      this.radioButton7.Name = "radioButton7";
      this.radioButton7.Size = new Size(60, 17);
      this.radioButton7.TabIndex = 6;
      this.radioButton7.Text = "ps_2_b";
      this.radioButton7.UseVisualStyleBackColor = true;
      this.radioButton8.AutoSize = true;
      this.radioButton8.Location = new Point(210, 49);
      this.radioButton8.Name = "radioButton8";
      this.radioButton8.Size = new Size(60, 17);
      this.radioButton8.TabIndex = 7;
      this.radioButton8.Text = "ps_3_0";
      this.radioButton8.UseVisualStyleBackColor = true;
      this.radioButton9.AutoSize = true;
      this.radioButton9.Location = new Point(12, 72);
      this.radioButton9.Name = "radioButton9";
      this.radioButton9.Size = new Size(60, 17);
      this.radioButton9.TabIndex = 8;
      this.radioButton9.TabStop = true;
      this.radioButton9.Text = "vs_1_1";
      this.radioButton9.UseVisualStyleBackColor = true;
      this.radioButton10.AutoSize = true;
      this.radioButton10.Location = new Point(78, 72);
      this.radioButton10.Name = "radioButton10";
      this.radioButton10.Size = new Size(60, 17);
      this.radioButton10.TabIndex = 9;
      this.radioButton10.Text = "vs_2_0";
      this.radioButton10.UseVisualStyleBackColor = true;
      this.radioButton11.AutoSize = true;
      this.radioButton11.Location = new Point(144, 72);
      this.radioButton11.Name = "radioButton11";
      this.radioButton11.Size = new Size(60, 17);
      this.radioButton11.TabIndex = 10;
      this.radioButton11.Text = "vs_2_a";
      this.radioButton11.UseVisualStyleBackColor = true;
      this.radioButton12.AutoSize = true;
      this.radioButton12.Location = new Point(210, 72);
      this.radioButton12.Name = "radioButton12";
      this.radioButton12.Size = new Size(60, 17);
      this.radioButton12.TabIndex = 11;
      this.radioButton12.Text = "vs_3_0";
      this.radioButton12.UseVisualStyleBackColor = true;
      this.tbEntry.Location = new Point(12, 95);
      this.tbEntry.MaxLength = 256;
      this.tbEntry.Name = "tbEntry";
      this.tbEntry.Size = new Size(126, 20);
      this.tbEntry.TabIndex = 12;
      this.tbEntry.Text = "Main";
      this.bImport.Location = new Point(158, 144);
      this.bImport.Name = "bImport";
      this.bImport.Size = new Size(75, 23);
      this.bImport.TabIndex = 13;
      this.bImport.Text = "Import";
      this.bImport.UseVisualStyleBackColor = true;
      this.bImport.Click += new EventHandler(this.bImport_Click);
      this.bCancel.DialogResult = DialogResult.Cancel;
      this.bCancel.Location = new Point(32, 144);
      this.bCancel.Name = "bCancel";
      this.bCancel.Size = new Size(75, 23);
      this.bCancel.TabIndex = 14;
      this.bCancel.Text = "Cancel";
      this.bCancel.UseVisualStyleBackColor = true;
      this.bCancel.Click += new EventHandler(this.bCancel_Click);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(77, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "Compiler target";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(144, 98);
      this.label2.Name = "label2";
      this.label2.Size = new Size(57, 13);
      this.label2.TabIndex = 16;
      this.label2.Text = "Entry point";
      this.cbDebug.AutoSize = true;
      this.cbDebug.Location = new Point(12, 121);
      this.cbDebug.Name = "cbDebug";
      this.cbDebug.Size = new Size(172, 17);
      this.cbDebug.TabIndex = 17;
      this.cbDebug.Text = "Compile with debug information";
      this.cbDebug.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.bImport;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.bCancel;
      this.ClientSize = new Size(292, 183);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cbDebug);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.bCancel);
      this.Controls.Add((Control) this.bImport);
      this.Controls.Add((Control) this.tbEntry);
      this.Controls.Add((Control) this.radioButton12);
      this.Controls.Add((Control) this.radioButton11);
      this.Controls.Add((Control) this.radioButton10);
      this.Controls.Add((Control) this.radioButton9);
      this.Controls.Add((Control) this.radioButton8);
      this.Controls.Add((Control) this.radioButton7);
      this.Controls.Add((Control) this.radioButton6);
      this.Controls.Add((Control) this.radioButton5);
      this.Controls.Add((Control) this.radioButton4);
      this.Controls.Add((Control) this.radioButton3);
      this.Controls.Add((Control) this.radioButton2);
      this.Controls.Add((Control) this.radioButton1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (HLSLImporter);
      this.Text = "HLSL importer settings";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
