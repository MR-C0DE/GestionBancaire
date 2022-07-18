using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionBancaire
{
    /// <summary>
    /// La classe Journal
    /// </summary>
      internal class Journal
    {
        private string _message;

        /// <summary>
        /// Un Constructeur par defaut
        /// </summary>
          public Journal()
        {
            _message = String.Empty;
        }

        /// <summary>
        /// Pour lire l'historique de trasaction du client
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Historique de transacrtion</returns>
          private string Lecture(string username)
        {
            string FileToRead = "../../../database/comptes/journal/" + username + ".log";
            string[] lines = File.ReadAllLines(FileToRead);
            String message = "";
            foreach (string line in lines) {
                message += line + "\n";
            }
            return message;
        }

        /// <summary>
        /// Ajoute L'action de la creation du compte a l'historique du client
        /// </summary>
        /// <param name="username"></param>
          public void CreationCompte(string username)
        {
            _message = $"[ {DateTime.Now} ] - Action : Creation du compte";
            Sauvegarde.Log(username, _message);
        }

        /// <summary>
        ///Ajoute Les depots dans L'historique du client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="argent"></param>
          public void Depot(string username, Argent argent)
        {
            _message = Lecture(username);
            _message += $"[ {DateTime.Now} ] - Action : Depôt - Montant [ {argent}$CAD ] ";
            Sauvegarde.Log(username, _message);
        }

        /// <summary>
        /// Ajoute les retraits dans l'historique du client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="argent"></param>
          public void Retrait(string username, Argent argent)
        {
            _message = Lecture(username);
            _message += $"[ {DateTime.Now} ] - Action : Retrait - Montant [ {argent}$CAD ] ";
            Sauvegarde.Log(username, _message);
        }

        /// <summary>
        /// Ajoute les fonds envoyés par l'utilisateur dans son historique
        /// </summary>
        /// <param name="username"></param>
        /// <param name="friend"></param>
        /// <param name="argent"></param>
          private void Envoi(string username, string friend, Argent argent)
        {
            _message = Lecture(username);
            _message += $"[ {DateTime.Now} ] - Action : Envoi - DESTINATEUR: [ {friend} ] - Montant [ {argent}$CAD ]  ";
            Sauvegarde.Log(username, _message);
        }

        /// <summary>
        /// Ajoute les fons recus dans l'historique de l'utilisateur
        /// </summary>
        /// <param name="username"></param>
        /// <param name="friend"></param>
        /// <param name="argent"></param>
        private void Recoit(string username, string friend, Argent argent)
        {
            _message = Lecture(friend);
            _message += $"[ {DateTime.Now} ] - Action : Recoit - EXPEDITEUR: [ {username} ] - Montant [ {argent}$CAD ]  ";
            Sauvegarde.Log(friend, _message);
        }

        /// <summary>
        /// Ajoute le nom de L'expediteur ou du destinateur a l'historique du client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="friend"></param>
        /// <param name="argent"></param>
        public void Interac(string username, string friend, Argent argent)
        {
            Envoi(username, friend, argent);
            Recoit(username, friend, argent);
        }

    }
}
