using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCRS.Shared.Objects.CitationResolution;

namespace TCRS.Client.Pages
{
    public class CitationResolutionBase : ComponentBase
    {
        protected CitationResolutionData CitationData { get; set; } = new CitationResolutionData();
        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            fillTable();
            EditContext = new EditContext(CitationData);
        }
        private void OpenPayDialog()
        {
            //DialogService.Show<PayDialog>("Fine Payment");
        }

        private void OpenCourseEnrollmentDialog()
        {
            //DialogService.Show<CourseEnrollmentDialog>("Course Enrollment");
        }

        public class ShowPayment
        {
            public bool paymentRequired { get; set; }
        }

        public class ShowScheduleTraining
        {
            public bool trainingAvailable { get; set; }
        }

        public class Citation
        {
            public int citationid { get; set; }
            public string status { get; set; }
            public DateTime dateIssued { get; set; }
            public DateTime dateDue { get; set; }
            public double fineAmount { get; set; }
            public bool trainingAvailable { get; set; }
            public bool paymentRequired { get; set; }

        }
        private static IEnumerable<Citation> Citations { get; set; }

        private Random gen = new Random();
        private DateTime randomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private void fillTable()
        {
            IList<Citation> citations = new List<Citation>();
            citations.Add(new Citation
            {
                citationid = 10,
                status = "Processing",
                dateIssued = randomDay(),
                dateDue = randomDay(),
                fineAmount = 70.90,
                trainingAvailable = true,
                paymentRequired = true
            });

            citations.Add(new Citation
            {
                citationid = 1,
                status = "Resolved",
                dateIssued = randomDay(),
                dateDue = randomDay(),
                fineAmount = 37.31,
                trainingAvailable = false,
                paymentRequired = false
            });

            citations.Add(new Citation
            {
                citationid = 16,
                status = "Processing",
                dateIssued = randomDay(),
                dateDue = randomDay(),
                fineAmount = 63.92,
                trainingAvailable = true,
                paymentRequired = true
            });

            citations.Add(new Citation
            {
                citationid = 29,
                status = "BAD",
                dateIssued = randomDay(),
                dateDue = randomDay(),
                fineAmount = 8.90,
                trainingAvailable = true,
                paymentRequired = true
            });

            Citations = citations;
        }

        private void ShowBtnPress(String choice)
        {
            if (choice.Equals("Payment"))
            {

            }
            else
            {

            }
        }
    }
}
