using Model.DTOs;
using Model.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserRepository
    {
        #region USERINFO&MENU
        public  Task<string> createUserAccount(UserAccountDto user);
        public UserAccountDto getUserAccountforCreate(string cif);
        public Task<userInfoDTO> GetUserInfoDTO(string loggedOnUser);
        public List<CompanyAccessHelper> GetCompanyList(string UserName, int CompanyID, int BranchID);
        public List<UserAccessHelper> GetCompanyBranchList(string UserName, int CompanyID, int BranchID);

        public Task<List<UserAuthorizedParrentMenuDto>> GetAuthorizedMenuList(string userName, int CompanyID, int BranchID, int ModuleID);

        public Task<userAccessInfoDTO> GetUserAccessInfoDTO(string userName, int CompanyID = 0, int BranchID = 0);

        public Task<List<Lic_QryUserDetailDTO>> GetUserInformation(string username);
        public bool WindowsAuthencation(string username);

        public List<MenuModuleListDto> GetAllMenuList(string userName);
        public Task<string> AddUpdateMenu(UserMenuDto menu, string userName);
        #endregion USERINFO&MENU

        #region MODULE-ROLE-MENU
        public Task<List<ModuleDto>> ModuleList();
        public Task<string> AddUpdateModule(ModuleDto data);
        public Task<List<RoleMasterDto>> RoleList();
        public Task<string> AddUpdateRole(RoleMasterDto data);
        public Task<string> setMenuPermissionInRole(int RoleID, int ModuleID, int MenuID, bool IsRemove);
        public List<UserDto> AllUserList();
        public List<CompanyDto> CompanyList();
        public  Task<string> setUserAuthoriy(UserDto data);
        #endregion MODULE-ROLE-MENU

        public Task<string> AddUpdateAgrGroup(AccountGroupReportPermissionDto data);
        public Task<List<AccountGroupReportPermissionDto>> GetAgrGrpList(string userName, int CompanyID, int BranchID);
        public AccountGroupReportPermissionDto GetAgrGrp(string userName, int CompanyID, int BranchID, int AccGrpID);
    }
}
