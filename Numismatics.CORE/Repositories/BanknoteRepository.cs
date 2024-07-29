using Numismatics.CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Numismatics.CORE.Repositories
{
    public class BanknoteRepository
    {
        private readonly string _filePath = @"../Data/banknotes.csv";
        private readonly int _nextId;

        public BanknoteRepository() { }
        public BanknoteRepository(int nextId)
        {
            _nextId = nextId;
        }

        public Banknote Add(Banknote banknote)
        {
            banknote.Id = _nextId;
            return banknote;
        }


    }
}
