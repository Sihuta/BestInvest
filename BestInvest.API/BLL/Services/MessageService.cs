using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;

namespace BestInvest.API.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext dbContext;
        private readonly Mapper mapper;

        public MessageService(AppDbContext appDbContext, Mapper mapper)
        {
            dbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task CreateAsync(MessageDTO messageDTO)
        {
            var message = new Message()
            {
                AccountId = messageDTO.AccountId,
                ChatId = messageDTO.ChatId,
                Text = messageDTO.Text,
                Timestamp = messageDTO.Timestamp
            };

            await dbContext.AddAsync(message);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var message = await dbContext.Messages.FindAsync(id);
            if (message == null)
            {
                return false;
            }

            dbContext.Remove(message);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
