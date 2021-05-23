using XKom.Models.ModelsDB;

namespace XKom.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly XKomContext _xKomContext;

        public BaseRepository(XKomContext xKomContext)
        {
            _xKomContext = xKomContext;
        }
    }
}
