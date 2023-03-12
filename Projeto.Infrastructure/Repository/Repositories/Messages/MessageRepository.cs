using Microsoft.EntityFrameworkCore;
using Projeto.Domain.Interfaces.Messages;
using Projeto.Entities.Entities.Messages;
using Projeto.Infrastructure.Configuration;
using Projeto.Infrastructure.Repository.Generics;
using System.Linq.Expressions;

namespace Projeto.Infrastructure.Repository.Repositories.Messages
{
    public class MessageRepository : GenericRepository<Message>, IMessage
    {
        private readonly DbContextOptions<ContextBase> _dbContextOptions;

        public MessageRepository()
        {
            _dbContextOptions = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Message>> ListarPorUsuario(Expression<Func<Message, bool>> exMessage)
        {
            using (var banco = new ContextBase(_dbContextOptions))
            {
                return await banco.Messages.Where(exMessage).AsNoTracking().ToListAsync();
            }
        }
    }
}
