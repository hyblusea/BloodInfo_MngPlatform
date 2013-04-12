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
using System.Reflection;

namespace AuthrzForDevDx
{
    public class ScanFormControl
    {
        Dictionary<string, ItemTree> dicCtrl = new Dictionary<string, ItemTree>();

        public Dictionary<string, ItemTree> GetButton(Control c)
        {
            ItemTree it = new ItemTree();
            it.ItemName = c.Name;
            it.ItemFather = "0";
            it.ItemCaption = c.Text;
            dicCtrl.Add(c.Name, it);

            Type form =c.GetType();
            Control[] cc = c.Controls.Find("barbtnAddRegist", true);
            FieldInfo[] fieldInfos = form.GetFields( BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                if (fieldInfos[i].FieldType.FullName == "DevExpress.XtraBars.BarButtonItem")
                {
                    var o1 = fieldInfos[i].GetValue(c);
                    it = new ItemTree();
                    it.ItemCaption = ((DevExpress.XtraBars.BarButtonItem)o1).Caption;
                    it.ItemName = ((DevExpress.XtraBars.BarButtonItem)o1).Name;
                    it.ItemFather = c.Name;
                    dicCtrl.Add(it.ItemName, it);
                }
            }

            return dicCtrl;
        }

        private void GetControl(FieldInfo[] fieldInfos)
        {
            for (int i = 0; i < fieldInfos.Length; i++ )
            {
                if (fieldInfos[i].FieldType.FullName == "DevExpress.XtraBars.BarButtonItem")
                {
                    //fieldInfos[i].GetValue(o);
                    Type t = fieldInfos[i].FieldType;
                    PropertyInfo[] pinfo =  t.GetProperties();
                    PropertyInfo p = t.GetProperty("Caption");
                    string oo = string.Empty;
                    //p.SetValue(

                    ItemTree it = new ItemTree();
                    it.ItemCaption = t.GetProperty("Caption").ToString();
                    it.ItemName = fieldInfos[i].Name;
                    //it.ItemFather = c.Parent.Name;

                    //dicCtrl.Add(fieldInfos[i].Name, 
                    //((DevExpress.XtraBars.Bar)fieldInfos[i])
                }
                //ItemTree it = new ItemTree();
                //it.ItemCaption = c.Text;
                //it.ItemName = c.Name;
                //it.ItemFather = c.Parent.Name;
            } 
        }

        private void GetBarBtn(DevExpress.XtraBars.BarDockControl barDocCtrol)
        {
            //barDocCtrol.Manager.Form
        }

        public void SetBtn(ComponentCollection componts, Dictionary<string, bool> dic)
        {
            var o = componts;
            for (int i = 0; i < o.Count; i++)
            {
                if (o[i] is DevExpress.XtraBars.BarManager)
                {
                    var btns = ((DevExpress.XtraBars.BarManager)o[i]).Items;
                    for (int k = 0; k < btns.Count; k++)
                    {
                        if (dic.ContainsKey(btns[k].Name))
                            btns[k].Enabled = dic[btns[k].Name];
                    }
                }
            }
        }
    }
}
