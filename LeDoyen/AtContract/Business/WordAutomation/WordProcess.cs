using System;
using System.IO;
using System.Reflection;
using LeDoyen.AtContract.Business.WordAutomation.Configuration;
using Microsoft.Office.Interop.Word;
using Template = LeDoyen.AtContract.Business.WordAutomation.Configuration.Template;

namespace LeDoyen.AtContract.Business.WordAutomation
{
	public abstract class WordProcess : IDisposable
	{
		internal Config _config;

		protected static Object _missing = Missing.Value;
		protected static Object _true = true;
		protected static Object _false = false;

		public void Work()
		{
		    Initialize();
			DoWork();
		}

		protected abstract void DoWork();

		protected virtual void Dispose()
		{
		}

		void IDisposable.Dispose()
		{
		}

		internal virtual void Initialize()
		{
		    _config = Config.Load(Path.Combine(Environment.CurrentDirectory, "wordconfig.xml"));
		}

		protected virtual void InitializeApplication()
		{
			Application = new Application();
			Application.Visible = true;
		}

		public Configuration.Template[] TemplateConfigs { get; protected set; }

		public Application Application { get; private set; }

		public _Document Document { get; protected set; }

		#region Sample
		private void InsertLines(int LineNum)
		{
			int iCount;

			// Insert "LineNum" blank lines.	
			for (iCount = 1; iCount <= LineNum; iCount++)
			{
				Application.Selection.TypeParagraph();
			}
		}

		private void FillRow(_Document oDoc, int Row, string Text1,
		                     string Text2, string Text3, string Text4)
		{
			// Insert the data into the specific cell.
			oDoc.Tables[1].Cell(Row, 1).Range.InsertAfter(Text1);
			oDoc.Tables[1].Cell(Row, 2).Range.InsertAfter(Text2);
			oDoc.Tables[1].Cell(Row, 3).Range.InsertAfter(Text3);
			oDoc.Tables[1].Cell(Row, 4).Range.InsertAfter(Text4);
		}

		private void CreateMailMergeDataFile()
		{
			_Document oDataDoc;
			int iCount;

			Object oName = "C:\\DataDoc.doc";
			Object oHeader = "FirstName, LastName, Address, CityStateZip";
			Document.MailMerge.CreateDataSource(ref oName, ref _missing,
			                                    ref _missing, ref oHeader, ref _missing, ref _missing,
			                                    ref _missing, ref _missing, ref _missing);

			// Open the file to insert data.
			oDataDoc = Application.Documents.Open(ref oName, ref _missing,
			                                      ref _missing, ref _missing, ref _missing, ref _missing,
			                                      ref _missing, ref _missing, ref _missing, ref _missing,
			                                      ref _missing, ref _missing, ref _missing, ref _missing,
			                                      ref _missing /*, ref oMissing */);

			for (iCount = 1; iCount <= 2; iCount++)
			{
				oDataDoc.Tables[1].Rows.Add(ref _missing);
			}
			// Fill in the data.
			FillRow(oDataDoc, 2, "Steve", "DeBroux",
			        "4567 Main Street", "Buffalo, NY  98052");
			FillRow(oDataDoc, 3, "Jan", "Miksovsky",
			        "1234 5th Street", "Charlotte, NC  98765");
			FillRow(oDataDoc, 4, "Brian", "Valentine",
			        "12348 78th Street  Apt. 214",
			        "Lubbock, TX  25874");
			// Save and close the file.
			oDataDoc.Save();
			oDataDoc.Close(ref _false, ref _missing, ref _missing);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Selection wrdSelection;
			MailMerge wrdMailMerge;
			MailMergeFields wrdMergeFields;
			Table wrdTable;
			string StrToAdd;

			// Create an instance of Word  and make it visible.
			Application.Visible = true;

			// Add a new document.
			Document = Application.Documents.Add(ref _missing, ref _missing,
			                                     ref _missing, ref _missing);
			Document.Select();

			wrdSelection = Application.Selection;
			wrdMailMerge = Document.MailMerge;

			// Create a MailMerge Data file.
			CreateMailMergeDataFile();

			// Create a string and insert it into the document.
			StrToAdd = "State University\r\nElectrical Engineering Department";
			wrdSelection.ParagraphFormat.Alignment =
				WdParagraphAlignment.wdAlignParagraphCenter;
			wrdSelection.TypeText(StrToAdd);

			InsertLines(4);

			// Insert merge data.
			wrdSelection.ParagraphFormat.Alignment =
				WdParagraphAlignment.wdAlignParagraphLeft;
			wrdMergeFields = wrdMailMerge.Fields;
			wrdMergeFields.Add(wrdSelection.Range, "FirstName");
			wrdSelection.TypeText(" ");
			wrdMergeFields.Add(wrdSelection.Range, "LastName");
			wrdSelection.TypeParagraph();

			wrdMergeFields.Add(wrdSelection.Range, "Address");
			wrdSelection.TypeParagraph();
			wrdMergeFields.Add(wrdSelection.Range, "CityStateZip");

			InsertLines(2);

			// Right justify the line and insert a date field
			// with the current date.
			wrdSelection.ParagraphFormat.Alignment =
				WdParagraphAlignment.wdAlignParagraphRight;

			Object objDate = "dddd, MMMM dd, yyyy";
			wrdSelection.InsertDateTime(ref objDate, ref _false, ref _missing,
			                            ref _missing, ref _missing);

			InsertLines(2);

			// Justify the rest of the document.
			wrdSelection.ParagraphFormat.Alignment =
				WdParagraphAlignment.wdAlignParagraphJustify;

			wrdSelection.TypeText("Dear ");
			wrdMergeFields.Add(wrdSelection.Range, "FirstName");
			wrdSelection.TypeText(",");
			InsertLines(2);

			// Create a string and insert it into the document.
			StrToAdd = "Thank you for your recent request for next " +
			           "semester's class schedule for the Electrical " +
			           "Engineering Department. Enclosed with this " +
			           "letter is a booklet containing all the classes " +
			           "offered next semester at State University.  " +
			           "Several new classes will be offered in the " +
			           "Electrical Engineering Department next semester.  " +
			           "These classes are listed below.";
			wrdSelection.TypeText(StrToAdd);

			InsertLines(2);

			// Insert a new table with 9 rows and 4 columns.
			wrdTable = Document.Tables.Add(wrdSelection.Range, 9, 4,
			                               ref _missing, ref _missing);
			// Set the column widths.
			wrdTable.Columns[1].SetWidth(51, WdRulerStyle.wdAdjustNone);
			wrdTable.Columns[2].SetWidth(170, WdRulerStyle.wdAdjustNone);
			wrdTable.Columns[3].SetWidth(100, WdRulerStyle.wdAdjustNone);
			wrdTable.Columns[4].SetWidth(111, WdRulerStyle.wdAdjustNone);
			// Set the shading on the first row to light gray.
			wrdTable.Rows[1].Cells.Shading.BackgroundPatternColorIndex =
				WdColorIndex.wdGray25;
			// Bold the first row.
			wrdTable.Rows[1].Range.Bold = 1;
			// Center the text in Cell (1,1).
			wrdTable.Cell(1, 1).Range.Paragraphs.Alignment =
				WdParagraphAlignment.wdAlignParagraphCenter;

			// Fill each row of the table with data.
			FillRow(Document, 1, "Class Number", "Class Name",
			        "Class Time", "Instructor");
			FillRow(Document, 2, "EE220", "Introduction to Electronics II",
			        "1:00-2:00 M,W,F", "Dr. Jensen");
			FillRow(Document, 3, "EE230", "Electromagnetic Field Theory I",
			        "10:00-11:30 T,T", "Dr. Crump");
			FillRow(Document, 4, "EE300", "Feedback Control Systems",
			        "9:00-10:00 M,W,F", "Dr. Murdy");
			FillRow(Document, 5, "EE325", "Advanced Digital Design",
			        "9:00-10:30 T,T", "Dr. Alley");
			FillRow(Document, 6, "EE350", "Advanced Communication Systems",
			        "9:00-10:30 T,T", "Dr. Taylor");
			FillRow(Document, 7, "EE400", "Advanced Microwave Theory",
			        "1:00-2:30 T,T", "Dr. Lee");
			FillRow(Document, 8, "EE450", "Plasma Theory",
			        "1:00-2:00 M,W,F", "Dr. Davis");
			FillRow(Document, 9, "EE500", "Principles of VLSI Design",
			        "3:00-4:00 M,W,F", "Dr. Ellison");

			// Go to the end of the document.
			Object oConst1 = WdGoToItem.wdGoToLine;
			Object oConst2 = WdGoToDirection.wdGoToLast;
			Application.Selection.GoTo(ref oConst1, ref oConst2, ref _missing, ref _missing);
			InsertLines(2);

			// Create a string and insert it into the document.
			StrToAdd = "For additional information regarding the " +
			           "Department of Electrical Engineering, " +
			           "you can visit our Web site at ";
			wrdSelection.TypeText(StrToAdd);
			// Insert a hyperlink to the Web page.
			Object oAddress = "http://www.ee.stateu.tld";
			Object oRange = wrdSelection.Range;
			wrdSelection.Hyperlinks.Add(oRange, ref oAddress, ref _missing,
			                            ref _missing, ref _missing, ref _missing);
			// Create a string and insert it into the document
			StrToAdd = ".  Thank you for your interest in the classes " +
			           "offered in the Department of Electrical " +
			           "Engineering.  If you have any other questions, " +
			           "please feel free to give us a call at " +
			           "555-1212.\r\n\r\n" +
			           "Sincerely,\r\n\r\n" +
			           "Kathryn M. Hinsch\r\n" +
			           "Department of Electrical Engineering \r\n";
			wrdSelection.TypeText(StrToAdd);

			// Perform mail merge.
			wrdMailMerge.Destination = WdMailMergeDestination.wdSendToNewDocument;
			wrdMailMerge.Execute(ref _false);

			// Close the original form document.
			Document.Saved = true;
			Document.Close(ref _false, ref _missing, ref _missing);


			// Release References.
			wrdSelection = null;
			wrdMailMerge = null;
			wrdMergeFields = null;
			Document = null;
			Application = null;

			// Clean up temp file.
			File.Delete("C:\\DataDoc.doc");
		}
		#endregion

		protected void SaveDocument(string fileName)
		{
			string dir = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			Application.ChangeFileOpenDirectory(dir);
			object nameObj = Path.GetFileName(fileName);
			object fileFormat = WdSaveFormat.wdFormatXMLDocument;
			Document.SaveAs2000(ref nameObj, ref fileFormat, AddToRecentFiles: ref _false);
		}

		protected static string GetUniqueFileName(string fileName)
		{
			int counter = 1;
			string ext = Path.GetExtension(fileName) ?? "";
			string baseName = fileName.Substring(0, fileName.Length - ext.Length);
			while (File.Exists(fileName))
			{
				fileName = string.Format("{0} ({1}).{2}", baseName, ++counter, ext);
			}
			return fileName;
		}
	}
}