using Model.DTOs.UpdateLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUpdateLogRepository 
    {
        public Task<bool> SaveUpdateLog(List<UpdateLogDetailDto> logs, string username, int masterID,int UnitID);
        public Task<List<UpdateLogMasterDto>> getUpdateLogList(int UpdateLogID, int UpdateUnitID);
        public Task<List<UpdateLogPresentDetailDto>> getUpdateLogDetailList(int logid);

    }
}
