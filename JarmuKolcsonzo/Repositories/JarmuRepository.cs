using JarmuKolcsonzo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.Repositories
{
    public class JarmuRepository<TContext> : GenericRepository<Jarmu, TContext> where TContext : DbContext
    {
        public JarmuRepository(TContext context) : base(context)
        {

        }

        public override List<Jarmu> GetAll()
        {
            return _context.Set<Jarmu>().Include(x => x.tipus).ToList();
        }
    }
}
