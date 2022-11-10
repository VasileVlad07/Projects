using Fitness.Data;
using Fitness.Entities;
using Fitness.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

namespace InstructorInformationBase
{
    public class InstructorInformationBase : ComponentBase
    {
        public InstructorInformation InstructorInformation { get; set; }

        private const int maxHours = 20;
        private static bool removed = true;


        public static bool Removed 
        { 
            get { return removed; }
            set { removed = value; } 
        }

        Random random = new Random();

        protected override Task OnInitializedAsync()
        {
            InstructorInformation = new InstructorInformation();

            return base.OnInitializedAsync(); 
        }

        public void GenerateID()
        {
            if (InstructorInformation.LastName != null && InstructorInformation.FirstName != null)
            {
                InstructorInformation.InstructorId = random.Next(1, 999);

                // Aici folosesc baza de date pentru a verifica daca numarul generat exista deja deoarece avem nevoie de un ID unic!!!

                FitnessGym gym = new FitnessGym();

                var exists = gym.InstructorInformations.Any(x => x.InstructorId == InstructorInformation.InstructorId);

                while(exists == true)
                {
                    InstructorInformation.InstructorId = random.Next(1, 999);
                }
            }
        }

        public static bool CheckInstructorHours()
        {
            FitnessGym data = new FitnessGym();

            if (data.InstructorInformations.Any(x => x.Hours < maxHours))
            {
                return true; // Putem sa adaugam clientul respectiv la acel instructor
            }

            return false;
        }

        public void SaveInstructorInformation()
        {
            
            using (FitnessDateAcces date = new FitnessDateAcces())
            {
                date.SaveIstructorInformation(InstructorInformation);
            }
        }

        public void RemoveInstructor()
        {
            ClientEntity clientData = new ClientEntity();
            FitnessGym instructorDate = new FitnessGym();

            int minHours = 20;
            int newInstructorID = 0;
            int clientHours = 0;

            foreach (var instructor in instructorDate.InstructorInformations)
            {
                if (instructor.Hours < minHours)
                {
                    minHours = instructor.Hours;
                    newInstructorID = (int)instructor.InstructorId;  // Orele si ID-ul instructorului cu cele mai putine ore 
                }

            }

            foreach (var instructor in instructorDate.InstructorInformations)
            {
                if (InstructorInformation.InstructorId == instructor.InstructorId)
                {

                    foreach (var client in clientData.ClientInformations)
                    {
                        if (client.InstructorID == instructor.InstructorId)
                        {
                            client.InstructorID = newInstructorID;
                            clientHours += client.Hours;
                        }
                    }

                    instructorDate.InstructorInformations.Remove(instructor);
                    Removed = true;
                }
                else
                {
                    Removed = false;
                }
            }

            foreach (var instructor in instructorDate.InstructorInformations)
            {
                if(instructor.InstructorId == newInstructorID)
                {
                    instructor.Hours += clientHours;    
                }
            }

            instructorDate.SaveChanges();
            clientData.SaveChanges();
        }

    }
}
