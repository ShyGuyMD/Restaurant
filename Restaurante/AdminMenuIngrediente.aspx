<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="AdminMenuIngrediente.aspx.cs" Inherits="Restaurante.AdminMenuIngrediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Seleccionar Ingrediente:"></asp:Label>
        <asp:DropDownList ID="lstIngredientes" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="btnIngrediente" runat="server" Text="Buscar" OnClick="btnIngrediente_Click" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <asp:GridView ID="GrillaIngredientes" runat="server">
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="Costo" HeaderText="Costo" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
