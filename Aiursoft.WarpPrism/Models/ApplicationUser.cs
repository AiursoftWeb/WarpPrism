using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aiursoft.WarpPrism.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    }

    public enum PropertyType : int
    {
        @int = 1,
        @varchar= 2
    }

    public class DataBase
    {
        public int DataBaseId { get; set; }
        public string DataBaseName { get; set; }
        public DateTime CreateTime { get; set; }

        [InverseProperty(nameof(Table.Context))]
        public IEnumerable<Table> Tables { get; set; }
    }

    public class Table
    {
        public int TableId { get; set; }
        public string TableName { get; set; }

        [InverseProperty(nameof(Property.Context))]
        public IEnumerable<Property> Properties { get; set; }
        [InverseProperty(nameof(Item.Context))]
        public IEnumerable<Item> Items { get; set; }

        public int DatabaseId { get; set; }
        [ForeignKey(nameof(DatabaseId))]
        public DataBase Context { get; set; }
    }

    public class Property
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public PropertyType Type { get; set; }
        [InverseProperty(nameof(Value.PropertyContext))]
        public IEnumerable<Value> Values { get; set; }

        public int TableId { get; set; }
        [ForeignKey(nameof(TableId))]
        public Table Context { get; set; }

        [NotMapped]
        public string CurrentValue { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        [InverseProperty(nameof(Value.ItemContext))]
        public IEnumerable<Value> Values { get; set; }

        public int TableId { get; set; }
        [ForeignKey(nameof(TableId))]
        public Table Context { get; set; }
    }

    public class Value
    {
        public int ValueId { get; set; }
        public string RealValue { get; set; }

        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Item ItemContext { get; set; }

        public int? PropertyId { get; set; }
        [ForeignKey(nameof(PropertyId))]
        public virtual Property PropertyContext { get; set; }
    }
}
