using System;
using System.Collections.Generic;
using System.Text;

namespace DMFX.NewsAnalysis.Interfaces
{
    public interface IDalBase<TEntity> : IInitializable
    {
        IList<TEntity> GetAll();

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);
    }
}
