using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ZtxFrameWork.Utilities
{
  public   static class ImageConverter
    {
        /// <summary>
        /// byte[]转换成Image
        /// </summary>
        /// <param name="byteArrayIn">二进制图片流</param>
        /// <returns>Image</returns>
        public static System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Flush();
             //   this.iamge.Source = ChangeBitmapToImageSource((Bitmap)returnImage);
                return returnImage;
            }

        }
        /// <summary>
        /// 将Base64字符串转换为图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       public static Bitmap ToImage(string strSource)
        {
            // this.iamge.Source = null;
            string base64 = strSource;//.Substring(0,strSource.Length-2);
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            BinaryFormatter binFormatter = new BinaryFormatter();
            System.Drawing.Bitmap img = (System.Drawing.Bitmap)binFormatter.Deserialize(memStream);
            return img;
            //  this.iamge.Source = ChangeBitmapToImageSource(img);
        }
        /// <summary>
        /// 将图片Image转换成Byte[]
        /// </summary>
        /// <param name="Image">image对象</param>
        /// <param name="imageFormat">后缀名</param>
        /// <returns></returns>
       public static byte[] ImageToBytes(Image Image, System.Drawing.Imaging.ImageFormat imageFormat  )
        {

            if (Image == null) { return null; }

            byte[] data = null;

            using (MemoryStream ms = new MemoryStream())
            {

                using (Bitmap Bitmap = new Bitmap(Image))
                {

                    Bitmap.Save(ms, imageFormat);

                    ms.Position = 0;

                    data = new byte[ms.Length];

                    ms.Read(data, 0, Convert.ToInt32(ms.Length));

                    ms.Flush();

                }

            }

            return data;

        }

        /// <summary>
        /// 将图片数据转换为Base64字符串
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static string ToBase64(String filePath)
        {
            //   Image img = this.pictureBox.Image;
            //    System.Drawing.Image image = System.Drawing.Image.FromFile(@"E:\酷图\146814243.jpg");
           // String[] fileStrings = System.IO.Directory.GetFiles(@"\\192.168.0.168\mapic", "*.png");
          //  int aa = new Random().Next(1, 5000);
          //  string filePath = fileStrings[aa - 1];
            //   Image img = this.pictureBox.Image;
            System.Drawing.Image image = System.Drawing.Image.FromFile(filePath);
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            binFormatter.Serialize(memStream, image);
            byte[] bytes = memStream.GetBuffer();
            string base64 = Convert.ToBase64String(bytes);
            var dfd = bytes.Length;
            var fff = base64.Length;
            return base64;
            //   return filePath;
        }
    }
}
