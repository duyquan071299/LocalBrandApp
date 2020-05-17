using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDetailDao
    {
    
        OnlineShopDbContext db = null;
        public ProductDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<ProductDetail> ListAllPaging(int id,int page, int pageSize)
        {
            IQueryable<ProductDetail> model = db.ProductDetails;
       
           
      
            return model.OrderByDescending(x => x.ID).Where(x => x.ProductID == id).ToPagedList(page, pageSize);
        }
        public List<ProductDetail> ViewDetails(int id) 
        {
            return db.ProductDetails.Where(x => x.ProductID==id).ToList();
        }
        public ProductDetail ViewDetail(long id)
        {
            return db.ProductDetails.Find(id);
        }
        public void UpdateImages(long productId, string images)
        {
            var product = db.ProductDetails.Find(productId);
            product.MoreImages = images;
            db.SaveChanges();
        }
        public long Insert(ProductDetail entity)
        {
            db.ProductDetails.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public String SetName(int id)
        {
            var user = db.Products.Single(x => x.ID == id);
        
            return user.Name;
        }
        public bool Update(ProductDetail entity)
        {
            try
            {
                var detail = db.ProductDetails.Find(entity.ID);
                detail.Color = entity.Color;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var product = db.ProductDetails.Find(id);
                db.ProductDetails.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
