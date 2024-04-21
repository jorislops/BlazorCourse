using BlazorCourse.Models;

namespace BlazorCourse.Components.Controls;

public class NavigationTreeData
{
    public static List<TreeItem> GetNavigationTree()
    {
        List<TreeItem> navigationTree = new();

        navigationTree.Add(new TreeItem
        {
            NodeId = "1",
            NodeText = "1 - Introduction",
            Icon = "folder",
            Expanded = false,
            Child = new List<TreeItem>
            {
                new()
                {
                    NodeId = "1-00", NodeText = "Counter Example", Url = "/counter", Code = "Counter.razor",
                    Icon = "html"
                },
                new()
                {
                    NodeId = "1-01", NodeText = "Razor Syntax", Url = "/razor-syntax", Code = "RazorSyntax.razor",
                    Icon = "html"
                },
                new()
                {
                    NodeId = "1-02", NodeText = "Data Binding", Icon = "folder",
                    Child = new List<TreeItem>
                    {
                        new()
                        {
                            NodeId = "1-02-01", NodeText = "One way", Url = "/data-binding/one-way", Icon = "html"
                        },
                        new()
                        {
                            NodeId = "1-02-02", NodeText = "Two way", Url = "/data-binding/two-way", Icon = "html"
                        },

                        new() { NodeId = "1-02-03", NodeText = "Object", Url = "/data-binding/object", Icon = "html" },

                        new()
                        {
                            NodeId = "1-02-04", NodeText = "Get Set", Url = "/data-binding/get-set", Icon = "html"
                        },
                        new()
                        {
                            NodeId = "1-02-05", NodeText = "Get After", Url = "/data-binding/bind-after", Icon = "html"
                        }
                    }
                },
                new() { NodeId = "1-03", NodeText = "Events", Url = "/events", Icon = "html" },
                new() { NodeId = "1-04", NodeText = "Simple Todo Example", Url = "/todo", Icon = "html" }
            }
        });
        navigationTree.Add(new TreeItem
        {
            NodeId = "2",
            NodeText = "2 - Components Basics",
            Icon = "folder",
            Expanded = false,
            Child = new List<TreeItem>
            {
                new()
                {
                    NodeId = "2-01", NodeText = "Construction", Icon = "folder",
                    Child = new List<TreeItem>
                    {
                        new()
                        {
                            NodeId = "2-01-01", NodeText = "Simple Component", Url = "/simple-component-example",
                            Icon = "html"
                        },
                        new()
                        {
                            NodeId = "2-01-02", NodeText = "Partial Component",
                            Url = "/simple-component-example-partial", Icon = "html"
                        },
                        new()
                        {
                            NodeId = "2-01-03", NodeText = "Inheritance Component",
                            Url = "/simple-component-example-inheritance", Icon = "html"
                        }
                    }
                },
                new()
                {
                    NodeId = "2-02", NodeText = "Parameters", Icon = "folder",
                    Child = new List<TreeItem>
                    {
                        new()
                        {
                            NodeId = "2-02-01", NodeText = "Component Parameter", Url = "/component-parameter",
                            Icon = "html",
                            RelatedFiles = new List<RelatedCodeFile>
                            {
                                new()
                                {
                                    Text = "CounterWithParameter.razor",
                                    CodeUrl = "/Components/Parameters/ComponentParameter.razor"
                                }
                            }
                        },
                        new()
                        {
                            NodeId = "2-02-02", NodeText = "Component Chained Binding",
                            Url = "/component-with-chained-binding", Icon = "html"
                        }
                    }
                },
                new()
                {
                    NodeId = "2-03", NodeText = "Event Callback", Url = "/component-events", Icon = "html"
                },
                new()
                {
                    NodeId = "2-04", NodeText = "State management", Icon = "folder",
                    Child = new List<TreeItem>
                    {
                        new()
                        {
                            NodeId = "2-04-1", NodeText = "Component State", Url = "counter-component-state",
                            Icon = "html"
                        },
                        new()
                        {
                            NodeId = "2-04-2", NodeText = "Counter State Static (don't)",
                            Url = "/counter-state-static-variable", Icon = "html"
                        },
                        new()
                        {
                            NodeId = "2-04-3", NodeText = "Cascade Parameters", Url = "/counter-cascade-state",
                            Icon = "html"
                        }
                    }
                },

                new()
                {
                    NodeId = "2-07", NodeText = "Parent Child And Service Example",
                    Url = "/parent-child-and-service-example", Icon = "html"
                },

                new() { NodeId = "2-06", NodeText = "Counter Les Demo", Url = "/counter-example-les", Icon = "html" },
                new() { NodeId = "2-05", NodeText = "Todo Example", Url = "/todo-list", Icon = "html" }
            }
        });

        navigationTree.Add(new TreeItem
        {
            NodeId = "3",
            NodeText = "3 - Databases",
            Icon = "folder",
            Expanded = false,
            Child =
            [
                new TreeItem
                {
                    NodeId = "3-01", NodeText = "Todo db (no repo)", Url = "/todo-without-repository", Icon = "html"
                },
                new TreeItem { NodeId = "3-02", NodeText = "Todo list db", Url = "/todo-list-db", Icon = "html" },
                new TreeItem { NodeId = "3-03", NodeText = "Bier", Url = "/bier", Icon = "html" },
                new TreeItem { NodeId = "3-04", NodeText = "Brouwer", Url = "/brouwer", Icon = "html" }
            ]
        });

        navigationTree.Add(new TreeItem
        {
            NodeId = "4",
            NodeText = "4 - Forms & Generic Components",
            Icon = "folder",
            Expanded = false,
            Child =
            [
                new TreeItem { NodeId = "4-01", NodeText = "Form + Validation", Url = "forms/add-bier", Icon = "html" },
                new TreeItem
                    { NodeId = "4-02", NodeText = "Fluent Validation", Url = "forms/add-bier-fluent", Icon = "html" },
                new TreeItem
                    { NodeId = "4-03", NodeText = "Render Fragments", Url = "/render-fragments", Icon = "html" },
                new TreeItem
                    { NodeId = "4-04", NodeText = "Generic Data table", Url = "/generic-data-table", Icon = "html" }
            ]
        });

        navigationTree.Add(new TreeItem
        {
            NodeId = "5",
            NodeText = "5 UI Library - Blazorise",
            Icon = "folder",
            Expanded = false,
            Child =
            [
                new TreeItem { NodeId = "5-01", NodeText = "Datagrid", Url = "/blazorise-datagrid", Icon = "html" },
                new TreeItem
                {
                    NodeId = "5-02", NodeText = "Advanced Datagrid", Url = "/advanced-datatable-example", Icon = "html"
                }
            ]
        });

        navigationTree.Add(new TreeItem
        {
            NodeId = "6",
            NodeText = "Service for State Management (advanced)",
            Icon = "folder",
            Expanded = false,
            Child =
            [
                new TreeItem
                {
                    NodeId = "6-01", NodeText = "State Management Service with Notification",
                    Url = "/counter-state-service", Icon = "html"
                }
                // new SiteNavigation.TreeItem() { NodeId = "6-02", NodeText = "State Management Service multiple components (Todo)", Url = "/todo-state-service", Icon = "html" },
                // new SiteNavigation.TreeItem() { NodeId = "6-03", NodeText = "State Management Service with Fluxor (Todo)", Url = "/todo-state-fluxor", Icon = "html" }
            ]
        });


        //UpdateCode(navigationTree);


        return navigationTree;
    }
}

public class RelatedCodeFile
{
    public string Text { get; set; } = null!;
    public string CodeUrl { get; set; } = null!;
}