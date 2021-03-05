using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates.Deploy
{
    public interface IDeployStateBehaviour
    {
        bool Execute();
        IPipelineState NextState();
        string GetErrorMessage();
    }
}
