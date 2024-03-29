﻿using AutoMapper;
using CustomerAccountDeletionRequest.DomainModels;
using CustomerAccountDeletionRequest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAccountDeletionRequest.Profiles
{
    /// <summary>
    /// This class is used to map all objects for AutoMapper's reference.
    /// </summary>
    public class DeletionRequestProfile : Profile
    {
        public DeletionRequestProfile()
        {
            CreateMap<DeletionRequestModel, DeletionRequestReadDTO>();
            CreateMap<DeletionRequestCreateDTO, DeletionRequestModel>();
            CreateMap<DeletionRequestModel, DeletionRequestApproveDTO>();
            CreateMap<DeletionRequestApproveDTO, DeletionRequestModel>();
        }
    }
}
