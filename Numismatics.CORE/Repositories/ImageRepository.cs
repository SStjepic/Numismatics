using Microsoft.VisualBasic;
using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public class ImageRepository
    {
        private readonly string _baseFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        public ImageRepository()
        {
            Directory.CreateDirectory(_baseFolder);
        }

        public (string obverseImagePath, string reverseImagePath) SaveCoinImage(int moneyId, string obversePath, string reversePath)
        {
            string coinImageFolder = Path.Combine(_baseFolder, Images.CoinImages.ToString(), moneyId.ToString());
            Directory.CreateDirectory(coinImageFolder);

            string destinationObverse = Path.Combine(coinImageFolder, "obverse.jpg");
            string destinationReverse = Path.Combine(coinImageFolder, "reverse.jpg");

            if (string.IsNullOrWhiteSpace(obversePath))
            {
                if (File.Exists(destinationObverse))
                {
                    File.Delete(destinationObverse);
                }
            }
            else
            {
                if (!File.Exists(destinationObverse))
                {
                    File.Copy(obversePath, destinationObverse, true);
                }
            }

            if (string.IsNullOrWhiteSpace(reversePath))
            {
                if (File.Exists(destinationReverse))
                {
                    File.Delete(destinationReverse);
                }

            }
            else
            {
                if (!File.Exists(destinationReverse))
                {
                    File.Copy(reversePath, destinationReverse, true);
                }
            }

            return (destinationObverse, destinationReverse);
        }

        public string GetImagePath(int moneyId, Images imagesFolder, ImageSide imageSide)
        {
            string imagePath = "";

            return imagePath;
        }
    }
}
