using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System;
public static class ImageExtensionMethods
{

    static private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
    }

    public static void SaveAsJpeg(this Image Img, string FileName, Int64 Quality)
    {
        ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
        Encoder QualityEncoder = Encoder.Quality;

        using (EncoderParameters EP = new EncoderParameters(1))
        {
            using (EncoderParameter QualityEncoderParameter = new EncoderParameter(QualityEncoder, Quality))
            {
                EP.Param[0] = QualityEncoderParameter;
                Img.Save(FileName, jgpEncoder, EP);
            }
        }
    }

    public static void SaveAsGif(this Image Img, string FileName, Int64 Quality)
    {
        ImageCodecInfo gifEncoder = GetEncoder(ImageFormat.Gif);
        Encoder QualityEncoder = Encoder.Quality;

        using (EncoderParameters EP = new EncoderParameters(1))
        {
            using (EncoderParameter QualityEncoderParameter = new EncoderParameter(QualityEncoder, Quality))
            {
                EP.Param[0] = QualityEncoderParameter;
                Img.Save(FileName, gifEncoder, EP);
            }
        }
    }

    public static Image Resize(this Image Img, int Width, int Height, InterpolationMode InterpolationMode)
    {
        
        Image CropedImage = new Bitmap(Width, Height);
        using (Graphics G = Graphics.FromImage(CropedImage))
        {
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.InterpolationMode = InterpolationMode;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.DrawImage(Img, 0, 0, Width, Height);
        }

        return CropedImage;
    }

    public static Image Resize(this Image Img, int Width, int Height)
    {
        return Img.Resize(Width, Height, InterpolationMode.HighQualityBicubic);
    }

    private static Rectangle EnsureAspectRatio(this Image Image, int Width, int Height)
    {
        float AspectRatio = Width / (float)Height;
        float CalculatedWidth = Image.Width, CalculatedHeight = Image.Height;

        if (Image.Width >= Image.Height)
        {
            if (Width > Height)
            {
                CalculatedHeight = Image.Width / AspectRatio;
                if (CalculatedHeight > Image.Height)
                {
                    CalculatedHeight = Image.Height;
                    CalculatedWidth = CalculatedHeight * AspectRatio;
                }
            }
            else
            {
                CalculatedWidth = Image.Height * AspectRatio;
                if (CalculatedWidth > Image.Width)
                {
                    CalculatedWidth = Image.Width;
                    CalculatedHeight = CalculatedWidth / AspectRatio;
                }
            }
        }
        else
        {
            if (Width < Height)
            {
                CalculatedHeight = Image.Width / AspectRatio;
                if (CalculatedHeight > Image.Height)
                {
                    CalculatedHeight = Image.Height;
                    CalculatedWidth = CalculatedHeight * AspectRatio;
                }
            }
            else
            {
                CalculatedWidth = Image.Height * AspectRatio;
                if (CalculatedWidth > Image.Width)
                {
                    CalculatedWidth = Image.Width;
                    CalculatedHeight = CalculatedWidth / AspectRatio;
                }
            }
        }
        return Rectangle.Ceiling(new RectangleF((Image.Width - CalculatedWidth) / 2, 0, CalculatedWidth, CalculatedHeight));
    }

    public static Image ResizeToCanvas(this Image Img, int Width, int Height, out Rectangle CropRectangle)
    {
        return Img.ResizeToCanvas(Width, Height, InterpolationMode.HighQualityBicubic, out CropRectangle);
    }

    public static Image ResizeToCanvas(this Image Img, int Width, int Height, InterpolationMode InterpolationMode, out Rectangle CropRectangle)
    {
        CropRectangle = EnsureAspectRatio(Img, Width, Height);
        Image CropedImage = new Bitmap(Width, Height);

        using (Graphics G = Graphics.FromImage(CropedImage))
        {
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.InterpolationMode = InterpolationMode;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.DrawImage(Img, new Rectangle(0, 0, Width, Height), CropRectangle, GraphicsUnit.Pixel);
        }

        return CropedImage;
    }

    public static Image ResizeToCanvas(this Image Img, int Width, int Height, RectangleF CR)
    {
        return Img.ResizeToCanvas(Width, Height, InterpolationMode.HighQualityBicubic, CR);
    }

    public static Image ResizeToCanvas(this Image Img, int Width, int Height, InterpolationMode InterpolationMode, RectangleF CR)
    {
        Image CropedImage = new Bitmap(Width, Height);
        using (Graphics G = Graphics.FromImage(CropedImage))
        {
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.InterpolationMode = InterpolationMode;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.DrawImage(Img, new Rectangle(0, 0, Width, Height), CR, GraphicsUnit.Pixel);
        }

        return CropedImage;
    }
}