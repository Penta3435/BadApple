using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Accord.Video.FFMPEG;

namespace ConsoleApp1
{
    internal class Program
    {
        static int currentFrame = -18;
        static Queue<Bitmap> frames = new Queue<Bitmap>();
        static Queue<Bitmap> resizedFrames = new Queue<Bitmap>();
        static Queue<string> stringFrames = new Queue<string>();
        static void Main(string[] args)
        {
            Thread loadFrames = new Thread(new ThreadStart(GetFrame));
            Thread resizeFrames = new Thread(new ThreadStart(ResizeFrame));
            Thread frameToString = new Thread(new ThreadStart(BitmapToString));


            Thread printFrames = new Thread(() => {
                while (true)
                {
                    Thread.Sleep(100);
                    if (stringFrames.Count > 0)
                    {
                        Console.Clear();
                        Console.Write(stringFrames.Dequeue());
                    }
                }
            });

            loadFrames.Start();
            resizeFrames.Start();
            frameToString.Start();
            printFrames.Start();





            //Bitmap frame = null;
            //do
            //{
            //    frame = GetFrame();
            //    for (int pixelY = 0; pixelY < frame.Height; pixelY++)
            //    {
            //        for (int pixelX = 0; pixelX < frame.Width; pixelX++)
            //        {
            //            if (frame.GetPixel(pixelX, pixelY).GetBrightness() > 0.5f)
            //            {
            //                Console.Write("■");
            //            }
            //            else Console.Write(" ");
            //        }
            //    }
            //    Console.Clear();
            //}
            //while (frame != null);
            
        }
        static void GetFrame()
        {
            using (VideoFileReader reader = new VideoFileReader())
            {
                reader.Open("C:\\Users\\cliu\\Desktop\\SnapSave.io-【東方】Bad Apple!! ＰＶ【影絵】(360p).mp4");
                while(true)
                    frames.Enqueue(reader.ReadVideoFrame(currentFrame += 18));
            }
        }
        static void ResizeFrame()
        {
            while (true)
            {
                if(frames.Count > 0) resizedFrames.Enqueue(new Bitmap(frames.Dequeue(), 120, 30));
                else Thread.Sleep(200);
            }
        }
        static void BitmapToString()
        {
            while (true)
            {
                if (resizedFrames.Count > 0)
                {
                    Bitmap bitmap = resizedFrames.Dequeue();
                    string stringFrame = "";
                    for (int pixelY = 0; pixelY < bitmap.Height; pixelY++)
                    {
                        for (int pixelX = 0; pixelX < bitmap.Width; pixelX++)
                        {
                            if (bitmap.GetPixel(pixelX, pixelY).GetBrightness() > 0.5f)
                            {
                                stringFrame += "■";
                            }
                            else stringFrame += " ";
                        }
                        stringFrame += "\n";
                    }
                    stringFrames.Enqueue(stringFrame);
                }
                else { Thread.Sleep(200); continue; }
            }
        }




        //static void GetFrame()
        //{
        //    Bitmap frameResized;
        //    do
        //    {
        //        string frameString = "";
        //        using (VideoFileReader reader = new VideoFileReader())
        //        {
        //            reader.Open("C:\\Users\\cliu\\Desktop\\SnapSave.io-【東方】Bad Apple!! ＰＶ【影絵】(360p).mp4");
        //            Bitmap frame = reader.ReadVideoFrame(currentFrame += 18);
        //            reader.Close();
        //            frameResized = new Bitmap(frame, new Size(120, 30));
        //        }

        //        for (int pixelY = 0; pixelY < frameResized.Height; pixelY++)
        //        {
        //            for (int pixelX = 0; pixelX < frameResized.Width; pixelX++)
        //            {
        //                if (frameResized.GetPixel(pixelX, pixelY).GetBrightness() > 0.5f)
        //                {
        //                    frameString += "■";
        //                }
        //                else frameString += " ";
        //            }
        //            frameString += "\n";
        //        }
        //        frames.Enqueue(frameString);
        //    } while (true);
        //}

        //static IEnumerable<string> GetFrame()
        //{
        //    Bitmap frameResized;
        //    while (true)
        //    {
        //        string frameString = "";
        //        using (VideoFileReader reader = new VideoFileReader())
        //        {
        //            reader.Open("C:\\Users\\cliu\\Desktop\\SnapSave.io-【東方】Bad Apple!! ＰＶ【影絵】(360p).mp4");
        //            Bitmap frame = reader.ReadVideoFrame(currentFrame += 18);
        //            reader.Close();
        //            frameResized = new Bitmap(frame, new Size(120, 30));
        //        }

        //        for (int pixelY = 0; pixelY < frameResized.Height; pixelY++)
        //        {
        //            for (int pixelX = 0; pixelX < frameResized.Width; pixelX++)
        //            {
        //                if (frameResized.GetPixel(pixelX, pixelY).GetBrightness() > 0.5f)
        //                {
        //                    frameString += "■";
        //                }
        //                else frameString += " ";
        //            }
        //            frameString += "\n";
        //        }
        //        yield return frameString;
        //    }
        //}

        //static Bitmap GetFrame()
        //{
        //    using (VideoFileReader reader = new VideoFileReader())
        //    {
        //        reader.Open("C:\\Users\\cliu\\Desktop\\SnapSave.io-【東方】Bad Apple!! ＰＶ【影絵】(360p).mp4");
        //        Bitmap frame = reader.ReadVideoFrame(currentFrame += 18);
        //        reader.Close();
        //        return new Bitmap(frame, new Size(120, 30));
        //    }
        //}

        //static IEnumerable<Bitmap> GetFrame()
        //{
        //    VideoFileReader reader = new VideoFileReader();
        //    reader.Open("C:\\Users\\cliu\\Desktop\\SnapSave.io-【東方】Bad Apple!! ＰＶ【影絵】(360p).mp4");
        //    for (int frameIndex = 0; frameIndex < reader.FrameCount; frameIndex += 18)
        //    {
        //        Bitmap frame = reader.ReadVideoFrame(frameIndex);
        //        yield return new Bitmap(frame, new Size(120, 30));
        //    }
        //}
    }
}
