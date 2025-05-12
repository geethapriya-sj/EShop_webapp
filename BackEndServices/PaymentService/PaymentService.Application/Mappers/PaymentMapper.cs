using AutoMapper;
using PaymentService.Application.DTOs;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Mappers
{
    public class PaymentMapper: Profile
    {
        public PaymentMapper() {
            CreateMap<PaymentDetailDTO, PaymentDetail>().ReverseMap();
        }
    }
}
