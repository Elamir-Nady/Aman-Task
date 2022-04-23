using Entites;
using Intrerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace AmanTask.Web.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _UnitOfWOrk;
        public ProductController(IUnitOfWork UnitOfWOrk)
        {
            _UnitOfWOrk = UnitOfWOrk;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetString("_Token") != null)
                {
                    string[] includes = new string[] { "MainCategory", "SubCategory" };

                    var products = _UnitOfWOrk.GetProductRepo().Get(includes);
                    if (products == null)
                        return NotFound();

                    return View(products);
                }


                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("_Token") != null)
            {
                string[] includes = new string[] { "MainCategory", "SubCategory" };

                if (id == 0)
                    return BadRequest();
                var productinDb = _UnitOfWOrk.GetProductRepo().GetByID(id, includes);

                if (productinDb == null)
                    return NotFound();


                return View(productinDb);
            }
            return NotFound();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("_Token") != null)
            {
                string[] includes = new string[] { "MainCategory" };

                var mainCat = _UnitOfWOrk.GetMainCategoryRepo().Get();
                ViewBag.MianCategoryId = new SelectList(mainCat, "Id", "Name");

                var SubCat = _UnitOfWOrk.GetSubCategoryRepo().Get(includes);
                ViewBag.SubCategoryId = new SelectList(SubCat, "Id", "Name");
            }


            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (HttpContext.Session.GetString("_Token") != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (product == null)
                            return BadRequest();
                        _UnitOfWOrk.GetProductRepo().Add(product);
                        _UnitOfWOrk.Save();
                    }
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("_Token") != null)
            {
                string[] includes = new string[] { "MainCategory" };

                var mainCat = _UnitOfWOrk.GetMainCategoryRepo().Get();
                ViewBag.MianCategoryId = new SelectList(mainCat, "Id", "Name");

                var SubCat = _UnitOfWOrk.GetSubCategoryRepo().Get(includes);
                ViewBag.SubCategoryId = new SelectList(SubCat, "Id", "Name");
                if (id == 0)
                    return BadRequest();
                var product = _UnitOfWOrk.GetProductRepo().GetByID(id);
                if (product == null)
                    return NotFound();


                return View(product);
            }
            return NotFound();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (HttpContext.Session.GetString("_Token") != null)
                {
                    string[] includes = new string[] { "MainCategory", "SubCategory" };

                    if (ModelState.IsValid)
                    {
                        if (id == 0)
                            return BadRequest();
                        var productinDb = _UnitOfWOrk.GetProductRepo().GetByID(id, includes);

                        if (productinDb == null || product == null)
                            return NotFound();
                        productinDb.Name = product.Name;
                        productinDb.price = product.price;
                        productinDb.Qty = product.Qty;
                        productinDb.SubCategoryId = product.SubCategoryId;
                        productinDb.MianCategoryId = product.MianCategoryId;
                        _UnitOfWOrk.GetProductRepo().Update(productinDb);
                        _UnitOfWOrk.Save();
                    }


                    return RedirectToAction(nameof(Index));
                }
                return NotFound();

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("_Token") != null)
            {
                string[] includes = new string[] { "MainCategory", "SubCategory" };


                if (id == 0)
                    return BadRequest();
                var product = _UnitOfWOrk.GetProductRepo().GetByID(id, includes);
                if (product == null)
                    return NotFound();
                _UnitOfWOrk.GetProductRepo().Remove(product);
                _UnitOfWOrk.Save();

                return View(product);
            }
            return NotFound();

        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                if (HttpContext.Session.GetString("_Token") != null)
                {
                    string[] includes = new string[] { "MainCategory", "SubCategory" };

                    if (id == 0)
                        return BadRequest();
                    var productinDb = _UnitOfWOrk.GetProductRepo().GetByID(id, includes);

                    if (productinDb == null)
                        return NotFound();


                    _UnitOfWOrk.GetProductRepo().Remove(productinDb);
                    _UnitOfWOrk.Save();
                    return RedirectToAction(nameof(Index));

                }
                return NotFound();

            }
            catch
            {
                return View();
            }
        }
    }
}
