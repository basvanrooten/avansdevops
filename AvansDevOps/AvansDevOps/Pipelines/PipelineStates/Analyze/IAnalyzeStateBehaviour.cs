using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates.Analyze
{
    public interface IAnalyzeStateBehaviour
    {
        bool Execute();
        IPipelineState NextState();
        string GetErrorMessage();
    }
}
