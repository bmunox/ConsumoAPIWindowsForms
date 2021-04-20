using ConsumoAPIWindowsForms.Clases;
using ConsumoAPIWindowsForms.ConsumoAPI_Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumoAPIWindowsForms
{
    public partial class frmPopupDoctor : Form
    {
        DoctorCLS oDoctorCLS;
        public int iidDoctor { get; set; }
        public string nombreArchivo;

        public frmPopupDoctor()
        {
            InitializeComponent();
        }

        private async void frmPopupDoctor_Load(object sender, EventArgs e)
        {
            if (iidDoctor == 0)
            {
                rdMascu.Checked = true;
                this.Text = "Agregando Doctor";
            }
            else
            {
                this.Text = "Editando Doctor";
                DoctorAPI oDoctorApi = new DoctorAPI();
                oDoctorCLS = await oDoctorApi.RecuperarDoctor(iidDoctor);
                txtidDoctor.Text = oDoctorCLS.iidDoctor.ToString();
                txtNombre.Text = oDoctorCLS.nombre;
                txtApPaterno.Text = oDoctorCLS.apPaterno;
                txtApMaterno.Text = oDoctorCLS.apMaterno;
                txtEmail.Text = oDoctorCLS.email;
                dtFecha.Value = oDoctorCLS.fechaContrato;
                //cboClinica.SelectedValue = oDoctorCLS.iidClinica;
                //cboEspecialidad.SelectedValue = oDoctorCLS.iidEspecialidad;
                //Aqui vamos a llenar los combos
                if (oDoctorCLS.iidSexo == 1)
                {
                    rdMascu.Checked = true;
                }
                else { rdFeme.Checked = true; }

                txtSueldo.Text = oDoctorCLS.sueldo.ToString();
                txtCelular.Text = oDoctorCLS.telfCelular == null ? "" : oDoctorCLS.telfCelular;
                string foto = oDoctorCLS.archivo;
                nombreArchivo = oDoctorCLS.nombreArchivo;
                if (foto != null && foto != "" && nombreArchivo != null && nombreArchivo!="")
                {
                    string extension = Path.GetExtension(nombreArchivo).Substring(1);
                    foto = foto.Replace("data:image/"+extension+";base64,","");
                    byte[] fotoArray = Convert.FromBase64String(foto);
                    using (MemoryStream ms = new MemoryStream(fotoArray))
                    {
                        pbFoto.Image = Image.FromStream(ms);
                    }
                }

            }
            llenarCombo();
        }
        public async void llenarCombo()
        {
            DoctorAPI oDoctorAPI = new DoctorAPI();
            List<ClinicaCLS> oClinica = await oDoctorAPI.listarClinica();
            List<EspecialidadCLS> oEspecialidad = await oDoctorAPI.listarEspecialidad();
            //Combo Clinica
            //cboClinica.DataSource = oClinica;
            oClinica.Insert(0, new ClinicaCLS { iidClinica = 0, nombreClinica = "--Seleccione" });
            cboClinica.DataSource = oClinica;
            cboClinica.DisplayMember = "nombreClinica";
            cboClinica.ValueMember = "iidClinica";

            //Comobo Especialidad
            //cboEspecialidad.DataSource = oEspecialidad;
            oEspecialidad.Insert(0, new EspecialidadCLS { iidEspecialidad = 0, NombreEspecialidad = "--Seleccione" });
            cboEspecialidad.DataSource = oEspecialidad;
            cboEspecialidad.DisplayMember = "NombreEspecialidad";
            cboEspecialidad.ValueMember = "iidEspecialidad";
            if (iidDoctor != 0)
            {
                cboClinica.SelectedValue = oDoctorCLS.iidClinica;
                cboEspecialidad.SelectedValue = oDoctorCLS.iidEspecialidad;
            }

        }
        private void btnImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivo de imagen |*jpg;*png;*bmp";
            if (ofd.ShowDialog().Equals(DialogResult.OK))
            {
                nombreArchivo = Path.GetFileName(ofd.FileName);
                byte[] buffer = File.ReadAllBytes(ofd.FileName);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    pbFoto.Image = Image.FromStream(ms);
                }
            }
        }

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            DoctorAPI oDoctorAPI = new DoctorAPI();
            DoctorCLS oDoctor = new DoctorCLS();
            oDoctor.iidDoctor = int.Parse(txtidDoctor.Text);
            oDoctor.nombre = txtNombre.Text;
            oDoctor.apPaterno = txtApPaterno.Text;
            oDoctor.apMaterno = txtApMaterno.Text;
            oDoctor.email = txtEmail.Text;
            oDoctor.iidClinica = (int)cboClinica.SelectedValue;
            oDoctor.iidEspecialidad = (int)cboEspecialidad.SelectedValue; ;
            oDoctor.iidSexo = rdMascu.Checked?1:2;
            oDoctor.sueldo = decimal.Parse(txtSueldo.Text);
            oDoctor.telfCelular = txtCelular.Text;
            oDoctor.fechaContrato = dtFecha.Value==null ? DateTime.Now : dtFecha.Value;

            //Manejo de imagen
            byte[] buffer;
            Image img = pbFoto.Image;
            //ImageFormat format = img.RawFormat;
            ImageFormat format = img.RawFormat as ImageFormat;
            if (img != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap bmp = new Bitmap(img);
                    bmp.Save(ms, format);
                    buffer = ms.ToArray();
                }
                string fotoBase64 = Convert.ToBase64String(buffer);
                string extensionArchivo = Path.GetExtension(nombreArchivo).Substring(1);
                oDoctor.archivo = "data:image/" + extensionArchivo + ";base64,"+fotoBase64;
                oDoctor.nombreArchivo = nombreArchivo;
            }
            oDoctor.bhabilitado = 1;

            int rpta = await oDoctorAPI.AgregarEditarInformacion(oDoctor);
            if (rpta == 1 )
            {
                MessageBox.Show("Se guardo correctamente");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Ocurrio un error");
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
