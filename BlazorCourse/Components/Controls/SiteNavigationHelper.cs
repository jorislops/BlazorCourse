using System.Globalization;
using System.Reflection;
using System.Text;

namespace BlazorCourse.Components.Controls;

public class SiteNavigationHelper
{
    public static void UpdateTreeWidthCode(SiteNavigation.TreeItem treeItem, IEnumerable<RouteAttributeResult> routings)
    {
        if (treeItem.Url != null && treeItem.Url.StartsWith("/"))
        {
            treeItem.Url = treeItem.Url.Substring(1);
        }

        if (treeItem.Code != null)
        {
            var treeUrl = treeItem.Url;


            treeItem.Url = $"/code-viewer?code-url={treeItem.Code}&page-url={treeUrl}";
        }
        else if (treeItem.Code == null && treeItem.Url != null)
        {
            var treeUrl = treeItem.Url;
            // treeItem.Code = $"{FirstCharToUpper(Capitalise(treeUrl, "-", CultureInfo.InvariantCulture)).Replace("-", "")}.razor";
            var route = routings.Where(r =>
                    r.RouteAttribues.Any(w => w.ConstructorArguments.Any(a => a.Value.ToString()
                        .Equals("/" + treeUrl, StringComparison.OrdinalIgnoreCase))))
                .ToList();
            // .ForEach(r => treeItem.Code = $"{FirstCharToUpper(Capitalise(r.FullName, "-", CultureInfo.InvariantCulture)).Replace("-", "")}.razor");

            // var r = routings.FirstOrDefault(x => x.FullName.Contains("OneWay"));
            // var w = "/" + treeUrl;
            // bool result = r.RouteAttribues.Any(w => w.ConstructorArguments.Any(a =>
            //     a.Value.ToString().Equals(w, StringComparison.OrdinalIgnoreCase)));
            //
            // var ww = r.RouteAttribues.First().ConstructorArguments.First().Value;
            var codeFile = "/" + route[0].FullName.Replace("BlazorCourse.Components.Pages.", "").Replace(".", "/") + ".razor";
            treeItem.Url = $"/code-viewer?code-url={codeFile}&page-url={treeUrl}";
        }


        foreach (var child in treeItem.Child)
        {
            UpdateTreeWidthCode(child, routings);
        }
    }

    public class RouteAttributeResult
    {
        public string FullName { get; set; }
        public IEnumerable<CustomAttributeData> RouteAttribues { get; set; }
        public Type Type { get; set; }
    }

    public static string FirstCharToUpper(string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static string Capitalise(string text, string targets, CultureInfo culture)
    {
        bool capitalise = true;
        var result = new StringBuilder(text.Length);

        foreach (char c in text)
        {
            if (capitalise)
            {
                result.Append(char.ToUpper(c, culture));
                capitalise = false;
            }
            else
            {
                if (targets.Contains(c))
                    capitalise = true;

                result.Append(c);
            }
        }

        return result.ToString();
    }
}