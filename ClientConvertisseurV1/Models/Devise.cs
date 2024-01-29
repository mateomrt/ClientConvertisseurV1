using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {
        private int id;
        private string? nomDevise;
        private double taux;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string? NomDevise
        {
            get { return nomDevise; }
            set { nomDevise = value; }
        }
        public double Taux
        {
            get { return taux; }
            set { taux = value; }
        }
        public Devise(int id, string? nomDevise, double taux)
        {
            this.Id = id;
            this.NomDevise = nomDevise;
            this.Taux = taux;
        }
        public Devise()
        {




        }

        public override bool Equals(object? obj)
        {
            return obj is Devise devise &&
                   Id == devise.Id &&
                   NomDevise == devise.NomDevise &&
                   Taux == devise.Taux;
        }
    }
}
