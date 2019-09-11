using Ef2SqlLib;
using System;
using System.Linq;

namespace Ef2SqlConsole {
    class Program {
        void Run() {
            UsersRepository.GetAll()
                .ForEach(u => { Console.WriteLine(u); });
        }
        static void Main(string[] args) {
            (new Program()).Run();
        }
        #region Tutorial Methods
        static void Run1() { 

            using(var context = new PrsDb7Context()) {

                var reqs = (from r in context.Requests
                            select r).ToList();
                reqs.ForEach(r => {
                    r.Total = r.RequestLines.Sum(l => l.Product.Price * l.Quantity);
                    ToConsole(r);
                });
                context.SaveChanges();

                var total = context.Requests.Sum(r => r.Total);
                Console.WriteLine( $"Total all requests is {total}");

                #region Commented Out
                //var request = new Requests() {
                //    Id = 0,
                //    Description = "Another New Request",
                //    Justification = "I don't need one!",
                //    RejectionReason = null,
                //    DeliveryMode = "Pickup",
                //    Status = "NEW",
                //    Total = 0,
                //    UserId = context.Users.SingleOrDefault(u => u.Username.Equals("sa")).Id
                //};
                //context.Requests.Add(request);

                //var request = new Requests() { Id = 3, Description = "Another Changed Description" };
                //var dbRequest = context.Requests.Find(request.Id);
                //dbRequest.Description = request.Description;

                //dbRequest = context.Requests.Find(3);
                //context.Requests.Remove(dbRequest);
                //context.SaveChanges();

                //var amazon = context.Vendors.Find(3);

                //Console.WriteLine($"{amazon.Code} {amazon.Name}");

                ////var users = context.Users.ToList();
                //var users = from u in context.Users.ToList()
                //            where u.Username.Contains("5") || u.Username.Contains("6")
                //            select u;

                //foreach(var user in users) {
                //    Console.WriteLine($"{user.Username} {user.Firstname} {user.Lastname}");
                //}
                #endregion
            }

        }
        static void ToConsole(Requests req) {
            Console.WriteLine($"{req.Description} {req.Status} {req.Total.ToString("C")}");
            req.RequestLines.ToList().ForEach(rl => {
                Console.WriteLine($"{rl.Product.Name,-10} {rl.Quantity,5} " +
                    $"{rl.Product.Price.ToString("C"),10} " +
                    $"{(rl.Product.Price * rl.Quantity).ToString("C"),11}");
            });
        }
        #endregion
    }
}
