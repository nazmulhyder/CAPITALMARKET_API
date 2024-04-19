using Model.DTOs.EquityIncorporation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEquityIncorporationRepository
    {
        public Task<object> ListCollectionForEquityAddition(string UserName, int CompanyID, int BranchID, string AccountNo);
        public Task<object> ListEquityAddition(string UserName, int CompanyID, int BranchID, string Status);
        public Task<string> EquityIncorporationApprove(string UserName, int CompanyID, int BranchID, EquityAdditionApprovalDto data);
        public Task<string> InsertEquityAddition(string UserName, int CompanyID, int BranchID, EquityAdditionDto data);

        public Task<object> ClientInfoForEquityDeduction(string UserName, int CompanyID, int BranchID, string AccountNo);


        public Task<string> SaveEquityDeduction(string UserName, int CompanyID, int BranchID, EquityDeductionDto deductionDto);

        public Task<object> ListEquityDeduction(string UserName, int CompanyID, int BranchID, string Status);

    }
}
