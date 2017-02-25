using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZtxFrameWork.Utilities
{
    /// <summary>
    /// 读取，查询和更新xml
    /// 创建时间：2015-03-05 14:11:37
    /// 作者：杨洋
    /// </summary>
    public class ConfigSetting
    {

        public static XDocument xmlDoc = null;

        public ConfigSetting() { }

        /// <summary>  
        /// 返回XMl文件指定元素的指定属性值  
        /// </summary>  
        /// <param name="xmlElement">指定元素</param>  
        /// <param name="xmlAttribute">指定属性</param>  
        /// <param name="xmlpath">读取的xml文件名字</param>  
        /// <param name="reloadXml">是否重新加载XML</param>  
        /// <returns></returns>  
        public static string GetXmlValue(string xmlElement, string xmlAttribute = "value", string xmlpath = "AppSet.xml", bool reloadXml = false)
        {
            if (xmlDoc == null || reloadXml)
            {
                xmlDoc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Config\\" + xmlpath);
            }
            var results = from c in xmlDoc.Descendants(xmlElement)
                          select c;
            string s = "";
            foreach (var result in results)
            {
                s = result.Attribute(xmlAttribute).Value.ToString();
            }
            return s;
        }

        /// <summary>  
        /// 设置XMl文件指定元素的指定属性的值  
        /// </summary>  
        /// <param name="xmlElement">指定元素</param>  
        /// <param name="xmlAttribute">指定属性</param>  
        /// <param name="xmlValue">指定值</param>  
        public static void SetXmlValue(string xmlElement, string xmlAttribute, string xmlValue, string xmlpath = "AppSet.xml", string xmlRootName = "Application", bool reloadXml = false)
        {
            if (xmlDoc == null || reloadXml)
            {
                xmlDoc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Config\\" + xmlpath);
            }
            xmlDoc.Element(xmlRootName).Element(xmlElement).Attribute(xmlAttribute).SetValue(xmlValue);
            string filepath = Directory.GetCurrentDirectory() + "\\Config\\" + xmlpath;

            xmlDoc.Save(filepath);

        }
    }
}
