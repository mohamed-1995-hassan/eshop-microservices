namespace Shopping.Web.Controllers;

public class CartController : Controller
{
    private readonly IBasketService _basketService;
    private readonly ILogger<CartController> _logger;

    public CartController(IBasketService basketService, ILogger<CartController> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var cart = await _basketService.LoadUserBasket();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(Guid productId)
    {
        _logger.LogInformation("Remove from cart button clicked");
        var cart = await _basketService.LoadUserBasket();

        cart.Items.RemoveAll(x => x.ProductId == productId);

        await _basketService.StoreBasket(new StoreBasketRequest(cart));

        return RedirectToAction("Index");
    }
}
