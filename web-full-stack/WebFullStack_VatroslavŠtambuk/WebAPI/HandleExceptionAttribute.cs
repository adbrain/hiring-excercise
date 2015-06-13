using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Diagnostics;

namespace WebAPI
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public Type Type { get; set; }
        public HttpStatusCode Status { get; set; }

        public override void OnException(HttpActionExecutedContext context)
        {
            var ex = context.Exception;
            if (ex.GetType() is Type)
            {
                while (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                {
                    // should never really happen though!
                    if (ex.InnerException == null)
                    {
                        break;
                    }

                    ex = ex.InnerException;
                }

                StackTrace st = new StackTrace(ex, true);

                // 
                // Get the first stack frame with a filename defined. Start from the zeroth stack frame which will land us to the innermost stack frame corresponding to the source code.
                //
                string debugInfo = String.Empty;
                foreach (var frame in st.GetFrames())
                {
                    if (!string.IsNullOrEmpty(frame.GetFileName()))
                    {

                        // Get the file name
                        string fileName = frame.GetFileName();

                        // Get the method name
                        string methodName = frame.GetMethod().Name;

                        // Get the line number from the stack frame
                        int line = frame.GetFileLineNumber();
                        debugInfo = String.Format("Filename: '{0}', method: '{1}', line: '{2}'. ", fileName, methodName, line);
                        break;
                    }
                }

                var response = context.Request.CreateResponse<string>(Status, debugInfo + ex.Message);
                throw new HttpResponseException(response);
            }
        }
    }
}
