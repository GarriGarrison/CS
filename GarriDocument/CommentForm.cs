using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Documents
{
    public partial class CommentForm : Form
    {
        public CommentForm()
        {
            InitializeComponent();
            //Process.Start(@"Devices\\" + DeviceUses.way_datasheet + "\\Rar\\" + DeviceUses.docum_type + "\\" + DeviceUses.file_name + DeviceUses.file_rev + ".pdf");
            //@"Devices\\" + way_type_device + way_type_docum + "\\_list.dat"

            string[] readText = File.ReadAllLines(@"Devices\\" + DeviceUses.way_datasheet + "\\Rar\\" + DeviceUses.docum_type + "\\" + DeviceUses.file_name + DeviceUses.file_rev + ".dat");
            foreach (string s in readText)
            {
                listBoxComment.Items.Add(s);
            }
        }
    }
}
