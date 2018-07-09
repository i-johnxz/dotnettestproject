//using System;
//using System.Linq;
//using Xunit;

//namespace EfCoreTransactionExampleTest
//{
//    public class ControllingTransactionTest
//    {
//        [Fact]
//        public void Test1()
//        {
//            using (var context = new BloggingContext())
//            {
//                context.Database.EnsureDeleted();
//                context.Database.EnsureCreated();
//            }


//            using (var context = new BloggingContext())
//            {
//                using (var transaction = context.Database.BeginTransaction())
//                {
//                    try
//                    {
//                        context.Blogs.Add(new Blog
//                        {
//                            Url = "http://blogs.msdn.com/dotnet"
//                        });
//                        context.SaveChanges();

//                        context.Blogs.Add(new Blog
//                        {
//                            Url = "http://blogs.msdn.com/visualstudio"
//                        });
//                        context.SaveChanges();

//                        var blogs = context.Blogs.OrderBy(b => b.Url).ToList();

//                        transaction.Commit();
//                    }
//                    catch (Exception e)
//                    {
//                        Console.WriteLine(e);
                        
//                        throw;
//                    }
//                }
//            }

//            Console.WriteLine("End");

//        }
//    }
//}
