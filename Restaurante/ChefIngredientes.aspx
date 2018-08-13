<%@ Page Title="" Language="C#" MasterPageFile="~/Maestra.Master" AutoEventWireup="true" CodeBehind="ChefIngredientes.aspx.cs" Inherits="Restaurante.IngredientesChef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="ListaMenu" runat="server">
        <asp:DropDownList ID="lstMenu" runat="server" OnSelectedIndexChanged="lstMenu_SelectedIndexChanged"/>
    </asp:Panel>
    <br />
    <asp:Button ID="btnCargarMenu" runat="server" Text="Cargar" OnClick="btnCargarMenu_Click" />
    <asp:Panel ID="GrillaModificar" runat="server">
        <asp:GridView ID="GrillaIngredientes" runat="server" AutoGenerateColumns="false" OnRowCommand="GrillaIngredientes_RowCommand" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Ingrediente.Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br/>
    <asp:Panel ID="AltaIngrediente" runat="server">
        <asp:Label ID="lbl1" runat="server" Text="Modificar o añadir nuevo ingrediente (seleccionar un ingrediente existente modificará la cantidad del menú)."></asp:Label>
        <br/>
        <asp:Label ID="lbl2" runat="server" Text="Seleccionar ingrediente:"></asp:Label>
        
        <asp:DropDownList ID="lstIngredientes" runat="server" />
        <br/>
        <asp:Label ID="Label1" runat="server" Text="Cantidad:"></asp:Label>

        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="txtCantidad" ID="RequiredFieldValidator4" runat="server" ForeColor="Tomato" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtCantidad" runat="server" ErrorMessage="Cantidad Inválida" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
        <br/>
        <asp:Button ID="btnAgregar" runat="server" Text="Añadir al Menu" OnClick="btnAgregar_Click" />
    </asp:Panel>

</asp:Content>


