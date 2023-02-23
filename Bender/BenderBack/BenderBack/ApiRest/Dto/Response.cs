using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Dto
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Response SelectedResponse(bool Success, string Message="")
        {
            Response objReturn = new();

            objReturn.Success = Success;
            objReturn.Message = Success == true ? "Exitoso" : Message == "" ? "" : Message;
            return objReturn;
        }
    }
}
