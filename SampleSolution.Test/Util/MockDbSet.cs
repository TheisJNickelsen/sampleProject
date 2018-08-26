using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace SampleSolution.Test.Util
{
    public class MockDbSet<TEntity> : Mock<DbSet<TEntity>> where TEntity : class
    {
        public MockDbSet(List<TEntity> dataSource = null)
        {
            var data = (dataSource ?? new List<TEntity>());
            var queryable = data.AsQueryable();

            this.As<IQueryable<TEntity>>().Setup(e => e.Provider).Returns(queryable.Provider);
            this.As<IQueryable<TEntity>>().Setup(e => e.Expression).Returns(queryable.Expression);
            this.As<IQueryable<TEntity>>().Setup(e => e.ElementType).Returns(queryable.ElementType);
            this.As<IQueryable<TEntity>>().Setup(e => e.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            //Mocking the insertion of entities
            this.Setup(_ => _.Add(It.IsAny<TEntity>())).Returns((TEntity arg) => {
                data.Add(arg);
                return arg as EntityEntry<TEntity>;
            });

            this.Setup(_ => _.Remove(It.IsAny<TEntity>())).Returns((TEntity arg) => {
                data.Remove(arg);
                return arg as EntityEntry<TEntity>;
            });
        }
        public MockDbSet(IQueryable<TEntity> dataSource = null)
        {
            var data = (dataSource ?? new List<TEntity>().AsQueryable());
            var queryable = data.AsQueryable();

            this.As<IQueryable<TEntity>>().Setup(e => e.Provider).Returns(queryable.Provider);
            this.As<IQueryable<TEntity>>().Setup(e => e.Expression).Returns(queryable.Expression);
            this.As<IQueryable<TEntity>>().Setup(e => e.ElementType).Returns(queryable.ElementType);
            this.As<IQueryable<TEntity>>().Setup(e => e.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            //Mocking the insertion of entities
            this.Setup(_ => _.Add(It.IsAny<TEntity>())).Returns((TEntity arg) => {
                data.Append(arg);
                return arg as EntityEntry<TEntity>;
            });

            this.Setup(_ => _.Remove(It.IsAny<TEntity>())).Returns((TEntity arg) => {
                data = data.Where(a => a != arg);
                return arg as EntityEntry<TEntity>;
            });
        }
    }
}
