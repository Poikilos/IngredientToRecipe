/*
 * Created by SharpDevelop.
 * User: Owner
 * Date: 2/8/2010
 * Time: 5:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;

namespace ExpertMultimedia
{
	/// <summary>
	/// Description of RFormula.
	/// </summary>
	public class RFormula {
		public static int iIngredientInfosUsed=0;
		public static RFormula[] IngredientInfos=null;
		public static ArrayList alCategories=new ArrayList();
		
		/// <summary>
		/// For use by other classes for any purpose desired--initial value should be set before using.
		/// </summary>
		public bool bFlag=false;
		
		public string sName="";
		public string[] sarrIngredient=null;
		public string sCategory="";
		public ArrayList alAttrib=new ArrayList(); //saved/loaded in "(x; y)" format
		public static ArrayList alCategoriesImportant=new ArrayList();
		
		public static int Maximum {
			set {
				if (IngredientInfos!=null) {
					RFormula[] NewInfos=new RFormula[value];
					for (int iOld=0; iOld<IngredientInfos.Length&&iOld<NewInfos.Length; iOld++) {
						NewInfos[iOld]=IngredientInfos[iOld];
					}
					IngredientInfos=NewInfos;
				}
				else IngredientInfos=new RFormula[value];
			}
			get {
				return (IngredientInfos!=null)?IngredientInfos.Length:0;
			}
		}
		
		public RFormula()
		{
		}
		public static string ShortStat(string sFullStatName) {
			string sReturn="";
			if (sFullStatName!=null) {
				sFullStatName=RString.Capitalized(sFullStatName.ToLower());
				sReturn=RString.SafeSubstring(sFullStatName,0,3);
				if (sReturn=="Agi") sReturn="Ag";
				else if (sReturn=="Def") sReturn="D";
				else if (sReturn=="Att") sReturn="Atk";
			}
			return sReturn;
		}
		public static int AttribIncrease(ArrayList AttribsToCheck) {
			int iReturn=0;
			if (AttribsToCheck!=null) {
				foreach (string val in AttribsToCheck) {
					if (val.Contains("^")) {
						string[] sarrPart=RString.Explode(val,'^',true);
						iReturn=RConvert.ToInt(sarrPart[1]);
					}
				}
			}
			return iReturn;
		}
		/// <summary>
		/// Format: "dragon robe (D^108; E:J,A; Reduces damage from fire and ice-based spells by 40 points; Arena:Rank S)"
		/// </summary>
		/// <param name="sLine"></param>
		public static void AddIngredient(string sLine, string sCategory) {
			if (sLine.Contains("(")) {
				string[] sarrParts=RString.Explode(sLine,'(',true);
				string[] sarrAttrib=null;
				if (sarrParts!=null&&sarrParts.Length>1&&sarrParts[1].EndsWith(")")) {
					sarrParts[1]=RString.SafeSubstring(sarrParts[1],0,sarrParts[1].Length-1);
					sarrAttrib=RString.Explode(sarrParts[1],';',true);
				}
				ArrayList alAttribNew=new ArrayList();
				if (sarrAttrib!=null) {
					for (int i=0; i<sarrAttrib.Length; i++) {
						RString.RemoveEndsWhiteSpace(ref sarrAttrib[i]);
						if (sarrAttrib[i]!=null&&sarrAttrib[i]!="") {
							alAttribNew.Add(sarrAttrib[i]);
						}
					}
				}
				RFormula.AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(sarrParts[0].ToLower(),alAttribNew,null,sCategory);
			}
		}//end AddIngredient
		public static void SortKnownsByAscendingAttributeIncrease() {
			RFormula[] formulas=new RFormula[RFormula.IngredientInfos.Length];
			int iNewSize=0;
			int iFlag=0;
			for (iFlag=0; iFlag<RFormula.iIngredientInfosUsed; iFlag++) {
				RFormula.IngredientInfos[iFlag].bFlag=false;
			}
			for (int iValue=0; iValue<10000&&iNewSize<RFormula.iIngredientInfosUsed; iValue++) {
				for (int index=0; index<RFormula.iIngredientInfosUsed; index++) {
					if (AttribIncrease(RFormula.IngredientInfos[index].alAttrib)==iValue) {
						formulas[iNewSize]=RFormula.IngredientInfos[index];
						RFormula.IngredientInfos[index].bFlag=true;
						iNewSize++;
					}
				}
			}
			for (iFlag=0; iFlag<RFormula.iIngredientInfosUsed; iFlag++) {
				if (!RFormula.IngredientInfos[iFlag].bFlag) {
					formulas[iNewSize]=RFormula.IngredientInfos[iFlag];
					iNewSize++;
				}
			}
			RFormula.IngredientInfos=formulas;
			RFormula.iIngredientInfosUsed=iNewSize;
		}//end SortKnownsByAscendingAttributeIncrease
		public static bool IsImportantCategory(string sCategory) {
			bool bFound=false;
			foreach (string val in alCategoriesImportant) {
				if (val.ToLower()==sCategory.ToLower()) {
					bFound=true;
					break;
				}
			}
			return bFound;
		}
		public static void AddCategoryIfUnique(string sCategory) {
			AddCategoryIfUnique(sCategory,false);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sCategory">DOES allow "" but NOT null</param>
		/// <param name="bStartsSection"></param>
		public static void AddCategoryIfUnique(string sCategory, bool bStartsSection) {
			if (sCategory!=null) {
				RString.RemoveEndsWhiteSpace(ref sCategory);
				bool bFound=false;
				foreach (string val in alCategories) {
					if (sCategory.ToLower()==val.ToLower()) {
						bFound=true;
						break;
					}
				}
				if (!bFound) {
					if (bStartsSection) alCategoriesImportant.Add(sCategory);
					alCategories.Add(sCategory);
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="NameNew"></param>
		/// <param name="AttributesNew">If an attribute ends with g, that (the last) word will be used as the unique identifier (so that Pickham 100g will not be appended if Farebury 100g exists already)</param>
		/// <param name="IngredientsNew"></param>
		/// <param name="CategoryNew">Will overwrite previous category if previous category is blank or starting with '~'</param>
		/// <returns></returns>
		public static int AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(string NameNew, ArrayList AttributesNew, string[] IngredientsNew, string CategoryNew) {
			int iAddedAt=-1;
			AddCategoryIfUnique(CategoryNew);
			string NameNew_ToLower=NameNew.ToLower();
			iAddedAt=IndexOf(RFormula.IngredientInfos,RFormula.iIngredientInfosUsed,NameNew_ToLower);
			if (iAddedAt<0)  {
				if (IngredientInfos==null) IngredientInfos=new RFormula[1000];
				if (IngredientInfos.Length<iIngredientInfosUsed+1) {
					Maximum=(int)(Maximum*1.5)+10;
				}
				iAddedAt=iIngredientInfosUsed;
				IngredientInfos[iAddedAt]=new RFormula();
				iIngredientInfosUsed++;
			}
			if (RFormula.IngredientInfos[iAddedAt].alAttrib==null) {
				RFormula.IngredientInfos[iAddedAt].alAttrib=AttributesNew;
			}
			else {
				foreach (string sNew in AttributesNew) {
					bool bFound=false;
					foreach (string sOld in RFormula.IngredientInfos[iAddedAt].alAttrib) {
						if (sOld.EndsWith("g")&&sNew.EndsWith("g")) {
							string[] sarrOld=RString.Explode(sOld,' ',true);
							string[] sarrNew=RString.Explode(sNew,' ',true);
							if (sarrOld.Length>1&&sarrNew.Length>1
							    &&sarrOld[sarrOld.Length-1].Length>1
								    &&sarrNew[sarrNew.Length-1].Length>1
								    &&RString.IsDigit(sarrOld[sarrOld.Length-1][sarrOld[sarrOld.Length-1].Length-2])
								    &&RString.IsDigit(sarrNew[sarrNew.Length-1][sarrNew[sarrNew.Length-1].Length-2])
								   ) {
								bFound=true;
								break;
							}
						}
						if (sOld.ToUpper()==sNew.ToUpper()) {
							bFound=true;
							break;
						}
					}
					if (!bFound) RFormula.IngredientInfos[iAddedAt].alAttrib.Add(sNew);
				}
			}
			if (IngredientsNew!=null) {
				if (RFormula.IngredientInfos[iAddedAt].sarrIngredient==null
				    ||RFormula.IngredientInfos[iAddedAt].sarrIngredient.Length<IngredientsNew.Length) {
					RFormula.IngredientInfos[iAddedAt].sarrIngredient=new string[IngredientsNew.Length];
					for (int iCopy=0; iCopy<IngredientsNew.Length; iCopy++) {
						RFormula.IngredientInfos[iAddedAt].sarrIngredient[iCopy]=IngredientsNew[iCopy];
					}
				}
			}
			if (CategoryNew!=null&&CategoryNew!=""
			    && (RFormula.IngredientInfos[iAddedAt].sCategory==null
			        ||RFormula.IngredientInfos[iAddedAt].sCategory==""
			        ||RFormula.IngredientInfos[iAddedAt].sCategory.StartsWith("~")) ) {
				RFormula.IngredientInfos[iAddedAt].sCategory=CategoryNew;
			}
			IngredientInfos[iAddedAt].sName=NameNew_ToLower;
			return iAddedAt;
		}//end AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists
		/// <summary>
		/// Processes a text equation.  If bLineHasSumLast, then processes as x(a,b)+y(c,d)=z(e,f)
		/// else as z(e,f)=x(a,b)+y(c,d)
		/// </summary>
		/// <param name="sLine"></param>
		/// <param name="bLineHasSumLast"></param>
		public void FromFormula(string sLine, bool bLineHasSumLast, string sCategoryNow) {
			int iChar=0;
			sCategory=sCategoryNow;
			if (sLine!=null) {
				int iSign=sLine.IndexOf("=");
				if (iSign>=0) {
					if (bLineHasSumLast) {
						sName=RString.RemoveEndsWhiteSpace( RString.SafeSubstring(sLine,iSign+1) );
						sarrIngredient=RString.Explode(RString.SafeSubstring(sLine,0,iSign),'+',true);
					}
					else {
						sName=RString.RemoveEndsWhiteSpace( RString.SafeSubstring(sLine,0,iSign) );
						sarrIngredient=RString.Explode(RString.SafeSubstring(sLine,iSign+1),'+',true);
					}
				}
				else {
					sName=sLine;
					Console.Error.WriteLine("FromFormula: no '=' in "+sLine);
				}
				string[] sarrParts=null;
				if (sName.Contains("(")) {
					sarrParts=RString.Explode(sName, '(', true);
					sName=sarrParts[0].ToLower();
					if (sarrParts[1].EndsWith(")")) sarrParts[1]=RString.SafeSubstring(sarrParts[1],0,sarrParts[1].Length-1);
					string[] sarrAttrib=RString.Explode(sarrParts[1],';',true);
					if (alAttrib!=null) alAttrib.Clear();
					else alAttrib=new ArrayList();
					for (int iNewAttrib=0; iNewAttrib<sarrAttrib.Length; iNewAttrib++) {
						alAttrib.Add(sarrAttrib[iNewAttrib]);
					}
				}
				else sarrParts=new string[]{sName.ToLower()};
				AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(sarrParts[0],alAttrib,null,sCategory);//do NOW AND later, to inforce order in case ingredient appears at a different point
				for (int i=0; i<sarrIngredient.Length; i++) {
					if (sarrIngredient[i].Contains("(")) {
						string[] sarrIngredientParts=RString.Explode(sarrIngredient[i],'(',true);
						sarrIngredientParts[0]=sarrIngredientParts[0].ToLower();
						if (sarrIngredientParts[1].EndsWith(")")) sarrIngredientParts[1]=RString.SafeSubstring(sarrIngredientParts[1],0,sarrIngredientParts[1].Length-1);
						ArrayList alInfoAttrib=new ArrayList();
						string[] sarrInfoAttrib=RString.Explode(sarrIngredientParts[1],';',true);
						for (int index=0; index<sarrInfoAttrib.Length; index++) {
							alInfoAttrib.Add(sarrInfoAttrib[index]);
						}
						AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(sarrIngredientParts[0],alInfoAttrib,null,null);
						sarrIngredient[i]=sarrIngredientParts[0];
					}
					else sarrIngredient[i]=sarrIngredient[i].ToLower();
				}
				RFormula.AddIngredientInfo_AppendingAspectsNotAlreadyRecordedIfExists(sName.ToLower(),alAttrib,sarrIngredient,sCategoryNow);
			}//end if sLine!=null
		}//end FromReverseFormula
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sName"></param>
		/// <returns>in parenthesis separated by "; " else blank string</returns>
		public static string AttributesOf(string sName, bool bPrecedeBySpaceIfNonEmpty, bool bEnloseInParenthesisIfNonEmpty) {
			string sReturn="";
			ArrayList alCondensed=new ArrayList();
			int iFound=IndexOf(RFormula.IngredientInfos,RFormula.iIngredientInfosUsed,sName);
			if (iFound>=0) {
				if (RFormula.IngredientInfos[iFound].alAttrib!=null) {
					foreach (string AttibOrig in RFormula.IngredientInfos[iFound].alAttrib) {
						string attrib=AttibOrig;
						if (attrib!=null) {
							RString.RemoveEndsWhiteSpace(ref attrib);
							if (attrib!=null
								&&attrib!="") {
								alCondensed.Add(attrib);
							}
						}
					}
				}
				if (alCondensed.Count>0) {
					if (bPrecedeBySpaceIfNonEmpty) sReturn+=" ";
					if (bEnloseInParenthesisIfNonEmpty) sReturn+="(";
					bool bFirst=true;
					foreach (string val in alCondensed) {
						sReturn+=(bFirst?"":"; ")+val;
						bFirst=false;
					}
					if (bEnloseInParenthesisIfNonEmpty) sReturn+=")";
				}
			}
			return sReturn;
		}//end AttributesOf
		public string[] ToIngredientToRecipeFormulas(bool bSumLast,bool bAddHtml) {
			string[] sarrReturn=null;
			if (sarrIngredient!=null&&sName!=null&&sName!="") {
				sarrReturn=new string[sarrIngredient.Length];
				for (int iNow=0; iNow<sarrIngredient.Length; iNow++) {
					if (bSumLast) sarrReturn[iNow]=Reordered(sarrIngredient,iNow," + ",bAddHtml)+" = "+(bAddHtml?"<b>":"")+sName+(bAddHtml?"</b>":"")+AttributesOf(sName,true,true);
					else sarrReturn[iNow]=(bAddHtml?"<b>":"")+sName+(bAddHtml?"</b>":"")+AttributesOf(sName,true,true)+" = "+Reordered(sarrIngredient,iNow," + ",bAddHtml);
				}
			}
			return sarrReturn;
		}
		public override string ToString() {
			return ToString(false,false);
		}
		/// <summary>
		/// Returns formula if item has any ingredients.
		/// </summary>
		/// <param name="bSumLast"></param>
		/// <param name="bAddHtml"></param>
		/// <returns></returns>
		public string ToString(bool bSumLast, bool bAddHtml) {
			string sReturn=null;
			string sNamePart=RString.Capitalized(sName);
			string sIngredientsPart="";
			bool bAnyIngredients=false;
			if (sarrIngredient!=null&&sarrIngredient.Length>0) {
				for (int index=0; index<sarrIngredient.Length; index++) {
					if (sarrIngredient[index]!=""&&sarrIngredient[index]!=null
					    &&sarrIngredient[index].ToLower()!=this.sName.ToLower()) {
						bAnyIngredients=true;
						break;
					}
				}
				if (bAnyIngredients) {
					for (int iIngredient=0; iIngredient<sarrIngredient.Length; iIngredient++) {
						if (iIngredient!=0) sIngredientsPart+=" + ";
						if (bAddHtml) sIngredientsPart+="<i>";
						sIngredientsPart+=RString.Capitalized(sarrIngredient[iIngredient]);
						if (bAddHtml) sIngredientsPart+="</i>";
						int index=RFormula.IndexOf(RFormula.IngredientInfos,RFormula.iIngredientInfosUsed,sarrIngredient[iIngredient]);
						if (index>=0&&RFormula.IngredientInfos[index].alAttrib.Count>0) {
							sIngredientsPart+=" ("+RString.ToString(RFormula.IngredientInfos[index].alAttrib,"; ","\"")+")";
						}
					}
				}//end if any ingredients neither self nor blank
			}//end if any ingredent array
			if (bAddHtml&&bAnyIngredients) sNamePart="<b>"+sNamePart+"</b>";
			//bool bAnyAttribs=false;
			
			if (this.alAttrib!=null&&this.alAttrib.Count>0) {
				//foreach (string sAttribOrig in this.alAttrib) {
				//	string sAttrib=sAttribOrig;
				//	RString.RemoveEndsWhiteSpace(ref sAttrib);
				//	if (sAttrib!=null&&sAttrib!="") {
				//		bAnyAttribs=true;
				//		break;
				//	}
				//}
				//if (bAnyAttribs) sNamePart+=" ("+RString.ToString(this.alAttrib,"; ","\"")+")";
				sNamePart+=AttributesOf(sName.ToLower(),true,true);
			}
			if (sIngredientsPart!=null&&sIngredientsPart!="") {
				if (bSumLast) sReturn=sIngredientsPart+" = "+sNamePart;
				else {
					sReturn=sNamePart+" = "+sIngredientsPart;
				}
			}
			else {
				sReturn=sNamePart;
			}
			return sReturn;
		}//end ToString
		public static int IndexOf(RFormula[] formulas, int iMax, string sName) {
			int iReturn=-1;
			string sName_ToLower=sName.ToLower();
			if (formulas!=null) {
				for (int i=0; i<formulas.Length&&i<iMax; i++) {
					if (formulas[i]!=null) {
						if (formulas[i].sName==sName_ToLower) {
							iReturn=i;
							break;
						}
					}
				}
			}
			return iReturn;
		}//end IndexOf
		public static bool HasSameIngredients(RFormula fmla1, RFormula fmla2) {
			int iMustMatch=0;
			int iMatched=-1;
			if (fmla1.sarrIngredient!=null&&fmla2.sarrIngredient!=null) {
				iMustMatch=fmla2.sarrIngredient.Length;
				iMatched=0;
				if (fmla2.sarrIngredient.Length==fmla1.sarrIngredient.Length) {
					for (int i=0; i<fmla1.sarrIngredient.Length; i++) {
						if (RString.Contains(fmla2.sarrIngredient,fmla1.sarrIngredient[i])) {
							iMatched++;
						}
						else break;
					}
				}
			}
			return iMustMatch==iMatched;
		}
		public static string Reordered(string[] sarrVal, int iFirstVal, string sDelimiter, bool bAddHtml) {
			string sReturn="";
			sReturn=(bAddHtml?"<i>":"")+RString.Capitalized(sarrVal[iFirstVal])+(bAddHtml?"</i>":"")+AttributesOf(sarrVal[iFirstVal],true,true);
			for (int i=0; i<sarrVal.Length; i++) {
				//string sAppend=="";
				//int iFormula=IndexOf(IngredientInfos,RFormula.iIngredientInfosUsed,sarrVal[i]);
				//if (iFormula>=0&&RFormula.IngredientInfos[iFormula].alAttrib.Count>0) {
				//	sAppend=" ("+RString.ToString(RFormula.IngredientInfos[iFormula].alAttrib,"; ","\"")+")";
				//}
				if (i!=iFirstVal) sReturn+=sDelimiter+(bAddHtml?"<i>":"")+RString.Capitalized(sarrVal[i])+(bAddHtml?"</i>":"")+AttributesOf(sarrVal[i],true,true);//+sAppend;
			}
			return sReturn;
		}
	}//end RFormula
}//end namespace
