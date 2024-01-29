
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    internal class ConvertisseurEuroViewModel: ObservableObject
    {
        private ObservableCollection<Devise> devises;
        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set
            {
                devises = value;
                OnPropertyChanged();
            }
        }



        private Double montantEuro;
        public Double MontantEuro
        { //Property Resultat
            get { return montantEuro; }
            set
            {
                montantEuro = value;
                OnPropertyChanged();
            }
        }

        private Double resultat;
        public Double Resultat
        { //Property Resultat
            get { return resultat; }
            set
            {
                resultat = value;
                OnPropertyChanged();
            }
        }



        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7228/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");

            if (result == null)
            {
                await MessageAsync("API non disponible !", "Erreur");
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        private async Task MessageAsync(string message, string titre)
        {
            ContentDialog contentDialog = new ContentDialog
            {
                Title = titre,
                Content = message,
                CloseButtonText = "Ok"
            };
            await contentDialog.ShowAsync();
        }


    }
}
