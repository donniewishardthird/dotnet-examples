using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using System.Text;

namespace ActionFilterApi.Filters
{
    public class ApiLogActionFilter : IActionFilter
    {
        //private readonly IDbContextFactory<YourDbContextt> _dbFactory;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor, the DB Context is commented out, enter your details in place and uncomment to have access to your database.
        /// </summary>
        /// <param name="dbContext"></param>
        public ApiLogActionFilter(/*IDbContextFactory<YourDbContext> dbContext,*/
            ILogger<ApiLogActionFilter> logger)
        {
            _logger = logger;
            //_dbFactory = dbContext;
        }

        /// <summary>
        /// Catch incoming calls and log them
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string ipAddress = filterContext.HttpContext.Connection.RemoteIpAddress?.ToString();
                string method = filterContext.HttpContext.Request.Method;
                StringBuilder requestBodyBuilder = new StringBuilder();
                string actionName = ((ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
                string controllerName = ((ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;
                var dateTime = DateTime.Now;
                var action = string.Format("{0}/{1}", controllerName, actionName);

                //attempt to read the body if it is present
                if (filterContext.HttpContext.Request.ContentLength.HasValue &&
                    filterContext.HttpContext.Request.ContentLength > 0)
                {
                    if (filterContext.HttpContext.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                    {
                        //in this sample we are only worried about JSON, feel free to add other content type actions
                        if (filterContext.HttpContext.Request.ContentType != null && 
                                filterContext.HttpContext.Request.ContentType.Equals("application/json"))
                        {
                            if (filterContext.ActionArguments.Count > 0)
                            {
                                foreach (object o in filterContext.ActionArguments)
                                {
                                    requestBodyBuilder.Append(string.Format("{0} ", JsonSerializer.Serialize(o)));
                                }
                            }
                        }
                    }

                    var guid = Guid.NewGuid();

                    #region Add your DB stuff here.  You can log the request to yor db here
                    //using YourDbContext db = _dbFactory.CreateDbContext();
                    //..call a stored procedure here to save data?
                    #endregion

                    //add the guid to the request headers, this can be pulled later to log the response also
                    filterContext.HttpContext.Request.Headers.Append("ApiRequestId", guid.ToString());

                    _logger.LogInformation("guid: {0}, action: {1}, time: {2}, data: {3}",
                        guid, action, dateTime, requestBodyBuilder.ToString());
                }
            }
            catch (Exception x)
            {
                _logger.LogError("unable to intercept request: {0}", x.ToString());
            }
        }

        /// <summary>
        /// Catch outgoing calls and log them
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var responseData = string.Empty;

            try
            {
                //get the result and make JSON
                if (filterContext.Result is ObjectResult objectResult)
                {
                    // Check if the result is an ObjectResult (e.g., JsonResult, ObjectResult).
                    // You can access the data and serialize it to JSON.
                    responseData = JsonSerializer.Serialize(objectResult.Value);
                }

                //get the http status code for the response
                var httpStatus = ((int)filterContext.HttpContext.Response.StatusCode).ToString();

                //get the request id (correlation id) from the header, this was set in the OnActionExecuting above
                StringValues requestIdHeader = new StringValues();
                filterContext.HttpContext.Request.Headers
                    .TryGetValue("ApiRequestId", out requestIdHeader);

                //ensure there is a header
                if (requestIdHeader.Count() == 0)
                {
                    _logger.LogInformation("unable to get corresponding request for response, skipping");
                    return;
                }

                //get the actu
                var correlationId = requestIdHeader.First();

                #region Add your DB stuff here.  You can log the response for a particular request to yor db here.
                //using YourDbContext db = _dbFactory.CreateDbContext();
                //..call a stored procedure here to save data?
                #endregion

                _logger.LogInformation("requestId: {0}, status: {1}, data: {2}",
                    correlationId, filterContext.HttpContext.Response.StatusCode, responseData);
            }
            catch (Exception x)
            {
                _logger.LogError("unable to intercept response: {0}", x.ToString());
            }
        }
    }
}