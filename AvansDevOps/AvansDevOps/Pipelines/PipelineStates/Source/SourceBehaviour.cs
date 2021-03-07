﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates.Source
{
    public class SourceBehaviour : ISourceStateBehaviour
    {
        private readonly IPipeline _pipeline;
        private string _errors;

        public SourceBehaviour(IPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        public bool Execute()
        {
            // Stub execution step
            Console.WriteLine("Source pipeline step run");
            return true;
        }

        public string GetErrorMessage()
        {
            return _errors;
        }
    }
}
