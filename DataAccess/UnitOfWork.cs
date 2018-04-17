using Core;
using Core.Repositorios;
using DataAcces.Repositorios;

public class UnitOfWork : IUnitOfWork
    {
        private readonly BDContext _context;

        public UnitOfWork(BDContext context)
        {
            _context = context;
        //Courses = new CourseRepository(_context);
        Usuarios = new UsuarioRepositorio(_context);
        Colaboracion = new ColaboracionRepositorio(_context);
        TipoColaboracion = new TipoColaboracionRepositorio(_context);
        Area = new AreaRepositorio(_context);
        Nivel1 = new Nivel1Repositorio(_context);
        Nivel2 = new Nivel2Repositorio(_context);
        Nivel3 = new Nivel3Repositorio(_context);
        Nivel4 = new Nivel4Repositorio(_context);
        Nivel5 = new Nivel5Repositorio(_context);
        TipoDocumento = new TipoDocumentoRepositorio(_context);
        Docs = new DocumentoRepositorio(_context);

    }

    //public ICourseRepository Courses { get; private set; }
    public IUsuario Usuarios { get; private set; }
    public IColaboracion Colaboracion { get; private set; }
    public ITipoColaboracion TipoColaboracion { get; private set; }
    public IArea Area { get; private set; }
    public INivel1 Nivel1 { get; private set; }
    public INivel2 Nivel2 { get; private set; }
    public INivel3 Nivel3 { get; private set; }
    public INivel4 Nivel4 { get; private set; }
    public INivel5 Nivel5 { get; private set; }
    public IDocumento Docs { get; private set; }
    public ITipoDocumento TipoDocumento { get; private set; }
    public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
