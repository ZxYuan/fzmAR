using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace fzmAR
{
    public partial class Form1 : Form
    {
        private Capture _capture = null;
        private bool _captureInProgress;

        private HandTracker handtracker = new HandTracker();

        Image<Bgr, Byte> sword = new Image<Bgr, byte>(@"data/lightsword.png");

        public Form1()
        {
            InitializeComponent();
            try
            {
                _capture = new Capture();
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> frame = _capture.RetrieveBgrFrame();
            Image<Bgr, Byte> rawFrame = frame.Copy();
            Image<Gray, Byte> grayFrame = frame.Convert<Gray, Byte>();
            Rectangle rect1 = handtracker.DetectFist(grayFrame.Bitmap);
            Rectangle rect2 = handtracker.DetectPalm(grayFrame.Bitmap);
            if (true || rect1.Width > 0)
            {
                Console.WriteLine("fist");
                Console.WriteLine(rect1.Width+", "+rect1.Height);
                Image<Bgr, Byte> curSword = sword.Resize(108, 94, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                Rectangle roi = new Rectangle(rect1.X, rect1.Y, curSword.Cols, curSword.Rows);
                frame.ROI = roi;
                Image<Bgr, Byte> roiImage = frame.Copy();
                curSword = curSword.Add(roiImage);
                curSword.CopyTo(frame);
                frame.ROI = Rectangle.Empty;
            }
            if (rect2.Width > 0)
            {
                Console.WriteLine("palm");
            }
            imageBox2.Image = frame;
            imageBox1.Image = rawFrame;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {  //stop the capture
                    button1.Text = "Start";
                    _capture.Pause();
                }
                else
                {
                    //start the capture
                    button1.Text = "Stop";
                    _capture.Start();
                }

                _captureInProgress = !_captureInProgress;
            }
        }

        private void ReleaseData()
        {
            if (_capture != null)
                _capture.Dispose();
        }
    }
}
