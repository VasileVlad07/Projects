using Fitness.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using ClientInformationBase;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Schedule.Internal;
using Microsoft.Extensions.Logging;

namespace Fitness.Data
{
    public class FitnessDateAcces : IDisposable
    {

        int instructorHours;
        int clientID;
        private static bool clientSaved = false;
        //string? clientName;

        public void Dispose()
        {
        }

        public static bool ClientSaved
        {
            get { return clientSaved; }
            set { clientSaved = value; }
        }

        public void SaveIstructorInformation(InstructorInformation InstructorInformation)
        {
            using (FitnessGym gym = new FitnessGym())
            {
                gym.InstructorInformations.Add(InstructorInformation);
                
                gym.SaveChanges();  
            }
        }

        public void SaveClientInformation(ClientInformation ClientInformation)
        {
            using (ClientEntity client = new ClientEntity())
            {
                clientID = ClientInformation.InstructorID;
                instructorHours = ClientInformation.Hours;
                client.ClientInformations.Add(ClientInformation);
                client.SaveChanges();
            }
        }

        public void SaveInstructorHours()
        {
            using (FitnessGym gym = new FitnessGym())
            {
                foreach (var instructor in gym.InstructorInformations)
                {
                    if(instructor.InstructorId == clientID)
                    {
                        instructor.Hours += instructorHours;
                    }    
                } 
                gym.SaveChanges();
            }
            clientSaved = true;
        }
 
    }
}
