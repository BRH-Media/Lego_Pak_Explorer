using Lego_Pak_Explorer.Properties;
using Microsoft.Win32;
using System;
using System.Windows.Forms;
using TT_Games_Explorer.Properties;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable LocalizableElement

namespace Lego_Pak_Explorer
{
    public class MruManager
    {
        private readonly string _subKeyName;
        private readonly ToolStripMenuItem _parentMenuItem;
        private readonly Action<object, EventArgs> _onRecentFileClick;
        private readonly Action<object, EventArgs> _onClearRecentFilesClick;

        private void _onClearRecentFiles_Click(object obj, EventArgs evt)
        {
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(_subKeyName, true);
                if (registryKey == null)
                    return;
                foreach (var valueName in registryKey.GetValueNames())
                    registryKey.DeleteValue(valueName, true);
                registryKey.Close();
                _parentMenuItem.DropDownItems.Clear();
                _parentMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            _onClearRecentFilesClick?.Invoke(obj, evt);
        }

        private void _refreshRecentFilesMenu()
        {
            RegistryKey registryKey;
            try
            {
                registryKey = Registry.CurrentUser.OpenSubKey(_subKeyName, false);
                if (registryKey == null)
                {
                    _parentMenuItem.Enabled = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot open recent files registry key:\n{ex}");
                return;
            }
            _parentMenuItem.DropDownItems.Clear();
            foreach (var valueName in registryKey.GetValueNames())
            {
                if (registryKey.GetValue(valueName, null) is string text)
                {
                    var toolStripItem = _parentMenuItem.DropDownItems.Add(text);
                    toolStripItem.Image = Resources.folder;
                    toolStripItem.Click += _onRecentFileClick.Invoke;
                }
            }
            if (_parentMenuItem.DropDownItems.Count == 0)
            {
                _parentMenuItem.Enabled = false;
            }
            else
            {
                _parentMenuItem.DropDownItems.Add("-");
                _parentMenuItem.DropDownItems.Add("Clear list").Click += _onClearRecentFiles_Click;
                _parentMenuItem.Enabled = true;
            }
        }

        public void AddRecentFile(string fileNameWithFullPath)
        {
            try
            {
                var subKey = Registry.CurrentUser.CreateSubKey(_subKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                int num;
                for (num = 0; subKey?.GetValue(num.ToString(), null) is string str; ++num)
                {
                    if (str != fileNameWithFullPath) continue;

                    subKey.Close();
                    _refreshRecentFilesMenu();
                }
                subKey?.SetValue(num.ToString(), fileNameWithFullPath);
                subKey?.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void RemoveRecentFile(string fileNameWithFullPath)
        {
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(_subKeyName, true);
                foreach (var valueName in registryKey?.GetValueNames())
                {
                    if (registryKey?.GetValue(valueName, null) as string != fileNameWithFullPath) continue;

                    registryKey?.DeleteValue(valueName, true);
                    _refreshRecentFilesMenu();
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            _refreshRecentFilesMenu();
        }

        public MruManager(
          ToolStripMenuItem parentMenuItem,
          string nameOfProgram,
          Action<object, EventArgs> onRecentFileClick,
          Action<object, EventArgs> onClearRecentFilesClick = null)
        {
            if (parentMenuItem != null && onRecentFileClick != null)
            {
                switch (nameOfProgram)
                {
                    case "":
                    case null:
                        break;

                    default:
                        if (!nameOfProgram.Contains("\\"))
                        {
                            _parentMenuItem = parentMenuItem;
                            _onRecentFileClick = onRecentFileClick;
                            _onClearRecentFilesClick = onClearRecentFilesClick;
                            _subKeyName = $"Software\\{nameOfProgram}\\MRU";
                            _refreshRecentFilesMenu();
                            return;
                        }
                        break;
                }
            }
            throw new ArgumentException("Bad argument.");
        }
    }
}