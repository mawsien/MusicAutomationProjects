//----------------------------------------------------------------------------
//  Copyright (C) 2004-2011 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.GPU;
using Emgu.CV.OCR;

namespace SURFFeatureExample
{
    static class ImageDetector
    {

        public static bool Match(string pathImage, string pathTemplate, ref Point p, int templateIndexToMatch = 1,double accuracy=0.98)
        {
          
              
                Image<Gray, Byte> sourceImage = new Image<Gray, Byte>(pathImage);
                Image<Gray, Byte> templateImage = new Image<Gray, Byte>(pathTemplate);
                Image<Gray, float> result = sourceImage.MatchTemplate(templateImage, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
           
                double[] min, max;
                Point[] minLocations, maxLocations;

                result.MinMax(out min, out max, out minLocations, out maxLocations);

                int index = 0;
                foreach (double d in max)
                {

                    p = new Point(maxLocations[index].X + (templateImage.Size.Width / 2), maxLocations[index].Y + (templateImage.Size.Height / 2));
                    Console.WriteLine("D= {0}", d);
                    GibbonLib.Logging.WriteLine("Accuracy is D=" + d,"");
                    if (d >= accuracy)
                    {

                        return true;
                    }

                    index++;
                }
            
           
          return false;
   
        }
        public static bool RecognizeByImage(string pathImage, string pathTemplate, ref Point point)
        {

          //  Image<Gray, Byte> modelImage = new Image<Gray, byte>(@"Images/CurrentPhoneScreen.png");
          //  Image<Gray, Byte> observedImage = new Image<Gray, byte>(@"Images/espn.png");
            Image<Gray, Byte> modelImage = new Image<Gray, byte>(pathTemplate );
            Image<Gray, Byte> observedImage = new Image<Gray, byte>(pathImage);
            Stopwatch watch;
            HomographyMatrix homography = null;

            SURFDetector surfCPU = new SURFDetector(500, false);

            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;
            Matrix<int> indices;
            Matrix<float> dist;
            Matrix<byte> mask;
            bool isFound = false;
            if (GpuInvoke.HasCuda)
            {
                GpuSURFDetector surfGPU = new GpuSURFDetector(surfCPU.SURFParams, 0.01f);
                using (GpuImage<Gray, Byte> gpuModelImage = new GpuImage<Gray, byte>(modelImage))
                //extract features from the object image
                using (GpuMat<float> gpuModelKeyPoints = surfGPU.DetectKeyPointsRaw(gpuModelImage, null))
                using (GpuMat<float> gpuModelDescriptors = surfGPU.ComputeDescriptorsRaw(gpuModelImage, null, gpuModelKeyPoints))
                using (GpuBruteForceMatcher matcher = new GpuBruteForceMatcher(GpuBruteForceMatcher.DistanceType.L2))
                {
                    modelKeyPoints = new VectorOfKeyPoint();
                    surfGPU.DownloadKeypoints(gpuModelKeyPoints, modelKeyPoints);
                    watch = Stopwatch.StartNew();

                    // extract features from the observed image
                    using (GpuImage<Gray, Byte> gpuObservedImage = new GpuImage<Gray, byte>(observedImage))
                    using (GpuMat<float> gpuObservedKeyPoints = surfGPU.DetectKeyPointsRaw(gpuObservedImage, null))
                    using (GpuMat<float> gpuObservedDescriptors = surfGPU.ComputeDescriptorsRaw(gpuObservedImage, null, gpuObservedKeyPoints))
                    using (GpuMat<int> gpuMatchIndices = new GpuMat<int>(gpuObservedDescriptors.Size.Height, 2, 1))
                    using (GpuMat<float> gpuMatchDist = new GpuMat<float>(gpuMatchIndices.Size, 1))
                    {
                        observedKeyPoints = new VectorOfKeyPoint();
                        surfGPU.DownloadKeypoints(gpuObservedKeyPoints, observedKeyPoints);

                        matcher.KnnMatch(gpuObservedDescriptors, gpuModelDescriptors, gpuMatchIndices, gpuMatchDist, 2, null);

                        indices = new Matrix<int>(gpuMatchIndices.Size);
                        dist = new Matrix<float>(indices.Size);
                        gpuMatchIndices.Download(indices);
                        gpuMatchDist.Download(dist);

                        mask = new Matrix<byte>(dist.Rows, 1);

                        mask.SetValue(255);

                        Features2DTracker.VoteForUniqueness(dist, 0.8, mask);

                        int nonZeroCount = CvInvoke.cvCountNonZero(mask);
                        if (nonZeroCount >= 4)
                        {
                            nonZeroCount = Features2DTracker.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, indices, mask, 1.5, 20);
                            if (nonZeroCount >= 4)
                                homography = Features2DTracker.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, indices, mask, 3);
                        }

                        watch.Stop();
                    }
                }
            }
            else
            {
                //extract features from the object image
                modelKeyPoints = surfCPU.DetectKeyPointsRaw(modelImage, null);
                //MKeyPoint[] kpts = modelKeyPoints.ToArray();
                Matrix<float> modelDescriptors = surfCPU.ComputeDescriptorsRaw(modelImage, null, modelKeyPoints);

                watch = Stopwatch.StartNew();

                // extract features from the observed image
                observedKeyPoints = surfCPU.DetectKeyPointsRaw(observedImage, null);
                Matrix<float> observedDescriptors = surfCPU.ComputeDescriptorsRaw(observedImage, null, observedKeyPoints);

                BruteForceMatcher matcher = new BruteForceMatcher(BruteForceMatcher.DistanceType.L2F32);
                matcher.Add(modelDescriptors);
                int k = 2;
                indices = new Matrix<int>(observedDescriptors.Rows, k);
                dist = new Matrix<float>(observedDescriptors.Rows, k);
                matcher.KnnMatch(observedDescriptors, indices, dist, k, null);

                mask = new Matrix<byte>(dist.Rows, 1);

                mask.SetValue(255);

                Features2DTracker.VoteForUniqueness(dist, 0.8, mask);

                int nonZeroCount = CvInvoke.cvCountNonZero(mask);
                if (nonZeroCount >= 4)
                {
                    nonZeroCount = Features2DTracker.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, indices, mask, 1.5, 20);
                    if (nonZeroCount >= 4)
                        homography = Features2DTracker.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, indices, mask, 3);
                }
                if (nonZeroCount > 0) isFound = true;
                watch.Stop();
            }

            //Draw the matched keypoints
            Image<Bgr, Byte> result = Features2DTracker.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
               indices, new Bgr(255, 255, 255), new Bgr(255, 255, 255), mask, Features2DTracker.KeypointDrawType.NOT_DRAW_SINGLE_POINTS);

          
            #region draw the projected region on the image
            if (homography != null)
            {  //draw a rectangle along the projected model
                Rectangle rect = modelImage.ROI;
                PointF[] pts = new PointF[] { 
               new PointF(rect.Left, rect.Bottom),
               new PointF(rect.Right, rect.Bottom),
               new PointF(rect.Right, rect.Top),
               new PointF(rect.Left, rect.Top)};
                homography.ProjectPoints(pts);

                result.DrawPolyline(Array.ConvertAll<PointF, Point>(pts, Point.Round), true, new Bgr(Color.Red), 5);

                float X = (pts[1].X + pts[3].X) / 2;
                float Y = (pts[1].Y + pts[3].Y) / 2;
                point.X = (int)X;
                point.Y= (int)Y;
                if (X == 0 && Y == 0) isFound = false;
                else isFound = true;

            }
            #endregion

          Console.WriteLine(String.Format("Matched using {0} in {1} milliseconds", GpuInvoke.HasCuda ? "GPU" : "CPU", watch.ElapsedMilliseconds));

        
           return isFound;
        }
   //           static void Run()
   //   {
   //      MCvSURFParams surfParam = new MCvSURFParams(500, false);
 
   //      Image<Gray, Byte> modelImage = new Image<Gray, byte>("box.png");
   //      //extract features from the object image
   //      SURFFeature[] modelFeatures = modelImage.ExtractSURF(ref surfParam);
 
   //      Image<Gray, Byte> observedImage = new Image<Gray, byte>("box_in_scene.png");
   //      // extract features from the observed image
   //      SURFFeature[] imageFeatures = observedImage.ExtractSURF(ref surfParam);
 
   //      //Create a SURF Tracker using k-d Tree

   //      Emgu.CV.Features2D.FastDetector detector = new FastDetector(09, false);
   //      //Comment out above and uncomment below if you wish to use spill-tree instead
   //      //SURFTracker tracker = new SURFTracker(modelFeatures, 50, .7, .1);

   //      detecto.MatchedSURFFeature[] matchedFeatures = tracker.MatchFeature(imageFeatures, 2, 20);
   //      matchedFeatures = SURFTracker.VoteForUniqueness(matchedFeatures, 0.8);
   //      matchedFeatures = SURFTracker.VoteForSizeAndOrientation(matchedFeatures, 1.5, 20);
   //      HomographyMatrix homography = SURFTracker.GetHomographyMatrixFromMatchedFeatures(matchedFeatures);
 
   //      //Merge the object image and the observed image into one image for display
   //      Image<Gray, Byte> res = modelImage.ConcateVertical(observedImage);
 
   //      #region draw lines between the matched features
   //      foreach (SURFTracker.MatchedSURFFeature matchedFeature in matchedFeatures)
   //      {
   //         PointF p = matchedFeature.ObservedFeature.Point.pt;
   //         p.Y += modelImage.Height;
   //         res.Draw(new LineSegment2DF(matchedFeature.ModelFeatures[0].Point.pt, p), new Gray(0), 1);
   //      }
   //      #endregion
 
   //      #region draw the project region on the image
   //      if (homography != null)
   //      {  //draw a rectangle along the projected model
   //         Rectangle rect = modelImage.ROI;
   //         PointF[] pts = new PointF[] { 
   //            new PointF(rect.Left, rect.Bottom),
   //            new PointF(rect.Right, rect.Bottom),
   //            new PointF(rect.Right, rect.Top),
   //            new PointF(rect.Left, rect.Top)};
   //         homography.ProjectPoints(pts);
 
   //         for (int i = 0; i < pts.Length; i++)
   //            pts[i].Y += modelImage.Height;
 
   //         res.DrawPolyline(Array.ConvertAll<PointF, Point>(pts, Point.Round), true, new Gray(255.0), 5);
   //      }
   //      #endregion
 
   //      ImageViewer.Show(res);
      
   //}
        public static bool RecognizeByWord(string imagePath, string wordToFind, ref Point p, int templateIndexToMatch = 1)
       {
           Stopwatch watch = Stopwatch.StartNew();  
           String alltexts = String.Empty;
           bool isFound=false;
               Bgr drawColor = new Bgr(Color.Blue);
               try
               {
                   Image<Bgr, Byte> image = new Image<Bgr, byte>(imagePath);

                   using (Image<Gray, byte> gray = image.Convert<Gray, byte>())
                   {
                       Tesseract _ocr=null;
                       Tesseract.OcrEngineMode[] ocrEngineModes = new Tesseract.OcrEngineMode[2] { 
                       Tesseract.OcrEngineMode.OEM_DEFAULT,Tesseract.OcrEngineMode.OEM_CUBE_ONLY };
                       bool found = false;
                       Tesseract.Charactor[] charactors=null;
                       for (int i = 0; i < ocrEngineModes.Length; i++)
                       {
                           _ocr = new Tesseract(@"tessdata", "eng", ocrEngineModes[i]);
                        
                           _ocr.Recognize(gray);
                           charactors = _ocr.GetCharactors();
                           alltexts = _ocr.GetText().Replace(" ","");
                           Console.WriteLine("Word >> {0} Text : {1} >> ", wordToFind, alltexts);
                           if (alltexts.Replace(" ", "").Contains(wordToFind))
                           {
                               found = true;
                            
                               break;
                           }
                       }
                       if (!found) return false;
                    
                       char[] charArrayWordToFind = wordToFind.ToCharArray();
                       Tesseract.Charactor[] charactorsFoundWord = new Tesseract.Charactor[charArrayWordToFind.Length];
                       List<Tesseract.Charactor[]> listFoundWord = new List<Tesseract.Charactor[]>();
                       
                       //remove empty space
                      List<Tesseract.Charactor> charactorsWithoutEmptySpace=new List<Tesseract.Charactor>();
                      foreach (Tesseract.Charactor c in charactors)
                      {
                          if (c.Text != " ")
                              charactorsWithoutEmptySpace.Add(c);
                      }
                   
                       //int count=0;
                       //foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(alltexts, wordToFind, System.Text.RegularExpressions.RegexOptions.None))
                       //{
                       //    count++;
                       //}
                       int count = 0;
                       int wordOccurrence=    CountStringOccurrences(alltexts, wordToFind);
                       int emptyCharacterCounter = 0;
                       for (int i = 0; i < charactorsWithoutEmptySpace.Count - 1; i++)
                       {
                           if (charactorsWithoutEmptySpace[i].Text == charArrayWordToFind[0].ToString())
                           {
                               for (int j=0;j< charArrayWordToFind.Length;j++)
                               {
                                   if (charactorsWithoutEmptySpace[i + j].Text == charArrayWordToFind[j].ToString())
                                   {
                                       charactorsFoundWord[j] = charactorsWithoutEmptySpace[i + j];
                                       isFound = true;
                                   }
                                   else
                                   {
                                       isFound = false;
                                       break;
                                   }
                               }
                           }
                           if (isFound )
                           {
                               ++count;
                               Tesseract.Charactor centerCharactor = charactorsFoundWord[charactorsFoundWord.Length / 2];
                               p = new Point(centerCharactor.Region.X + (centerCharactor.Region.Width / 2), centerCharactor.Region.Y + (centerCharactor.Region.Height / 2));
                               if (count == templateIndexToMatch)
                               {
                                   break;
                               }
                               if (wordOccurrence > 1)
                               {
                                   isFound = false;
                                  
                               }else break;
                           }
                       }
                       
                         //  foreach (Tesseract.Charactor c in charactors)
                        //   {
                        //       image.Draw(c.Region, drawColor, 1);

                         //  }
                     //  String text = String.Concat( Array.ConvertAll(charactors, delegate(Tesseract.Charactor t) { return t.Text; }) );
                     //   alltexts = _ocr.GetText();
                     //  return alltexts;
                   }
               }
               catch (Exception exception)
               {
                   Console.WriteLine(exception.Message);
               }
               watch.Stop();
             //  Console.WriteLine(String.Format("Matched using {0} in {1} milliseconds", GpuInvoke.HasCuda ? "GPU" : "CPU", watch.ElapsedMilliseconds));
               Console.WriteLine(String.Format("OCR {0} milliseconds", watch.ElapsedMilliseconds));
               return isFound;
       }



       public static string GetAllText(string imagePath)
       {
           Stopwatch watch = Stopwatch.StartNew();
           String alltexts = String.Empty;
     
           Bgr drawColor = new Bgr(Color.Blue);
           try
           {
               Image<Bgr, Byte> image = new Image<Bgr, byte>(imagePath);

               using (Image<Gray, byte> gray = image.Convert<Gray, Byte>())
               {
                   Tesseract _ocr = null;
                   Tesseract.OcrEngineMode[] ocrEngineModes = new Tesseract.OcrEngineMode[2] { 
                       Tesseract.OcrEngineMode.OEM_DEFAULT,Tesseract.OcrEngineMode.OEM_CUBE_ONLY };
                   bool found = false;
                   Tesseract.Charactor[] charactors = null;
                 
                   for (int i = 0; i < ocrEngineModes.Length; i++)
                   {
                       _ocr = new Tesseract(@"tessdata", "eng", ocrEngineModes[i]);
                       _ocr.Recognize(gray);
                       charactors = _ocr.GetCharactors();
                       alltexts += _ocr.GetText().Replace(" ","");
                   }
               }
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.Message);
           }
           return alltexts;
                 
       }
      
       //\r\n
       public static int CountStringOccurrences(string text, string pattern)
       {
           // Loop through all instances of the string 'text'.
           int count = 0;
           int i = 0;
           while ((i = text.IndexOf(pattern, i)) != -1)
           {
               i += pattern.Length;
               count++;
           }
           return count;
       }
        /// <summary>
        /// Check if both the managed and unmanaged code are compiled for the same architecture
        /// </summary>
        /// <returns>Returns true if both the managed and unmanaged code are compiled for the same architecture</returns>
        static bool IsPlaformCompatable()
        {
            int clrBitness = Marshal.SizeOf(typeof(IntPtr)) * 8;
            if (clrBitness != CvInvoke.UnmanagedCodeBitness)
            {
                Console.WriteLine(String.Format("Platform mismatched: CLR is {0} bit, C++ code is {1} bit."
                   + " Please consider recompiling the executable with the same platform target as C++ code.",
                   clrBitness, CvInvoke.UnmanagedCodeBitness));
                return false;
            }
            return true;
        }

      
    }
}