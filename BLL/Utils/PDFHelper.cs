using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

namespace BLL.Utils
{
    public class PDFHelper
    {
        public static byte[] GenerateAppointmentToken(string patientName, string doctorName, DateTime date, string appointmentId)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var pdf = Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);

                    page.Content().Column(column =>
                    {
                        column.Item().AlignCenter().Column(c =>
                        {
                            c.Item().Text("Shashto Clinic").FontSize(24).Bold();
                            c.Item().Text("Dhaka, Bangladesh").FontSize(12);
                            c.Item().Text("123 Health Street, Dhaka 1000").FontSize(10).Italic();
                        });

                        column.Item().PaddingVertical(20);

                        column.Item().Border(1).BorderColor(Colors.Black).Padding(10).AlignCenter()
                              .Text($"Appointment ID: {appointmentId}")
                              .FontSize(14)
                              .Bold();

                        column.Item().PaddingVertical(15);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text("Patient Name").SemiBold();
                                c.Item().Text(patientName).FontSize(14);
                            });

                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text("Doctor Name").SemiBold();
                                c.Item().Text(doctorName).FontSize(14);
                            });
                        });

                        column.Item().PaddingVertical(15);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Border(1).BorderColor(Colors.Black).Padding(10).AlignCenter()
                               .Column(c =>
                               {
                                   c.Item().Text("Date").SemiBold();
                                   c.Item().Text(date.ToString("dddd, MMM dd yyyy")).FontSize(13);
                               });

                            row.RelativeItem().Border(1).BorderColor(Colors.Black).Padding(10).AlignCenter()
                               .Column(c =>
                               {
                                   c.Item().Text("Time").SemiBold();
                                   c.Item().Text(date.ToString("hh:mm tt")).FontSize(13);
                               });
                        });

                        column.Item().PaddingTop(20);

                        column.Item().AlignCenter().Text("Please arrive 10 minutes early for your appointment.").FontSize(10).Italic();
                    });
                });
            });

            using (var ms = new MemoryStream())
            {
                pdf.GeneratePdf(ms);
                return ms.ToArray();
            }
        }
    }
}
