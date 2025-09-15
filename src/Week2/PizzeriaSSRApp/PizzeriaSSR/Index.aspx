<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PizzeriaSSR.Index" MaintainScrollPositionOnPostBack="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PizzeriaSSR - Bienvenue</title>
    <meta charset="utf-8" />

    <link rel="stylesheet" type="text/css" href="Index.aspx.css" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Header -->
        <asp:Panel ID="pnlHeader" runat="server" CssClass="header">
            <asp:Label ID="lblTitle" runat="server" Text="PizzeriaSSR" Font-Size="X-Large" />
        </asp:Panel>

        <!-- Banner -->
        <asp:Panel ID="pnlBanner" runat="server" CssClass="banner">
            <marquee behavior="scroll" direction="left">
                <asp:Label ID="lblPhilosophy" runat="server" 
                           Text="Notre philosophie : des pizzas fraîches, faites maison, avec amour !" />
            </marquee>
        </asp:Panel>


        <!-- Featured pizzas -->
        <asp:Panel ID="pnlFeaturedWrapper" runat="server" CssClass="featured-wrapper">
            <asp:Label ID="lblFeatured" runat="server" Text="En Vedette" Font-Bold="true" Font-Size="Large" />
            <asp:Panel ID="pnlFeatured" runat="server" CssClass="featured">
                <div class="image-wrapper">
                    <asp:Image ID="imgPizza1" runat="server" ImageUrl="~/images/pizza1.jpg" AlternateText="Pizza Margherita" />
                    <div class="label">Margherita</div>
                </div>
                <div class="image-wrapper">
                    <asp:Image ID="imgPizza2" runat="server" ImageUrl="~/images/pizza2.jpg" AlternateText="Pizza Pepperoni" />
                    <div class="label">Pepperoni</div>
                </div>
                <div class="image-wrapper">
                    <asp:Image ID="imgPizza3" runat="server" ImageUrl="~/images/pizza3.jpg" AlternateText="Pizza Végétarienne" />
                    <div class="label">Végétarienne</div>
                </div>
            </asp:Panel>
        </asp:Panel>

        <!-- Main content -->
        <asp:Panel ID="pnlMain" runat="server" CssClass="main">
            <!-- Order form -->
            <asp:Panel ID="pnlForm" runat="server" CssClass="form-container">

                <asp:Label ID="lblFormTitle" runat="server" Text="Passer une commande" Font-Bold="true" Font-Size="Large" /><br /><br />

                <!-- Customer Info -->
                <asp:Label ID="lblName" runat="server" Text="Nom :" AssociatedControlID="txtBoxName" /><br />
                <asp:TextBox ID="txtBoxName" runat="server" /><br />
                <asp:Label ID="lblNameError" runat="server" Text="" CssClass="error" style="display:none;color:red;" /><br /><br />

                <asp:Label ID="lblPhone" runat="server" Text="Téléphone :" AssociatedControlID="txtBoxPhone" /><br />
                <asp:TextBox ID="txtBoxPhone" runat="server" /><br />
                <asp:Label ID="lblPhoneError" runat="server" Text="" CssClass="error" style="display:none;color:red;" /><br /><br />

                <asp:CheckBox ID="CheckBoxDelivery" runat="server" 
                              Text="Livraison à domicile" 
                              AutoPostBack="true" 
                              OnCheckedChanged="CheckBoxDelivery_CheckedChanged" /><br /><br />

                <asp:Label ID="lblAddress" runat="server" Text="Adresse de livraison :" AssociatedControlID="txtBoxAddress" /><br />
                <asp:TextBox ID="txtBoxAddress" runat="server" TextMode="MultiLine" Rows="3" Width="100%" /><br />
                <asp:Label ID="lblAddressError" runat="server" Text="" CssClass="error" style="display:none;color:red;" /><br /><br />


                <!-- Dropdown to select specialities -->
                <asp:Label ID="lblSpeciality" runat="server" Text="Pizzas Spécialités :" /><br />
                <asp:DropDownList ID="ddlSpeciality" runat="server" AutoPostBack="true" 
                                  OnSelectedIndexChanged="ddlSpeciality_SelectedIndexChanged" /><br /><br />

                <!-- Custom pizza builder -->
                <asp:Panel ID="pnlCustomPizza" runat="server">
                    <asp:Label ID="lblCustomTitle" runat="server" Text="Composez votre pizza" Font-Bold="true" Font-Size="Large" /><br /><br />

                    <asp:Label ID="lblDough" runat="server" Text="Pâte :" /><br />
                    <asp:DropDownList ID="ddlDough" runat="server" /><br /><br />

                    <asp:Label ID="lblSauce" runat="server" Text="Sauce :" /><br />
                    <asp:DropDownList ID="ddlSauce" runat="server" /><br /><br />

                    <asp:Label ID="lblCheese" runat="server" Text="Fromage :" /><br />
                    <asp:DropDownList ID="ddlCheese" runat="server" /><br /><br />
                </asp:Panel>

                <!-- Extras -->
                <asp:Label ID="lblToppings" runat="server" Text="Extras :" /><br />
                <asp:CheckBoxList ID="cblToppings" runat="server" /><br /><br />

                <!-- Size -->
                <asp:Label ID="lblSize" runat="server" Text="Taille :" /><br />
                <asp:DropDownList ID="ddlSize" runat="server" /><br /><br />

                <asp:ListBox ID="lstSizes" runat="server" SelectionMode="Single">
                    <asp:ListItem Value="8" Text="Petite (8 pouces) – 8 $" />
                    <asp:ListItem Value="12" Text="Moyenne (12 pouces) – 12 $" Selected="True"/> <!-- default selection -->
                    <asp:ListItem Value="16" Text="Grande (16 pouces) – 16 $" />
                </asp:ListBox>
                <script>
                    private int GetSelectedSizeValue()
                    {
                        // Safely convert the SelectedValue to int even if I was the one who set the values haha
                        int value;
                        if (int.TryParse(lstSizes.SelectedValue, out value)) {
                            return value;
                        }

                        // Fallback 
                        return 0;
                    }

                    private string GetSelectedSizeText()
                    {
                        // Returns the Text property ("Petite (8 pouces) – 8 $", etc.)
                        return lstSizes.SelectedItem.Text;
                    }
                </script>


                <!-- Quantity -->
                <asp:Label ID="lblQuantity" runat="server" Text="Quantité :" /><br />
                <asp:TextBox ID="txtQuantity" runat="server" Text="1" Width="50px" /><br />
                <asp:Label ID="lblQuantityError" runat="server" Text="" CssClass="error" style="display:none;color:red;" /><br /><br />



                <!-- Actions -->
                <asp:Button ID="btnCalculate" runat="server" Text="Calculer le prix" OnClick="btnCalculate_Click" /><br /><br />
                <asp:Label ID="lblPrice" runat="server" Text="Prix total : 0 $" Font-Bold="true" />

                <hr />

                <asp:Button ID="btnSubmit" runat="server" Text="Commander" OnClick="btnSubmit_Click" /><br /><br />
                <!-- Invoice -->
                <asp:Panel ID="pnlInvoice" runat="server" Visible="false" CssClass="invoice">
                    <h3>Facture</h3>
                    <asp:Literal ID="litInvoice" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
    </form>

    <!-- Frontend validation script -->
    <script type="text/javascript">
        // expose client IDs to JavaScript
        var clientIDs = {
            form: '<%= form1.ClientID %>',
            name: '<%= txtBoxName.ClientID %>',
            phone: '<%= txtBoxPhone.ClientID %>',
            address: '<%= txtBoxAddress.ClientID %>',
            delivery: '<%= CheckBoxDelivery.ClientID %>',
            quantity: '<%= txtQuantity.ClientID %>',
            nameErr: '<%= lblNameError.ClientID %>',
            phoneErr: '<%= lblPhoneError.ClientID %>',
            addrErr: '<%= lblAddressError.ClientID %>',
            qtyErr: '<%= lblQuantityError.ClientID %>'
        };
    </script>
    <script src="Index.aspx.js"></script>


</body>
</html>
