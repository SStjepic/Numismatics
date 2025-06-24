using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Domains.Enums
{
    public enum MoneyQuality
    {
        UNC,
        aUNC,
        XF,
        VF,
        F,
        VG,
        G,
        FAIR,
        PR
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