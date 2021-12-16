using Gruppo3.ClientiDTO.Domain.Entities;
using Gruppo3.ClientiDTO.Domain.Events;
using MassTransit;
using Microservices.EcommerceApp.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microservices.EcommerceApp.API.Consumer.ClientConsumers
{
    public class UpdateClientConsumer : IConsumer<UpdateClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        public UpdateClientConsumer(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        public async Task Consume(ConsumeContext<UpdateClientCommand> context)
        {
            var client = new Client
            {
                Name = context.Message.Name,
                Surname = context.Message.Surname,
                Address = context.Message.Address,
                Businessname = context.Message.Businessname,
                CF = context.Message.CF,
                Email = context.Message.Email,
                Piva = context.Message.Piva,
                Year = context.Message.Year,
                Id = context.Message.Id
            };


            await _clientRepository.UpdateClient(client);
        }
    }
}
