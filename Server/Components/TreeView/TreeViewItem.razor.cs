﻿using Microsoft.AspNetCore.Components;

namespace Remotely.Server.Components.TreeView;

public partial class TreeViewItem<T> : ComponentBase
{
    [CascadingParameter]
    public required TreeView<T> ParentTree { get; set; }

    [Parameter]
    [EditorRequired]
    public required T Source { get; set; }

    [Parameter]
    [EditorRequired]
    public required Func<T, List<T>> ChildItemSelector { get; set; }

    [Parameter]
    [EditorRequired]
    public required Func<T, string> HeaderSelector { get; set; }

    [Parameter]
    [EditorRequired]
    public required Func<T, string> ItemIconCssSelector { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public int IndentLevel { get; set; }

    [Parameter]
    [EditorRequired]
    public required Func<T, TreeItemType> ItemTypeSelector { get; set; }

    [Parameter]
    public EventCallback<T> ItemSelected { get; set; }

    [Parameter]
    [EditorRequired]
    public required Func<T, string> KeySelector { get; set; }


    public bool IsExpanded { get; set; }

    public bool IsSelected { get; set; }

    protected override void OnInitialized()
    {
        if (ParentTree is null)
        {
            throw new ArgumentException("TreeViewItem must be contained in a TreeView.");
        }

        base.OnInitialized();
    }

    private void OnItemClick()
    {
        if (ItemTypeSelector.Invoke(Source) == TreeItemType.Folder)
        {
            IsExpanded = !IsExpanded;
        }

        ItemSelected.InvokeAsync(Source);
        ParentTree.SelectedNode = this;
    }

    private string GetActiveClass()
    {
        if (ParentTree.SelectedNode == this)
        {
            return "bg-info";
        }
        return "bg-secondary";
    }
}
