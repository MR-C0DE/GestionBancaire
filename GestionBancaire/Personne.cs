using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionBancaire
{
    internal class Personne
    {
        private string _nom;
        private string _prenom;
        private int _age;

        /// <summary>
        /// Un constructeur par defaut.
        /// </summary>
        public Personne()
        {
            _nom = String.Empty;
            _prenom = String.Empty;
            _age = 0;
        }

        /// <summary>
        /// Un Constructeur qui initilise chaque attribut de this
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="age"></param>
        public Personne(string nom, string prenom, int age)
        {
            Nom = nom;
            Prenom = prenom;
            Age = age;
        }

        /// <summary>
        /// Les Getters et Setters
        /// </summary>
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public int Age { get => _age; set => _age = value; }

        /// <summary>
        /// Une methode permettant de verifier l'age de l'utilisateur.
        /// </summary>
        /// <param name="age"></param>
        /// <returns>bool</returns>
        public static bool CheckAge(int age)
        {
            if (age <= 15) return false;
            else if (age >= 120) return false;
            return true;
        }
        /// <summary>
        /// Une methode permettant d'afficher l'objet
        /// </summary>
        public void Afficher()
        {
            Console.WriteLine($"Nom : {Nom} \nPrenom: {Prenom}\nAge: {Age}");
        }


        /// <summary>
        /// Une methode permettant de retourner l'objet.
        /// </summary>
        /// <returns>this.toString</returns>
        public virtual string ToString()
        {
            return ($"Nom : {Nom} \nPrenom: {Prenom}\nAge: {Age}");
        }

    }
}
