<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="SimpleWebApp.Views.Index" EnableSessionState="True" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">

        <!-- Guest selection pre-form -->
        <asp:Panel ID="pnlGuestSelection" runat="server" Visible="true" CssClass=" card mb-4">
            <h2>Start Your Order</h2>
            <p>Select number of guests and optionally enter their names.</p>

            <div class="mb-3">
                <asp:Button ID="btnRemoveGuest" runat="server" Text="-" OnClick="btnRemoveGuest_Click"
                    CssClass="btn btn-sm btn-outline-danger" />
                <asp:Button ID="btnAddGuest" runat="server" Text="+" OnClick="btnAddGuest_Click"
                    CssClass="btn btn-sm btn-outline-success ms-2" />
            </div>

            <asp:Repeater ID="rptGuests" runat="server">
                <ItemTemplate>
                    <div class="form-group mb-2">
                        <label>Guest <%# Container.ItemIndex + 1 %>:</label>
                        <asp:TextBox ID="txtGuestName" runat="server" CssClass="form-control"
                                     Text='<%# Eval("GuestName") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Button ID="btnContinueToMenu" runat="server" Text="Continue to Menu"
                CssClass="btn btn-primary mt-3" OnClick="btnContinueToMenu_Click" />
        </asp:Panel>


        <!-- Menu Display -->
        <asp:Panel ID="pnlMenu" runat="server" Visible="false">
            <h2>Menu</h2>
            <asp:Repeater ID="rptCategories" runat="server">
                <ItemTemplate>
                    <div class="menu-category mb-4">
                        <h3><%# Eval("Category") %></h3>
                        <div class="menu-items d-flex flex-wrap">
                            <asp:Repeater ID="rptItems" runat="server"
                                          DataSource='<%# Eval("Items") %>'
                                          OnItemCommand="rptItems_ItemCommand">
                                <ItemTemplate>
                                    <div class="menu-item card m-2 p-2">
                                        <asp:Image ID="imgThumb" runat="server"
                                            width="100px"
                                            ImageUrl='<%# Eval("ThumbnailUrl") %>'
                                            Visible='<%# Eval("ThumbnailUrl") != null %>'
                                            CssClass="card-img-top" AlternateText='<%# Eval("Name") %>' />

                                        <div class="card-body text-center">
                                            <h5 class="card-title"><%# Eval("Name") %></h5>
                                            <p class="fw-bold">$<%# Eval("Price", "{0:F2}") %></p>
                                            <asp:Button ID="btnView" runat="server"
                                                Text="View"
                                                CssClass="btn btn-sm btn-outline-primary"
                                                CommandName="ViewItem"
                                                CommandArgument='<%# Eval("Id") %>' />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>

        <div class="cart-summary mt-3">
            <asp:Label ID="lblCartTotal" runat="server" Text="Subtotal: $0.00"
                        CssClass="fw-bold d-block mb-2" />

            <div class="tip-options mb-2">
                <label class="fw-bold d-block">Tip:</label>
                <asp:RadioButtonList ID="rblTip" runat="server" RepeatDirection="Horizontal"
                                        CssClass="tip-radios"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblTip_SelectedIndexChanged">
                    <asp:ListItem Text="0%" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="5%" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10%" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15%" Value="15"></asp:ListItem>
                    <asp:ListItem Text="20%" Value="20"></asp:ListItem>
                    <asp:ListItem Text="25%" Value="25"></asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <div class="cart-details">
                <div><asp:Label ID="lblTPS" runat="server" /></div>
                <div><asp:Label ID="lblTVQ" runat="server" /></div>
                <div><asp:Label ID="lblTip" runat="server" /></div>
                <div class="grand"><asp:Label ID="lblGrandTotal" runat="server" /></div>
            </div>
        </div>


    </div>

<!-- Item Modal -->
<asp:Panel ID="pnlItemModal" runat="server" CssClass="modal-overlay" Visible="false">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <asp:Label ID="lblModalTitle" runat="server" CssClass="modal-title h4" />
                <asp:Button ID="btnCloseModal" runat="server" Text="×"
                    CssClass="btn-close"
                    OnClick="btnCloseModal_Click" />
            </div>
            <div class="modal-body">
                <asp:Image ID="imgModal" runat="server"
                    CssClass="modal-image" Visible="false" />

                <asp:Label ID="lblModalDescription" runat="server"
                    CssClass="d-block mb-3" />

                <asp:Repeater ID="rptGuestQuantities" runat="server"
                              OnItemCommand="rptGuestQuantities_ItemCommand">
                    <ItemTemplate>
                        <div class="guest-quantity-row">
                            <span class="guest-name"><%# Eval("GuestName") %></span>
                            <div class="quantity-controls">
                                <asp:Button ID="btnMinus" runat="server" Text="-" CommandName="Decrement"
                                    CommandArgument='<%# Eval("GuestName") %>'
                                    CssClass="btn btn-sm btn-outline-danger" />
                                <asp:Label ID="lblQty" runat="server"
                                    Text='<%# Eval("Quantity") %>' CssClass="qty-label" />
                                <asp:Button ID="btnPlus" runat="server" Text="+" CommandName="Increment"
                                    CommandArgument='<%# Eval("GuestName") %>'
                                    CssClass="btn btn-sm btn-outline-success" />
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSaveItem" runat="server" Text="Save"
                    CssClass="btn btn-primary" OnClick="btnSaveItem_Click" />
            </div>
        </div>
    </div>
</asp:Panel>

</asp:Content>
