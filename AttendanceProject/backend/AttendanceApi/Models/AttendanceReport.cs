using AttendanceApi.Models.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AttendanceApi.Models;

public class AttendanceReport : IDocument
{
    private readonly AttendanceReportDTO _attendanceReportDTO;


    public AttendanceReport(AttendanceReportDTO attendanceReportDTO)
    {
        _attendanceReportDTO = attendanceReportDTO;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(40);
            page.Size(PageSizes.A4);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Header().Text($"Session Attendance Report").SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

            page.Content().Column(col =>
            {
                col.Item().Text($"Session Name: {_attendanceReportDTO.SessionName}").Bold();
                col.Item().Text($"Session Date: {_attendanceReportDTO.Date:dd MMM yyyy}").Bold();
                col.Item().Text($"Session Timings: {_attendanceReportDTO.StartTime}-{_attendanceReportDTO.EndTime}").Bold();
                col.Item().Text($"Total Registered: {_attendanceReportDTO.RegisteredCount}").Bold();
                col.Item().Text($"Total Attended: {_attendanceReportDTO.AttendedCount}").Bold();

                col.Item().PaddingVertical(10).Text("Attendee Details:").Bold().FontSize(14);

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Name
                        columns.RelativeColumn(); // Email
                        columns.ConstantColumn(80); // Attended
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Name").Bold();
                        header.Cell().Element(CellStyle).Text("Email").Bold();
                        header.Cell().Element(CellStyle).Text("Attended").Bold();
                    });

                    foreach (var attendee in _attendanceReportDTO.SessionAttendance)
                    {
                        table.Cell().Element(CellStyle).Text(attendee.StudentName);
                        table.Cell().Element(CellStyle).Text(attendee.Email);
                        table.Cell().Element(CellStyle).Text(attendee.Attended ? "Yes" : "No");
                    }

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .PaddingVertical(5)
                            .PaddingHorizontal(10)
                            .BorderBottom(1)
                            .BorderColor(Colors.Grey.Lighten2);
                    }
                });
            });

            page.Footer().AlignCenter().Text(text =>
            {
                text.Span("Generated on ").FontSize(10).FontColor(Colors.Grey.Medium);
                text.Span($"{DateTime.Now:dd MMM yyyy HH:mm}").SemiBold().FontSize(10).FontColor(Colors.Grey.Medium);
            });
        });
    }
}