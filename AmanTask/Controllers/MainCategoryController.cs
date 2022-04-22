using Entites;
using Intrerfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmanTask.Web.Controllers
{
    public class MainCategoryController : Controller
    {
        public IUnitOfWork _UnitOfWork { get; }

        public MainCategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        // GET: MainCategoryController
        public ActionResult Index()
        {
            var maincats=_UnitOfWork.GetMainCategoryRepo().Get();
            if (maincats == null)
                return NotFound();

            return View(maincats);
        }

        // GET: MainCategoryController/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
                return NotFound();
            var maincat = _UnitOfWork.GetMainCategoryRepo().GetByID(id);
            if (maincat == null)
                return NotFound(id);
            return View(maincat);
        }

        // GET: MainCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MainCategory maincat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (maincat == null)
                        return NotFound();
                    _UnitOfWork.GetMainCategoryRepo().Add(maincat);
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
            if (id == 0)
                return NotFound();
            var maincat = _UnitOfWork.GetMainCategoryRepo().GetByID(id);
            if (maincat == null)
                return NotFound(id);
            return View(maincat);
        }

        // POST: MainCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MainCategory maincat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                        return NotFound();
                    if (maincat == null)
                        return NotFound();
                    var maincatdb = _UnitOfWork.GetMainCategoryRepo().GetByID(id);
                    if (maincatdb == null)
                        return NotFound(id);
                    maincatdb.Name = maincat.Name;
                    _UnitOfWork.GetMainCategoryRepo().Update(maincatdb);
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
            if (id == 0)
                return NotFound();
            var maincat = _UnitOfWork.GetMainCategoryRepo().GetByID(id);
            if (maincat == null)
                return NotFound(id);
            return View(maincat);
        }

        // POST: MainCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MainCategory maincat)
        {
            try
            {
                if (id == 0)
                    return NotFound();
                if (maincat == null)
                    return NotFound();
                var maincatdb = _UnitOfWork.GetMainCategoryRepo().GetByID(id);
                if (maincatdb == null)
                    return NotFound(id);

                _UnitOfWork.GetMainCategoryRepo().Remove(maincatdb);
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
