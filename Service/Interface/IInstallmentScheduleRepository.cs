using Model.DTOs.InstallmentSchedule;
using Model.DTOs.Withdrawal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IInstallmentScheduleRepository
    {
        public Task<string> AddInstallmentScheduleSL(int companyId, int branchID, string userName, GenInstallmentSchedule entityDto);
        public Task<string> AddInstallmentScheduleIL(int companyId, int branchID, string userName, GenInstallmentSchedule entityDto);
        public Task<string> AddInstallmentScheduleAML(int companyId, int branchID, string userName, GenInstallmentSchedule entityDto);
    }
}
