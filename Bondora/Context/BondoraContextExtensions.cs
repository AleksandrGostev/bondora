using System.Collections.Generic;
using Bondora.Entities;
using Bondora.Enums;

namespace Bondora.Context
{
    public static class BondoraContextExtensions
    {
	    public static void EnsureSeedDataForContext(this BondoraContext context)
	    {
		    // first, clear the database.  This ensures we can always start 
		    // fresh with each startup.
			context.Equipments.RemoveRange(context.Equipments);
		    context.Orders.RemoveRange(context.Orders);
			context.Rentals.RemoveRange(context.Rentals);

		    context.SaveChanges();

			// init seed data
		    var equipments = new List<Equipment>()
		    {
			    new Equipment(EquipmentType.Heavy, "Caterpillar bulldozer"),
				new Equipment(EquipmentType.Regular, "KamAZ truck"),
				new Equipment(EquipmentType.Heavy, "Komatsu crane"),
				new Equipment(EquipmentType.Regular, "Volvo steamroller"),
				new Equipment(EquipmentType.Specialized, "Bosch jackhammer")
		    };

		    context.Equipments.AddRange(equipments);
		    context.SaveChanges();
	    }
	}
}
