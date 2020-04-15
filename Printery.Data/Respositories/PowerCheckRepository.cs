using Printery.Data.Contexts;
using Printery.Data.Model;
using Printery.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printery.Data.Respositories
{
    public interface IPowerCheckRepository
    {
        SAViewModel CheckSAuser(string empid);
        List<string> CheckPower(string usergroupname);
    }
    public class PowerCheckRepository:IPowerCheckRepository
    {
        private readonly IPrinteryContext _db;
        private readonly DbContext _dbContext;
        public PowerCheckRepository(IPrinteryContext db)
        {
            _db = db;
            _dbContext = db.GetDbContext();
        }
        public SAViewModel CheckSAuser(string empid)
        {
            var sa = new SAViewModel();
            var exitsa = _db.SuperUserList.FirstOrDefault(s => s.EmpId == empid);
            if (exitsa != null)
            {
                sa.EmpId = exitsa.EmpId;
                sa.PowerGUIDName = exitsa.PowerGUIDName;
            }
            return sa;
        }
        public List<string> CheckPower(string usergroupname)
        {
            var power = new List<string>();
            var usergroup = _db.EmpGroup.FirstOrDefault(s => s.GroupName == usergroupname);
            if (usergroup != null) {
                var powerlist = from u in _db.PowerControlList
                                where u.GroupId == usergroup.GroupId
                                select u;
                foreach (var item in powerlist)
                {
                    power.Add(item.PowerId);
                }
            }
            return power;
        }
    }
}
