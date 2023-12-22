using Microsoft.AspNetCore.Components;

namespace Scroll.Web.Client.Layout;

public partial class GuideCard : ComponentBase
{
    [Parameter]
    public CategoryDto Category { get; set; } = default!;
}