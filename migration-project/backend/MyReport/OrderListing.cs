using Backend.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Backend.MyReport;

public class OrderListing : IDocument
{
    private readonly List<Order> _orders;

    public OrderListing(List<Order> orders)
    {
        _orders = orders;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(20);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Content().Column(column =>
            {
                column.Spacing(15);

                column.Item().Text("Order Listing").Bold().FontSize(16).AlignCenter();

                column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                table.Cell().Text("Order ID").Bold();
                table.Cell().Text("Payment Type").Bold();
                table.Cell().Text("Status").Bold();
                table.Cell().Text("Customer Name").Bold();
                table.Cell().Text("Customer Phone").Bold();

                foreach (var order in _orders)
                {
                    table.Cell().Text(order.OrderID.ToString());
                    table.Cell().Text(order.PaymentType);
                    table.Cell().Text(order.Status);
                    table.Cell().Text(order.CustomerName);
                    table.Cell().Text(order.CustomerPhone);
                }
            });
            });
        });
    }
}