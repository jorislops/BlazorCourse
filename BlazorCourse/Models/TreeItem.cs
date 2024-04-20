using BlazorCourse.Components.Controls;

namespace BlazorCourse.Models;

public class TreeItem
{
    public string NodeId { get; set; } = null!;
    public string NodeText { get; set; } = null!;
    public string Icon { get; set; } = "folder";
    public bool Expanded { get; set; }
    public string? ParentId = null;
        
    public bool Selected { get; set; }
    public List<TreeItem>? Child { get; set; }
    public string? ImageUrl { get; set; }
    public string? Url { get; set; }
    public string? Code { get; set; }
    public List<RelatedCodeFile> RelatedFiles { get; set; } = new List<RelatedCodeFile>();
    public string? CodeUrl { get; set; }
    public string? PageUrl { get; set; }
    public TreeItem? Parent { get; set; }


    public static TreeItem? FindNode(List<TreeItem> nodes, string id)
    {
        foreach (var node in nodes)
        {
            if (node.NodeId == id)
            {
                return node;
            }

            if (node.Child != null)
            {
                var found = FindNode(node.Child, id);
                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }
    
    public static List<string> FindParentIds(List<TreeItem> nodes, string id)
    {
        List<string> parents = new List<string>();
        foreach (TreeItem node in nodes)
        {
            if (node.NodeId == id)
            {
                parents.Add(node.NodeId);
                return parents;
            }

            if (node.Child != null)
            {
                List<string> found = FindParentIds(node.Child, id);
                if (found.Count > 0)
                {
                    parents.Add(node.NodeId);
                    if(found.Contains(id)) return parents;
                    parents.AddRange(found);
                    return parents;
                }
            }
        }
        return parents;
    }
    public static TreeItem? FindNodeByUrl(List<TreeItem> nodes, string url)
    {
        foreach (TreeItem node in nodes)
        {
            if (node.Url != null)
            {
                if (node.Url.Contains(url))
                {
                    return node;
                }
            }
            
            if(node.Child != null)
            {
                TreeItem? found = FindNodeByUrl(node.Child, url);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
}