using EvidentaAmenzi;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


namespace AppLogicUnitTests
{
    public class AppLogicTests
    {
        [Test]
        public void DeleteAgent_FromTheListOfAgents()
        {
            AppLogic app = new AppLogic();

            int input = 3;

            app.Agents.Add(new Agent(3, "Mircea"));
            app.MoveFinesToVirtualAgent(input);


            Assert.True(app.Agents.Count == 0);
        }

        [Test]
        public void DriverFines_CheckDriverName_CorrectDisplayCategoryAndFine()
        {
            AppLogic app = new AppLogic();
            Ticket ticket;

            string driver = "Mircea";

            ticket = new Ticket(100, driver, Category.Car, 500);
            app.Tickets.Add(ticket);

            string expected = "Categoria: " + ticket.category + " -> amenda de: " + ticket.Fine + " RON";

            string result = app.CategoryAndFine(driver);

            Assert.AreEqual(expected, result);

        }

        [Test]
        public void DisplayAgentFines_CheckCorrectNameAndTicket()
        {
            AppLogic app = new AppLogic();
            Ticket ticket;

            int agentId = 10;

            Agent agent = new Agent(agentId, "Vlad");

            ticket = new Ticket(agentId, "Mihai", Category.Truck, 1000);
            app.Tickets.Add(ticket);
            app.Agents.Add(agent);

            string expected = "Soferul " + ticket.Name + " a fost amendat cu " + ticket.Fine + " RON";

            string actual = app.NameAndTicket(agentId);

           
            Assert.That(actual, Is.EqualTo(expected));  
        }

    }
}