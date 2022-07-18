
//C'est elle qui nous permet de parvenir aux différentes fonctions de l'application

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GestionBancaire
{
    /// <summary>
    /// La classe Apllication
    /// </summary>
      internal class Application
    {
        private Choix choix;
        private Argent argent;

        /// <summary>
        /// Un constructeur par defaut qui initialise le choix et l'argent
        /// </summary>
          public Application()
        {
            this.choix = 0;
            this.argent = new Argent();
        }

        /// <summary>
        /// Appelle la methode Demarrer() pour lancer le programme
        /// </summary>
          public void Run()
        {
            Demarrer();
            
        }

        /// <summary>
        /// Demarre l'application
        /// </summary>
          private void Demarrer()
        {
            Console.Clear();
            Console.WriteLine("Bienvenue à notre banque");
            Console.WriteLine("1- Se connecter à votre compte\n2- Créer un compte\n3- Quitter");
           
            bool pass = true;

            do
            {
                Console.Write("> ");
                Choix.TryParse(Console.ReadLine(), out this.choix);

                switch (choix)
                {
                    case Choix.premier:
                        pass = false;
                        Session();
                        break;

                    case Choix.deuxieme:
                        CreationCompte();
                        pass = false;
                        break;

                    case Choix.troisieme:
                        pass = false;
                        break;

                    default:
                        Console.WriteLine("Veuillez faire un choix valide!");
                        break;
                }

            } while (pass);
        }


        /// <summary>
        /// Creation d'un client
        /// </summary>
        /// <returns></returns>
          private Client CreationClient()
        {
            string firstname = "";
            string lastname = "";
            string address = "";
            int age = 0;

            do
            {
                Console.Write("Veuillez entrez votre prénom: ");
                firstname = Console.ReadLine();

                if (firstname.Length == 0)
                {
                    Console.WriteLine("Votre Prénom est trop court");
                }

            } while (firstname.Length == 0);


            do
            {
                Console.Write("Veuillez entrez votre nom: ");
                lastname = Console.ReadLine();

                if (lastname.Length == 0)
                {
                    Console.WriteLine("Votre nom est trop court");
                }

            } while (lastname.Length == 0);


            do
            {
                Console.Write("Veuillez entrez votre adresse: ");
                address = Console.ReadLine();

                if (address.Length == 0)
                {
                    Console.WriteLine("Votre adresse est trop court");
                }

            } while (address.Length == 0);


            do
            {
                Console.Write("Veuillez entrez votre age: ");
                int.TryParse(Console.ReadLine(), out age);

                if (!Client.CheckAge(age))
                {
                    Console.WriteLine("Vous devez avoir minimum 16 ans");
                }

            } while (!Client.CheckAge(age));

            return new Client(lastname, firstname, address, age);
        }


        /// <summary>
        /// Creation d'un compte
        /// </summary>
          private void CreationCompte()
        {
            Console.Clear();
            Client client = CreationClient();
            string password = "password";
            string username = "username";

            Console.Clear();
            Console.WriteLine($"Bienvenue Mr/Mrs: {client.Nom} {client.Prenom}, Vous êtes maintenant client de notre banque.");
            Console.WriteLine("Nous allons procéder à la création de votre compte...\n");

            do
            {
                Console.Write("Veuillez entrez un nom d\'utilisateur: ");
                username = Console.ReadLine();
            } while (CompteClient.UsernameExist(username));

            do
            {

                Console.Write("Veuillez entrez votre mot de passe: ");
                password = Console.ReadLine();

                if (!CompteClient.CheckPassword(password))
                {
                    Console.WriteLine("Veillez respecter les normes svp!");
                }

            } while (!CompteClient.CheckPassword(password));

            CompteClient cp = new CompteClient(username, password, client);
            cp.Sauvegarder();
            
            Console.WriteLine($"\nM./Mme {client.Nom} {client.Prenom}, votre compte vient d'etre créer avec succès.");
            Console.WriteLine("Vous pouvez maintenant vous connecter afin d'effectuer des operations\nAppuyez sur une touche pour continuer...\n");
            Console.ReadKey();
            Session();
        }


        /// <summary>
        /// Ouverture de session
        /// </summary>
          private void Session()
        {
            Console.Clear();
            string username = "";
            string password = "";
            int compteur = 0;

            Console.WriteLine("Ouverture d'une session, Taper [ q ] pour retourner\n");

            do
            {
                Console.Write("Nom d'utilisateur: ");
                username = Console.ReadLine();

                if (username.Equals("q"))
                {
                    password = "q";
                    break;
                }

                if (!CompteClient.UsernameExist(username))
                {
                    Console.WriteLine("Le nom d\'utilisateur est incorrecte!");
                }

            } while (!CompteClient.UsernameExist(username));

            if (!username.Equals("q"))
            {
                do
                {
                    Console.Write("Mot de passe: ");
                    password = Console.ReadLine();

                    if (password.Equals("q"))
                    {
                        break;
                    }

                    if (!CompteClient.GetPassword(username, password))
                    {
                        compteur++;
                        Console.WriteLine($"Vous avez tapé un mot de passe incorrecte...\nTENTATIVE [{compteur}/3]");
                    }

                } while (!CompteClient.GetPassword(username, password) && compteur != 3);
            }

            if(!username.Equals("q") && !password.Equals("q"))
            {
                if (CompteClient.GetPassword(username, password) == false && compteur == 3)
                {
                    Console.WriteLine("Vous avez une limite de 3 tentatives\n" +
                        "Veuillez reessayer plus tard merci!...");
                    Console.ReadKey();
                    Console.Clear();
                    Demarrer();
                }

                else
                {
                    OuvrirSession(username);
                }
            }

            else
            {
                Demarrer();
            }
        }

        /// <summary>
        /// Ouverture de l'accueil de la banque
        /// </summary>
        /// <param name="username"></param>
          private void OuvrirSession(string username)
        {
            Console.Clear();
            Client client = CompteClient.GetClient(username);
            Console.WriteLine($"Bienvenu M./Mme {client.Prenom} {client.Nom} ");
            Console.WriteLine($"Solde de votre compte : {CompteClient.GetSolde(username)}$");
            MenuCompte(username, client);

        }


        /// <summary>
        /// Menu de transaction de la banque
        /// </summary>
        /// <param name="username"></param>
        /// <param name="client"></param>
          private void MenuCompte(string username, Client client)
        {
           
            bool pass = true;
            Console.WriteLine("1- Transaction");
            Console.WriteLine("2- Historique");
            Console.WriteLine($"3- Se déconnecter du compte");

            do 
            {
                Console.Write("> ");
                Choix.TryParse(Console.ReadLine(), out this.choix);

                switch (this.choix)
                {
                    case Choix.premier:
                        MenuTransaction(username, client);
                        pass = false;
                        break;

                    case Choix.deuxieme:
                        Historique(username);
                        pass = false;
                        break;

                    case Choix.troisieme:
                        if(Confirmation("Se déconnecter du compte"))
                        {
                            Session();   
                        }

                        else
                        {
                            OuvrirSession(username);
                        }

                        pass = false;
                        break;

                    default:
                        break;
                }

            } while (pass);
        }

       /// <summary>
       /// Affiche le menu des transaction possible
       /// </summary>
       /// <param name="username"></param>
       /// <param name="client"></param>
         private void MenuTransaction(string username, Client client)
        {
            Console.Clear();
            bool pass = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Transaction.\n");
                Console.WriteLine("1. Dépôt");
                Console.WriteLine("2. Retrait");
                Console.WriteLine("3. Interac");
                Console.WriteLine("4. Retour");
                Console.Write("> ");
                Choix.TryParse(Console.ReadLine(), out choix);

                switch (choix)
                {
                    case Choix.premier:
                        Depot(username);
                        break;

                    case Choix.deuxieme:
                        Retrait(username);
                        break;

                    case Choix.troisieme:
                        Envoyer(username);
                        break;

                    case Choix.quatrieme:
                        OuvrirSession(username);
                        pass = false;
                        break;

                    default:
                        break;
                }

            } while (pass);
        }

        /// <summary>
        /// Affiche  l'historique 
        /// </summary>
        /// <param name="username"></param>
          private void Historique(string username)
        {
            Console.Clear();
            String choix = "";
            string path = "../../../database/comptes/journal/"+ username +".log";
            String content = Sauvegarde.Lecture(path, username);
            Console.WriteLine(content);

            do
            {
                Console.Write("Veuillez taper [q] pour sortir: ");
                choix = Console.ReadLine().ToLower();
            } while (choix != "q");

            if(choix == "q")
            {
                OuvrirSession(username);
            }
        }

        /// <summary>
        /// Affiche le menu pour effectuer un depot
        /// </summary>
        /// <param name="username"></param>
        private void Depot(string username)
        {
            Console.Write(" Quel montant souhaitez vous deposer? ");
            argent.ReadLine();

            Console.WriteLine($"    [ Votre solde avant le depôt     : { CompteClient.GetSolde(username) }$CAD ]");
            Console.WriteLine($"    [ Vous avez effectué un depôt de : { argent }$CAD  ]");
            CompteClient.Depot(username, argent);
            Console.WriteLine($"    [ Votre solde actuel est de      : { CompteClient.GetSolde(username) }$CAD ]");
            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche les informations pour effectuer un retrait
        /// </summary>
        /// <param name="username"></param>
        private void Retrait(string username)
        {
            Console.Write(" Quel montant souhaitez vous retirer? ");
            argent.ReadLine();

            if (CompteClient.GetSolde(username) > argent)
            {
                Console.WriteLine($"    [ Votre solde avant le retrait     : {CompteClient.GetSolde(username)}$CAD ]");
                Console.WriteLine($"    [ Vous avez effectué un retrait de : {argent}$CAD  ]");
                CompteClient.Retrait(username, argent);
                Console.WriteLine($"    [ Votre solde actuel est de        : { CompteClient.GetSolde(username) }$CAD ]");
            }

          
            else
            {
                Console.WriteLine("Votre solde est insufisant!");

            }
            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadLine();

        }

        /// <summary>
        /// Afiche les informations 
        /// </summary>
        /// <param name="username"></param>
          private void Envoyer(string username)
        {
            string friend = "";
     

            Console.WriteLine("Envoyer de l'argent par Interac aux autres utilisateurs.");
            Console.WriteLine("\nVeuillez s'il vous plait entrer le montant.");
            Console.WriteLine("Si le montant comprend un nombre à virgule, n'utilisez pas un [.] à la place.");
            Console.Write("Veuillez taper le montant [q] pour sortir: ");
            
            if (argent.ReadLine())
            {
                if (argent < CompteClient.GetSolde(username))
                {
                    Console.WriteLine("Maintenant vous devez recherche utilisateur qui recevra l'argent.");
                    Console.WriteLine("Utilisateur doit detenir un compte et vous devez utiliser son USERNAME.");
                    do
                    {
                        Console.Write("Taper la recherche [Username] ou [q] pour sortir: ");
                        friend = Console.ReadLine();
                        if (friend == "q")
                        {
                            break;
                        }

                        if (!CompteClient.UsernameExist(friend))
                        {
                            Console.WriteLine("Utilisateur introuvable!");
                        }


                    } while (!CompteClient.UsernameExist(friend));

                    if (friend != "q")
                    {
                        if(friend != username)
                        {
                            if (Confirmation("la transaction"))
                            {
                                CompteClient.Interac(username, friend, argent);
                                Console.WriteLine($"La transaction au compte {friend} est effectué!");
                            }

                            else
                            {
                                Console.WriteLine($"La transaction au compte {friend} est annulé!");
                            }
                        }

                        else
                        {
                            Console.WriteLine("Vous ne pouvez pas vous envoyer de l'argent à vous même.");
                        }
                    }
                }

                else
                {
                    Console.WriteLine("Le solde de votre compte est insuffisant!");
                }
            }

            else
            {
                Console.WriteLine("Soit une erreur est survenue ou vous avez taper [q]!...");
            }
            Console.ReadKey();
        }

        private bool Confirmation(string action)
        {
            string confirmation;

            do
            {
                Console.Write($"Confirmez-vous {action}? [o/n]: ");
                confirmation = Console.ReadLine().ToLower();

            } while (confirmation != "o" && confirmation != "n");

            return confirmation == "o";
        }

        /// <summary>
        /// Enum de choix de l'utilisateur
        /// </summary>
          private enum Choix : int
        {
            premier = 1,
            deuxieme,
            troisieme,
            quatrieme
        } 
    }
}
