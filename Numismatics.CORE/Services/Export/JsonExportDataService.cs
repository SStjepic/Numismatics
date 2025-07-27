using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Repositories;
using Numismatics.CORE.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Export
{
    public class JsonExportDataService : IExportDataService
    {
        public ExportFormat Format => ExportFormat.Json;

        public string ExportPath {  get; set; }

        private IBanknoteRepository _banknoteRepository;
        private IOwnedBanknotesRepository _ownedBanknotesRepository;
        private ICoinRepository _coinRepository;
        private IOwnedCoinRepository _ownedCoinRepository;
        private ICurrencyRepository _currencyRepository;
        private ICountryRepository _countryRepository;
        private INationalCurrencyRepository _nationalCurrencyRepository;

        public JsonExportDataService(IBanknoteRepository banknoteRepository, IOwnedBanknotesRepository ownedBanknotesRepository, ICoinRepository coinRepository, IOwnedCoinRepository ownedCoinRepository, ICurrencyRepository currencyRepository, ICountryRepository countryRepository, INationalCurrencyRepository nationalCurrencyRepository)
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
            WriteToFile("banknotes.json", banknotes);
        }

        public void ExportCoinsData()
        {
            var coins = _coinRepository.GetAll();
            WriteToFile("coins.json", coins);
        }

        public void ExportCountriesData()
        {
            var countries = _countryRepository.GetAll();
            WriteToFile("countries.json", countries);
        }

        public void ExportCurrenciesData()
        {
            var currencies = _currencyRepository.GetAll();
            WriteToFile("currencies.json", currencies);
        }

        public void ExportOwnedCoinsData()
        {
            var ownedCoins = _ownedCoinRepository.GetAll();
            WriteToFile("owned_coins.json", ownedCoins);
        }

        public void ExportOwnedBanknotesData()
        {
            var ownedBanknotes = _ownedBanknotesRepository.GetAll();
            WriteToFile("owned_banknotes.json", ownedBanknotes);
        }

        private void WriteToFile<T>(string fileName, IEnumerable<T> data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            string json = JsonSerializer.Serialize(data, options);

            string fullPath = Path.Combine(ExportPath, fileName);
            File.WriteAllText(fullPath, json);
        }

    }
}
