using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
