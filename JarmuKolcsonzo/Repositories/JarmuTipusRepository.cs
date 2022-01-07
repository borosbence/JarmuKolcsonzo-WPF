using JarmuKolcsonzo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.Repositories
{
    public class JarmuTipusRepository : GenericRepository<JarmuTipus, JKContext>
    {
        public JarmuTipusRepository(JKContext context) : base(context)
        {

        }

        public override List<JarmuTipus> GetAll()
        {
            return _context.Jarmu_tipusok.OrderBy(x => x.megnevezes).ToList();
        }
    }
}
