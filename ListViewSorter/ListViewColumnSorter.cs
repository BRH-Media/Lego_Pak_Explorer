using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TT_Games_Explorer.ListViewSorter.Enums;

// ReSharper disable UnusedVariable

namespace TT_Games_Explorer.ListViewSorter
{
    public class ListViewColumnSorter : IComparer
    {
        private readonly CaseInsensitiveComparer _ciCompare;
        private SortTypes[] _columnSortType;
        private bool _warned;
        private ListView _lv;

        public void lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == SortColumn)
            {
                Order = Order != SortOrder.Ascending ? SortOrder.Ascending : SortOrder.Descending;
            }
            else
            {
                SortColumn = e.Column;
                Order = SortOrder.Descending;
            }
            _lv.Sort();
            _lv.Refresh();
            Application.DoEvents();
        }

        public ListViewColumnSorter()
        {
            SortColumn = 0;
            Order = SortOrder.None;
            _ciCompare = new CaseInsensitiveComparer();
        }

        public void SetColumnHeaders(string csvString)
        {
            var str = csvString;
            var chArray = new[] { ',' };
            foreach (var text in str.Split(chArray))
                _lv.Columns.Add(text);
        }

        public void Initialize(ListView lvv, string initSortTypes, string columnHeaders)
        {
            _lv = lvv;
            _lv.ColumnClick += lv_ColumnClick;
            _lv.FullRowSelect = true;
            _lv.GridLines = true;
            _lv.HideSelection = false;
            _lv.MultiSelect = true;
            _lv.View = View.Details;
            _lv.ListViewItemSorter = this;
            _lv.Sorting = SortOrder.Ascending;
            _lv.AutoArrange = true;
            if (!string.IsNullOrEmpty(columnHeaders))
                SetColumnHeaders(columnHeaders);
            switch (initSortTypes)
            {
                case "":
                    break;

                case null:
                    break;

                default:
                    if (initSortTypes.IndexOf(",", StringComparison.Ordinal) < 1)
                    {
                        MessageBox.Show(@"ListViewSorter.Initialize initSortTypes argument invalid");
                        Debugger.Break();
                    }
                    var strArray = initSortTypes.Split(',');
                    _columnSortType = new SortTypes[strArray.Length];
                    var index = 0;
                    foreach (var str in strArray)
                    {
                        switch (str.ToLower())
                        {
                            case "num":
                                _columnSortType[index] = SortTypes.StNumeric;
                                break;

                            case "hex":
                                _columnSortType[index] = SortTypes.StHexNumber;
                                break;

                            case "text":
                                _columnSortType[index] = SortTypes.StText;
                                break;

                            default:
                                _columnSortType[index] = SortTypes.StNone;
                                break;
                        }
                        ++index;
                    }
                    break;
            }
        }

        public int Compare(object x, object y)
        {
            var num1 = 0;
            var listViewItem1 = (ListViewItem)x;
            var listViewItem2 = (ListViewItem)y;
            if (_columnSortType == null)
            {
                if (_warned) return 0;

                MessageBox.Show(@"You have not set the ColumnSortTypes!");
                _warned = true;
                return 0;
            }
            if (SortColumn > _columnSortType.Length)
                return 0;
            var sortTypes = _columnSortType[SortColumn];
            if (sortTypes == SortTypes.StNone)
                return 0;
            string text1;
            string text2;
            if (SortColumn == 0)
            {
                text1 = listViewItem1?.Text;
                text2 = listViewItem2?.Text;
            }
            else
            {
                text1 = listViewItem1?.SubItems[SortColumn].Text;
                text2 = listViewItem2?.SubItems[SortColumn].Text;
            }
            switch (sortTypes)
            {
                case SortTypes.StText:
                    num1 = _ciCompare.Compare(text1, text2);
                    break;

                case SortTypes.StNumeric:
                    if (IsWholeNumber(text1) && IsWholeNumber(text2))
                    {
                        num1 = _ciCompare.Compare(Convert.ToInt32(text1), Convert.ToInt32(text2));
                    }
                    break;

                case SortTypes.StHexNumber:
                    if (IsHexNumber(text1) && IsHexNumber(text2))
                    {
                        num1 = _ciCompare.Compare(int.Parse(text1 ?? string.Empty, NumberStyles.HexNumber), int.Parse(text2 ?? string.Empty, NumberStyles.HexNumber));
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (Order == SortOrder.Ascending)
                return -num1;
            return Order == SortOrder.Descending ? num1 : 0;
        }

        public int SortColumn { set; get; }

        public SortOrder Order { set; get; }

        private static bool IsWholeNumber(string strNumber) => !new Regex("[^0-9]").IsMatch(strNumber);

        private static bool IsHexNumber(string strNumber)
        {
            try
            {
                int.Parse(strNumber, NumberStyles.HexNumber);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}