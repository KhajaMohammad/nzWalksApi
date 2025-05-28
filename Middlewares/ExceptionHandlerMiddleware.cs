using System.Net;

namespace nzWalksApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext ) {

            try { await next(httpContext); }
            catch (Exception e)
            { //log the exceptions

                var errorId = Guid.NewGuid();

                logger.LogError(e,$"{errorId} : {e.Message}");

                // return a ustom error response 

                httpContext.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new {
                    Id = errorId,
                    ErrorMessage="Something went Wrong, We are checking it "
                    };

                await httpContext.Response.WriteAsJsonAsync( error );
             

            }
        
        }


    }
}
