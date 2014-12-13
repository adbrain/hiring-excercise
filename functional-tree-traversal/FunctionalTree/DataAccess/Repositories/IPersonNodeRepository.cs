using Adbrain.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adbrain.DataAccess.Repositories
{
    public interface IPersonNodeRepository
    {
        PersonNode GetHead();

        bool IsEmpty();

        void Add(PersonNode person);
    }
}
