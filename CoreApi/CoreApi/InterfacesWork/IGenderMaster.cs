using CoreApi.DatabaseModels;
using CoreApi.Models;

namespace CoreApi.InterfacesWork
{
    public interface IGenderMaster
    {
        Task<GenderModel> AddGender(GenderModel _GenderModel);  //Add Gender details 
        Task<IEnumerable<GenderModel>> GetGenderList();  // Details of Gender list
        Task<GenderModel> GetGenderByGuid(int GenderGuid); // get single data for edit
        Task UpdateGender(string GenderGuid); // gender update 
       
       
    }
}
