using Entites;
using Intrerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Data.Context;
using Task.Data.Entites;

namespace Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepostory<MainCategory> MainCategorey { get; }
        public IAuthService AuthService { get; }
        public IGenericRepostory<SubCategory> SubCategory { get; }
        public IGenericRepostory<Product> Product { get; }
        public ApplicationDbContext Context { get; }

        public UnitOfWork(IGenericRepostory<MainCategory> mainCategorey, IAuthService authService, IGenericRepostory<SubCategory> subCategory, IGenericRepostory<Product> product,ApplicationDbContext context)
        {
            MainCategorey = mainCategorey;
            AuthService = authService;
            SubCategory = subCategory;
            Product = product;
            Context = context;
        }


        public IAuthService  GetAuthRepo()
        {
            return AuthService;
        }
        public IGenericRepostory<MainCategory> GetMainCategoryRepo()
        {
            return MainCategorey;
        }

        public IGenericRepostory<Product> GetProductRepo()
        {
            return Product;
        }

        public IGenericRepostory<SubCategory> GetSubCategoryRepo()
        {
            return SubCategory;
        }

        public  int Save()
        {
            return  Context.SaveChanges(); 
        }
    }
}
