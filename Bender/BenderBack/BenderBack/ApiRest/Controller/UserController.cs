using ApiRest.Dto.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml;

namespace ApiRest.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Model.Bender.BenderContext _context = new();
        private List<GetData> objGetData = new();
        private GetData objGetDataObject = new();
        private LoginResponse loginResponse = new(){Success = false};

        //GET  User/GetAll
        [HttpGet("GetAll")]
        public List<GetData> GetAll()
        {
            try
            {
                List<Model.Bender.User> ObjData = _context.Users.OrderBy(x => x.Iduser).ToList();
                foreach (var data in ObjData)
                {
                    GetData row = new()
                    {
                        Identification = Convert.ToString(data.Iduser),
                        Name = data.Names,
                        Password = data.Password,
                        RolId = data.RolIdrol
                    };
                    objGetData.Add(row);
                }
            }
            catch(Exception ex){}
            return objGetData;
        }

        //GET  User/GetById/132456
        [HttpGet("GetById/{Identification}")]
        public GetData GetById(int Identification)
        {
            try
            {
                Model.Bender.User ObjData = _context.Users.Where(x=>x.Iduser== Identification).OrderBy(x => x.Iduser).FirstOrDefault();
                objGetDataObject = new()
                {
                    Identification = Convert.ToString(ObjData.Iduser),
                    Name = ObjData.Names,
                    Password = ObjData.Password,
                    RolId = ObjData.RolIdrol
                };
            }
            catch (Exception ex) { }
            return objGetDataObject;
        }

        //POST  User/Insert
        [HttpPost("Insert")]
        public Dto.Response Insert(Insert objInsert)
        {
            var objReturn = new Dto.Response();
            try
            {
                int RolIdAdmin = 1;
                List<Model.Bender.User> ObjDataAdmin = _context.Users.Where(x => x.RolIdrol == RolIdAdmin).ToList();
                if (objInsert.RolId== RolIdAdmin && ObjDataAdmin.Count >= 1)
                {
                    objReturn = objReturn.SelectedResponse(false, "Ya existe un usuario administrador");
                }
                else
                {
                    Model.Bender.User objUsers = new()
                    {
                        Iduser = Convert.ToInt32(objInsert.Identification),
                        Names = objInsert.Name,
                        RolIdrol = objInsert.RolId,
                        Password = BCrypt.Net.BCrypt.HashPassword(objInsert.Password),
                        BranchIdbranch = objInsert.Idbranch
                    };
                    _context.Users.Add(objUsers);
                    _context.SaveChanges();
                    if (objUsers.Iduser > 0)
                    {
                        objReturn = objReturn.SelectedResponse(true);
                    }
                }
            }
            catch (Exception ex) {
                objReturn=objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }

        //PUT  User/Edit/123456789
        [HttpPut("Edit/{Identification}")]
        public Dto.Response Edit(long Identification, Edit objEdit)
        {
            var objReturn = new Dto.Response();
            try
            {
                var objUsers = _context.Users.Where(x => x.Iduser == Identification).OrderBy(x => x.Iduser).FirstOrDefault();
                objUsers.Names = String.IsNullOrEmpty(objEdit.Name) ? objUsers.Names : objEdit.Name;
                objUsers.RolIdrol = objEdit.RolId == 0 ? objUsers.RolIdrol : objEdit.RolId;
                objUsers.BranchIdbranch = objUsers.BranchIdbranch;
                objUsers.Password = String.IsNullOrEmpty(objEdit.Password) ? objUsers.Password : BCrypt.Net.BCrypt.HashPassword(objEdit.Password);
                _context.Entry(objUsers).State = EntityState.Modified;
                _context.SaveChanges();
                if (objUsers.Iduser > 0)
                {
                    objReturn = objReturn.SelectedResponse(true);
                }
            }
            catch (Exception ex)
            {
                objReturn=objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }

        //DELETE  User/Delete/123456789
        [HttpDelete("Delete/{Identification}")]
        public Dto.Response Delete(long Identification)
        {
            var objReturn = new Dto.Response();
            try
            {
                var objUsers = _context.Users.Where(x => x.Iduser == Identification).OrderBy(x => x.Iduser).FirstOrDefault();
                _context.Remove(objUsers);
                _context.SaveChanges();
                objReturn=objReturn.SelectedResponse(true);
            }
            catch (Exception ex)
            {
                objReturn=objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }

        //POST  User/Login
        [HttpPost("Login")]
        public LoginResponse Login(LoginRequest objInsert)
        {
            try
            {
                Model.Bender.User objUsers = _context.Users.Where(x => x.Iduser == Convert.ToInt32(objInsert.User)).OrderBy(x => x.Iduser).FirstOrDefault();
                if(objUsers != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(objInsert.Password, objUsers.Password))
                    {
                        loginResponse.Name = objUsers.Names;
                        loginResponse.RolId = objUsers.RolIdrol;
                        loginResponse.Success = true;
                    }
                }
            }
            catch (Exception ex){}
            return loginResponse;
        }
    }
}
