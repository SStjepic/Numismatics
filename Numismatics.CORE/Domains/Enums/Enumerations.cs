using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Enums
{
    public enum MoneyQuality
    {
        G,
        VG,
        F,
        VF,
        XF,
        UNC
    }

    public enum Era
    {
        BCE,
        CE
    }

    public enum Images
    {
        BanknoteImages,
        CoinImages
    }

    public enum ImageSide
    {
        Obverse,
        Reverse
    }
}