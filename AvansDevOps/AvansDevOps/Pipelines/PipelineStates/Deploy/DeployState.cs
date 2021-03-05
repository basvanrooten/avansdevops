using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Pipelines.PipelineStates.Utility;

namespace AvansDevOps.Pipelines.PipelineStates.Deploy
{
    public class DeployState : IPipelineState
    {

        private readonly IPipeline _pipeline;
        private readonly IDeployStateBehaviour _behaviour;
        public DeployState(IPipeline pipeline)
        {
            _pipeline = pipeline;
            _behaviour = new DeployBehaviour(_pipeline);
        }

        public void Execute()
        {
            var pipelineResult = _behaviour.Execute();

            if (!pipelineResult || _behaviour.GetErrorMessage() != null)
                throw new Exception($"Pipeline failed: DeployState: {_behaviour.GetErrorMessage()} ");

            this.NextState();
        }

        public void NextState()
        {
            _pipeline.SetState(new UtilityState(_pipeline));
        }
    }
}
