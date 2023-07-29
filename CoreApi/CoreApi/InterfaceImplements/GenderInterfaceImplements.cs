using CoreApi.DatabaseModels;
using CoreApi.InterfacesWork;
using CoreApi.Models;
using Microsoft.EntityFrameworkCore;

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
        public Task<GenderModel> AddGender(GenderModel _GenderModel)
        {
            throw new NotImplementedException();
        }

        public Task<GenderModel> GetGenderByGuid(int GenderGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GenderModel>> GetGenderList()
        {
            try
            {
                var genderList = await (from g in appDbContext.Genders
                                        select new GenderModel
                                        {
                                            GenderGuid = g.GenderGuid.ToString(),
                                            Name = g.Name
                                        }).ToListAsync();

                return genderList;
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "An error occurred while fetching gender list.");
                return Enumerable.Empty<GenderModel>();
            }
        }

        public Task UpdateGender(string GenderGuid)
        {
            throw new NotImplementedException();
        }
    }
}
