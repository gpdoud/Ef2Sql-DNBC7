using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ef2SqlLib {

    public class ProductsRepository {
        private static PrsDb7Context context = new PrsDb7Context();

        public static List<Products> GetAll() {
            return context.Products.ToList();
        }
        public static Products GetByPk(int id) {
            return context.Products.Find(id);
        }
        public static bool Insert(Products product) {
            if(product == null) { throw new Exception("Product instance must not be null"); }
            product.Id = 0;
            context.Products.Add(product);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Products product) {
            if(product == null) { throw new Exception("Product instance must not be null"); }
            var dbproduct = context.Products.Find(product.Id);
            if(dbproduct == null) { throw new Exception("No product with that id."); }
            dbproduct.PartNbr = product.PartNbr;
            dbproduct.Name = product.Name;
            dbproduct.Price = product.Price;
            dbproduct.Unit = product.Unit;
            dbproduct.PhotoPath = product.PhotoPath;
            dbproduct.VendorId = product.VendorId;
            // ...
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Products product) {
            if(product == null) { throw new Exception("Product instance must not be null"); }
            var dbproduct = context.Products.Find(product.Id);
            if(dbproduct == null) { throw new Exception("No product with that id."); }
            context.Products.Remove(dbproduct);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var product = context.Products.Find(id);
            if(product == null) { return false; }
            var rc = Delete(product);
            return rc;
        }
    }
}
