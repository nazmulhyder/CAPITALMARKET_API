using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.Demat;
using Model.DTOs.SecurityElimination;

namespace Service.Interface
{
    public interface ISecurityEliminationRepository
    {
        public Task<List<SecurityEliminationDTO>> CMSecurityEliminationList(int InstrumentID, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertCMSecurityElimination(SecurityEliminationInsertDTO entrySecurityELimination, int CompanyID, int BranchID, string Maker);
        public Task<List<SecuirtyEliminationMaster>> CMSecurityEliminationApprovalList(int InstrumentID, string Status,int CompanyID, int BranchID, string Maker);
        public Task<string> GetCMApprovedInstrumentElimination(CMSecurityInstrumentEliminationApproveDTO objCMApprovedInstrumentElimination, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCMUpdateSecurityElimination(CMSecurityEliminationUpdateMaster objCMUpdateSecurityElimination, int CompanyID, int BranchID, string Maker);
        

    }
}
