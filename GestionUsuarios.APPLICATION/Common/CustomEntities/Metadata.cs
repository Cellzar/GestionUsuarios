using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.APPLICATION.Common.CustomEntities;

public class Metadata
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public bool HasNextPage { get; set; }

    public bool HasPreviousPage { get; set; }

    public string? NexPageUrl { get; set; }

    public string? PreviousPageUrl { get; set; }

    public Metadata()
    {
    }
}
