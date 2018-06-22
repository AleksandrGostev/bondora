using System;
using Bondora.Entities;
using Bondora.Enums;
using Bondora.Helpers;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Bondora.Tests
{
    public class EquipmentTests : IClassFixture<ConfigFixture>
    {
		public decimal OneTimeFee = decimal.Parse(Config.AppSettings["Fees:OneTime"]);
	    public decimal PremiumFee = decimal.Parse(Config.AppSettings["Fees:Premium"]);
	    public decimal RegularFee = decimal.Parse(Config.AppSettings["Fees:Regular"]);
		public int RegularPremiumDays = int.Parse(Config.AppSettings["PremiumDays:Regular"]);
	    public int SpecializedPremiumDays = int.Parse(Config.AppSettings["PremiumDays:Specialized"]);
	    public const int RentalDays = 4;

		[Fact]
        public void TestHeavyPrice()
        {
	        var heavyEquipment = new Equipment(EquipmentType.Heavy, "HeavyName");
	        
	        var equipmentRentalPrice = heavyEquipment.CalculatePrice(RentalDays);
			Assert.Equal(OneTimeFee + (PremiumFee * RentalDays), equipmentRentalPrice);
        }

		[Fact]
	    public void TestRegularPrice()
	    {
			var regularEquipment = new Equipment(EquipmentType.Regular, "RegularName");
		    var equipmentRentalPrice = regularEquipment.CalculatePrice(RentalDays);
		    var regularDays = RentalDays - RegularPremiumDays;
			Assert.Equal(OneTimeFee + (PremiumFee * RegularPremiumDays) + (RegularFee * regularDays), equipmentRentalPrice);
	    }

	    [Fact]
	    public void TestSpecializedPrice()
	    {
		    var specializedEquipment = new Equipment(EquipmentType.Specialized, "SpecializedName");
		    var equipmentRentalPrice = specializedEquipment.CalculatePrice(RentalDays);
		    var regularDays = RentalDays - SpecializedPremiumDays;
		    Assert.Equal(PremiumFee * SpecializedPremiumDays + RegularFee * regularDays, equipmentRentalPrice);
	    }
	}
}
