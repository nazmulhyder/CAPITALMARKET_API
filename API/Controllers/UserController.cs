using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.DTOs.User;
using System.Security.Principal;
using Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<UserController> _logger;
        public UserController(IService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        private string LoggedOnUser()
        {
            var principal = HttpContext.User;
            _logger.LogInformation("Principal: {0}", principal.Identity.Name);
            var windowsIdentity = principal?.Identity as WindowsIdentity;
            //var userName = WindowsIdentity.GetCurrent().Name;
            string loggedOnUser = windowsIdentity.Name;
            if (loggedOnUser.Contains('\\'))
                loggedOnUser = loggedOnUser.Split("\\")[1];
            return loggedOnUser;
        }


        [HttpPost]
        [Route("createUserAccount")]
        public async Task<IActionResult> createUserAccount(UserAccountDto user)
        {
            try
            {
                return getResponse(_service.Users.createUserAccount(user));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("getUserAccountforCreate/{cif}")]
        public IActionResult getUserAccountforCreate(string cif)
        {
            try
            {
                var data = _service.Users.getUserAccountforCreate(cif);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUserInformation")]
        public async Task<IActionResult> GetUserInformation()
        {
            try
            {
                var data = await _service.Users.GetUserInfoDTO(LoggedOnUser());
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetCompanyList")]
        public async Task<IActionResult> GetCompanyList()
        {
            try
            {
                var data = _service.Users.GetCompanyList(LoggedOnUser(), 0, 0);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetCompanyBranchList/{CompanyID}")]
        public async Task<IActionResult> GetCompanyBranchList(int CompanyID)
        {
            try
            {
                var data = _service.Users.GetCompanyBranchList(LoggedOnUser(), CompanyID, 0);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetAuthorizedMenuList/{CompanyID}/{BranchID}/{ModuleID}")]
        public async Task<IActionResult> GetAuthorizedMenuList(int CompanyID, int BranchID, int ModuleID)
        {
            try
            {
                var data = await _service.Users.GetAuthorizedMenuList(LoggedOnUser(), CompanyID, BranchID, ModuleID);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetAllMenuList")]
        public async Task<IActionResult> GetAllMenuList()
        {
            try
            {
                return getResponse(_service.Users.GetAllMenuList(LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost]
        [Route("AddUpdateMenu")]
        public async Task<IActionResult> AddUpdateMenu(UserMenuDto menu)
        {
            try
            {
                return getResponse(_service.Users.AddUpdateMenu(menu, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Authentication/{CompanyID}/{BranchID}")]
        public IActionResult Authentication(int CompanyID = 0, int BranchID = 0)
        {
            try
            {
                string loggedOnUser = LoggedOnUser();
                var req = Request;
                userInfoDTO userInformation = _service.Users.GetUserInfoDTO(loggedOnUser).Result;

                userAccessInfoDTO usrAccessInfoDto = _service.Users.GetUserAccessInfoDTO(loggedOnUser, CompanyID, BranchID).Result;
                usrAccessInfoDto.userInfo = userInformation;
                usrAccessInfoDto.resultStatus = "Success";
                return getResponse(usrAccessInfoDto);
            }
            catch (Exception ex)
            {
                userAccessInfoDTO usrAccessInfoDto = new userAccessInfoDTO { resultStatus = "Fail", resultMsg = ex.Message };
                return BadRequest(usrAccessInfoDto);
            }
        }


        #region MODULE-ROLE-MENU

        [HttpGet]
        [Route("GetModuleList")]
        public async Task<IActionResult> ModuleList()
        {
            try
            {
                return getResponse(await _service.Users.ModuleList());
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost]
        [Route("AddUpdateModule")]
        public async Task<IActionResult> AddUpdateModule(ModuleDto data)
        {
            try
            {
                return getResponse(await _service.Users.AddUpdateModule(data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost]
        [Route("AddUpdateRole")]
        public IActionResult AddUpdateRole(RoleMasterDto data)
        {
            try
            {
                return getResponse(_service.Users.AddUpdateRole(data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpGet]
        [Route("RoleList")]
        public async Task<IActionResult> RoleList()
        {
            try
            {
                return getResponse(await _service.Users.RoleList());
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("setMenuPermissionInRole/{RoleID}/{ModuleID}/{MenuID}/{IsRemove}")]
        public IActionResult setMenuPermissionInRole(int RoleID, int ModuleID, int MenuID, bool IsRemove)
        {
            try
            {
                return getResponse(_service.Users.setMenuPermissionInRole(RoleID, ModuleID, MenuID, IsRemove));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("CompanyList")]
        public IActionResult CompanyList()
        {
            try
            {
                return getResponse(_service.Users.CompanyList());
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("AllUserList")]
        public IActionResult AllUserList()
        {
            try
            {
                return getResponse(_service.Users.AllUserList());
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost]
        [Route("AddUpdateUserAuthority")]
        public IActionResult AddUpdateUserAuthority(UserDto data)
        {
            try
            {
                return getResponse(_service.Users.setUserAuthoriy(data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion


        [HttpPost]
        [Route("AddUpdateAgrGroup")]
        public async Task<IActionResult> AddUpdateAgrGroup(AccountGroupReportPermissionDto data)
        {
            try
            {
                return getResponse(await _service.Users.AddUpdateAgrGroup(data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AccountGrpList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAgrGrpList(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.Users.GetAgrGrpList(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetAgrGrp/{CompanyID}/{BranchID}/{AgrGrpID}")]
        public IActionResult GetAgrGrp(int CompanyID, int BranchID, int AgrGrpID)
        {
            try
            {
                return getResponse(_service.Users.GetAgrGrp(LoggedOnUser(), CompanyID, BranchID,AgrGrpID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
