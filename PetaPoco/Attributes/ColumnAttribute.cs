// PetaPoco - A Tiny ORMish thing for your POCO's.
// Copyright © 2011-2012 Topten Software.  All Rights Reserved.
 
using System;

namespace PetaPoco
{
	/// <summary>
	/// For explicit poco properties, marks the property as a column and optionally 
	/// supplies the DB column name.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ColumnAttribute : Attribute
	{
		public ColumnAttribute() 
		{
			ForceToUtc = false;
		}
		
		public ColumnAttribute(string Name) 
		{ 
			this.Name = Name;
			ForceToUtc = false;
		}

		public string Name 
		{ 
			get; 
			set; 
		}

		public bool ForceToUtc
		{
			get;
			set;
		}
	}

	// 给列填充中文备份 
	[AttributeUsage(AttributeTargets.Property)]
	public class CommentsAttribute : Attribute
	{
		public CommentsAttribute(string comments) { Comments = comments; }
		public string Comments { get; set; }
	}

	// 列附加信息 
	[AttributeUsage(AttributeTargets.Property)]
	public class ColumnAddtionInfoAttribute : Attribute
	{
        public ColumnAddtionInfoAttribute() 
        { 
        }

        public ColumnAddtionInfoAttribute(string dataType, string length, string scale, string precision)
        {
            this.DataType = dataType; 
            this.Length = length; 
            this.Scale = scale;
            this.Precision = precision;
        }

		public string DataType { get; set; }
		public string Length { get; set; }
		public string Scale { get; set; }
        public string Precision { get; set; }
	}
}
