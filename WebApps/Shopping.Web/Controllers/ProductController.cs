namespace Shopping.Web.Controllers;

public class ProductController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ICatalogService catalogService, IBasketService basketService, ILogger<ProductController> logger)
    {
        _catalogService = catalogService;
        _basketService = basketService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? categoryName)
    {
        var response = await _catalogService.GetProducts();

        var categoryList = response.Products.SelectMany(p => p.Category).Distinct();
        ViewBag.CategoryList = categoryList;
        ViewBag.SelectedCategory = categoryName;

        IEnumerable<ProductModel> productList;
        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            productList = response.Products.Where(p => p.Category.Contains(categoryName));
        }
        else
        {
            productList = response.Products;
        }

        return View(productList);
    }

    public async Task<IActionResult> Detail(Guid productId)
    {
        var response = await _catalogService.GetProduct(productId);
        return View(response.Product);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId, string color = "Black", int quantity = 1)
    {
        _logger.LogInformation("Add to cart button clicked");
        var productResponse = await _catalogService.GetProduct(productId);

        var basket = await _basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = quantity,
            Color = color
        });

        await _basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToAction("Index", "Cart");
    }
}
