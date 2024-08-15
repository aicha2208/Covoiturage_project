using System;
using System.Collections.Generic;

namespace GESTIONCOVOITURES.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int TrajetId { get; set; }
    public Trajet Trajet { get; set; }

    public int PassagerId { get; set; }
    public Utilisateur Passager { get; set; }

    public DateTime DateReservation { get; set; }
}
