using ConsumoAPIWindowsForms.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsumoAPIWindowsForms.ConsumoAPI_Datos
{
    public class DoctorAPI
    {
        public async Task<List<DoctorCLS>> ListarDoctor()
        {
            string rpta = "";
            HttpClient cliente = new HttpClient();
            string url = "http://192.168.100.43/api/doctor";
            HttpResponseMessage response =  await cliente.GetAsync(url);
            List<DoctorCLS> ListaDoctor = new List<DoctorCLS>();
            if (response!=null)
            {
                rpta = await response.Content.ReadAsStringAsync();
                ListaDoctor = JsonConvert.DeserializeObject<List<DoctorCLS>>(rpta);
            }
            return ListaDoctor;
        }
        public async Task<DoctorCLS> RecuperarDoctor(int iidDoctor)
        {
            string rpta = "";
            HttpClient cliente = new HttpClient();
            string url = "http://192.168.100.43/api/Doctor?iidDoctor="+iidDoctor;
            HttpResponseMessage response = await cliente.GetAsync(url);
            DoctorCLS oDoctor = new DoctorCLS();
            if (response != null)
            {
                rpta = await response.Content.ReadAsStringAsync();
                oDoctor = JsonConvert.DeserializeObject<DoctorCLS>(rpta);
            }
            return oDoctor;
        }
        public async Task<List<ClinicaCLS>> listarClinica()
        {
            string rpta = "";
            HttpClient cliente = new HttpClient();
            string url = "http://192.168.100.43/api/Clinica";
            HttpResponseMessage response = await cliente.GetAsync(url);
            List<ClinicaCLS> oClinica = new List<ClinicaCLS>();
            if (response != null)
            {
                rpta = await response.Content.ReadAsStringAsync();
                oClinica = JsonConvert.DeserializeObject<List<ClinicaCLS>>(rpta);
            }
            return oClinica;
        }
        public async Task<List<EspecialidadCLS>> listarEspecialidad()
        {
            string rpta = "";
            HttpClient cliente = new HttpClient();
            string url = "http://192.168.100.43/api/Especialidad";
            HttpResponseMessage reponse = await cliente.GetAsync(url);
            List<EspecialidadCLS> oEspecialidad = new List<EspecialidadCLS>();
            if (oEspecialidad != null)
            {
                rpta = await reponse.Content.ReadAsStringAsync();
                oEspecialidad = JsonConvert.DeserializeObject<List<EspecialidadCLS>>(rpta);
            }
            return oEspecialidad;
        }
        public async Task<int> eliminarDoctor(int iidDoctor)
        {
            int rpta = 0;
            HttpClient cliente = new HttpClient();
            string url = "http://192.168.100.43/api/Doctor?iidDoctor=" + iidDoctor;
            DoctorCLS oDoctor = new DoctorCLS
            {
                iidDoctor = iidDoctor
            };
            var jsonRequest = JsonConvert.SerializeObject(oDoctor);
            var content = new StringContent(jsonRequest,Encoding.UTF8,"text/json");
            HttpResponseMessage reponse = await cliente.PutAsync(url,content);
            if (reponse != null)
            {
                string rptaCadena = await reponse.Content.ReadAsStringAsync();
                rpta = int.Parse(rptaCadena);
            }
            return rpta;
        }
        public async Task<int> AgregarEditarInformacion(DoctorCLS oDoctorCLS)
        {
            int rpta = 0;
            HttpClient cliente = new HttpClient();
            string url = "http://192.168.100.43/api/Doctor";
            var jsonRequest = JsonConvert.SerializeObject(oDoctorCLS);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");
            HttpResponseMessage reponse = await cliente.PostAsync(url, content);
            if (reponse != null)
            {
                string rptaCadena = await reponse.Content.ReadAsStringAsync();
                rpta = int.Parse(rptaCadena);
            }
            return rpta;
        }
    }
}
