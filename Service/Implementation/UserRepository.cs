using Dapper;
using Model.DTOs;
using Model.DTOs.BrokerageCommision;
using Model.DTOs.User;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class UserRepository : IUserRepository
    {

        public readonly IDBCommonOpService _dbCommonOperation;
        public UserRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }
       
        public async Task<string> createUserAccount(UserAccountDto user)
        {
            string sp = "CreateUserAccount";

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@MemberCode", user.MemberCode);
            SpParameters.Add("@MemberName", user.MemberName);
            SpParameters.Add("@Designation", user.Designation);
            SpParameters.Add("@Company", user.Company);
            SpParameters.Add("@Branch", user.Branch);
            SpParameters.Add("@Team", user.Team);
            SpParameters.Add("@UserName", user.UserName);
            SpParameters.Add("@Role", 0);
            SpParameters.Add("@IsRM", user.IsRM);
            var res = await  _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            return res;
        }
        public UserAccountDto getUserAccountforCreate(string cif)
        {
            UserAccountDto user = new UserAccountDto();
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CIF", cif)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("user_getEmployeeForUserCreate", sqlParams);

            user = CustomConvert.DataSetToList<UserAccountDto>(DataSets.Tables[0]).FirstOrDefault();

            return user;
        }
        public async Task<userInfoDTO> GetUserInfoDTO(string loggedOnUser)
        {
            userInfoDTO LoggedUser = new userInfoDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@ClientIPAddress", "test"),
                new SqlParameter("@UserName", loggedOnUser)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("Lic_QryAuthenticationInformationtest", sqlParams);

            LoggedUser = CustomConvert.DataSetToList<userInfoDTO>(DataSets.Tables[1]).FirstOrDefault();

            return LoggedUser;
        }

        public List<CompanyAccessHelper> GetCompanyList(string UserName, int CompanyID, int BranchID)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[Lic_LstUserAuthority]", sqlParams);
            List<CompanyAccessHelper> companyAccesses = CustomConvert.DataSetToList<CompanyAccessHelper>(DataSets.Tables[0]);
            return companyAccesses.Where(c => c.hasAccessOnCompany == "Yes").ToList();
        }

        public List<UserAccessHelper> GetCompanyBranchList(string UserName, int CompanyID, int BranchID)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UserName",UserName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[Lic_LstUserAuthority]", sqlParams);
            List<UserAccessHelper> ComBranchAccessInfo = CustomConvert.DataSetToList<UserAccessHelper>(DataSets.Tables[1]);
            return ComBranchAccessInfo.Where(c => c.companyID == CompanyID && c.hasAccessOnComBranch == "Yes").ToList();
        }

        public List<MenuModuleListDto> GetAllMenuList(string userName)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
          {};
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("user_ListAllMenu", sqlParams);
            var AllMenu = CustomConvert.DataSetToList<UserAuthorizedParrentMenuDto>(DataSets.Tables[0]).ToList();


            var Modules = AllMenu
                       .Select(m => new { m.ModuleID, m.ModuleName })
                       .Distinct()
                       .ToList();

            List<MenuModuleListDto> moduleList = new List<MenuModuleListDto>();


            foreach (var module in Modules)
            {
                var moduleListDto = new MenuModuleListDto
                {
                    ModuleID = module.ModuleID,
                    ModuleName = module.ModuleName,
                    MenuList = AllMenu.Where(r => r.ModuleID == module.ModuleID).ToList()
                };
                moduleList.Add(moduleListDto);
            }


            return moduleList;

        }
        public async Task<List<UserAuthorizedParrentMenuDto>> GetAuthorizedMenuList(string userName, int CompanyID, int BranchID, int ModuleID)
        {
            List<UserAuthorizedParrentMenuDto> MenuList = new List<UserAuthorizedParrentMenuDto>();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UserName",userName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("user_ListAuthorizedMenu", sqlParams);

            MenuList = CustomConvert.DataSetToList<UserAuthorizedParrentMenuDto>(DataSets.Tables[0]).Where(m => m.ModuleID == ModuleID).ToList();

            List<UserAuthorizedParrentMenuDto> ChildMenuList = CustomConvert.DataSetToList<UserAuthorizedParrentMenuDto>(DataSets.Tables[1]).ToList();

            foreach (var menu in MenuList) menu.ChildMenuList = ChildMenuList.Where(c => c.ParentMenuID == menu.MenuID).ToList();

            return MenuList;

        }

        private List<liceseeCompanyDTO> LoadCompanyNBranchAccess(List<CompanyAccessHelper> companyAccesses, List<UserAccessHelper> ComBranchAccessInfo)
        {
            List<liceseeCompanyDTO> companiesAccess = new List<liceseeCompanyDTO>();
            foreach (var com in companyAccesses)
            {
                liceseeCompanyDTO objCompany = new liceseeCompanyDTO { companyID = com.companyID, companyName = com.licComShortName, hasAccess = com.hasAccessOnCompany };
                var comBranches = from comBranch in ComBranchAccessInfo
                                  where comBranch.companyID == com.companyID
                                  select comBranch;
                objCompany.branches = new List<licenseeComBranchDTO>();
                foreach (var br in comBranches)
                {
                    objCompany.branches.Add(new licenseeComBranchDTO { branchID = br.licBranchID, branchName = br.licBranchName, hasAccess = br.hasAccessOnComBranch });
                }
                companiesAccess.Add(objCompany);
            }
            return companiesAccess;
        }

        private List<moduleDTO> LoadMenuAccess(List<MenuAccessHelper> AuthorizedMenu)
        {
            List<moduleDTO> authorizedModules = new List<moduleDTO>();
            var distModules = (from authMenu in AuthorizedMenu
                               select authMenu.moduleName).Distinct();

            foreach (var mod in distModules)
            {
                moduleDTO modDTO = new moduleDTO();
                modDTO.module = mod;
                modDTO.authorizedMenu = new List<menuDetailDTO>();

                var distParMenues = (from authMenu in AuthorizedMenu
                                     select authMenu.parentMenu).Distinct();

                foreach (var parMenu in distParMenues)
                {
                    var menues = (from menu in AuthorizedMenu
                                  where menu.parentMenu == parMenu && menu.moduleName == mod
                                  select menu);
                    menuDetailDTO mDetail = new menuDetailDTO();// { module = menue.moduleName, category = menue.menuName };
                    mDetail.subCategories = new List<menuSubCategoryDTO>();
                    string module = "", category = "";

                    foreach (var menue in menues)
                    {
                        module = menue.moduleName;
                        category = menue.parentMenu;
                        menuSubCategoryDTO subMenu = new menuSubCategoryDTO { menuName = menue.menuName, hasAcccess = menue.hasAccess };
                        if (menue.hasAccess == "Yes") mDetail.subCategories.Add(subMenu);
                        //userAccessInfo.authorizedMenu.Add(new menuDetailDTO { category=menue.parentMenu,module=menue.me})
                    }
                    mDetail.category = category;
                    modDTO.authorizedMenu.Add(mDetail);
                }
                authorizedModules.Add(modDTO);
            }
            return authorizedModules;
        }

        public async Task<userAccessInfoDTO> GetUserAccessInfoDTO(string userName, int CompanyID = 0, int BranchID = 0)
        {
            userAccessInfoDTO userAccessInfo = new userAccessInfoDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UserName",userName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[Lic_LstUserAuthority]", sqlParams);

            List<CompanyAccessHelper> companyAccesses = CustomConvert.DataSetToList<CompanyAccessHelper>(DataSets.Tables[0]);
            List<UserAccessHelper> ComBranchAccessInfo = CustomConvert.DataSetToList<UserAccessHelper>(DataSets.Tables[1]);
            userAccessInfo.companiesAccess = LoadCompanyNBranchAccess(companyAccesses, ComBranchAccessInfo);

            List<MenuAccessHelper> AuthorizedMenu = CustomConvert.DataSetToList<MenuAccessHelper>(DataSets.Tables[2]);
            userAccessInfo.authorizedModules = LoadMenuAccess(AuthorizedMenu);

            userAccessInfo.permittedModules = CustomConvert.DataSetToStringList(DataSets.Tables[3]);
            userAccessInfo.permittedPrentMenues = CustomConvert.DataSetToStringList(DataSets.Tables[4]);
            userAccessInfo.permittedMenues = CustomConvert.DataSetToStringList(DataSets.Tables[5]);

            return userAccessInfo;
        }

        public async Task<List<Lic_QryUserDetailDTO>> GetUserInformation(string username)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@LoggedOnUser", username)
            };
            return await _dbCommonOperation.ReadSingleTable<Lic_QryUserDetailDTO>("Lic_QryUserDetail @LoggedOnUser", sqlParams);
        }
        public bool WindowsAuthencation(string username)
        {
            var user = GetUserInformation(username);
            return user == null ? false : true;
        }


        #region MODULE-ROLE-MENU
        public async Task<List<ModuleDto>> ModuleList()
        {
            return await _dbCommonOperation.ReadSingleTable<ModuleDto>("user_ListModule",null);
        }
        public async Task<string> AddUpdateModule(ModuleDto data)
        {
            if (data.ModuleID == 0 || data.ModuleID == null)
            {
                string sp = "user_InsertModule";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@ModuleName", data.ModuleName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            else
            {
                string sp = "user_UpdateModule";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@ModuleID", data.ModuleID);
                SpParameters.Add("@ModuleName", data.ModuleName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }

        }

        public async Task<string> AddUpdateMenu(UserMenuDto menu, string userName)
        {
            
            if (menu.MenuID > 0)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@MenuIcon", menu.IconString);
                SpParameters.Add("@OrderID", menu.OrderID);
                SpParameters.Add("@MenuID", menu.MenuID);
                SpParameters.Add("@MenuName", menu.MenuName);
                SpParameters.Add("@MenuURL", menu.MenuURL);
                SpParameters.Add("@ModuleID", menu.ModuleID);
                SpParameters.Add("@ParentMenuID", menu.ParentMenuID);
                SpParameters.Add("@MenuLevel", menu.MenuLevel);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("user_UpdateMenu", SpParameters);
            }
            else
            {

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@MenuIcon", menu.IconString);
                SpParameters.Add("@OrderID", menu.OrderID);
                SpParameters.Add("@MenuName", menu.MenuName);
                SpParameters.Add("@MenuURL", menu.MenuURL);
                SpParameters.Add("@ModuleID", menu.ModuleID);
                SpParameters.Add("@ParentMenuID", menu.ParentMenuID);
                SpParameters.Add("@MenuLevel", menu.ParentMenuID > 0 ? 2 : 1);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("user_InsertMenu", SpParameters);
            }



        }

        public async Task<string> AddUpdateRole(RoleMasterDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@RoleID", data.RoleID);
            SpParameters.Add("@RoleName", data.RoleName);
            SpParameters.Add("@Purpse", data.Purpose);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("user_InsertUpdateRole", SpParameters);
        }
        public async Task<List<RoleMasterDto>> RoleList()
        {
            List<RoleMasterDto> roles = new List<RoleMasterDto>();
            SqlParameter[] sqlParams = new SqlParameter[] { };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("user_ListRole", sqlParams);
            List<RoleDetailDto> AllRoleList = CustomConvert.DataSetToList<RoleDetailDto>(DataSets.Tables[0]).ToList();


            var RoleMasters = AllRoleList
                        .Select(m => new { m.RoleID, m.RoleName })
                        .Distinct()
                        .ToList();

            var Modules = AllRoleList
                       .Select(m => new { m.ModuleID, m.ModuleName })
                       .Distinct()
                       .ToList();

            foreach (var item in RoleMasters)
            {
                RoleMasterDto role = new RoleMasterDto();
                List<ModuleListDto> moduleList = new List<ModuleListDto>();

                role.RoleID = item.RoleID;
                role.RoleName = item.RoleName;

                foreach (var module in Modules)
                {
                    var moduleListDto = new ModuleListDto
                    {
                        ModuleID = module.ModuleID,
                        ModuleName = module.ModuleName,
                        roleDetailList = AllRoleList.Where(r => r.RoleID == item.RoleID && r.ModuleID == module.ModuleID).ToList()
                    };
                    moduleList.Add(moduleListDto);

                    foreach(var roledet in moduleListDto.roleDetailList)
                    {
                        if(roledet.ParentMenuID  != null)
                        {
                            var parrenmenu = AllRoleList.Where(r => r.MenuID == roledet.ParentMenuID).FirstOrDefault();
                            if(parrenmenu != null)
                            roledet.ParentMenuName = AllRoleList.Where(r => r.MenuID == roledet.ParentMenuID).FirstOrDefault().MenuName;
                        }
                        
                    }
                }
                role.ModuleList = moduleList;
                roles.Add(role);
            }


            return roles;
        }


        public async Task<string> setMenuPermissionInRole(int RoleID, int ModuleID, int MenuID, bool IsRemove)
        {

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@RoleID", RoleID);
            SpParameters.Add("@ModuleID", ModuleID);
            SpParameters.Add("@MenuID", MenuID);
            SpParameters.Add("@IsRemove", IsRemove);

            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("user_setMenuPermissionInRole", SpParameters);
        }

        public List<CompanyDto> CompanyList()
        {
            List<CompanyDto> CompanyList = new List<CompanyDto>();
            SqlParameter[] sqlParams = new SqlParameter[] { };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("user_ListCompanyBranch", sqlParams);

            CompanyList = CustomConvert.DataSetToList<CompanyDto>(DataSets.Tables[0]).ToList();

            foreach (var com in CompanyList)
            {
                com.BranchList = CustomConvert.DataSetToList<BranchDto>(DataSets.Tables[1]).ToList().Where(b => b.CompanyID == com.CompanyID).ToList();
            }

            return CompanyList;
        }

        public List<UserDto> AllUserList()
        {
            List<UserDto> UserList = new List<UserDto>();
            SqlParameter[] sqlParams = new SqlParameter[] { };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("user_ListAllUser", sqlParams);
            UserList = CustomConvert.DataSetToList<UserDto>(DataSets.Tables[0]).ToList();

            List<CompanyDto> Companys = CompanyList();
            var Authorites = _dbCommonOperation.FindMultipleDataSetBySP("user_ListUserAuthorise", sqlParams);
            List<UserAuthority> AllAuthority = CustomConvert.DataSetToList<UserAuthority>(Authorites.Tables[0]).ToList();

            foreach (var user in UserList)
            {
                user.UserPermissionList = AllAuthority.Where(u => u.UserID == user.UserID).ToList();
            }


            return UserList;
        }

        public async Task<string> setUserAuthoriy(UserDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@AuthorityList", ListtoDataTableConverter.ToDataTable(data.UserPermissionList).AsTableValuedParameter("Type_UserAuthority"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("user_InsertUpdateUserAuthority", SpParameters);
        }

        #endregion

        #region Account Grp
        public async Task<string> AddUpdateAgrGroup(AccountGroupReportPermissionDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@AgrGroupID", data.AgrGroupID);
            SpParameters.Add("@GroupName", data.GroupName);
            SpParameters.Add("@Description", data.Description);
            SpParameters.Add("@AssignContracts", ListtoDataTableConverter.ToDataTable(data.AgrGrpContracts).AsTableValuedParameter("Type_Contracts"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("user_InsertUpdateAgrGroupAccPermission", SpParameters);
        }

        public async Task<List<AccountGroupReportPermissionDto>> GetAgrGrpList(string userName,int CompanyID, int BranchID)
        {
            var values = new { userName = userName ,CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<AccountGroupReportPermissionDto>("user_AgrGroupPermissionList", values);
        }

        public AccountGroupReportPermissionDto GetAgrGrp(string userName, int CompanyID, int BranchID, int AccGrpID)
        {
            AccountGroupReportPermissionDto accountGroupReportPermission = new AccountGroupReportPermissionDto();
            //List<AssignAgrGrpToContractDto> assignAgrGrpToContracts = new List<AssignAgrGrpToContractDto>();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@userName", userName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@AgrGroupID", AccGrpID)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("user_GetAgrGroupPermission", sqlParams);

            accountGroupReportPermission = CustomConvert.DataSetToList<AccountGroupReportPermissionDto>(DataSets.Tables[0]).FirstOrDefault();
            accountGroupReportPermission.AgrGrpContracts = CustomConvert.DataSetToList<AssignAgrGrpToContractDto>(DataSets.Tables[1]).ToList();

            return accountGroupReportPermission;
        }
        #endregion
    }
}
