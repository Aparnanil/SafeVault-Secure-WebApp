using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    public IActionResult Dashboard()
    {
        return Content("Welcome to Admin Dashboard");
    }
}
