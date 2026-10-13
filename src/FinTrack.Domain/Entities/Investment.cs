using System.Formats.Tar;
using System.Security.Cryptography.X509Certificates;
using FinTrack.Domain.Common;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Enums;

namespace FinTrack.Domain.Entities;

public class Investment : BaseEntity
{
    public const decimal MinimumAmount = 500m;
    public const decimal StampDutyRate = 0.00005m; // 0.005% stamp duty on MF purchases

    public Guid UserId { get; private set; }
    public Guid FundId { get; private set; }
    public InvestmentType Type { get; private set; }
    public decimal Amount { get; private set; }
    public decimal StampDuty { get; private set; }
    public decimal NavAtPurchase { get; private set; }
    public decimal Units { get; private set; }
    public DateOnly InvestedOn { get; private set; }

    private Investment(){}

    public Investment(Guid userId, Guid fundId, InvestmentType type,
                      decimal amount, decimal nav, DateOnly investedOn)
    {
        if(userId==Guid.Empty)
        throw new ArgumentException("User id requried", nameof(userId));

        if (fundId ==Guid.Empty)
        throw new ArgumentException("Funds are requried", nameof(fundId));

        if(amount<MinimumAmount)
        throw new ArgumentException("Amounst should be greater then", nameof(amount));

        if(nav<=0)
        throw new  ArgumentException("NAV should be gteratae then 0",nameof(nav));

        var todatIst  =DateOnly.FromDateTime(DateTime.UtcNow.AddHours(5.5));

        if(InvestedOn>todatIst)
        throw new ArgumentException("Date shyould be greater",nameof(investedOn));


        UserId=userId;
        FundId=fundId;
        Type = type;
        Amount=amount;
        NavAtPurchase = nav;
        InvestedOn=investedOn;

        StampDuty = Math.Round(amount * StampDutyRate,2);
        Units= Math.Round((amount-StampDuty)/nav,3,MidpointRounding.ToZero);

  

    }
  public decimal CurrentValue(decimal CurrentNav) => Math.Round(Units*CurrentNav ,2);

    public decimal GainOrLoss(decimal CurrentNav) => CurrentValue(CurrentNav)-Amount;


}