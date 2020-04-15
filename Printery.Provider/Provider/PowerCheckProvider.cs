using Printery.Data.Respositories;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Provider.Provider
{
    public interface IPowerCheckProvider
    {
        SAViewModel CheckSAuser(string empid);
        List<string> CheckPower(string usergroupname);
    }
    public class PowerCheckProvider:IPowerCheckProvider
    {
        private readonly IPowerCheckRepository _powercheckRespository;
        public PowerCheckProvider(IPowerCheckRepository powercheckRespository)
        {
            _powercheckRespository = powercheckRespository;
        }
        public SAViewModel CheckSAuser(string empid)
        {
            var sa = new SAViewModel();
            sa= _powercheckRespository.CheckSAuser(empid);
            return sa;
        }
        public List<string> CheckPower(string usergroupname)
        {
            var list = new List<string>();
            list=_powercheckRespository.CheckPower(usergroupname);
            return list;
        }
    }
}
