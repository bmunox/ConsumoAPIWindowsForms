using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsumoAPIWindowsForms.Clases;
using ConsumoAPIWindowsForms.ConsumoAPI_Datos;

namespace ConsumoAPIWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listarDoctor();
        }
        public async void listarDoctor()
        {
            DoctorAPI oDoctorAPI = new DoctorAPI();
            List<DoctorCLS> listaDoctor = await oDoctorAPI.ListarDoctor();
            dgvDoctor.DataSource = listaDoctor;

            for (int i = 6; i < dgvDoctor.Columns.Count; i++)
            {
                dgvDoctor.Columns[i].Visible = false;
            }
        }

        private void tsNuevo_Click(object sender, EventArgs e)
        {
            frmPopupDoctor ofrmPopupDoctor = new frmPopupDoctor();
            ofrmPopupDoctor.iidDoctor = 0;
            ofrmPopupDoctor.ShowDialog();
            if (ofrmPopupDoctor.DialogResult.Equals(DialogResult.OK))
            {
                listarDoctor();
            }
        }

        private void tsEditar_Click(object sender, EventArgs e)
        {
            frmPopupDoctor ofrmPopupDoctor = new frmPopupDoctor();
            ofrmPopupDoctor.iidDoctor = (int)dgvDoctor.CurrentRow.Cells[0].Value;
            ofrmPopupDoctor.ShowDialog();
            if (ofrmPopupDoctor.DialogResult.Equals(DialogResult.OK))
            {
                listarDoctor();
            }
        }

        private async void tsEliminar_Click(object sender, EventArgs e)
        {
            int iidDoctor = (int)dgvDoctor.CurrentRow.Cells[0].Value;
            DoctorAPI oDoctor = new DoctorAPI();
            var rpta = await oDoctor.eliminarDoctor(iidDoctor);
            if (rpta == 1)
            {
                MessageBox.Show("Se elimino correctamente");
                listarDoctor();
            }
            else
            {
                MessageBox.Show("Error al eliminar el registro");
                listarDoctor();
            }
        }
    }
}
