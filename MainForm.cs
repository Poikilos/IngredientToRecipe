/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 2/8/2010
 * Time: 5:03 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ExpertMultimedia
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form {
		//public static string[] sarrFiles=null;
		public static string sHelpPrev="";
		public static string sHelpAIexShopInventory="AIex Shop inventory"
			+"\n-Starts at: line containing \"Shop Inventory\""
			+"\n-Formatted as:"
			+"\n| WEAPON SHOP                  | PRICE            |             PRICE |"
			+"\n| Copper Sword                  | Farebury            |             270 Gold |"
			+"\n-Price converts to \"Farebury 270g\" or \"Pickham Casino 1000 tokens\""
			+"\n-Section is detected when first 2 columns are all caps"
			+"\n-Section is converted to mutable \"~Other Weapon\""
			+"\n-Ends at: 2 blank lines";
		public static string sHelpAIexItemList="AIex Item List"
			+"\n-Starts at: line containing \"Item List\""
			+"\n-Formatted as:"
			+"\n| NAME                | Find: x               | Buy: y                       |"
			+"\n|---------------------+-----------------------+------------------------------|"
			+"\n|                     | Restores 60 or more HP to a single ally.             |"
			+"\n-Item line detected when column count is 3 (see formatting above)"
			+"\n-Only value after Find is saved (as \"Find:x\"), and only when not \"not found\""
			+"\n-Ends at: 2 blank lines";
		public static string sHelpAIexEquipmentList="AIex Equipment List"
			+"\n-Starts at line containing \"Equipment List\" not \"Equipment List...\""
			+"\n-Formatted as:"
			+"\n+== SWORDS ========================================================= 0A.14 ==+"
			+"\n"
			+"\n| ZOMBIESBANE             |   Equip On:   | Hero | -      | Jessica | -      |"
			+"\n|-------------------------+---------------+------+--------+---------+--------+"
			+"\n| Attack............. +54 | A holy sword created for slaying zombies and     |"
			+"\n|_________________________| other undead monsters.                           |"
			+"\n-Equippability detected when Column count is 6 and uppercase is different then raw (when not |~~|~~|~~|~~|~~|~~|)"
			+"\n-Type detected when line starts and ends with '+'"
			+"\n-Ends at: 4 blank lines";
		public static StreamWriter streamOut=null;
		public MainForm() {
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			lvMain.Columns.Add("Name");
			lvMain.Columns[0].Width=200;
			lvMain.Columns.Add("Attributes");
			lvMain.Columns[1].Width=200;
			lvMain.Columns.Add("Ingredients");
			lvMain.Columns[2].Width=400;
			lvMain.Columns.Add("Category");
			lvMain.Columns[3].Width=200;
		}
				/// <summary>
		/// 
		/// </summary>
		/// <param name="sEquippability">in "E:H,Y,J,A" format</param>
		/// <param name="sAppendCategory"></param>
		/// <returns></returns>
		string CategoryGeneratedByEquippability(string sEquippability, string sAppendCategory) {
			string sReturn="";
			if (sAppendCategory!=null&&sAppendCategory!="") sAppendCategory=RString.Capitalized(sAppendCategory.ToLower());
			string[] sarrParts=RString.Explode(sEquippability,':',true);
			if (sarrParts.Length>1) {
				string[] sarrPerson=RString.Explode(sarrParts[1],',',true);
				if (sarrPerson.Length==4) {
					sReturn="Versatile";
				}
				else if (sAppendCategory=="Swords"
				         &&sEquippability.ToUpper()=="E:H,J,A") {
					sReturn="Crossover Light";
				}
				else if (sarrPerson.Length>1) {
					if (sAppendCategory=="Swords" && sEquippability.ToUpper()=="E:H,J") sReturn="";//just a sword
					else {
						sReturn="Crossover";
						for (int iPerson=0; iPerson<sarrPerson.Length; iPerson++) {
							if (sarrPerson[iPerson]=="H") sReturn+=" Heroic";
							else if (sarrPerson[iPerson]=="Y") sReturn+=" Heavy";
							else if (sarrPerson[iPerson]=="J") sReturn+=" Vanity";
							else if (sarrPerson[iPerson]=="J") sReturn+=" Light";
						}
					}
				}
				else if (sarrPerson.Length==1) {
					if (sarrPerson[0].ToUpper()=="H") sReturn="Heroic";
					else if (sarrPerson[0].ToUpper()=="Y") sReturn="Heavy";
					else if (sarrPerson[0].ToUpper()=="J") sReturn="Vanity";
					else if (sarrPerson[0].ToUpper()=="A") sReturn="Light";
				}
			}
			if (sReturn=="") sReturn=sAppendCategory;
			else sReturn=sReturn+" "+sAppendCategory;
			return sReturn;
		}//end CategoryGeneratedByEquippability
		
		public static string CategoryInferredByFuzzyNameAnalysisElseDefault(string sName, string sCategoryDefault) {
			string sReturn=sCategoryDefault;
			string sName_ToLower=sName.ToLower();
			if (sName_ToLower.Contains("armour")) {
				sReturn="~Other Armour";
			}
			else if (sName_ToLower.Contains("sword")) {
				sReturn="~Other Swords";
			}
			else if (sName_ToLower.Contains("sil leaf")) {
				sReturn="Consumables";
			}
			else if (sName_ToLower.Contains("axe")) {
				sReturn="Axes";
			}
			else if (sName_ToLower.Contains("seed")) {
				sReturn="Consumables";
			}
			else if (sName_ToLower.Contains("medicine")) {
				sReturn="Consumables";
			}
			else if (sName_ToLower.Contains("antidot")) {
				sReturn="Consumables";
			}
			else if (sName_ToLower.Contains("mould")) {
				sReturn="Ingredients";
			}
			else if (sName_ToLower.Contains(" key")) {
				sReturn="Special Items";
			}
			else if (sName_ToLower.EndsWith(" hide")) {
				sReturn="Ingredients";
			}
			return sReturn;
		}//end CategoryInferredByFuzzyNameAnalysisElseDefault
		
		void ShowItems() {
			lvMain.Items.Clear();
			for (int index=0; index<RFormula.iIngredientInfosUsed; index++) {
				lvMain.Items.Add(new ListViewItem(new string[]{
				                                  	RFormula.IngredientInfos[index].sName,
				                                  	RString.ToString(RFormula.IngredientInfos[index].alAttrib,"; ",null),
				                                  	RString.ToString(RFormula.IngredientInfos[index].sarrIngredient," + ",null),
				                                  	RFormula.IngredientInfos[index].sCategory
				                                  }));
			}
		}
		
		void MainFormLoad(object sender, EventArgs e) {
			RFormula.AddCategoryIfUnique("Spears");
			RFormula.AddCategoryIfUnique("Boomerangs");
			RFormula.AddCategoryIfUnique("Heroic Swords");
			RFormula.AddCategoryIfUnique("Swords");
			RFormula.AddCategoryIfUnique("Hammers",true);
			RFormula.AddCategoryIfUnique("Axes");
			RFormula.AddCategoryIfUnique("Scythes");
			RFormula.AddCategoryIfUnique("Knives");
			RFormula.AddCategoryIfUnique("Whips");
			RFormula.AddCategoryIfUnique("Staves");
			RFormula.AddCategoryIfUnique("Bows",true);
			RFormula.AddCategoryIfUnique("Crossover Light Swords");
			RFormula.AddCategoryIfUnique("Light Swords");
			RFormula.AddCategoryIfUnique("Shields",true);
			RFormula.AddCategoryIfUnique("Versatile Armour",true);
			RFormula.AddCategoryIfUnique("Heroic Armour");
			RFormula.AddCategoryIfUnique("Crossover Light Heroic Armour");
			RFormula.AddCategoryIfUnique("Crossover Heavy Heroic Armour");
			RFormula.AddCategoryIfUnique("Heavy Armour");
			RFormula.AddCategoryIfUnique("Vanity Armour");
			RFormula.AddCategoryIfUnique("Crossover Vanity Light Armour");
			RFormula.AddCategoryIfUnique("Light Armour");
			RFormula.AddCategoryIfUnique("Versatile Headgear",true);
			RFormula.AddCategoryIfUnique("Heroic Headgear");
			RFormula.AddCategoryIfUnique("Crossover Heroic Heavy Headgear");
			RFormula.AddCategoryIfUnique("Heavy Headgear");
			RFormula.AddCategoryIfUnique("Crossover Heroic Light Headgear");
			RFormula.AddCategoryIfUnique("Crossover Heroic Vanity Headgear");
			RFormula.AddCategoryIfUnique("Vanity Headgear");
			RFormula.AddCategoryIfUnique("Crossover Vanity Light Headgear");
			RFormula.AddCategoryIfUnique("Light Headgear");
			RFormula.AddCategoryIfUnique("Accessories",true);
			RFormula.AddCategoryIfUnique("Special Items",true);
			RFormula.AddCategoryIfUnique("Consumables",true);
			RFormula.AddCategoryIfUnique("Cheeses");

			DirectoryInfo diNow=new DirectoryInfo(Directory.GetCurrentDirectory());
			//ArrayList alFiles=new ArrayList();
			foreach (FileInfo fiNow in diNow.GetFiles()) {
				if (fiNow.Name.ToLower().EndsWith(".txt")) {
					if (fiNow.Name!="err.txt"&&fiNow.Name!="out.txt") {
						//alFiles.Add(fiNow.Name);
						lbFiles.Items.Add(fiNow.Name);
					}
				}
			}
			//if (alFiles.Count>0) sarrFiles=new string[alFiles.Count];
			//for (int i=0; i<sarrFiles.Length; i++) {
			//	sarrFiles[i]=alFiles[i].ToString();
			//	lbFiles.Items.Add(sarrFiles[i]);
			//}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			IngredientToRecipes.LoadXEqualsFormulas(new string[]{this.tbFile0.Text},false,null,false);
			ShowItems();
		}
		
		void BtnSwitchFormulasClick(object sender, EventArgs e) {
			IngredientToRecipes.LoadXEqualsFormulas(new string[]{this.tbFile0.Text},true,null,false);
			ShowItems();
		}
		
		void BtnSaveClick(object sender, EventArgs e) {
			RFormula.SortKnownsByAscendingAttributeIncrease();
			bool bAddHtml=this.cbHtml.Checked;
			streamOut=new StreamWriter(this.tbFileOut.Text);
			this.tbStatus.Text="Writing "+this.tbFileOut.Text+"...";
			Application.DoEvents();
			this.Refresh();
			string sCategoryWriting="";
			//HTML to use:
			//before section:<br/><br/><p>
			//before major section:<br/><br/><br/><p>
			//before item:<p>
			
			if (bAddHtml) {
				streamOut.WriteLine("<html><head><title>Extreme Item Guide - How to Obtain</title>");
			   streamOut.WriteLine("<style type=\"text/css\">");
			   streamOut.WriteLine("p {margin-left:.2in; text-indent:-.2in; margin-top:0in; margin-bottom:0in}");
				streamOut.WriteLine("</style>");
				streamOut.WriteLine("</head><body style=\"font-family:arial,helvetica\">");
			}
			bool bAnyBlank=false;
			for (int index=0; index<RFormula.iIngredientInfosUsed; index++) {
				if (RFormula.IngredientInfos[index].sCategory==null)
					RFormula.IngredientInfos[index].sCategory="";
				if (RFormula.IngredientInfos[index].sCategory=="")
					bAnyBlank=true;
			}
			if (bAnyBlank) RFormula.AddCategoryIfUnique("");
			foreach (string sCategoryNow in RFormula.alCategories) {
				for (int i=0; i<RFormula.iIngredientInfosUsed; i++) {
					if (RFormula.IngredientInfos[i]!=null) {
						if (RFormula.IngredientInfos[i].sCategory==sCategoryNow) {
							bool bFoundSimilar=false;
							bool bRecipe=true;
							if (RFormula.IngredientInfos[i].sarrIngredient==null
							    ||RFormula.IngredientInfos[i].sarrIngredient.Length==1) {
								bRecipe=false;
								for (int iFind=0; iFind<i; iFind++) {
									if (RFormula.IngredientInfos[i].sName.ToLower()==RFormula.IngredientInfos[iFind].sName.ToLower()) {
										bFoundSimilar=true;
										break;
									}
								}
							}
							else {
								for (int iFind=0; iFind<i; iFind++) {
									if (RFormula.HasSameIngredients(RFormula.IngredientInfos[i],RFormula.IngredientInfos[iFind])) {
										bFoundSimilar=true;
										break;
									}
								}
							}
							if (!bFoundSimilar) {
								if (RFormula.IngredientInfos[i].sCategory!=sCategoryWriting) {
									if (RFormula.IsImportantCategory(RFormula.IngredientInfos[i].sCategory)) streamOut.WriteLine(bAddHtml?"<br/>":"");
									streamOut.WriteLine(bAddHtml?"<br/><p>":"");
									streamOut.WriteLine(RFormula.IngredientInfos[i].sCategory+(bAddHtml?"</p>":""));
									sCategoryWriting=RFormula.IngredientInfos[i].sCategory;
								}
								string sFormula=RFormula.IngredientInfos[i].ToString(false,bAddHtml);
								streamOut.WriteLine((bAddHtml?"<p>":"")+sFormula+(bAddHtml?"</p>":""));
							}
						}//end if in current category
					}//end if non-null item
				}//end for recipe i
			}
			if (bAddHtml) streamOut.WriteLine("</body></html>");
			streamOut.Close();
			this.tbStatus.Text="Writing "+this.tbFileOut.Text+"...OK";
		}//end BtnSaveClick
		
		void BtnSaveIngredientToRecipeGuideClick(object sender, EventArgs e) {
			bool bAddHtml=this.cbHtml.Checked;
			bool bWriteCategories=false;
			streamOut=new StreamWriter(this.tbFileOut.Text);
			this.tbStatus.Text="Writing "+this.tbFileOut.Text+"...";
			Application.DoEvents();
			this.Refresh();

			string sCategoryWriting="";
			if (bAddHtml) {
				streamOut.WriteLine("<html><head><title>Extreme Item Guide - How to Obtain</title>");
			   streamOut.WriteLine("<style type=\"text/css\">");
			   streamOut.WriteLine("p {margin-left:.2in; text-indent:-.2in; margin-top:0in; margin-bottom:0in}");
				streamOut.WriteLine("</style>");
				streamOut.WriteLine("</head><body style=\"font-family:arial,helvetica\">");
			}
			bool bAnyBlank=false;
			for (int index=0; index<RFormula.iIngredientInfosUsed; index++) {
				if (RFormula.IngredientInfos[index].sCategory==null)
					RFormula.IngredientInfos[index].sCategory="";
				if (RFormula.IngredientInfos[index].sCategory=="")
					bAnyBlank=true;
			}
			if (bAnyBlank) RFormula.AddCategoryIfUnique("");
			foreach (string sCategoryNow in RFormula.alCategories) {
				for (int i=0; i<RFormula.iIngredientInfosUsed; i++) {
					if (RFormula.IngredientInfos[i]!=null) {
						if (RFormula.IngredientInfos[i].sCategory.ToLower()==sCategoryNow.ToLower()) {
							bool bFoundSimilar=false;
							bool bRecipe=true;
							if (RFormula.IngredientInfos[i].sarrIngredient==null
							    ||RFormula.IngredientInfos[i].sarrIngredient.Length==1) {
								bRecipe=false;
								for (int iFind=0; iFind<i; iFind++) {
									if (RFormula.IngredientInfos[i].sName.ToLower()==RFormula.IngredientInfos[iFind].sName.ToLower()) {
										bFoundSimilar=true;
										break;
									}
								}
							}
							else {
								for (int iFind=0; iFind<i; iFind++) {
									if (RFormula.HasSameIngredients(RFormula.IngredientInfos[i],RFormula.IngredientInfos[iFind])) {
										bFoundSimilar=true;
										break;
									}
								}
							}
							if (!bFoundSimilar) {
								if (RFormula.IngredientInfos[i].sCategory!=sCategoryWriting) {
									if (bWriteCategories&&RFormula.IsImportantCategory(RFormula.IngredientInfos[i].sCategory)) streamOut.WriteLine(bAddHtml?"<br/>":"");
									if (bWriteCategories) streamOut.WriteLine(bAddHtml?"<br/><p>":"");
									if (bWriteCategories) streamOut.WriteLine(RFormula.IngredientInfos[i].sCategory+(bAddHtml?"</p>":""));
									sCategoryWriting=RFormula.IngredientInfos[i].sCategory;
								}
								string[] sarrFormulas=RFormula.IngredientInfos[i].ToIngredientToRecipeFormulas(true,bAddHtml);
								if (sarrFormulas!=null) {
									//string sFormula=RFormula.IngredientInfos[i].ToString(true,bAddHtml);
									//streamOut.WriteLine(sFormula+(bAddHtml?"<br/>":""));
									for (int iFormulaOrderNow=0; iFormulaOrderNow<sarrFormulas.Length; iFormulaOrderNow++) {
										streamOut.WriteLine((bAddHtml?"<p>":"")+sarrFormulas[iFormulaOrderNow]+(bAddHtml?"</p>":""));
									}
								}
								else Console.Error.WriteLine("No ingredients found for "+RFormula.IngredientInfos[i].sName);
							}
						}//end if matches current category
					}//end if not null
				}//end for ingredient
			}//end for category
			if (bAddHtml) streamOut.WriteLine("</body></html>");
			streamOut.Close();
			this.tbStatus.Text="Writing "+this.tbFileOut.Text+"...OK";
		}//end BtnSaveIngredientToRecipeGuideClick
		void BtnLoadShopInventoryClick(object sender, EventArgs e) {
			bool bShopInventorySection=false;
			int iBlanksInARow=0;
			StreamReader streamIn=null;
			streamIn=new StreamReader(this.tbFile0.Text);
			string sLine;
			string sLastShopType="";
			while ( (sLine=streamIn.ReadLine()) != null ) {
				RString.RemoveEndsWhiteSpace(ref sLine);
				if (sLine!="") {
					iBlanksInARow=0;
					if (bShopInventorySection) {
						if (sLine.StartsWith("|")&&sLine.EndsWith("|")&&sLine.Length>1) {
							sLine=RString.SafeSubstring(sLine,1,sLine.Length-2);
							string[] sarrParts=RString.Explode(sLine,'|',true);
							//TEST whether a line like "| Copper Sword                  | Farebury            |             270 Gold |"
							string sarrParts_0_ToLower=sarrParts[0].ToLower();
							if (sarrParts[0].ToUpper()==sarrParts[0]) {
								//if all caps or _|_|_
								if (sarrParts_0_ToLower.StartsWith("weapon")) {
									sLastShopType="~Other Weapons";
								}
								else if (sarrParts_0_ToLower.StartsWith("armour")||sarrParts_0_ToLower.StartsWith("armor")) {
									sLastShopType="~Other Armour";
								}
								else if (sarrParts_0_ToLower.StartsWith("item peddler")) {
									sLastShopType="~Other Items";
								}
								else if (sarrParts_0_ToLower.StartsWith("item")) {
									sLastShopType="~Other Items";
								}
								else if (sarrParts_0_ToLower.StartsWith("equipment")) {
									sLastShopType="~Other Equipment";
								}
								else if (sarrParts_0_ToLower.StartsWith("accessory")) {
									sLastShopType="~Other Accessories";
								}
								else if (sarrParts_0_ToLower.StartsWith("some guy")) {
									sLastShopType="~Other Items";
								}
							}//end if shop
							else if (sarrParts.Length==3&&sarrParts[2].ToLower().Contains("gold")) {
								string sCategoryInferred=sLastShopType;
								if (sarrParts_0_ToLower=="bandit's mail") {
									sarrParts_0_ToLower="bandit mail";
									sarrParts[0]=sarrParts[0].Replace("'","");
								}
								sCategoryInferred=MainForm.CategoryInferredByFuzzyNameAnalysisElseDefault(sarrParts_0_ToLower,sLastShopType);
								ArrayList alAttribNew=new ArrayList();
								alAttribNew.Add(sarrParts[1]+" "+sarrParts[2].Replace(" Gold","g"));
								RFormula.AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(sarrParts_0_ToLower,alAttribNew,null,sCategoryInferred);
							}//end if item
						}
						//else not an item line
					}//end if bShopInventorySection
					else if (sLine.ToLower().Contains("shop inventory")) bShopInventorySection=true;
				}//end if not blank
				else {
					iBlanksInARow++;
					if (iBlanksInARow>=2) bShopInventorySection=false;
				}
			}
			streamIn.Close();
			ShowItems();
		}//end BtnLoadShopInventoryClick
		
		void BtnLoadItemListClick(object sender, EventArgs e) {
			bool bItemListSection=false;
			int iBlanksInARow=0;
			StreamReader streamIn=null;
			streamIn=new StreamReader(this.tbFile0.Text);
			string sLine;
			while ( (sLine=streamIn.ReadLine()) != null ) {
				RString.RemoveEndsWhiteSpace(ref sLine);
				if (sLine!="") {
					iBlanksInARow=0;
					if (bItemListSection) {
						if (sLine.StartsWith("|")&&sLine.EndsWith("|")&&sLine.Length>1) {
							sLine=RString.SafeSubstring(sLine,1,sLine.Length-2);
							string[] sarrParts=RString.Explode(sLine,'|',true);
							//TEST whether a line like "| Copper Sword                  | Farebury            |             270 Gold |"
							string sarrParts_0_ToLower=sarrParts[0].ToLower();
							if (sarrParts.Length==3) {
								ArrayList alAttribNew=new ArrayList();
								string[] sarrFindable=RString.Explode(sarrParts[1],':',true);
								if (sarrFindable.Length==2
								    && (sarrFindable[1].ToLower()!="not found") ) {
									alAttribNew.Add(sarrFindable[0]+":"+sarrFindable[1]);
									RFormula.AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(sarrParts_0_ToLower,alAttribNew,null,CategoryInferredByFuzzyNameAnalysisElseDefault(sarrParts_0_ToLower,"~Other Items"));
								}
							}//end if item
						}//end if enclosed in '|' on both ends
						//else not an item line
					}//end if bItemListSection
					else if (sLine.ToLower().Contains("item list")) bItemListSection=true;
				}//end if not blank
				else {
					iBlanksInARow++;
					if (iBlanksInARow>=2) bItemListSection=false;
				}
			}
			streamIn.Close();
			ShowItems();
		}//end BtnLoadItemListClick

		void BtnLoadEquipmentListClick(object sender, EventArgs e) {
			bool bEquipmentListSection=false;
			int iBlanksInARow=0;
			StreamReader streamIn=null;
			streamIn=new StreamReader(this.tbFile0.Text);
			string sLine;
			string sCategoryNow="";
			string sLastEquip="";
			string sNamePrev="";
			while ( (sLine=streamIn.ReadLine()) != null ) {
				RString.RemoveEndsWhiteSpace(ref sLine);
				if (sLine!="") {
					iBlanksInARow=0;
					if (bEquipmentListSection) {
						if (sLine.StartsWith("+")&&sLine.EndsWith("+")&&sLine.Length>1) {
							sLine=RString.SafeSubstring(sLine,1,sLine.Length-2);
							int iStart=-1;
							int iEndBefore=-1;
							for (int iChar=0; iChar<sLine.Length; iChar++) {
								if (iStart<0) {
									if (sLine[iChar]!='='&&!RString.IsWhiteSpace(sLine[iChar])) {
										iStart=iChar;
									}
								}
								else {
									if (sLine[iChar]=='='||RString.IsWhiteSpace(sLine[iChar])) {
										iEndBefore=iChar;
										break;
									}
								}
							}
							if (iStart>=0&&iEndBefore>iStart) {
								sCategoryNow=RString.Capitalized(sLine.Substring(iStart,iEndBefore-iStart).ToLower());
							}
						}//end if section header ('+' on both ends)
						else if (sLine.StartsWith("|")&&sLine.EndsWith("|")&&sLine.Length>1) {
							sLine=RString.SafeSubstring(sLine,1,sLine.Length-2);
							string[] sarrParts=RString.Explode(sLine,'|',true);
							//TEST whether a line like "| Copper Sword                  | Farebury            |             270 Gold |"
							string sarrParts_0_ToLower=sarrParts[0].ToLower();
							if (sarrParts.Length==6) {
								if (sarrParts[1].ToUpper()=="EQUIP ON:") {
									//Console.Error.WriteLine("Found: \"EQUIP ON:\"");
									sNamePrev=sarrParts[0].ToLower();
									sLastEquip="E:";
									bool bFirst=true;
									for (int iPerson=2; iPerson<=5; iPerson++) {
										if (sarrParts[iPerson]!=""&&sarrParts[iPerson]!="-") {
											sLastEquip+=(bFirst?"":",")+sarrParts[iPerson].Substring(0,1).ToUpper();//saves first letter
											bFirst=false;
										}
									}
									//don't save yet--wait for "x... +y |" line
								}//end if equippability
							}
							else if (sarrParts.Length==2&&sarrParts[0].Contains("+")) {
								//Console.Error.WriteLine("Found: \"EQUIP ON:\"");
								//Knowns:
								//-sCategoryNow
								//-sLastEquip
								//To get:
								//-increase (can be multiple i.e. "Attack. +20 Agility +20" including stray elipsis dots
								ArrayList alAttribNew=new ArrayList();
								while (sarrParts[0].Contains("..")) sarrParts[0]=sarrParts[0].Replace("..",".");
								sarrParts[0].Replace("."," ");
								sarrParts[0].Replace("+"," +");//allow space to be used as delimiter cleanly
								while (sarrParts[0].Contains("  ")) sarrParts[0]=sarrParts[0].Replace("  "," ");
								RString.RemoveEndsWhiteSpace(ref sarrParts[0]);
								string[] sarrIncreases=RString.Explode(sarrParts[0],' ',true);
								string sLastStat="";
								if (sarrIncreases!=null&&sarrIncreases.Length>=2) {
									for (int iNow=0; iNow<sarrIncreases.Length; iNow++) {
										if (sarrIncreases[iNow].StartsWith("+")
										    &&sLastStat!="") {
											alAttribNew.Add(RFormula.ShortStat(sLastStat)+"^"+RString.SafeSubstring(sarrIncreases[iNow],1));
											sLastStat="";
										}
										else sLastStat=sarrIncreases[iNow];
									}
								}
								string sCategoryInferred=RString.Capitalized(sCategoryNow.ToLower());
								if (sCategoryInferred=="Armour"||sCategoryInferred=="Armor") {
									if (sLastEquip=="E:H,Y,J,A") sCategoryInferred="Versatile Armour";
									else if (sLastEquip=="E:H") sCategoryInferred="Heroic Armour";
									else if (sLastEquip=="E:H,A") sCategoryInferred="Crossover Light Heroic Armour";
									else if (sLastEquip=="E:H,Y") sCategoryInferred="Crossover Heavy Heroic Armour";
									else if (sLastEquip=="E:Y") sCategoryInferred="Heavy Armour";
									else if (sLastEquip=="E:J") sCategoryInferred="Vanity Armour";
									else if (sLastEquip=="E:J,A") sCategoryInferred="Crossover Vanity Light Armour";
									else if (sLastEquip=="E:A") sCategoryInferred="Light Armour";
									else sCategoryInferred=CategoryGeneratedByEquippability(sLastEquip,"Armour");
								}
								else if (sCategoryInferred=="Helmets"||sCategoryInferred=="Helms"||sCategoryInferred=="Headgear") {
									if (sLastEquip=="E:H,Y,J,A") sCategoryInferred="Versatile Headgear";
									else if (sLastEquip=="E:H") sCategoryInferred="Heroic Headgear";
									else if (sLastEquip=="E:H,Y") sCategoryInferred="Crossover Heroic Heavy Headgear";
									else if (sLastEquip=="E:Y") sCategoryInferred="Heavy Headgear";
									else if (sLastEquip=="E:H,A") sCategoryInferred="Crossover Heroic Light Headgear";
									else if (sLastEquip=="E:H,J") sCategoryInferred="Crossover Heroic Vanity Headgear";
									else if (sLastEquip=="E:J") sCategoryInferred="Vanity Headgear";
									else if (sLastEquip=="E:J,A") sCategoryInferred="Crossover Vanity Light Headgear";
									else if (sLastEquip=="E:A") sCategoryInferred="Light Headgear";
									else sCategoryInferred=CategoryGeneratedByEquippability(sLastEquip,"Headgear");
								}//end if Helmets, Helms, or Headgear
								else if (sCategoryInferred=="Swords") {
									if (sLastEquip=="E:H,J,A") sCategoryInferred="Crossover Light Swords";
									else if (sLastEquip=="E:H") sCategoryInferred="Heroic Swords";
									else if (sLastEquip=="E:A") sCategoryInferred="Light Swords";
									else sCategoryInferred=CategoryGeneratedByEquippability(sLastEquip,"Swords");
								}//end if "Swords"
								//else if (sCategoryInferred=="Accessories") {
								//	sCategoryInferred="~Other Accessories";
								//}
								alAttribNew.Add(sLastEquip);
								
								RFormula.AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(sNamePrev,alAttribNew,null,sCategoryInferred);
							}//end if item "+" attribute line (final necessary line)
						}
						//else not an item line
					}//end if bEquipmentListSection
					else if (sLine.ToLower().Contains("equipment list")&&!sLine.ToLower().Contains("equipment list...")) bEquipmentListSection=true;
				}//end if not blank
				else {
					iBlanksInARow++;
					if (iBlanksInARow>=4) bEquipmentListSection=false;
				}
			}
			streamIn.Close();
			ShowItems();
		}//end BtnLoadEquipmentListClick
		
		void BtnLoadShopInventoryDragEnter(object sender, DragEventArgs e) {
		}
		
		void BtnLoadShopInventoryDragLeave(object sender, EventArgs e) {
		}
		
		void BtnLoadItemListDragEnter(object sender, DragEventArgs e) {
		}
		
		void BtnLoadItemListDragLeave(object sender, EventArgs e) {
		}
		
		void BtnLoadEquipmentListDragEnter(object sender, DragEventArgs e) {
		}
		
		void BtnLoadEquipmentListDragLeave(object sender, EventArgs e) {
		}
		
		void BtnLoadShopInventoryMouseEnter(object sender, EventArgs e) {
			MainForm.sHelpPrev=this.lblHelp.Text;
			this.lblHelp.Text=MainForm.sHelpAIexShopInventory;
		}
		
		void BtnLoadShopInventoryMouseLeave(object sender, EventArgs e) {
			this.lblHelp.Text=MainForm.sHelpPrev;
		}
		
		void BtnLoadItemListMouseEnter(object sender, EventArgs e) {
			MainForm.sHelpPrev=this.lblHelp.Text;
			this.lblHelp.Text=MainForm.sHelpAIexItemList;
		}
		
		void BtnLoadItemListMouseLeave(object sender, EventArgs e) {
			this.lblHelp.Text=MainForm.sHelpPrev;
		}
		
		void BtnLoadEquipmentListMouseEnter(object sender, EventArgs e) {
			MainForm.sHelpPrev=this.lblHelp.Text;
			this.lblHelp.Text=MainForm.sHelpAIexEquipmentList;
		}
		
		void BtnLoadEquipmentListMouseLeave(object sender, EventArgs e) {
			this.lblHelp.Text=MainForm.sHelpPrev;
		}
		
		void LbFilesSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lbFiles.Items!=null) {
				if (this.lbFiles.SelectedIndex>=0&&this.lbFiles.SelectedIndex<this.lbFiles.Items.Count) {
					this.tbFile0.Text=this.lbFiles.Items[this.lbFiles.SelectedIndex].ToString();
				}
			}
		}
		
		void SplitContainer1SplitterMoved(object sender, SplitterEventArgs e)
		{
			
		}
		
		void BtnSortByAttribBoostAscClick(object sender, EventArgs e)
		{
			this.tbStatus.Text="Sorting...";
			Application.DoEvents();
			this.Refresh();
			RFormula.SortKnownsByAscendingAttributeIncrease();
			this.tbStatus.Text="Sorting...Displaying...";
			Application.DoEvents();
			this.Refresh();
			ShowItems();
			this.tbStatus.Text="Sorting...OK";
		}
		
		void BtnLoadIngredientListClick(object sender, EventArgs e)
		{
			StreamReader streamIn=null;
			streamIn=new StreamReader(this.tbFile0.Text);
			string sLine;
			string sCategoryNow="";
			while ( (sLine=streamIn.ReadLine()) != null ) {
				RString.RemoveEndsWhiteSpace(ref sLine);
				if (sLine!="") {
					if (sLine.Contains("(")) {
						RFormula.AddIngredient(sLine,sCategoryNow);
					}
					else sCategoryNow=sLine;
				}
			}
			streamIn.Close();
			ShowItems();
		}
	}//end MainForm
}//end namespace
