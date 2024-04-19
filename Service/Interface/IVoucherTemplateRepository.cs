using Model.DTOs.VoucherTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IVoucherTemplateRepository
	{
		public Task<object> InsertUpdateAccVoucherTmplateLink(string UserName, int CompanyID, int BranchID, List<VoucherTempleteLinkDto> linkdata);

		public Task<object> GetAllAccVoucherTmplateLink(string UserName, int CompanyID, int BranchID, int ProductID);

		public Task<object> GetAllLedgerHead(string UserName, int CompanyID, int BranchID, int ProductID);

		public Task<object> SaveUpdateLedgerTemplate(string UserName, int CompanyID, int BranchID, VoucherLedgerheadDto LedgerHead);
	}
}
