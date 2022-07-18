using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionBancaire
{
    /// <summary>
    /// La class Client nous permet d'entrer les données personnels de l'utilisateur
    /// </summary>
    internal class Client : Personne
    {

        private string _adresse;

        /// <summary>
        /// Constructeur sans paramettre.
        /// </summary>
        public Client():base()
        {
            Adresse = String.Empty;
        }
        /// <summary>
        /// Constructeur avec paramettre
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="adresse"></param>
        /// <param name="age"></param>
        public Client(string nom, string prenom, string adresse, int age):base(nom, prenom,  age)
        {
            Adresse= adresse;
        }

        public string Adresse { get => _adresse; set => _adresse = value; }

        /// <summary>
        /// Une methode permettant de retourner l'objet.
        /// </summary>
        /// <returns>this.toString</returns>
        public override string ToString()
        {
            return ($"Nom : {Nom} \nPrenom: {Prenom}\nAdresse : {Adresse}\nAge: {Age}");
        }
    }
}
