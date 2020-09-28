using System;
using System.Collections.Generic;
using System.Text;

namespace DocuStore.DAL.Interfaces.ModelExtensions
{
    public interface ITrackUpdate
    {
        int ModifiedBy { get; set; }

        DateTime? ModifiedAt { get; set; }
    }
}
