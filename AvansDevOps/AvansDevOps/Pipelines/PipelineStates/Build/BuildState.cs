﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Pipelines.PipelineStates.Test;

namespace AvansDevOps.Pipelines.PipelineStates.Build
{
    public class BuildState : IPipelineState
    {

        private readonly IPipeline _pipeline;
        private readonly IBuildStateBehaviour _behaviour;
        public BuildState(IPipeline pipeline)
        {
            _pipeline = pipeline;
            _behaviour = new BuildBehaviour(_pipeline);
        }

        public void Execute()
        {
            var pipelineResult = _behaviour.Execute();

            if (!pipelineResult || _behaviour.GetErrorMessage() != null)
                throw new Exception($"Pipeline failed: Build state: {_behaviour.GetErrorMessage()} ");

            this.NextState();
        }

        public void NextState()
        {
            _pipeline.SetState(new TestState(_pipeline));
        }
    }
}
