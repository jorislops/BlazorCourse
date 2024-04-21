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

        var currentUrl = uri.Replace(baseUri, "");
        var route = routings.Where(r =>
                r.RouteAttributes.Any(w => w.ConstructorArguments.Any(a => a.Value!.ToString()!
                    .Equals("/" + currentUrl, StringComparison.OrdinalIgnoreCase))))
            .ToList();
        var codeFile = "/" + route[0].FullName.Replace("BlazorCourse.Components.Pages.", "").Replace(".", "/") +
                       ".razor";
        var codeFileUri = baseUri + "code/Pages" + codeFile;
        return codeFileUri.Replace(".razor", "");
    }

    public record RouteAttributeResult
    {
        public required string FullName { get; set; }
        public required IEnumerable<CustomAttributeData> RouteAttributes { get; set; }
        public required Type Type { get; set; }
    }
}