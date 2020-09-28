using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces.ModelExtensions
{
    public interface ITrackCreate
    {
        int CreatedBy { get; set; }

        DateTime? CreatedAt { get; set; }
    }
}
