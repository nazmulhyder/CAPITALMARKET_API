using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class userAccessInfoDTO
    {
        public string resultStatus { get; set; }
        public string resultMsg { get; set; }
        public userInfoDTO userInfo { get; set; }
        public List<liceseeCompanyDTO> companiesAccess { get; set; }
        public List<moduleDTO> authorizedModules { get; set; }

        public List<string> permittedModules { get; set; }
        public List<string> permittedPrentMenues { get; set; }
        public List<string> permittedMenues { get; set; }

    }

    public class userInfoDTO
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
        public string userStatus { get; set; }
        public string transactionDate { get; set; }
        public string userCIFNo { get; set; }
    }

    public class moduleDTO
    {
        public string module { get; set; }
        public List<menuDetailDTO> authorizedMenu { get; set; }
    }
    public class menuDetailDTO
    {
        public string category { get; set; }
        public List<menuSubCategoryDTO> subCategories { get; set; }
    }
    public class menuSubCategoryDTO
    {
        public string menuName { get; set; }
        public string hasAcccess { get; set; }
    }
    public class liceseeCompanyDTO
    {
        public int companyID { get; set; }
        public string companyName { get; set; }
        public string hasAccess { get; set; }
        public List<licenseeComBranchDTO> branches { get; set; }
    }
    public class licenseeComBranchDTO
    {
        public int branchID { get; set; }
        public string branchName { get; set; }
        public string hasAccess { get; set; }
    }

    public class MenuAccessHelper
    {
        public int menuID { get; set; }
        public string moduleName { get; set; }
        public string parentMenu { get; set; }
        public string menuName { get; set; }
        public string menuURL { get; set; }
        public string hasAccess { get; set; }
    }
    public class CompanyAccessHelper
    {
        public int companyID { get; set; }
        public string licComShortName { get; set; }
        public string hasAccessOnCompany { get; set; }

    }
    public class UserAccessHelper
    {
        public int companyID { get; set; }
        public string licComShortName { get; set; }
        public string hasAccessOnCompany { get; set; }
        public int licBranchID { get; set; }
        public string licBranchName { get; set; }
        public string hasAccessOnComBranch { get; set; }
    }
}
