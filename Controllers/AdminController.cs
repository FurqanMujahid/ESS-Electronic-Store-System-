using ESS.Repository;
using ESS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESS.Models;
using Newtonsoft.Json;
using System.IO;

namespace ESS.Controllers
{
    public class AdminController : Controller
    {

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        public ActionResult UpdateCategory(int? categoryId)
        {
            CategoryDetail cd;
            if (categoryId.HasValue)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId.Value)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);
        }

        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }
        public ActionResult ProductEdit(int productId)
        {
            if (productId != null)
            {
                ViewBag.CategoryList = GetCategory();
                return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
            }
            else
            {
                return HttpNotFound();
            }
            return Product();
        }
        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Get the file name
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/ProductImg/"), pic);

                // Save the file
                file.SaveAs(path);

                // Convert the file to a byte array
                using (var ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    tbl.ProductImage = ms.ToArray();
                }
            }

            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            return RedirectToAction("Product");
        }
        public ActionResult ProductAdd()
        {
            Tbl_Product model = new Tbl_Product();
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product tbl, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string pic = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                    file.SaveAs(path);

                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        tbl.ProductImage = ms.GetBuffer();
                    }
                }

                tbl.CreatedDate = DateTime.Now;
                // Add tbl to the database
                _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);
                return RedirectToAction("Product");
            }

            // If we got this far, something failed, redisplay form
            ViewBag.CategoryList = GetCategory(); // Ensure this is populated
            return View(tbl);
        }


    }
}