﻿
using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurDeviseViewModel: ObservableObject
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



        private Double montantDevise;
        public Double MontantDevise
        { //Property Resultat
            get { return montantDevise; }
            set
            {
                montantDevise = value;
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

        private Devise selectedDevise;
        public Devise SelectedDevise
        { //Property Resultat
            get { return selectedDevise; }
            set
            {
                selectedDevise = value;
                OnPropertyChanged();
            }
        }

        public IRelayCommand BtnSetConversion { get; }
        public ConvertisseurDeviseViewModel()
        {
            GetDataOnLoadAsync();
            //Boutons
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }
        public async void ActionSetConversion()
        {
            Devise d = SelectedDevise;
            if (d == null)
                await MessageAsync("Il faut selectionner une devise !", "Erreur");
            else
                Resultat = MontantDevise / d.Taux;

        }

        public async void GetDataOnLoadAsync()
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
            contentDialog.XamlRoot = App.MainRoot.XamlRoot;
            await contentDialog.ShowAsync();
        }
    }
}
