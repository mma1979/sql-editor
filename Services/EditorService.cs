using System;
using System.Collections.Generic;
using System.Linq;

namespace sql_editor.Services
{
    public class EditorService
    {
        public List<EditorTab> Tabs { get; private set; } = new();
        public event Action? OnChange;

        public void AddTab(string name, string content)
        {
            // Deactivate others
            foreach (var t in Tabs) t.IsActive = false;

            // Check if already open
            var existing = Tabs.FirstOrDefault(t => t.Name == name);
            if (existing != null)
            {
                existing.IsActive = true;
            }
            else
            {
                Tabs.Add(new EditorTab(name, true, content));
            }
            
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
            if (tab.IsActive && Tabs.Any())
            {
                Tabs.Last().IsActive = true;
            }
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
