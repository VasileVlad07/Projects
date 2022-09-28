using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaAmenzi

{
    [Serializable]
    public class AppLogic
    {
        private string input;
        private string driverName;
        private bool check = false;
        private bool enter = true;
        private byte select;
        private int selectAgent;
        private uint fine = 0;
        private uint totalFines = 0;

        Ticket ticket;

        List<Agent> agents;
        List<Ticket> tickets;
        List<string> driversListName;
        List<VirtualAgent> virtualAgent;

        public AppLogic()
        {
            agents = new List<Agent>();
            tickets = new List<Ticket>();
            driversListName = new List<string>();
            virtualAgent = new List<VirtualAgent>();
        }
         
        public List<Agent> Agents { get { return agents; } }
        public List<Ticket> Tickets { get { return tickets; } }
        public List <string> DriversListName { get { return driversListName; } }
        public List<VirtualAgent> VirtualAgents { get { return virtualAgent; } }

        
        public void AddAgent()
        {
            Console.WriteLine("Introduceti numele agentului: ");

            string name = Console.ReadLine();

            Console.WriteLine();

            if (name == "0")
                Meniu.DisplayOptions();

            while (name.Any(x => Char.IsDigit(x)) || string.IsNullOrEmpty(name))  // Validare nume corect agent
            {
                Console.WriteLine("Nu ati introdus un nume corect, incercati din nou..");
                name = Console.ReadLine();
            }

            int agentID = RandomNumbers.GetNext();

            while (agents.Exists(a => a.Code == agentID))  // Validare cod unic agent
                agentID = RandomNumbers.GetNext();

            Console.WriteLine("Agentului i s-a atribuit urmatorul cod unic: " + agentID);

            Console.WriteLine();

            Console.WriteLine("Noul agent este " + name + " si are codul " + agentID);
            Console.WriteLine();

            agents.Add(new Agent(agentID, name));

            Meniu.Return();
        }

        private void DisplayAgents()
        {
            Console.WriteLine("Lista agenti disponibili: ");
            Console.WriteLine();

            foreach (Agent agent in agents)
            {
                Console.WriteLine("Agentul: " + agent.Name + " -> codul agentului: " + agent.Code);
            }

            Console.WriteLine();

            if(agents.Count == 0)
               Meniu.Return();
        }

        public void DeleteAgent()
        {
            DisplayAgents();

            Console.WriteLine(">> 0. Revenire Ecran Anterior");
            Console.WriteLine();
            Console.WriteLine("Selectati codul agentului de sters: ");
            Console.WriteLine();

            input = Console.ReadLine();
            int inputNum;

            if (input == "0")
            {
                Meniu.DisplayOptions();
            }
            else
            {
                while (!int.TryParse(input, out inputNum) || !agents.Exists(a => a.Code == inputNum))
                {
                    Console.WriteLine("Input invalid, incercati din nou!");
                    input = Console.ReadLine();
                }

                MoveFinesToVirtualAgent(inputNum);

                Console.WriteLine();
                Console.WriteLine("TASTA 1 -> Amenzi agent virtual: ");
                Console.WriteLine(">> 0. Revenire Ecran Anterior");
                Console.WriteLine();

                int newInput = int.Parse(Console.ReadLine());

                if (newInput == 1)
                {
                    Console.WriteLine("Amenzi agent virtual:");

                    foreach (VirtualAgent vAgent in virtualAgent)
                    {
                        Console.WriteLine("Categorie: " + vAgent.category + " -> " + vAgent.Fine + " RON");
                    }
                }
                else if (newInput == 0)
                {
                    Console.Clear();
                    Meniu.DisplayMeniu();
                }
            }

            Console.WriteLine();
            Meniu.Return();

        }

        public void MoveFinesToVirtualAgent(int input)
        {
            var agentsAndTickets = agents.Select(a => tickets.FindAll(t => t.AgentID == a.Code));

            if (agents.Exists(a => a.Code == input))
            {
                int agentIndex = agents.FindIndex(x => x.Code == input);

                foreach (var list in agentsAndTickets)
                {
                    foreach (var i in list)
                    {
                        if (input == i.AgentID)
                        {
                            Console.WriteLine("Amenzile agentului cu codul " + input + " au fost mutate in contul agentului virtual: " + i.category + ": " + i.Fine + " RON");
                            virtualAgent.Add(new VirtualAgent(i.category, i.Fine));
                        }
                    }
                }
                agents.RemoveAt(agentIndex);
                Console.WriteLine("Agentul cu codul " + input + " a fost sters!");
            }
        }

        public void AddTicket()
        {
            Console.WriteLine("Introduceti numele soferului: ");

            driverName = Console.ReadLine();

            if (driverName == "0")
                Meniu.DisplayOptions();

            while (driverName.Any(x => Char.IsDigit(x)) || string.IsNullOrEmpty(driverName))
            {
                Console.WriteLine("Input invalid, va rugam introduceti numele soferului..");
                driverName = Console.ReadLine();
            }

            Console.WriteLine();

            while (true && !check)
            {

                Console.WriteLine("Selectati categoria: ");
                Console.WriteLine();

                Console.WriteLine("1." + Category.Bicycle);
                Console.WriteLine("2." + Category.MopedMotorcycle);
                Console.WriteLine("3." + Category.Car);
                Console.WriteLine("4." + Category.Truck);
                Console.WriteLine("5." + Category.Tractor);
                Console.WriteLine();

                input = Console.ReadLine();

                while (!byte.TryParse(input, out select))
                {
                    Console.WriteLine("Nu ati introdus un numar!");
                    input = Console.ReadLine();
                }

                switch (select)    // Aici adaugam categoria si amenda in functie de select
                {
                    case 1:
                        SetFine();
                        ticket = new Ticket(selectAgent, driverName, Category.Bicycle, fine);
                        tickets.Add(ticket); 
                        fine = 0;
                        AgentCheck();
                        check = true;
                        break;
                    case 2:
                        SetFine();
                        ticket = new Ticket(selectAgent, driverName, Category.MopedMotorcycle, fine);
                        tickets.Add(ticket);
                        fine = 0;
                        AgentCheck();
                        check = true;
                        break;
                    case 3:
                        SetFine();
                        ticket = new Ticket(selectAgent, driverName, Category.Car, fine);
                        tickets.Add(ticket);
                        fine = 0;
                        AgentCheck();
                        check = true;
                        break;
                    case 4:
                        SetFine();
                        ticket = new Ticket(selectAgent, driverName, Category.Truck, fine);
                        tickets.Add(ticket);
                        fine = 0;
                        AgentCheck();
                        check = true;
                        break;
                    case 5:
                        SetFine();
                        ticket = new Ticket(selectAgent, driverName, Category.Tractor, fine);
                        tickets.Add(ticket);
                        fine = 0;
                        AgentCheck();
                        check = true;
                        break;
                    default:
                        Console.WriteLine("Ati introdus un numar gresit!");
                        Console.WriteLine("Va rugam introduceti un numar corespunzator categoriei: ");
                        Console.WriteLine();
                        check = false;
                        break;
                }
            }
            check = false;
        }

        private void SetFine()
        {

            do
            {
                Console.WriteLine("Precizati valoarea amenzii: ");
                Console.WriteLine();

                string newInput = Console.ReadLine();

                try
                {
                    fine = uint.Parse(newInput);
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Nu ati introdus un numar: {0}", newInput);
                    enter = true;
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ati introdus un numar negativ");
                    enter = true;
                    Console.WriteLine();
                }

                if (fine > 0)
                {
                    enter = false;
                }
            }
            while (true && enter);
            enter = true;
            Console.Clear();

            DisplayAgents();
        }

        private void AgentCheck()
        {
            Console.WriteLine("Selectati agentul constatator: ");

            input = Console.ReadLine();

            while (!int.TryParse(input, out selectAgent) || !agents.Exists(a => a.Code == selectAgent))
            {
                Console.WriteLine("Input invalid, incercati din nou!");
                input = Console.ReadLine();
            }

            if (agents.Exists(a => a.Code == selectAgent))
            {
                ticket.AgentID = selectAgent;
                driversListName.Add(ticket.Name);
            }

            Console.WriteLine($"Agentul cu codul {input} a fost selectat!");
            Console.WriteLine();
            Meniu.Return();
            Console.WriteLine();
        }

        public void DriverFines()
        {
            Console.WriteLine("Lista contravenientilor: ");
            Console.WriteLine();

            foreach (string name in driversListName.Distinct())
            {
                Console.WriteLine(name);
            }

            Console.WriteLine();
            Console.WriteLine(">> 0. Revenire Ecran Anterior");
            Console.WriteLine("................................");
            Console.WriteLine("Selectati contravenientul: ");
            Console.WriteLine();

            if (driversListName.Count == 0)
                Meniu.Return();

            driverName = Console.ReadLine();

            if (driverName == "0")
                Meniu.DisplayOptions();

            while (!tickets.Exists(t => t.Name == driverName))
            {
                Console.WriteLine("Contravenientul introdus nu exista, incercat din nou..");
                driverName = Console.ReadLine();
            }

            CategoryAndFine(driverName);

            Meniu.Return();
        }

        public string CategoryAndFine(string driver)
        {
            totalFines = 0;
            string result = "";

            if (tickets.Exists(x => x.Name == driver))
            {
                foreach (Ticket ticket in tickets)
                {
                    if (driver == ticket.Name)
                    {
                        result = "Categoria: " + ticket.category + " -> amenda de: " + ticket.Fine + " RON";
                        Console.WriteLine(result);
                        totalFines += ticket.Fine;
                    }
                }
                Console.WriteLine("*********************************************");
                Console.WriteLine("Totalul amenzilor acestui sofer este de: " + totalFines + " RON");
                Console.WriteLine();
                return result;
            }

            return String.Empty;
        }

        public void DisplayAgentFines()
        {
            DisplayAgents();
            Console.WriteLine(">> 0. Revenire Ecran Anterior");
            Console.WriteLine();

            Console.WriteLine("Selectati codul agentului: ");
            Console.WriteLine();

            if (agents.Count == 0)
                Meniu.Return();

            input = Console.ReadLine();

            if (input == "0")
                Meniu.DisplayOptions();

            while (!int.TryParse(input, out selectAgent) || !agents.Exists(a => a.Code == selectAgent))
            {
                Console.WriteLine("Input invalid, incercati din nou!");
                input = Console.ReadLine();
            }

            NameAndTicket(selectAgent);

            Meniu.Return();
        }

        public string NameAndTicket(int id)
        {

            totalFines = 0;
            string result = "";

            var namesAndFines = agents.Select(a => tickets.FindAll(x => x.AgentID == a.Code));

            if (agents.Exists(a => a.Code == id))
            {
                foreach (var tickets in namesAndFines)
                {
                    foreach (var t in tickets)
                    {
                        if (id == t.AgentID)
                        {
                            result = "Soferul " + t.Name + " a fost amendat cu " + t.Fine + " RON";
                            Console.WriteLine(result);
                            totalFines += t.Fine;
                        }
                    }
                }
                Console.WriteLine("*********************************************");
                Console.WriteLine("Totalul amenzilor acestui agent: " + totalFines + " RON");
                Console.WriteLine();
                return result;
            }

            return String.Empty;
        }

        public void DisplayFines()
        {
            totalFines = 0;

            var newList = from agent in agents
                          join fine in tickets
                          on agent.Code equals
                          fine.AgentID into namesAndTickets
                          select new { Name = agent.Name, total = namesAndTickets.Sum(x => x.Fine) };

            foreach (var item in newList.OrderByDescending(x => x.total))
            {
                Console.WriteLine("Agentul " + item.Name + " -> amenzi totale: " + item.total);
                totalFines += (uint)item.total;
            }

            Console.WriteLine();
            Console.WriteLine("Total amenzi agenti: " + totalFines + " RON");

            Console.WriteLine();
            Meniu.Return();
        }

    }
}
