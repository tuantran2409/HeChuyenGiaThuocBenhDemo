using HeChuyenGiaThuocBenh.DAL.Repositories;
using HeChuyenGiaThuocBenh.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HeChuyenGiaThuocBenh.BLL.Services;

public class BaoCaoService : IBaoCaoService
{
    private readonly ILichSuChanDoanRepository _lichSuRepo;

    public BaoCaoService(ILichSuChanDoanRepository lichSuRepo)
    {
        _lichSuRepo = lichSuRepo;
    }

    public async Task<IEnumerable<LichSuChanDoan>> GetThongKeAsync(DateTime from, DateTime to)
        => await _lichSuRepo.GetByDateRangeAsync(from.Date, to.Date.AddDays(1).AddTicks(-1));

    public Task XuatPDFAsync(DateTime from, DateTime to, IEnumerable<LichSuChanDoan> data, string outputPath)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var list = data.ToList();

        var byDay = list
            .GroupBy(r => r.NgayChanDoan.Date)
            .OrderBy(g => g.Key)
            .Select(g => (Date: g.Key, Count: g.Count()))
            .ToList();

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                page.Header()
                    .PaddingBottom(10)
                    .Column(col =>
                    {
                        col.Item().Text("HỆ CHUYÊN GIA THUỐC BỆNH")
                            .Bold().FontSize(18).FontColor(Colors.Blue.Darken2).AlignCenter();
                        col.Item().Text("BÁO CÁO THỐNG KÊ CHẨN ĐOÁN")
                            .FontSize(14).FontColor(Colors.Grey.Darken1).AlignCenter();
                        col.Item().PaddingTop(4).Text(
                            $"Từ ngày {from:dd/MM/yyyy} đến ngày {to:dd/MM/yyyy}")
                            .FontSize(11).AlignCenter();
                    });

                page.Content().Column(col =>
                {
                    col.Item().PaddingVertical(10).Row(row =>
                    {
                        row.RelativeItem().Border(1).BorderColor(Colors.Blue.Lighten3)
                            .Padding(10).Column(c =>
                            {
                                c.Item().Text("TỔNG QUAN").Bold().FontColor(Colors.Blue.Darken2);
                                c.Item().PaddingTop(4).Text($"Tổng số chẩn đoán: {list.Count}");
                                c.Item().Text($"Số ngày có dữ liệu: {byDay.Count}");
                                c.Item().Text($"Ngày nhiều nhất: {(byDay.Any() ? byDay.OrderByDescending(d => d.Count).First().Count : 0)} ca");
                            });
                    });

                    if (byDay.Any())
                    {
                        col.Item().PaddingBottom(6).Text("THỐNG KÊ THEO NGÀY").Bold().FontSize(12);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(40);
                                cols.RelativeColumn(2);
                                cols.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(4).Text("#").Bold();
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(4).Text("Ngày").Bold();
                                header.Cell().Background(Colors.Blue.Lighten3).Padding(4).Text("Số chẩn đoán").Bold();
                            });

                            for (int i = 0; i < byDay.Count; i++)
                            {
                                var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten5;
                                table.Cell().Background(bg).Padding(4).Text($"{i + 1}");
                                table.Cell().Background(bg).Padding(4).Text(byDay[i].Date.ToString("dd/MM/yyyy"));
                                table.Cell().Background(bg).Padding(4).Text($"{byDay[i].Count}");
                            }
                        });
                    }

                    col.Item().PaddingTop(14).PaddingBottom(6).Text("DANH SÁCH CHẨN ĐOÁN").Bold().FontSize(12);
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.ConstantColumn(30);
                            cols.RelativeColumn(2);
                            cols.ConstantColumn(60);
                            cols.RelativeColumn(3);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Blue.Lighten3).Padding(4).Text("#").Bold();
                            header.Cell().Background(Colors.Blue.Lighten3).Padding(4).Text("Ngày chẩn đoán").Bold();
                            header.Cell().Background(Colors.Blue.Lighten3).Padding(4).Text("BN #").Bold();
                            header.Cell().Background(Colors.Blue.Lighten3).Padding(4).Text("Kết quả").Bold();
                        });

                        for (int i = 0; i < list.Count; i++)
                        {
                            var item = list[i];
                            var bg = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten5;
                            table.Cell().Background(bg).Padding(3).Text($"{i + 1}").FontSize(10);
                            table.Cell().Background(bg).Padding(3).Text(item.NgayChanDoan.ToString("dd/MM/yyyy HH:mm")).FontSize(10);
                            table.Cell().Background(bg).Padding(3).Text($"{item.BenhNhanId}").FontSize(10);
                            table.Cell().Background(bg).Padding(3).Text(item.KetQuaBenh).FontSize(10);
                        }
                    });
                });

                page.Footer()
                    .AlignRight()
                    .Text(x =>
                    {
                        x.Span($"Xuất ngày {DateTime.Now:dd/MM/yyyy HH:mm}  |  Trang ");
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
            });
        }).GeneratePdf(outputPath);

        return Task.CompletedTask;
    }
}
