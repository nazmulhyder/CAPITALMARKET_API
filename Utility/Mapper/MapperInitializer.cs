using AutoMapper;
using Model.DTOs.Allocation;
using Model.DTOs.BrokerageCommision;
using Model.DTOs.TradingPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Mapper
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<SLTradeSettlementRuleDTO, SLTradeSettlementRuleEditDTO>().ReverseMap();
            //CreateMap<UpdateBrokerageCommissionAMLDetailListDto, BrokerageCommissionAMLDetailListDto>().ReverseMap();
            CreateMap<AccountSaleOrderAllocationDto, AccountSaleOrderAllocation_v2_Dto>().ReverseMap();
            CreateMap<AccountSaleOrderAllocation_v2_Dto, AccountSaleOrderAllocationDto>().ReverseMap();

            CreateMap<AccountBuyOrderAllocationDto, AccountBuyOrderAllocation_v2_Dto>().ReverseMap();
            CreateMap<AccountBuyOrderAllocation_v2_Dto, AccountBuyOrderAllocationDto>().ReverseMap();
        }
    }
}
