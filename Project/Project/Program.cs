using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing.Imaging;

internal class Program
{
    public static void GetImg()
    {
        Mat img = CvInvoke.Imread("thumbsUp.jpg", ImreadModes.AnyColor);
        Image<Bgr, byte> imageFrame = img.ToImage<Bgr, byte>();
        Image<Gray, byte> grayFrame = imageFrame.Convert<Gray, byte>();

        CascadeClassifier haar = new CascadeClassifier("test.xml");

        var hands = haar.DetectMultiScale(grayFrame);
        Console.WriteLine("Img: " + imageFrame.Height + " " + imageFrame.Width);
        foreach (var hand in hands)
        {
            Console.WriteLine("X: " + (hand.X - hand.Width/2) + " Y: " + (hand.Y - hand.Height/2));
            imageFrame.Draw(hand, new Bgr(System.Drawing.Color.Green), 3);
        }
        //wystarczy przeskalowac dane na podstsawie wielkosci zdjecia

        imageFrame.ToBitmap().Save("thumbsUp2.png", ImageFormat.Png);
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        GetImg();
    }
}