using Microsoft.AspNetCore.Mvc;
using ProductsManager.Services;

namespace ProductsManager.WebApi.ServiceResponce
{
    public static class ServiceResponseExtensions
    {
        public static IActionResult ToJsonResult(this ServiceResponse response)
        {
            IActionResult result;
            if (response.IsSuccess)
            {
                result = new StatusCodeResult(response.StatusCode);
            }
            else
            {
                result = new JsonResult(response.Errors) { StatusCode = response.StatusCode };
            }

            return result;
        }

        public static JsonResult ToJsonResult<TResponse>(this ServiceResponse<TResponse> response)
        {
            JsonResult result;
            if (response.IsSuccess)
            {
                result = new JsonResult(response.Response);
            }
            else
            {
                result = new JsonResult(response.Errors);
            }

            result.StatusCode = response.StatusCode;
            return result;
        }
    }
}
