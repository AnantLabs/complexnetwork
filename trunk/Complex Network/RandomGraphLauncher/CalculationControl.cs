using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Flash.External;
using Newtonsoft.Json;

using RandomGraphLauncher.Controllers;
using CommonLibrary.Model.Attributes;
using CommonLibrary.Model.Util;
using RandomGraph.Settings;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using RandomGraph.Core.Events;
using RandomGraph.Core.Manager.Impl;
using RandomGraph.Core.Manager.Status;
using RandomGraphLauncher.Controls;
using RandomGraphLauncher.Properties;
using AnalyzerFramework.Manager.Impl;
using log4net;

namespace RandomGraphLauncher
{
    // Реализация одной вычислительной формы.
    public partial class CalculationControl : UserControl
    {
        // Организация работы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(CalculationControl));

        // Имя job-а.
        private string jobName;
        // Поля для параметры генерации.
        private Dictionary<GenerationParam, Control> generationParamsControls;
        // Поля для свойств анализа.
        private Dictionary<string, AnalyseOptions> analyzeOptionsControls;

        public CalculationControl(string name)
        {
            log.Info("Started constructing calculation UI.");

            InitializeComponent();

            jobName = name;
            InitializeModelInfo();
            InitializeGenerationParamsControls();
            InitializeAnalyzeOptionsControls();
            InitializeCommonControls();
            InitializeGlobalModes();
        }

        // Обработчики сообщений.

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (Control childControl in genParamsGrp.Controls)
                {
                    if ((string)childControl.Tag == "FilePath")
                    {
                        childControl.Text = openFileDialog1.FileName;
                    }
                }
            }
        }

        private void buttonB_CheckedChanged(object sender, EventArgs e)
        {
            generationParamsControls[GenerationParam.InitialProbability].Hide();
            generationParamsControls[GenerationParam.InitialProbability].Text = "0";
            genParamsGrp.Controls[8].Hide();
        }

        private void buttonA_CheckedChanged(object sender, EventArgs e)
        {
            generationParamsControls[GenerationParam.InitialProbability].Text = "";
            generationParamsControls[GenerationParam.InitialProbability].Show();
            genParamsGrp.Controls[8].Show();
        }
        private void selectallcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (selectAllCheck.Checked)
            {
                deselectAllCheck.Checked = false;
                for (int i = 0; i < optionsCheckList.Items.Count; i++)
                {
                    optionsCheckList.SetItemChecked(i, true);
                }
                selectAllCheck.Enabled = false;
                deselectAllCheck.Enabled = true;
            }
        }

        private void deselectallcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (deselectAllCheck.Checked)
            {
                selectAllCheck.Checked = false;
                for (int i = 0; i < optionsCheckList.Items.Count; i++)
                {
                    optionsCheckList.SetItemChecked(i, false);
                }
                deselectAllCheck.Enabled = false;
                selectAllCheck.Enabled = true;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            log.Info("<Start> button clicked.");
            try
            {
                PassGenerationParameterValues();
            }
            catch (FormatException ex)
            {
                log.Info("FormatException occured. There are some invalid input parameters.");
                log.Fatal(ex);
                MessageBox.Show("There are some invalid input parameter!");
                return;
            }

            PassAnalyzeOptions();
            PassInstanceCount();

            if (CheckParameters())
            {
                for (int i = 0; i < Convert.ToInt32(implementationCountNumeric.Value); i++)
                {
                    calculationStatusGrd.Rows.Add();
                }
                implementationCountNumeric.Enabled = false;
                DisableGenerationParamsInput();
                DisableAnalyseOptins();

                SetButtonEnabled(startBtn, false);
                SetButtonEnabled(pauseBtn, true);
                SetButtonEnabled(stopBtn, true);

                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                Tuple<Dictionary<GenerationParam, object>, AnalyseOptions> tmp =
                    Tuple.Create<Dictionary<GenerationParam, object>, AnalyseOptions>(
                    SessionController.GetGenParamValues(jobName),
                    SessionController.GetSelectedAnalyzeOptions(jobName));
                backgroundStartWorker.RunWorkerAsync(tmp);
            }
            else
            {
                string errMsg = SessionController.GetErrorMessage(jobName);
                if (errMsg != null)
                {
                    MessageBox.Show(errMsg);
                }
                else
                {
                    MessageBox.Show("Generation parameters are too large!");
                }
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            pauseBtn.Enabled = false;
            continueBtn.Enabled = false;
            stopBtn.Enabled = false;
            FreezeGridButtons();
            backgroundStopWorker.RunWorkerAsync();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            pauseBtn.Enabled = false;
            continueBtn.Enabled = true;
            stopBtn.Enabled = true;
            backgroundPauseWorker.RunWorkerAsync();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            pauseBtn.Enabled = true;
            continueBtn.Enabled = false;
            stopBtn.Enabled = true;
            backgroundContinueWorker.RunWorkerAsync();
        }

        private void tableCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                // Кнопка <Stop>.
                if (e.ColumnIndex == 5) 
                {
                    if (stopBtn.Enabled)
                    {
                        SetColumnButtonEnable(
                            (DataGridViewDisableButtonCell)calculationStatusGrd.Rows[e.RowIndex].Cells[e.ColumnIndex], 
                            false);
                        SetColumnButtonEnable(
                            (DataGridViewDisableButtonCell)calculationStatusGrd.Rows[e.RowIndex].Cells[e.ColumnIndex + 1],
                            false);
                        StopInstance(e.RowIndex);
                    }
                }
                // Кнопка <Pause/Continue>.
                else if (e.ColumnIndex == 6)
                {
                    DataGridViewDisableButtonCell cell = 
                        (DataGridViewDisableButtonCell)calculationStatusGrd.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell.Enabled)
                    {
                        string buttonText = (string)cell.Value;
                        if (buttonText == "Pause")
                        {
                            SetColumnButtonText(cell, "Continue");
                            PauseInstance(e.RowIndex);
                        }
                        else if (buttonText == "Continue")
                        {
                            SetColumnButtonText(cell, "Pause");
                            ContinueInstance(e.RowIndex);
                        }
                    }
                }
            }
        }

        private void StartButtonEnableChanged(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundImage = button.Enabled ? Resources.Start : Resources.Start_dis;
        }

        private void StopButtonEnableChanged(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundImage = button.Enabled ? Resources.Stop : Resources.Stop_dis;
        }

        private void PauseButtonEnableChanged(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundImage = button.Enabled ? Resources.Pause : Resources.Pause_dis;
        }

        private void ContinueButtonEnableChanged(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundImage = button.Enabled ? Resources.Cont : Resources.Cont_dis;
        }

        private void motiveLow_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionController.SetAnalyzeOptionValue(jobName, "MotiveLow", motiveLowCmb.SelectedItem);
        }

        private void motiveHi_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionController.SetAnalyzeOptionValue(jobName, "MotiveHigh", motiveHighCmb.SelectedItem);
        }

        private void constant_InputChange(object sender, EventArgs e)
        {
            SessionController.SetAnalyzeOptionValue(jobName, "Constant", this.constantInput.Text);
        }

        private void cyclesLow_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionController.SetAnalyzeOptionValue(jobName, "CyclesLow", cyclesLowCmb.SelectedItem);
        }

        private void stepcount_InputChange(object sender, EventArgs e)
        {
            SessionController.SetAnalyzeOptionValue(jobName, "StepCount", this.stepcountInput.Text);
        }
    

        private void cyclesHi_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionController.SetAnalyzeOptionValue(jobName, "CyclesHigh", cyclesHighCmb.SelectedItem);
        }

        private void manager_OverallProgress(object sender, GraphProgressEventArgs e)
        {
            if (calculationStatusGrd.InvokeRequired)
            {
                OverallCallback d = new OverallCallback(OverallProgress);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                OverallProgress(e);
            }
        }

        private void manager_ExecutionStatusChange(object sender, ExecutionStatusEventArgs e)
        {
            ExecutionStatus currentStatus = e.ExecutionStatus;
            if (currentStatus == ExecutionStatus.Success || 
                currentStatus == ExecutionStatus.Failed || 
                currentStatus == ExecutionStatus.Stopped)
            {
                FreezeGridButtons();
                SetButtonEnabled(stopBtn, false);
                SetButtonEnabled(pauseBtn, false);
                SetButtonEnabled(startBtn, false);
                SetButtonEnabled(continueBtn, false);
                if (currentStatus == ExecutionStatus.Success)
                {
                    toolStripLabel1.Text = "Saving results...";
                    SessionController.SaveResults(jobName);
                    toolStripLabel1.Text = "";
                    MessageBox.Show("Calculation has completed successfully", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (currentStatus == ExecutionStatus.Stopped)
                {
                    MessageBox.Show("Calculation has stopped by user", "Stopped", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (SessionController.GetResultsCount(jobName) != 0)
                    {
                        toolStripLabel1.Text = "Saving results...";
                        SessionController.SaveResults(jobName);
                        toolStripLabel1.Text = "";
                    }
                }
                else if (currentStatus == ExecutionStatus.Failed)
                {
                    MessageBox.Show("Calculation has failed", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void manager_GraphsGenerated(object sender, List<GraphTable> e)
        {
            CallFlash(e);
        }

        private void StartWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Tuple<Dictionary<GenerationParam, object>, AnalyseOptions> args = 
                (Tuple<Dictionary<GenerationParam, object>,
                AnalyseOptions>)e.Argument;
            object[] invokeParams = new object[] { args.Item1, args.Item2, 
                SessionController.GetAnalyzeOptionsValues(jobName)};
            SessionController.StartGraphModel(jobName, invokeParams);
        }

        private void PauseWork(object sender, DoWorkEventArgs e)
        {
            SessionController.PauseJob(jobName);
        }

        private void ContiuneWork(object sender, DoWorkEventArgs e)
        {
            SessionController.ContinueJob(jobName);
        }

        private void StopWork(object sender, DoWorkEventArgs e)
        {
            SessionController.StopJob(jobName);
        }

        private object proxy_ExternalInterfaceCall(object sender, ExternalInterfaceCallEventArgs e)
        {
            switch (e.FunctionCall.FunctionName)
            {
                case "sendToJavaScript":
                    CallFromFlash(e.FunctionCall.Arguments);
                    break;
                default:
                    return null;
            }
            return null;
        }
         
        // Утилиты.

        private void InitializeModelInfo()
        {
            GraphModel graphMetaData = SessionController.GetGraphModel(jobName);
            modelName.Text = "Model name: \n" + graphMetaData.Name;
            description.Text = "Description: \n" + graphMetaData.Description;
            if (graphMetaData.CheckModel)
            {
               // this.infoGrp.Controls.Add(checkModel);
               // this.infoGrp.Controls.Add(buttonB);
              ///  this.infoGrp.Controls.Add(buttonA);
            }

        }

        private void InitializeGenerationParamsControls()
        {
            log.Info("Constructing generation parameters boxes");
            bool randomGeneration = (Options.Generation == Options.GenerationMode.randomGeneration) ? true : false;

            int position = 20;
            if(randomGeneration)
            {
                generationParamsControls = new Dictionary<GenerationParam,Control>();
                List<RequiredGenerationParam> genParams = SessionController.GetRequiredGenParams(jobName);
                foreach (RequiredGenerationParam rp in genParams)
                {
                    GenerationParam p = rp.GenParam;
                    GenerationParamInfo paramInfo = (GenerationParamInfo)(p.GetType().GetField(p.ToString()).
                        GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                    Control control = null;
                    Label textBoxLabel = null;
                    if (paramInfo.Name.Equals("Initial Graph"))
                    {
                        ComboBox listBox = new ComboBox();
                        listBox.DropDownStyle = ComboBoxStyle.DropDown;
                        listBox.Items.Add("Variable");
                        listBox.Items.Add("Permanent");
                        listBox.SelectedIndex = 1;
                        listBox.BackColor = Color.White;
                        listBox.Location = new Point(105, position);
                        listBox.Width = 100;
                        textBoxLabel = new Label() { Width = 100 };
                        textBoxLabel.Location = new Point(15, position);
                        textBoxLabel.Text = paramInfo.Name;
                        generationParamsControls.Add(p, listBox);
                        genParamsGrp.Controls.Add(listBox);
                        genParamsGrp.Controls.Add(textBoxLabel);
                        position += 25;
                       

                    }
                    else
                    {
                        if (paramInfo.Type != typeof(String))
                        {
                            control = new TextBox();
                            control.Width = 100;
                            control.Location = new Point(105, position);
                            if (paramInfo.Name.Equals("Initial Probability"))
                            {
                                control.Text = "0";
                            }

                            textBoxLabel = new Label() { Width = 100 };
                            textBoxLabel.Location = new Point(15, position);

                            textBoxLabel.Text = paramInfo.Name;
                            generationParamsControls.Add(p, control);

                            genParamsGrp.Controls.Add(control);
                            genParamsGrp.Controls.Add(textBoxLabel);
                            position += 25;
                        }

                    }
                    
                   
                }
            }                   
            else
            {
                Control control = new TextBox();
                Label textBoxLabel = null;
                Button brButton = new Button();
                brButton.Click += browseButton_Click;
                brButton.Location = new Point(105, position + 40);
                brButton.Height = 20;
                brButton.Width = 100;
                brButton.Text = "Browse";
                genParamsGrp.Controls.Add(brButton);
                control.Width = 100;
                control.Tag = "FilePath";
                control.Location = new Point(105, position);
                textBoxLabel = new Label() { Width = 100 };
                textBoxLabel.Location = new Point(15, position);
                textBoxLabel.Text = "File Path";
                genParamsGrp.Controls.Add(control);
                genParamsGrp.Controls.Add(textBoxLabel);
                position += 25;
            }  
        }

        private void InitializeAnalyzeOptionsControls()
        {
            analyzeOptionsControls = new Dictionary<string, AnalyseOptions>();
            AnalyseOptions availableOptions = SessionController.GetAvailableAnalyzeOptions(jobName);
            foreach (AnalyseOptions opt in Enum.GetValues(typeof(AnalyseOptions)))
            {                
                if ((opt & availableOptions) == opt && opt != AnalyseOptions.None)
                {
                    AnalyzeOptionInfo optionInfo = (AnalyzeOptionInfo)(opt.GetType().
                        GetField(Enum.GetName(typeof(AnalyseOptions), opt)).
                        GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
                    analyzeOptionsControls.Add(optionInfo.Name, opt);
                    optionsCheckList.Items.Add(optionInfo.Name, true);
                }
                if ((opt & availableOptions) == opt && opt == AnalyseOptions.Motifs)
                {
                    motiveHighCmb.SelectedIndex = 0;
                    motiveLowCmb.SelectedIndex = 0;
                    motiveHighCmb.Show();
                    motiveLowCmb.Show();
                    motives.Show();
                }
                if ((opt & availableOptions) == opt && opt == AnalyseOptions.Cycles)
                {
                    cyclesHighCmb.SelectedIndex = 0;
                    cyclesLowCmb.SelectedIndex = 0;
                    cyclesHighCmb.Show();
                    cyclesLowCmb.Show();
                    cycles.Show();
                }

                if ((opt & availableOptions) == opt && opt == AnalyseOptions.TriangleTrajectory)
                {
                    constantInputLabel.Show();
                    constantInput.Show();
                }
            }
        }

        private void InitializeCommonControls()
        {
            implementationCountNumeric.Enabled = (Options.Generation == Options.GenerationMode.randomGeneration) ? 
                true : false;
        }

        private void InitializeGlobalModes()
        {
            SessionController.SetStatusChangedEventHandler(jobName, manager_ExecutionStatusChange);

            if (Options.TrainingMode)
            {
                SessionController.SetGraphsGeneratedEventHandler(jobName, manager_GraphsGenerated);

                this.axShockwaveFlash1.Visible = true;
                calculationStatusGrp.Visible = false;
                InitFlashAPI();
                InitFlash();
                DisableControlButtons();
            }
            else
            {
                SessionController.SetGraphProgressEventHandler(jobName, manager_OverallProgress);

                this.axShockwaveFlash1.Visible = false;
                calculationStatusGrp.Visible = true;
                if (Options.DistributedMode)
                {
                    calculationStatusGrd.Columns[4].Visible = true;
                }
            }
        }

        private void PassGenerationParameterValues()
        {
            if (Options.GenerationMode.randomGeneration == Options.Generation)
            {
                Dictionary<GenerationParam, object> values = new Dictionary<GenerationParam, object>();
                foreach (GenerationParam paramType in generationParamsControls.Keys)
                {
                    string genParamValue = generationParamsControls[paramType].Text;
                    GenerationParamInfo paramInfo = (GenerationParamInfo)(paramType.GetType().
                        GetField(paramType.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);

                    if (paramInfo.Type.Equals(typeof(Double)))
                    {
                        values.Add(paramType, Convert.ToDouble(genParamValue, CultureInfo.InvariantCulture));
                    }
                    else if (paramInfo.Type.Equals(typeof(Int16)))
                    {
                        values.Add(paramType, Convert.ToInt16(genParamValue));
                    }
                    else if (paramInfo.Type.Equals(typeof(Int32)))
                    {
                        values.Add(paramType, Convert.ToInt32(genParamValue));
                    }
                    else if (paramInfo.Type.Equals(typeof(String)))
                    {
                        values.Add(paramType, genParamValue);
                    }
                }
                SessionController.SetGenParamValuesForJob(jobName, values);
            }
            else if (Options.GenerationMode.staticGeneration == Options.Generation)
            {
                foreach (Control childControl in genParamsGrp.Controls)
                {
                    if ((string)childControl.Tag == "FilePath")
                    {
                        SessionController.SetFilePath(jobName, childControl.Text);
                        break;
                    }
                }
            }
        }

        private void PassAnalyzeOptions()
        {
            AnalyseOptions selectedOptions = AnalyseOptions.None;
            foreach (string option in analyzeOptionsControls.Keys)
            {
                if (optionsCheckList.CheckedItems.Contains(option))
                {
                    selectedOptions |= analyzeOptionsControls[option];
                }
            }
            SessionController.SetSelectedOptions(jobName, selectedOptions);
        }

        private void PassInstanceCount()
        {
            SessionController.SetInstanceCount(jobName, Convert.ToInt32(implementationCountNumeric.Value));
        }

        private bool CheckParameters()
        {
            return SessionController.CheckParameters(jobName);
        }

        private void DisableControlButtons()
        {
            this.pauseBtn.Visible = false;
            this.continueBtn.Visible = false;
        }

        // Замарозить все строки Grid-а.
        private void FreezeGridButtons()
        {
            for (int i = 0; i < calculationStatusGrd.RowCount; i++)
            {
                FreezeGridRow(i);
            }
        }

        // Замарозить i-тую строкы Grid-а.
        private void FreezeGridRow(int index)
        {
            SetColumnButtonEnable((DataGridViewDisableButtonCell)calculationStatusGrd.Rows[index].Cells[5], false);
            SetColumnButtonEnable((DataGridViewDisableButtonCell)calculationStatusGrd.Rows[index].Cells[6], false);
        }

        // Деактивировать поля i-той строки Grid-а. 
        private void SetColumnButtonEnable(DataGridViewDisableButtonCell button, bool enable)
        {
            button.Enabled = enable;
            calculationStatusGrd.InvalidateCell(button.ColumnIndex, button.RowIndex);
        }

        // Деактивация ввода параметров генерации.
        private void DisableGenerationParamsInput()
        {
            if (generationParamsControls != null)
            {
                foreach (var item in generationParamsControls)
                {
                    item.Value.Enabled = false;
                }
            }
            else
            {
                this.genParamsGrp.Enabled = false;
            }
        }

        // Активация ввода параметров генерации.
        private void EnableGenerationParamsInput()
        {
            foreach (var item in generationParamsControls)
            {
                item.Value.Enabled = true;
            }
        }

        // Деактивация отметки свойств анализа.
        private void DisableAnalyseOptins()
        {
            optionsCheckList.Enabled = false;
            motiveLowCmb.Enabled = false;
            motiveHighCmb.Enabled = false;
            cyclesLowCmb.Enabled = false;
            cyclesHighCmb.Enabled = false;
        }

        // Активация отметки свойств анализа.
        private void EnableAnalyseOptins()
        {
            optionsCheckList.Enabled = true;
            motiveLowCmb.Enabled = true;
            motiveHighCmb.Enabled = true;
            cyclesLowCmb.Enabled = true;
            cyclesHighCmb.Enabled = true;
        }

        private void SetButtonEnabled(Button btn, bool val)
        {
            if (btn.InvokeRequired)
            {
                BtnEnabled d = new BtnEnabled(ButtonEnabledThreadSafe);
                this.Invoke(d, new object[] { btn, val });
            }
            else
            {
                btn.Enabled = val;
            }
        }

        private static void ButtonEnabledThreadSafe(Button btn, bool val)
        {
            btn.Enabled = val;
        }

        private void SetColumnButtonText(DataGridViewButtonCell button, string text)
        {
            button.UseColumnTextForButtonValue = false;
            button.Value = text;
        }

        private void StopInstance(int index)
        {
            SessionController.StopJob(jobName, index);
        }

        private void PauseInstance(int index)
        {
            SessionController.PauseJob(jobName, index);
        }

        private void ContinueInstance(int index)
        {
            SessionController.ContinueJob(jobName, index);
        }

        void OverallProgress(GraphProgressEventArgs e)
        {
            if (calculationStatusGrd.Rows.Count != 0)
            {
                GraphProgressStatus status = e.Progress;
                DataGridViewRow row = calculationStatusGrd.Rows[status.ID];
                row.Cells[0].Value = status.ID + 1;
                row.Cells[1].Value = status.GraphProgress;
                row.Cells[2].Value = status.Percent;
                row.Cells[3].Value = status.TargetName;
                row.Cells[4].Value = status.HostName;
            }
        }

        // Утилиты Flash-а, которые используются в тренировачном режиме.

        private void InitFlash()
        {
            log.Info("Initializing Flash.");
            this.axShockwaveFlash1.Movie = AppDomain.CurrentDomain.BaseDirectory + @"\SWF\communication.swf";
        }

        private void InitFlashAPI()
        {
            log.Info("Initializing Flash API.");
            // !исправить!
            //controller.InitFlashApi(this.axShockwaveFlash1, proxy_ExternalInterfaceCall); 
        }

        private void CallFlash(List<GraphTable> e)
        {
            log.Info("Calling Flash.");
            GraphModel graphMetaData = SessionController.GetGraphModel(jobName);
            StringBuilder theJson = new StringBuilder();
            theJson.Append("{\"type\":\"");
            theJson.Append(graphMetaData.Name);
            theJson.Append("\",  \"matrixes\":");
            theJson.Append(JsonConvert.SerializeObject(e));
            theJson.Append(", \"params\":");
            theJson.Append(JsonConvert.SerializeObject(SessionController.GetRequiredGenParams(jobName)));
            theJson.Append("}");
            // !исправить!
            //controller.CallFlash(theJson.ToString());
            log.Info("End calling Flash.");
        }

        private void CallFromFlash(object[] p)
        {
            MessageBox.Show(p[0].ToString());
        }

        // Делегаты.

        private delegate void OverallCallback(GraphProgressEventArgs e);
        private delegate void BtnEnabled(Button btn, bool val);
    }
}
