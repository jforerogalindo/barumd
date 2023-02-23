using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Response
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public long Identity { get; set; }

        public Response SelectedResponse(int code, long identidad = 99)
        {
            Response objReturn = new();
            objReturn.Code = code;
            objReturn.Identity = identidad;
            switch (code)
            {
                case 1:
                    objReturn.Description = "Exitoso";
                    break;
                case 3:
                    objReturn.Description = "Rechazada";
                    break;
                case 99:
                    objReturn.Description = "Fallido";
                    break;
                default:
                    objReturn.Code = 99;
                    objReturn.Description = "Fallido";
                    break;
            }

            return objReturn;
        }
    }
}
