using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.XtraBars.Helpers;

namespace AuthrzForDevDx
{
    public struct ItemTree
    {
        public string ItemName;
        public string ItemCaption;
        public string ItemFather;
    }

    public class ScanMainMenu
    {        
        Dictionary<string, ItemTree> dicMenu = new Dictionary<string, ItemTree>();

        public Dictionary<string, ItemTree> getMenuItem(DevExpress.XtraBars.Bar mainMenu)
        {
            for (int i = 0; i < mainMenu.ItemLinks.Count; i++)
            {
                var o = mainMenu.ItemLinks[i];
                ItemTree it = new ItemTree();
                it.ItemName = o.Item.Name;
                it.ItemCaption = o.Caption;
                it.ItemFather = "0";
                dicMenu.Add(o.Item.Name, it);

                getAllItem(o.Item);
                
            }
            return dicMenu;
        }

        private void getAllItem(DevExpress.XtraBars.BarItem item)
        {
            if (item is DevExpress.XtraBars.BarSubItem)
            {
                var o = ((DevExpress.XtraBars.BarSubItem)item);
                for (int i = 0; i < o.ItemLinks.Count; i++)
                {
                    var o1 = o.ItemLinks[i];
                    ItemTree it = new ItemTree();
                    it.ItemCaption = o1.Caption;
                    it.ItemName = o1.Item.Name;
                    it.ItemFather = o1.OwnerItem.Name;

                    if (o1 is DevExpress.XtraBars.BarSubItemLink)
                    {
                        if ( o1.Item.Name != "" )
                            dicMenu.Add(o1.Item.Name, it);
                        getAllItem((DevExpress.XtraBars.BarItem)o1.Item);
                    }
                    else
                        if (o1.Item.Name != "")
                            dicMenu.Add(o1.Item.Name, it);
                }
            }
        }

        public Dictionary<string, ItemTree> getNavItem(DevExpress.XtraNavBar.NavBarControl navBar)
        {
            Dictionary<string, ItemTree> dicNavItem = new Dictionary<string, ItemTree>();

            for (int i = 0; i < navBar.Items.Count; i++)
            {
                ItemTree it = new ItemTree();
                it.ItemName = navBar.Items[i].Name;
                it.ItemCaption = navBar.Items[i].Caption;
                dicNavItem.Add(it.ItemName, it);
            }
            return dicNavItem;
        }
    }

    public class SetMainMenuEnable
    {
        //Dictionary<string, ItemTree> dicMenu = new Dictionary<string, ItemTree>();

        public void SetMenuItem(DevExpress.XtraBars.Bar mainMenu, Dictionary<string, bool> dicEnable)
        {
            //Dictionary<string, string> dicMenu = new Dictionary<string, string>();
            for (int i = 0; i < mainMenu.ItemLinks.Count; i++)
            {
                var o = mainMenu.ItemLinks[i];
                if( dicEnable.ContainsKey(o.Item.Name))
                    o.Item.Enabled = dicEnable[o.Item.Name];
                    
                SetAllItem(o.Item, dicEnable);
            }
        }

        private void SetAllItem(DevExpress.XtraBars.BarItem item, Dictionary<string, bool> dicEnable)
        {
            if (item is DevExpress.XtraBars.BarSubItem)
            {
                var o = ((DevExpress.XtraBars.BarSubItem)item);
                for (int i = 0; i < o.ItemLinks.Count; i++)
                {
                    var o1 = o.ItemLinks[i];

                    if (dicEnable.ContainsKey(o1.Item.Name))
                        o1.Item.Enabled = dicEnable[o1.Item.Name];

                    if (o1 is DevExpress.XtraBars.BarSubItemLink)
                        SetAllItem((DevExpress.XtraBars.BarItem)o1.Item, dicEnable);
                }
            }
        }

        public void SetNav(DevExpress.XtraNavBar.NavBarControl nav, Dictionary<string, bool> dicEnable)
        {
            for (int i = 0; i < nav.Items.Count; i++)
            {
                if (dicEnable.ContainsKey(nav.Items[i].Name))
                    nav.Items[i].Enabled = dicEnable[nav.Items[i].Name];
            }
        }
    }
}
