namespace Shopping.Web.Controllers;

public class CheckoutController : Controller
{
    private readonly IBasketService _basketService;
    private readonly ILogger<CheckoutController> _logger;

    public CheckoutController(IBasketService basketService, ILogger<CheckoutController> logger)
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
    public async Task<IActionResult> Checkout(BasketCheckoutModel order)
    {
        _logger.LogInformation("Checkout button clicked");

        var cart = await _basketService.LoadUserBasket();

        if (!ModelState.IsValid)
        {
            return View("Index", cart);
        }

        // assumption customerId is passed in from the UI authenticated user swn
        order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
        order.UserName = cart.UserName;
        order.TotalPrice = cart.TotalPrice;

        await _basketService.CheckoutBasket(new CheckoutBasketRequest(order));

        return RedirectToAction("OrderSubmitted", "Confirmation");
    }
}
