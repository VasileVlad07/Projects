using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaAmenzi
{
    public static class Meniu 
    {
        private static string saveFile = "DateSalvate.bin";

        static AppLogic appLogic = new AppLogic();

        private static bool input = true;

        private static string backInput;

        public static void DisplayMeniu()
        {
            Console.WriteLine("<<-MENIU->>");
            
            Console.WriteLine();

            while (true && input)
            {
                Console.WriteLine("Selectati una din optiunile de mai jos: ");

                Console.WriteLine();

                Console.WriteLine(">> 1. Adauga agent");
                Console.WriteLine(">> 2. Sterge agent");
                Console.WriteLine(">> 3. Adauga amenda");
                Console.WriteLine(">> 4. Afisaza amenzi agent");
                Console.WriteLine(">> 5. Afisaza amenzi contravenient");
                Console.WriteLine(">> 6. Afisaza situatie amenzi");
                Console.WriteLine(">> 7. Iesire");

                Console.WriteLine();

                string newInput = Console.ReadLine();
                byte number;

                while (!byte.TryParse(newInput, out number))
                {
                    Console.WriteLine("Nu ati introdus un numar!");
                    newInput = Console.ReadLine();
                }

                switch (number)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("--------ADAUGARE AGENT--------");
                        Console.WriteLine(">> 0. Revenire Ecran Anterior");
                        Console.WriteLine();
                        appLogic.AddAgent();
                        input = false;
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("--------STERGE AGENT--------");
                        Console.WriteLine();
                        appLogic.DeleteAgent();
                        input = false;
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("--------ADAUGARE AMENDA--------");
                        Console.WriteLine(">> 0. Revenire Ecran Anterior");
                        Console.WriteLine();
                        appLogic.AddTicket();
                        input = false;
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("--------AFISAZA AMENZI AGENT--------");
                        Console.WriteLine();
                        appLogic.DisplayAgentFines();
                        input = false;
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("--------AFISAZA AMENZI CONTRAVENIENT--------");
                        Console.WriteLine();
                        appLogic.DriverFines();
                        input = false;
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("--------AFISAZA SITUATIE AMENZI--------");
                        Console.WriteLine();
                        appLogic.DisplayFines();
                        input = false;
                        break;
                    case 7:
                        Console.Clear();
                        SaveData.Save(appLogic, saveFile);
                        Console.WriteLine("Modificarile au fost salvate!");
                        Console.WriteLine("EXIT");
                        Environment.Exit(0);
                        input = false;
                        break;
                    default:
                        Console.WriteLine("Ati introdus un numar gresit!");
                        Console.WriteLine("Va rugam introduceti un numar intre 1 si 7!");
                        Console.WriteLine();
                        input = true;
                        break;
                }
            }
        }

        public static void Return()
        {
            Console.WriteLine(">> 0. Revenire Ecran Anterior");

            backInput = Console.ReadLine();
            byte back;

            while (byte.TryParse(backInput, out back) == false || back != 0)
            {
                Console.WriteLine("Input incorect, pentru BACK apasati TASTA 0");
                backInput = Console.ReadLine();
            }

            if (back == 0)
            {
                DisplayOptions();  
            }
        }

        public static void DisplayOptions()
        {
            Console.Clear();
            Meniu.DisplayMeniu();
        }

        public static void LoadData()
        {
            appLogic.Agents.Clear();
            appLogic.Tickets.Clear();
            appLogic.DriversListName.Clear();
            appLogic.VirtualAgents.Clear();

            appLogic = SaveData.Load(saveFile);
        }

    }
}
