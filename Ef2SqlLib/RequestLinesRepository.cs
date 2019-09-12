using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ef2SqlLib {

    public class RequestLinesRepository {

        private static PrsDb7Context context = new PrsDb7Context();

        private static void RecalculateRequestTotal(int requestId) {
            var request = RequestsRepository.GetByPk(requestId);
            request.Total = request.RequestLines.Sum(l => l.Product.Price * l.Quantity);
            // SaveChanges is after this code in Insert, Update, Delete
        }

        public static List<RequestLines> GetAll() {
            return context.RequestLines.ToList();
        }
        public static RequestLines GetByPk(int id) {
            return context.RequestLines.Find(id);
        }
        public static bool Insert(RequestLines requestLines) {
            if(requestLines == null) { throw new Exception("RequestLine instance must not be null"); }
            requestLines.Id = 0;
            context.RequestLines.Add(requestLines);
            RecalculateRequestTotal(requestLines.RequestId);
            return context.SaveChanges() == 1;
        }
        public static bool Update(RequestLines requestLines) {
            if(requestLines == null) { throw new Exception("RequestLine instance must not be null"); }
            var dbrequestline = context.RequestLines.Find(requestLines.Id);
            if(dbrequestline == null) { throw new Exception("No requestLines with that id."); }
            dbrequestline.RequestId = requestLines.RequestId;
            dbrequestline.ProductId = requestLines.ProductId;
            dbrequestline.Quantity = requestLines.Quantity;
            // ...
            RecalculateRequestTotal(dbrequestline.RequestId);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(RequestLines requestLines) {
            if(requestLines == null) { throw new Exception("RequestLine instance must not be null"); }
            var dbrequestline = context.RequestLines.Find(requestLines.Id);
            if(dbrequestline == null) { throw new Exception("No requestLines with that id."); }
            context.RequestLines.Remove(dbrequestline);
            RecalculateRequestTotal(dbrequestline.RequestId);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var requestLine = context.RequestLines.Find(id);
            if(requestLine == null) { return false; }
            var rc = Delete(requestLine);
            return rc;
        }
    }
}
