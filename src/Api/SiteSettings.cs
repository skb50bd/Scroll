namespace Scroll.Api;

public class SiteSetting
{
    public string Title { get; set; } = string.Empty;
    public string Copyright { get; set; } = string.Empty;
    public int Year => DateTime.Now.Year;
    public static readonly string Key = "SiteSetting";
}