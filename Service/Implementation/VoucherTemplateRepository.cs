using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.VoucherTemplate;
using Utility;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Implementation
{
	public class VoucherTemplateRepository : IVoucherTemplateRepository
	{
		public readonly IDBCommonOpService _dbCommonOperation;
		public VoucherTemplateRepository(IDBCommonOpService dbCommonOperation)
		{
			_dbCommonOperation = dbCommonOperation;
		}

		public async Task<object> InsertUpdateAccVoucherTmplateLink(string UserName, int CompanyID, int BranchID, List<VoucherTempleteLinkDto> linkdata)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@TemplateLinkList", ListtoDataTableConverter.ToDataTable(linkdata).AsTableValuedParameter("Type_AccVoucherTmplateLink"));
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateAccVoucherTmplateLink", SpParameters);
		}

		public async Task<object> GetAllAccVoucherTmplateLink(string UserName, int CompanyID, int BranchID, int ProductID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ProductID", ProductID);

			DataSet DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccVoucherTmplateLink]", sqlParams);

			return new
			{
				AccountList = DataSets.Tables[0],
				TemplateList = DataSets.Tables[1],
				TemplateLinkList = DataSets.Tables[2],
			};
		}

		public async Task<object> SaveUpdateLedgerTemplate(string UserName, int CompanyID, int BranchID, VoucherLedgerheadDto LedgerHead)
		{

			var debitTemplate = LedgerHead.Templates.Where(c=>c.IsDebited == true).FirstOrDefault();
			var creditTemplate = LedgerHead.Templates.Where(c=>c.IsDebited == false).FirstOrDefault();

			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@LedgerHeadID", LedgerHead.LedgerHeadID);

			SpParameters.Add("@DebitVoucherTempleteID", debitTemplate.VoucherTempleteID);
			SpParameters.Add("@DebitAmount", debitTemplate.Amount);
			SpParameters.Add("@DebitIsPercentage", debitTemplate.IsPercentage);

			SpParameters.Add("@CreditVoucherTempleteID", creditTemplate.VoucherTempleteID);
			SpParameters.Add("@CreditAmount", creditTemplate.Amount);
			SpParameters.Add("@CreditIsPercentage", creditTemplate.IsPercentage);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateACCVoucherTemplete", SpParameters);
		}

		public async Task<object> GetAllLedgerHead(string UserName, int CompanyID, int BranchID, int ProductID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ProductID", ProductID);

			DataSet DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadForVoucherTemplate]", sqlParams);

			List<VoucherLedgerheadDto> LedgerHeadList = CustomConvert.DataSetToList<VoucherLedgerheadDto>(DataSets.Tables[0]);

			List<VoucherLedgerheadDto> LeafLedgerHeadList = new List<VoucherLedgerheadDto>();

			foreach(var item in LedgerHeadList)
			{
				if(item.ParentGLCode != null && LedgerHeadList.Where(c=>c.ParentGLCode == item.GLCode).Count() == 0) LeafLedgerHeadList.Add(item);
			}

			SqlParameter[] Params = new SqlParameter[4];

			Params[0] = new SqlParameter("@UserName", UserName);
			Params[1] = new SqlParameter("@CompanyID", CompanyID);
			Params[2] = new SqlParameter("@BranchID", BranchID);
			Params[3] = new SqlParameter("@ProductID", ProductID);

			DataSet Sets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListACCVoucherTemplete]", sqlParams);
			
			List<VoucherTemplateDto> TemplateList = CustomConvert.DataSetToList<VoucherTemplateDto>(Sets.Tables[0]);

			foreach(var item in LeafLedgerHeadList)
			{
				var templates = TemplateList.Where(c => c.LedgerHeadID == item.LedgerHeadID).ToList();
				if(templates.Count() > 0) { item.Templates = templates; }
				else
				{
					item.Templates = new List<VoucherTemplateDto>();

					item.Templates.Add(new VoucherTemplateDto
					{
						LedgerHeadID = item.LedgerHeadID,
						VoucherTempleteID = 0,
						IsDebited = true,
						MakeDate = DateTime.Now,
						Maker = UserName,
						Amount = 0,
						ApprovalStatus = "Unapproved",
						ApprovalSetID = 0,
						IsPercentage = false
					});
					item.Templates.Add(new VoucherTemplateDto
					{
						LedgerHeadID = item.LedgerHeadID,
						VoucherTempleteID = 0,
						IsDebited = false,
						MakeDate = DateTime.Now,
						Maker = UserName,
						Amount = 0,
						ApprovalStatus = "Unapproved",
						ApprovalSetID = 0,
						IsPercentage = false
					});
				}
			}

			return LeafLedgerHeadList;
		}
	}
}
