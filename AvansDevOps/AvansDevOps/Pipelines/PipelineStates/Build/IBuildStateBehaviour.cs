using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates.Build
{
    public interface IBuildStateBehaviour
    {
        bool Execute();
        string GetErrorMessage();
    }
}
