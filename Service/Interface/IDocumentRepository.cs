using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDocumentRepository
    {
        public Task<string> UpdateDocumentDetail(int memberDocumentID, string docuentPath, string docFileExtension, string userName, int companyID = 0, int comBranchID = 0);
        public Task<string> UpdateAgreementDocumentDetail(int agreementDocumentID, string docuentPath, string docFileExtension, string userName, int companyID = 0, int comBranchID = 0);
    }
}
