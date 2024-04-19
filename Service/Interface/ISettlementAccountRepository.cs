using Model.DTOs.SettlementAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface ISettlementAccountRepository
	{

		public Task<object> GetProductSettlementAccount(string UserName, int CompanyID, int BranchID, int ProductID, int ContractID);

		public Task<object> GetSettlementAccountList(string UserName, int CompanyID, int BranchID, string ListType);

		public Task<object> SaveUpdateSettlementAccount(string UserName, int CompanyID, int BranchID, SettlementAccountDto accountDto);
	}
}
