﻿using MtChangeLog.DataObjects.Entities.Editable;
using MtChangeLog.DataObjects.Entities.Views.Shorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface IAuthorsRepository : ICRUD<AuthorEditable>
    {
        IEnumerable<AuthorShortView> GetShortEntities();
        IEnumerable<AuthorEditable> GetTableEntities();
        AuthorEditable GetTemplate();
    }
}
