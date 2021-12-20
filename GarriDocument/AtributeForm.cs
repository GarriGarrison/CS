using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Documents
{
    public partial class AtributeForm : Form
    {
        public AtributeForm()
        {
            InitializeComponent();

            textBoxAutor.Text = DeviceUses.AtributeFile.autor;
            textBoxControl.Text = DeviceUses.AtributeFile.control;
            textBoxVersion.Text = DeviceUses.AtributeFile.version;
            textBoxDate.Text = DeviceUses.AtributeFile.date;
            textBoxProgram.Text = DeviceUses.AtributeFile.program;
        }
    }
}
