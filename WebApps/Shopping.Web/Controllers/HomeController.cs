using System.Diagnostics;
using Shopping.Web.Models;

namespace Shopping.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ICatalogService catalogService, IBasketService basketService, ILogger<HomeController> logger)
    {
        _catalogService = catalogService;
        _basketService = basketService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Index page visited");
        var result = await _catalogService.GetProducts();
        return View(result.Products);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId)
    {
        _logger.LogInformation("Add to cart button clicked");

        var productResponse = await _catalogService.GetProduct(productId);
        var basket = await _basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = 1,
            Color = "Black"
        });

        await _basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToAction("Index", "Cart");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
