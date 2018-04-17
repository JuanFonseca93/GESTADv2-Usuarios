
using Core.Dominio;
using Core.Repositorios;

namespace DataAcces.Repositorios
{
    class UsuarioRepositorio : Repositorio<Usuario>, IUsuario
    {
        public UsuarioRepositorio(BDContext context) : base(context)
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
