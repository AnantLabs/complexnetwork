using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CommonLibrary.Model.Attributes;
using AnalyzerFramework.Manager.ModelRepo;

namespace RandomGraphLauncher
{
    public partial class ModelCheckWindow : Form
    {
        public ModelCheckWindow()
        {
            InitializeComponent();
        }

        private void ModelCheckWindow_Load(object sender, EventArgs e)
        {
            InitializeModelNameCmb();
        }

        // Utilities //
        private void InitializeModelNameCmb()
        {
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelFactoryTypes();
            foreach (Type modelFactoryType in availableModelFactoryTypes)
            {
                object[] attr = modelFactoryType.GetCustomAttributes(typeof(TargetGraphModel), false);
                TargetGraphModel targetGraphMetadata = (TargetGraphModel)attr[0];
                Type modelType = targetGraphMetadata.GraphModelType;

                attr = modelType.GetCustomAttributes(typeof(GraphModel), false);
                string modelName = ((GraphModel)attr[0]).Name;

                this.modelNameCmb.Items.Add(modelName);
            }

            this.modelNameCmb.SelectedIndex = 0;
        }
    }
}
