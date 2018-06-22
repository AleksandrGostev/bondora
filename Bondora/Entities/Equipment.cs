using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bondora.Enums;
using Bondora.Helpers;
using Serilog;

namespace Bondora.Entities
{
	public class Equipment
	{
		public Equipment()
		{
			OneTimeFee = int.Parse(Config.AppSettings["Fees:OneTime"]);
			PremiumFee = int.Parse(Config.AppSettings["Fees:Premium"]);
			RegularFee = int.Parse(Config.AppSettings["Fees:Regular"]);
		}

		public Equipment(EquipmentType type, string title) : this()
		{
			Title = title;
			Type = type;
			int.TryParse(Config.AppSettings["BonusRates:Default"], out var configBonusRate);

			if (Type == EquipmentType.Heavy)
			{
				int.TryParse(Config.AppSettings["BonusRates:Heavy"], out configBonusRate);
			}

			BonusRate = configBonusRate;

			var configPremiumDays = 0;
			switch (Type)
			{
				case EquipmentType.Regular:
					int.TryParse(Config.AppSettings["PremiumDays:Regular"], out configPremiumDays);
					break;
				case EquipmentType.Specialized:
					int.TryParse(Config.AppSettings["PremiumDays:Specialized"], out configPremiumDays);
					break;
			}

			PremiumDays = configPremiumDays;
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid EquipmentId { get; set; }

		[Required]
		public string Title { get; set; }
		[Required]
		public EquipmentType Type { get; set; }

		// Fees for equipment are set on it's creation
		// Other way price calculation on invoice could be wrong
		public decimal OneTimeFee { get; set; }
		public decimal PremiumFee { get; set; }
		public decimal RegularFee { get; set; }

		public int PremiumDays { get; set; }
		public int BonusRate { get; set; }

		public ICollection<Rental> Rentals { get; set; }

		public decimal CalculatePrice(int rentalDays)
		{
			switch (Type)
			{
				case EquipmentType.Regular:
					return OneTimeFee + TypedFee(rentalDays);
				case EquipmentType.Heavy:
					return OneTimeFee + PremiumFee * rentalDays;
				case EquipmentType.Specialized:
					return TypedFee(rentalDays);
				default:
					var err = new ArgumentOutOfRangeException("Wrong equipment type");
					Log.Error(err, err.Message);
					throw err;
			}
		}

		private decimal TypedFee(int rentalDays)
		{
			if (rentalDays <= PremiumDays)
			{
				return PremiumFee * rentalDays;
			}

			var regularFeeDays = rentalDays - PremiumDays;
			var premiumDaysFee = PremiumDays * PremiumFee;
			return premiumDaysFee + RegularFee * regularFeeDays;
		}
	}
}
