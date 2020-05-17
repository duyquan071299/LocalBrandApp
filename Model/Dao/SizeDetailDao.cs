using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SizeDetailDao
    {
        OnlineShopDbContext db = null;
        public SizeDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<SizeDetail> ListAllPaging(int id, int page, int pageSize)
        {
            IQueryable<SizeDetail> model = db.SizeDetails;

            return model.OrderByDescending(x => x.ID).Where(x => x.DetailID == id).ToPagedList(page, pageSize);
        }

        public List<SizeDetail> ViewDetails(int id)
        {
            return db.SizeDetails.Where(x => x.DetailID == id).ToList();
        }
        public SizeDetail ViewDetail(long id)
        {
            return db.SizeDetails.Find(id);
        }

        public long Insert(SizeDetail entity)
        {
            db.SizeDetails.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(SizeDetail entity)
        {
            try
            {
                var detail = db.SizeDetails.Find(entity.ID);
                detail.Size = entity.Size;
                detail.Quantity = entity.Quantity;
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
                var product = db.SizeDetails.Find(id);
                db.SizeDetails.Remove(product);
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
