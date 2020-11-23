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
        /// Run a script
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Run();
    }
}