using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Enums;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BestInvest.API.BLL.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDbContext dbContext;
        private readonly Mapper mapper;

        public ChatService(AppDbContext appDbContext, Mapper mapper)
        {
            dbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int chatId)
        {
            var chat = await dbContext.Chats.FindAsync(chatId);
            if (chat == null)
            {
                return false;
            }

            dbContext.Chats.Remove(chat);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ChatDTO>> GetAsync(int projectId)
        {
            return await dbContext.Chats
                .Include(c => c.Messages)
                .Where(c => c.ProjectId == projectId)
                .Select(c => new ChatDTO()
                {
                    Id = c.Id,
                    AccountId = c.AccountId,
                    Name = c.Name,
                    ProjectId = c.ProjectId,
                    Messages = mapper.Map<Message, MessageDTO>(c.Messages),
                    IsActive = IsActive(c)
                }).ToListAsync();
        }

        public async Task<ChatDTO> GetAsync(int projectId, int investorId)
        {
            return await dbContext.Chats
                .Include(c => c.Messages)
                .Where(c => c.ProjectId == projectId && c.AccountId == investorId)
                .Select(c => new ChatDTO()
                {
                    Id = c.Id,
                    AccountId = c.AccountId,
                    Name = c.Name,
                    ProjectId = c.ProjectId,
                    Messages = mapper.Map<Message, MessageDTO>(c.Messages),
                    IsActive = IsActive(c)
                }).FirstOrDefaultAsync();
        }

        private bool IsActive(Chat chat)
        {
            return dbContext.Deals
                .Where(d => d.ProjectId == chat.ProjectId && d.AccountId == chat.AccountId)
                .FirstOrDefault()
                .State == DealState.Started;
        }
    }
}
