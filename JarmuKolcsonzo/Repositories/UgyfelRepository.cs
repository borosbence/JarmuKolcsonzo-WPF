using JarmuKolcsonzo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.Repositories
{
    public class UgyfelRepository : GenericRepository<Ugyfel, JKContext>
    {
        public UgyfelRepository(JKContext context) : base(context)
        {

        }
        private int _totalItems;

        public List<Ugyfel> GetAll(
            int page = 1,
            int itemsPerPage = 20,
            string search = null,
            string sortBy = null,
            bool ascending = true)
        {
            var query = _context.Ugyfelek.OrderBy(x => x.vezeteknev).ThenBy(x => x.keresztnev).AsQueryable();
            // Keresés
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                search = search.Replace('.', ',');
                // Ha a keresési kulcsszó szám
                decimal.TryParse(search, out decimal szamErtek);

                if (search.Contains('+'))
                {
                    szamErtek = 0;
                }

                if (szamErtek > 0)
                {
                    query = query.Where(x =>
                        x.pont == szamErtek ||
                        x.iranyitoszam == szamErtek);
                }
                else
                {
                    query = query.Where(x =>
                        x.vezeteknev.ToLower().Contains(search) ||
                        x.keresztnev.ToLower().Contains(search) ||
                        x.varos.ToLower().Contains(search) ||
                        x.cim.ToLower().Contains(search) ||
                        x.telefonszam.ToLower().Contains(search) ||
                        x.email.ToLower().Contains(search));
                }
            }

            // Sorbarendezés
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy)
                {
                    case "keresztnev":
                        query = ascending ? query.OrderBy(x => x.keresztnev).ThenBy(x => x.vezeteknev) : query.OrderByDescending(x => x.keresztnev).ThenByDescending(x => x.vezeteknev);
                        break;
                    case "varos":
                        query = ascending ? query.OrderBy(x => x.varos) : query.OrderByDescending(x => x.varos);
                        break;
                    case "cim":
                        query = ascending ? query.OrderBy(x => x.cim) : query.OrderByDescending(x => x.cim);
                        break;
                    case "telefonszam":
                        query = ascending ? query.OrderBy(x => x.telefonszam) : query.OrderByDescending(x => x.telefonszam);
                        break;
                    case "email":
                        query = ascending ? query.OrderBy(x => x.email) : query.OrderByDescending(x => x.email);
                        break;
                    default:
                        query = ascending ? query.OrderBy(x => x.vezeteknev).ThenBy(x => x.keresztnev) : query.OrderByDescending(x => x.vezeteknev).ThenByDescending(x => x.keresztnev);
                        break;
                }
            }

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

        public bool Exist(int id)
        {
            return _context.Ugyfelek.Any(x => x.id == id);
        }
    }
}
