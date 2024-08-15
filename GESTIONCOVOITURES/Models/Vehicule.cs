using System;
using System.Collections.Generic;

namespace GESTIONCOVOITURES.Models;

public partial class Vehicule
{
    public int Id { get; set; }

    public string Marque { get; set; } = null!;

    public string Modele { get; set; } = null!;

    public string Immatriculation { get; set; } = null!;

    public int? ConducteurId { get; set; }

    public virtual Utilisateur? Conducteur { get; set; }
}
