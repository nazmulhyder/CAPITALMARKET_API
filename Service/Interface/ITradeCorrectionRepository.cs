using Microsoft.AspNetCore.Http;
using Model.DTOs.PriceFileUpload;
using Model.DTOs.TradeDataCorrection;
using Model.DTOs.TradeFileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITradeCorrectionRepository
    {
       
        public Task<List<TradeDataListForCorrectionDto>> ListTradeDataForCorrection(int CompanyID, string UserName, string AccountNumber, string ExchaneName, DateTime TradeDate );
        public Task<string> TradeCorrectionEntry(string UserName,List<Type_TradeCorrectionLogDto> data);
        public Task<List<TradeDataListForCorrectionDto>> ListTradeDataCorrectionListForApproval(int CompanyID, string UserName, DateTime TradeDate);

        public Task<string> TradeCorrectioApprove(string UserName, ApproveTradeCorrectionDataDto data);

      
    }
}
