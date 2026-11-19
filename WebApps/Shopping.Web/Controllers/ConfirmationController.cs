namespace Shopping.Web.Controllers;

public class ConfirmationController : Controller
{
    public IActionResult Contact()
    {
        ViewBag.Message = "Your email was sent.";
        return View("Index");
    }

    public IActionResult OrderSubmitted()
    {
        ViewBag.Message = "Your order submitted successfully.";
        return View("Index");
    }

    public IActionResult Index()
    {
        return View();
    }
}
