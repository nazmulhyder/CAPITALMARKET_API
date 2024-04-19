using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Utility;

namespace Api.Controllers
{
    public class ControllerBase : Controller
    {
       
        protected ActionResult getResponse(object Data, string Message = "")
        {
            CommonResponseDto response = new CommonResponseDto();
            response.Status = "OK";
            response.Message = Message;
            response.Data = Data;
            Response.StatusCode = StatusCodes.Status200OK;
            return Content(new JSONSerialize().getJSONSFromObject(response, true), "application/json");
        }

        protected ActionResult getResponse(Exception ex)
        {
            CommonResponseDto response = new CommonResponseDto();
            response.Status = "ERROR";
            response.Message = getAllErrorString(ex);
            response.Data = ex;
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return Content(new JSONSerialize().getJSONSFromObject(response, true), "application/json");
        }

        public static string getAllErrorString(Exception ex)
        {
            try
            {
                string Message = "";

                if (ex.InnerException != null)
                {
                    var iEx = ex.InnerException;
                    if (!string.IsNullOrEmpty(iEx.Message))
                    {
                        Message += iEx.Message;
                    }
                }
                if (ex.Message != null) Message += ex.Message;
                return Message;
            }
            catch { return ""; }
        }

    }
}
