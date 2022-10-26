using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MudBlazor.Input.Data.ViewModel;

namespace MudBlazor.Input.Data
{
    public interface IProvinsiRepository
    {
     
        Task<IEnumerable<Provinsi>> GetAll();
     

    }
}
