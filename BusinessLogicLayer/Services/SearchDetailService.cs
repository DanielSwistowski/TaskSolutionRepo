using DataAccessLayer.Models;
using DataAccessLayer;

namespace BusinessLogicLayer.Services
{
    public interface ISearchDetailService : IBaseService<SearchDetail>
    {

    }

    public class SearchDetailService : BaseService<SearchDetail>, ISearchDetailService
    {
        public SearchDetailService(ICompanyDbContext context) : base(context)
        {
        }
    }
}
