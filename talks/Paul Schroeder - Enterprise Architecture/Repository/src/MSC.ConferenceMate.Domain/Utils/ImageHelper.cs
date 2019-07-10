using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace MSC.ConferenceMate.Domain.Utils
{
	public static class ImageHelper
	{
		/// <summary>
		/// Resize the image to the specified width and height.
		/// </summary>
		/// <remarks>
		/// wrapMode.SetWrapMode(WrapMode.TileFlipXY) prevents ghosting around the image borders -- naïve resizing will sample transparent pixels beyond the image boundaries, but by mirroring the image we can get a better sample (this setting is very noticeable)
		/// destImage.SetResolution maintains DPI regardless of physical size -- may increase quality when reducing image dimensions or when printing
		/// Compositing controls how pixels are blended with the background -- might not be needed since we're only drawing one thing.
		/// graphics.CompositingMode determines whether pixels from a source image overwrite or are combined with background pixels.SourceCopy specifies that when a color is rendered, it overwrites the background color.
		/// graphics.CompositingQuality determines the rendering quality level of layered images.
		/// graphics.InterpolationMode determines how intermediate values between two endpoints are calculated
		/// graphics.SmoothingMode specifies whether lines, curves, and the edges of filled areas use smoothing(also called antialiasing) -- probably only works on vectors
		/// graphics.PixelOffsetMode affects rendering quality when drawing the new image
		/// </remarks>
		/// <param name="image">The image to resize.</param>
		/// <param name="width">The width to resize to.</param>
		/// <param name="height">The height to resize to.</param>
		/// <returns>The resized image.</returns>
		public static Bitmap ResizeImage(Image image, int width, int height)
		{
			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighSpeed; // .HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighSpeed; //.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			return destImage;
		}
	}
}