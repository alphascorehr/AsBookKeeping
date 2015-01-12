using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using DalEf;
using System.Collections.Generic;

namespace BusinessObjects.MDEntities
{
	[Serializable]
	public partial class cMDEntities_Product_Pictures: CoreBusinessChildClass<cMDEntities_Product_Pictures>
	{
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > productIdProperty = RegisterProperty<System.Int32>(p => p.ProductId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 ProductId
		{
			get { return GetProperty(productIdProperty); }
			set { SetProperty(productIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Byte[] > pictureProperty = RegisterProperty<System.Byte[]>(p => p.Picture, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Byte[] Picture
		{
			get { return GetProperty(pictureProperty); }
			set { SetProperty(pictureProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cMDEntities_Product_Pictures NewMDEntities_Product_Pictures()
		{
			return DataPortal.CreateChild<cMDEntities_Product_Pictures>();
		}

        public static cMDEntities_Product_Pictures GetcMDEntities_Product_Pictures(MDEntities_Product_PicturesCol data)
        {
            return DataPortal.FetchChild<cMDEntities_Product_Pictures>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(MDEntities_Product_PicturesCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(productIdProperty, data.ProductId);
            LoadProperty<Byte[]>(pictureProperty, data.Picture);
            
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(MDEntities_Product parent)
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Product_PicturesCol();

                data.MDEntities_Product = parent;

                data.Picture = ReadProperty<Byte[]>(pictureProperty);
              
                ctx.ObjectContext.AddToMDEntities_Product_PicturesCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(productIdProperty, data.ProductId);
                        LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                        LastChanged = data.LastChanged;
                    }
                };
            }
        }

        private void Child_Update()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Product_PicturesCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Picture = ReadProperty<Byte[]>(pictureProperty);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "LastChanged")
                        LastChanged = data.LastChanged;
                };

            }
        }

        private void Child_DeleteSelf()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Product_PicturesCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
            }
        }
        #endregion
        public void MarkChild()
        {
            this.MarkAsChild();
        }
    }
    [Serializable]
    public partial class cMDEntities_Product_PicturesCol : BusinessListBase<cMDEntities_Product_PicturesCol, cMDEntities_Product_Pictures>
    {
        internal static cMDEntities_Product_PicturesCol NewMDEntities_Product_PicturesCol()
        {
            return DataPortal.CreateChild<cMDEntities_Product_PicturesCol>();
        }

        public static cMDEntities_Product_PicturesCol GetcMDEntities_Product_PicturesCol(IEnumerable<MDEntities_Product_PicturesCol> dataSet)
        {
            var childList = new cMDEntities_Product_PicturesCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<MDEntities_Product_PicturesCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cMDEntities_Product_Pictures.GetcMDEntities_Product_Pictures(data));

            RaiseListChangedEvents = true;


        }

        #endregion //Data Access
    }
}

