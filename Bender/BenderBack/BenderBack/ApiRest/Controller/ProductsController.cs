using ApiRest.Dto.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Model.Bender.BenderContext _context = new();
        private List<GetData> objGetData = new();
        private GetData objGetDataObject = new();

        //GET  User/GetAll
        [HttpGet("GetAll")]
        public List<GetData> GetAll()
        {
            try
            {
                List<Model.Bender.Product> ObjData = _context.Products.OrderBy(x => x.Idproduct).ToList();
                foreach (var data in ObjData)
                {
                    GetData row = new()
                    {
                        Idproduct = data.Idproduct,
                        InvoiceIdinvoice = data.InvoiceIdinvoice,
                        Name = data.Name,
                        Price = data.Price,
                        Supplier = data.Supplier
                    };
                    objGetData.Add(row);
                }
            }
            catch (Exception ex) { }
            return objGetData;
        }

        //GET  Products/GetById/132456
        [HttpGet("GetById/{Idproduct}")]
        public GetData GetById(int Idproduct)
        {
            try
            {
                Model.Bender.Product ObjData = _context.Products.Where(x => x.Idproduct == Idproduct).OrderBy(x => x.Idproduct).FirstOrDefault();
                objGetDataObject = new()
                {
                    Idproduct = ObjData.Idproduct,
                    InvoiceIdinvoice = ObjData.InvoiceIdinvoice,
                    Name = ObjData.Name,
                    Price = ObjData.Price,
                    Supplier = ObjData.Supplier
                };
            }
            catch (Exception ex) { }
            return objGetDataObject;
        }

        //POST  Products/Insert
        [HttpPost("Insert")]
        public Dto.Response Insert(Insert objInsert)
        {
            var objReturn = new Dto.Response();
            try
            {
                Model.Bender.Product objProducts = new()
                {
                    InvoiceIdinvoice = objInsert.InvoiceIdinvoice,
                    Name = objInsert.Name,
                    Price = objInsert.Price,
                    Supplier = objInsert.Supplier
                };
                _context.Products.Add(objProducts);
                _context.SaveChanges();
                if (objProducts.Idproduct > 0)
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

        //PUT  Products/Edit/123456789
        [HttpPut("Edit/{Idproduct}")]
        public Dto.Response Edit(int Idproduct, Edit objEdit)
        {
            var objReturn = new Dto.Response();
            try
            {
                var objProducts = _context.Products.Where(x => x.Idproduct == Idproduct).OrderBy(x => x.Idproduct).FirstOrDefault();
                objProducts.InvoiceIdinvoice = objEdit.InvoiceIdinvoice ==0 ? objProducts.InvoiceIdinvoice : objEdit.InvoiceIdinvoice;
                objProducts.Supplier = String.IsNullOrEmpty(objEdit.Supplier) ? objProducts.Supplier : objEdit.Supplier;
                objProducts.Price = String.IsNullOrEmpty(objEdit.Price) ? objProducts.Price : objEdit.Price;
                objProducts.Name = String.IsNullOrEmpty(objEdit.Name) ? objProducts.Name : objEdit.Name;
                _context.Entry(objProducts).State = EntityState.Modified;
                _context.SaveChanges();
                if (objProducts.Idproduct > 0)
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

        //DELETE  Products/Delete/Idproduct
        [HttpDelete("Delete/{Idproduct}")]
        public Dto.Response Delete(int Idproduct)
        {
            var objReturn = new Dto.Response();
            try
            {
                var objProducts = _context.Products.Where(x => x.Idproduct == Idproduct).OrderBy(x => x.Idproduct).FirstOrDefault();
                _context.Remove(objProducts);
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
