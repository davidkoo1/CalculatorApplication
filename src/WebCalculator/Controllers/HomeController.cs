using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebCalculator.Controllers;

public class HomeController : Controller
{
    private readonly Calculator _Calculator;

    public HomeController(Calculator calculator)
    {
        _Calculator = calculator;
    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Calculate([FromBody] string expression)
    {
        try
        {
            var result = await _Calculator.Evaluate(expression);

            return Json(new { StatusCode = 200, Message = result });
        }
        catch (Exception ex)
        {
            return Json(new { StatusCode = 500, Message = ex.Message });
        }
    }

}
