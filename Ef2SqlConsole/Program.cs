using Ef2SqlLib;
using System;
using System.Linq;

namespace Ef2SqlConsole {
    class Program {
        void Run() {
            //TestUsersRepository();
            //TestVendorsRepository();
            //TestProductsRepository();
            TestRequestsRepository();
        }
        void TestUsersRepository() {
            var newuser = new Users() {
                Username = "TEST-XXX", Password = "XXX",
                Firstname = "XXX", Lastname = "XXX",
                Phone = null, Email = null,
                IsReviewer = false, IsAdmin = false
            };
            if(!UsersRepository.Insert(newuser)) { throw new Exception("User insert failed!"); }
            DisplayAllUsers();
            newuser.Firstname = "ZZZ";
            if(!UsersRepository.Update(newuser)) { throw new Exception("User update failed!"); }
            DisplayAllUsers();
            if(!UsersRepository.Delete(newuser)) { throw new Exception("User delete failed!"); }
            DisplayAllUsers();

        }
        void TestVendorsRepository() {
            var newvendor = new Vendors() {
                Code = "ZZZ", Name = "ZZZ",
                Address = "123 Any St.", City = "Cincinnati", State = "OH", Zip = "54321",
                Phone = null, Email = null
            };
            if(!VendorsRepository.Insert(newvendor)) { throw new Exception("Vendor insert failed!"); }
            DisplayAllVendors();
            newvendor.Name = "YYY";
            if(!VendorsRepository.Update(newvendor)) { throw new Exception("Vendor update failed!"); }
            DisplayAllVendors();
            if(!VendorsRepository.Delete(newvendor)) { throw new Exception("Vendor delete failed!"); }
            DisplayAllVendors();
        }
        void TestProductsRepository() {
            var newproduct = new Products() {
                PartNbr = "ZZZ", Name = "ZZZ",
                Price = 10.99M, Unit = "Each", PhotoPath = null,
                VendorId = VendorsRepository.GetByPk(3).Id
            };
            if(!ProductsRepository.Insert(newproduct)) { throw new Exception("Product insert failed!"); }
            DisplayAllProducts();
            newproduct.Name = "YYY";
            if(!ProductsRepository.Update(newproduct)) { throw new Exception("Product update failed!"); }
            DisplayAllProducts();
            if(!ProductsRepository.Delete(newproduct)) { throw new Exception("Product delete failed!"); }
            DisplayAllProducts();
        }
        void TestRequestsRepository() {
            var newrequest = new Requests() {
                Description = "ZZZ", Justification = "ZZZ", RejectionReason = null,
                DeliveryMode = "Pickup", UserId = UsersRepository.GetByPk(2).Id
            };
            if(!RequestsRepository.Insert(newrequest)) { throw new Exception("Request insert failed!"); }
            DisplayAllRequests();
            newrequest.Description = "YYY";
            if(!RequestsRepository.Update(newrequest)) { throw new Exception("Request update failed!"); }
            DisplayAllRequests();
            if(!RequestsRepository.Delete(newrequest)) { throw new Exception("Request delete failed!"); }
            DisplayAllRequests();
        }
        #region Display methods
        void DisplayAllUsers() {
            Console.WriteLine("*** Users ***");
            UsersRepository.GetAll()
                .ForEach(u => { Console.WriteLine(u); });
        }
        void DisplayAllVendors() {
            Console.WriteLine("\n*** Vendors ***");
            VendorsRepository.GetAll()
                .ForEach(v => { Console.WriteLine(v); });
        }
        void DisplayAllProducts() {
            Console.WriteLine("\n*** Products ***");
            ProductsRepository.GetAll()
                .ForEach(p => { Console.WriteLine(p); });

        }
        void DisplayAllRequests() {
            Console.WriteLine("\n*** Requests ***");
            RequestsRepository.GetAll()
                .ForEach(r => { Console.WriteLine(r); });

        }
        void DisplayAllRequestLines() {
            Console.WriteLine("\n*** RequestLines ***");
            RequestLinesRepository.GetAll()
                .ForEach(l => { Console.WriteLine(l); });

        }
        void DisplayAll() {
            DisplayAllUsers();
            DisplayAllVendors();
            DisplayAllProducts();
            DisplayAllRequests();
            DisplayAllRequestLines();
        }
        #endregion
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
