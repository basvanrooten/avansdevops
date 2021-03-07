using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates
{
    public interface IPipelineState
    {
        void Execute();
        void NextState();
    }
}
