using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.ViewComponents
{
    public class CourseGroupComponent : ViewComponent
    {
        private ICourseSerervice _courseSerervice;

        public CourseGroupComponent(ICourseSerervice courseSerervice)
        {
            _courseSerervice = courseSerervice;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult) View("CourseGroup", _courseSerervice.GetAllGroup()));
        }
    }
}