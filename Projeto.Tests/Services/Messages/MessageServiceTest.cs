using Moq;
using Projeto.Domain.Interfaces.Messages;
using Projeto.Domain.Interfaces.Services.Messages;
using Projeto.Domain.Services.Messages;
using Projeto.Entities.Entities.Messages;
using Xunit;

namespace Projeto.Tests.Services.Messages
{
    public class MessageServiceTest
    {
        private readonly IMessageService _messageService;
        private readonly Mock<IMessage> _messageRepository;

        public MessageServiceTest()
        {
            _messageRepository = new Mock<IMessage>();
            _messageService = new MessageService(_messageRepository.Object);
        }

        [Fact(DisplayName = "AddMessage: 01 - Deve utilizar _messageRepository.Add")]
        public async Task AddMessage01()
        {
            int Id = 1;
            string Titulo = "Teste";
            bool Ativo = true;
            DateTime DataCadastro = DateTime.Now;
            DateTime DataAlteracao = DateTime.Now;
            string UserId = "940adf7a-26f8-40fa-b89e-3dbe764d3086";

            Message message = new()
            {
                Id = Id,
                Titulo = Titulo,
                Ativo = Ativo,
                DataCadastro = DataCadastro,
                DataAlteracao = DataAlteracao,
                UserId = UserId
            };

            await _messageService.Adicionar(message);

            _messageRepository.Verify(m => m.Add(message), Times.Once);
        }
    }
}
