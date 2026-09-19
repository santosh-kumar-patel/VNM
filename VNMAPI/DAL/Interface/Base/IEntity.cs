using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Interface.Base
{
    public interface IEntity
    {
        int ID { get; set; }
    }
}
