<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdminMenuPrecio.aspx.cs" Inherits="Restaurante.AdminMenuPrecio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PanelMenus" runat="server">
        <asp:GridView ID="GrillaMenus" runat="server" AutoGenerateColumns="false" DataKeyNames="Id">
            <Columns> 
                <asp:BoundField DataField ="Descripcion" HeaderText ="Descripción"/>
                <asp:BoundField DataField ="Precio" HeaderText ="PrecioVenta"/>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
