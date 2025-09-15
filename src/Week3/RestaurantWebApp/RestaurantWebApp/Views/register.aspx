<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="SimpleWebApp.Views.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h1>Register</h1>

        <!-- Register fields -->
        <div class="form-fields">
            <asp:TextBox ID="txtUsername" runat="server" CssClass="input" placeholder="Username" />

            <asp:TextBox ID="txtEmail" runat="server" CssClass="input" TextMode="Email" placeholder="Email" />
            <ul id="email-feedback" class="feedback"></ul>

            <asp:TextBox ID="txtPassword" runat="server" CssClass="input" TextMode="Password" placeholder="Password" />
            <ul id="password-feedback" class="feedback"></ul>

            <asp:TextBox ID="txtConfirm" runat="server" CssClass="input" TextMode="Password" placeholder="Confirm Password" />
            <ul id="confirm-feedback" class="feedback"></ul>

            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn" OnClick="btnRegister_Click" />

            <asp:Label ID="lblError" runat="server" CssClass="error" />
        </div>

        <p style="text-align:center; margin-top:15px;">
            Already have an account? <a href="/Views/login.aspx">Login</a>
        </p>
    </div>
</asp:Content>
