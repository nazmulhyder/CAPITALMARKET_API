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
    public interface ITradeFileRepository
    {
        public Task<object> ShortSaleList(string UserName, int CompanyID,int ProductID, DateTime TradeDate);

        #region SL
        public Task<object> SLTradeFileProcessingValidation(IFormFile formData, string UserName, int CompanyID, int BranchID, string strTradeDate, string ExcName);
        public Task<string> SLTradeFileUpload(IFormCollection formData, string UserName);
       public Task<List<SLTradeSummaryDto>> SLTradeSummary(int CompanyID, DateTime TradeDate);
        public Task<string> ApproveSLTradeSummaryProductWise(string UserName, int CompanyID, int BranchID, DateTime TradeDate, int ProductID, string ApprovalRemark);

        public Task<string> TradeReversalRequestSL(string UserName, TradeFileReversalRequest data);

        public Task<List<SLTradeFileStatusDto>> TradeFileStatusSL(string UserName, DateTime TradeDate);

        public Task<string> ApproveTradeFileReversalSL(string UserName, ApproveTradeFileReversalRequest data);


        #endregion SL

        #region IL
        public Task<object> ILPanelBrokerList(string UserName,int CompanyID);
        public Task<object> ILTradeFileUploadValidation(IFormCollection formData, string UserName);
        public Task<string> ILTradeFileUpload(IFormCollection formData, string UserName);

        public Task<List<ILTradeSummaryDto>> ILTradeSummary(int CompanyID, DateTime TradeDate);
        public Task<object> ApproveILTradeSummaryProductWise(string UserName, int CompanyID, int BrokerID, DateTime TradeDate, int ProductID, string ApprovalRemark);

        public Task<string> TradeReversalRequestIL(string UserName, TradeFileReversalRequest data);

        public Task<List<SLTradeFileStatusDto>> TradeFileStatusIL(string UserName, DateTime TradeDate);

        public Task<string> ApproveTradeFileReversalIL(string UserName, ApproveTradeFileReversalRequest data);

        public Task<object> GetNonMarginTradeDataIL(string UserName, int CompanyID, DateTime TradeDate);
        
        public Task<string> InsertNonMarginTradeDataIL(string UserName, int CompanyID, int BranchID, List<NonMarginTradeDataDto> data);

        public Task<object> GetListOverBuyIL(string UserName, int CompanyID, int BranchID, DateTime TradeDate);

        public Task<string> InsertListOverBuyIL(string UserName, int CompanyID, int BranchID, List<ListOverBuyILDto> data);

        #endregion IL

        #region AML
        public Task<object> AMLPanelBrokerList(string UserName, int CompanyID);
        public Task<object> AMLTradeFileUploadValidation(IFormCollection formData, string UserName);
        public Task<string> AMLTradeFileUpload(IFormCollection formData, string UserName);

           public Task<List<AMLProductDto>> AMLTradeSummary(int CompanyID, DateTime TradeDate);
        public Task<string> ApproveAMLTradeSummaryProductWise(string UserName, int CompanyID, int BranchID, DateTime TradeDate, int ProductID, string ApprovalRemark);


        public Task<string> TradeReversalRequestAML(string UserName, TradeFileReversalRequest data);

        public Task<List<SLTradeFileStatusDto>> TradeFileStatusAML(string UserName, DateTime TradeDate);

        public Task<string> ApproveTradeFileReversalAML(string UserName, ApproveTradeFileReversalRequest data);


        #endregion AML
    }
}
