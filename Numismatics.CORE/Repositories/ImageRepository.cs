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

        public (string obverseImagePath, string reverseImagePath) SaveCoinImage(long moneyId, string obversePath, string reversePath)
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
                destinationObverse = obversePath;
            }
            else
            {
                if (!File.Exists(destinationObverse) || destinationObverse != obversePath)
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
                destinationReverse = reversePath;
            }
            else
            {
                if (!File.Exists(destinationReverse) || destinationReverse != reversePath)
                {
                    File.Copy(reversePath, destinationReverse, true);
                }
            }

            return (destinationObverse, destinationReverse);
        }

        public (string obverseImagePath, string reverseImagePath) SaveBanknoteImage(long moneyId, string obversePath, string reversePath)
        {
            string banknoteImageFolder = Path.Combine(_baseFolder, Images.BanknoteImages.ToString(), moneyId.ToString());
            Directory.CreateDirectory(banknoteImageFolder);

            string destinationObverse = Path.Combine(banknoteImageFolder, "obverse.jpg");
            string destinationReverse = Path.Combine(banknoteImageFolder, "reverse.jpg");

            if (string.IsNullOrWhiteSpace(obversePath))
            {
                if (File.Exists(destinationObverse))
                {
                    File.Delete(destinationObverse);
                }
                destinationObverse = obversePath;
            }
            else
            {
                if (!File.Exists(destinationObverse) || destinationObverse != obversePath)
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
                destinationReverse = reversePath;
            }
            else
            {
                if (!File.Exists(destinationReverse) || destinationReverse != reversePath)
                {
                    File.Copy(reversePath, destinationReverse, true);
                }
            }

            return (destinationObverse, destinationReverse);
        }
    }
}
