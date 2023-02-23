using ApiRest.Dto.Purchase;
using ApiRest.Model.Bender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly BenderContext _context = new();
        List<GetData> objGetData = new();
        GetData objGetDataObject = new();

        //GET Purchases/GetAll
        [HttpGet("GetAll")]
        public List<GetData> GetAll()
        {
            try
            {
                List<Purchase> ObjData = _context.Purchases.OrderBy(x => x.IdPurchase).ToList();
                foreach (var data in ObjData)
                {

                    GetData row = new()
                    {
                        Id = data.IdPurchase,
                        Identification = data.Nitsupplier,
                        Name = data.Supplier 
                    };
                    objGetData.Add(row);
                }
            }
            catch (Exception ex) { }
            return objGetData;
        }

        //GET Purchases/GetById/123456798
        [HttpGet("GetById/{IdPurchase}")]
        public GetData GetById(int IdPurchase)
        {
            try
            {
                Purchase ObjData = _context.Purchases.Where(x => x.IdPurchase == IdPurchase).OrderBy(x => x.IdPurchase).FirstOrDefault();

                objGetDataObject = new()
                {
                    Id = ObjData.IdPurchase,
                    Identification = ObjData.Nitsupplier,
                    Name = ObjData.Supplier
                };
            }
            catch (Exception ex) { }
            return objGetDataObject;
        }

        //POST  Purchases/Insert
        [HttpPost("Insert")]
        public Dto.Response Insert(Insert objInsert)
        {
            var objReturn = new Dto.Response();
            try
            {
                Purchase objPurchase = new()
                {
                    //Date = DateOnly.Parse(objInsert.Date),
                    Nitsupplier = objInsert.Identification,
                    ProductIdproduct = 0,
                    Quantity = "",
                    Supplier = objInsert.Name
                };
                _context.Purchases.Add(objPurchase);
                _context.SaveChanges();
                if (objPurchase.IdPurchase > 0)
                {
                    objReturn = objReturn.SelectedResponse(true);
                }
            }
            catch (Exception ex)
            {
                objReturn = objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }

        //PUT  Purchases/Edit/123456789
        [HttpPut("Edit/{IdPurchase}")]
        public Dto.Response Edit(int IdPurchase, Edit objEdit)
        {
            var objReturn = new Dto.Response();
            try
            {
                var objPurchase = _context.Purchases.Where(x => x.IdPurchase == IdPurchase).OrderBy(x => x.IdPurchase).FirstOrDefault();
                //objPurchase.Date = objEdit.Date == null ? objPurchase.Date : DateOnly.Parse(objEdit.Date);
                objPurchase.Supplier = String.IsNullOrEmpty(objEdit.Name) ? objPurchase.Supplier : objEdit.Name;
                objPurchase.Nitsupplier = String.IsNullOrEmpty(objEdit.Identification) ? objPurchase.Nitsupplier : objEdit.Identification;
                //objPurchase.Quantity = String.IsNullOrEmpty(objEdit.Quantity) ? objPurchase.Quantity : objEdit.Quantity;
                //objPurchase.ProductIdproduct = objEdit.ProductIdproduct == 0 ? objPurchase.ProductIdproduct : objEdit.ProductIdproduct;
                _context.Entry(objPurchase).State = EntityState.Modified;
                _context.SaveChanges();
                if (objPurchase.IdPurchase > 0)
                {
                    objReturn = objReturn.SelectedResponse(true);
                }
            }
            catch (Exception ex)
            {
                objReturn = objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }

        //DELETE  Purchases/Delete/123456789
        [HttpDelete("Delete/{IdPurchase}")]
        public Dto.Response Delete(int IdPurchase)
        {
            var objReturn = new Dto.Response();
            try
            {
                var objPurchase = _context.Purchases.Where(x => x.IdPurchase == IdPurchase).OrderBy(x => x.IdPurchase).FirstOrDefault();
                _context.Remove(objPurchase);
                _context.SaveChanges();
                objReturn = objReturn.SelectedResponse(true);
            }
            catch (Exception ex)
            {
                objReturn = objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }
    }
}
