using Numismatics.CORE.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Interface
{
    public interface IExportDataService
    {
        ExportFormat Format { get; }
        string ExportPath { get; set; }
        public void ExportCountriesData();
        public void ExportCurrenciesData();
        public void ExportCoinsData();
        public void ExportBanknotesData();
        public void ExportOwnedCoinsData();
        public void ExportOwnedBanknotesData();
    }
}
