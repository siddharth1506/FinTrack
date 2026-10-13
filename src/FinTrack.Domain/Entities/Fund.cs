using  FinTrack.Domain.Common;

namespace FinTrack.Domain.Entities;

public class Fund : BaseEntity
{
    public int SchemeCode {get; private set;}
    public string  SchemeName {get; private set;} = string.Empty;

    public string FundHouse {get ; private set;} = string.Empty;

    public string Category  {get; private set;} = string.Empty;
    public decimal CurrentNav {get; private set;}
    public DateOnly NavDate {get; private set;}

  private Fund()
    {   }
    
    public Fund(int schemeCode, string schemeName, string fundHouse,string category)
    {
        if(schemeCode<=0)
        throw new ArgumentException("Scheme code must be positive", nameof(schemeCode));
        if (string.IsNullOrWhiteSpace(schemeName))
        throw new ArgumentException("Scheme nameis required",nameof(schemeName));

        SchemeCode = schemeCode;
        SchemeName = schemeName;
        FundHouse= fundHouse;
        Category = category;
    }
    public void UpdateNav(decimal nav,DateOnly navDate)
    {
        if(nav<=0)
        throw new ArgumentException("NAV must be greater than Zero.",nameof(nav));
        if(navDate<NavDate)
        return;

        CurrentNav =nav;
        NavDate=navDate;
        MarkUpdated();
    }
    
}

