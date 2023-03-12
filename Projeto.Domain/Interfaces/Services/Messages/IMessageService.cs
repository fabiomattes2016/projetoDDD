using Projeto.Entities.Entities.Messages;

namespace Projeto.Domain.Interfaces.Services.Messages
{
    public interface IMessageService
    {
        Task<List<Message>> ListarTodos();
        Task<List<Message>> ListarPorUsuario(string userId);
        Task Adicionar(Message message);
        Task<Message> BuscarPorId(int id);
        Task Atualizar(Message message);
        Task Deletar(Message message);

    }
}
