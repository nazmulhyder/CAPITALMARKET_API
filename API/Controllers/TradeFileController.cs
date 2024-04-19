using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.TradeDataCorrection;
using Model.DTOs.TradeFileUpload;
using Newtonsoft.Json;
using Service.Interface;
using System.Net.Mime;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TradeFileController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<PriceFileController> _logger;

        public TradeFileController(IService service, ILogger<PriceFileController> logger)
        {
            _service = service;
            _logger = logger;
        }

        private string LoggedOnUser()
        {
            var principal = HttpContext.User;
            _logger.LogInformation("Principal: {0}", principal.Identity.Name);
            var windowsIdentity = principal?.Identity as WindowsIdentity;
            string loggedOnUser = windowsIdentity.Name;
            if (loggedOnUser.Contains('\\'))
                loggedOnUser = loggedOnUser.Split("\\")[1];
            return loggedOnUser;
        }

        [HttpGet("ShortSale/{CompanyID}/{ProductID}/{TradeDate}")]
        public async Task<IActionResult> ShortSaleList(int CompanyID,int ProductID, DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.TradeFile.ShortSaleList(LoggedOnUser(), CompanyID, ProductID, TradeDate));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        #region SL

        [HttpGet("SL/TradeFile/Status/{TradeDate}")]
        public async Task<IActionResult> TradeFileStatusSL(DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.TradeFile.TradeFileStatusSL(LoggedOnUser(), TradeDate));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SL/FileReversal/Request")]
        public async Task<IActionResult> TradeReversalRequestSL(TradeFileReversalRequest data)
        {
            try
            {
                return getResponse(await _service.TradeFile.TradeReversalRequestSL(LoggedOnUser(), data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("SL/TradeFile/ReversalRequest/Approve")]
        public async Task<IActionResult> ApproveTradeFileReversalSL(ApproveTradeFileReversalRequest data)
        {
            try
            {
                return getResponse(await _service.TradeFile.ApproveTradeFileReversalSL(LoggedOnUser(), data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("TradeFileUpload")]
        public async Task<IActionResult> TradeFileUpload(IFormCollection formData)
        {
            try
            {
                //if(Path.GetExtension(formData.Files[0].FileName) == ".xsd")
                //{

                //    using (var stream = new MemoryStream(System.IO.File.ReadAllBytes("\\\\intra.idlc.com\\IDLCDrive\\Site1\\Users\\shourav\\Documents\\My Received Files\\Flextrade-BOS-Clients.xsd")))
                //    {
                //        var schema = XmlSchema.Read(XmlReader.Create(stream), null);
                //        var gen = new XmlSampleGenerator(schema, new XmlQualifiedName("rootElement"));
                //        gen.WriteXml(XmlWriter.Create(@"c:\temp\autogen.xml"));
                //    }

                //}

                string Res = await _service.TradeFile.SLTradeFileUpload(formData, LoggedOnUser());
                if (Res == "Trade File Upload successful.")
                    return getResponse(Res);
                else throw new Exception(Res);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("TradeFileProcessingValidation/{CompanyID}/{BranchID}/{ExchangeName}/{TradeDate}")]
        public async Task<IActionResult> TradeFileProcessingValidation(IFormFile formData, int CompanyID, int BranchID, string ExchangeName, string TradeDate)
        {
            try
            {
                return getResponse(await _service.TradeFile.SLTradeFileProcessingValidation(formData, LoggedOnUser(), CompanyID, BranchID, Regex.Replace(TradeDate, "-", string.Empty).ToString(), ExchangeName));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    

        [HttpGet("SLTradeDataSummary/{CompanyID}/{TradeDate}")]
        public async Task<IActionResult> TradeSummary( int CompanyID, DateTime TradeDate)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.SLTradeSummary( CompanyID, TradeDate);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("Approve/SLTradeDataSummary")]
        public async Task<IActionResult> ApproveSLTradeSummaryProductWise(TradeSummaryApprovalDto approvalDto)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.ApproveSLTradeSummaryProductWise(LoggedOnUser(), approvalDto.CompanyID, approvalDto.BrokerID, approvalDto.TradeDate, approvalDto.ProductID, approvalDto.ApprovalRemark);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion SL

        #region IL

        [HttpPost("IL/FileReversal/Request")]
        public async Task<IActionResult> TradeReversalRequestIL(TradeFileReversalRequest data)
        {
            try
            {
                return getResponse(await _service.TradeFile.TradeReversalRequestIL(LoggedOnUser(), data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IL/TradeFile/Status/{TradeDate}")]
        public async Task<IActionResult> TradeFileStatusIL(DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.TradeFile.TradeFileStatusIL(LoggedOnUser(), TradeDate));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("IL/TradeFile/ReversalRequest/Approve")]
        public async Task<IActionResult> ApproveTradeFileReversalIL(ApproveTradeFileReversalRequest data)
        {
            try
            {
                return getResponse(await _service.TradeFile.ApproveTradeFileReversalIL(LoggedOnUser(), data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/Panel/Broker/{CompanyID}")]
        public async Task<IActionResult> ILPanelBrokerList(int CompanyID)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.ILPanelBrokerList(LoggedOnUser(), CompanyID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ILTradeFileUploadValidation")]
        public async Task<IActionResult> ILTradeFileUploadValidation(IFormCollection formData)
        {
            try
            {
                return getResponse(await _service.TradeFile.ILTradeFileUploadValidation(formData, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ILTradeFileUpload")]
        public async Task<IActionResult> ILTradeFileUpload(IFormCollection formData)
        {
            try
            {
                string Res =await _service.TradeFile.ILTradeFileUpload(formData, LoggedOnUser());
                if (Res == "Trade File Upload successful.")
                    return getResponse(Res);
                else throw new Exception(Res);

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ILTradeDataSummary/{CompanyID}/{TradeDate}")]
        public async Task<IActionResult> ILTradeSummary(int CompanyID, DateTime TradeDate)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.ILTradeSummary(CompanyID, TradeDate);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("Approve/ILTradeDataSummary")]
        public async Task<IActionResult> ApproveILTradeSummaryProductWise(TradeSummaryApprovalDto approvalDto)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.ApproveILTradeSummaryProductWise(LoggedOnUser(), approvalDto.CompanyID, approvalDto.BrokerID, approvalDto.TradeDate, approvalDto.ProductID, approvalDto.ApprovalRemark);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetNonMarginTradeDataIL/{CompanyID}/{TradeDate}")]
        public async Task<IActionResult> GetNonMarginTradeDataIL(int CompanyID, DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.TradeFile.GetNonMarginTradeDataIL(LoggedOnUser(), CompanyID, TradeDate));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveNonMarginTradeDataIL/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertNonMarginTradeDataIL(int CompanyID, int BranchID,List<NonMarginTradeDataDto> data)
        {
            try
            {
                return getResponse(await _service.TradeFile.InsertNonMarginTradeDataIL(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }



        [HttpGet("GetListOverBuyIL/{CompanyID}/{BranchID}/{TradeDate}")]
        public async Task<IActionResult> GetListOverBuyIL(int CompanyID, int BranchID, DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.TradeFile.GetListOverBuyIL(LoggedOnUser(), CompanyID, BranchID, TradeDate));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveOverBuyIL/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertListOverBuyIL(int CompanyID,int BranchID, List<ListOverBuyILDto> data)
        {
            try
            {
                return getResponse(await _service.TradeFile.InsertListOverBuyIL(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion IL

        #region AML
        [HttpPost("AML/FileReversal/Request")]
        public async Task<IActionResult> TradeReversalRequestAML(TradeFileReversalRequest data)
        {
            try
            {
                return getResponse(await _service.TradeFile.TradeReversalRequestAML(LoggedOnUser(), data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AML/TradeFile/Status/{TradeDate}")]
        public async Task<IActionResult> TradeFileStatusAML(DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.TradeFile.TradeFileStatusAML(LoggedOnUser(), TradeDate));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AML/TradeFile/ReversalRequest/Approve")]
        public async Task<IActionResult> ApproveTradeFileReversalAML(ApproveTradeFileReversalRequest data)
        {
            try
            {
                return getResponse(await _service.TradeFile.ApproveTradeFileReversalAML(LoggedOnUser(), data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("List/AML/Broker/{CompanyID}")]
        public async Task<IActionResult> AMLPanelBrokerList(int CompanyID)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.AMLPanelBrokerList(LoggedOnUser(), CompanyID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AMLTradeFileUploadValidation")]
        public async Task<IActionResult> AMLTradeFileUploadValidation(IFormCollection formData)
        {
            try
            {
                return getResponse(await _service.TradeFile.AMLTradeFileUploadValidation(formData, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AMLTradeFileUpload")]
        public async Task<IActionResult> AMLTradeFileUpload(IFormCollection formData)
        {
            try
            {
              
                string Res = await _service.TradeFile.AMLTradeFileUpload(formData, LoggedOnUser());
                if (Res == "Trade File Upload successful.")
                    return getResponse(Res);
                else throw new Exception(Res);

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        
        [HttpGet("AMLTradeDataSummary/{CompanyID}/{TradeDate}")]
        public async Task<IActionResult> AMLTradeSummary(int CompanyID, DateTime TradeDate)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.AMLTradeSummary(CompanyID, TradeDate);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("Approve/AMLTradeDataSummary")]
        public async Task<IActionResult> ApproveAMLTradeSummaryProductWise(TradeSummaryApprovalDto approvalDto)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeFile.ApproveAMLTradeSummaryProductWise(LoggedOnUser(), approvalDto.CompanyID, approvalDto.BrokerID, approvalDto.TradeDate, approvalDto.ProductID, approvalDto.ApprovalRemark);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion AML
    }
}
