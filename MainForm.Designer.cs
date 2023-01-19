/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 2/8/2010
 * Time: 5:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ExpertMultimedia
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.lvMain = new System.Windows.Forms.ListView();
			this.button1 = new System.Windows.Forms.Button();
			this.btnSwitchFormulas = new System.Windows.Forms.Button();
			this.tbFile0 = new System.Windows.Forms.TextBox();
			this.tbFileOut = new System.Windows.Forms.TextBox();
			this.cbHtml = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnSaveIngredientToRecipeGuide = new System.Windows.Forms.Button();
			this.btnLoadShopInventory = new System.Windows.Forms.Button();
			this.btnLoadItemList = new System.Windows.Forms.Button();
			this.btnLoadEquipmentList = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnLoadIngredientList = new System.Windows.Forms.Button();
			this.btnSortByAttribBoostAsc = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.lbFiles = new System.Windows.Forms.ListBox();
			this.tbStatus = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lblHelp = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvMain
			// 
			this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMain.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvMain.FullRowSelect = true;
			this.lvMain.GridLines = true;
			this.lvMain.Location = new System.Drawing.Point(287, 6);
			this.lvMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.lvMain.Name = "lvMain";
			this.lvMain.Size = new System.Drawing.Size(547, 420);
			this.lvMain.TabIndex = 0;
			this.lvMain.UseCompatibleStateImageBehavior = false;
			this.lvMain.View = System.Windows.Forms.View.Details;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(3, 258);
			this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(266, 24);
			this.button1.TabIndex = 1;
			this.button1.Text = "Load as z=y+x";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// btnSwitchFormulas
			// 
			this.btnSwitchFormulas.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSwitchFormulas.Location = new System.Drawing.Point(3, 161);
			this.btnSwitchFormulas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnSwitchFormulas.Name = "btnSwitchFormulas";
			this.btnSwitchFormulas.Size = new System.Drawing.Size(266, 24);
			this.btnSwitchFormulas.TabIndex = 2;
			this.btnSwitchFormulas.Text = "Load as Recipes for Items: as x+y=z";
			this.btnSwitchFormulas.UseVisualStyleBackColor = true;
			this.btnSwitchFormulas.Click += new System.EventHandler(this.BtnSwitchFormulasClick);
			// 
			// tbFile0
			// 
			this.tbFile0.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbFile0.Location = new System.Drawing.Point(3, 4);
			this.tbFile0.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbFile0.Name = "tbFile0";
			this.tbFile0.Size = new System.Drawing.Size(265, 22);
			this.tbFile0.TabIndex = 3;
			this.tbFile0.Text = "Walkthrough (AIex with ProtoArmor corrections) with Equipment info - dragon_quest" +
			"_viii_e.txt";
			// 
			// tbFileOut
			// 
			this.tbFileOut.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbFileOut.Location = new System.Drawing.Point(3, 4);
			this.tbFileOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbFileOut.Name = "tbFileOut";
			this.tbFileOut.Size = new System.Drawing.Size(336, 22);
			this.tbFileOut.TabIndex = 4;
			this.tbFileOut.Text = "Items - Extreme Item Guide - How to Obtain.html";
			// 
			// cbHtml
			// 
			this.cbHtml.Checked = true;
			this.cbHtml.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbHtml.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbHtml.Location = new System.Drawing.Point(3, 41);
			this.cbHtml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.cbHtml.Name = "cbHtml";
			this.cbHtml.Size = new System.Drawing.Size(266, 22);
			this.cbHtml.TabIndex = 5;
			this.cbHtml.Text = "HTML formatted output";
			this.cbHtml.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(3, 78);
			this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(266, 29);
			this.btnSave.TabIndex = 6;
			this.btnSave.Text = "Save Unique Formulas";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// btnSaveIngredientToRecipeGuide
			// 
			this.btnSaveIngredientToRecipeGuide.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSaveIngredientToRecipeGuide.Location = new System.Drawing.Point(3, 115);
			this.btnSaveIngredientToRecipeGuide.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnSaveIngredientToRecipeGuide.Name = "btnSaveIngredientToRecipeGuide";
			this.btnSaveIngredientToRecipeGuide.Size = new System.Drawing.Size(266, 50);
			this.btnSaveIngredientToRecipeGuide.TabIndex = 7;
			this.btnSaveIngredientToRecipeGuide.Text = "Save All Versions of Recipe Listing Every Starting Ingredient";
			this.btnSaveIngredientToRecipeGuide.UseVisualStyleBackColor = true;
			this.btnSaveIngredientToRecipeGuide.Click += new System.EventHandler(this.BtnSaveIngredientToRecipeGuideClick);
			// 
			// btnLoadShopInventory
			// 
			this.btnLoadShopInventory.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadShopInventory.Location = new System.Drawing.Point(3, 78);
			this.btnLoadShopInventory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnLoadShopInventory.Name = "btnLoadShopInventory";
			this.btnLoadShopInventory.Size = new System.Drawing.Size(266, 40);
			this.btnLoadShopInventory.TabIndex = 8;
			this.btnLoadShopInventory.Text = "Load as AIex Shop Inventory: \"Item|Location|x Gold\"";
			this.btnLoadShopInventory.UseVisualStyleBackColor = true;
			this.btnLoadShopInventory.MouseLeave += new System.EventHandler(this.BtnLoadShopInventoryMouseLeave);
			this.btnLoadShopInventory.Click += new System.EventHandler(this.BtnLoadShopInventoryClick);
			this.btnLoadShopInventory.DragLeave += new System.EventHandler(this.BtnLoadShopInventoryDragLeave);
			this.btnLoadShopInventory.DragEnter += new System.Windows.Forms.DragEventHandler(this.BtnLoadShopInventoryDragEnter);
			this.btnLoadShopInventory.MouseEnter += new System.EventHandler(this.BtnLoadShopInventoryMouseEnter);
			// 
			// btnLoadItemList
			// 
			this.btnLoadItemList.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadItemList.Location = new System.Drawing.Point(3, 129);
			this.btnLoadItemList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnLoadItemList.Name = "btnLoadItemList";
			this.btnLoadItemList.Size = new System.Drawing.Size(266, 24);
			this.btnLoadItemList.TabIndex = 12;
			this.btnLoadItemList.Text = "Load as AIex item list: \"|NAME|Find:x|Buy:y|\"";
			this.btnLoadItemList.UseVisualStyleBackColor = true;
			this.btnLoadItemList.MouseLeave += new System.EventHandler(this.BtnLoadItemListMouseLeave);
			this.btnLoadItemList.Click += new System.EventHandler(this.BtnLoadItemListClick);
			this.btnLoadItemList.DragLeave += new System.EventHandler(this.BtnLoadItemListDragLeave);
			this.btnLoadItemList.DragEnter += new System.Windows.Forms.DragEventHandler(this.BtnLoadItemListDragEnter);
			this.btnLoadItemList.MouseEnter += new System.EventHandler(this.BtnLoadItemListMouseEnter);
			// 
			// btnLoadEquipmentList
			// 
			this.btnLoadEquipmentList.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadEquipmentList.Location = new System.Drawing.Point(3, 41);
			this.btnLoadEquipmentList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnLoadEquipmentList.Name = "btnLoadEquipmentList";
			this.btnLoadEquipmentList.Size = new System.Drawing.Size(266, 24);
			this.btnLoadEquipmentList.TabIndex = 13;
			this.btnLoadEquipmentList.Text = "Load as AIex Equipment List";
			this.btnLoadEquipmentList.UseVisualStyleBackColor = true;
			this.btnLoadEquipmentList.MouseLeave += new System.EventHandler(this.BtnLoadEquipmentListMouseLeave);
			this.btnLoadEquipmentList.Click += new System.EventHandler(this.BtnLoadEquipmentListClick);
			this.btnLoadEquipmentList.DragLeave += new System.EventHandler(this.BtnLoadEquipmentListDragLeave);
			this.btnLoadEquipmentList.DragEnter += new System.Windows.Forms.DragEventHandler(this.BtnLoadEquipmentListDragEnter);
			this.btnLoadEquipmentList.MouseEnter += new System.EventHandler(this.BtnLoadEquipmentListMouseEnter);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lvMain, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbFiles, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.85714F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.14286F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 688);
			this.tableLayoutPanel1.TabIndex = 14;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.btnSwitchFormulas, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.tbFile0, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnSortByAttribBoostAsc, 0, 6);
			this.tableLayoutPanel2.Controls.Add(this.btnLoadEquipmentList, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.btnLoadShopInventory, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.btnLoadItemList, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.button1, 0, 7);
			this.tableLayoutPanel2.Controls.Add(this.btnLoadIngredientList, 0, 5);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 6);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 8;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(273, 334);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// btnLoadIngredientList
			// 
			this.btnLoadIngredientList.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadIngredientList.Location = new System.Drawing.Point(3, 194);
			this.btnLoadIngredientList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnLoadIngredientList.Name = "btnLoadIngredientList";
			this.btnLoadIngredientList.Size = new System.Drawing.Size(266, 24);
			this.btnLoadIngredientList.TabIndex = 15;
			this.btnLoadIngredientList.Text = "Load as Ingredients List: line=category if no \"(\"";
			this.btnLoadIngredientList.UseVisualStyleBackColor = true;
			this.btnLoadIngredientList.Click += new System.EventHandler(this.BtnLoadIngredientListClick);
			// 
			// btnSortByAttribBoostAsc
			// 
			this.btnSortByAttribBoostAsc.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSortByAttribBoostAsc.Location = new System.Drawing.Point(3, 226);
			this.btnSortByAttribBoostAsc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnSortByAttribBoostAsc.Name = "btnSortByAttribBoostAsc";
			this.btnSortByAttribBoostAsc.Size = new System.Drawing.Size(266, 24);
			this.btnSortByAttribBoostAsc.TabIndex = 14;
			this.btnSortByAttribBoostAsc.Text = "Sort by Attribute Value after \"^\"";
			this.btnSortByAttribBoostAsc.UseVisualStyleBackColor = true;
			this.btnSortByAttribBoostAsc.Click += new System.EventHandler(this.BtnSortByAttribBoostAscClick);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.tbFileOut, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.btnSaveIngredientToRecipeGuide, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.cbHtml, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.btnSave, 0, 2);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(287, 436);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 4;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(385, 169);
			this.tableLayoutPanel3.TabIndex = 11;
			// 
			// lbFiles
			// 
			this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbFiles.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbFiles.FormattingEnabled = true;
			this.lbFiles.ItemHeight = 16;
			this.lbFiles.Location = new System.Drawing.Point(5, 436);
			this.lbFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.lbFiles.Name = "lbFiles";
			this.lbFiles.Size = new System.Drawing.Size(274, 244);
			this.lbFiles.TabIndex = 12;
			this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.LbFilesSelectedIndexChanged);
			// 
			// tbStatus
			// 
			this.tbStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tbStatus.Location = new System.Drawing.Point(0, 732);
			this.tbStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbStatus.Name = "tbStatus";
			this.tbStatus.ReadOnly = true;
			this.tbStatus.Size = new System.Drawing.Size(839, 22);
			this.tbStatus.TabIndex = 15;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lblHelp);
			this.splitContainer1.Size = new System.Drawing.Size(839, 732);
			this.splitContainer1.SplitterDistance = 688;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 16;
			this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer1SplitterMoved);
			// 
			// lblHelp
			// 
			this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblHelp.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHelp.Location = new System.Drawing.Point(0, 0);
			this.lblHelp.Name = "lblHelp";
			this.lblHelp.Size = new System.Drawing.Size(839, 39);
			this.lblHelp.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(839, 754);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.tbStatus);
			this.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainForm";
			this.Text = "IngredientToRecipe";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button btnLoadIngredientList;
		private System.Windows.Forms.Button btnSortByAttribBoostAsc;
		private System.Windows.Forms.Label lblHelp;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox lbFiles;
		private System.Windows.Forms.TextBox tbStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnLoadEquipmentList;
		private System.Windows.Forms.Button btnLoadItemList;
		private System.Windows.Forms.Button btnLoadShopInventory;
		private System.Windows.Forms.Button btnSaveIngredientToRecipeGuide;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.CheckBox cbHtml;
		private System.Windows.Forms.TextBox tbFileOut;
		private System.Windows.Forms.TextBox tbFile0;
		private System.Windows.Forms.Button btnSwitchFormulas;
		private System.Windows.Forms.ListView lvMain;
		private System.Windows.Forms.Button button1;
	}
}
