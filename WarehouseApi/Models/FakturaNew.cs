﻿using System;
using System.Collections.Generic;

namespace WarehouseApi.Models;

public partial class FakturaNew
{
    public int Id { get; set; }

    public int Lp { get; set; }

    public string Nazwa { get; set; } = null!;

    public double Ilosc { get; set; }

    public string Jm { get; set; } = null!;

    public double CeanaBrutto { get; set; }

    public string StawkaVat { get; set; } = null!;

    public double WartoscBrutto { get; set; }

    public double WartoscNetto { get; set; }

    public double WartoscVat { get; set; }

    public string NumerFv { get; set; } = null!;

    public string Gtu { get; set; } = null!;

    public string Kod { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string Kod1 { get; set; } = null!;
}
