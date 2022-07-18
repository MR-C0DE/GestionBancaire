using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionBancaire
{
       /// <summary>
       /// La classe Compteclient
       /// </summary>
         internal class CompteClient
    {
 
        private string _username;
        private string _password;
        private double _solde;
        private Client client;
        private Journal journal;

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public double Solde { get => _solde; set => _solde = value; }

        /// <summary>
        /// Un constructeur qui initialise tous les atributs
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="client"></param>
          public CompteClient(string username, string password, Client client)
        {
           
            Username = username;
            Password = password;
            this.client = client;
            journal = new Journal();
            
        }

        /// <summary>
        /// Verifie que le username existe dans le database
        /// </summary>
        /// <returns>True si le username existe et false pour le contraire</returns>
        public static bool UsernameExist(string username)
        {
            string path = "../../../database/clients/" + username + ".txt";
            return File.Exists(path);
        }
        /// <summary>
        /// Verifie que le mot de passe repect les normes de la banque
        /// </summary>
        /// <param name="password"></param>
        /// <returns>True si le password répond aux critères et false pour le contraire</returns>
        public static bool CheckPassword(string password)
        {
            int speciaux = 0;
            int upper = 0;
            if(password.Length < 8)
            {
                return false;
            }

            foreach(char ch in password)
            {
                if (ch >= 33 && ch <= 64)
                {
                    speciaux++;
                }
            }

            foreach (char ch in password)
            {
                if (ch >= 65 && ch <= 90)
                {
                    upper++;
                }
            }

            if (speciaux == 4 && upper >= 1)
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Vérifie que le mot de passe que l'utilisateur a utilisé pour se connecter est correct
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true si le mot de passe est correct et false pour le contraire</returns>
          public static bool GetPassword(string username, string password)
        {
            string FileToRead = "../../../database/comptes/authentification/" + username + ".txt";
            string[] lines = File.ReadAllLines(FileToRead);
            if (lines[0].Equals(password))
                return true;
            return false;
        }

        /// <summary>
        /// Obtenir le client a patir du nom du fichier dans le database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Le client et ses données personnelles</returns>
          public static Client GetClient(string username)
        {
            string FileToRead = "../../../database/clients/" + username + ".txt";
            string[] lines = File.ReadAllLines(FileToRead);
            for(int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Split(": ")[1];
            }
            return new Client(lines[0], lines[1], lines[2], int.Parse(lines[3]));
        }

        /// <summary>
        ///Methode pour avoir le solde d'un client
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Le solde du compte</returns>
          public static Argent GetSolde(string username)
        {
            double montant = 0; 
            string FileToRead = "../../../database/comptes/solde/" + username + ".txt";
            string[] lines = File.ReadAllLines(FileToRead);
            montant = Convert.ToDouble(lines[0]);
            return new Argent(montant);
        }

        /// <summary>
        /// Pour déposer de l'argent dans le compte d'un client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="argentEntrer"></param>
          public static void Depot(string username, Argent argentEntrer)
        {
            Journal journal = new Journal();
            Argent argent = CompteClient.GetSolde(username);
            argent += argentEntrer;
            Sauvegarde.Solde(username, argent);
            journal.Depot(username, argentEntrer);
        }

        /// <summary>
        /// Pour faire un retrait dans le compte d'un client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="argentEntrer"></param>
          public static void Retrait(string username, Argent argentEntrer)
        {
            Journal journal = new Journal();
            Argent argent = CompteClient.GetSolde(username);
            argent  -= argentEntrer;
            Sauvegarde.Solde(username, argent);
            journal.Retrait(username, argentEntrer); 
        }

        /// <summary>
        /// Pour envoyez de l'argent a un autre client de la banque
        /// </summary>
        /// <param name="username"></param>
        /// <param name="friend"></param>
        /// <param name="argentEntrer"></param>
          public static void Interac(string username, string friend, Argent argentEntrer)
        {
            Journal journal = new Journal();
            Argent argent = CompteClient.GetSolde(username);
            Argent argentFriend = CompteClient.GetSolde(friend);
            argent -= argentEntrer;
            argentFriend += argentEntrer;
            Sauvegarde.Solde(friend, argentFriend);
            Sauvegarde.Solde(username, argent);
            journal.Interac(username, friend, argentEntrer);

        }

        /// <summary>
        /// Pour S'auvegarder un client dans notre database
        /// </summary>
          public void Sauvegarder()
        {
            Sauvegarde.Client(Username, client);
            Sauvegarde.Compte(Username, Password);
            Sauvegarde.Solde(Username, new Argent());
            Sauvegarde.Liste(Username);
            journal.CreationCompte(Username);
        }
    }
}
