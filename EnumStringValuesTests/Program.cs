using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using EnumStringValues;

namespace EnumStringValuesTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().CheckOutputsMatch();

            // It appears that v4.0.1 of EnumStringValues is DEBUG
            var config = DefaultConfig.Instance.With(ConfigOptions.DisableOptimizationsValidator);
            BenchmarkRunner.Run<Program>(config);
            Console.Out.WriteLine("OK2");
        }

        private void CheckOutputsMatch()
        {
            if (EnumerateEnumStdlib() != EnumerateEnumStdlib())
            {
                throw new Exception("Mismatch");
            }
            Console.Out.WriteLine("OK");
        }

        [Benchmark]
        public int EnumerateEnumStdlib()
        {
            int sum = 0;
            foreach (var value in Enum.GetValues(typeof(TestEnum)))
            {
                sum = 3 * sum + value.ToString().GetHashCode();
            }

            return sum;
        }

        [Benchmark]
        public int EnumerateEnumWithEnumStringValuesCaching()
        {
            int sum = 0;
            foreach (var value in EnumExtensions.EnumerateValues<TestEnum>())
            {
                sum = 3 * sum + value.ToString().GetHashCode();
            }

            return sum;
        }
    }
}