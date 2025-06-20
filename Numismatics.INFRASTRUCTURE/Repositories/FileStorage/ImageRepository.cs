﻿using Microsoft.VisualBasic;
using Numismatics.CORE.Domains.Models;
using Numismatics.CORE.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numismatics.CORE.Repositories;

namespace Numismatics.INFRASTRUCTURE.Repositories.FileStorage
{
    public class ImageRepository: IImageRepository
    {
        private readonly string _baseFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NumismaticsAppData",
            "Images"
        );
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

        public void DeleteBanknote(long id)
        {
            string banknoteImageFolder = Path.Combine(_baseFolder, Images.BanknoteImages.ToString(), id.ToString());
            Directory.Delete(banknoteImageFolder);
        }

        public void DeleteCoin(long id)
        {
            string coinImageFolder = Path.Combine(_baseFolder, Images.CoinImages.ToString(), id.ToString());
            Directory.Delete(coinImageFolder);
        }
    }
}
