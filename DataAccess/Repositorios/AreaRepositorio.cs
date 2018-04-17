using Core.Dominio;
using Core.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositorios
{
    class AreaRepositorio : Repositorio<Area>, IArea
    {
        public AreaRepositorio(BDContext context) : base(context)
        {
        }

        /*public Usuario GetAuthorWithCourses(int id)
        {
            return PlutoContext.Authors.Include(a => a.Courses).SingleOrDefault(a => a.Id == id);
        }*/

        public BDContext BDContext
        {
            get { return Context as BDContext; }
        }
    }
}
