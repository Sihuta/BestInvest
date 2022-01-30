﻿using BestInvest.API.BLL.DTO;
using BestInvest.API.BLL.Interfaces;
using BestInvest.API.DAL.EF;
using BestInvest.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BestInvest.API.BLL.Services
{
    public class StartuperService : IStartuperService
    {
        private readonly AppDbContext dbContext;
        private readonly IdentityService identityService;

        public StartuperService(AppDbContext appDbContext, IdentityService identityService)
        {
            dbContext = appDbContext;
            this.identityService = identityService;
        }

        public async Task<AccountDTO> GetFullInfoAsync(ClaimsPrincipal user)
        {
            var account = await GetCurrentUserAccountAsync(user);

            return new AccountDTO()
            {
                Id = account.Id,
                AccountInfoId = account.AccountInfoId,
                
                Login = account.Login,
                Email = account.Email,
                Role = account.Role,

                FullName = account.AccountInfo.FullName,
                DateOfBirth = account.AccountInfo.DateOfBirth,
                LinkedIn = account.AccountInfo.LinkedIn,
                WorkingExperience = account.AccountInfo.WorkingExperience,
            };
        }

        public async Task<bool> UpdateAsync(ClaimsPrincipal user, AccountDTO account)
        {
            var result = await identityService.UpdateUserAsync(user, account.Email, account.Login);
            if (!result)
            {
                return false;
            }
            
            var currentAccount = await GetCurrentUserAccountAsync(user);
            var accountInfo = currentAccount.AccountInfo;

            currentAccount.Email = account.Email;
            currentAccount.Login = account.Login;

            accountInfo.DateOfBirth = account.DateOfBirth;
            accountInfo.LinkedIn = account.LinkedIn;
            accountInfo.WorkingExperience = account.WorkingExperience;
            accountInfo.FullName = account.FullName;

            dbContext.Entry(currentAccount).State = EntityState.Modified;
            dbContext.Entry(accountInfo).State = EntityState.Modified;
            dbContext.SaveChanges();

            return true;
        }

        private async Task<Account> GetCurrentUserAccountAsync(ClaimsPrincipal user)
        {
            var currentUser = await identityService.GetCurrentUserAsync(user);
            var userEmail = currentUser.Email;

            return await dbContext.Accounts
                .Where(a => a.Email == userEmail)
                .Include(a => a.AccountInfo)
                .FirstOrDefaultAsync();
        }
    }
}