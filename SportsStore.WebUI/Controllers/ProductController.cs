using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 2; // We will change this later

        private readonly IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var viewModel = new ProductsListViewModel
                                {
                                    Products = repository.Products
                                        .Where(p => category == null || p.Category == category)
                                        .OrderBy(p => p.ProductID)
                                        .Skip((page - 1)*PageSize)
                                        .Take(PageSize),
                                    PagingInfo = new PagingInfo
                                                     {
                                                         CurrentPage = page,
                                                         ItemsPerPage = PageSize,
                                                         TotalItems = category == null
                                                                          ? repository.Products.Count()
                                                                          : repository.Products.Count(e => e.Category == category)
                                                     },
                                    CurrentCategory = category
                                };
            return View(viewModel);
        }

    }
}
