using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebCalculator.Models;

namespace WebCalculator.Controllers;

//Расположение*
//Наименование+Controller*
// наследование Controller
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


    public IActionResult WithoutJS(TmpForTets? test)
    {
        return View(test);
    }

    [HttpPost]
    public async Task<IActionResult> WithoutJsS(TmpForTets model, string btn, bool calculate = false)
    {
        if (!calculate)
        {
            if (btn != "=")
            {
                model.expresion += btn;
            }
            else
            {
                // Здесь может быть логика вычисления выражения
                // Например, использование DataTable.Compute() или другой метод вычисления строки выражения
            }
        }
        else
        {
            double tmp = await _Calculator.Evaluate(model.expresion);
            model.expresion = tmp.ToString();
        }

        return RedirectToAction("WithoutJS", model); // Вернуть обновленную модель обратно в представление
    }



}
