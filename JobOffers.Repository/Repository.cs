using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOffers.Repository
{
    public abstract class Repository<T> : IRepository<T> where T:class
    {
    }
}
