﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ProjectHelpers;

namespace ServiceContracts.DTO
{
  public class BuyOrderRequest
  {
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? StockSymbol { get; set; }
    [Required(ErrorMessage = "{0} must be supplied")]
    public string? StockName { get; set; }
    [CorrectDateTimeValidation(2000, 1, 1, ErrorMessage = "{0} should be older than 2000.01.01")]
    public DateTime? DateAndTimeOfOrder { get; set; }
    [Range(1, 100000, ErrorMessage = "{0} should be between 1 and 100000")]
    public uint Quantity { get; set; }
    [Range(1, 10000, ErrorMessage = "{0} should be between 1 and 100000")]
    public double Price { get; set; }
  }
}
