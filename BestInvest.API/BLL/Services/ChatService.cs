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

        public async Task<bool> RemoveAsync(int chatId)
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

        public async Task<List<ChatDTO>> GetAllAsync(int projectId)
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
                    Messages = MapMessages(c),
                    IsActive = IsActive(c)
                }).ToListAsync();
        }

        public async Task<ChatDTO> GetAsync(int chatId)
        {
            return await dbContext.Chats
                .Include(c => c.Messages)
                .Include(c => c.Project)
                .Where(c => c.Id == chatId)
                .Select(c => new ChatDTO()
                {
                    Id = c.Id,
                    AccountId = c.AccountId,
                    Name = c.Name,
                    ProjectId = c.ProjectId,
                    Messages = MapMessages(c),
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

        private List<MessageDTO> MapMessages(Chat chat)
        {
            return chat.Messages.Select(m => new MessageDTO()
            {
                AccountId = m.AccountId,
                ChatId = m.ChatId,
                Id = m.Id,
                Text = m.Text,
                Timestamp = m.Timestamp,

                FullName = GetFullName(m.AccountId),
                ChatRole = GetChatRole(m.AccountId, chat.Project.TeamId)
            }).ToList();
        }

        private string GetFullName(int accountId)
        {
            return dbContext.AccountsInfo
                .Where(a => a.AccountId == accountId)
                .FirstOrDefault()
                .FullName;
        }

        private string GetChatRole(int accountId, int teamId)
        {
            var teamMember = dbContext.TeamMembers
                .Where(tm => tm.AccountId == accountId && tm.TeamId == teamId)
                .FirstOrDefault();

            return teamMember == null ? "Investor" : teamMember.Position;
        }

        public async Task<bool> UpdateAsync(ChatDTO chatDTO)
        {
            var chat = await dbContext.Chats.FindAsync(chatDTO.Id);
            if (chat == null)
            {
                return false;
            }

            chat.Name = chatDTO.Name;
            dbContext.Chats.Update(chat);

            if (!chatDTO.IsActive)
            {
                var deal = await dbContext.Deals
                    .Where(d => d.ProjectId == chat.ProjectId && d.AccountId == chat.AccountId)
                    .FirstOrDefaultAsync();

                if (deal != null)
                {
                    deal.State = DealState.Canceled;
                    dbContext.Deals.Update(deal);
                }
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task CreateAsync(ChatDTO chatDTO)
        {
            var chat = new Chat()
            {
                AccountId = chatDTO.AccountId,
                ProjectId = chatDTO.ProjectId,
                Name = chatDTO.Name
            };

            await dbContext.Chats.AddAsync(chat);
            await dbContext.SaveChangesAsync();
        }
    }
}
