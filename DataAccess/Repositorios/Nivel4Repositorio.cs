using Core.Dominio;
using Core.Repositorios;
using DataAcces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositorios
{
    class Nivel4Repositorio : Repositorio<Nivel4>, INivel4
    {
        public Nivel4Repositorio(BDContext context) : base(context)
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
