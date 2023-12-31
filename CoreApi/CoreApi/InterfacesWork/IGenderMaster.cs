﻿using CoreApi.DatabaseModels;
using CoreApi.Models;

namespace CoreApi.InterfacesWork
{
    public interface IGenderMaster
    {
        Task<GenderModel> AddGender(GenderModel _GenderModel);  //Add Gender details 
        Task<IEnumerable<GenderModellist>> GetGenderList();  // Details of Gender list
        Task<GenderModel> GetGenderByGuid(string GenderGuid); // get single data for edit
        Task UpdateGender(GenderModel _GenderModel); // gender update 
       
       
    }
}
