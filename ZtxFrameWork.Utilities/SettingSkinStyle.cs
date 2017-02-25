using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZtxFrameWork.Utilities
{
    public struct SkinSet
    {
        public string SkinName { get; set; }
      //  public string SkinCategory { get; set; }
    }
  public  class SettingSkinStyle
    {
      public string ThemeName { get; set; }

      

    
     /// <summary>
     /// 获取用户设置的样式
     /// </summary>
     /// <param name="appPath"></param>
     /// <returns></returns>
     public static SkinSet GetSetting()
     {
         SkinSet skinSet = new SkinSet() { SkinName = "" };

         try
         {
             string path = System.Environment.CurrentDirectory + @"\Setting\";
             string str2 = path + "SetSkinStyle.txt";
             if (!Directory.Exists(path))
             {
                 Directory.CreateDirectory(path);
             }
             if (!File.Exists(str2))
             {
                 File.Create(str2).Close();
             }
             StreamReader reader = new StreamReader(str2);
             while (!reader.EndOfStream)
             {
                 string str3 = reader.ReadLine();
                 if (str3 != "")
                 {
                     string[] strArray = str3.Split(new char[] { '=' });
                     if (strArray.Length == 2)
                     {
                         string str4 = strArray[0].ToUpper();
                         if ((str4 != null) && (str4 == "SKINSTYLE"))
                         {
                             if (strArray[1] == "")
                             {
                                 skinSet.SkinName = "MetropolisLight";
                             }
                             else
                             {
                                 skinSet.SkinName = strArray[1];
                             }
                         }
                         //else if ((str4 != null) && (str4 == "SKINCATEGORY"))
                         //{
                         //    if (strArray[1] == "")
                         //    {
                         //        skinSet.SkinCategory = "Office 2007";
                         //    }
                         //    else
                         //    {
                         //        skinSet.SkinCategory = strArray[1];
                         //    }
                         //}


                     }
                 }
             }
             reader.Close();
         }
         catch
         {
         }
         return skinSet;
     }
     /// <summary>
     /// 保存用户设置的样式
     /// </summary>
     /// <param name="setting"></param>
     /// <param name="appPath"></param>
     public static void SaveSetting(SkinSet skinSet)
     {
         try
         {
             string path = System.Environment.CurrentDirectory + @"\Setting\";
             string str2 = path + "SetSkinStyle.txt";
             if (!Directory.Exists(path))
             {
                 Directory.CreateDirectory(path);
             }
             if (!File.Exists(str2))
             {
                 File.Create(str2).Close();
             }
             StreamWriter writer = new StreamWriter(str2, false);
             writer.WriteLine("SkinStyle=" + skinSet.SkinName);
            // writer.WriteLine("SkinCategory=" + skinSet.SkinCategory);
             writer.Close();
         }
         catch
         {
         }
     }
    }
}
