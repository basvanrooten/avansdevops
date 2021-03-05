using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates.Source
{
    public interface ISourceStateBehaviour
    {
        bool Execute();
        IPipelineState NextState();
        string GetErrorMessage();
    }
}
