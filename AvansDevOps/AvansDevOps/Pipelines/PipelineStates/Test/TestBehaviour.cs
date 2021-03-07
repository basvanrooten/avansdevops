﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Pipelines.PipelineStates.Test
{
    public class TestBehaviour : ITestStateBehaviour
    {
        private readonly IPipeline _pipeline;
        private string _errors;

        public TestBehaviour(IPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        public bool Execute()
        {
            // Stub execution step
            Console.WriteLine("Test pipeline step run");
            return true;
        }

        public string GetErrorMessage()
        {
            return _errors;
        }
    }
}
