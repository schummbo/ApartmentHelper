using System;
using System.Collections.Generic;
using System.Linq;
using CraigslistHelper.Core.Entities;
using CraigslistHelper.Core.Evaluators;
using Microsoft.Extensions.Configuration;

namespace CraigslistHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = new Settings();
            config.Bind(settings);

            new CraigslistHelperRunner(settings, CreateEvaluators(settings.Evaluators, settings)).Run();
        }

        private static List<BaseEvaluator> CreateEvaluators(EvaluatorDefinition[] evaluatorDefinitions, Settings settings)
        {
            List<BaseEvaluator> evals = new List<BaseEvaluator>();

            foreach (var evaluatorDefinition in evaluatorDefinitions.Where(e => !e.Disabled))
            {
                Type type = Type.GetType($"CraigslistHelper.Core.Evaluators.{evaluatorDefinition.Type}, CraigslistHelper.Core");

                object[] args = { evaluatorDefinition.PerfectRange, evaluatorDefinition.AcceptableRange, settings };

                var instance = (BaseEvaluator)Activator.CreateInstance(type, args);
                
                evals.Add(instance);
            }

            return evals;
        }
    }
}
