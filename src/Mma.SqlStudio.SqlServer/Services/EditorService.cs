using System;
using System.Collections.Generic;
using System.Linq;

namespace Mma.SqlStudio.SqlServer.Services
{
    public class EditorService
    {
        public List<EditorTab> Tabs { get; private set; } = new();
        public QueryResult? LastResult { get; private set; }
        public bool IsExecuting { get; private set; }
        
        public event Action? OnChange;

        public void AddTab(string name, string content)
        {
            foreach (var t in Tabs) t.IsActive = false;
            var existing = Tabs.FirstOrDefault(t => t.Name == name);
            if (existing != null) existing.IsActive = true;
            else Tabs.Add(new EditorTab(name, true, content));
            NotifyStateChanged();
        }

        public void SelectTab(EditorTab tab)
        {
            foreach (var t in Tabs) t.IsActive = false;
            tab.IsActive = true;
            NotifyStateChanged();
        }

        public void CloseTab(EditorTab tab)
        {
            Tabs.Remove(tab);
            if (tab.IsActive && Tabs.Any()) Tabs.Last().IsActive = true;
            NotifyStateChanged();
        }

        public void SetResult(QueryResult result)
        {
            LastResult = result;
            IsExecuting = false;
            NotifyStateChanged();
        }

        public void SetExecuting(bool executing)
        {
            IsExecuting = executing;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class EditorTab
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Content { get; set; }
        public EditorTab(string name, bool isActive, string content = "")
        {
            Name = name;
            IsActive = isActive;
            Content = content;
        }
    }
}
