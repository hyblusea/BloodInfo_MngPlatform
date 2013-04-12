using System.Collections.Generic;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;
using System.Reflection;
using System.Windows.Forms;


namespace UcCommentsForDX
{
    [Designer(typeof(CommentsForDXDesigner), typeof(IDesigner))]
    public partial class UcCommentsForDXGrid : Component
    {
        public DevExpress.XtraGrid.GridControl Grids { get; set; }

        public UcCommentsForDXGrid()
        {
            InitializeComponent();
        }

        public UcCommentsForDXGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }


    public class CommentsForDXDesigner : ComponentDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(
                        new smtTagActionList(this.Component));
                }
                return actionLists;
            }
        }
    }


    public class smtTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        private UcCommentsForDXGrid commentsForDx;

        private TextBox txt1 = new TextBox();
        private DesignerActionUIService designerActionUISvc = null;

        public smtTagActionList(IComponent component) : base(component)
        {
            this.commentsForDx = component as UcCommentsForDXGrid;

            // Cache a reference to DesignerActionUIService, 
            // so the DesigneractionList can be refreshed.
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        public DevExpress.XtraGrid.GridControl Grids
        {
            get
            { return commentsForDx.Grids; }
            set
            {
                TypeDescriptor.GetProperties(typeof(UcCommentsForDXGrid))["Grids"]
                  .SetValue(commentsForDx, value);
            }
        }

        public void RefreshGridControls()
        {
            Dictionary<string, string> dicColComments = new Dictionary<string, string>();
            var ds = Grids.DataSource ;

            // 获取BindingSource绑定的类
            if (ds.GetType().FullName == "System.Windows.Forms.BindingSource")
            {
                if (((BindingSource)ds).DataSource != null)
                {
                    Type t = (Type)((BindingSource)ds).DataSource;
                    PropertyInfo[] pptInfo = t.GetProperties();
                    for (int i = 0; i < pptInfo.Length; i++)
                    {
                        var v = pptInfo[i].GetCustomAttributes(false);
                        for (int j = 0; j < v.Length; j++)
                        {

                            if (v[j].GetType().FullName == "PetaPoco.CommentsAttribute")
                            {
                                PropertyInfo infoOfComments = v[j].GetType().GetProperty("Comments");
                                if (infoOfComments != null)
                                {
                                    string sComments = infoOfComments.GetValue(v[j], null).ToString();
                                    dicColComments.Add(pptInfo[i].Name, sComments);
                                }
                            }
                        }
                    }
                }
            }

            // 绑定Grid列标题
            for (int i = 0; i < commentsForDx.Container.Components.Count; i++)
            {
                if (commentsForDx.Container.Components[i].GetType().FullName == "DevExpress.XtraGrid.Columns.GridColumn" || 
                    commentsForDx.Container.Components[i].GetType().FullName == "DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn")
                {
                    DevExpress.XtraGrid.Columns.GridColumn col = ((DevExpress.XtraGrid.Columns.GridColumn)commentsForDx.Container.Components[i]);

                    if (col.FieldName != null)
                    {
                        string sComments = string.Empty;
                        dicColComments.TryGetValue(col.FieldName, out sComments);
                        if (!string.IsNullOrEmpty(sComments))
                        {
                            col.Caption = sComments;
                        }
                    }
                }
            }

            MessageBox.Show("Comments标签绑定成功.");
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            // header Item
            DesignerActionHeaderItem hInfo = new DesignerActionHeaderItem("请选择Grid控件", "Info");
            DesignerActionHeaderItem hAction = new DesignerActionHeaderItem("绑定中文标题", "Action");
            DesignerActionHeaderItem hDescription = new DesignerActionHeaderItem("将实体中的Comments标签值,绑定到Grid控件的标题中.");

            items.Add(hInfo);
            items.Add(hAction);
            items.Add(hDescription);

            items.Add(new DesignerActionPropertyItem("Grids", "表格控件", "Info", ""));
            items.Add(new DesignerActionMethodItem(this, "RefreshGridControls", "绑定Comments标签", "Action", true));

            return items;
        }

    }
}
