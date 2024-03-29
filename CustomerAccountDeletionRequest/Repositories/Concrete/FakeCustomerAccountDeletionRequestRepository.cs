﻿using CustomerAccountDeletionRequest.CustomExceptionMiddleware;
using CustomerAccountDeletionRequest.DomainModels;
using CustomerAccountDeletionRequest.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAccountDeletionRequest.Repositories.Concrete
{
    public class FakeCustomerAccountDeletionRequestRepository : ICustomerAccountDeletionRequestRepository
    {
        private List<DeletionRequestModel> _deletionRequests;

        public FakeCustomerAccountDeletionRequestRepository()
        {
            _deletionRequests = new List<DeletionRequestModel>
            {
                new DeletionRequestModel { DeletionRequestID = 1, CustomerID = 1, DeletionReason = "Terrible Site.", DateRequested = new System.DateTime(2010, 10, 01, 8, 5, 3), DateApproved = new System.DateTime(1, 1, 1, 0, 0, 0), StaffID = 1, DeletionRequestStatus = Enums.DeletionRequestStatusEnum.AwaitingDecision },
                new DeletionRequestModel { DeletionRequestID = 2, CustomerID = 2, DeletionReason = "Prefer Amazon.", DateRequested = new System.DateTime(2012, 01, 02, 10, 3, 45), DateApproved = new System.DateTime(1, 1, 1, 0, 0, 0), StaffID = 1, DeletionRequestStatus = Enums.DeletionRequestStatusEnum.AwaitingDecision },
                new DeletionRequestModel { DeletionRequestID = 3, CustomerID = 3, DeletionReason = "Too many clicks.", DateRequested = new System.DateTime(2013, 02, 03, 12, 2, 40), DateApproved = new System.DateTime(1, 1, 1, 0, 0, 0), StaffID = 2, DeletionRequestStatus = Enums.DeletionRequestStatusEnum.AwaitingDecision },
                new DeletionRequestModel { DeletionRequestID = 4, CustomerID = 4, DeletionReason = "Scammed into signing up.", DateRequested = new System.DateTime(2014, 03, 04, 14, 1, 35), DateApproved = new System.DateTime(1, 1, 1, 0, 0, 0), StaffID = 3, DeletionRequestStatus = Enums.DeletionRequestStatusEnum.AwaitingDecision },
                new DeletionRequestModel { DeletionRequestID = 5, CustomerID = 5, DeletionReason = "If Wish was built by students...", DateRequested = new System.DateTime(2007, 04, 05, 16, 50, 30), DateApproved = new System.DateTime(1, 1, 1, 0, 0, 0), StaffID = 4, DeletionRequestStatus = Enums.DeletionRequestStatusEnum.AwaitingDecision }
            };
        }

        public async Task<List<DeletionRequestModel>> GetAllAwaitingDeletionRequestsAsync()
        {
            return await Task.FromResult(new List<DeletionRequestModel>(_deletionRequests.Where(dr => dr.DeletionRequestStatus == Enums.DeletionRequestStatusEnum.AwaitingDecision)));
        }

        public async Task<DeletionRequestModel> GetDeletionRequestAsync(int ID)
        {
            if (ID < 1)
                throw new ArgumentOutOfRangeException("IDs cannot be less than 0.", nameof(ArgumentOutOfRangeException));

            DeletionRequestModel deletionRequestModel = _deletionRequests.FirstOrDefault(d => d.CustomerID == ID);

            if (deletionRequestModel == null)
                throw new ResourceNotFoundException("A resource for ID: " + ID + " does not exist.");

            DeletionRequestModel returnableDeletionRequestModel = new DeletionRequestModel()
            {
                CustomerID = deletionRequestModel.CustomerID,
                StaffID = deletionRequestModel.StaffID,
                DateApproved = deletionRequestModel.DateApproved,
                DateRequested = deletionRequestModel.DateRequested,
                DeletionReason = deletionRequestModel.DeletionReason,
                DeletionRequestID = deletionRequestModel.DeletionRequestID,
                DeletionRequestStatus = deletionRequestModel.DeletionRequestStatus
            };

            return await Task.FromResult(returnableDeletionRequestModel);
        }

        public DeletionRequestModel CreateDeletionRequest(DeletionRequestModel deletionRequestModel)
        {
            if (deletionRequestModel == null)
                throw new ArgumentNullException("The deletion request to be created cannot be null.", nameof(ArgumentNullException));

            int deletionRequestID = (_deletionRequests.Count + 1);
            deletionRequestModel.DeletionRequestID = deletionRequestID;

            _deletionRequests.Add(deletionRequestModel);

            return deletionRequestModel;
        }

        public void UpdateDeletionRequest(DeletionRequestModel deletionRequestModel)
        {
            if (deletionRequestModel == null)
                throw new ArgumentNullException("The deletion request to be updated cannot be null.", nameof(ArgumentNullException));

            var productReviewModelOld = _deletionRequests.FirstOrDefault(r => r.CustomerID == deletionRequestModel.CustomerID);
            _deletionRequests.Remove(productReviewModelOld);
            _deletionRequests.Add(deletionRequestModel);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}