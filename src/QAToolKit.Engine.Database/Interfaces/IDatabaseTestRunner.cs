using QAToolKit.Engine.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QAToolKit.Engine.Database.Interfaces
{
    /// <summary>
    /// Database runner interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDatabaseTestRunner<T>
    {
        /// <summary>
        /// Database type
        /// </summary>
        public DatabaseKind DatabaseKind { get; }
        /// <summary>
        /// Run a script
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Run();
    }
}
