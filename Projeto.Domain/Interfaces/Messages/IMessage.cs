using Projeto.Domain.Interfaces.Generics;
using Projeto.Entities.Entities.Messages;
using System.Linq.Expressions;

namespace Projeto.Domain.Interfaces.Messages
{
    public interface IMessage : IGeneric<Message>
    {
        Task<List<Message>> ListarPorUsuario(Expression<Func<Message, bool>> exMessage);
    }
}
