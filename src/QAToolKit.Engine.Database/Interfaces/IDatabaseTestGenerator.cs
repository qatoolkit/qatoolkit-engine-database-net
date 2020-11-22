using QAToolKit.Engine.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Database.Interfaces
{
    /// <summary>
    /// Database script generator interface
    /// </summary>
    public interface IDatabaseTestGenerator<T>
    {
        /// <summary>
        /// Database type
        /// </summary>
        public DatabaseKind DatabaseKind { get; }
        /// <summary>
        /// Generate a script
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Generate();
    }
}