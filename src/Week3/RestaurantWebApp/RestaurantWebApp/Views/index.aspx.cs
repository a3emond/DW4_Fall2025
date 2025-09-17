using SimpleWebApp.Models;
using SimpleWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleWebApp.Views
{
    public partial class Index : Page
    {

        private readonly IMenuItemService _menuItemService = new MenuItemService();
        private readonly IMenuItemMediaService _mediaService = new MenuItemMediaService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeGuestSelection();
                LoadMenu();
            }
        }

        private void InitializeGuestSelection()
        {
            var guests = new List<GuestCart>
            {
                new GuestCart { GuestName = "You" },
                new GuestCart { GuestName = "Guest 1" }
            };

            if (Context.Session != null)
            {
                Session["GuestCarts"] = guests;
            }

            rptGuests.DataSource = guests;
            rptGuests.DataBind();
        }

        private void LoadMenu()
        {
            var items = _menuItemService.GetAll();
            var grouped = items
                .GroupBy(m => m.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Items = g.Select(i => new
                    {
                        i.Id,
                        i.Name,
                        i.Description,
                        i.Price,
                        ThumbnailUrl = ResolveMedia(i.Id)
                    }).ToList()
                })
                .ToList();
            
            rptCategories.DataSource = grouped;
            rptCategories.DataBind();
        }
        // asset folder mapping based on category
        private static readonly Dictionary<string, string> CategoryFolderMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Appetizer", "appetizers" },
            { "Main", "main_dishes" },
            { "Side", "side_dishes" },
            { "Dessert", "desserts" },
        };

        private string ResolveMedia(int menuItemId)
        {
            var item = _menuItemService.GetById(menuItemId);
            if (item == null) return null;

            // Skip Drinks and Liquors
            if (item.Category.Equals("Drink", StringComparison.OrdinalIgnoreCase) ||
                item.Category.Equals("Liquor", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            // Find primary media
            var media = _mediaService.GetAll()
                .FirstOrDefault(m => m.MenuItemId == menuItemId && m.IsPrimary);

            if (media == null) return null;

            // Map category to folder
            if (!CategoryFolderMap.TryGetValue(item.Category, out var folder))
                folder = "misc";

            return $"~/Assets/{folder}/{media.FileName}";
        }



        protected void btnAddGuest_Click(object sender, EventArgs e)
        {
            var guests = Session["GuestCarts"] as List<GuestCart> ?? new List<GuestCart>();
            guests.Add(new GuestCart { GuestName = $"Guest {guests.Count}" });
            Session["GuestCarts"] = guests;
            rptGuests.DataSource = guests;
            rptGuests.DataBind();
        }

        protected void btnRemoveGuest_Click(object sender, EventArgs e)
        {
            var guests = Session["GuestCarts"] as List<GuestCart>;
            if (guests != null && guests.Count > 1)
            {
                guests.RemoveAt(guests.Count - 1);
                Session["GuestCarts"] = guests;
                rptGuests.DataSource = guests;
                rptGuests.DataBind();
            }
        }

        protected void btnContinueToMenu_Click(object sender, EventArgs e)
        {
            var guests = new List<GuestCart>();
            foreach (RepeaterItem item in rptGuests.Items)
            {
                var txtName = item.FindControl("txtGuestName") as TextBox;
                guests.Add(new GuestCart { GuestName = txtName?.Text ?? $"Guest {item.ItemIndex + 1}" });
            }

            Session["GuestCarts"] = guests;
            pnlGuestSelection.Visible = false;
            pnlMenu.Visible = true;
        }

        protected void rptItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewItem")
            {
                int menuItemId = Convert.ToInt32(e.CommandArgument);
                var menuItem = _menuItemService.GetById(menuItemId);
                if (menuItem == null) return;

                lblModalTitle.Text = menuItem.Name;
                lblModalDescription.Text = menuItem.Description;

                // Resolve image and handle visibility
                var imageUrl = ResolveMedia(menuItem.Id);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    imgModal.Visible = false;
                }
                else
                {
                    imgModal.ImageUrl = imageUrl;
                    imgModal.Visible = true;
                }

                var guests = Session["GuestCarts"] as List<GuestCart> ?? new List<GuestCart>();
                var guestQuantities = guests.Select(g =>
                {
                    var item = g.Items.FirstOrDefault(i => i.MenuItemId == menuItem.Id);
                    return new
                    {
                        GuestName = g.GuestName,
                        Quantity = item?.Quantity ?? 0
                    };
                });

                rptGuestQuantities.DataSource = guestQuantities;
                rptGuestQuantities.DataBind();

                pnlItemModal.Visible = true;
            }
        }


        protected void rptGuestQuantities_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var guests = Session["GuestCarts"] as List<GuestCart>;
            if (guests == null) return;

            string guestName = e.CommandArgument.ToString();
            var menuItem = _menuItemService.GetAll().FirstOrDefault(m => m.Name == lblModalTitle.Text);
            if (menuItem == null) return;

            var guest = guests.FirstOrDefault(g => g.GuestName == guestName);
            if (guest == null) return;

            var cartItem = guest.Items.FirstOrDefault(i => i.MenuItemId == menuItem.Id);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    MenuItemId = menuItem.Id,
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    Quantity = 0
                };
                guest.Items.Add(cartItem);
            }

            if (e.CommandName == "Increment")
                cartItem.Quantity++;
            else if (e.CommandName == "Decrement" && cartItem.Quantity > 0)
                cartItem.Quantity--;

            if (Context != null && Context.Session != null)
            {
                Session["GuestCarts"] = guests;
            }


            rptGuestQuantities.DataSource = guests.Select(g =>
            {
                var item = g.Items.FirstOrDefault(i => i.MenuItemId == menuItem.Id);
                return new
                {
                    GuestName = g.GuestName,
                    Quantity = item?.Quantity ?? 0
                };
            });
            rptGuestQuantities.DataBind();

            UpdateCartSummary(guests);
        }

        protected void btnSaveItem_Click(object sender, EventArgs e)
        {
            pnlItemModal.Visible = false;
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            pnlItemModal.Visible = false;
        }

        private void UpdateCartSummary(List<GuestCart> guests)
        {
            decimal subtotal = guests.SelectMany(g => g.Items).Sum(i => i.Price * i.Quantity);

            // Taxes (Quebec example)
            decimal tps = subtotal * 0.05m;
            decimal tvq = subtotal * 0.09975m;

            // Tip percentage from radio buttons
            int tipPercent = 0;
            if (!string.IsNullOrEmpty(rblTip.SelectedValue))
            {
                tipPercent = int.Parse(rblTip.SelectedValue);
            }
            decimal tip = subtotal * tipPercent / 100m;

            decimal grandTotal = subtotal + tps + tvq + tip;

            // Update labels
            lblCartTotal.Text = $"Subtotal: {subtotal:C2}";
            lblTPS.Text = $"TPS (5%): {tps:C2}";
            lblTVQ.Text = $"TVQ (9.975%): {tvq:C2}";
            lblTip.Text = $"Tip ({tipPercent}%): {tip:C2}";
            lblGrandTotal.Text = $"Grand Total: {grandTotal:C2}";
        }
        protected void rblTip_SelectedIndexChanged(object sender, EventArgs e)
        {
            var guests = Session["GuestCarts"] as List<GuestCart> ?? new List<GuestCart>();
            UpdateCartSummary(guests);
        }


    }
}
