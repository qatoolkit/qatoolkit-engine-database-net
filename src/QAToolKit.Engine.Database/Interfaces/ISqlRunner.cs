using System.Threading.Tasks;
using QAToolKit.Engine.Database.Models;

namespace QAToolKit.Engine.Database.Interfaces
{
    /// <summary>
    /// SQL Runner interface
    /// </summary>
    public interface ISqlRunner
    {
        /// <summary>
        /// Run SQl scripts
        /// </summary>
        /// <returns></returns>
        Task<TestResult> Run(Test databaseTest, TestRunnerOptions databaseTestOptions);
    }
}