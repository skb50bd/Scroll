namespace Scroll.Web;

public class SiteSetting
{
    public string Title { get; set; } = string.Empty;
    public string Copyright { get; set; } = string.Empty;
    public int Year => DateTime.Now.Year;
    public static string Key => nameof(SiteSetting);
}
