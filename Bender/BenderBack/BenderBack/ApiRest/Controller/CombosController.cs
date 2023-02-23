using ApiRest.Dto;
using ApiRest.Dto.Combo;
using ApiRest.Model.Bender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiRest.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CombosController : ControllerBase
    {
        private readonly Model.Bender.BenderContext _context = new();
        List<Dto.Combo.GetData> objGetData = new();
        Dto.Combo.GetData objGetDataObject = new();

        //GET Combos/GetAll
        [HttpGet("GetAll")]
        public List<GetData> GetAll()
        {
            try
            {
                List<Model.Bender.Combo> ObjData = _context.Combos.OrderBy(x => x.Idcombo).ToList();
                foreach (var data in ObjData)
                {
                    
                    List<Model.Bender.ProductHasCombo> ObjDataHas = _context.ProductHasCombos.Where(x => x.CombosIdcombos == data.Idcombo).ToList();
                    List<Dto.Product.ProductCombo> objProductCombo = new();
                    foreach (var dataHas in ObjDataHas)
                    {
                        Dto.Product.ProductCombo rowHas = new()
                        {
                            Idproduct = (int)dataHas.ProductoIdproducto
                        };
                        objProductCombo.Add(rowHas);
                    }
                    Dto.Combo.GetData row = new()
                    {
                        Idcombo = data.Idcombo,
                        nameCombo = data.Namecombo,
                        Products = objProductCombo
                    };
                    objGetData.Add(row);
                }
            }
            catch (Exception ex) { }
            return objGetData;
        }

        //GET  Combos/GetById/132456
        [HttpGet("GetById/{Idcombo}")]
        public GetData GetById(int Idcombo)
        {
            try
            {
                Model.Bender.Combo ObjData = _context.Combos.Where(x => x.Idcombo == Idcombo).OrderBy(x => x.Idcombo).FirstOrDefault();
                List<Model.Bender.ProductHasCombo> ObjDataHas = _context.ProductHasCombos.Where(x => x.CombosIdcombos == Idcombo).ToList();
                List<Dto.Product.ProductCombo> objProductCombo = new();
                foreach (var dataHas in ObjDataHas)
                {
                    Dto.Product.ProductCombo rowHas = new()
                    {
                        Idproduct = (int)dataHas.ProductoIdproducto
                    };
                    objProductCombo.Add(rowHas);
                }
                objGetDataObject = new()
                {
                    Idcombo = ObjData.Idcombo,
                    nameCombo = ObjData.Namecombo,
                    Products = objProductCombo
                };
            }
            catch (Exception ex) { }
            return objGetDataObject;
        }

        //GET  Combos/GetExists
        [HttpGet("GetExists")]
        public List<GetData> GetExists()
        {
            try

            {
                List<Model.Bender.Combostockexist> ObjData = _context.Combostockexists.OrderBy(x => x.CombosIdcombos).ToList();
                
                foreach (var data in ObjData)
                {
                    List<Model.Bender.Combostockexist>  ObjComboExist = ObjData.Where(x => x.CombosIdcombos == data.CombosIdcombos).ToList();
                    List<Model.Bender.ProductHasCombo> ObjCombo = _context.ProductHasCombos.Where(x => x.CombosIdcombos == data.CombosIdcombos).ToList();
                    if (ObjComboExist.Count == ObjCombo.Count)
                    {
                        List<Dto.Product.ProductCombo> objProductCombo = new();
                        foreach (var dataHas in ObjCombo)
                        {
                            Dto.Product.ProductCombo rowHas = new()
                            {
                                Idproduct = (int)dataHas.ProductoIdproducto
                            };
                            objProductCombo.Add(rowHas);
                        }
                        Dto.Combo.GetData row = new()
                        {
                            Idcombo = (int)data.CombosIdcombos,
                            nameCombo = data.Namecombo,
                            Products = objProductCombo
                        };
                        if (!objGetData.Exists(x => x.Idcombo == row.Idcombo))
                        {
                            objGetData.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return objGetData;
        }

        //GET  Combos/GetExistsById/123456
        [HttpGet("GetExistsById/{Idcombo}")]
        public GetData GetExistsById(int Idcombo)
        {
            try

            {
                List<Model.Bender.Combostockexist> ObjData = _context.Combostockexists.OrderBy(x => x.CombosIdcombos).ToList();

                foreach (var data in ObjData)
                {
                    List<Model.Bender.Combostockexist> ObjComboExist = ObjData.Where(x => x.CombosIdcombos == data.CombosIdcombos).ToList();
                    List<Model.Bender.ProductHasCombo> ObjCombo = _context.ProductHasCombos.Where(x => x.CombosIdcombos == data.CombosIdcombos).ToList();
                    if (ObjComboExist.Count == ObjCombo.Count)
                    {
                        List<Dto.Product.ProductCombo> objProductCombo = new();
                        foreach (var dataHas in ObjCombo)
                        {
                            Dto.Product.ProductCombo rowHas = new()
                            {
                                Idproduct = (int)dataHas.ProductoIdproducto
                            };
                            objProductCombo.Add(rowHas);
                        }
                        objGetDataObject = new()
                        {
                            Idcombo = (int)data.CombosIdcombos,
                            nameCombo = data.Namecombo,
                            Products = objProductCombo
                        };
                        if (!objGetData.Exists(x => x.Idcombo == objGetDataObject.Idcombo))
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return objGetDataObject;
        }

        //POST  Combos/Insert
        [HttpPost("Insert")]
        public Dto.Response Insert(Dto.Combo.Insert objInsert)
        {
            var objReturn = new Dto.Response();
            try
            {
                Model.Bender.Combo objCombo = new()
                {
                    Namecombo = objInsert.nameCombo,
                };
                _context.Combos.Add(objCombo);
                _context.SaveChanges();
                if (objCombo.Idcombo > 0)
                {
                    bool FlagInsertProduct = false;
                    foreach(var product in objInsert.Products)
                    {
                        Model.Bender.ProductHasCombo ObjProductCombo = new()
                        {
                            ProductoIdproducto = product.Idproduct,
                            CombosIdcombos = objCombo.Idcombo
                        };
                        _context.ProductHasCombos.Add(ObjProductCombo);
                        _context.SaveChanges();
                        if (ObjProductCombo.Idproductocombo > 0)
                        {
                            FlagInsertProduct = true;
                            continue;
                        }
                        else
                        {
                            FlagInsertProduct = false;
                            break;
                        }
                    }
                   
                    objReturn = FlagInsertProduct ? objReturn.SelectedResponse(true)  : objReturn.SelectedResponse(false);
                }
            }
            catch (Exception ex)
            {
                objReturn = objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }

        //PUT  Combos/Edit/123456789
        [HttpPut("Edit/{IdCombo}")]
        public Dto.Response Edit(int IdCombo, Edit objEdit)
        {
            var objReturn = new Dto.Response();
            try
            {
                var objCombos = _context.Combos.Where(x => x.Idcombo == IdCombo).OrderBy(x => x.Idcombo).FirstOrDefault();
                objCombos.Namecombo = String.IsNullOrEmpty(objEdit.nameCombo) ? objCombos.Namecombo : objEdit.nameCombo;

                _context.ProductHasCombos.RemoveRange(_context.ProductHasCombos.Where(x => x.CombosIdcombos == IdCombo));
                _context.SaveChanges();

                bool FlagInsertProduct = false;
                foreach (var product in objEdit.Products)
                {
                    Model.Bender.ProductHasCombo ObjProductCombo = new()
                    {
                        ProductoIdproducto = product.Idproduct,
                        CombosIdcombos = IdCombo
                    };
                    _context.ProductHasCombos.Add(ObjProductCombo);
                    _context.SaveChanges();
                    if (ObjProductCombo.Idproductocombo > 0)
                    {
                        FlagInsertProduct = true;
                        continue;
                    }
                    else
                    {
                        FlagInsertProduct = false;
                        break;
                    }
                }

                objReturn = FlagInsertProduct ? objReturn.SelectedResponse(true) : objReturn.SelectedResponse(false);
            }
            catch (Exception ex)
            {
                objReturn = objReturn.SelectedResponse(false, ex.Message);
            }
            return objReturn;
        }

        //DELETE  Combos/Delete/123456789
        [HttpDelete("Delete/{IdCombo}")]
        public Dto.Response Delete(int IdCombo)
        {
            var objReturn = new Dto.Response();
            try
            {
                _context.ProductHasCombos.RemoveRange(_context.ProductHasCombos.Where(x => x.CombosIdcombos == IdCombo));
                _context.SaveChanges();

                var objCombos = _context.Combos.Where(x => x.Idcombo == IdCombo).OrderBy(x => x.Idcombo).FirstOrDefault();
                _context.Remove(objCombos);
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
