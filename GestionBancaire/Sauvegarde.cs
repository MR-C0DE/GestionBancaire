using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionBancaire
{
    static class Sauvegarde
    {


        /// <summary>
        /// Fait la lecture de l'historique du client dans le database
        /// </summary>
        /// <param name="path"></param>
        /// <param name="username"></param>
        /// <returns>L'historique du client</returns>
          public static string Lecture(string path, string username)
        {
            string FileToRead = path;
            string[] lines = File.ReadAllLines(FileToRead);
            String message = "";
            foreach (string line in lines)
            {
                message += line + "\n";
            }
            return message;
        }

        /// <summary>
        /// Sauvegarde la liste des clients pour une ultérieure mise a jour sans perte de données
        /// </summary>
        /// <param name="username"></param>
          public static void Liste(string username)
        {
            string path = "../../../database/clients/.sys/list.txt";
            String content = Lecture(path, username);
            content += username;
            File.WriteAllText(path, content);
        }

        /// <summary>
        /// Sauvegarde le client dans le database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="client"></param>
          public static void Client(string username, Client client)
        {
            string path = "../../../database/clients/" + username + ".txt";
            File.WriteAllText(path, client.ToString());
        }

        /// <summary>
        /// Sauvegarde un compte dans le database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Compte(string username, string password)
        {
            string path = "../../../database/comptes/authentification/" + username + ".txt";
            File.WriteAllText(path, password);
        }


        /// <summary>
        /// Sauvegarde le solde d'un client dans le database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="argent"></param>
          public static void Solde(string username, Argent argent)
        {
            string path = "../../../database/comptes/solde/" + username + ".txt";
            File.WriteAllText(path,  argent.ToString());
        }

        /// <summary>
        /// Sauvegarde l'historique des transactions d'un client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
          public static void Log(string username, string message)
        {
            string path = "../../../database/comptes/journal/" + username + ".log";
            File.WriteAllText(path, message);
        }
    }
}
