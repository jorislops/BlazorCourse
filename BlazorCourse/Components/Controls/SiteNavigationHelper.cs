using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace BlazorCourse.Components.Controls;

public static class SiteNavigationHelper
{
    public static string GetFileUrl(string uri, string baseUri)
    {
        var allComponents = Assembly
            .GetExecutingAssembly()
            .ExportedTypes
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

        var routableComponents = allComponents
            .Where(c => c
                .GetCustomAttributes(true)
                .OfType<RouteAttribute>()
                .Any());
        var routings = routableComponents
            .Select(x => new RouteAttributeResult
                {
                    Type = x,
                    FullName = x.FullName!,
                    RouteAttributes = x.CustomAttributes.Where(ca => ca.AttributeType == typeof(RouteAttribute))
                }
            );

        
        

        var currentUrl = new Uri(uri).AbsolutePath; 
        
        //remove query parameter part
        if(currentUrl.Contains("?"))
            currentUrl = currentUrl.Substring(0, currentUrl.IndexOf("?", StringComparison.Ordinal));

        var route = FindRoute(currentUrl);
        
        var codeFile = "/" + route[0].FullName.Replace("BlazorCourse.Components.Pages.", "").Replace(".", "/") +
                       ".razor";
        var codeFileUri = baseUri + "code/Pages" + codeFile;
        return codeFileUri.Replace(".razor", "");

        List<RouteAttributeResult> FindRoute(string url)
        {
            var routeAttributeResults = routings.Where(r =>
                    r.RouteAttributes.Any(w => w.ConstructorArguments.Any(a => 
                        a.Value!.ToString()!.Equals(url, StringComparison.OrdinalIgnoreCase)
                        ||
                        a.Value!.ToString()!.StartsWith(url, StringComparison.OrdinalIgnoreCase)
                        
                        )))
                .ToList();

            if (routeAttributeResults.Count == 0)
            {
                var urlParts = url.Split("/");
                if (urlParts.Length > 1)
                {
                    var newUrl = string.Join("/", urlParts.Take(urlParts.Length - 1));
                    return FindRoute(newUrl);
                }
            }
            
            return routeAttributeResults;
        }
    }

    public record RouteAttributeResult
    {
        public required string FullName { get; set; }
        public required IEnumerable<CustomAttributeData> RouteAttributes { get; set; }
        public required Type Type { get; set; }
    }
}