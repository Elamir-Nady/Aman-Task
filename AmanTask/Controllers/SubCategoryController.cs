using Entites;
using Intrerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AmanTask.Web.Controllers
{
    [Authorize]
    public class SubCategoryController : Controller
    {
        public IUnitOfWork _UnitOfWork { get; }

        public SubCategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        // GET: MainCategoryController
        public ActionResult Index()
        {

            string[] includes = new string[] { "MainCategory" };

            var subcats = _UnitOfWork.GetSubCategoryRepo().Get(includes);
            if (subcats == null)
                return NotFound();

            return View(subcats);
        }

        // GET: MainCategoryController/Details/5
        public ActionResult Details(int id)
        {
            string[] includes = new string[] { "MainCategory" };

            if (id == 0)
                return NotFound();
            var subcat = _UnitOfWork.GetSubCategoryRepo().GetByID(id, includes);
            if (subcat == null)
                return NotFound(id);
            return View(subcat);
        }

        // GET: MainCategoryController/Create
        public ActionResult Create()
        {
            string[] includes = new string[] { "MainCategory" };
            var mainCat = _UnitOfWork.GetMainCategoryRepo().Get(includes);
            ViewBag.MianCategoryId = new SelectList(mainCat, "Id", "Name");
            return View();
        }

        // POST: MainCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subcat)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (subcat == null)
                        return NotFound();
                    _UnitOfWork.GetSubCategoryRepo().Add(subcat);
                    _UnitOfWork.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MainCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            string[] includes = new string[] { "MainCategory" };

            var mainCat = _UnitOfWork.GetMainCategoryRepo().Get(includes);
            ViewBag.MianCategoryId = new SelectList(mainCat, "Id", "Name");
            if (id == 0)
                return NotFound();
            var subcat = _UnitOfWork.GetSubCategoryRepo().GetByID(id, includes);
            if (subcat == null)
                return NotFound(id);
            return View(subcat);
        }

        // POST: MainCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubCategory subcat)
        {
            try
            {
                string[] includes = new string[] { "MainCategory" };

                if (ModelState.IsValid)
                {
                    if (id == 0)
                        return NotFound();
                    if (subcat == null)
                        return NotFound();
                    var subcatdb = _UnitOfWork.GetSubCategoryRepo().GetByID(id, includes);
                    if (subcatdb == null)
                        return NotFound(id);
                    subcatdb.Name = subcat.Name;
                    subcatdb.MianCategoryId = subcat.MianCategoryId;
                    _UnitOfWork.GetSubCategoryRepo().Update(subcatdb);
                    _UnitOfWork.Save();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MainCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            string[] includes = new string[] { "MainCategory" };

            if (id == 0)
                return NotFound();
            var subcat = _UnitOfWork.GetSubCategoryRepo().GetByID(id, includes);
            if (subcat == null)
                return NotFound(id);
            return View(subcat);
        }

        // POST: MainCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SubCategory subcat)
        {
            try
            {
                string[] includes = new string[] { "MainCategory" };

                if (id == 0)
                    return NotFound();
                if (subcat == null)
                    return NotFound();
                var subcatdb = _UnitOfWork.GetSubCategoryRepo().GetByID(id, includes);
                if (subcatdb == null)
                    return NotFound(id);

                _UnitOfWork.GetSubCategoryRepo().Remove(subcatdb);
                _UnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
