using JarmuKolcsonzo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.Repositories
{
    public class JarmuRepository : GenericRepository<Jarmu, JKContext>
    {
        public JarmuRepository(JKContext context) : base(context)
        {

        }
        private int _totalItems;

        public List<Jarmu> GetAll(
            int page = 1,
            int itemsPerPage = 20,
            string search = null,
            string sortBy = null,
            bool ascending = true)
        {
            var query = _context.Jarmuvek.Include(x => x.tipus).AsQueryable();
            // Keresés
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                // Ha a keresési kulcsszó szám
                int.TryParse(search, out int dij);
                // Ha dátum
                DateTime.TryParse(search, out DateTime datum);

                query = query.Where(x =>
                    x.rendszam.ToLower().Contains(search) ||
                    x.dij.Equals(dij) ||
                    x.tipus.megnevezes.ToLower().Contains(search) ||
                    x.szerviz_datum.Equals(datum));
            }

            // Sorbarendezés
            //if (!string.IsNullOrWhiteSpace(sortBy))
            //{
            //    switch (sortBy)
            //    {
            //        case "rendszam":
            //            query = ascending ? query.OrderBy(x => x.rendszam) : query.OrderByDescending(x => x.rendszam);
            //            break;
            //        case "tipus":
            //            query = ascending ? query.OrderBy(x => x.tipus.megnevezes) : query.OrderByDescending(x => x.tipus.megnevezes);
            //            break;
            //        case "dij":
            //            query = ascending ? query.OrderBy(x => x.dij) : query.OrderByDescending(x => x.dij);
            //            break;
            //        case "elerheto":
            //            query = ascending ? query.OrderBy(x => x.elerheto) : query.OrderByDescending(x => x.elerheto);
            //            break;
            //        case "szerviz_datum":
            //            query = ascending ? query.OrderBy(x => x.szerviz_datum) : query.OrderByDescending(x => x.szerviz_datum);
            //            break;
            //        default:
            //            break;
            //    }
            //}

            // Összes találat kiszámítása
            _totalItems = query.Count();

            // Oldaltördelés
            if (page + itemsPerPage > 0)
            {
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }
            return query.ToList();
        }

        public int TotalItems
        {
            get { return _totalItems; }
        }
    }
}
