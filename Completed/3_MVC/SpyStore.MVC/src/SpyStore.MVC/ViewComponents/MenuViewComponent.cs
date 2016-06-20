using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SpyStore.MVC.DataAccess;

namespace SpyStore.MVC.ViewComponents
{
    //https://docs.asp.net/projects/mvc/en/latest/views/view-components.html
    //http://www.tugberkugurlu.com/archive/exciting-things-about-asp-net-vnext-series-mvc-view-components
//The view 'Components/Menu/Default' was not found. The following locations were searched: /Views/Home/Components/Menu/Default.cshtml /Views/Shared/Components/Menu/Default.cshtml.
//Where does the view component need to live? Check this.

    public class MenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cats = await WebAPICalls.GetCategories();
            if (cats != null)
            {
                return View("MenuView", cats);
            }
            return new ContentViewComponentResult("There was an error getting the categories");
        }
    }
}
