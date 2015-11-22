using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Net;

namespace Utility.Media
{
    public static class Images
    {
        /// <summary>
        /// Returns an bitmap image with rounded corners.
        /// </summary>
        /// <param name="StartImage">Image to round corners</param>
        /// <param name="CornerRadius">Corner radius</param>
        /// <returns></returns>
        public static Bitmap RoundCorners(Image StartImage, int CornerRadius)
        {
            Bitmap roundedImage;

            try
            {
                CornerRadius *= 2;

                roundedImage = new Bitmap(StartImage.Width, StartImage.Height);

                Graphics graphicsRoundedImage = Graphics.FromImage(roundedImage);
                graphicsRoundedImage.Clear(System.Drawing.Color.White);
                graphicsRoundedImage.SmoothingMode = SmoothingMode.AntiAlias;

                Brush brush = new TextureBrush(StartImage);

                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
                gp.AddArc(0 + roundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
                gp.AddArc(0 + roundedImage.Width - CornerRadius, 0 + roundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gp.AddArc(0, 0 + roundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);

                graphicsRoundedImage.FillPath(brush, gp);
            }
            catch { roundedImage = null; }
            return roundedImage;
        }

        /// <summary>
        /// Returns an image pass by parameter in which insert a text
        /// </summary>
        /// <param name="fileName">The file name and path of the image</param>
        /// <param name="text">Text to inter into the image</param>
        /// <param name="font">The font to write a text</param>
        /// <param name="layoutWidthText">Width of the layout of the text</param>
        /// <param name="layoutHeightText">Height of the layout of the text</param>        
        /// <returns></returns>
        public static Bitmap InsertTextOnImage(string fileName, string text, Font font, float layoutWidthText = -1, float layoutHeightText = -1)
        {
            Bitmap imageBitmap = new Bitmap(fileName);
            return Images.InsertTextOnImage(imageBitmap, text, font, layoutWidthText, layoutHeightText);
        }

        /// <summary>
        /// Returns an image pass by parameter in which insert a text
        /// </summary>
        /// <param name="bitmapImage">The bitmap of the image</param>
        /// <param name="text">Text to inter into the image</param>
        /// <param name="font">The font to write a text</param>
        /// <param name="layoutWidthText">Width of the layout of the text</param>
        /// <param name="layoutHeightText">Height of the layout of the text</param>    
        /// <returns></returns>
        public static Bitmap InsertTextOnImage(Bitmap bitmapImage, string text, Font font, float layoutWidthText = -1, float layoutHeightText = -1)
        {
            try
            {
                Graphics graphicsImagen = Graphics.FromImage(bitmapImage);
                graphicsImagen.TextRenderingHint = TextRenderingHint.AntiAlias;

                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Center;
                strFormat.LineAlignment = StringAlignment.Center;

                if (layoutWidthText == -1) layoutWidthText = bitmapImage.Width;
                if (layoutHeightText == -1) layoutHeightText = bitmapImage.Height;

                graphicsImagen.DrawString(text, font, Brushes.White, new RectangleF(0, 0, layoutWidthText, layoutHeightText), strFormat);
            }
            catch { bitmapImage = null; }

            return bitmapImage;
        }


        /// <summary>
        /// Download an image by the URL parameter
        /// </summary>
        /// <param name="url">URL where is the image</param>
        /// <returns></returns>
        public static Stream DownloadImage(string url)
        {
            Stream stream = null;
            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                stream = response.GetResponseStream();
            }
            catch { stream = null; }
            return stream;
        }

        /// <summary>
        /// Creates an image with a width, height and color defined by parameters
        /// </summary>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="red">RGB Red</param>
        /// <param name="green">RBG green</param>
        /// <param name="blue">RGB blue</param>
        /// <param name="minAncho">Optional: Set a minimum width to the image</param>
        /// <returns></returns>
        public static Bitmap CreateImage(int width, int height, int red, int green, int blue, int? minAncho = null)
        {
            if ((red < 0 || red > 256) || (green < 0 || green > 256) || (blue < 0 || blue > 256))
                return null;

            if (minAncho != null)
            {
                if (width < (int)minAncho)
                    width = (int)minAncho;
            }

            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics gfx = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(red, green, blue)))
            {
                gfx.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            }

            return bitmap;
        }

        /// <summary>
        /// Get an image from bytes array
        /// </summary>
        /// <param name="source">Bytes of the image</param>
        /// <returns></returns>
        public static Image ByteArrayToImage(byte[] source)
        {
            MemoryStream ms = new MemoryStream(source);
            Image image = Image.FromStream(ms);
            return image;
        }

        /// <summary>
        /// Get a bytes array from image
        /// </summary>
        /// <param name="image">Image</param>
        /// <param name="imageFormat">Optional: Formtat of the image. Jpeg if not specified</param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(Image image, ImageFormat imageFormat = null)
        {
            if (imageFormat == null) imageFormat = ImageFormat.Jpeg;

            MemoryStream ms = new MemoryStream();
            image.Save(ms, imageFormat);
            return ms.ToArray();
        }
    }
}
