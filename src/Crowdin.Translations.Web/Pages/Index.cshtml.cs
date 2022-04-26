using Crowdin.Translations.Web.Datas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdin.Translations.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string _defaultColorId;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            Colors = new List<ColorData>
            {
                new ColorData { Id = "red", Name = "Red" },
                new ColorData { Id = "yellow", Name = "Yellow" },
                new ColorData { Id = "blue", Name = "Blue" }
            };
            _defaultColorId = Colors.First().Id;
        }

        public IList<ColorData> Colors { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ColorId { get; set; }

        public ColorData SelectedColor { get; set; }

        public void OnGet()
        {
            var colorId = string.IsNullOrEmpty(ColorId) ? _defaultColorId : ColorId;
            var color = Colors.FirstOrDefault(c => c.Id == colorId);
            color ??= Colors.First();
            ColorId = color.Id;
            SelectedColor = color;
        }
    }
}
