namespace Scroll.Web;

public class SiteSetting
{
    public string Title { get; set; }
    public string Copyright { get; set; }
    public int Year => DateTime.Now.Year;
    public static string Key => nameof(SiteSetting);
}
