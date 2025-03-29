using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspExample11307.Pages;

public class Index : PageModel
{
    public string DateTime { get; set; }
    public void OnGet()
    {
        DateTime = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
}