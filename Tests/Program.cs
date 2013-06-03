using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            FuzzyRulesTest fuzzyRulesTest = new FuzzyRulesTest();

            Console.WriteLine("Fuzzy Simple Rules Test");
            Console.WriteLine("______________________________");
            Console.WriteLine("\nExperiment 1");
            fuzzyRulesTest.Experiment1();

            Console.WriteLine("\nExperiment 2");
            fuzzyRulesTest.Experiment2();

            Console.WriteLine("\nExperiment 3");
            fuzzyRulesTest.Experiment3();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            BackPropagationTest backPropagationTest = new BackPropagationTest();

            Console.WriteLine("Fuzzy Simple Rules Test");
            Console.WriteLine("______________________________");
            Console.WriteLine("\nExperiment 1");
            backPropagationTest.Experiment1();

            Console.WriteLine("\nExperiment 2");
            backPropagationTest.Experiment2();

            Console.WriteLine("\nExperiment 3");
            backPropagationTest.Experiment3();

            Console.WriteLine("\nPress any key to end tests...");
            Console.ReadKey();

        }
    }
}
