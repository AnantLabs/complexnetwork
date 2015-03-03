using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CarlosAg.ExcelXmlWriter;

using Core;
using Core.Enumerations;
using Core.Attributes;
using Core.Result;

namespace Storage
{
    /// <summary>
    /// 
    /// </summary>
    class ExcelResultStorage : AbstractResultStorage
    {
        Workbook workbook;

        public ExcelResultStorage(string str) : base(str) 
        {
            // TODO maybe exception will be thrown
            if (!storageStr.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                storageStr += Path.DirectorySeparatorChar;
            }
        }

        public override StorageType GetStorageType()
        {
            return StorageType.ExcelStorage;
        }

        public override void Save(ResearchResult result)
        {
            if (!Directory.Exists(storageStr))
            {
                Directory.CreateDirectory(storageStr);
            }

            string fileName = storageStr + result.ResearchName;
            if (File.Exists(fileName + ".xls"))
                fileName += result.ResearchID;

            InitializeWorkbook(result.ResearchName);

            SaveResearchInfo(result.ResearchID, result.ResearchName,
                    result.ResearchType, result.ModelType, result.RealizationCount, 
                    result.Size, result.Edges);
            SaveResearchParameters(result.ResearchParameterValues);
            SaveGenerationParameters(result.GenerationParameterValues);

            for (int i = 0; i < result.EnsembleResults.Count; ++i)
            {
                SaveEnsembleResult(result.EnsembleResults[i], i);
            }
            
            workbook.Save(fileName + ".xls");
        }

        public override void Delete(Guid researchID)
        {
            throw new NotImplementedException();
        }

        public override List<ResearchResult> LoadAllResearchInfo()
        {
            throw new NotImplementedException();
        }

        public override ResearchResult Load(Guid researchID)
        {
            throw new NotImplementedException();
        }

        #region Utilities

        private void InitializeWorkbook(string title)
        {
            workbook = new Workbook();

            // Some optional properties of the Document
            workbook.Properties.Author = "Ani Kocharyan";
            workbook.Properties.Title = title;
            workbook.Properties.Created = DateTime.Now;

            // Add some styles to the Workbook
            WorksheetStyle style = workbook.Styles.Add("HeaderStyle");
            style.Font.FontName = "TimesNewRoman";
            style.Font.Size = 12;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;

            style = workbook.Styles.Add("DefaultStyle");
            style.Font.FontName = "TimesNewRoman";
            style.Font.Size = 11;
            style.Alignment.Horizontal = StyleHorizontalAlignment.Center;

            // Add sheets
            Worksheet researchInfoSheet = workbook.Worksheets.Add("Research Info");
            researchInfoSheet.Table.Columns.Add(new WorksheetColumn(100));
            researchInfoSheet.Table.Columns.Add(new WorksheetColumn(250));

            Worksheet genParametersSheet = workbook.Worksheets.Add("Generation Parameters");
            genParametersSheet.Table.Columns.Add(new WorksheetColumn(100));
            genParametersSheet.Table.Columns.Add(new WorksheetColumn());
        }

        private void SaveResearchInfo(Guid researchID,
            string researchName,
            ResearchType rType,
            ModelType mType,
            int realizationCount,
            UInt32 size,
            Double edges)
        {
            Worksheet researchInfoSheet = workbook.Worksheets["Research Info"];

            WorksheetRow rowID = researchInfoSheet.Table.Rows.Add();
            rowID.Cells.Add(new WorksheetCell("ResearchID", "HeaderStyle"));
            rowID.Cells.Add(new WorksheetCell(researchID.ToString(), "HeaderStyle"));

            WorksheetRow rowName = researchInfoSheet.Table.Rows.Add();
            rowName.Cells.Add(new WorksheetCell("ResearchName", "HeaderStyle"));
            rowName.Cells.Add(new WorksheetCell(researchName, "HeaderStyle"));

            WorksheetRow rowType = researchInfoSheet.Table.Rows.Add();
            rowType.Cells.Add(new WorksheetCell("ResearchType", "HeaderStyle"));
            rowType.Cells.Add(new WorksheetCell(rType.ToString(), "HeaderStyle"));

            WorksheetRow rowModel = researchInfoSheet.Table.Rows.Add();
            rowModel.Cells.Add(new WorksheetCell("ModelType", "HeaderStyle"));
            rowModel.Cells.Add(new WorksheetCell(mType.ToString(), "HeaderStyle"));

            WorksheetRow rowRCount = researchInfoSheet.Table.Rows.Add();
            rowRCount.Cells.Add(new WorksheetCell("RealizationCount", "HeaderStyle"));
            rowRCount.Cells.Add(new WorksheetCell(realizationCount.ToString(), 
                DataType.Number, "HeaderStyle"));

            WorksheetRow rowDate = researchInfoSheet.Table.Rows.Add();
            rowDate.Cells.Add(new WorksheetCell("Date", "HeaderStyle"));
            rowDate.Cells.Add(new WorksheetCell(DateTime.Now.ToString(), "HeaderStyle"));

            WorksheetRow rowSize = researchInfoSheet.Table.Rows.Add();
            rowSize.Cells.Add(new WorksheetCell("Size", "HeaderStyle"));
            rowSize.Cells.Add(new WorksheetCell(size.ToString(), 
                DataType.Number, "HeaderStyle"));

            WorksheetRow rowEdges = researchInfoSheet.Table.Rows.Add();
            rowSize.Cells.Add(new WorksheetCell("Edges", "HeaderStyle"));
            rowSize.Cells.Add(new WorksheetCell(edges.ToString(),
                DataType.Number, "HeaderStyle"));
        }

        private void SaveResearchParameters(Dictionary<ResearchParameter, object> p)
        {
            if (p.Count != 0)
            {
                Worksheet researchParametersSheet = workbook.Worksheets.Add("Research Parameters");
                researchParametersSheet.Table.Columns.Add(new WorksheetColumn(100));
                researchParametersSheet.Table.Columns.Add(new WorksheetColumn(100));

                foreach (ResearchParameter rp in p.Keys)
                {
                    if (p[rp] != null)
                    {
                        WorksheetRow row = researchParametersSheet.Table.Rows.Add();
                        WorksheetCell rCellName = new WorksheetCell(rp.ToString(), "HeaderStyle");
                        WorksheetCell rCellValue = new WorksheetCell(p[rp].ToString(),
                            DataType.Number, "HeaderStyle");
                        row.Cells.Add(rCellName);
                        row.Cells.Add(rCellValue);
                    }
                }
            }
        }

        private void SaveGenerationParameters(Dictionary<GenerationParameter, object> p)
        {
            Worksheet genParametersSheet = workbook.Worksheets["Generation Parameters"];

            foreach (GenerationParameter gp in p.Keys)
            {
                if (p[gp] != null)
                {
                    WorksheetRow row = genParametersSheet.Table.Rows.Add();
                    WorksheetCell rCellName = new WorksheetCell(gp.ToString(), "HeaderStyle");
                    WorksheetCell rCellValue = new WorksheetCell(p[gp].ToString(), 
                        DataType.Number, "HeaderStyle");
                    row.Cells.Add(rCellName);
                    row.Cells.Add(rCellValue);
                }
            }
        }

        private void SaveEnsembleResult(EnsembleResult e, int id)
        {
            foreach (AnalyzeOption opt in e.Result.Keys)
            {
                AnalyzeOptionInfo info = ((AnalyzeOptionInfo[])opt.GetType().GetField(opt.ToString()).GetCustomAttributes(typeof(AnalyzeOptionInfo), false))[0];
                OptionType optionType = info.OptionType;

                switch (optionType)
                {
                    case OptionType.Global:
                        SaveToGlobalSheet(opt.ToString(), e.Result[opt].ToString());
                        break;
                    case OptionType.ValueList:
                        SaveValueListSheet(info, e.Result[opt]);
                        break;
                    case OptionType.Distribution:
                    case OptionType.Trajectory:
                        SaveDistributionSheet(info, e.Result[opt]);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SaveToGlobalSheet(string optName, string optValue)
        {
            Worksheet globalSheet;
            try
            {
                globalSheet = workbook.Worksheets["Global"];
            }
            catch (ArgumentException)
            {
                globalSheet = workbook.Worksheets.Add("Global");
                globalSheet.Table.Columns.Add(new WorksheetColumn(200));
                globalSheet.Table.Columns.Add(new WorksheetColumn(100));
            }

            WorksheetRow row = globalSheet.Table.Rows.Add();
            row.Cells.Add(new WorksheetCell(optName, "DefaultStyle"));
            row.Cells.Add(new WorksheetCell(optValue, DataType.Number, "DefaultStyle"));
        }

        private void SaveValueListSheet(AnalyzeOptionInfo info, Object value)
        {
            if (info.EnsembleResultType.Equals(typeof(List<Double>)))
            {
                Worksheet valueListSheet = workbook.Worksheets.Add(info.FullName);
                valueListSheet.Table.Columns.Add(new WorksheetColumn());

                List<Double> l = value as List<Double>;
                foreach (Double d in l)
                {
                    WorksheetRow row = valueListSheet.Table.Rows.Add();
                    row.Cells.Add(new WorksheetCell(d.ToString(), DataType.Number, "DefaultStyle"));
                }
            }
        }

        private void SaveDistributionSheet(AnalyzeOptionInfo info, Object value)
        {
            int length = (info.FullName.Length > 31) ? 30 : info.FullName.Length;
            Worksheet distributionSheet = workbook.Worksheets.Add(info.FullName.Substring(0, length));
            distributionSheet.Table.Columns.Add(new WorksheetColumn(100));
            distributionSheet.Table.Columns.Add(new WorksheetColumn(100));

            WorksheetRow headerRow = distributionSheet.Table.Rows.Add();
            headerRow.Cells.Add(new WorksheetCell(info.XAxisName, "HeaderStyle"));
            headerRow.Cells.Add(new WorksheetCell(info.YAxixName, "HeaderStyle"));

            if (info.EnsembleResultType.Equals(typeof(SortedDictionary<Double, Double>)))
            {
                SortedDictionary<Double, Double> l = value as SortedDictionary<Double, Double>;
                foreach (Double d in l.Keys)
                {
                    WorksheetRow row = distributionSheet.Table.Rows.Add();
                    row.Cells.Add(new WorksheetCell(d.ToString(), DataType.Number, "DefaultStyle"));
                    row.Cells.Add(new WorksheetCell(l[d].ToString(), DataType.Number, "DefaultStyle"));
                }
            }
            else if (info.EnsembleResultType.Equals(typeof(SortedDictionary<UInt32, Double>)))
            {
                SortedDictionary<UInt32, Double> l = value as SortedDictionary<UInt32, Double>;
                foreach (UInt32 d in l.Keys)
                {
                    WorksheetRow row = distributionSheet.Table.Rows.Add();
                    row.Cells.Add(new WorksheetCell(d.ToString(), DataType.Number, "DefaultStyle"));
                    row.Cells.Add(new WorksheetCell(l[d].ToString(), DataType.Number, "DefaultStyle"));
                }
            }
            else if (info.EnsembleResultType.Equals(typeof(SortedDictionary<UInt16, Double>)))
            {
                SortedDictionary<UInt16, Double> l = value as SortedDictionary<UInt16, Double>;
                foreach (UInt16 d in l.Keys)
                {
                    WorksheetRow row = distributionSheet.Table.Rows.Add();
                    row.Cells.Add(new WorksheetCell(d.ToString(), DataType.Number, "DefaultStyle"));
                    row.Cells.Add(new WorksheetCell(l[d].ToString(), DataType.Number, "DefaultStyle"));
                }
            }
        }

        #endregion
    }
}
