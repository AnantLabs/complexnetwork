using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RandomNetworksExplorer
{
    public class ImageComboBoxCell : DataGridViewTextBoxCell
    {
        public ImageComboBoxCell()
            : base()
        { }

        public override void InitializeEditingControl(int rowIndex, object 
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value. 
            base.InitializeEditingControl(rowIndex, initialFormattedValue, 
                dataGridViewCellStyle);
            ImageComboBoxForDataGridView ctl = DataGridView.EditingControl as ImageComboBoxForDataGridView;
            // Use the default row value when Value property is null. 
            /*if (this.Value == null)
            {
                ctl.Value = (DateTime)this.DefaultNewRowValue;
            }
            else
            {
                ctl.Value = (DateTime)this.Value;
            }*/
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses. 
                return typeof(ImageComboBoxForDataGridView);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains. 

                return typeof(String);
            }
        }
    }
}
