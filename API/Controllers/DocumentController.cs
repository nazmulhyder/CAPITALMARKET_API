using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Document;
using Service.Interface;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<DocumentController> _logger;
        public DocumentController(IService service, ILogger<DocumentController> logger)
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

        [HttpPost("DocumentUpload")]
        public async Task<IActionResult> DocumentUpload([FromForm()] DocumentUploadDTO uploadedFile, int companyID, int comBranchID)
        {
            try
            {
                string filePath = Utility.FilePath.GetFileUploadURL();
                //string filePath = Utility.FilePath.GetAuditInspectionFileUploadURL();
                string userName = LoggedOnUser();
                if (uploadedFile.docFile.Length > 0)
                {
                    string[] filePrts = uploadedFile.docFile.FileName.Split(".");
                    string fileExtenstion = filePrts[filePrts.Length - 1];
                    string fileName = "";
                    if (uploadedFile.stage.ToUpper() == "CIF")
                        fileName = await _service.documentRepository.UpdateDocumentDetail(uploadedFile.documentId, filePath, fileExtenstion, userName, companyID, comBranchID);
                    else
                        fileName =  await _service.documentRepository.UpdateAgreementDocumentDetail(uploadedFile.documentId, filePath, fileExtenstion, userName, companyID, comBranchID);
                    string fileFullPath = filePath + fileName;


                    using (Stream fileStream = new FileStream(fileFullPath, FileMode.Create))
                    {
                        await uploadedFile.docFile.CopyToAsync(fileStream);
                    }

                    return getResponse("File Uploaded.");
                }
                return getResponse("No file for upload.");
            }
            catch (Exception ex)
            {
                return getResponse(ex);
            }
        }

        [HttpPost("MarginRequestDocumentUpload")]
        public async Task<IActionResult> MarginRequestDocumentUpload([FromForm()] DocumentUploadDTO uploadedFile, int companyID, int comBranchID)
        {
            try
            {
                string filePath = Utility.FilePath.GetMarginRequestFileUploadURL();
                string userName = LoggedOnUser();
                if (uploadedFile.docFile.Length > 0)
                {
                    string[] filePrts = uploadedFile.docFile.FileName.Split(".");
                    string fileExtenstion = filePrts[filePrts.Length - 1];
                    string fileName = "";
                    fileName = await _service.documentRepository.UpdateAgreementDocumentDetail(uploadedFile.documentId, filePath, fileExtenstion, userName, companyID, comBranchID);
                    string fileFullPath = filePath + fileName;


                    using (Stream fileStream = new FileStream(fileFullPath, FileMode.Create))
                    {
                        await uploadedFile.docFile.CopyToAsync(fileStream);
                    }

                    return getResponse("File Uploaded.");
                }
                return getResponse("No file for upload.");
            }
            catch (Exception ex)
            {
                return getResponse(ex);
            }
        }
    }
}
