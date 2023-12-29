using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Helpers
{
    #pragma warning disable CA1416
    public static class ImageWorker
    {
        public static Task<Bitmap> FromBase64StringToImageAsync(this string base64String)
        {
            return Task.Run(() =>
            {
                byte[] byteBuffer = Convert.FromBase64String(base64String);
                try
                {
                    MemoryStream memoryStream = new MemoryStream(byteBuffer);
                    memoryStream.Position = 0;
                    Image image = Image.FromStream(memoryStream);
                    memoryStream.Close();
                    byteBuffer = null;
                    return new Bitmap(image);
                }
                catch { return null; }
            });
        }

        public static Task<string> SaveImageAsync(string imageBase64, string folderPath, string filename, string fileExtension)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (imageBase64.Contains(',')) imageBase64 = imageBase64.Split(',')[1];
                    var image = await imageBase64.FromBase64StringToImageAsync();
                    var format = GetImageFormat(fileExtension);

                    var compressedImage = await CompressImage(image, 1200, 1200, (format == ImageFormat.Png));
                    compressedImage.Save(Path.Combine(folderPath, filename), format);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving image! " + ex.Message);
                }
                return filename;
            });
        }

        private static ImageFormat GetImageFormat(string fileExtension)
        {
            switch (fileExtension)
            {
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "png":
                    return ImageFormat.Png;
                case "jpg":
                    return ImageFormat.Jpeg;
                case "ico":
                    return ImageFormat.Icon;
                case "webp":
                    return ImageFormat.Webp;
                case "bmp":
                    return ImageFormat.Bmp;
                case "gif":
                    return ImageFormat.Gif;
                case "tiff":
                    return ImageFormat.Tiff;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        public static Task<Bitmap> CompressImage(Bitmap originalPic, int maxWidth, int maxHeight, bool transperent = false)
        {
            return Task.Run(() =>
            {
                try
                {
                    int width = originalPic.Width;
                    int height = originalPic.Height;
                    int widthDiff = width - maxWidth;
                    int heightDiff = height - maxHeight;
                    bool doWidthResize = (maxWidth > 0 && width > maxWidth && widthDiff > heightDiff);
                    bool doHeightResize = (maxHeight > 0 && height > maxHeight && heightDiff > widthDiff);

                    if (doWidthResize || doHeightResize || (width.Equals(height) && widthDiff.Equals(heightDiff)))
                    {
                        int iStart;
                        Decimal divider;
                        if (doWidthResize)
                        {
                            iStart = width;
                            divider = Math.Abs((Decimal)iStart / maxWidth);
                            width = maxWidth;
                            height = (int)Math.Round((height / divider));
                        }
                        else
                        {
                            iStart = height;
                            divider = Math.Abs((Decimal)iStart / maxHeight);
                            height = maxHeight;
                            width = (int)Math.Round(width / divider);
                        }
                    }
                    using (Bitmap outBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
                    {
                        using (Graphics oGraphics = Graphics.FromImage(outBmp))
                        {
                            oGraphics.Clear(Color.White);
                            oGraphics.DrawImage(originalPic, 0, 0, width, height);
                            if (transperent)
                            {
                                outBmp.MakeTransparent();
                            }

                            return new Bitmap(outBmp);
                        }
                    }
                }
                catch
                {
                    return null;
                }
            });
        }
    }
    #pragma warning restore CA1416
}
