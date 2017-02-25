using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ZtxFrameWork.Utilities
{

    /// <summary> 
    /// 设置本机时间 
    /// </summary> 
    public class LocalTimeSync
    {
        [DllImport("Kernel32.dll")]
        public static extern bool SetSystemTime(ref SystemTime sysTime);

        [DllImport("Kernel32.dll")]
        public static extern void GetSystemTime(ref SystemTime sysTime);

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }
        /// <summary> 
        /// 设置本机时间 
        /// </summary> 
        public static void SyncTime(DateTime currentTime)
        {
            SystemTime sysTime = new SystemTime();
            sysTime.wYear = Convert.ToUInt16(currentTime.Year);
            sysTime.wMonth = Convert.ToUInt16(currentTime.Month);
            sysTime.wDay = Convert.ToUInt16(currentTime.Day);
            sysTime.wDayOfWeek = Convert.ToUInt16(currentTime.DayOfWeek);
            sysTime.wMinute = Convert.ToUInt16(currentTime.Minute);
            sysTime.wSecond = Convert.ToUInt16(currentTime.Second);
            sysTime.wMiliseconds = Convert.ToUInt16(currentTime.Millisecond);

            //处理北京时间 
            int nBeijingHour = currentTime.Hour - 8;
            if (nBeijingHour <= 0)
            {
                nBeijingHour = 24;
                sysTime.wDay = Convert.ToUInt16(currentTime.Day - 1);
                //sysTime.wDayOfWeek = Convert.ToUInt16(current.DayOfWeek - 1); 
            }
            else
            {
                sysTime.wDay = Convert.ToUInt16(currentTime.Day);
                sysTime.wDayOfWeek = Convert.ToUInt16(currentTime.DayOfWeek);
            }
            sysTime.wHour = Convert.ToUInt16(nBeijingHour);

            SetSystemTime(ref sysTime);//设置本机时间 
        }
    }


    //设置时间需要以管理员身份运行程序


    //    windows 7和vista提高的系统的安全性，同时需要明确指定“以管理员身份运行”才可赋予被运行软件比较高级的权限，比如访问注册表等。否则，当以普通身份运行的程序需要访问较高级的系统资源时，将会抛出异常。

    //如何让程序在启动时，自动要求“管理员”权限了，我们只需要修改app.manifest文件中的配置项即可。

    //app.manifest文件默认是不存在的，我们可以通过以下操作来自动添加该文件。

    //（1）进入项目属性页。

    //（2）选择“安全性”栏目。

    //（3）将“启用ClickOnce安全设置”勾选上。

    //  现在，在Properties目录下就自动生成了app.manifest文件，打开该文件，将trustInfo/security/requestedPrivileges节点的requestedExecutionLevel的level的值修改为requireAdministrator即可。如下所示：

    //      <requestedPrivileges xmlns="urn:schemas-microsoft-com:asm.v3">
    //         <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
    //      </requestedPrivileges>



    //若编译报"ClickOnce 不支持请求执行级别requireAdministrator"错误的话，请去掉requestedPrivileges的xmlns="urn:schemas-microsoft-com:asm.v3"属性即可编译通过！

    //记住，如果不需要ClickOnce，可以回到项目属性页将“启用ClickOnce安全设置”不勾选。 　　

    //接下来，重新编译你的程序就OK了。



}
