using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RandomNetworksExplorer
{
    public class ImageComboBoxColumn : DataGridViewColumn
    {
        public ImageComboBoxColumn()
            : base(new ImageComboBoxCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell. 
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(ImageComboBoxCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
