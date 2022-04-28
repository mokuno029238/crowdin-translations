using System.Globalization;
using Crowdin.Translations.Web.Datas;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crowdin.Translations.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly string _defaultColorId;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Colors = new List<ColorData>
            {
                new ColorData { Id = "red", Name = "Red" },
                new ColorData { Id = "yellow", Name = "Yellow" },
                new ColorData { Id = "blue", Name = "Blue" }
            };
            SupportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-TW"),
                new CultureInfo("ja-JP"),
                new CultureInfo("aa-ER"),
            };
            _defaultColorId = Colors.First().Id;
        }

        public IList<ColorData> Colors { get; set; }

        public IList<CultureInfo> SupportedCultures { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ColorId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Culture { get; set; }

        public ColorData SelectedColor { get; set; }

        public string SelectedCulture { get; set; }

        public void OnGet()
        {
            var colorId = string.IsNullOrEmpty(ColorId) ? _defaultColorId : ColorId;
            var color = Colors.FirstOrDefault(c => c.Id == colorId);
            color ??= Colors.First();
            ColorId = color.Id;
            SelectedColor = color;

            var culture = string.IsNullOrEmpty(Culture) ? Thread.CurrentThread.CurrentUICulture.Name : Culture;
            Culture = culture;
            SelectedCulture = culture;

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );
        }
    }
}
