using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Interfaces
{
    public interface ISmartphoneRepository
    {
        Task<List<Smartphone>> GetAllSmartphonesAsync(int page);
        Task<Smartphone> GetSmartphoneByIdAsync(int id);
        Task<Smartphone> UpdateSmartphoneAsync(Smartphone smartphone);
        Task<List<Smartphone>> AddSmartphonesAsync(List<Smartphone> smartphones);
        Task<Smartphone> DeleteSmartphoneAsync(int id);
        Task<List<Smartphone>> FindSimilarAsync(Smartphone smartphone);
    }
}
