using ClientConvertisseurV1.Models;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WSConvertisseur.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private ObservableCollection<Devise> devises;
        public ObservableCollection<Devise> Devises { 
            get { return devises; }
            set { 
                devises = value;
                OnPropertyChanged(nameof(Devises));
            }
        }

        

        private Double montantEuro;
        public Double MontantEuro
        { //Property Resultat
            get { return montantEuro; }
            set
            {
                montantEuro = value;
                OnPropertyChanged(nameof(MontantEuro));
            }
        }

        private Double resultat;
        public Double Resultat
        { //Property Resultat
            get { return resultat; }
            set
            {
                resultat = value;
                OnPropertyChanged(nameof(Resultat));
            }
        }


        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7228/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");

            if (result == null)
            {
                MessageAsync("API non disponible !", "Erreur");
            }
            else
            {
                Devises= new ObservableCollection<Devise>(result);
            }
        }

        private async void MessageAsync(string message, string titre)
        {
            //ContentDialog contentDialog = new ContentDialog
            //{
            //    Title = titre,
            //    Content = message,
            //    CloseButtonText = "Ok"
            //};
            //contentDialog.XamlRoot = this.Content.XamlRoot;
            //await contentDialog.ShowAsync();
        }

        private void BtConvertir_Click(object sender, RoutedEventArgs e)
        {
            Devise d = (Devise)CbDevise.SelectedItem;
            if (d == null)
            {
                throw new ArgumentException("aucune devise selectionné");
            }
            Resultat = MontantEuro * d.Taux;
        }
    }
}
