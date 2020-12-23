using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OfxImport
{
    public partial class frmImport : Form
    {
        public frmImport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                dataGridView1.DataSource = OfxController.LerArquivoOfx(openFileDialog.FileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(225,225,162);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == colValor.Index)
            {
                var value = (double)e.Value;
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = value > 0 ? Color.Navy : Color.Red;
            }
        }
    }
}
