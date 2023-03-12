using Projeto.Domain.Interfaces.Messages;
using Projeto.Domain.Interfaces.Services.Messages;
using Projeto.Entities.Entities.Messages;

namespace Projeto.Domain.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IMessage _message;

        public MessageService(IMessage message)
        {
            _message = message;
        }

        public async Task<List<Message>> ListarTodos()
        {
            return await _message.GetAll();
        }

        public async Task<List<Message>> ListarPorUsuario(string userId)
        {
            return await _message.ListarPorUsuario(m => m.UserId == userId);
        }

        public async Task Adicionar(Message message)
        {
            var validaTitulo = message.ValidaPropriedadeString(message.Titulo, "Titulo");

            if (validaTitulo)
            {
                message.DataCadastro = DateTime.Now;
                message.DataAlteracao = DateTime.Now;
                message.Ativo = true;

                await _message.Add(message);
            }
        }

        public async Task<Message> BuscarPorId(int id)
        {
            return await _message.GetEntityById(id);
        }

        public async Task Atualizar(Message message)
        {
            var validaTitulo = message.ValidaPropriedadeString(message.Titulo, "Titulo");


            if (validaTitulo)
            {
                message.DataAlteracao = DateTime.Now;

                await _message.Update(message);
            }

        }

        public async Task Deletar(Message message)
        {
            await _message.Delete(message);
        }
    }
}
