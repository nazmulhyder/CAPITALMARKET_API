using Model.DTOs.Dashborad;
using Model.DTOs.TradeRestriction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDashboradRepository
    {
        public Task<DashboardDTO> GetAll(int CompanyID, int BranchID, string userName);
    }
}
