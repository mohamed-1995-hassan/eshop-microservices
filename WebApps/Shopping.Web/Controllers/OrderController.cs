namespace Shopping.Web.Controllers;

public class OrderController : Controller
{
    private readonly IOrderingService _orderingService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderingService orderingService, ILogger<OrderController> logger)
    {
        _orderingService = orderingService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        // assumption customerId is passed in from the UI authenticated user swn
        var customerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");

        var response = await _orderingService.GetOrdersByCustomer(customerId);
        return View(response.Orders);
    }
}
