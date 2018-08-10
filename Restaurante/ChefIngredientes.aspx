<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="ChefIngredientes.aspx.cs" Inherits="Restaurante.IngredientesChef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="ListaMenu" runat="server">

        <asp:DropDownList ID="lstCanchas" runat="server" />
        <br />
    </asp:Panel>
    <asp:Button ID="btnCargarMenu" runat="server" Text="Cargar" OnClick="btnCargarMenu_Click" />
    <asp:Panel ID="Grilla" runat="server">
        <asp:GridView ID="GrillaIngredientes" runat="server" AutoGenerateColumns="false" OnRowCommand="GrillaIngredientes_RowCommand" DataKeyNames="ID" OnSelectedIndexChanged="GrillaIngredientes_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="NombreIngrediente" />
                <asp:TemplateField HeaderText="Cantidad" SortExpression="Cantidad">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"
                            Text='<%# Bind("Cantidad") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="modificar" Text="Modificar" />
                <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
