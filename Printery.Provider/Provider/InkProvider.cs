using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    public interface IInkProvider
    {
        Task<List<InkCViewModel>> GetAllInk();
        void UpdateInk(InkCViewModel ink);
    }
    public class InkProvider:IInkProvider
    {
        private readonly IInkRespository _inkRespository;
        public InkProvider(IInkRespository inkRespository)
        {
            _inkRespository = inkRespository;
        }

        public async Task<List<InkCViewModel>> GetAllInk()
        {
            var list = new List<InkCViewModel>();
            list = await _inkRespository.GetAllInk();
            return (list != null) ? list : new List<InkCViewModel>();
        }
        public void UpdateInk(InkCViewModel ink)
        {
            _inkRespository.UpdateInk(ink);
        }
    }
}

