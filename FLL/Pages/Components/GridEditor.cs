using FLL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor;

namespace FLL.Pages.Components
{
    public class GridEditor<T> : object where T : class, new()
    {
        public enum ChangeType
        {
            Create,
            Update,
            Delete
        }

        ApplicationDbContext? db;
      
        public bool DisableAdd => ItemToInsert != null || ItemToUpdate != null;
        public RadzenDataGrid<T>? Grid { get; set; }

        public IQueryable<T>? Items { get; set; }

        public T? ItemToInsert { get; set; }

        public T? ItemToUpdate { get; set; }
        public Action<GridEditor<T>.ChangeType, T>? AfterChange { get; internal set; } = null;

        public void CancelEdit(T item)
        {
            if (Grid == null || db == null)
                return;
            if (item == ItemToInsert)
            {
                ItemToInsert = null;
            }

            ItemToUpdate = null;

            Grid.CancelEditRow(item);

            var itemEntry = db.Entry(item);
            if (itemEntry.State == EntityState.Modified)
            {
                itemEntry.CurrentValues.SetValues(itemEntry.OriginalValues);
                itemEntry.State = EntityState.Unchanged;
            }
        }

        public async Task DeleteRow(T item)
        {
            if (Grid == null || Items == null || db == null)
                return;

            if (item == ItemToInsert)
            {
                ItemToInsert = null;
            }

            if (item == ItemToUpdate)
            {
                ItemToUpdate = null;
            }

            if (Items.Contains(item))
            {
                db.Remove<T>(item);
                db.SaveChanges();
                AfterChange?.Invoke(ChangeType.Delete, item);
                await Grid.Reload();
            }
            else
            {
                Grid.CancelEditRow(item);
                await Grid.Reload();
            }
        }

        public async Task EditRow(T item)
        {
            if (Grid == null) return;
            ItemToUpdate = item;
            await Grid.EditRow(item);
        }

        public void Init(IQueryable<T> items, ApplicationDbContext db)
        {
            Items = items;
            this.db = db;
        }
        public async Task InsertRow()
        {
            if (Grid == null)
                return;

            ItemToInsert = new T();
            AfterChange?.Invoke(ChangeType.Create, ItemToInsert);
            await Grid.InsertRow(ItemToInsert);
        }

        public void OnCreateRow(T item)
        {
            if (db == null)
                return;
            db.Add(item);
            db.SaveChanges();
        }

        public void OnUpdateRow(T item)
        {
            if (db == null)
                return;
            if (item == ItemToInsert)
            {
                ItemToInsert = null;
            }

            ItemToUpdate = null;

            db.Update(item);
            AfterChange?.Invoke(ChangeType.Update, item);
            db.SaveChanges();
        }

        public void Reset()
        {
            ItemToInsert = null;
            ItemToUpdate = null;
        }
        public async Task SaveRow(T item)
        {
            if (Grid == null)
                return;

            await Grid.UpdateRow(item);
        }
    }
}
