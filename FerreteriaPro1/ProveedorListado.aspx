<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProveedorListado.aspx.cs" Inherits="FerreteriaPro1.ProveedorListado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">        
        <div class="col-lg-12">
            <input type="button" class="btn btn-primary" value="Agregar" onclick="location.href = 'Proveedores.aspx';" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="dgvListado" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-striped table-bordered table-hover dataTable no-footer" role="grid">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="id_proveedor" DataField="id_proveedor" HeaderText="id proveedor">
                            <ItemStyle HorizontalAlign="Center" CssClass="hidden" />
                            <HeaderStyle CssClass="text-center info hidden" />
                        </asp:BoundField>
                        <asp:HyperLinkField AccessibleHeaderText="nombre_proveedor" DataTextField="nombre_proveedor" HeaderText="Nombre del proveedor" DataNavigateUrlFields="id_proveedor" DataNavigateUrlFormatString="Proveedores.aspx?idp={0}&op=4">
                            <HeaderStyle CssClass="info" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField AccessibleHeaderText="direccion_proveeedor" DataField="direccion_proveedor" HeaderText="Direccion">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="telefono_proveedor" DataField="telefono_proveedor" HeaderText="Telefono">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <% 
            if (_MensajeError != "")
            {
        %>
        <div class="alert alert-danger">
            <%=_MensajeError %>
        </div>
        <%
            }
            if (_MensajeSatisfactorio != "")
            {
        %>
        <div class="alert alert-success">
            <%=_MensajeSatisfactorio %>
        </div>
        <%
            }

        %>
    </div>
</asp:Content>
