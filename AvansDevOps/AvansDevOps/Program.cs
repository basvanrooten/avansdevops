using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Persons;
using AvansDevOps.Sprints;

namespace AvansDevOps
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Bas");
            Person p2 = new Person("Tom");
            Project project = new Project("Test Project", p1);

            project.AddSprint(new ReviewSprint("Review Sprint 1", DateTime.Now, DateTime.Now.AddDays(1), project, p1, new List<Person>() { p2 }));
            ISprint sprint = project.GetSprints().First();
            Console.WriteLine(sprint.GetName());
            sprint.GetState().SetName("Changed Name");
            sprint.GetState().startStateAction();

            Console.WriteLine(sprint.GetName());
            Console.ReadLine();

        }

        public static string HelloWorld()
        {
            return "Hello World";
        }
    }
}
