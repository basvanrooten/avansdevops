using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansDevOps.Sprints;

namespace AvansDevOps.Reports.ReportBuilders
{
    public class AvansPlusReportBuilder: IReportBuilder
    {
        private readonly Report _report;

        public AvansPlusReportBuilder()
        {
            this._report = new Report();
        }

        public void BuildFooter()
        {
            this._report.Footer = new Footer()
                { CompanyColor = "#d1ecbb", CompanyName = "Avans+", CompanyWebsite = "https://avansplus.nl" };
        }

        public void BuildHeader(ISprint sprint, string reportVersion, DateTime date)
        {
            this._report.Header = new Header()
            {
                CompanyColor = "#d1ecbb",
                CompanyName = "Avans+",
                CompanyLogoURL = "https://avansplus.nl/logo.png",
                Date = date,
                ReportVersion = reportVersion,
                SprintName = sprint.GetName()
            };
        }

        public void BuildContent(List<string> contents)
        {
            this._report.Contents = contents;
        }

        public Report GetReport(EReportFormat format)
        {
            this._report.Format = format;
            return this._report;
        }
    }
}
