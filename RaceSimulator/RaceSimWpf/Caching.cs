using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace RaceSimWpf
{
    public static class Caching
    {
        private static Dictionary<string, Bitmap> _bitMaps = new Dictionary<string, Bitmap>();

        public static Bitmap GetBitMap(string url)
        {
            if (_bitMaps.ContainsKey(url))
            {
                return _bitMaps[url];
            }
            else
            {
                var newBitmap = new Bitmap(url);
                _bitMaps.Add(url, newBitmap);
                return _bitMaps[url];
            }
        }

        public static void ClearCache()
        {
            _bitMaps = new Dictionary<string, Bitmap>();
        }

        public static Bitmap GetTrackSize(int x, int y)
        {
            int width = x;
            int height = y;

            if (_bitMaps.ContainsKey("empty"))
            {
                Bitmap clone = (Bitmap)_bitMaps["empty"].Clone();
                return clone;
            }
            else
            {
                var trackEmpty = new Bitmap(width, height);
                using (Graphics gfx = Graphics.FromImage(trackEmpty))
                using (SolidBrush background = new SolidBrush(System.Drawing.Color.Green))
                {
                    trackEmpty.SetResolution(gfx.DpiX, gfx.DpiY);
                    gfx.FillRectangle(background, 0, 0, width, height);
                }
                _bitMaps.Add("empty", trackEmpty);
                Bitmap clone = (Bitmap)_bitMaps["empty"].Clone();
                return clone;
            }
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
