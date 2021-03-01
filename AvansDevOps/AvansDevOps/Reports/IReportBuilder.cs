using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Sprints;

namespace AvansDevOps.Reports
{
    public interface IReportBuilder
    {
        void BuildFooter();
        void BuildHeader(ISprint sprint, string reportVersion, DateTime date);
        void BuildContent(List<string> contents);
        Report GetReport(EReportFormat format);
    }
}
