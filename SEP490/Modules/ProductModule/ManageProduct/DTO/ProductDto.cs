﻿public class ProductDto
{
    public int Id { get; set; }
    public string? ProductCode { get; set; }
    public string? ProductName { get; set; }
    public string? ProductType { get; set; }
    public string? UOM { get; set; }
    public string? Height { get; set; }
    public string? Width { get; set; }
    public decimal? Thickness { get; set; }
    public decimal? Weight { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? GlassStructureId { get; set; }
    public string GlassStructureProductName { get; set; } = string.Empty;
    public int TotalSoldQuantity { get; set; } // tổng số lượng đã bán

}
