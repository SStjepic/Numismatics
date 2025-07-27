using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Repositories;
using Numismatics.CORE.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Export
{
    public class CsvExportDataService: IExportDataService
    {
        public ExportFormat Format => ExportFormat.Csv;

        public string ExportPath {  get; set; }

        private IBanknoteRepository _banknoteRepository;
        private IOwnedBanknotesRepository _ownedBanknotesRepository;
        private ICoinRepository _coinRepository;
        private IOwnedCoinRepository _ownedCoinRepository;
        private ICurrencyRepository _currencyRepository;
        private ICountryRepository _countryRepository;
        private INationalCurrencyRepository _nationalCurrencyRepository;

        public CsvExportDataService(IBanknoteRepository banknoteRepository, IOwnedBanknotesRepository ownedBanknotesRepository, ICoinRepository coinRepository, IOwnedCoinRepository ownedCoinRepository, ICurrencyRepository currencyRepository, ICountryRepository countryRepository, INationalCurrencyRepository nationalCurrencyRepository)
        {
            _banknoteRepository = banknoteRepository;
            _ownedBanknotesRepository = ownedBanknotesRepository;
            _coinRepository = coinRepository;
            _ownedCoinRepository = ownedCoinRepository;
            _currencyRepository = currencyRepository;
            _countryRepository = countryRepository;
            _nationalCurrencyRepository = nationalCurrencyRepository;
        }

        public void ExportBanknotesData()
        {
            var banknotes = _banknoteRepository.GetAll();
            WriteCsvFile("banknotes.csv", banknotes);
        }

        public void ExportCoinsData()
        {
            var coins = _coinRepository.GetAll();
            WriteCsvFile("coins.csv", coins);
        }

        public void ExportCountriesData()
        {
            var countries = _countryRepository.GetAll();
            WriteCsvFile("countries.csv", countries);
        }

        public void ExportCurrenciesData()
        {
            var currencies = _currencyRepository.GetAll();
            WriteCsvFile("currencies.csv", currencies);
        }

        public void ExportOwnedCoinsData()
        {
            var ownedCoins = _ownedCoinRepository.GetAll();
            WriteCsvFile("owned_coins.csv", ownedCoins);
        }

        public void ExportOwnedBanknotesData()
        {
            var ownedBanknotes = _ownedBanknotesRepository.GetAll();
            WriteCsvFile("owned_banknotes.csv", ownedBanknotes);
        }

        private void WriteCsvFile<T>(string fileName, IEnumerable<T> data)
        {
            var separator = ";"; // <- koristi tačka-zarez za kompatibilnost sa evropskim Excel-om

            var properties = typeof(T)
                .GetProperties()
                .Where(p =>
                    p.PropertyType.IsPrimitive ||
                    p.PropertyType == typeof(string) ||
                    p.PropertyType == typeof(DateTime) ||
                    p.PropertyType.IsValueType)
                .ToArray();

            var csvLines = new List<string>();

            // Header
            var header = string.Join(separator, properties.Select(p => EscapeCsv(p.Name)));
            csvLines.Add(header);

            // Rows
            foreach (var item in data)
            {
                var values = properties.Select(p =>
                {
                    var value = p.GetValue(item);
                    return EscapeCsv(value?.ToString() ?? "");
                });

                var line = string.Join(separator, values);
                csvLines.Add(line);
            }

            string fullPath = Path.Combine(ExportPath, fileName);
            File.WriteAllLines(fullPath, csvLines, Encoding.UTF8);
        }

        private string EscapeCsv(string value)
        {
            if (value.Contains(";") || value.Contains("\"") || value.Contains("\n"))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }
            return value;
        }


    }
}
