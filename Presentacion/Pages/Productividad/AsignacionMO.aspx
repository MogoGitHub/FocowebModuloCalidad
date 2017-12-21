<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Main.Master" CodeBehind="AsignacionMO.aspx.vb" Inherits="Presentacion.AsignacionMO" %>
<%@ register assembly="DevExpress.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="MainHeader" ContentPlaceHolderID="head" runat="server">
    <script>
        /**
         * limpia bloqueo por validacion
         */
        var validaDate = false;
        var validaTexto = false;
        $(function () {
            var $j = jQuery.noConflict();
            $j("#datepicker2").datepicker({ maxDate: '0' });
            $j("#datepicker2").on("change", function () {

                var selectedDate = $(this).val();
                validaDate = true;
                document.getElementById("datepicker2").style["background-color"] = "#FFF";
                document.getElementById("test2").className = " w3-border";
                $("#contenedorTabla").removeClass("disabledbutton");
                $("#contenedorTabla2").removeClass("disabledbutton");
                $("#openMod").removeClass("disabledbutton");

            });
        });
    </script>
    <script type="text/javascript">
        /**
         * limpia bloqueo por trabajador asignado con checkbox
        validar numeros negativos
         */
        var selectCheckBox = false;
        var contador;
        var conteoTrabajadores = 0;
        function grdTrab_BatchEditEndEditing(s, e) {


            contador = e.rowValues[(s.GetColumnByField("chkbox").index)].value;

            selectCheckBox = true;
            console.log('entra');

            $("#alertaWorker").removeClass("checkWorker");
            $("#contenedorTabla2").removeClass("disabledbutton");
            $("#openMod").removeClass("disabledbutton");

            if (contador == 1) {
                conteoTrabajadores = conteoTrabajadores + 1;
                document.getElementById('conteo').innerText = String(conteoTrabajadores);
            } else if (contador == 0) {

                conteoTrabajadores = conteoTrabajadores - 1;
                document.getElementById('conteo').innerText = String(conteoTrabajadores);
            }


        }
    </script>
    <style>
        .notify-badge {
            position: relative;
            right: -35px;
            top: -45px;
            background: #e6b800;
            text-align: center;
            border-radius: 10px 10px 10px 10px;
            color: white;
            padding: 5px 10px;
            font-size: 14px;
            font-weight: 600;
        }

        .checkWorker {
            display: block !important;
        }

        .disabledbutton {
            pointer-events: none;
            opacity: 0.4;
        }

        .claseTest {
            background-color: #005F6B;
            font-weight: bold;
        }

        .claseTest1 {
            background-color: #008C9E;
        }

        .claseTest2 {
            background-color: #00B4CC;
        }

        .claseTest3 {
            background-color: #343838;
            color: #00DFFC !important;
            font-weight: bold;
        }

        .dxgvHeader, .dxgvHeader table {
            color: #f1f1f1;
            border-color: #333;
        }

        .dxgvControl, .dxgvDisabled {
            border: 1px solid #333;
        }

        .resultado {
            vertical-align: middle;
            text-align: center;
        }

        .col {
            word-wrap: break-word;
        }
    </style>
</asp:Content>

<asp:content id="MainBody" contentplaceholderid="body" runat="server">
    <!-- 
    - BODY -
    Contenido Asignacion de mano de obra
    Non-Responsive - Contenido ASP: ASPxGridView
    
-->
    <div class="w3-main" id="main">

        <div class="w3-container w3-content" style="min-width: 1366px; margin-top: 25px">

            <div class="w3-row ">

<!-- columna izquierda-->
                <div id="columLeft" class="w3-quarter w3-margin-bottom w3-content w3-container w3-padding-32 w3-responsive">
                    <div class="w3-row w3-padding w3-teal w3-card-4">
<!-- Paso 1 Seleccionar Fecha-->
                        <div class="w3-row">

                            <h4><b>Paso 1</b></h4>
                            <p style="line-height: 0px">Selecciona Fecha de Asignación</p>
                        </div>
<!-- Selector fecha asignacion -->
                        <div id="test2" class="w3-row w3-border">
                        <input class="w3-input w3-centered w3-small w3-border-0" type="text" id="datepicker2" autocomplete="off" placeholder="Presiona acá para seleccionar fecha" style="width: 100%; ">
                        </div>
<!-- Paso 2 Seleccionar trabajador-->
                        <div class="w3-row">
                            <h4><b>Paso 2</b></h4>
                            <p style="line-height: 0px">Selecciona Trabajador</p>
                        </div>
                        <div class="w3-row">
<!-- Boton Expandir -->
                            <div class="w3-col w3-container w3-border-0 w3-left" style="width: 20%;">
                                <button id="rutData" class="w3-btn w3-small w3-blue w3-left w3-round-large w3-card-2" value="Hide" type="button" title="Activar/Desactivar Filtro" style="margin-left: -17px">EXPANDIR</button>
                            </div>
<!-- Lista desplegable costos directos -->
                            <div class="w3-col w3-border-0 w3-right-align w3-right" style="width: 60%">
                                <select class="w3-select w3-content w3-border-0 w3-right-align w3-right w3-small" name="option" style="margin-top: 5px; width: 90%">
                                    <option value="" selected>Costos Directos</option>
                                    <option value="1">Subcontratos</option>
                                    <option value="2">Mano de Obra Directa</option>
                                    <option value="3">Mano de Obra Indirecta</option>
                                </select>
                            </div>
                        </div>
<!-- validacion y checkbox saldo por asignar -->                      
                        <div class="w3-row" style="margin-top: 15px">
                            <p class="w3-small"><input class="w3-check" type="checkbox" name="" value="" id="checkTest"> Sólo Trabajadores con saldo por asignar.</p>
                            <p id="alertaWorker" style="display:none; color:#FFF774"> <b>Selecciona un trabajador antes de asignar!</b></p>
                        </div>
                    </div>
<!-- Grilla Left -->
                      <div id="contenedorTabla" class="w3-row">
                        <dx:aspxgridview
                            id="grdDetalle"
                            runat="server"
                            theme="Default"
                            width="100%"
                            enablerowscache="False"
                            font-size="X-Small"
                            keyfieldname="chkbox;nombre;estado;rut;especialidad;cargo;supervisor"
                            clientinstancename="grdDetalle"    
                            enablecallbacks="False" 
                            enabletheming="True"
                            SettingsEditing-Mode="Batch"
                            Settings-ShowStatusBar = "Hidden"
                            AutoGenerateColumns= "false"
                            >
                            <Columns>
                            <dx:GridViewDataCheckColumn width="45" Caption= " " FieldName="chkbox" Name="chkbox" VisibleIndex="1">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                            <CellStyle CssClass="claseTestCelda" />
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataTextColumn width="150" Caption="Nombre Trabajador" FieldName="nombre" Name="nombre" VisibleIndex="2">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Estado" FieldName="estado" Name="estado" VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="R.U.T" FieldName="rut" Name="rut" VisibleIndex="4">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Especialidad" FieldName="especialidad" Name="especialidad" VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn width="150" Caption="Cargo" FieldName="cargo" Name="cargo" VisibleIndex="6">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Supervisor" FieldName="supervisor" Name="supervisor" VisibleIndex="7">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            </Columns>
                            <settingspager Mode="EndlessPaging" PageSize="25">
                            </settingspager>
                            <settings showfilterrow="True" ShowGroupPanel="false" VerticalScrollableHeight="240" HorizontalScrollBarMode="auto" VerticalScrollBarMode="auto" UseFixedTableLayout="true"/>
                            <SettingsEditing BatchEditSettings-ShowConfirmOnLosingChanges="false">
                            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
                            </SettingsEditing>
                            <ClientSideEvents BatchEditEndEditing="grdTrab_BatchEditEndEditing"  />
                        </dx:aspxgridview>
                    </div>
                </div>
<!-- End columna left-->
<!-- Columna derecha -->
                   <div id="columRight" class="w3-threequarter w3-margin-bottom w3-content w3-container w3-padding-32 ">
                    <div class="w3-row w3-padding w3-teal w3-card-4">
<!-- Paso 3 -->                        
                        <div class="w3-half w3-container">
                            <h4><b>Paso 3</b></h4>
                            <p style="line-height: 0px">Asigna Actividades</p>
                        </div>
<!-- Boton agregar partidas -->
                         <div class="w3-quarter w3-container w3-right-align" style="margin-top: 20px">
                            <button id="openMod0" 
                                   class="w3-btn w3-small w3-blue w3-round-large w3-card-2" 
                                   value="" 
                                   type="button" 
                                   onclick="document.getElementById('alerta4').style.display='block'" 
                                   title="Ver/Asignar" 
                                   style="margin-left: -17px">TRABAJADORES SELECCIONADOS
                           </button> 
                           <span class="notify-badge" id="conteo">0</span>
                            
                        </div>
                        <div class="w3-quarter w3-container w3-right-align" style="margin-top: 20px">
                           
                            <button id="openMod" 
                                   class="w3-btn w3-small w3-blue w3-round-large w3-card-2" 
                                   value="" 
                                   type="button" 
                                   onclick="document.getElementById('alerta').style.display='block'" 
                                   title="Agregue actividades personalizadas" 
                                   style="margin-left: -17px">AGREGAR ACTIVIDADES
                           </button> 
                        </div>
                    </div>
<!-- contenedor tabla -->
                    <div id="contenedorTabla2" class="w3-row w3-responsive"> 
                        <dx:aspxgridview
                            id="Aspxgridview1"
                            runat="server"
                            theme="Default"
                            width="100%"
                            enablerowscache="False"
                            font-size="X-Small"
                            keyfieldname="id"
                            clientinstancename="gvVista"
                            enablecallbacks="False"    
                            enabletheming="True" AutoGenerateColumns="False"
                            SettingsEditing-Mode="Batch"
                            Settings-ShowStatusBar = "Hidden"
                            >
                            <Columns >
                        <dx:GridViewDataTextColumn Caption="Cod" Width="40" VisibleIndex="1" FieldName="cod" Name="cod" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                    <EditFormSettings Visible="false" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn Caption="Nombre Actividad" Width="130" VisibleIndex="2" FieldName="partida" Name="partida">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                    <EditFormSettings Visible="false" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn VisibleIndex="4" Caption="Cant. Avance" FieldName="qavance" Name="qavance" Width="60">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                    <EditFormSettings Visible="false" />
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewBandColumn Caption="L" VisibleIndex="5" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1"  />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="1" VisibleIndex="0" FieldName="chkbox1" Name="chkbox1" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="M" VisibleIndex="6" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1"  />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="2" VisibleIndex="0" FieldName="chkbox2" Name="chkbox2" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="M" VisibleIndex="7" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="3" VisibleIndex="0" FieldName="chkbox3" Name="chkbox3" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="J" VisibleIndex="8" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="4" VisibleIndex="0" FieldName="chkbox4" Name="chkbox4" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="V" VisibleIndex="9" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="5" VisibleIndex="0" FieldName="chkbox5" Name="chkbox5" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="S" VisibleIndex="10" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="6" VisibleIndex="0" FieldName="chkbox6" Name="chkbox6" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>

                                <dx:GridViewBandColumn Caption="D" VisibleIndex="11">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="7" VisibleIndex="0" FieldName="chkbox7" Name="chkbox7" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest1" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="L" VisibleIndex="12" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="8" VisibleIndex="0" FieldName="chkbox8" Name="chkbox8" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="M" VisibleIndex="13" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="9" VisibleIndex="0" FieldName="chkbox9" Name="chkbox9" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="M" VisibleIndex="14" >
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="10" VisibleIndex="0" FieldName="chkbox10" Name="chkbox10" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="J" VisibleIndex="15">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="11" VisibleIndex="0"  FieldName="chkbox11" Name="chkbox11" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="V" VisibleIndex="16">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="12" VisibleIndex="0"  FieldName="chkbox12" Name="chkbox12" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="S" VisibleIndex="17">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="13" VisibleIndex="0"  FieldName="chkbox13" Name="chkbox13" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="D" VisibleIndex="18">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                    <Columns>
                                        <dx:GridViewDataCheckColumn Caption="14" VisibleIndex="0" FieldName="chkbox14" Name="chkbox14" Width="40">
                                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest2" />
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewDataTextColumn Caption="Total x Act." VisibleIndex="19" FieldName="totalavance" Name="totalavance" Width="70">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest3" />
                                    <cellstyle horizontalalign="Center" font-bold="True" backcolor="#F2F2F2">
                                    </cellstyle>
                                    <EditFormSettings Visible="false" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="u.m" Width="40" VisibleIndex="3" FieldName="um" Name="um">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                    <EditFormSettings Visible="false" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="id" FieldName="id" Name="id" 
                                    Visible="False" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <settingspager Mode="EndlessPaging" PageSize="25">
                            </settingspager>
                            <settings showfilterrow="True" ShowGroupPanel="false" VerticalScrollBarMode="auto" VerticalScrollableHeight="380" />
                            <SettingsEditing BatchEditSettings-ShowConfirmOnLosingChanges="false">
                                 <BatchEditSettings ShowConfirmOnLosingChanges="False" />
                            </SettingsEditing>
                        </dx:aspxgridview>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(document).ready(function () {
            /*Oculta secciones onLoad*/
            $("table[id*=grdDetalle] tr").each(function (index, item) {
                //$(item).children("th:eq(2), td:eq(2)").hide();
                //$(item).children("th:eq(3), td:eq(3)").hide();
                //$(item).children("th:eq(4), td:eq(4)").hide();
                //$(item).children("th:eq(6), td:eq(6)").hide();
            });

            $('#rutData').click(function () {
               
                var x = document.getElementById('#rutData');
                var value = this.value;

                if (value == "Show") {
                      /*Click ocultar panel (achicar)*/
                    this.value = "Hide"
                    $("table[id*=grdDetalle] tr").each(function (index, item) {
                        //$(item).children("th:eq(2), td:eq(2)").hide();
                        //$(item).children("th:eq(3), td:eq(3)").hide();
                        //$(item).children("th:eq(4), td:eq(4)").hide();
                        //$(item).children("th:eq(6), td:eq(6)").hide();
                        grdDetalle.SetWidth(300);
                    });

                    document.getElementById("columLeft").className = "w3-quarter w3-margin-bottom w3-content w3-container w3-padding-32";
                    document.getElementById("columRight").className = "w3-threequarter w3-content w3-container margin-bottom w3-padding-32";

                } else {
                    this.value = "Show"
                      /*Click para mostrar*/
                    $("table[id*=grdDetalle] tr").each(function (index, item) {
                        $(item).children("th:eq(2), td:eq(2)").show();
                        $(item).children("th:eq(3), td:eq(3)").show();
                        $(item).children("th:eq(4), td:eq(4)").show();
                        $(item).children("th:eq(6), td:eq(6)").show();
                    });
                    document.getElementById("columLeft").className = "w3-half w3-margin-bottom w3-content w3-container w3-padding-32";
                    document.getElementById("columRight").className = "w3-half margin-bottom w3-padding-32  w3-content w3-container";
                    grdDetalle.SetWidth(635);
                     /*Mouse encima de grilla 2 oculta panel 1*/
                    $("#columRight").hover(
                        function () {
                            $("table[id*=grdDetalle] tr").each(function (index, item) {
                                //$(item).children("th:eq(2), td:eq(2)").hide();
                                //$(item).children("th:eq(3), td:eq(3)").hide();
                                //$(item).children("th:eq(4), td:eq(4)").hide();
                                //$(item).children("th:eq(6), td:eq(6)").hide();
                            });
                            document.getElementById("columLeft").className = "w3-quarter w3-margin-bottom w3-content w3-container w3-padding-32";
                            document.getElementById("columRight").className = "w3-threequarter w3-content w3-container margin-bottom w3-padding-32";                            //grdDetalle.SetWidth(350);
                            grdDetalle.SetWidth(300);
                        }
                    );
                }
            });
        });
    </script>
   

    <!-- MODAL: Listado de Partidas (CLICK EN AGREGAR PARTIDAS) -->
    <div id="alerta" class="w3-modal">
    <div class="w3-modal-content w3-card-4 w3-animate-right" style="margin-top:-75px;">
      <!-- header -->
        <header class="w3-container w3-theme-l1" style="padding-bottom:8px"> 
       
            <span onclick="document.getElementById('alerta').style.display='none'" 
        class="w3-btn w3-display-topright w3-teal w3-padding-32 w3-hover-blue">Cerrar
        </span>
        <div class="w3-half">
<h3>
           
            <b>Selecciona actividades a asignar</b>
         </h3>
             <input class="w3-check" type="checkbox" name="" value="" id="checkTest2"> Mostrar sólo actividades con avance

        </div>
            

<div class="w3-half">
    <div class="w3-btn-bar" style="margin-top:30px;margin-right:90px;">
                    <button id="saveData" 
                        onclick="document.getElementById('alerta').style.display='none'"
                        class="w3-btn w3-small w3-blue w3-right w3-round-large w3-card-2" 
                        value="save" 
                        type="button"
                        title="Asignar Actividades" 
                        style="">ASIGNAR

                    </button>
            
                 </div>
            
            

        </div>



         </header>
       <!-- contenido -->
        <div class="w3-container w3-theme-l4">
            
             
          <div class="w3-row-padding w3-center w3-margin-bottom">    
             <div class="w3-row w3-content w3-margin-top ">
                <dx:aspxgridview id="popUpTable" runat="server" theme="Default" width="100%" enablerowscache="False"
                    font-size="X-Small" keyfieldname="chk2;cod2;partida2;um2;qvance2" clientinstancename="popUpTable"
                    autogeneratecolumns="False" enablecallbacks="False" enabletheming="True">
                            <Columns>
                                <dx:GridViewDataCheckColumn Caption=" " Width="50" VisibleIndex="1" FieldName="chk2" Name="chk2">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn Caption="Cod" Width="50" VisibleIndex="2" FieldName="cod2" Name="cod2">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Nombre Actividad" VisibleIndex="3" FieldName="partida2" Name="partida2">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" Caption="Cantidad Avance" FieldName="qavance2" Name="qavance2">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="u.m" Width="60" VisibleIndex="4" FieldName="um2" Name="um2">
                                    <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                    <cellstyle horizontalalign="Center">
                                    </cellstyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <settingspager Mode="EndlessPaging" PageSize="25">
                            </settingspager>
                            <settings showfilterrow="True" ShowGroupPanel="false" VerticalScrollBarMode="auto" VerticalScrollableHeight="350" />
                    </dx:aspxgridview>
              </div>
              
            
          </div>
      </div>
        <!-- footer -->
      <footer class="w3-container w3-teal w3-margin-bottom">
        <div class="w3-row w3-right">
          <p>Foco en Obra.</p>
        </div>
      </footer>
    </div>
  </div>


     <!-- MODAL 4: Listado de Partidas (CLICK EN AGREGAR PARTIDAS) -->
    <div id="alerta4" class="w3-modal">
    <div class="w3-modal-content w3-card-4 w3-animate-right" style="margin-top:-75px;">
      <!-- header -->
        <header class="w3-container w3-theme-l1" style="padding-bottom:8px"> 
        <span onclick="document.getElementById('alerta4').style.display='none'" 
        class="w3-btn w3-display-topright w3-teal w3-padding-32 w3-hover-blue">Cerrar
        </span>
        <div class="w3-half">
<h3>
           
            <b>Trabajadores Seleccionados</b>
         </h3>
             Trabajadores actualmente seleccionados para asignación.

        </div>
            

<div class="w3-half">
   <%-- <div class="w3-btn-bar" style="margin-top:30px;margin-right:90px;">
                    <button id="saveDataw" 
                        onclick="document.getElementById('alerta4').style.display='none'"
                        class="w3-btn w3-small w3-blue w3-right w3-round-large w3-card-2" 
                        value="save" 
                        type="button"
                        title="Asignar Actividades" 
                        style="">ASIGNAR

                    </button>
            
                 </div>--%>
            
            

        </div>
         </header>
       <!-- contenido -->
        <div class="w3-container w3-theme-l4">
            
           
          <div class="w3-row-padding w3-center w3-margin-bottom">    
             <div class="w3-row w3-content w3-margin-top ">
                

                 <dx:aspxgridview
                            id="grdDetalle4"
                            runat="server"
                            theme="Default"
                            width="100%"
                            enablerowscache="False"
                            font-size="X-Small"
                            keyfieldname="chkbox4;nombre4;estado4;rut4;especialidad4;cargo4;supervisor4"
                            clientinstancename="grdDetalle4"    
                            enablecallbacks="False" 
                            enabletheming="True"
                            SettingsEditing-Mode="Batch"
                            Settings-ShowStatusBar = "Hidden"
                            AutoGenerateColumns= "false"
                            >
                            <Columns>
                            <dx:GridViewDataCheckColumn width="45" Caption= " " FieldName="chkbox4" Name="chkbox4" VisibleIndex="1">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                            <CellStyle CssClass="claseTestCelda" />
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataTextColumn width="150" Caption="Nombre Trabajador" FieldName="nombre4" Name="nombre4" VisibleIndex="2">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Estado" FieldName="estado4" Name="estado4" VisibleIndex="3">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="R.U.T" FieldName="rut4" Name="rut4" VisibleIndex="4">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Especialidad" FieldName="especialidad4" Name="especialidad4" VisibleIndex="5">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn width="150" Caption="Cargo" FieldName="cargo4" Name="cargo4" VisibleIndex="6">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Supervisor" FieldName="supervisor4" Name="supervisor4" VisibleIndex="7">
                            <HeaderStyle HorizontalAlign="Center" CssClass="claseTest" />
                                <cellstyle horizontalalign="Center">
                                </cellstyle>
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataTextColumn>
                            </Columns>
                            <settingspager Mode="EndlessPaging" PageSize="25">
                            </settingspager>
                            <settings showfilterrow="True" ShowGroupPanel="false" VerticalScrollableHeight="240" HorizontalScrollBarMode="auto" VerticalScrollBarMode="auto" UseFixedTableLayout="true"/>
                            <SettingsEditing BatchEditSettings-ShowConfirmOnLosingChanges="false">
                            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
                            </SettingsEditing>
                            <ClientSideEvents BatchEditEndEditing="grdTrab_BatchEditEndEditing"  />
                        </dx:aspxgridview>


              </div>
              
            
          </div>
      </div>
        <!-- footer -->
      <footer class="w3-container w3-teal w3-margin-bottom">
        <div class="w3-row w3-right">
          <p>Foco en Obra.</p>
        </div>
      </footer>
    </div>
  </div>

    <script>
        var modal = document.getElementById('alerta');
        window.onclick = function (event) {
            if (event.target == alerta) {
                alerta.style.display = "none";
            }
        }
    </script>

    <script>
         /*Validaciones pasos*/
        var presionaTabla1 = false;
        document.getElementById("contenedorTabla").onmousedown = function () {
            presionaTabla1 = true;
            if (validaDate == false) {
                $("#contenedorTabla").addClass("disabledbutton");
                $("#contenedorTabla2").addClass("disabledbutton");
                $("#openMod").addClass("disabledbutton");
                document.getElementById("datepicker2").style["background-color"] = "#FFF774";
            }
          

            
        };
        document.getElementById("contenedorTabla2").onmousedown = function () {
            if (validaDate == false) {
                $("#contenedorTabla2").addClass("disabledbutton");
                $("#contenedorTabla").addClass("disabledbutton");
                $("#openMod").addClass("disabledbutton");
                document.getElementById("datepicker2").style["background-color"] = "#FFF774";

            }
            else if (selectCheckBox == false) {
                $("#contenedorTabla2").addClass("disabledbutton");
                $("#openMod").addClass("disabledbutton");
                    $("#alertaWorker").addClass("checkWorker");
            }
            else if (contador == 0) {
                $("#contenedorTabla2").addClass("disabledbutton");
                $("#openMod").addClass("disabledbutton");
                $("#alertaWorker").addClass("checkWorker");
            }
        };
    </script>
</asp:content>
