using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bondora.Entities;
using Bondora.Enums;
using Bondora.Helpers;
using Xunit;

namespace Bondora.Tests
{
    public class OrderTests : IClassFixture<ConfigFixture>
    {
	    public decimal OneTimeFee = decimal.Parse(Config.AppSettings["Fees:OneTime"]);
	    public decimal PremiumFee = decimal.Parse(Config.AppSettings["Fees:Premium"]);
	    public decimal RegularFee = decimal.Parse(Config.AppSettings["Fees:Regular"]);
	    public int RegularPremiumDays = int.Parse(Config.AppSettings["PremiumDays:Regular"]);
	    public int SpecializedPremiumDays = int.Parse(Config.AppSettings["PremiumDays:Specialized"]);
	    public const int RentalDays = 4;

		[Fact]
	    public void TestOrderPrice()
	    {
		    var equipmentToRent = new List<Equipment>()
		    {
			    new Equipment(EquipmentType.Heavy, "HeavyName"),
			    new Equipment(EquipmentType.Regular, "RegularName"),
			    new Equipment(EquipmentType.Specialized, "SpecializedName")
			};

		    var rentals = equipmentToRent.Select(e => new Rental() {Equipment = e, Days = RentalDays});
			var order = new Order()
			{
				Rentals = rentals.ToList()
			};

		    var regularDays = RentalDays - RegularPremiumDays;
		    var specializedRegularDays = RentalDays - SpecializedPremiumDays;

			var expectedTotalPrice = 
			    OneTimeFee + (PremiumFee * RentalDays) +
			    OneTimeFee + (PremiumFee * RegularPremiumDays) + (RegularFee * regularDays) +
				PremiumFee* SpecializedPremiumDays + RegularFee * specializedRegularDays;
		    var expectedTotalBonus = equipmentToRent.Sum(x => x.BonusRate);
			
			Assert.Equal(expectedTotalPrice, order.TotalPrice);
			Assert.Equal(expectedTotalBonus, order.TotalBonus);
	    }
	}
}
