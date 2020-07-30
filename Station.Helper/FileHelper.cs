using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace NetCore
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        #region 文件转换

        /// <summary>
        /// 获取拍照时间
        /// </summary>
        /// <param name="FileStream"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(Stream FileStream)
        {
            Bitmap bmp = new Bitmap(FileStream, false);
            System.Drawing.Image image = System.Drawing.Image.FromStream(FileStream); //得到原图
            return image;
        }

        public static System.Drawing.Image PhotoZH(Stream FileStream, int width, int height)
        {
            Bitmap bmp = new Bitmap(FileStream, false);
            System.Drawing.Image image = System.Drawing.Image.FromStream(FileStream); //得到原图
            //创建指定大小的图
            System.Drawing.Image newImage = image.GetThumbnailImage(bmp.Width, bmp.Height, null, new IntPtr());
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(newImage, 0, 0, width, height); //将原图画到指定的图上
            g.Dispose();
            FileStream.Close();
            return image;
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="bytes">对象</param>
        /// <param name="FilePath">附件路径</param>
        /// <param name="Model">模型 1://指定高宽缩放（可能变形）2://指定宽，高按比例3://指定高，宽按比例4://指定高宽裁减（不变形）</param>
        /// <param name="Width">宽度</param>
        /// <param name="Height">高度</param>
        /// <param name="WaterMarkUrl">水印路径</param>
        public static void PhotoZH(byte[] bytes, string FilePath, int Model, int Width, int Height, string WaterMarkUrl)
        {
            Stream stream = BytesToStream(bytes);
            PhotoZH(stream, FilePath, Model, Width, Height, WaterMarkUrl);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="Stream">流对象</param>
        /// <param name="FilePath">附件路径</param>
        /// <param name="Model">模型 1://指定高宽缩放（可能变形）2://指定宽，高按比例3://指定高，宽按比例4://指定高宽裁减（不变形）</param>
        /// <param name="Width">宽度</param>
        /// <param name="Height">高度</param>
        /// <param name="WaterMarkUrl">水印路径</param>
        public static void PhotoZH(Stream stream, string FilePath, int Model, int Width, int Height, string WaterMarkUrl)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(stream);
            int towidth = Width;
            int toheight = Height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (Model)
            {
                case 1://指定高宽缩放（可能变形）
                    break;
                case 2://指定宽，高按比例
                    toheight = originalImage.Height * towidth / originalImage.Width;
                    break;
                case 3://指定高，宽按比例 
                    towidth = originalImage.Width * toheight / originalImage.Height;
                    break;
                case 4://指定高宽裁减（不变形）                 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * toheight / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                case 5://不改变大小
                    towidth = originalImage.Width;
                    toheight = originalImage.Height;
                    break;
                default:

                    break;
            }

            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);
            try
            {
                if (WaterMarkUrl != "")
                {
                    Image img = InsertWaterMark(bitmap, WaterMarkUrl);
                    img.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                    //以jpg格式保存缩略图 
                    bitmap.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 添加水印图片(为图片添加公司logo)
        /// </summary>
        /// <param name="image">需要添加的水印的图片</param>
        /// <param name="url">水印图片</param>
        /// <returns>返回添加水印后的新图片</returns>
        public static Image InsertWaterMark(Image image, string url)
        {
            //Image image = Image.FromFile(path);
            Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            //公司logo
            Image watermark = new Bitmap(url);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            float[][] colorMatrixElements = {   
             new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},   
             new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},   
             new float[] {0.0f, 0.0f, 0.0f, 1.0f, 0.0f},   
             new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}   
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            int xpos = 0;
            int ypos = 0;

            xpos = ((image.Width - watermark.Width) - 10);
            ypos = image.Height - watermark.Height - 10;

            g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

            watermark.Dispose();
            imageAttributes.Dispose();

            //b.Save(path);
            //b.Dispose();
            image.Dispose();

            return b;
        }

        #endregion

        #region 文件读取和写入
        /// <summary>
        /// 写文件的相关方法
        /// </summary>
        /// <param name="FileName">文件路径包含文件名称</param>
        /// <param name="ff">二进制</param>
        /// <param name="IsBestrow">是否覆盖已有文件</param>
        public static void WriteFile(string FileName, byte[] ff, bool IsBestrow)
        {
            try
            {
                if (FileName == "")
                {
                    throw new Exception("配置物理路径为空无法进行保存");
                }
                FileInfo finfo = new FileInfo(FileName);
                if (!finfo.Directory.Exists)
                {
                    finfo.Directory.Create();
                }
                if (finfo.Exists && !IsBestrow)
                {
                    throw new Exception(FileName + "文件以存在，无法覆盖，如需上传请先将其删除");
                }
                else
                {
                    FileMode Model = FileMode.CreateNew;
                    if (IsBestrow)
                        Model = FileMode.Create;

                    FileStream fs = new FileStream(FileName, Model, FileAccess.ReadWrite, FileShare.None, ff.Length, false);
                    fs.Write(ff, 0, ff.Length);
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 写txt文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="FileContext"></param>
        public static bool WriteLine(string filePath, string FileContext,string charset="utf-8")
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Directory.Exists)
                file.Directory.Create();
            StreamWriter writer = new StreamWriter(filePath,false, Encoding.GetEncoding(charset));
          
            writer.Write(FileContext);
            writer.Close();
            return true;
        }

        /// <summary>
        /// 读取txt文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string ReadLine(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                return "";
            }
            StreamReader reader = new StreamReader(filePath);
            string FileContent = reader.ReadToEnd();
            reader.Close();
            return FileContent;
        }

        /// <summary>
        /// 写文件的相关方法
        /// </summary>
        /// <param name="path">文件路径包含文件名称</param>
        /// <param name="ff">二进制</param>
        /// <param name="ff">是否覆盖已有文件</param>
        public static void WriteFile(string FileName, byte[] ff, bool IsBestrow, string Id)
        {
            try
            {
                if (FileName == "")
                {
                    throw new Exception("配置物理路径为空无法进行保存");
                }
                FileInfo finfo = new FileInfo(FileName);
                if (!finfo.Directory.Exists)
                {
                    finfo.Directory.Create();
                }
                if (finfo.Exists)
                {
                    throw new Exception(FileName + "文件以存在，无法覆盖，如需上传请先将其删除");
                }
                else
                {
                    PhotoZH(ff, FileName, 4, 600, 700, "");//原图
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取文件的相关方法
        /// </summary>
        /// <param name="path">文件路径包含文件名称</param>
        /// <param name="ff">二进制</param>
        public static bool ReadFile(string FileName, out byte[] buffer)
        {
            try
            {
                buffer = null;
                if (FileName == "")
                {
                    throw new Exception("配置物理路径为空无法进行保存");
                }
                FileInfo finfo = new FileInfo(FileName);
                if (!finfo.Directory.Exists)
                {
                    finfo.Directory.Create();
                }
                if (finfo.Exists)
                {

                    FileStream stream = finfo.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                    stream.Seek(0, SeekOrigin.Begin);
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    stream.Close();
                    stream.Dispose();
                    return true;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static bool Delete(string filepath)
        {
            if (File.Exists(filepath))
                File.Delete(filepath);
            return true;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static bool DirectoryDelete(string filepath)
        {
            if (Directory.Exists(filepath))
                Directory.Delete(filepath,true);
            return true;
        }

        #endregion

        #region 转换
        /// <summary>
        /// 把字节转换成字符串
        /// </summary>
        /// <param name="EncodingType">字符串编码集</param>
        /// <param name="buffer">字节</param>
        /// <returns>string</returns>
        public static string ByteToString(string EncodingType, byte[] buffer)
        {
            return System.Text.Encoding.GetEncoding(EncodingType).GetString(buffer);
        }

        /// <summary>
        /// 把字节转换成字符串
        /// </summary>
        /// <param name="EncodingType">字符串编码集</param>
        /// <param name="buffer">字节</param>
        /// <returns>string</returns>
        public static byte[] StringToByte(string EncodingType, string Content)
        {
            return System.Text.Encoding.GetEncoding(EncodingType).GetBytes(Content);
        }

        /// <summary>
        /// 把文件转换成字符串
        /// </summary>
        /// <param name="EncodingType">字符串编码集</param>
        /// <param name="buffer">字节</param>
        /// <returns>string</returns>
        public static string ByteToString(string FileName, string EncodingType)
        {
            byte[] buffer = null;
            if (ReadFile(FileName, out buffer))
                return System.Text.Encoding.GetEncoding(EncodingType).GetString(buffer);
            return "";
        }

        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #endregion 

        #region 文件转移

        /// <summary>
        /// 文件复制
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="movepath">目标文件</param>
        /// <param name="movepath">允许改写同名文件</param>
        /// <returns></returns>
        public static bool CopyTo(string SourceFileName, string destFileName, bool overwrite)
        {
            if (!File.Exists(SourceFileName))
                throw new Exception("源文件不存在，无法复制");
            FileInfo newfile = new FileInfo(destFileName);
            if (!newfile.Exists)
            {
                if (!newfile.Directory.Exists)
                {
                    newfile.Directory.Create();
                }
            }
            File.Copy(SourceFileName, destFileName, overwrite);
            return true;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="SourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static bool MoveTo(string SourceFileName, string destFileName)
        {
            if (!File.Exists(SourceFileName))
                throw new Exception("源文件不存在，无法移动");
            FileInfo newfile = new FileInfo(destFileName);
            if (!newfile.Exists)
            {
                if (!newfile.Directory.Exists)
                {
                    newfile.Directory.Create();
                }
            }
            File.Move(SourceFileName, destFileName);
            return true;
        }

        /// <summary>
        /// 剪切文件夹
        /// </summary>
        /// <param name="path">临时文件夹</param>
        /// <param name="NewPath">目标文件夹</param>
        /// <returns></returns>
        public static bool CutDirectory(string path, string NewPath)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
                return false;
            DirectoryInfo NewDir = new DirectoryInfo(NewPath);
            if (!NewDir.Exists)
            {
                NewDir.Create();
            }
            try
            {
                foreach (FileInfo info in dir.GetFiles())
                {
                   info.MoveTo(NewPath + info.Name);
                }
                dir.Delete(true);
            }
            catch (Exception)
            {
                //LogManager log = new LogManager();
                //log.LogInfo("error", "Directory Delete", ex.Message);
            }
            return true;
        }
        #endregion

        #region 插入水印
        /// <summary>
        /// 插入水印
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static bool InsertWaterMark(Stream stream, string path, string Watermark, ImageFormat format)
        {
            if (!File.Exists(Watermark))
            {
                return false;
            }
            Image image = Image.FromStream(stream);
            return InsertWaterMark(image, path, Watermark, format);
        }

        /// <summary>
        /// 插入水印
        /// </summary>
        /// <param name="image">image对象</param>
        /// <param name="path">路径</param>
        /// <param name="Watermark">水印</param>
        /// <param name="format">格式</param>
        /// <returns></returns>
        public static bool InsertWaterMark(Image image, string path, string Watermark, ImageFormat format)
        {
            if (!File.Exists(Watermark))
            {
                return false;
            }

            //公司logo
            Image watermark = new Bitmap(Watermark);
            Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            ImageAttributes imageAttributes = new ImageAttributes();
            try
            {

                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.DrawImage(image, 0, 0, image.Width, image.Height);

                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };
                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                float[][] colorMatrixElements = {   
             new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},   
             new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},   
             new float[] {0.0f, 0.0f, 0.0f, 0.7f, 0.0f},   
             new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}   
            };
                ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int xpos = 0;
                int ypos = 0;

                xpos = ((image.Width - watermark.Width) - 10);
                ypos = image.Height - watermark.Height - 10;

                g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                watermark.Dispose();
                imageAttributes.Dispose();
                image.Dispose();
            }
            b.Save(path, format);
            return true;
        }

        /// <summary>
        /// 获取图片的格式
        /// </summary>
        /// <param name="fileExtension">文件后缀名</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".gif":
                    return ImageFormat.Gif;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".png":
                    return ImageFormat.Png;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".wmf":
                    return ImageFormat.Wmf;
                case ".ico":
                    return ImageFormat.Icon;
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".exif":
                    return ImageFormat.Exif;
                case ".emf":
                    return ImageFormat.Emf;
                default:
                    throw new Exception("文件类型不存在，无法保存");
            }
        }

        #endregion

        #region  文件夹
        /// <summary>
        /// 获取文件夹中的所有文件
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static FileInfo[] GetFiles(string dirPath)
        {
            return GetFiles(dirPath, "");
        }

        /// <summary>
        /// 获取文件夹中的所有文件
        /// </summary>
        /// <param name="dirPath">根目录</param>
        /// <param name="searchPattern">关键字</param>
        /// <returns></returns>
        public static FileInfo[] GetFiles(string dirPath,string searchPattern)
        {
            DirectoryInfo directory = new DirectoryInfo(dirPath);
            if (!directory.Exists)
            {
                return null;
            }
            if (!string.IsNullOrWhiteSpace(searchPattern))
            {
                return directory.GetFiles(searchPattern);
            }
            return directory.GetFiles();


        }
        #endregion


    }
}
