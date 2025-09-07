using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PizzeriaSSR
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // roule la logique une fois seulement sinon les repost trigger un toggle ---> on ne veut pas ca!
            if (!IsPostBack)
            {
                ToggleAddressVisibility(false);
                lblPrice.Text = "Prix total : 0 $";
                lblConfirmation.Visible = false; // hide confirmation at first
            }
        }

        protected void CheckBoxDelivery_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle visibility of delivery address when checkbox is changed
            ToggleAddressVisibility(CheckBoxDelivery.Checked);
        }

        // Toggle Address visibility helper method
        private void ToggleAddressVisibility(bool visible)
        {
            lblAddress.Visible = visible;
            txtBoxAddress.Visible = visible;
            if (visible)
            {
                // Move cursor into the textbox on toggle!
                txtBoxAddress.Focus();
            }
        }

        // Calculate price of custom pizza
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            double total = 0;

            // Dough
            if (!string.IsNullOrEmpty(ddlDough.SelectedValue))
            {
                total += double.Parse(ddlDough.SelectedValue, CultureInfo.InvariantCulture);
            }

            // Sauce
            if (!string.IsNullOrEmpty(ddlSauce.SelectedValue))
            {
                total += double.Parse(ddlSauce.SelectedValue, CultureInfo.InvariantCulture);
            }

            // Cheese
            if (!string.IsNullOrEmpty(ddlCheese.SelectedValue))
            {
                total += double.Parse(ddlCheese.SelectedValue, CultureInfo.InvariantCulture);
            }

            // Toppings
            foreach (ListItem item in cblToppings.Items)
            {
                if (item.Selected && !string.IsNullOrEmpty(item.Value))
                {
                    total += double.Parse(item.Value, CultureInfo.InvariantCulture);
                }
            }

            // Show price
            lblPrice.Text = $"Prix total : {total:0.00} $";
        }

        // Submit order
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtBoxName.Text.Trim();
            string phone = txtBoxPhone.Text.Trim();
            string address = txtBoxAddress.Text.Trim();

            string confirmation = $"Merci {name}, votre commande a été reçue. " +
                                  $"Nous vous contacterons au {phone}";

            if (CheckBoxDelivery.Checked && !string.IsNullOrEmpty(address))
            {
                confirmation += $", et nous livrerons à l’adresse : {address}.";
            }
            else
            {
                confirmation += ". Vous pourrez récupérer votre commande sur place.";
            }

            lblConfirmation.Text = confirmation;
            lblConfirmation.Visible = true;
        }
    }
}
