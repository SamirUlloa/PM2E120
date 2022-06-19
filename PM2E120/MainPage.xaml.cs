using Ejercicio1_3.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E120
{
    public partial class MainPage : ContentPage
    {
        double lati;
        double longi;

        public MainPage()
        {
            InitializeComponent();
            ubicacion();
            llenarDatos();
        }

        

        public async void ubicacion()
        {
            //////////////////////////////////
            ///



            ///////////////////////////////



            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;
            if (locator.IsGeolocationAvailable)
            {
                if (locator.IsGeolocationEnabled)
                {
                    if (!locator.IsListening)
                    {
                        await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 5);
                    }
                    locator.PositionChanged += (cambio, args) =>
                    {
                        var loc = args.Position;
                        lon.Text = loc.Longitude.ToString();
                        longi = double.Parse(lon.Text);
                        lat.Text = loc.Latitude.ToString();
                        lati = double.Parse(lat.Text);
                    };
                }
            }
        }

        private async void btnMostrarUbicacion_Clicked(object sender, EventArgs e)
        {
            MapLaunchOptions options = new MapLaunchOptions { Name = "Ubicación Actual" };
            await Map.OpenAsync(lati, longi, options);
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Personas pers = new Personas
                {
                    latitud = lat.Text,
                    longitud = lon.Text,
                    descripcion = txtDescripcion.Text,
                };
                await App.SQLiteDB.GuardarPersona(pers);
                
                txtDescripcion.Text = "";

                llenarDatos();
                await DisplayAlert("Registro", "Guardado Exitosamente", "OK");
            }
            else
            {
                await DisplayAlert("Advertencia", "Campos Vacias", "OK");
            }
        }

        public async void llenarDatos()
        {
            var personaList = await App.SQLiteDB.GetPersonas();
            if (personaList != null)
            {
                listPersonas.ItemsSource = personaList;
            }
        }

        public bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(lat.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(lon.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void listPersonas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Personas)e.SelectedItem;
            btnGuardar.IsVisible = false;
            btnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.id.ToString()))
            {
                var persona = await App.SQLiteDB.GetPersonasByIdAsync(obj.id);
                if (persona != null)
                {
                    lblId.Text = persona.id.ToString();
                    lat.Text = persona.latitud;
                    lon.Text = persona.longitud;
                    txtDescripcion.Text = persona.descripcion;
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var persona = await App.SQLiteDB.GetPersonasByIdAsync(Convert.ToInt32(lblId.Text));

            if (persona != null)
            {
                await App.SQLiteDB.EliminarPersona(persona);
                lblId.Text = "";
                txtDescripcion.Text = "";
                llenarDatos();
                await DisplayAlert("Aviso", "Registro Eliminado Exitosamente", "Ok");
                lblId.IsVisible = false;
                //btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnGuardar.IsVisible = true;
            }
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
