using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intrerfaces
{
    public interface IUnitOfWork
    {
        IGenericRepostory<Product> GetProductRepo();
        IAuthService GetAuthRepo();
        IGenericRepostory<MainCategory> GetMainCategoryRepo();
        IGenericRepostory<SubCategory> GetSubCategoryRepo();

       int Save();
    }
}
