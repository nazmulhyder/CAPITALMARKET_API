using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.MarginRequest
{
    public class CodesMarginRequestDto
    {
        public int? ID
        {
            get; set;
        }
        public string? typeValue
        {
            get; set;
        }
        public int? Score
        {
            get; set;
        }
        
    }

    public class CodesMarginRequestLoanRatioDto
    {
        public int? ID
        {
            get; set;
        }
        public string? typeValue
        {
            get; set;
        }
        public string? Score
        {
            get; set;
        }

    }

    public class AllCodesMarginDto
    {
        public List<CodesMarginRequestDto>? CodesMrReqEducation { get; set; }
        public List<CodesMarginRequestDto>? CodesMrStockMktExp { get; set; }
        public List<CodesMarginRequestDto>? CodesMrReqProfession { get; set; }
        public List<CodesMarginRequestDto>? CodesMrPerceptionNetWorth { get; set; }
        public List<CodesMarginRequestLoanRatioDto>? CodesScoreOutOfTwnty { get; set; }

        public string? Education { get; set; }
        public string? TargetNetWorth { get; set; }
        public string? SMExperiance { get; set; }
        public string? Profession { get; set; }

        public int? eduScore { get; set; }
        public int? smEScore { get; set; }
        public int? professionScore { get; set; }
        public int? netWorthScore { get; set; }
        public int? TotalScore { get; set; }

        public string? staticTitle1 = "*An individual's score will be 1 point less if the employing company is operating for less than 5 years or has an annual turnover below 50 cr";
        public string? staticTitle2 = "*An individual's score will be 1 point less if s/he is working for less than 3 years";


    }

    public class CodesMarginJson
    {
        public string? Category { get; set; }
        public string? Details { get; set; }
        public string? Score { get; set; }
        public string? Mark { get; set; }
        public string? Remark { get; set; }
        public string? TotalScore { get; set; }
        public int? RowSpan { get; set; }
    }
}
