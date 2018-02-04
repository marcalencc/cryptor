using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptor.Model;

namespace Cryptor.Services
{
    public interface IDataProvider
    {
        string ApiEndPoint { get; }
        Task<List<Currency>> GetData();
    }
}
