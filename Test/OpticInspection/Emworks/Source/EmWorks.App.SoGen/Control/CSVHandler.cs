using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmWorks.App.SoGen
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="CSVHandler"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2020-12-09
    /// Description : CSV Handler
    /// </summary>
    public static class CSVHandler
    {
        #region Methods

        public static string CsvToJson(string filePath, ref System.Windows.Controls.TextBox txb)
        {
            int CheckIndex = 0;

            try
            {
                // Get lines.
                if (filePath == null) return null;

                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length < 2) throw new InvalidDataException("Must have header line.");

                // Get headers.
                string[] headers = lines.First().Split(',');

                // Build JSON array.
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < lines.Length; i++)
                {
                    CheckIndex = i + 1;
                    string[] fields = lines[i].Split(',');
                    if (fields.Length != headers.Length)
                    {
                        OutputMessage("Field count must match header count.", ref txb);
                        OutputMessage("Check Index :" + CheckIndex.ToString(), ref txb);
                        return string.Empty;
                    }

                    var jsonElements = headers.Zip(fields, (header, field) => string.Format(@"""{0}"":""{1}""", header, field)).ToArray();
                    string jsonObject = @"""" + (i - 1).ToString() + @""":" + "{" + string.Format("{0}", string.Join(",", jsonElements)) + "},";

                    sb.AppendLine(jsonObject);
                }

                string resultString = sb.ToString();

                resultString = "{" + resultString.Substring(0, resultString.Length - 3) + "}";
                return resultString;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                OutputMessage(ex.ToString(), ref txb);
                OutputMessage("Check Index :" + CheckIndex.ToString(), ref txb);
                return string.Empty;
            }
        }

        public static bool RunExport(System.Windows.Forms.DataGridView dgvItemList, string objectFile, string outputPath, ref System.Windows.Controls.TextBox txb)
        {
            if (dgvItemList.Rows.Count > 0)
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                string fileName = Path.GetFileNameWithoutExtension(objectFile);
                sfd.FileName = fileName + ".csv";
                sfd.InitialDirectory = outputPath;

                bool fileError = false;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            OutputMessage("It wasn't possible to write the data to the disk." + ex.Message, ref txb);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dgvItemList.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dgvItemList.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dgvItemList.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dgvItemList.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    string data = string.Empty;

                                    if (dgvItemList.Rows[i - 1].Cells[j].Value != null)
                                    {
                                        data = dgvItemList.Rows[i - 1].Cells[j].Value.ToString();
                                    }

                                    outputCsv[i] += data + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);

                            return true;
                        }
                        catch (Exception ex)
                        {
                            OutputMessage("Error :" + ex.Message, ref txb);
                            return false;
                        }
                    }
                }

                return false;
            }
            else
            {
                OutputMessage("No Record To Export !!!", ref txb);
                return false;
            }
        }

        public static bool RunImport(string objectFile,
                                            string outputPath,
                                    string objectsNamespace,
                                    string selectClassName,
                                    ref List<object> objects,
                                    ref System.Windows.Controls.TextBox txb)
        {
            if (objectFile == string.Empty) return false;

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = outputPath;
            openFileDialog.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;

            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string jsonText = CSVHandler.CsvToJson(openFileDialog.FileName, ref txb);

                if (jsonText == string.Empty)
                {
                    OutputMessage("CSV 파일 열기에 실패하였습니다. 파일을 확인해주세요", ref txb);
                    return false;
                } // else

                string loggerData = string.Empty;
                if (JSONHandler.OpenJsonText(jsonText, objectsNamespace, selectClassName, ref objects, out loggerData) == true)
                {
                    return true;
                }
                else
                {
                    OutputMessage("파일 열기에 실패 했습니다. [ " + loggerData + "]", ref txb);
                    return false;
                }

                return false;
            }

            return false;
        }

        private static void OutputMessage(string message, ref System.Windows.Controls.TextBox txb)
        {
            try
            {
                if (txb == null)
                {
                    return;
                }

                txb.AppendText(message + "\n");
                txb.ScrollToEnd();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return;
            }
        }

        #endregion Methods
    }
}
