using Fitness.Data;
using Fitness.Entities;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography.X509Certificates;

namespace ClientInformationBase
{
    public class ClientInformationBase : ComponentBase
    {
        public ClientInformation ClientInformation { get; set; }
        public InstructorInformation InstructorInformation { get; set; }
        

        protected override Task OnInitializedAsync()
        {
            ClientInformation = new ClientInformation();

            return base.OnInitializedAsync();
        }

        public void SaveClient()
        {
            using (FitnessDateAcces data = new FitnessDateAcces())
            {
                data.SaveClientInformation(ClientInformation);
                data.SaveInstructorHours();
            }
        }

    }
}
