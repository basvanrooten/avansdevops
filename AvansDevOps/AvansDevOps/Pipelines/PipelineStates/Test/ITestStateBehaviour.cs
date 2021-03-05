using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates.Test
{
    public interface ITestStateBehaviour
    {
        bool Execute();
        string GetErrorMessage();
    }
}
