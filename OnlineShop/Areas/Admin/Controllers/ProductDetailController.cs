using System;
using System.Collections.Generic;
using System.Linq;
using Model.Dao;
using System.Web;
using Model.EF;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Drawing; 


namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductDetailController : BaseController
    {
        // GET: Admin/ProductDetail
        public ActionResult Index(int id,int page = 1, int pageSize = 10)
        {
            var dao = new ProductDetailDao();
            
            var model = dao.ListAllPaging( id,page, pageSize);
  
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var dao = new ProductDao();
            return View();
        }

        public JsonResult LoadImages(long id)
        {
            ProductDetailDao dao = new ProductDetailDao();
            var product = dao.ViewDetail(id);
            var images = product.MoreImages;

            XElement xImages = XElement.Parse(images);
            List<string> listImagesReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImagesReturn.Add(element.Value);
            }
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveImages(long id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("Images");

            foreach (var item in listImages)
            {
                var subStringItem = item.Substring(22);
                xElement.Add(new XElement("Image", subStringItem));
            }
            ProductDetailDao dao = new ProductDetailDao();
            try
            {
                dao.UpdateImages(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }

        }

        public ActionResult Edit(long id)
        {
            var detail = new ProductDetailDao().ViewDetail(id);
            return View(detail);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_PRODUCT")]
        public ActionResult Edit(ProductDetail detail)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var dao = new ProductDetailDao();
              
                bool result = dao.Update(detail);

                if (result)
                {
                    SetAlert("Chỉnh sửa thành công", "success");
                    return RedirectToAction("Index/"+detail.ProductID, "ProductDetail");
                }
                else
                {
                    ModelState.AddModelError("", "Chỉnh sửa không thành công");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(ProductDetail product)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var listImages = serializer.Deserialize<List<string>>(product.MoreImages);

                XElement xElement = new XElement("Images");

                foreach (var item in listImages)
                {
                    var subStringItem = item.Substring(22);
                    xElement.Add(new XElement("Image", subStringItem));
                }
                product.MoreImages = xElement.ToString();
                var dao = new ProductDetailDao();

                product.ID = 0;
                //Color color = (Color)ColorConverter.ConvertFromString("123123");

           
                long id = dao.Insert(product);
               

                if (id > 0)
                {
                    SetAlert("Thêm sản phẩm thành công", "success");
                    return RedirectToAction("Index/"+product.ProductID, "ProductDetail");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDetailDao().Delete(id);

            return RedirectToAction("Index");
        }
    }
}