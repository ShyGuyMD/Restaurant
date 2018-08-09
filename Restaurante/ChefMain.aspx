<%@ Page Title="" Language="C#" MasterPageFile="~/Chef.Master" AutoEventWireup="true" CodeBehind="ChefMain.aspx.cs" Inherits="Restaurante.MainChef" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <div>
        <asp:Panel runat="server">
            <asp:Label ID="lblMain" runat="server" Text="Menu Principal"></asp:Label>
            <asp:TextBox ID= "TextBox1" runat="server" placeholder ="Ingrese Texto"></asp:TextBox>
            <asp:Button ID="btnPrincipal" runat="server" Text="Ingresar" />
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
