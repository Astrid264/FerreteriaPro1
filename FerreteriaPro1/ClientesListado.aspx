<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientesListado.aspx.cs" Inherits="FerreteriaPro1.ClientesListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Listado de clientes</h3>
    <div class="row">
        <div class="col-lg-12">
            <input type="button" class="btn btn-primary" value="Agregar" onclick="location.href = 'Clientes.aspx?op=1';" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="dgvListado" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-striped table-bordered table-hover dataTable no-footer" role="grid" OnRowEditing="dgvListado_RowEditing" OnRowDeleting="dgvListado_RowDeleting">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="id_cliente" DataField="id_cliente" HeaderText="id cliente">
                            <ItemStyle HorizontalAlign="Center" CssClass="hidden" />
                            <HeaderStyle CssClass="text-center info hidden" />
                        </asp:BoundField>
                        <asp:HyperLinkField AccessibleHeaderText="nombre_cliente" DataTextField="nombre_cliente" HeaderText="Nombre del cliente" DataNavigateUrlFields="id_cliente" DataNavigateUrlFormatString="Clientes.aspx?idc={0}&op=4">
                            <HeaderStyle CssClass="info" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField AccessibleHeaderText="direccion_cliente" DataField="direccion_cliente" HeaderText="Direccion">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="telefono_cliente" DataField="telefono_cliente" HeaderText="Telefono">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="noShowLoader" EditText="<i class='glyphicon glyphicon-pencil text-primary'></i>" AccessibleHeaderText="" HeaderText="Modificar">
                            <ItemStyle CssClass="" HorizontalAlign="Center" Width="5%" />
                            <HeaderStyle CssClass="info text-center " />
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="noShowLoader" DeleteText="<i class='glyphicon glyphicon-trash text-danger'></i>" AccessibleHeaderText="" HeaderText="Eliminar">
                            <ItemStyle CssClass="" HorizontalAlign="Center" Width="5%" />
                            <HeaderStyle CssClass="info text-center " />
                        </asp:CommandField>
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
