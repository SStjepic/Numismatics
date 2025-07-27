using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Export
{
    public class ExportDataServiceFactory
    {
        private readonly IEnumerable<IExportDataService> _services;

        public ExportDataServiceFactory(IEnumerable<IExportDataService> services)
        {
            _services = services;
        }

        public IExportDataService? GetService(ExportFormat format)
        {
            return _services.FirstOrDefault(s => s.Format == format);
        }
    }
}
