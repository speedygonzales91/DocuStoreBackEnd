using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces.ModelExtensions
{
    public interface ITrackDelete
    {
        int DeletedBy { get; set; }

        DateTime? DeletedAt { get; set; }

        bool IsDeleted { get; set; }
    }
}
