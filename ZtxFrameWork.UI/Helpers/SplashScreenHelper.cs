using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.UI.Helpers
{
    public class SplashScreenHelper
    {
        private static SplashScreenHelper instance = null;

        public static SplashScreenHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SplashScreenHelper();
                }
                return instance;
            }
        }

        private SplashScreenHelper()
        {
        }

        //  private WaitIndicator indicator = new WaitIndicator();

        public void ShowSplashScreen()
        {
            if (!DXSplashScreen.IsActive)
            {
                DXSplashScreen.Show<SplashWindow>();
            }
        }

        public void HideSplashScreen()
        {
            if (DXSplashScreen.IsActive)
            {
                DXSplashScreen.Close();
            }
        }

    }
}
