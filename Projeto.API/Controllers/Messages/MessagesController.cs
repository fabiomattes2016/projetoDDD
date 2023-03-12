using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.API.Models.Messages;
using Projeto.Domain.Interfaces.Messages;
using Projeto.Domain.Interfaces.Services.Messages;
using Projeto.Entities.Entities.Messages;
using Projeto.Entities.Entities.Validations;

namespace Projeto.API.Controllers.Messages
{
    [Route("api/v1/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessage _message;
        private readonly IMessageService _messageService;

        public MessagesController(IMapper mapper, IMessage message, IMessageService messageService)
        {
            _mapper = mapper;
            _message = message;
            _messageService = messageService;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet()]
        public async Task<List<MessageViewModel>> List()
        {
            var messages = await _messageService.ListarTodos();
            var messagesMap = _mapper.Map<List<MessageViewModel>>(messages);

            return messagesMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("user")]
        public async Task<List<MessageViewModel>> ListByUserId()
        {
            var userId = await RetornaIdUsuarioLogado();
            var messages = await _messageService.ListarPorUsuario(userId);
            var messagesMap = _mapper.Map<List<MessageViewModel>>(messages);

            return messagesMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("create")]
        public async Task<List<Notifiers>> Create(MessageViewModel message)
        {
            message.UserId = await RetornaIdUsuarioLogado();

            var messageMap = _mapper.Map<Message>(message);
            await _messageService.Adicionar(messageMap);

            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<MessageViewModel> GetById(int id)
        {
            var message = await _messageService.BuscarPorId(id);
            var messageMap = _mapper.Map<MessageViewModel>(message);

            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPut("update")]
        public async Task<List<Notifiers>> Update(MessageViewModel message)
        {
            message.UserId = await RetornaIdUsuarioLogado();

            var messageMap = _mapper.Map<Message>(message);
            await _messageService.Atualizar(messageMap);

            return messageMap.Notificacoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("delete")]
        public async Task<List<Notifiers>> Delete(MessageViewModel message)
        {
            message.UserId = await RetornaIdUsuarioLogado();

            var messageMap = _mapper.Map<Message>(message);
            await _messageService.Deletar(messageMap);

            return messageMap.Notificacoes;
        }

        private async Task<string> RetornaIdUsuarioLogado()
        {
            if (User != null)
            {
                var userId = User.FindFirst("userId");
                return userId.Value;
            }

            return string.Empty;
        }
    }
}
