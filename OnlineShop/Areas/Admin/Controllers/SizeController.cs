using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dao;
using System.Web;
using Model.EF;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Script.Serialization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class SizeController : BaseController
    {
        // GET: Admin/ProductDetail
        public ActionResult Index(int id, int page = 1, int pageSize = 10)
        {
            var dao = new SizeDetailDao();

            var model = dao.ListAllPaging(id, page, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var dao = new SizeDetailDao();
            return View();
        }

     
        public ActionResult Edit(long id)
        {
            var detail = new SizeDetailDao().ViewDetail(id);
            return View(detail);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_SIZE")]
        public ActionResult Edit(SizeDetail detail)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var dao = new SizeDetailDao();

                bool result = dao.Update(detail);

                if (result)
                {
                    SetAlert("Chỉnh sửa thành công", "success");
                    return RedirectToAction("Index/" + detail.DetailID, "Size");
                }
                else
                {
                    ModelState.AddModelError("", "Chỉnh sửa không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(SizeDetail product)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {

              
                var dao = new SizeDetailDao();

                product.ID = 0;

                long id = dao.Insert(product);


                if (id > 0)
                {
                    SetAlert("Thêm chi tiết thành công", "success");
                    return RedirectToAction("Index/" + product.DetailID, "Size");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm chi tiết không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new SizeDetailDao().Delete(id);

            return RedirectToAction("Index");
        }
    }
}