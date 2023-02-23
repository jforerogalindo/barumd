using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly Model.Bender.BenderContext _context = new();
        private List<Dto.Roles.GetData> objGetData = new();

        //GET  Roles/GetAll
        [HttpGet("GetAll")]
        public List<Dto.Roles.GetData> GetAll()
        {
            try
            {
                List<Model.Bender.Rol> ObjData = _context.Rols.OrderBy(x => x.Idrol).ToList();
                foreach (var data in ObjData)
                {
                    Dto.Roles.GetData row = new()
                    {
                        RolId = Convert.ToInt64(data.Idrol),
                        RolName = data.Rolname
                    };
                    objGetData.Add(row);
                }
            }
            catch (Exception ex) { }
            return objGetData;
        }
    }
}
