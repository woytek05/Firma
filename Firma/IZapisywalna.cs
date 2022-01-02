using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    interface IZapisywalna
    {
        void ZapiszBin(string nazwa);
        object OdczytajBin(string nazwa);
    }
}
