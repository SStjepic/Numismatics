﻿using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Services.Interface
{
    public interface IService<T>
    {
        public T? Get(long entityId);
        public T? Create(T entity);
        public T? Update(T entity);
        public T? Delete(T entity);
        public List<T> GetAll();
        public int GetTotalPageNumber(int pageSize);
    }
}
