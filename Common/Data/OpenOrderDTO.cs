﻿using System;
using Enums;

namespace Common.Data
{
    public class OpenOrderDTO
    {
        public string Uuid{get;set;}
        public string OrderUuid{get;set;}
        public string Exchange{get;set;}
        public OrderType OrderType{get;set;}
        public decimal Quantity{get;set;}
        public decimal QuantityRemaining{get;set;}
        public decimal Limit{get;set;}
        public decimal CommissionPaid{get;set;}
        public decimal Price{get;set;}
        //public decimal? PricePerUnit{get;set;}
        public DateTime Opened{get;set;}
        //public string Closed" : null,
        public bool CancelInitiated{get;set;}
        public bool ImmediateOrCancel{get;set;}
        public bool IsConditional{get;set;}
        public string Condition{get;set;}
        public string ConditionTarget { get; set; }
    }
}