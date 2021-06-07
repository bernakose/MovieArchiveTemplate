using System;

namespace MovieArchiveTemplate.Repositories 
{
    public interface IBaseRepository : IDisposable{ }

    public class BaseRepository : IBaseRepository 
    {
        protected MovieArchiveDB movieArchiveDB = new MovieArchiveDB();

        public BaseRepository()
        {   
        }

        public void Dispose()
        {
            movieArchiveDB.Dispose();
        }


    }
}