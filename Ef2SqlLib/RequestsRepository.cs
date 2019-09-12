using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ef2SqlLib {

    public class RequestsRepository {

        private static PrsDb7Context context = new PrsDb7Context();
        public static string RequestNew = "NEW";
        public static string RequestEdit = "EDIT";
        public static string RequestReview = "REVIEW";
        public static string RequestApproved = "APPROVED";
        public static string RequestRejected = "REJECTED";

        public static List<Requests> GetAll() {
            return context.Requests.ToList();
        }
        public static Requests GetByPk(int id) {
            return context.Requests.Find(id);
        }
        public static bool Insert(Requests request) {
            if(request == null) { throw new Exception("Request instance must not be null"); }
            request.Id = 0;
            context.Requests.Add(request);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Requests request) {
            if(request == null) { throw new Exception("Request instance must not be null"); }
            var dbrequest = context.Requests.Find(request.Id);
            if(dbrequest == null) { throw new Exception("No request with that id."); }
            dbrequest.Description = request.Description;
            dbrequest.Justification = request.Justification;
            dbrequest.RejectionReason = request.RejectionReason;
            dbrequest.DeliveryMode = request.DeliveryMode;
            dbrequest.Status = request.Status;
            dbrequest.Total = request.Total;
            dbrequest.UserId = request.UserId;
            // ...
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Requests request) {
            if(request == null) { throw new Exception("Request instance must not be null"); }
            var dbrequest = context.Requests.Find(request.Id);
            if(dbrequest == null) { throw new Exception("No request with that id."); }
            context.Requests.Remove(dbrequest);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var request = context.Requests.Find(id);
            var rc = Delete(request);
            return rc;
        }
        public static void Review(int id) {
            SetStatus(id, RequestReview);
        }
        public static void Approve(int id) {
            SetStatus(id, RequestApproved);
        }
        public static void Reject(int id) {
            SetStatus(id, RequestRejected);
        }
        private static void SetStatus(int id, string status) {
            var request = GetByPk(id);
            if(request == null) { throw new Exception("No request with that id."); }
            request.Status = status;
            var success = Update(request);
            if(!success) { throw new Exception("Request update failed!"); }
        }
    }
}
