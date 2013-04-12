using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using System.Configuration;

namespace BloodInfo_MngPlatform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();


            UserLookAndFeel.Default.SetSkinStyle(ConfigurationManager.AppSettings["Skin"]);
            Application.Run(new FrmUserLogin());
        }
    }
}