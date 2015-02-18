using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace fzmAR
{
    public class HandTracker : IDisposable
    {
        CascadeClassifier palmHaar = new CascadeClassifier(@"data/palm.dat");
        CascadeClassifier fistHaar = new CascadeClassifier(@"data/fist.dat");

        public void Dispose()
        {
            if (palmHaar != null)
            {
                palmHaar.Dispose();
                palmHaar = null;
            }
            if (fistHaar != null)
            {
                fistHaar.Dispose();
                fistHaar = null;
            }
        }

        public Rectangle DetectPalm(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                return new Rectangle(0, 0, 0, 0);
            }
            Image<Bgr, Byte> img = new Image<Bgr, byte>(imagePath);
            Image<Gray, Byte> grayImage = img.Copy().Convert<Gray, byte>();
            Rectangle[] palmDetected = palmHaar.DetectMultiScale(grayImage, 1.4, 10, new Size(20, 20), Size.Empty);
            //faces.AddRange(facesDetected);
            if (palmDetected == null || palmDetected.Length == 0)
            {
                return new Rectangle(0, 0, 0, 0);
            }

            return palmDetected[0];
        }
        public Rectangle DetectPalm(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return new Rectangle(0, 0, 0, 0);
            }
            Image<Bgr, Byte> img = new Image<Bgr, byte>(bitmap);
            Image<Gray, Byte> grayImage = img.Copy().Convert<Gray, byte>();
            Rectangle[] palmDetected = palmHaar.DetectMultiScale(grayImage, 1.4, 10, new Size(20, 20), Size.Empty);
            //faces.AddRange(facesDetected);
            if (palmDetected == null || palmDetected.Length == 0)
            {
                return new Rectangle(0, 0, 0, 0);
            }

            return palmDetected[0];
        }
        public Rectangle DetectFist(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                return new Rectangle(0, 0, 0, 0);
            }
            Image<Bgr, Byte> img = new Image<Bgr, byte>(imagePath);
            Image<Gray, Byte> grayImage = img.Copy().Convert<Gray, byte>();
            Rectangle[] fistDetected = fistHaar.DetectMultiScale(grayImage, 1.4, 10, new Size(20, 20), Size.Empty);
            if (fistDetected == null || fistDetected.Length == 0)
            {
                return new Rectangle(0, 0, 0, 0);
            }

            return fistDetected[0];
        }

        public Rectangle DetectFist(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return new Rectangle(0, 0, 0, 0);
            }
            Image<Bgr, Byte> img = new Image<Bgr, byte>(bitmap);
            Image<Gray, Byte> grayImage = img.Copy().Convert<Gray, byte>();
            Rectangle[] fistDetected = fistHaar.DetectMultiScale(grayImage, 1.4, 10, new Size(20, 20), Size.Empty);
            if (fistDetected == null || fistDetected.Length == 0)
            {
                return new Rectangle(0, 0, 0, 0);
            }

            return fistDetected[0];
        }
    }
}
