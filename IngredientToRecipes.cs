/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 2/8/2010
 * Time: 4:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.IO;

namespace ExpertMultimedia {
	/// <summary>
	/// Description of IngredientToRecipe.
	/// </summary>
	public class IngredientToRecipes {
		public static ArrayList alDone=new ArrayList();
		public IngredientToRecipes()
		{
		}
		public static void LoadXEqualsFormulas(string[] args, bool bSumIsLast, StreamWriter WriteIngredientToFormulaInfo_ElseNull, bool bAddHtmlToStream) {
			for (int iArg=0; iArg<args.Length; iArg++) {
				Console.Error.WriteLine("Loading "+args[iArg]);
				StreamReader streamIn=new StreamReader(args[iArg]);
				string sLine;
				int iLines=0;
				while ( (sLine=streamIn.ReadLine()) != null ) {
					iLines++;
				}
				streamIn.Close();
				int iFormulas=0;
				int iIngredients=0;
				string sCategoryPrev="";
				if (iLines>0) {
					Console.Error.WriteLine("Loading "+iLines+" lines");
					RFormula[] formulas=new RFormula[iLines*2];
					streamIn=new StreamReader(args[0]);
					while ( (sLine=streamIn.ReadLine()) != null ) {
						RString.RemoveEndsWhiteSpace(ref sLine);
						if (sLine.Contains("=")) {
							if (RReporting.bUltraDebug) Console.Error.Write(" ["+iFormulas+"]");
							formulas[iFormulas]=new RFormula();
							if (RReporting.bUltraDebug) Console.Error.Write(".");
							formulas[iFormulas].FromFormula(sLine,bSumIsLast,sCategoryPrev);
							if (RReporting.bUltraDebug) Console.Error.WriteLine("Ingredients:"+formulas[iFormulas].sarrIngredient.Length);
							iIngredients+=formulas[iFormulas].sarrIngredient.Length;
							iFormulas++;
						}
						else if (sLine!="") {
							sCategoryPrev=sLine;
						}
					}
					Console.Error.WriteLine("Processing "+iFormulas+" formulas {iIngredients:"+iIngredients+"}");
					string sCategoryWriting="";
					for (int iFormula=0; iFormula<iFormulas; iFormula++) {
						for (int iIngredient=0; iIngredient<formulas[iFormula].sarrIngredient.Length; iIngredient++) {
							if (WriteIngredientToFormulaInfo_ElseNull!=null) {
								string sFormula=RFormula.Reordered(formulas[iFormula].sarrIngredient,iIngredient," + ",bAddHtmlToStream)+" = "+formulas[iFormula].sName;
								if (formulas[iFormula].sCategory!=sCategoryWriting) {
									WriteIngredientToFormulaInfo_ElseNull.WriteLine();
									WriteIngredientToFormulaInfo_ElseNull.WriteLine(formulas[iFormula].sCategory);
									sCategoryWriting=formulas[iFormula].sCategory;
								}
								if (!RString.Contains(alDone,sFormula)) {
									WriteIngredientToFormulaInfo_ElseNull.WriteLine(sFormula);
									alDone.Add(sFormula);
								}
							}
						}
					}
					streamIn.Close();
				}//end if iLines>0
			}//end for iArg
		}//end LoadXEqualsFormulas
	}//end IngredientToRecipe
}//end namespace
