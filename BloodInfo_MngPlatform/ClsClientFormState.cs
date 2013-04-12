using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodInfo_MngPlatform
{
    public class ClientFormStateClass
    {
        string path = System.Windows.Forms.Application.StartupPath + "\\FrmState.xml";
        
        /// <summary>
        /// 保存窗体中所有的dockpanel的状态
        /// </summary>
        /// <param name="dockManage"></param>
        public void SavePanelsState(DevExpress.XtraBars.Docking.DockManager dockManage)
        {
            dockManage.SaveLayoutToXml(path);
        }

        /// <summary>
        /// 恢复窗体中panels的状态
        /// </summary>
        /// <param name="dockManage"></param>
        public void RestorePanelsState(DevExpress.XtraBars.Docking.DockManager dockManage)
        {
            if (System.IO.File.Exists(path))
                dockManage.RestoreLayoutFromXml(path);
        }
    }
}
