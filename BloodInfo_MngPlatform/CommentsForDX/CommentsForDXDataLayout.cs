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
    partial class CommentsForDXDataLayout
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }

    [Designer(typeof(CommentsForDXDataLayoutDesigner), typeof(IDesigner))]
    public partial class CommentsForDXDataLayout : Component
    {
        public DevExpress.XtraDataLayout.DataLayoutControl DataLayouts { get; set; }

        public CommentsForDXDataLayout()
        {
            InitializeComponent();
        }

        public CommentsForDXDataLayout(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    public class CommentsForDXDataLayoutDesigner : ComponentDesigner
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
                        new smtTagActionList_dataLayout(this.Component));
                }
                return actionLists;
            }
        }
    }


    public class smtTagActionList_dataLayout : System.ComponentModel.Design.DesignerActionList
    {
        private CommentsForDXDataLayout commentsForDataLayout;

        private TextBox txt1 = new TextBox();
        private DesignerActionUIService designerActionUISvc = null;

        public smtTagActionList_dataLayout(IComponent component)
            : base(component)
        {
            this.commentsForDataLayout = component as CommentsForDXDataLayout;

            // Cache a reference to DesignerActionUIService, 
            // so the DesigneractionList can be refreshed.
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        public DevExpress.XtraDataLayout.DataLayoutControl DataLayouts
        {
            get
            { return commentsForDataLayout.DataLayouts; }
            set
            {
                TypeDescriptor.GetProperties(typeof(CommentsForDXDataLayout))["DataLayouts"]
                  .SetValue(commentsForDataLayout, value);
            }
        }

        public void RefreshDataLayoutControls()
        {
            Dictionary<string, string> dicColComments = new Dictionary<string, string>();
            var ds = DataLayouts.DataSource;

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

            // 编辑字段标题
            for (int i = 0; i < commentsForDataLayout.Container.Components.Count; i++)
            {
                if (commentsForDataLayout.Container.Components[i].GetType().FullName == "DevExpress.XtraLayout.LayoutControlItem")
                {
                    DevExpress.XtraLayout.LayoutControlItem item = ((DevExpress.XtraLayout.LayoutControlItem)commentsForDataLayout.Container.Components[i]);

                    if (item.Control.DataBindings.Count == 1)
                    {
                        string sComments = string.Empty;

                        dicColComments.TryGetValue(item.Control.DataBindings[0].BindingMemberInfo.BindingField, out sComments);
                        if (!string.IsNullOrEmpty(sComments))
                        {
                            item.Text = sComments + ":";
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
            DesignerActionHeaderItem hInfo = new DesignerActionHeaderItem("请选择DataLayout控件", "Info");
            DesignerActionHeaderItem hAction = new DesignerActionHeaderItem("绑定中文标题", "Action");
            DesignerActionHeaderItem hDescription = new DesignerActionHeaderItem("将实体中的Comments标签值,绑定到DataLayout字段标题中.");

            items.Add(hInfo);
            items.Add(hAction);
            items.Add(hDescription);

            items.Add(new DesignerActionPropertyItem("DataLayouts", "DataLayout控件", "Info", ""));
            items.Add(new DesignerActionMethodItem(this, "RefreshDataLayoutControls", "绑定Comments标签", "Action", true));

            return items;
        }

    }
}
