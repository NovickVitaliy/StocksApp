using Microsoft.AspNetCore.Mvc;

namespace StockAppWithxUnit.ViewComponents
{
  public class StockViewComponent : ViewComponent
  {

    public async Task<IViewComponentResult> InvokeAsync(Dictionary<string, object> CurrentStock)
    {
      return View(CurrentStock);
    }

  }
}
