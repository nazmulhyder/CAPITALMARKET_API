using Model.DTOs;
using Model.DTOs.Allocation;
using Model.DTOs.Document;
using Model.DTOs.FAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFAQRepository
    {
        public Task<GetClientNameByProductAndAccountDto> GetClientNameByProductAndAccount(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID);
        public Task<GetBasicCustomersDTO> CustomerListByName(int CompanyID, int BranchID, string CustomerName, string UserName);
        public Task<List<DocCheckListDTO>> GetDocumentCheckList(string docTypeName, string pTypeCode);
        public Task<List<RMDto>> ListRM(int CompanyID);
        public Task<List<InstrumentFaceValueListDto>> ListInstrumentFaceVal(int CompanyID, int BranchID);
    }
}
