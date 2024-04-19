using AutoMapper;
using Dapper;
using Model.DTOs.Allocation;
using Model.DTOs.Document;
using Model.DTOs.FAQ;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.MarginRequest;
using System.ComponentModel.Design;
using Model.DTOs;

namespace Service.Implementation
{
    public class FAQRepository : IFAQRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public IMapper mapper;
        public FAQRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            mapper = _mapper;
        }

        public async Task<GetClientNameByProductAndAccountDto> GetClientNameByProductAndAccount(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID)
        {
            var values = new { CompanyID = CompanyID,BranchID = BranchID , AccountNo = AccountNo, ProductID = ProductID, FundID = FundID };
            var result = await _dbCommonOperation.ReadSingleTable<GetClientNameByProductAndAccountDto>("[CM_QueryFAQMember]", values);
            return result.FirstOrDefault();
        }

        public async Task<GetBasicCustomersDTO> CustomerListByName(int CompanyID, int BranchID, string CustomerName, string UserName)
        {
            GetBasicCustomersDTO getBasicCustomers = new GetBasicCustomersDTO();
            var values = new {  CustomerName = CustomerName, UserName = UserName };
            getBasicCustomers.customers = await _dbCommonOperation.ReadSingleTable<BasicCustomerDto>("[ListCustomerByName]", values);
            getBasicCustomers.resultStatus = getBasicCustomers.customers.Count() > 0 ? "Success" : "Fail";
            return getBasicCustomers;
       }

        public async Task<List<DocCheckListDTO>> GetDocumentCheckList(string TypeName, string PTypeCode)
        {
            var values = new { TypeName = TypeName, PTypeCode = PTypeCode };
            return await _dbCommonOperation.ReadSingleTable<DocCheckListDTO>("LstDocumentCheckList", values);
        }

        public async Task<List<RMDto>> ListRM(int CompanyID)
        {
            var values = new { CompanyID = CompanyID };
            return await _dbCommonOperation.ReadSingleTable<RMDto>("ListRMInformation", values);
        }

        public async Task<List<InstrumentFaceValueListDto>> ListInstrumentFaceVal(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID , BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<InstrumentFaceValueListDto>("CM_GetInstrumentFaceValueList", values);
        }
    }
}
