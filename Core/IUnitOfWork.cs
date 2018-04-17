using System;


    public interface IUnitOfWork : IDisposable
    {
        //ICourseRepository Courses { get; }
        
        int Complete();
    }
