using System;
using System.Collections.Generic;
using System.Text;

namespace DBLite.Models
{
    class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }

    public enum MenuItemType
    {
        Browse,
        About
    }
}
