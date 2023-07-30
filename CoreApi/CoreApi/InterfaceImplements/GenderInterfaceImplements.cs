﻿using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreApi.InterfaceImplements
{
    public class GenderInterfaceImplements : IGenderMaster
    {
        private CurdApiContext appDbContext;
        private readonly ILogger<GenderInterfaceImplements> _logger;

        public GenderInterfaceImplements(CurdApiContext appDbContext, ILogger<GenderInterfaceImplements> logger)
        {
            this.appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<GenderModel> AddGender(GenderModel _GenderModel)
        {
            try
            {
                // Create a new Gender object
                var newGender = new Gender
                {
                    Name = _GenderModel.Name,
                    GenderGuid = Guid.NewGuid()
                };

                appDbContext.Genders.Add(newGender);
                await appDbContext.SaveChangesAsync();
                return new GenderModel
                {
                    Name = newGender.Name,
                    GenderGuid = newGender.GenderGuid.ToString(),
                };

            }
            catch (Exception ex)
            {
                // Log the error using ILogger
                _logger.LogError(ex, "An error occurred while adding a new gender to the database.");
                // Rethrow the exception to propagate it further if needed
                throw;
            }
            
        }

        public Task<GenderModel> GetGenderByGuid(int GenderGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GenderModellist>> GetGenderList()
        {
            try
            {
                List<GenderModellist> genderList = await (from g in appDbContext.Genders
                                                      select new GenderModellist
                                                      {
                                                          GenderGuid = g.GenderGuid.ToString(),
                                                          Name = g.Name
                                                      }).ToListAsync();

                return genderList;
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "An error occurred while fetching gender list.");
                return Enumerable.Empty<GenderModellist>();
            }
        }

        public Task UpdateGender(string GenderGuid)
        {
            throw new NotImplementedException();
        }
    }
}
