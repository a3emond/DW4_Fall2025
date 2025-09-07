<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PizzeriaSSR.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PizzeriaSSR - Bienvenue</title>
    <style>
html, body, form {
    height: 100%;
    margin: 0;
    padding: 0;
}

body {
    display: flex;
    flex-direction: column;   /* vertical stack */
    font-family: Arial, sans-serif;
    background: linear-gradient(#fdf4d9, #ffeaa7);
    color: #333;
}

/* header at top */
.header {
    background-color: #d35400; /* pizza crust color */
    color: white;
    padding: 1rem;
    text-align: center;
}

/* banner under header */
.banner {
    background-color: #e67e22; /* sauce/cheese tone */
    color: white;
    text-align: center;
    padding: 1rem;
    margin-bottom: 2rem;
}

/* main content fills remaining space */
.main {
    flex: 1;                  /* take all available height */
    display: flex;
    justify-content: center;  /* center horizontally */
    align-items: flex-start;  /* align at top, allow multiple sections */
    padding: 2rem;
    gap: 2rem;
    flex-wrap: wrap;          /* adapt on small screens */
}

/* featured pizzas */
.featured {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 1rem;
    background-color: #fff8ee;
    padding: 1rem;
    border-radius: 8px;
    box-shadow: 0 0 8px rgba(0,0,0,0.1);
}
.featured img {
    width: 200px;
    border-radius: 6px;
}

/* order form box */
.form-container {
    background-color: #fff3e0;
    border-radius: 8px;
    padding: 1.5rem;
    width: 400px;
    box-shadow: 0 0 8px rgba(0,0,0,0.15);
}
    </style>
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

        <!-- Main content -->
        <asp:Panel ID="pnlMain" runat="server" CssClass="main">

            <!-- Featured pizzas -->
            <asp:Panel ID="pnlFeatured" runat="server" CssClass="featured">
                <asp:Label ID="lblFeatured" runat="server" Text="En Vedette" Font-Bold="true" Font-Size="Large" />
                <asp:Image ID="imgPizza1" runat="server" ImageUrl="~/images/pizza1.jpg" AlternateText="Pizza Margherita" />
                <asp:Image ID="imgPizza2" runat="server" ImageUrl="~/images/pizza2.jpg" AlternateText="Pizza Pepperoni" />
                <asp:Image ID="imgPizza3" runat="server" ImageUrl="~/images/pizza3.jpg" AlternateText="Pizza Végétarienne" />
            </asp:Panel>

            <!-- Order form -->
            <asp:Panel ID="pnlForm" runat="server" CssClass="form-container">
                <asp:Label ID="lblFormTitle" runat="server" Text="Passer une commande" Font-Bold="true" Font-Size="Large" /><br /><br />

                <asp:Label ID="lblName" runat="server" Text="Nom :" AssociatedControlID="txtBoxName" /><br />
                <asp:TextBox ID="txtBoxName" runat="server" /><br /><br />

                <asp:Label ID="lblPhone" runat="server" Text="Téléphone :" AssociatedControlID="txtBoxPhone" /><br />
                <asp:TextBox ID="txtBoxPhone" runat="server" /><br /><br />

                <asp:CheckBox ID="CheckBoxDelivery" runat="server" 
                              Text="Livraison à domicile" 
                              AutoPostBack="true" 
                              OnCheckedChanged="CheckBoxDelivery_CheckedChanged" /><br /><br />

                <asp:Label ID="lblAddress" runat="server" Text="Adresse de livraison :" AssociatedControlID="txtBoxAddress" /><br />
                <asp:TextBox ID="txtBoxAddress" runat="server" 
                             TextMode="MultiLine" 
                             Rows="3" Width="100%" /><br /><br />

                <!-- Make your own pizza -->
                <asp:Label ID="lblCustomTitle" runat="server" Text="Composez votre pizza" Font-Bold="true" Font-Size="Large" /><br /><br />

                <asp:Label ID="lblDough" runat="server" Text="Pâte :" /><br />
                <asp:DropDownList ID="ddlDough" runat="server">
                    <asp:ListItem Text="Classique" Value="5" />
                    <asp:ListItem Text="Fine" Value="6" />
                    <asp:ListItem Text="Épaisse" Value="7" />
                </asp:DropDownList><br /><br />

                <asp:Label ID="lblSauce" runat="server" Text="Sauce :" /><br />
                <asp:DropDownList ID="ddlSauce" runat="server">
                    <asp:ListItem Text="Tomate" Value="2" />
                    <asp:ListItem Text="Crème" Value="3" />
                    <asp:ListItem Text="Pesto" Value="4" />
                </asp:DropDownList><br /><br />

                <asp:Label ID="lblCheese" runat="server" Text="Fromage :" /><br />
                <asp:DropDownList ID="ddlCheese" runat="server">
                    <asp:ListItem Text="Mozzarella" Value="3" />
                    <asp:ListItem Text="Cheddar" Value="4" />
                    <asp:ListItem Text="Parmesan" Value="5" />
                </asp:DropDownList><br /><br />

                <asp:Label ID="lblToppings" runat="server" Text="Garnitures :" /><br />
                <asp:CheckBoxList ID="cblToppings" runat="server">
                    <asp:ListItem Text="Pepperoni" Value="2" />
                    <asp:ListItem Text="Champignons" Value="1.5" />
                    <asp:ListItem Text="Poivrons" Value="1.5" />
                    <asp:ListItem Text="Olives" Value="1" />
                    <asp:ListItem Text="Oignons" Value="1" />
                </asp:CheckBoxList><br /><br />

                <asp:Button ID="btnCalculate" runat="server" Text="Calculer le prix" OnClick="btnCalculate_Click" /><br /><br />
                <asp:Label ID="lblPrice" runat="server" Text="Prix total : 0 $" Font-Bold="true" />

                <hr />

                <asp:Button ID="btnSubmit" runat="server" Text="Commander" OnClick="btnSubmit_Click" /><br /><br />
                <asp:Label ID="lblConfirmation" runat="server" Font-Bold="true" ForeColor="Green" Visible="false" />
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
