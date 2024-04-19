using Microsoft.AspNetCore.Http;
using Model.DTOs.NAVFileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface INAVRepository
	{

		#region MFReceive
		public Task<object> DeliverMFInstrument(string UserName, int CompanyID, int BranchID, MFReceiveDetailDto data);

		public Task<object> ReceiveMFInstrument(string UserName, int CompanyID, int BranchID, MFReceiveDetailDto data);

		public Task<object> GetListMFInstrumentForDelivery(string UserName, int CompanyID, int BranchID, string AccountNo);

		public Task<object> GetListMFInstrumentForReceive(string UserName, int CompanyID, int BranchID, string AccountNo);

		#endregion MFReceive

		#region NAVFile
		public Task<object> GetNavFileValidation(string UserName, int CompanyID, int BranchID, List<NAVFileDetailDto> data);

		public Task<object> SaveNavFile(string UserName, int CompanyID, int BranchID, IFormCollection data);

		public Task<object> GetNavFileList(string UserName, int CompanyID, int BranchID, string ListType);

		public Task<object> GetNavFileDetail(string UserName, int CompanyID, int BranchID, int FileID);

		public Task<object> ApproveNavFile(string UserName, int CompanyID, int BranchID, NAVFileApproveDto data);

		#endregion NAVFile
	}
}
