
//Le solde de l'utilisateur actif

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionBancaire
{
    internal class Argent
    {
        private double montant;

        public Argent()
        {
            this.montant = 0;
        }

        public Argent(double montant)
        {
            this.montant = Math.Round(montant,2);
        }

        public double Montant { get => montant; set => montant = Math.Round(value, 2); }

        public static Argent operator +(Argent gauche, Argent droite)
        {
            return new Argent(Math.Round(gauche.Montant + droite.Montant, 2));
        }

        public static Argent operator +(Argent gauche, double montant)
        {
            return new Argent(gauche.Montant + montant);
        }

        public static Argent operator +(double montant, Argent droite)
        {
            return new Argent(montant + droite.Montant);
        }

        public static Argent operator -(Argent gauche, Argent droite)
        {
            return new Argent(Math.Round(gauche.Montant - droite.Montant, 2));
        }

        public static Argent operator -(Argent gauche, double montant)
        {
            return new Argent(gauche.Montant - montant);
        }

        public static Argent operator -(double montant, Argent droite)
        {
            return new Argent(montant - droite.Montant);
        }

        public static bool operator ==(Argent gauche, Argent droite)
        {
            return gauche.Montant == droite.Montant;
        }

        public static bool operator !=(Argent gauche, Argent droite)
        {
            return !(gauche == droite);
        }

        public static bool operator <(Argent gauche, Argent droite)
        {
            return gauche.Montant < droite.Montant;
        }

        public static bool operator >(Argent gauche, Argent droite)
        {
            return gauche.Montant > droite.Montant;
        }

        /// <summary>
        /// Surcharge de l'operateur INFERIEUR OU EGAL sur 
        /// deux objets de type Argent
        /// </summary>
        /// <param name="gauche"></param>
        /// <param name="droite"></param>
        /// <returns>Boolean</returns>
        public static bool operator <=(Argent gauche, Argent droite)
        {
            return gauche.Montant <= droite.Montant;
        }

        /// <summary>
        /// Surcharge de l'operateur SUPERIEUR OU EGAL sur
        /// deux objets de type Argent
        /// </summary>
        /// <param name="gauche"></param>
        /// <param name="droite"></param>
        /// <returns>Boolean</returns>
        public static bool operator >=(Argent gauche, Argent droite)
        {
            return gauche.Montant >= droite.Montant;
        }

        /// <summary>
        /// Une methode qui permet de recuperer un montant.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool ReadLine()
        {
            bool check = double.TryParse(Console.ReadLine(), out montant);
            montant = Math.Round(montant, 2);
            return check;
        }

        /// <summary>
        /// Permet de retourner le montant.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return Montant.ToString();
        }

    }
}
