using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PizzeriaSSR
{
    public partial class Index : Page
    {
        private const double DeliveryFee = 5.0;

        // =========================
        // Section: Menu Definitions
        // =========================
        private static readonly Dictionary<string, double> Specialities = new Dictionary<string, double>()
        {
            { "Margherita - 8 $", 8 },
            { "Pepperoni - 10 $", 10 },
            { "Végétarienne - 9 $", 9 }
        };

        private static readonly Dictionary<string, double> Doughs = new Dictionary<string, double>()
        {
            { "Classique", 5 },
            { "Fine", 6 },
            { "Épaisse", 7 }
        };

        private static readonly Dictionary<string, double> Sauces = new Dictionary<string, double>()
        {
            { "Tomate", 2 },
            { "Crème", 3 },
            { "Pesto", 4 }
        };

        private static readonly Dictionary<string, double> Cheeses = new Dictionary<string, double>()
        {
            { "Mozzarella", 3 },
            { "Cheddar", 4 },
            { "Parmesan", 5 }
        };

        private static readonly Dictionary<string, double> Toppings = new Dictionary<string, double>()
        {
            { "Pepperoni", 2 },
            { "Champignons", 1.5 },
            { "Poivrons", 1.5 },
            { "Olives", 1 },
            { "Oignons", 1 }
        };

        private static readonly Dictionary<string, double> Sizes = new Dictionary<string, double>()
        {
            { "Petite", 1.0 },
            { "Moyenne", 1.5 },
            { "Grande", 2.0 }
        };

        // =========================
        // Section: Page lifecycle
        // =========================
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ToggleAddressVisibility(false);
                lblPrice.Text = "Prix total : 0 $";
                LoadMenuData();
                pnlCustomPizza.Visible = true; // default visible
            }
        }

        // =========================
        // Section: Event Handlers
        // =========================
        protected void CheckBoxDelivery_CheckedChanged(object sender, EventArgs e)
        {
            ToggleAddressVisibility(CheckBoxDelivery.Checked);
        }

        protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCustomPizza.Visible = ddlSpeciality.SelectedValue == "0";
            lblPrice.Text = "Prix total : 0 $"; // reset
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            double total = CalculateBasePizzaPrice();
            total += GetSelectedToppingsPrice();

            total *= ParseSelectedSize();
            total *= ParseQuantity();

            if (CheckBoxDelivery.Checked)
                total += DeliveryFee;

            lblPrice.Text = $"Prix total : {total:0.00} $";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtBoxName.Text.Trim();
            string phone = txtBoxPhone.Text.Trim();
            string address = txtBoxAddress.Text.Trim();

            int quantity = ParseQuantity();
            double basePrice = CalculateBasePizzaPrice();
            double extrasPrice = GetSelectedToppingsPrice();

            string pizzaDesc = GetPizzaDescription();
            string sizeLabel = ddlSize.SelectedItem.Text;
            double sizeMultiplier = ParseSelectedSize();

            double singlePizzaPrice = (basePrice + extrasPrice) * sizeMultiplier;
            double total = singlePizzaPrice * quantity;
            if (CheckBoxDelivery.Checked)
                total += DeliveryFee;

            litInvoice.Text = BuildInvoiceHtml(
                name, phone, address,
                pizzaDesc, sizeLabel,
                basePrice, extrasPrice,
                singlePizzaPrice, total,
                quantity, CheckBoxDelivery.Checked
            );

            pnlInvoice.Visible = true;
        }

        // =========================
        // Section: Helpers
        // =========================
        private void ToggleAddressVisibility(bool visible)
        {
            lblAddress.Visible = visible;
            txtBoxAddress.Visible = visible;
            if (visible) txtBoxAddress.Focus();
        }

        private int ParseQuantity()
        {
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 1)
            {
                quantity = 1;
                txtQuantity.Text = "1";
            }
            return quantity;
        }

        private double ParseSelectedSize() =>
            double.Parse(ddlSize.SelectedValue, CultureInfo.InvariantCulture);

        private double CalculateBasePizzaPrice()
        {
            if (ddlSpeciality.SelectedValue != "0")
                return double.Parse(ddlSpeciality.SelectedValue, CultureInfo.InvariantCulture);

            double total = 0;
            total += ParseIfNotEmpty(ddlDough.SelectedValue);
            total += ParseIfNotEmpty(ddlSauce.SelectedValue);
            total += ParseIfNotEmpty(ddlCheese.SelectedValue);
            return total;
        }

        private double ParseIfNotEmpty(string value) =>
            string.IsNullOrEmpty(value) ? 0 : double.Parse(value, CultureInfo.InvariantCulture);

        private double GetSelectedToppingsPrice() =>
            cblToppings.Items.Cast<ListItem>()
                .Where(i => i.Selected && !string.IsNullOrEmpty(i.Value))
                .Sum(i => double.Parse(i.Value, CultureInfo.InvariantCulture));

        private string GetPizzaDescription()
        {
            if (ddlSpeciality.SelectedValue != "0")
                return ddlSpeciality.SelectedItem.Text;

            string dough = ddlDough.SelectedItem?.Text ?? "";
            string sauce = ddlSauce.SelectedItem?.Text ?? "";
            string cheese = ddlCheese.SelectedItem?.Text ?? "";
            return $"Pizza personnalisée ({dough}, {sauce}, {cheese})";
        }

        private string BuildInvoiceHtml(
            string name, string phone, string address,
            string pizzaDesc, string sizeLabel,
            double basePrice, double extrasPrice,
            double singlePizzaPrice, double total,
            int quantity, bool delivery)
        {
            double tps = total * 0.05;
            double tvq = total * 0.09975;
            string invoiceHtml = $@"
            <p><strong>Client :</strong> {name}<br />
            <strong>Téléphone :</strong> {phone}<br />";

            invoiceHtml += delivery && !string.IsNullOrEmpty(address)
                ? $"<strong>Livraison :</strong> {address}</p>"
                : "<strong>Livraison :</strong> À récupérer sur place</p>";

            invoiceHtml += "<h4>Détails de la commande</h4>";
            invoiceHtml += $"<p>{quantity} x {pizzaDesc} ({sizeLabel})</p>";
            invoiceHtml += "<ul>";

            invoiceHtml += $"<li>Base : {basePrice:0.00} $</li>";

            if (extrasPrice > 0)
            {
                invoiceHtml += "<li>Extras :</li><ul>";
                foreach (var item in cblToppings.Items.Cast<ListItem>().Where(i => i.Selected))
                    invoiceHtml += $"<li>{item.Text} - {double.Parse(item.Value, CultureInfo.InvariantCulture):0.00} $</li>";
                invoiceHtml += "</ul>";
            }

            invoiceHtml += $"<li>Taille : {sizeLabel} (x{ParseSelectedSize():0.0})</li>";
            invoiceHtml += $"<li>Sous-total (1 pizza) : {singlePizzaPrice:0.00} $</li>";
            invoiceHtml += $"<li>Quantité : {quantity}</li>";
            invoiceHtml += delivery
                ? $"<li>Livraison : {DeliveryFee:0.00} $</li>"
                : "<li>À emporter : Gratuit</li>";
            // Taxes (TPS: 5%, TVQ: 9.975%)
            invoiceHtml += $"<li>TPS (5%) : {tps:0.00} $</li>";
            invoiceHtml += $"<li>TVQ (9.975%) : {tvq:0.00} $</li>";
            // Total
            total += tps + tvq;
            invoiceHtml += $"<li><strong>Total : {total:0.00} $</strong></li>";
            invoiceHtml += "</ul>";

            return invoiceHtml;
        }

        private void LoadMenuData()
        {
            ddlSpeciality.Items.Clear();
            ddlSpeciality.Items.Add(new ListItem("Sélectionner une spécialité", "0"));
            foreach (var kvp in Specialities)
                ddlSpeciality.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString(CultureInfo.InvariantCulture)));

            FillDropDown(ddlDough, Doughs);
            FillDropDown(ddlSauce, Sauces);
            FillDropDown(ddlCheese, Cheeses);

            cblToppings.Items.Clear();
            foreach (var kvp in Toppings)
                cblToppings.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString(CultureInfo.InvariantCulture)));

            ddlSize.Items.Clear();
            foreach (var kvp in Sizes)
                ddlSize.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString(CultureInfo.InvariantCulture)));
        }

        private void FillDropDown(DropDownList ddl, Dictionary<string, double> items)
        {
            ddl.Items.Clear();
            foreach (var kvp in items)
                ddl.Items.Add(new ListItem(kvp.Key, kvp.Value.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
