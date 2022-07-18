using Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace _3_hafta.Entity.Concrete
{
    public class Folder : IEntity
    {
        [Key]
        public int FolderId { get; set; }
        public int EmpId { get; set; }
        public string AccessType { get; set; }
    }
}
