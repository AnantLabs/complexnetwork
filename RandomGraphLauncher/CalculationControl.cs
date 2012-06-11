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
using CommonLibrary.Model.Attributes;
using CommonLibrary.Model.Util;
using Flash.External;
using Newtonsoft.Json;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Status;
using RandomGraph.Core.Events;
using RandomGraph.Core.Manager.Impl;
using RandomGraph.Core.Manager.Interface;
using RandomGraph.Core.Manager.Status;
using RandomGraphLauncher.controls;
using RandomGraphLauncher.models;
using RandomGraphLauncher.Properties;
using log4net;

namespace RandomGraphLauncher
{
    public partial class CalculationControl : UserControl
    {
        /// <summary>
        /// The logger static object for monitoring.
        /// </summary>
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(CalculationControl));
        /// <summary>
        /// View controller object.
        /// </summary>
        private RandomGraphLauncher.src.ViewController controller = new src.ViewController();
        private Dictionary<string, AnalyseOptions> analizeOptionBoxes;
        private Dictionary<GenerationParam, Control> genParamBoxes;

        public CalculationControl(Type arg_modelFactoryType, Type arg_modelType, string jobName, AbstractGraphManager manager, bool isDistributed, bool isTrainingMode, bool isTraceingMode)
        {
            log.Info("Started constructing UI");
            controller.Init(arg_modelFactoryType, arg_modelType, jobName, manager, isDistributed, isTrainingMode);
            controller.SetStatusChangedEventHandler(manager_ExecutionStatusChange);
            InitializeComponent();
            InitModelLabels();
            InitSelectionPanels();

            if (isTrainingMode)
            {
                controller.SetGraphsGeneratedEventHandler(manager_GraphsGenerated);
                axShockwaveFlash1.Visible = true;
                groupBox1.Visible = false;
                InitFlashAPI();
                InitFlash();
                DisapleControlButtons();
            }
            else
            {
                axShockwaveFlash1.Visible = false;
                groupBox1.Visible = true;
                controller.SetGraphProgressEventHandler(manager_OverallProgress);
                if (controller.isDistributed)
                {
                    dataGridView1.Columns[4].Visible = true;
                }
            }
            controller.isTraceingMode = isTraceingMode;
            GraphModel graphMetaData = controller.GetGraphModel();
            if (graphMetaData.Name == "Static Model")
            {
                numericUpDown_Instances_Count.Enabled = false;
            }
        }

        private void DisapleControlButtons()
        {
            pauseButton.Visible = false;
            continueButton.Visible = false;
        }

        private void InitFlash()
        {
            log.Info("Init Flash");
            axShockwaveFlash1.Movie = AppDomain.CurrentDomain.BaseDirectory + @"\SWF\communication.swf";
        }

        void manager_GraphsGenerated(object sender, List<GraphTable> e)
        {
            CallFlash(e);
        }

        private void CallFlash(List<GraphTable> e)
        {
            log.Info("Call Flash");
            GraphModel graphMetaData = controller.GetGraphModel();
            StringBuilder theJson = new StringBuilder();
            theJson.Append("{\"type\":\"");
            theJson.Append(graphMetaData.Name);
            theJson.Append("\",  \"matrixes\":");
            theJson.Append(JsonConvert.SerializeObject(e));
            theJson.Append(", \"params\":");
            theJson.Append(JsonConvert.SerializeObject(controller.genParams));
            theJson.Append("}");
            controller.CallFlash(theJson.ToString());
            log.Info("End calling Flash");
        }

        private void InitFlashAPI()
        {
            log.Info("Init Flash api");
            controller.InitFlashApi(axShockwaveFlash1, proxy_ExternalInterfaceCall);
        }

        // Flash API

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

        private void CallFromFlash(object[] p)
        {
            MessageBox.Show(p[0].ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller.CallFlash("Text from .NET");
        }

        private void InitModelLabels()
        {
            GraphModel graphMetaData = controller.GetGraphModel();
            model_Name.Text = "Model name: \n" + graphMetaData.Name;
            Description.Text = "Description: \n" + graphMetaData.Description;
            Gen_Rule.Text = "Generation rule: \n" + graphMetaData.GenerationRule.ToString();
        }

        /*        protected void OnAnalyzeEvent(AnalyzeEventArgs args)
                {
                    if (ExecutionStatusChange != null)
                    {
                        ExecutionStatusChange(this, args);
                    }
                }
        */
        void manager_OverallProgress(object sender, GraphProgressEventArgs e)
        {
            if (dataGridView1.InvokeRequired)
            {
                OverallCallback d = new OverallCallback(OverallProgress);
                this.Invoke(d, new object[] { e });
            }
            else
            {
                OverallProgress(e);
            }
        }

        private delegate void OverallCallback(GraphProgressEventArgs e);

        void OverallProgress(GraphProgressEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                GraphProgressStatus status = e.Progress;
                DataGridViewRow row = dataGridView1.Rows[status.ID];
                row.Cells[0].Value = status.ID + 1;
                row.Cells[1].Value = status.GraphProgress;
                row.Cells[2].Value = status.Percent;
                row.Cells[3].Value = status.TargetName;
                row.Cells[4].Value = status.HostName;
            }
        }

        void manager_ExecutionStatusChange(object sender, ExecutionStatusEventArgs e)
        {
            ExecutionStatus currentStatus = e.ExecutionStatus;
            if (currentStatus == ExecutionStatus.Success || currentStatus == ExecutionStatus.Failed || currentStatus == ExecutionStatus.Stopped)
            {
                frozeGridButtons();
                setButtonEnabled(stopButton, false);
                setButtonEnabled(pauseButton, false);
                setButtonEnabled(startButton, false);
                setButtonEnabled(continueButton, false);
                if (currentStatus == ExecutionStatus.Success)
                {
                    toolStripLabel1.Text = "Saving into database...";
                    controller.Save();
                    toolStripLabel1.Text = "";
                    MessageBox.Show("Calculation has completed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (currentStatus == ExecutionStatus.Stopped)
                {
                    MessageBox.Show("Calculation has stopped by user", "Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (controller.ResultCount() != 0)
                    {
                        toolStripLabel1.Text = "Saving into database...";
                        controller.Save();
                        toolStripLabel1.Text = "";
                    }

                }
                else if (currentStatus == ExecutionStatus.Failed)
                {
                    MessageBox.Show("Calculation has failed", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void setButtonEnabled(Button btn, bool val)
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

        private delegate void BtnEnabled(Button btn, bool val);

        private void InitSelectionPanels()
        {
            log.Info("Starting initialization of selection panels");
            analizeOptionBoxes = new Dictionary<string, AnalyseOptions>();
            foreach (AnalyseOptions opt in Enum.GetValues(typeof(AnalyseOptions)))
            {
                if ((opt & controller.availableOptions) == opt && opt != AnalyseOptions.None)
                {
                    AnalyzeOptionInfo optionInfo = (AnalyzeOptionInfo)(opt.GetType().GetField(Enum.GetName(typeof(AnalyseOptions), opt)).GetCustomAttributes(typeof(AnalyzeOptionInfo), false)[0]);
                    analizeOptionBoxes.Add(optionInfo.Name, opt);
                    checkedListBox_Options.Items.Add(optionInfo.Name, true);
                }
                if ((opt & controller.availableOptions) == opt && opt == AnalyseOptions.Motifs)
                {
                    motiveHi.SelectedIndex = 0;
                    motiveLow.SelectedIndex = 0;
                    motiveHi.Show();
                    motiveLow.Show();
                    motives.Show();

                }
                if ((opt & controller.availableOptions) == opt && opt == AnalyseOptions.Cycles)
                {
                    cyclesHi.SelectedIndex = 0;
                    cyclesLow.SelectedIndex = 0;
                    cyclesHi.Show();
                    cyclesLow.Show();
                    cycles.Show();
                }
            }

            genParamBoxes = new Dictionary<GenerationParam, Control>();
            int position = 20;
            controller.reqGenParams.Sort(delegate(RequiredGenerationParam arg1, RequiredGenerationParam arg2)
            {
                return arg1.Index.CompareTo(arg2.Index);
            });
            log.Info("Constructing generation parameters boxes");
            foreach (RequiredGenerationParam requiredGenerationParam in controller.reqGenParams)
            {
                GenerationParam param = requiredGenerationParam.GenParam;
                GenerationParamInfo paramInfo = (GenerationParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                Control control = null;
                if (paramInfo.Type == typeof(bool))
                {
                    control = new CheckBox();
                }
                if (paramInfo.Type == typeof(String))
                {
                    control = new TextBox();
                    Button brButton = new Button();
                    brButton.Click += browseButton_Click;
                    brButton.Location = new Point(105, position + 40);
                    brButton.Height = 20;
                    brButton.Width = 100;
                    brButton.Text = "Browse";
                    groupBox_Gen_params.Controls.Add(brButton);
                    control.Tag = "filePath";
                }
                else
                {
                    control = new TextBox();
                }

                control.Width = 100;
                control.Location = new Point(105, position);

                Label textBoxLabel = new Label() { Width = 100 };
                textBoxLabel.Location = new Point(15, position);

                textBoxLabel.Text = paramInfo.Name;
                genParamBoxes.Add(param, control);

                groupBox_Gen_params.Controls.Add(control);
                groupBox_Gen_params.Controls.Add(textBoxLabel);
                position += 25;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            log.Info("Start button clicked");
            try
            {
                controller.genParams.Clear();
                foreach (GenerationParam paramType in genParamBoxes.Keys)
                {
                    string a = genParamBoxes[paramType].Text;
                    GenerationParamInfo paramInfo = (GenerationParamInfo)(paramType.GetType().GetField(paramType.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                    if (paramInfo.Type.Equals(typeof(Double)))
                    {
                        controller.genParams.Add(paramType, Convert.ToDouble(a, CultureInfo.InvariantCulture));
                    }
                    else if (paramInfo.Type.Equals(typeof(Int16)))
                    {
                        controller.genParams.Add(paramType, Convert.ToInt16(a));
                    }
                    else if (paramInfo.Type.Equals(typeof(Int32)))
                    {
                        controller.genParams.Add(paramType, Convert.ToInt32(a));
                    }
                    else if (paramInfo.Type.Equals(typeof(String)))
                    {
                        controller.genParams.Add(paramType, a);
                    }
                    else if (paramInfo.Type.Equals(typeof(bool)))
                    {
                        controller.genParams.Add(paramType, Convert.ToBoolean(((CheckBox)genParamBoxes[paramType]).Checked));
                    }

                }
            }
            catch (FormatException ex)
            {
                log.Info("Exception are occured. Invalid input parameter.");
                log.Fatal(ex);
                MessageBox.Show("Invalid input parameter.");
                return;
            }

            AnalyseOptions selectedOptions = AnalyseOptions.None;
            foreach (string option in analizeOptionBoxes.Keys)
            {
                if (checkedListBox_Options.CheckedItems.Contains(option))
                {
                    selectedOptions |= analizeOptionBoxes[option];
                }
            }

            controller.instances = Convert.ToInt32(numericUpDown_Instances_Count.Value);
            SetParamsInfo(controller.genParams, selectedOptions);
            if (controller.CheckGenerationParams(selectedOptions))
            {
                for (int i = 0; i < controller.instances; i++)
                {
                    dataGridView1.Rows.Add();
                }
                numericUpDown_Instances_Count.Enabled = false;
                DisableGenerationParamsInput();
                DisableAnalyseOptins();

                setButtonEnabled(startButton, false);
                setButtonEnabled(pauseButton, true);
                setButtonEnabled(stopButton, true);

                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                Tuple<Dictionary<GenerationParam, object>, AnalyseOptions> tmp = Tuple.Create<Dictionary<GenerationParam, object>, AnalyseOptions>(controller.genParams, selectedOptions);
                backgroundStartWorker.RunWorkerAsync(tmp);
            }
            else
            {
                if (controller.errorMessage != null)
                {
                    MessageBox.Show(controller.errorMessage);
                }
                else
                {
                    MessageBox.Show("Generation parameters are too large.");
                }
            }
        }

        private void DisableAnalyseOptins()
        {
            checkedListBox_Options.Enabled = false;
            motiveLow.Enabled = false;
            motiveHi.Enabled = false;
            cyclesLow.Enabled = false;
            cyclesHi.Enabled = false;
        }

        private void EnableAnalyseOptins()
        {
            checkedListBox_Options.Enabled = true;
            motiveLow.Enabled = true;
            motiveHi.Enabled = true;
            cyclesLow.Enabled = true;
            cyclesHi.Enabled = true;
        }

        private void DisableGenerationParamsInput()
        {
            foreach (var item in genParamBoxes)
            {
                item.Value.Enabled = false;
            }
        }

        private void EnableGenerationParamsInput()
        {
            foreach (var item in genParamBoxes)
            {
                item.Value.Enabled = true;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            pauseButton.Enabled = false;
            continueButton.Enabled = false;
            stopButton.Enabled = false;
            frozeGridButtons();
            backgroundStopWorker.RunWorkerAsync();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            pauseButton.Enabled = false;
            continueButton.Enabled = true;
            stopButton.Enabled = true;
            backgroundPauseWorker.RunWorkerAsync();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            pauseButton.Enabled = true;
            continueButton.Enabled = false;
            stopButton.Enabled = true;
            backgroundContinueWorker.RunWorkerAsync();
        }

        private void randomModeButton_Click(object sender, EventArgs e)
        {
            EnableGenerationParamsInput();
            numericUpDown_Instances_Count.Enabled = true;
            startButton.Enabled = true;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (Control childControl in groupBox_Gen_params.Controls)
                {
                    if (childControl.Tag == "filePath")
                    {
                        childControl.Text = openFileDialog1.FileName;
                    }
                }
            }
        }

        private void StartWork(object sender, DoWorkEventArgs e)
        {

            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Tuple<Dictionary<GenerationParam, object>, AnalyseOptions> args = (Tuple<Dictionary<GenerationParam, object>, AnalyseOptions>)e.Argument;
            object[] invokeParams = new object[] { args.Item1, args.Item2, controller.AnalizeOptionsValues };
            controller.StartGraphModel(invokeParams);
        }

        private void SetParamsInfo(Dictionary<GenerationParam, object> genParams, AnalyseOptions selectedOptions)
        {
            Type[] constructTypes = new Type[] { typeof(Dictionary<GenerationParam, object>), typeof(AnalyseOptions), typeof(int) };
            object[] invokeParams = new object[] { genParams, selectedOptions, 0 };
            //AbstractGraphModel graphModel = (AbstractGraphModel)modelType.GetConstructor(constructTypes).Invoke(invokeParams);
        }

        private void PauseWork(object sender, DoWorkEventArgs e)
        {
            controller.Pause();
        }

        private void ContiuneWork(object sender, DoWorkEventArgs e)
        {
            controller.Continue();
        }

        private void StopWork(object sender, DoWorkEventArgs e)
        {
            controller.Stop();
        }

        private void tableCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 5) // Stop button clicked
                {
                    DataGridViewDisableButtonCell stopButton = (DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    DataGridViewDisableButtonCell manageButton = (DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1];
                    if (stopButton.Enabled)
                    {
                        setColumnButtonEnable(stopButton, false);
                        setColumnButtonEnable(manageButton, false);
                        stopInstance(e.RowIndex);
                    }
                }
                else if (e.ColumnIndex == 6) // Pause/Continue button clicked
                {
                    DataGridViewDisableButtonCell cell = (DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    DataGridViewDisableButtonCell stopButton = (DataGridViewDisableButtonCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1];
                    if (cell.Enabled)
                    {
                        string buttonText = (string)cell.Value;
                        if (buttonText == "Pause")
                        {
                            //setColumnButtonEnable(stopButton, false);
                            setColumnButtonText(cell, "Continue");
                            pauseInstance(e.RowIndex);
                        }
                        else if (buttonText == "Continue")
                        {
                            //setColumnButtonEnable(stopButton, true);
                            setColumnButtonText(cell, "Pause");
                            continueInstance(e.RowIndex);
                        }
                    }
                }
            }
        }

        private void setColumnButtonEnable(DataGridViewDisableButtonCell button, bool enable)
        {
            button.Enabled = enable;
            dataGridView1.InvalidateCell(button.ColumnIndex, button.RowIndex);
        }

        private void frozeGridRow(int index)
        {
            DataGridViewDisableButtonCell manageButton = (DataGridViewDisableButtonCell)dataGridView1.Rows[index].Cells[6];
            DataGridViewDisableButtonCell stopButton = (DataGridViewDisableButtonCell)dataGridView1.Rows[index].Cells[5];
            setColumnButtonEnable(manageButton, false);
            setColumnButtonEnable(stopButton, false);
        }

        private void frozeGridButtons()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                frozeGridRow(i);
            }
        }

        private void setColumnButtonText(DataGridViewButtonCell button, string text)
        {
            button.UseColumnTextForButtonValue = false;
            button.Value = text;
        }

        private void stopInstance(int index)
        {
            controller.Stop(index);
        }

        private void pauseInstance(int index)
        {
            controller.Pause(index);
        }

        private void continueInstance(int index)
        {
            controller.Continue(index);
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

        public void closeCalculation()
        {
            /* if (controller.manager.CurrentExecutionStatus == ExecutionStatus.Starting)
             {
                 controller.Stop();
             }*/
        }

        private void motiveLow_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.AnalizeOptionsValues["motiveLow"] = motiveLow.SelectedItem;
        }

        private void motiveHi_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.AnalizeOptionsValues["motiveHi"] = motiveHi.SelectedItem;
        }

        private void cyclesLow_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.AnalizeOptionsValues["cyclesLow"] = cyclesLow.SelectedItem;
        }

        private void cyclesHi_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.AnalizeOptionsValues["cyclesHi"] = cyclesHi.SelectedItem;
        }

        private void checkedListBox_Options_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((CheckedListBox)sender).Text == "Motives")
            {
                // if (((CheckedListBox)sender).)
            }
        }
    }
}
