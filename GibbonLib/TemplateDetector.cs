using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using AForge.Imaging;
//using AForge.Imaging.Filters;
using System.Drawing;

namespace GibbonLib
{

    public class ImagesDirectoryPath
    {
        public ImagesDirectoryPath()
        {
            CurrentScreenShotName = "None.png";
        }
        public string TemplatesPath { get; set; }
      
        public string CurrentScreenShotPath { get; set; }

        public string CurrentScreenShotName { get; set; }

        public string CurrentScreenShotFullPathAndName
        {
            get { return CurrentScreenShotPath + CurrentScreenShotName; }
        }

       
    }
    public class TemplateDetector
    {
       
        
        public TemplateDetector(ImagesDirectoryPath imagesDirectoryPath)
        {
            _paths = imagesDirectoryPath;
        }
        private ImagesDirectoryPath _paths { get; set; }

        public void ChangeCurrentScreenShotName(string newName)
        {
          _paths.CurrentScreenShotName=newName;
        }

        public ImagesDirectoryPath GetImagesDirectoryPath()
        {
          return  _paths;
        }
        public  RecognitionResult Recognize(string template,  int tempalteIndex = 1, double accuracy=0.90,string deviceID="")
        {
            bool isTextRcognition=true;
            RecognitionResult result = new RecognitionResult();
            Point postion = new Point();
            Logging.WriteLine("Verfiying template " + template, deviceID);
            string fullTemplate = template;
         
            if (template.EndsWith(".png"))//automatically detect if to use image for text 
            {
                fullTemplate = _paths.TemplatesPath + template;
                result.IsTemplateFound = SURFFeatureExample.ImageDetector.Match(_paths.CurrentScreenShotFullPathAndName, fullTemplate, ref postion, accuracy: accuracy);
            }
            else
            {
                result.IsTemplateFound = SURFFeatureExample.ImageDetector.RecognizeByWord(_paths.CurrentScreenShotFullPathAndName, fullTemplate, ref postion, tempalteIndex);  
            }
          
            if (result.IsTemplateFound)
                Logging.WriteLine("Template " + template + " is Verified. ", deviceID);
            else Logging.WriteLine("Template " + template + " is not Verified!", deviceID);
            result.TemplatePosition = postion;
            return result;
        }

        
        public  string GetAllText()
        {
            return SURFFeatureExample.ImageDetector.GetAllText(_paths.CurrentScreenShotFullPathAndName);
        }

        public string GetLabelStatusText(string template)
        {
             string alltext = SURFFeatureExample.ImageDetector.GetAllText(_paths.CurrentScreenShotFullPathAndName); ;
             string[] splitted = alltext.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int index = 0;
            foreach (string str in splitted)
            {
                if (str.Contains(template) && index< splitted.Count())
                {
                    return splitted[index+1];
                }
                index++;
            }
            return String.Empty;
        }
        
    }
  public  class RecognitionResult
    {
        public bool IsTemplateFound { get; set; }
        public Point TemplatePosition { get; set; }
        public void AddToX(int n)
        {
            TemplatePosition = new Point(TemplatePosition.X+ n, TemplatePosition.Y );
        }
        public void AddToY(int n)
        {
            TemplatePosition = new Point(TemplatePosition.X, TemplatePosition.Y + n);
        }
    }
}
