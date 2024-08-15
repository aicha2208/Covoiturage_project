using System;
using System.Collections.Generic;

namespace GESTIONCOVOITURES.Models;

public partial class Trajet
{
    public int Id { get; set; }

    public string VilleDepart { get; set; } = null!;

    public string VilleArrivee { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly Heure { get; set; }

    public long Nombreplace { get; set; }

    public long Nombreplacereserve { get; set; }

    public long Montant { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
