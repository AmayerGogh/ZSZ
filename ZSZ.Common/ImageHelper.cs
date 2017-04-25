using CaptchaGen;
using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using System.IO;

namespace ZSZ.Common
{
    public class ImageHelper
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="fileName"></param>
        public static void BuildThumbnail(string sourceFileName, string targetFileName)
        {
            ImageProcessingJob jobThumb = new ImageProcessingJob();
            jobThumb.Filters.Add(new FixedResizeConstraint(20, 20));//缩略图尺寸
            jobThumb.SaveProcessedImageToFileSystem(sourceFileName, targetFileName,new BmpFormatEncoderParams()); //第一个参数可以是string文件名，也可以是流
        }

        /// <summary>
        /// 生成水印
        /// </summary>
        /// <param name="waterFileName"></param>
        /// <param name="sourceFileName"></param>
        /// <param name="targetFileName"></param>
        public static void BuiildWaterMark(string waterFileName, string sourceFileName, string targetFileName)
        {
            ImageProcessingJob jobThumb = new ImageProcessingJob();

            ImageWatermark imgWater = new ImageWatermark(waterFileName);
            imgWater.ContentAlignment = System.Drawing.ContentAlignment.BottomRight; //水印位置
            imgWater.Alpha = 50; //透明度，需要水印图片是背景透明的png图片

            jobThumb.Filters.Add(imgWater); //添加水印
            jobThumb.Filters.Add(new FixedResizeConstraint(200, 200));//缩略图尺寸

            jobThumb.SaveProcessedImageToFileSystem(sourceFileName, targetFileName, new BmpFormatEncoderParams()); //第一个参数可以是string文件名，也可以是流
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="fileName"></param>
        public static void BuildCaptchaPicture(string code, string fileName)
        {
            using (MemoryStream ms = ImageFactory.GenerateImage(code, 60, 100, 20, 6))
            using (FileStream fs = File.OpenWrite(fileName))
            {
                ms.CopyTo(fs);
            }
        }

    }
}
